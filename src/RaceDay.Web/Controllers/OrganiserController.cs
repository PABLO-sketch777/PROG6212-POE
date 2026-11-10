using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc;
using RaceDay.Web.Models;
using RaceDay.Web.Services;

namespace RaceDay.Web.Controllers;

public class OrganiserController(ApiClient api) : Controller
{
    private bool IsOrganiser() => api.User?.Role == UserRole.Organiser;
    public async Task<IActionResult> Index(){if(!IsOrganiser())return RedirectToAction("Login","Account");var events=await api.Events();return View(events.Where(x=>x.OrganiserId==api.User!.Id).ToList());}
    [HttpGet] public IActionResult Create()=>IsOrganiser()?View(new EventForm()):RedirectToAction("Login","Account");
    [HttpPost] public async Task<IActionResult> Create(EventForm form){if(!IsOrganiser())return Forbid();if(!ModelState.IsValid)return View(form);var e=await api.Post<EventModel>("api/events",new{form.Name,form.Description,form.EventDate,form.Location,form.DistanceKm,form.EventType,BannerImageUrl=(string?)null});if(e is not null&&form.Banner is not null)await Upload($"api/events/{e.Id}/banner",form.Banner);return RedirectToAction(nameof(Index));}
    [HttpGet] public async Task<IActionResult> Edit(int id){if(!IsOrganiser())return Forbid();var e=await api.Event(id);return e is null?NotFound():View(new EventForm{Name=e.Name,Description=e.Description,EventDate=e.EventDate,Location=e.Location,DistanceKm=e.DistanceKm,EventType=e.EventType});}
    [HttpPost] public async Task<IActionResult> Edit(int id,EventForm form){if(!IsOrganiser())return Forbid();if(!ModelState.IsValid)return View(form);await api.Raw(HttpMethod.Put,$"api/events/{id}",JsonContent.Create(new{form.Name,form.Description,form.EventDate,form.Location,form.DistanceKm,form.EventType,BannerImageUrl=(string?)null}));if(form.Banner is not null)await Upload($"api/events/{id}/banner",form.Banner);return RedirectToAction(nameof(Index));}
    [HttpPost] public async Task<IActionResult> Delete(int id){if(IsOrganiser())await api.Delete($"api/events/{id}");return RedirectToAction(nameof(Index));}
    [HttpGet] public async Task<IActionResult> Categories(int id){if(!IsOrganiser())return Forbid();ViewBag.Event=await api.Event(id);return View(await api.Categories(id));}
    [HttpPost] public async Task<IActionResult> AddCategory(int id,CategoryForm form){if(IsOrganiser())await api.Post<CategoryModel>($"api/events/{id}/categories",form);return RedirectToAction(nameof(Categories),new{id});}
    [HttpGet] public async Task<IActionResult> Enrolments(int id){if(!IsOrganiser())return Forbid();ViewBag.Event=await api.Event(id);return View(await api.Get<List<EnrollmentModel>>($"api/events/{id}/enrollments")??[]);}
    [HttpPost] public async Task<IActionResult> Result(int id,int eventId,ResultModel result){if(IsOrganiser())await api.Post<object>($"api/enrollments/{id}/result",result);return RedirectToAction(nameof(Enrolments),new{id=eventId});}
    private async Task Upload(string path,IFormFile file){using var c=new MultipartFormDataContent();using var stream=new StreamContent(file.OpenReadStream());stream.Headers.ContentType=new MediaTypeHeaderValue(file.ContentType);c.Add(stream,"file",file.FileName);await api.Raw(HttpMethod.Post,path,c);}
}