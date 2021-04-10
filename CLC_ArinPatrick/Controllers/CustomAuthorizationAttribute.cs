using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Minesweeper_ArinPatrick.Controllers
{
    internal class CustomAuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string userKey = context.HttpContext.Session.GetString("username");
            if(userKey == null)
            {
                context.Result = new RedirectResult("/User/Login");
            }
            else
            {

            }
        }
    }
}