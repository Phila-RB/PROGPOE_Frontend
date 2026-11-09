using Microsoft.AspNetCore.Mvc;
using PROGPOE_Frontend.Models;
using PROGPOE_Frontend.Services;

namespace PROGPOE_Frontend.Controllers
{
    public class OrganiserController : Controller
    {

        private readonly EventApi _EventApi;

        public OrganiserController(EventApi _eventApi)
        {
            _EventApi = _eventApi;
        }

        public async Task<IActionResult> Home()
        {
            var res = await _EventApi.GetAllOrganiserEvents();
            return View(res);
        }

        public async Task<IActionResult> EventView(int EventID)
        {
            var res = await _EventApi.GetEvent(EventID);
            return View(res);
        }

        public async Task<IActionResult> EditEvent(int id)
        {
            Event i = new() { EventID = id };
            //var res = await _EventApi.UpdateEvent(id);
            return View(i);
        }

        public async Task<IActionResult> Update(Event updEvent)
        {
            var res = await _EventApi.UpdateEvent(updEvent);
            return RedirectToAction("Home", "Organiser");
        }

        public async Task<IActionResult> DeleteEvent(int id)
        {
           
            var res = await _EventApi.Delete(id);
            return RedirectToAction("Home", "Organiser");
        }

        public async Task<IActionResult> Create(Event newEvent)
        {

            var res = await _EventApi.CreateNewEvent(newEvent);
            return RedirectToAction("Home", "Organiser");
        }

        public async Task<IActionResult> CreateEvent()
        {
            return View();
        }

        public async Task<IActionResult> CreateCategory()
        {
            return View();
        }

        public async Task<IActionResult> CreateCat(Category cat)
        {
            var res = await _EventApi.CreateNewCat(cat);
            return RedirectToAction("Home", "Organiser");
        }

        public async Task<IActionResult> CategoriesView()
        {
            var res = await _EventApi.GetCategories();
            return View(res);
        }

        public async Task<IActionResult> Profile()
        {
            var res = await _EventApi.GetProfile();
            return View(res);
        }
        public async Task<IActionResult> UpdateProfile()
        {
            return View();
        }
        public async Task<IActionResult> UpdateU(User user)
        {
            var res = await _EventApi.UpdateP(user);
            return RedirectToAction("Profile", "Organiser");
        }

        public async Task<IActionResult> Results()
        {
            return View();
        }

        public async Task<IActionResult> CreateResults(ParticipantResults results)
        {
            var res = await _EventApi.CreateNewResults(results);
            return RedirectToAction("Home", "Organiser");
        }
        public async Task<IActionResult> Participants(int id)
        {
            var res = await _EventApi.GetParticipants(id);
            return View(res);
        }
    }
}
