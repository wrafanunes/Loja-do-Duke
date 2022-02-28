using Loja_do_Duke.Data;
using Loja_do_Duke.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loja_do_Duke.Controllers
{
    public class InventoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _user;

        public InventoryController(ApplicationDbContext db, UserManager<IdentityUser> user)
        {
            _db = db;
            _user = user;
        }

        public IActionResult Index()
        {
            string userId = _user.GetUserId(User);
            IEnumerable<ApplicationUserSupply> userSupplies = _db.ApplicationUserSupplies.Where(u => u.UserId == userId);
            return View(userSupplies);
        }
    }
}
