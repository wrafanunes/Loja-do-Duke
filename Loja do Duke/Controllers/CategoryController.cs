using Loja_do_Duke.Data;
using Loja_do_Duke.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loja_do_Duke.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> categories = _db.Categories.OrderBy(c => c.DisplayOrder);
            return View(categories);
        }

        //Get - Create
        public IActionResult Create()
        {
            return View();
        }

        //Post - Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                if (!_db.Categories.Any(x => x.DisplayOrder.Equals(category.DisplayOrder)))
                {
                    _db.Categories.Add(category);
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("DisplayOrder", "Este valor para a sequência de exibição já existe.");
                }

            }
            return View(category);
        }

        //Get - Edit
        public IActionResult Edit(int? id)
        {
            if (null == id || 0 == id) return NotFound();
            var obj = _db.Categories.Find(id);
            if (null == obj) return NotFound();
            return View(obj);
        }

        //Post - Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                if (!_db.Categories.Where(x => x.Id != category.Id).Any(c => c.DisplayOrder.Equals(category.DisplayOrder)))
                {
                    _db.Categories.Update(category);
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("DisplayOrder", "Este valor para a sequência de exibição já existe.");
                }
            }
            return View(category);
        }

        //Get - Delete
        public IActionResult Delete(int? id)
        {
            if (null == id || 0 == id) return NotFound();
            var obj = _db.Categories.Find(id);
            if (null == obj) return NotFound();
            return View(obj);
        }

        //Post - Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int id)
        {
            var obj = _db.Categories.Find(id);
            if (null == obj) return NotFound();
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
