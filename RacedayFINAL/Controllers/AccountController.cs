using Microsoft.AspNetCore.Mvc;
using RacedayFINAL.Services;
using RacedayFINAL.Models;
using RacedayFINAL.Services;

public class AccountController : Controller
{// where we handle the log in 
    private readonly IApiService _apiService;
    public AccountController(IApiService apiService) => _apiService = apiService;

    [HttpGet] public IActionResult Login() => View();
    [HttpGet] public IActionResult Register() => View();

    [HttpPost]
    [HttpPost]
    public async Task<IActionResult> Login(LoginDto model)
    {
        if (!ModelState.IsValid) return View(model);
        var result = await _apiService.LoginAsync(model);

        if (result == null || string.IsNullOrEmpty(result.Role))
        {
            ModelState.AddModelError("", "Invalid email or password.");
            return View(model);
        }

        // Save to session
        HttpContext.Session.SetInt32("UserId", result.UserId);
        HttpContext.Session.SetString("UserRole", result.Role);

        // Redirect based on role
        if (result.Role == "Organiser")
            return RedirectToAction("Dashboard", "Organiser");
        else
            return RedirectToAction("Home", "Participant");
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegistrationDTO model)
    {
        if (!ModelState.IsValid) return View(model);
        var success = await _apiService.RegisterAsync(model);
        if (!success)
        {
            ModelState.AddModelError("", "Registration failed. Email may already be in use.");
            return View(model);
        }
        TempData["Success"] = "Registration successful! Please log in.";
        return RedirectToAction("Login");
    }

    public async Task<IActionResult> Logout()
    {
        await _apiService.LogoutAsync();
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }
}