using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;
using RacedayFINAL.Services;
using RacedayFINAL.Models;


public class OrganiserController : Controller
{
    private readonly IApiService _apiService;
    public OrganiserController(IApiService apiService) => _apiService = apiService;

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (HttpContext.Session.GetString("UserRole") != "Organiser")
            context.Result = RedirectToAction("Login", "Account");
        base.OnActionExecuting(context);
    }

    public async Task<IActionResult> Dashboard()
    {
        var events = await _apiService.GetEventsAsync();
        return View(events);
    }

    [HttpGet] public IActionResult CreateEvent() => View();

    [HttpPost]
    public async Task<IActionResult> CreateEvent(EventDTO model)
    {
        if (!ModelState.IsValid) return View(model);
        var success = await _apiService.CreateEventAsync(model);
        if (success)
        {
            TempData["Success"] = "Event created successfully!";
            return RedirectToAction("Dashboard");
        }
        ModelState.AddModelError("", "Could not create event.");
        return View(model);
    }
}