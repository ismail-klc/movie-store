using Business.Helpers.Jwt;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Helpers
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IJwtService jwtService)
        {
            var token = context.Request.Cookies["jwt"];

            if (token != null)
                attachUserToContext(context, jwtService, token);


            await _next(context);
        }

        private void attachUserToContext(HttpContext context, IJwtService jwtService, string jwtToken)
        {
            try
            {
                var token = jwtService.Verify(jwtToken);

                var id = token.Claims.Where(x => x.Type == "id").SingleOrDefault().Value;
                var role = token.Claims.Where(x => x.Type == "role").SingleOrDefault().Value;

                // attach user to context on successful jwt validation
                context.Items["User"] = new TokenUserDto
                {
                    Id = int.Parse(id),
                    Role = role
                };
            }
            catch
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }
    }
}