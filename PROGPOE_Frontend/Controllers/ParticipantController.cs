using Microsoft.AspNetCore.Mvc;
using PROGPOE_Frontend.Models;
using PROGPOE_Frontend.Services;

namespace PROGPOE_Frontend.Controllers
{
    public class ParticipantController : Controller
    {
        private readonly EventApi _EventApi;

        public ParticipantController(EventApi _eventApi)
        {
            _EventApi = _eventApi;
        }
        public async Task<IActionResult> Home()
        {
            var res = await _EventApi.GetAllEvents();
            return View(res);
        }

        public async Task<IActionResult> EventView(int EventID)
        {
            Console.WriteLine(EventID);
            var res = await _EventApi.GetEvent(EventID);
            return View(res);
        }
        public async Task<IActionResult> CategoriesView()
        {
            var res = await _EventApi.GetCategories();
            return View(res);
        }

        public async Task<IActionResult> JoinEvent(int id)
        {
            var res = await _EventApi.GetCategory(id);
            return View(res);
        }
        public async Task<IActionResult> Signup(Category cat)
        {
            Console.WriteLine(cat.CategoryID + cat.EventID);
            Signup signup = new() { EventID = cat.EventID , CategoryID = cat.CategoryID};
            var res = await _EventApi.SignupEvent(signup);
            var res2 = await _EventApi.GetJoinedEvents();
            return RedirectToAction("SignedUp", "Participant");
        }
        public async Task<IActionResult> Profile()
        {
            var res = await _EventApi.GetProfile();
            return View(res);
        }

        public async Task<IActionResult> Results()
        {
            var res = await _EventApi.GetResults();
            return View(res);
        }

        public async Task<IActionResult> SignedUp()
        {
            var res = await _EventApi.GetJoinedEvents();
            return View(res);
        }
    }
}
