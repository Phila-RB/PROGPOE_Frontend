using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using PROGPOE.Models;
using PROGPOE_Frontend.Models;
using PROGPOE_Frontend.Services;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Security.Principal;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PROGPOE_Frontend.Controllers
{
    public class LoginController : Controller
    {
        private readonly LoginApi _LogApi;

        public LoginController(LoginApi logApi)
        {
            _LogApi = logApi;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            var res = await _LogApi.RegisterUser(user);
            if(res)
            {
                return RedirectToAction("Index", "Login");
            }

            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            var res = await _LogApi.LoginUser(user);
            if (res == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var claims = new List<Claim> { new Claim(ClaimTypes.Sid, res.UserID.ToString()), new Claim(ClaimTypes.Name, res.Name), new Claim(ClaimTypes.Surname, res.Surname), new Claim(ClaimTypes.Role, res.Access) };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(claimsIdentity);

            // Store the API token in the protected authentication ticket
            var properties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
            };

            properties.StoreTokens(new[]
            {
                    new AuthenticationToken
                    {
                        Name = "access_token",
                        Value = res.Token
                    }
                });

            // Create MVC authentication cookie
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                properties
            );

            if (res.Access.Equals("Organiser")) { 
                return RedirectToAction("Home", "Organiser");
            }
            return RedirectToAction("Home", "Participant");

        }

        //logout
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Logout()
        //{
        //    await HttpContext.SignOutAsync(
        //        CookieAuthenticationDefaults.AuthenticationScheme
        //    );

        //    return RedirectToAction("Login", "Account");
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(Student student)
        //{
        //    if (!ModelState.IsValid)
        //        return View(student);

        //    try
        //    {
        //        if (!await _studentApi.CreateAsync(student))
        //        {
        //            ModelState.AddModelError(
        //            string.Empty,
        //            "The API could not create the student.");
        //            return View(student);
        //        }

        //        TempData["Success"] = "Student created successfully.";
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch (HttpRequestException)
        //    {
        //        ModelState.AddModelError(
        //        string.Empty,
        //        "Could not connect to the Student API.");
        //        return View(student);
        //    }
        //}



    }
}
