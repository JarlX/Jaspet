namespace Jaspet.API.Controllers;

using System.Net;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Business.Abstract;
using Entity.DTO.User;
using Entity.Poco;
using Entity.Result;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("Jaspet/[action]")]
public class UserController : Controller
{
    private readonly IMapper _mapper;
    private readonly IUserService _userService;

    public UserController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
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

    [HttpPost("/AddUser")]
    [ProducesResponseType(typeof(Final<UserDTOResponse>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> AddUser(UserDTORequest userDtoRequest)
    {
        User user = _mapper.Map<User>(userDtoRequest);

        var hashedPwd = HashPassword(userDtoRequest.Password);
        user.Password = hashedPwd;
        user.Guid = new Guid();
        await _userService.AddAsync(user);


        var userDtoResponse = _mapper.Map<UserDTOResponse>(user);
        return Ok(Final<UserDTOResponse>.OkWithData(userDtoResponse));
    }

    [HttpGet("/User/{guid:guid}")]
    [ProducesResponseType(typeof(Final<UserDTOResponse>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetUserByGuid(Guid guid)
    {
        var user = await _userService.GetAsync(q => q.Guid == guid);

        if (user != null)
        {
            var userDtoResponse = _mapper.Map<UserDTOResponse>(user);

            return Ok(Final<UserDTOResponse>.OkWithData(userDtoResponse));
        }

        return NotFound(Final<UserDTOResponse>.NotFound());
    }

    [HttpGet("/GetAllUser")]
    [ProducesResponseType(typeof(Final<List<UserDTOResponse>>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetlAllUser()
    {
        var users = await _userService.GetAllAsync(q => q.IsActive == true && q.IsDeleted == false);

        if (users != null)
        {
            var userDtoResponses = new List<UserDTOResponse>();

            foreach (var user in users) userDtoResponses.Add(_mapper.Map<UserDTOResponse>(user));

            return Ok(Final<List<UserDTOResponse>>.OkWithData(userDtoResponses));
        }

        return NotFound(Final<List<UserDTORequest>>.NotFound());
    }

    [HttpPut("/UpdateUser/{guid:guid}")]
    public async Task<IActionResult> UpdateUser(UserDTORequest userDtoRequest, Guid guid)
    {
        var user = await _userService.GetAsync(q => q.Guid == guid);

        user.UserName = userDtoRequest.UserName;
        user.FirstName = userDtoRequest.FirstName;
        user.LastName = userDtoRequest.LastName;
        user.Address = userDtoRequest.Address;
        user.Email = userDtoRequest.Email;
        user.PhoneNumber = userDtoRequest.PhoneNumber;

        await _userService.UpdateAsync(user);
        return Ok(Final<UserDTOResponse>.OkWithOutData());
    }

    [HttpDelete("/RemoveUser/{guid:guid}")]
    [ProducesResponseType(typeof(Final<bool>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> RemoveUser(Guid guid)
    {
        var user = await _userService.GetAsync(q => q.Guid == guid);

        user.IsActive = false;
        user.IsDeleted = true;

        await _userService.RemoveAsync(user);

        return Ok(Final<bool>.OkWithData(true));
    }
}