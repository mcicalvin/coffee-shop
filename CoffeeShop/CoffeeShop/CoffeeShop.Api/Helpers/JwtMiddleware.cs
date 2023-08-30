using CoffeeShop.Domain.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace CoffeeShop.Api.Helpers
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration configuration;
        private readonly ILogger<JwtMiddleware> logger;

        public JwtMiddleware(RequestDelegate next, IConfiguration configuration, ILogger<JwtMiddleware> logger)
        {
            _next = next;
            this.configuration = configuration;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context, IAuthenticateService authenticateService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                attachUserToContext(context, authenticateService, token);

            await _next(context);
        }

        private void attachUserToContext(HttpContext context, IAuthenticateService authenticateService, string token)
        {
            try
            {
                var secret = configuration.GetValue<string>("AppSettings:Secret");
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(secret);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                   
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = jwtToken.Claims.First(x => x.Type == "id").Value;

               
                var response = authenticateService.GetUserById(userId);
                context.Items["User"] = response.Data;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
               
            }
        }
    }
}
