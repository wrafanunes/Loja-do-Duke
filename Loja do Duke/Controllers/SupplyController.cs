using Loja_do_Duke.Data;
using Loja_do_Duke.Models;
using Loja_do_Duke.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
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

        public SupplyController(ApplicationDbContext db)
        {
            _db = db;
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
    }
}
