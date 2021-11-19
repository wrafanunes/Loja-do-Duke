using Loja_do_Duke.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loja_do_Duke.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _user;

        public UserController(ApplicationDbContext context, UserManager<IdentityUser> user)
        {
            _context = context;
            _user = user;
        }

        public IActionResult Index()
        {
            var userList = _context.ApplicationUsers.ToList();
            var userRoles = _context.UserRoles.ToList();
            var roles = _context.Roles.ToList();
            foreach (var user in userList)
            {
                var role = userRoles.FirstOrDefault(u => u.UserId == user.Id);
                if (role == null)
                {
                    user.Role = "None";
                }
                else
                {
                    user.Role = roles.FirstOrDefault(u => u.Id == role.RoleId).Name;
                }
            }
            return View(userList);
        }
    }
}
