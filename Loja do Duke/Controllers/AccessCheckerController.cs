using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loja_do_Duke.Controllers
{
    [Authorize]
    public class AccessCheckerController : Controller
    {
        [AllowAnonymous]
        public IActionResult AllAccess()
        {
            return View();
        }

        [Authorize]
        public IActionResult AuthorizedAccess()
        {
            return View();
        }

        public IActionResult UserAccess()
        {
            return View();
        }

        public IActionResult AdminAccess()
        {
            return View();
        }

        public IActionResult Admin_CreateAccess()
        {
            return View();
        }

        public IActionResult Admin_Create_Edit_DeleteAccess()
        {
            return View();
        }

        public IActionResult Admin_Create_Edit_DeleteAccess_SuperAdmin()
        {
            return View();
        }
    }
}
