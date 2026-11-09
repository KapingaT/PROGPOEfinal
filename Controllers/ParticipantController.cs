using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RacedayFINAL.Services;
using RacedayFINAL.Models;
using RacedayFINAL.Services;

public class ParticipantController : Controller
{
    private readonly IApiService _apiService;
    public ParticipantController(IApiService apiService) => _apiService = apiService;

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (HttpContext.Session.GetString("UserRole") != "Participant")
            context.Result = RedirectToAction("Login", "Account");
        base.OnActionExecuting(context);
    }


    public async Task<IActionResult> Home()
    {
        // 1. Get the data from the API (This returns EventDTOs)
        var eventsDto = await _apiService.GetEventsAsync();

        // 2. Translate the DTOs into ViewModels so the View understands them
        var eventsVm = eventsDto
            .Where(e => e.Date >= DateTime.Now)
            .OrderBy(e => e.Date)
            .Select(e => new EventViewModel
            {
                EventId = e.EventId,       // We need to add this to EventDTO!
                Name = e.Name,
                Description = e.Description,
                Date = e.Date,
                Location = e.Location,
                Distance = e.Distance,
                EventType = e.EventType
            })
            .ToList();

        // 3. Send the correct model to the View
        return View(eventsVm);
    }

    public async Task<IActionResult> EventDetails(int id)
    {
        var ev = await _apiService.GetEventByIdAsync(id);
        if (ev == null) return NotFound();

        // Store categories in ViewBag for the enrol form
        ViewBag.Categories = await _apiService.GetCategoriesForEventAsync(id);
        return View(ev);
    }

    [HttpPost]
    public async Task<IActionResult> Enrol(int eventId, int categoryId)
    {
        var success = await _apiService.EnrollAsync(new EnrollmentDto { EventId = eventId, CategoryId = categoryId });
        if (success) TempData["Success"] = "Successfully enrolled!";
        else TempData["Error"] = "Could not enrol. You may already be enrolled.";
        return RedirectToAction("MyEnrollments");
    }

    public async Task<IActionResult> MyEnrollments()
    {
        var enrolments = await _apiService.GetMyEnrollmentsAsync();
        return View(enrolments);
    }
}