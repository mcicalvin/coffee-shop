using CoffeeShop.Domain.Enums;
using CoffeeShop.Domain.Models.Responses;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Api.Helpers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = (UserResponse)context.HttpContext.Items["User"];

            if (user == null)
            {
              
                context.Result = new JsonResult(
                    new ObjectResponse<object>
                    {
                        StatusCode = ResponseStatus.Unauthorized,
                        Message = "You are not authorized to access this resources.",
                        Data = null
                    })
                {

                    StatusCode = StatusCodes.Status401Unauthorized
                };
            }
        }
    }
}
