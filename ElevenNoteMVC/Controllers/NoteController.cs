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
    [Authorize]
    public class NoteController : Controller
    {
        private ApplicationDbContext _ctx = new ApplicationDbContext();
        // GET: Note
        public ActionResult Index()
        {
            var noteService = CreateNoteService();
            var notes = noteService.GetNotes();
            return View(notes);
        }
        //Get: Note/Create
        public ActionResult Create()
        {
            return View();
        }
        //Post: Note/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NoteCreate model)
        {
            if (!ModelState.IsValid) return View(model);
            var noteService = CreateNoteService();
            if (noteService.CreateNote(model))
            {
                TempData["SaveResult"] = "Your note was created.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(" ", "Note could not be created.");
            return View(model);
        }
        //Get: Note/Details/{id}
        public ActionResult Details(int id)
        {
            var noteService = CreateNoteService();
            var model = noteService.GetNoteById(id);
            return View(model);
        }
        //Get: Note/Edit/{id}
        public ActionResult Edit(int id)
        {
            var noteService = CreateNoteService();
            var details = noteService.GetNoteById(id);
            var model =
                new NoteEdit
                {
                    NoteId = details.NoteId,
                    Title = details.Title,
                    Content = details.Content
                };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //Post: Note/Edit/{id}
        public ActionResult Edit(int id, NoteEdit model)
        {
            if (!ModelState.IsValid) return View(model);
            if (model.NoteId != id)
            {
                ModelState.AddModelError(" ", "Id does not match an exsisting note.");
                return View(model);
            }
            var service = CreateNoteService();
            if (service.UpdateNote(model))
            {
                TempData["SaveResult"] = "Your note was updated";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(" ", "Your note could not be created.");
            return View();
        }
        //Get: Note/Delete/{id}
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var noteService = CreateNoteService();
            var model = noteService.GetNoteById(id);
            return View(model);
        }
        //Post: Note/Delete/{id}
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var noteService = CreateNoteService();
            noteService.DeleteNote(id);
            TempData["SaveResult"] = "Your note was deleted.";
            return RedirectToAction("Index");
        }
        private NoteService CreateNoteService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var noteService = new NoteService(userId);
            return noteService;
        }
    }
}