using Excellerent.APIModularization.Common;
using Excellerent.APIModularization.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Security.Claims;

namespace Excellerent.APIModularization.Controllers
{
    [Authorize]
    public class AuthorizedController : ControllerBase
    {
        private readonly IHttpContextAccessor htttpContextAccessor;
        private readonly IBusinessLog _businessLog;
        private readonly IConfiguration _configuration;
        private readonly string _feature;
        private readonly string _environmentName;


        public AuthorizedController(IHttpContextAccessor htttpContextAccessor, IConfiguration configuration, IBusinessLog _businessLog, string feature)
        {
            this.htttpContextAccessor = htttpContextAccessor;
            this._businessLog = _businessLog;
            _configuration = configuration;
            _feature = feature;
            _environmentName = this._configuration["EnvironmentName"] == null ? string.Empty : this._configuration["EnvironmentName"];

        }

        public UserModel CurrentUser
        {
            get
            {
                var claimsIdentity = this.htttpContextAccessor.HttpContext.User;
                var userName = claimsIdentity.Claims.FirstOrDefault(cl => cl.Type == ClaimTypes.Actor);
                var userID = claimsIdentity.Claims.FirstOrDefault(cl => cl.Type == "UserID");
                var name = claimsIdentity.Claims.FirstOrDefault(cl => cl.Type == ClaimTypes.Name);
                var firstName = claimsIdentity.Claims.FirstOrDefault(cl => cl.Type == "FirstName");
                var middleName = claimsIdentity.Claims.FirstOrDefault(cl => cl.Type == "MiddelName");
                var lastName = claimsIdentity.Claims.FirstOrDefault(cl => cl.Type == "LastName");
                var email = claimsIdentity.Claims.FirstOrDefault(cl => cl.Type == ClaimTypes.Email);

                var member = new UserModel()
                {
                    Username = userName.Value,
                    UniqueID = userID != null ? Convert.ToInt32(userID.Value) : 0,
                    FullName = name != null ? name.Value : string.Empty,
                    Email = email != null ? email.Value : string.Empty,
                    FirstName = firstName != null ? firstName.Value : string.Empty,
                    LastName = lastName != null ? lastName.Value : string.Empty,
                    MiddleName = middleName != null ? middleName.Value : string.Empty
                };

                return member;
            }
        }
    }
}
