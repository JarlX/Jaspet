namespace Jaspet.API.Middleware;

using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Helper.CustomException;
using Helper.Globals;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
public class ApiAuthorizationMiddleware
{
    private readonly IConfiguration _configuration; // AppSettingse erişim
    private readonly IOptionsMonitor<JWTExceptURLList> _jwtExceptionList;
    private readonly RequestDelegate _next;

    public ApiAuthorizationMiddleware(RequestDelegate next, IOptionsMonitor<JWTExceptURLList> jwtExceptionList,
        IConfiguration configuration)
    {
        _next = next;
        _jwtExceptionList = jwtExceptionList;
        _configuration = configuration;
    }

    public Task Invoke(HttpContext httpContext)
    {
        if (!_jwtExceptionList.CurrentValue.URLList.Contains(httpContext.Request.Path))
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            string authHeader = httpContext.Request.Headers["Authorization"];

            if (authHeader != null)
            {
                var token = authHeader.Split(" ")[1];
                var key = Encoding.UTF8.GetBytes(_configuration.GetValue<string>("AppSettings:JWTKey"));

                jwtHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                }, out var validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                if (jwtToken.ValidTo < DateTime.Now) throw new TokenException();
            }
            else
            {
                throw new TokenNotFoundException();
            }
        }

        return _next(httpContext);
    }
}

// Extension method used to add the middleware to the HTTP request pipeline.
public static class ApiAuthorizationMiddlewareExtensions
{
    public static IApplicationBuilder ApiAuthorizationMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ApiAuthorizationMiddleware>();
    }
}