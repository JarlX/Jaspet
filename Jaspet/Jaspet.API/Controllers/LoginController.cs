namespace Jaspet.API.Controllers;

using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Business.Abstract;
using Entity.DTO.Login;
using Entity.Result;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

[ApiController]
[Route("[action]")]
public class LoginController : Controller
{
    private readonly IConfiguration _configuration;
    private readonly IUserService _userService;

    public LoginController(IUserService userService, IConfiguration configuration)
    {
        _userService = userService;
        _configuration = configuration;
    }

    private string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var pwdBytes = Encoding.UTF8.GetBytes(password);
            var hashBytes = sha256.ComputeHash(pwdBytes);
            return Convert.ToBase64String(hashBytes);
        }
    }

    [HttpPost("/Login")]
    [ProducesResponseType(typeof(Final<LoginDTOResponse>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> LoginAsync(LoginDTORequest loginDtoRequest)
    {
        var hashedPwd = HashPassword(loginDtoRequest.Password);
        loginDtoRequest.Password = hashedPwd;

        var user = await _userService.GetAsync(q =>
            q.UserName == loginDtoRequest.UserName && q.Password == loginDtoRequest.Password);

        if (user == null)
        {
            if (user?.UserName != loginDtoRequest.Password || user.Password != loginDtoRequest.Password)
                return NotFound(Final<LoginDTOResponse>.UserInputError());

            return Unauthorized(Final<LoginDTOResponse>.AuthenticationError());
        }

        var key = Encoding.UTF8.GetBytes(_configuration.GetValue<string>("AppSettings:JWTKey"));

        var claims = new List<Claim>();
        claims.Add(new Claim("KullanıcıAdı", user.UserName));
        claims.Add(new Claim("KullanıcıId", user.Id.ToString()));

        var jwt = new JwtSecurityToken(
            expires: DateTime.Now.AddDays(15),
            claims: claims,
            issuer: "http://jaspet.com",
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature));

        var token = new JwtSecurityTokenHandler().WriteToken(jwt);

        var loginDtoResponse = new LoginDTOResponse
        {
            FullName = string.Concat(user.FirstName, " ", user.LastName),
            UserId = user.Id,
            Token = token,
            UserName = user.UserName,
            UserRole = user.UserRole,
            Email = user.Email,
            Address = user.Address,
            Password = user.Password
        };
        return Ok(Final<LoginDTOResponse>.OkWithData(loginDtoResponse));
    }
}