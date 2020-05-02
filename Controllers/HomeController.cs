using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using PgBookStore.Models;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;

namespace PgBookStore.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly SignInManager<ApplicationUser> _sigInManager;
        //private readonly string _externalCookieScheme;

        private readonly ILogger<HomeController> _logger;

        public HomeController(SignInManager<ApplicationUser> signInManager, ILogger<HomeController> logger)
        {
            _sigInManager = signInManager;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginFormViewModel item, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid) {
                var result = await _sigInManager.PasswordSignInAsync(item.UserName, item.Password, isPersistent: false, lockoutOnFailure : false);
                if (result.Succeeded) return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _sigInManager.SignOutAsync();
            return Redirect("/");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
