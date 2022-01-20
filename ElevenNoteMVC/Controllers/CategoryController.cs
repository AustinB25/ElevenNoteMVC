using ElevenNote.Data;
using ElevenNote.Models;
using ElevenNote.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElevenNoteMVC.Controllers
{
    public class CategoryController : Controller
    {
        private ApplicationDbContext _ctx = new ApplicationDbContext();
        // GET: Category
        public ActionResult Index()
        {
            var categoryService = CreateCategoryService();
            var categories = categoryService.GetCategories();
            return View(categories);
        }
        // Get: Category/Create
        public ActionResult Create()
        {
            return View();
        }
        //Get Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryCreate model)
        {
            if (!ModelState.IsValid) return View(model);
            var categoryService = CreateCategoryService();
            if (categoryService.CreateCategory(model))
            {
                TempData["SaveResult"] = "Your note was created.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(" ", "Category could not be created");
            return View(model);
        }
        //Get: Category/Details/{id}
        public ActionResult Details(int id)
        {
            var catService = CreateCategoryService();
            var model = catService.GetCategoryById(id);
            return View(model);
        }
        //Get: Category/Edit/{id}
        public ActionResult Edit(int id)
        {
            var catService = CreateCategoryService();
            var modelDetails = catService.GetCategoryById(id);
            var model =
                new CategoryEdit
                {
                    CategoryId = modelDetails.CategoryId,
                    Name = modelDetails.Name,
                    ModifiedUtc = DateTimeOffset.Now
                };
            return View(model);
        }
        //Post: Category/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CategoryEdit model)
        {
            if (!ModelState.IsValid) return View(model);
            if (model.CategoryId != id)
            {
                ModelState.AddModelError(" ", "The Id does not match an exsisting id");
                return View(model);
            }
            var catService = CreateCategoryService();
            if (catService.UpdateCategory(model))
            {
                TempData["SaveResult"] = "Your category was updated";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(" ", "Your category could not be updated.");
            return View();
        }
        //Get: Category/Delete/{id}
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var catService = CreateCategoryService();
            var model = catService.GetCategoryById(id);
            return View(model);
        }
        //Post: Category/Delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeletePost(int id)
        {
            var catService = CreateCategoryService();
            catService.DeleteCategory(id);
            TempData["SaveResult"] = "Your category was deleted.";
            return RedirectToAction("Index");

        }
        private CategoryService CreateCategoryService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var categoryService = new CategoryService(userId);
            return categoryService;
        }
    }
}