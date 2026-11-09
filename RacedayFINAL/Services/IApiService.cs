using Microsoft.AspNetCore.Mvc.Diagnostics;
using RacedayFINAL.Models;

namespace RacedayFINAL.Services
{
    public interface IApiService
    {
        Task<LoginResponse?> LoginAsync(LoginDto request);
        Task<bool> RegisterAsync(RegistrationDTO request);
        Task LogoutAsync();
        Task<List<EventViewModel>> GetEventsAsync();
        Task<List<CategoryViewModel>> GetCategoriesForEventAsync(int eventId);
        Task<bool> CreateEventAsync(EventDTO newEvent);
        Task<bool> EnrollAsync(EnrollmentDto request);
        Task<List<MyEnrollmentViewModel>> GetMyEnrollmentsAsync();
        Task<EventViewModel?> GetEventByIdAsync(int id);
    }
}