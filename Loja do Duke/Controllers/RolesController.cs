using Loja_do_Duke.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loja_do_Duke.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class RolesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _user;
        private readonly RoleManager<IdentityRole> _role;

        public RolesController(ApplicationDbContext context, UserManager<IdentityUser> user, RoleManager<IdentityRole> role)
        {
            _context = context;
            _user = user;
            _role = role;
        }

        public IActionResult Index()
        {
            var roles = _context.Roles.ToList();
            return View(roles);
        }

        [HttpGet]
        public IActionResult Upsert(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return View();
            }
            else
            {
                //atualizar
                var objFromDb = _context.Roles.FirstOrDefault(u => u.Id == id);
                return View(objFromDb);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "OnlySuperAdminChecker")]
        public async Task<IActionResult> Upsert(IdentityRole identityRole)
        {
            if (await _role.RoleExistsAsync(identityRole.Name))
            {
                //erro
                TempData[SD.Error] = "A Role já existe";
                return RedirectToAction(nameof(Index));
            }
            if (string.IsNullOrEmpty(identityRole.Id))
            {
                //criar
                await _role.CreateAsync(new IdentityRole() { Name = identityRole.Name });
                TempData[SD.Success] = "Role criada com sucesso";
            }
            else
            {
                //atualizar
                var objRoleFromDb = _context.Roles.FirstOrDefault(u => u.Id == identityRole.Id);
                if(null == objRoleFromDb)
                {
                    TempData[SD.Error] = "Role não encontrada";
                    return RedirectToAction(nameof(Index));
                }
                objRoleFromDb.Name = identityRole.Name;
                objRoleFromDb.NormalizedName = identityRole.Name.ToUpper();
                var result = await _role.UpdateAsync(objRoleFromDb);
                TempData[SD.Success] = "Role atualizada com sucesso";
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "OnlySuperAdminChecker")]
        public async Task<IActionResult> Delete(string id)
        {
            var objFromDb = _context.Roles.FirstOrDefault(u => u.Id == id);
            if (null == objFromDb)
            {
                TempData[SD.Error] = "Role não encontrada";
                return RedirectToAction(nameof(Index));
            }
            var userRolesForThisRole = _context.UserRoles.Where(u => u.RoleId == id).Count();
            if(userRolesForThisRole > 0)
            {
                TempData[SD.Error] = "Esta Role não pode ser excluída pois existe ao menos um usuário associado à ela";
                return RedirectToAction(nameof(Index));
            }
            await _role.DeleteAsync(objFromDb);
            TempData[SD.Success] = "Role excluída com sucesso";
            return RedirectToAction(nameof(Index));
        }
    }
}
