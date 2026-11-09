using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PROGPOE.Models;
using PROGPOE_Frontend.Models;
using System.Net.Http.Headers;

namespace PROGPOE_Frontend.Services
{
    public class EventApi
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public EventApi(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        [Authorize(Roles = "Participant")]
        [HttpGet]
        public async Task<List<Event>> GetAllEvents()
        {

            var token = await _httpContextAccessor.HttpContext!.GetTokenAsync("access_token");

            using var request = new HttpRequestMessage(
                HttpMethod.Get,
                "api/participant/events"
            );

            request.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var res = await _httpClient.SendAsync(request);

            if (res == null) { 
                return new List<Event> { };
            }
            //Console.WriteLine(await res.Content.ReadFromJsonAsync<List<Event>>());
            return await res.Content.ReadFromJsonAsync<List<Event>>();
        }

        [Authorize(Roles = "Organiser")]
        [HttpGet]
        public async Task<List<OrganiserEvents>> GetAllOrganiserEvents()
        {

            var token = await _httpContextAccessor.HttpContext!.GetTokenAsync("access_token");

            using var request = new HttpRequestMessage(
                HttpMethod.Get,
                "api/organiser/events"
            );

            request.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var res = await _httpClient.SendAsync(request);

            if (res == null)
            {
                return new List<OrganiserEvents> { };
            }
            //Console.WriteLine(await res.Content.ReadFromJsonAsync<List<Event>>());
            return await res.Content.ReadFromJsonAsync<List<OrganiserEvents>>();
        }

        [Authorize]
        [HttpPut]
        public async Task<bool> UpdateEvent(Event updEvent)
        {
            var token = await _httpContextAccessor.HttpContext!.GetTokenAsync("access_token");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var res = await _httpClient.PutAsJsonAsync<Event>($"api/events/{updEvent.EventID}", updEvent);

            return res.IsSuccessStatusCode;
        }

        [Authorize]
        [HttpPost]
        public async Task<bool> CreateNewEvent(Event newEvent)
        {
            var token = await _httpContextAccessor.HttpContext!.GetTokenAsync("access_token");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var res = await _httpClient.PostAsJsonAsync<Event>($"api/events", newEvent);

            return res.IsSuccessStatusCode;
        }

        [Authorize]
        [HttpGet]
        public async Task<Event> GetEvent(int id)
        {

            var token = await _httpContextAccessor.HttpContext!.GetTokenAsync("access_token");

            using var request = new HttpRequestMessage(
                HttpMethod.Get,
                $"api/events/{id}"
            );

            request.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var res = await _httpClient.SendAsync(request);

            if (res == null)
            {
                return new Event{ };
            }
            //Console.WriteLine(await res.Content.ReadFromJsonAsync<List<Event>>());
            return await res.Content.ReadFromJsonAsync<Event>();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<bool> Delete(int id)
        {
            //Console.WriteLine("front2  reached");
            var token = await _httpContextAccessor.HttpContext!
                .GetTokenAsync("access_token");
            Console.WriteLine(id);
            using var request = new HttpRequestMessage(
                HttpMethod.Delete,
                $"api/events/{id}"
            );
            //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            request.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var res = await _httpClient.SendAsync(request);

            return res.IsSuccessStatusCode;
        }

        [Authorize]
        [HttpGet]
        public async Task<List<Category>> GetCategories()
        {
            var token = await _httpContextAccessor.HttpContext!.GetTokenAsync("access_token");

            using var request = new HttpRequestMessage(
                HttpMethod.Get,
                "api/categories"
            );

            request.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var res = await _httpClient.SendAsync(request);

            if (res == null)
            {
                return new List<Category> { };
            }
            //Console.WriteLine(await res.Content.ReadFromJsonAsync<List<Event>>());
            return await res.Content.ReadFromJsonAsync<List<Category>>();
        }
      


        [Authorize]
        [HttpGet]
        public async Task<List<Category>> GetCategory(int id)
        {
            var token = await _httpContextAccessor.HttpContext!.GetTokenAsync("access_token");

            using var request = new HttpRequestMessage(
                HttpMethod.Get,
                $"api/event/{id}/categories"
            );

            request.Headers.Authorization =
                            new AuthenticationHeaderValue("Bearer", token);

            var res = await _httpClient.SendAsync(request);

            if (res == null)
            {
                return new List<Category> { };
            }
            //Console.WriteLine(await res.Content.ReadFromJsonAsync<List<Event>>());
            return await res.Content.ReadFromJsonAsync<List<Category>>();
        }

        [Authorize]
        [HttpGet]
        public async Task<bool> SignupEvent(Signup signup)
          {
            var token = await _httpContextAccessor.HttpContext!.GetTokenAsync("access_token");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var res = await _httpClient.PostAsJsonAsync($"api/category/{signup.CategoryID}/participate", signup);

            return res.IsSuccessStatusCode;
        }

        [Authorize]
        [HttpGet]
        public async Task<User> GetProfile()
        {

            var token = await _httpContextAccessor.HttpContext!.GetTokenAsync("access_token");

            using var request = new HttpRequestMessage(
                HttpMethod.Get,
                "api/auth/profile"
            );

            request.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var res = await _httpClient.SendAsync(request);

            if (res == null)
            {
                return new User { };
            }
            //Console.WriteLine(await res.Content.ReadFromJsonAsync<List<Event>>());
            return await res.Content.ReadFromJsonAsync<User>();
        }

        [HttpGet]
        public async Task<List<ParticipantResultsResponse>> GetResults()
        {

            var token = await _httpContextAccessor.HttpContext!.GetTokenAsync("access_token");

            using var request = new HttpRequestMessage(
                HttpMethod.Get,
                "api/auth/profile/results"
            );

            request.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var res = await _httpClient.SendAsync(request);

            if (res == null)
            {
                return new List<ParticipantResultsResponse> { };
            }
            //Console.WriteLine(await res.Content.ReadFromJsonAsync<List<Event>>());
            return await res.Content.ReadFromJsonAsync<List<ParticipantResultsResponse>>();
        }

        [HttpPut]
        public async Task<List<ParticipantResultsResponse>> UpdateProfile(User user)
        {
            var token = await _httpContextAccessor.HttpContext!.GetTokenAsync("access_token");

            using var request = new HttpRequestMessage(
                HttpMethod.Get,
                "api/auth/profile/results"
            );

            request.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var res = await _httpClient.SendAsync(request);

            if (res == null)
            {
                return new List<ParticipantResultsResponse> { };
            }
            //Console.WriteLine(await res.Content.ReadFromJsonAsync<List<Event>>());
            return await res.Content.ReadFromJsonAsync<List<ParticipantResultsResponse>>();
        }

        [HttpGet]
        public async Task<List<SignedUpResponse>> GetJoinedEvents()
        {
            var token = await _httpContextAccessor.HttpContext!.GetTokenAsync("access_token");

            using var request = new HttpRequestMessage(
                HttpMethod.Get,
                "api/participating"
            );

            request.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var res = await _httpClient.SendAsync(request);

            if (res == null)
            {
                return new List<SignedUpResponse> { };
            }
            //Console.WriteLine(await res.Content.ReadFromJsonAsync<List<Event>>());
            return await res.Content.ReadFromJsonAsync<List<SignedUpResponse>>();
        }
    }
}
