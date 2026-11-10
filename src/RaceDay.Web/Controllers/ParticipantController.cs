using Microsoft.AspNetCore.Mvc;
using RaceDay.Web.Models;
using RaceDay.Web.Services;

namespace RaceDay.Web.Controllers;

public class ParticipantController(ApiClient api) : Controller
{
    private bool IsParticipant() => api.User?.Role == UserRole.Participant;
    public async Task<IActionResult> Index()=>IsParticipant()?View(await api.Events()):RedirectToAction("Login","Account");
    public async Task<IActionResult> Event(int id){if(!IsParticipant())return RedirectToAction("Login","Account");ViewBag.Categories=await api.Categories(id);return View(await api.Event(id));}
    [HttpPost] public async Task<IActionResult> Enrol(int eventId,int categoryId){if(IsParticipant())await api.Post<object>($"api/events/{eventId}/enrollments",new{CategoryId=categoryId});return RedirectToAction(nameof(Event),new{id=eventId});}
    public async Task<IActionResult> History()=>IsParticipant()?View(await api.Get<List<EnrollmentModel>>("api/profile/enrollments")??[]):RedirectToAction("Login","Account");
}