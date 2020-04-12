using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SocialNetworkMVC.Models;

namespace SocialNetworkMVC.AOP
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class AuthenticatedAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var dbContext = context.HttpContext.RequestServices.GetService<ApplicationDbContext>();
            var userIdStr = context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdStr))
            {
                context.Result = new RedirectToActionResult("AccessDenied", "Account", null);
            }
            else
            {
                var user = dbContext.Users.Find(int.Parse(userIdStr));
                if (user == null)
                {
                    context.Result = new RedirectToActionResult("AccessDenied", "Account", null);
                }
            }

            base.OnActionExecuting(context);
        }
    }
}