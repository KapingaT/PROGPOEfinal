using Microsoft.AspNetCore.Mvc.Diagnostics;
using RacedayFINAL.Services;
using RacedayFINAL.Models;
using System.Net.Http.Json;

namespace RaceDayMVC.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7011/Account/Register"; 

        public ApiService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _baseUrl = config["ApiBaseUrl"]; // This must match the key in appsettings.json
        }
        // wee use this class to send data to the backend 

        public async Task<LoginResponse?> LoginAsync(LoginDto request)
        {
            // Notice the URL is using _baseUrl + "auth/login" (lowercase 'a' matches your API route)
            var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}Auth/login", request);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<LoginResponse>();
            return null;
        }


        public async Task<bool> RegisterAsync(RegistrationDTO request)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}Auth/register", request);
            return response.IsSuccessStatusCode;
        }

        public async Task LogoutAsync()
        {
            await _httpClient.PostAsync($"{_baseUrl}Auth/logout", null);
        }

        public async Task<List<EventViewModel>> GetEventsAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<EventViewModel>>($"{_baseUrl}Events");
            return result ?? new List<EventViewModel>();
        }

        public async Task<EventViewModel?> GetEventByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<EventViewModel>($"{_baseUrl}Events/{id}");
        }

        public async Task<List<CategoryViewModel>> GetCategoriesForEventAsync(int eventId)
        {
            var result = await _httpClient.GetFromJsonAsync<List<CategoryViewModel>>($"{_baseUrl}Categories/event/{eventId}");
            return result ?? new List<CategoryViewModel>();
        }

        public async Task<bool> CreateEventAsync(EventDTO newEvent)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}Events", newEvent);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> EnrollAsync(EnrollmentDto request)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}Enrollments", request);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<MyEnrollmentViewModel>> GetMyEnrollmentsAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<MyEnrollmentViewModel>>($"{_baseUrl}Enrollments/mine");
            return result ?? new List<MyEnrollmentViewModel>();
        }
    }
}