using Excellerent.SharedModules.DTO;
using Excellerent.Usermanagement.Domain.Entities;
using Excellerent.Usermanagement.Domain.Interfaces.ServiceInterfaces;
using Excellerent.Usermanagement.Presentation.MailService;
using Excellerent.UserManagement.Presentation.Helper;
using Excellerent.UserManagement.Presentation.Models.GetModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using UAParser;

namespace Excellerent.UserManagement.Presentation.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class PasswordResetController : Controller
    {
        private readonly IPasswordResetTokenService _service;
        private readonly IUserService _userService;
        private readonly IOptions<MailSenderOptions> _smtpOption;
        public PasswordResetController(IPasswordResetTokenService service, IUserService userService, ISendMailService mailService, IOptions<MailSenderOptions> smtpOption)
        {
            this._service = service;
            this._userService = userService;
            MailService = mailService;
            _smtpOption = smtpOption;
        }

        public ISendMailService MailService { get; }

        [HttpPost("ApplyRequestForPasswordReset")]
        public async Task<ResponseDTO> ApplyRequestForPasswordReset(string Email, string resetHandlingURL)
        {
            var user = await this._userService.GetUserByEmail(Email);
            if (user == null)
            {
                return new ResponseDTO
                {
                    ResponseStatus = ResponseStatus.Error,
                    Message = "User not found",
                };
            } else if (!user.IsActive || user.IsDeleted)
            {
                return new ResponseDTO
                {
                    ResponseStatus = ResponseStatus.Error,
                    Message = "In-Active Account",
                };
            }
            PasswordResetTokenEntity token = new PasswordResetTokenEntity()
            {
                UserId = user.Guid,
                Token = PasswordCryptographyPbkdf2.CreateToken(),
                TokenExpiry = DateTime.Now.AddMinutes(30)
            };
            var dto = await this._service.Add(token);
            if (dto.ResponseStatus == ResponseStatus.Success)
            {
                MailSenderOptions sender = this._smtpOption.Value;
                var agent = Request.Headers.ContainsKey("User-Agent")? Request.Headers["User-Agent"][0].ToString():"";
                var clientInfo = Parser.GetDefault().Parse(agent);
                sender.RequestInfo = $"OS: {clientInfo.OS} Browser: {clientInfo.UA} Time: {DateTime.Now} IP: {ipAddress()}";
                await this.MailService.NotifyPasswordResetLink(sender, user, $"{resetHandlingURL}?uid={Email}&&token={token.Token}");
            }
            return new ResponseDTO
            {
                ResponseStatus = ResponseStatus.Success,
                Message = "Sucessfully reset password",
            };
        }


        [HttpGet("VerifyResetPassword")]
        public async Task<ResponseDTO> VerifyResetPassword(string Email, string Token)
        {
            var user = await this._userService.GetUserByEmail(Email);
            if (user == null || user.IsDeleted == true)
            {
               return new ResponseDTO(ResponseStatus.Error, "User Invalid", null);
            }
            var passwordResetTokenEntities = await this._service.GetPasswordResetTokenByUserId(user.Guid);
            var entity = passwordResetTokenEntities.FirstOrDefault(t => t.Token == Token.Replace(' ', '+'));
            if (entity == null)
                return new ResponseDTO(ResponseStatus.Error, "Token Invalid", null);

            if (entity.TokenExpiry < DateTime.Now)
                return new ResponseDTO(ResponseStatus.Error, "Token Expired", null);

            return new ResponseDTO(ResponseStatus.Success, "Token Valid", null);

        }
        private string ipAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}
