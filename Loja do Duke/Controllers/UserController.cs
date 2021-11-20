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

        public IActionResult Edit(string userId)
        {
            var objFromDb = _context.ApplicationUsers.FirstOrDefault(u => u.Id == userId);
            if (null == objFromDb)
            {
                return NotFound();
            }
            var userRoles = _context.UserRoles.ToList();
            var roles = _context.Roles.ToList();
            var role = userRoles.FirstOrDefault(u => u.UserId == objFromDb.Id);
            if (null != role)
            {
                objFromDb.RoleId = roles.FirstOrDefault(u => u.Id == role.RoleId).Id;
            }
            objFromDb.RoleList = _context.Roles.Select(u => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = u.Name,
                Value = u.Id
            });
            return View(objFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ApplicationUser user)
        {
            if (ModelState.IsValid)
            {
                var objFromDb = _context.ApplicationUsers.FirstOrDefault(u => u.Id == user.Id);
                if (null == objFromDb)
                {
                    return NotFound();
                }
                var userRole = _context.UserRoles.FirstOrDefault(u => u.UserId == objFromDb.Id);
                if (userRole != null)
                {
                    //Select para atribuir apenas o valor desejado em vez do objeto inteiro
                    var previousRoleName = _context.Roles.Where(u => u.Id == userRole.RoleId).Select(e => e.Name).FirstOrDefault();
                    await _user.RemoveFromRoleAsync(objFromDb, previousRoleName);
                }
                await _user.AddToRoleAsync(objFromDb, _context.Roles.FirstOrDefault(u => u.Id == user.RoleId).Name);
                objFromDb.Name = user.Name;
                _context.SaveChanges();
                TempData[SD.Success] = "Usuário editado com sucesso.";
                return RedirectToAction(nameof(Index));
            }
            user.RoleList = _context.Roles.Select(u => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = u.Name,
                Value = u.Id
            });
            return View(user);
        }
    }
}
