﻿using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "User")]
        public IActionResult UserAccess()
        {
            return View();
        }

        [Authorize(Roles = "User,Admin")]
        public IActionResult UserORAdminAccess()
        {
            return View();
        }

        [Authorize(Policy = "UserAndAdmin")]
        public IActionResult UserANDAdminAccess()
        {
            return View();
        }

        [Authorize(Policy = "Admin")]
        public IActionResult AdminAccess()
        {
            return View();
        }

        [Authorize(Policy = "Admin_CreateAccess")]
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
