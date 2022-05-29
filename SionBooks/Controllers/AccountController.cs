using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SionBooks.Areas.Admin.Models;
using SionBooks.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SionBooks.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        private readonly SionBooksContext _context;
        public AccountController(SionBooksContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([Bind("Email,Password")] AccountModels.LoginModel model)
        {

            if (ModelState.IsValid)
            {

                var user = _context.User.Where(f => f.email == model.Email && f.password == model.Password).FirstOrDefault();

                if (user == null || user.isDeleted)
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View();
                }

                string role = "User";

                if (user.isAdmin)
                {
                    role = "Admin";
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.email),
                    new Claim(ClaimTypes.Role, role)
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    principal,
                    new AuthenticationProperties { IsPersistent = true });

            }
            return RedirectToAction("SearchBook", "Search", new { area = "" });
        }

        [HttpGet]
        public void Logout()
        {


        }

        [HttpPost]
        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (returnUrl != null && returnUrl != "")
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToAction("SearchBook", "Search", new { area = "" });
            }
        }
    }
}
