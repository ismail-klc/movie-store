using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;


[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    public string Role { get; set; }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = (TokenUserDto)context.HttpContext.Items["User"];
        
        if (user == null || (Role != null && Role.ToLower() != user.Role.ToLower()))
        {
            // not logged in
            context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
        }

    }
}