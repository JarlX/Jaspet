using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace JaspetWEBUI.WEBUI.Areas.AdminArea.Controllers
{
    using Core.DTO;
    using Core.Result;
    using Helper.SessionHelper;
    using Newtonsoft.Json;
    using RestSharp;

    [Area("AdminArea")]
    public class UserController : Controller
    {
        private const string BaseUrl = "http://localhost:5146";
        private const string JsonContentType = "application/json";

        [HttpGet("/Admin/Users")]
        public async Task<IActionResult> Index()
        {
            var url = $"{BaseUrl}/GetAllUser";
            var client = new RestClient(url);
            var request = new RestRequest(url);
            request.AddHeader("Content-Type", JsonContentType);
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<ApiResult<List<UserDTO>>>(restResponse.Content);

            var users = responseObject.Data;
            
            return View(users);
        }

        [HttpGet("/Admin/User/{userGUID}")]
        public async Task<IActionResult> GetUser(Guid userGUID)
        {
            var url = $"{BaseUrl}/User/" + userGUID;
            var client = new RestClient(url);
            var request = new RestRequest(url);
            request.AddHeader("Content-Type", JsonContentType);
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<ApiResult<UserDTO>>(restResponse.Content);

            var users = responseObject.Data;

            return Json(new { success = true, data = responseObject.Data });
        }

        [HttpPost("/Admin/AddUser")]
        public async Task<IActionResult> AddUser(UserDTO userDto)
        {
            var url = $"{BaseUrl}/AddUser";
            var client = new RestClient(url);
            var request = new RestRequest(url,Method.Post);
            request.AddHeader("Content-Type", JsonContentType);
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedUser.Token);
            var body = JsonConvert.SerializeObject(userDto);
            request.AddBody(body, JsonContentType);
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<ApiResult<UserDTO>>(restResponse.Content);
            return Json(new { success = true, data = responseObject.Data });
        }
    }
}