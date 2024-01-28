using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;

namespace Excellerent.ProjectManagement.Presentation.Validations
{
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        private class ResponseDTO
        {
            public string ResponseStatus { get; set; }
            public string Message { get; set; }
            public List<string> Ex { get; set; }
            public dynamic Data { get; set; }
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values.Where(v => v.Errors.Count > 0)
                        .SelectMany(v => v.Errors)
                        .Select(v => v.ErrorMessage)
                        .ToList();

                var responseObj = new ResponseDTO
                {
                    ResponseStatus = "error",
                    Message = "Bad Request",
                    Data = null,
                    Ex = errors
                };

                context.Result = new JsonResult(responseObj)
                {
                    StatusCode = 400
                };
            }
        }
    }
}