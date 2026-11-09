using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using PROGPOE.Models;
using PROGPOE_Frontend.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PROGPOE_Frontend.Services
{
    public class LoginApi
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LoginApi(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> RegisterUser(User user)
        {
            var res = await _httpClient.PostAsJsonAsync("api/auth/register", user);
          
            return res.IsSuccessStatusCode;
        }

        [HttpPost]
        public async Task<LoginResponse> LoginUser(User user)
        {
            var res = await _httpClient.PostAsJsonAsync(
                "api/auth/login",
                user
            );


            if (!res.IsSuccessStatusCode)
            {
                return null;
            }

            return await res.Content.ReadFromJsonAsync<LoginResponse>();
            //var res = await _httpClient.PostAsJsonAsync("auth/login",user);
            //var json = await res.Content.ReadFromJsonAsync<LoginResponse>();
            //return json;
            ////res.EnsureSuccessStatusCode();

            //Console.WriteLine("Status: " + res.StatusCode);


            //return await res.Content.ReadFromJsonAsync<LoginResponse>();
        }

        //var token = await _httpContextAccessor.HttpContext!.GetTokenAsync("access_token");

        //using var request = new HttpRequestMessage(
        //    HttpMethod.Post,
        //    "api/auth/login"
        //);

        //request.Headers.Authorization =
        //    new AuthenticationHeaderValue("Bearer", token);

}
}
