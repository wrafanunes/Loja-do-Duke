using Loja_do_Duke.Data;
using Loja_do_Duke.Models;
using Loja_do_Duke.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Loja_do_Duke.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
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

        [HttpPost]
        public IActionResult LockUnlock(string userId)
        {
            var objFromDb = _context.ApplicationUsers.FirstOrDefault(u => u.Id == userId);
            if (null == objFromDb)
            {
                return NotFound();
            }
            if (null != objFromDb.LockoutEnd && objFromDb.LockoutEnd > DateTime.Now)
            {
                objFromDb.LockoutEnd = DateTime.Now;
                TempData[SD.Success] = "Usuário desbloqueado com sucesso.";
            }
            else
            {
                objFromDb.LockoutEnd = DateTime.Now.AddYears(1000);
                TempData[SD.Success] = "Usuário bloqueado com sucesso.";
            }
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(string userId)
        {
            var objFromDb = _context.ApplicationUsers.FirstOrDefault(u => u.Id == userId);
            if (null == objFromDb)
            {
                return NotFound();
            }
            _context.ApplicationUsers.Remove(objFromDb);
            _context.SaveChanges();
            TempData[SD.Success] = "Usuário excluído com sucesso.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> ManageUserClaims(string userId)
        {
            IdentityUser identity = await _user.FindByIdAsync(userId);
            if (null == identity)
            {
                return NotFound();
            }
            var existingUserClaims = await _user.GetClaimsAsync(identity);

            var model = new UserClaimsVM()
            {
                UserId = userId
            };
            //usando uma classe estática diretamente
            foreach (Claim claim in ClaimStore.claims)
            {
                UserClaim user = new UserClaim
                {
                    ClaimType = claim.Type
                };
                if (existingUserClaims.Any(c => c.Type == claim.Type))
                {
                    user.IsSelected = true;
                }
                model.Claims.Add(user);
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageUserClaims(UserClaimsVM userClaimsVM)
        {
            IdentityUser identity = await _user.FindByIdAsync(userClaimsVM.UserId);
            if (null == identity)
            {
                return NotFound();
            }

            var claims = await _user.GetClaimsAsync(identity);
            var result = await _user.RemoveClaimsAsync(identity, claims);
            if (!result.Succeeded)
            {
                TempData[SD.Error] = "Erro ao tentar remover permissões";
                return View(userClaimsVM);
            }

            result = await _user.AddClaimsAsync(identity, userClaimsVM.Claims.Where(c => c.IsSelected).Select(c => new Claim(c.ClaimType, c.IsSelected.ToString())));
            if (!result.Succeeded)
            {
                TempData[SD.Error] = "Erro ao tentar adicionar permissões";
                return View(userClaimsVM);
            }
            TempData[SD.Success] = "Permissões atualizadas com sucesso";
            return RedirectToAction(nameof(Index));
        }
    }
}
