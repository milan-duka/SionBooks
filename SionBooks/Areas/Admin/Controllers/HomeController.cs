using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SionBooks.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        [Authorize(AuthenticationSchemes = "Cookies", Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
