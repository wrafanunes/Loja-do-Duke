using Loja_do_Duke.Data;
using Loja_do_Duke.Models;
using Loja_do_Duke.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loja_do_Duke.Controllers
{
    [Authorize]
    public class SupplyController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _user;

        public SupplyController(ApplicationDbContext db, UserManager<IdentityUser> user)
        {
            _db = db;
            _user = user;
        }

        [Authorize]
        public IActionResult Index()
        {
            IEnumerable<Supply> supplies = _db.Supplies.Include(x => x.Category);
            return View(supplies);
        }

        //Get - Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            SupplyVM vM = new SupplyVM()
            {
                Supply = new Supply(),
                CategorySelectList = _db.Categories.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };
            return View(vM);
        }

        //Post - Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Supply supply)
        {
            if (ModelState.IsValid)
            {
                _db.Supplies.Add(supply);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(supply);
        }

        //Get - Edit
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int? id)
        {
            if (null == id || 0 == id) return NotFound();
            SupplyVM vM = new SupplyVM()
            {
                Supply = new Supply(),
                CategorySelectList = _db.Categories.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };
            vM.Supply = _db.Supplies.Find(id);
            if (null == vM.Supply) return NotFound();
            return View(vM);
        }

        //Post - Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Supply supply)
        {
            if (ModelState.IsValid)
            {
                _db.Supplies.Update(supply);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(supply);
        }

        //Get - Buy
        [Authorize(Roles = "User")]
        public IActionResult Buy(int? id)
        {
            if (null == id || 0 == id) return NotFound();
            SupplyVM vM = new SupplyVM()
            {
                Supply = new Supply()
            };
            vM.Supply = _db.Supplies.Find(id);
            vM.ApplicationUserSupply = GetApplicationUserSupply(id).Item1;
            var user = GetApplicationUserSupply(id).Item2;
            vM.Lei = user.Lei;
            vM.InventoryCapacity = user.InventoryCapacity;
            if (null == vM.Supply) return NotFound();
            return View(vM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Buy(Supply supply, SupplyVM supplyVM)
        {
            if (ModelState.IsValid)
            {
                supply.AvailableQuantity -= supplyVM.Quantity;
                _db.Supplies.Update(supply);
                supplyVM.ApplicationUserSupply = GetApplicationUserSupply(supply.Id).Item1;
                SetUserLeiAndInventoryCapacity(supplyVM.ApplicationUserSupply.UserId, supply.Id, supply.Price, supplyVM.Quantity,
                    supplyVM.ApplicationUserSupply.UserInventoryQuantity);
                supplyVM.ApplicationUserSupply.UserInventoryQuantity += supplyVM.Quantity;
                if (supplyVM.ApplicationUserSupply.Id.Equals(null))
                {
                    _db.ApplicationUserSupplies.Add(supplyVM.ApplicationUserSupply);
                }
                else
                {
                    _db.ApplicationUserSupplies.Update(supplyVM.ApplicationUserSupply);
                }
                _db.SaveChanges();
                TempData[SD.Success] = "Compra realizada com sucesso";
                return RedirectToAction("Index");
            }
            return View(supply);
        }

        //Get - Delete
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int? id)
        {
            if (null == id || 0 == id) return NotFound();
            Supply supply = _db.Supplies.Include(x => x.Category).FirstOrDefault(x => x.Id == id);
            if (null == supply) return NotFound();
            return View(supply);
        }

        //Post - Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int id)
        {
            var obj = _db.Supplies.Find(id);
            if (null == obj) return NotFound();
            _db.Supplies.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        private Tuple<ApplicationUserSupply, ApplicationUser> GetApplicationUserSupply(int? supplyId)
        {
            var userId = _user.GetUserId(User);
            var user = _db.ApplicationUsers.Find(userId);
            var supply = _db.Supplies.Find(supplyId);
            var applicationUserSupply = _db.ApplicationUserSupplies.SingleOrDefault(x => x.UserId == userId && x.SupplyId == supplyId);
            if (null == applicationUserSupply) return Tuple.Create(new ApplicationUserSupply(userId, supplyId, supply.Name), user);
            return Tuple.Create(applicationUserSupply, user);
        }

        private void SetUserLeiAndInventoryCapacity(string userId, int? supplyId, int price, int quantity, int userInventoryQuantity)
        {
            var user = _db.ApplicationUsers.Find(userId);
            user.Lei -= price * quantity;
            if (supplyId == 1) user.InventoryCapacity -= (short)(2 * quantity);
            else if (supplyId == 5 && userInventoryQuantity % 30 == 0)
            {
                user.InventoryCapacity -= 2;
            }
        }
    }
}
