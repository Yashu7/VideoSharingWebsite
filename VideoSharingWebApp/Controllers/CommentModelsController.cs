using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VideoSharingWebApp.Models;
using VideoSharingWebApp.ViewModels;

namespace VideoSharingWebApp.Controllers
{
    public class CommentModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationDbContext dbSecond = new ApplicationDbContext();

        // GET: CommentModels
        public ActionResult Index()
        {
          
            return View(db.CommentModels.ToList());
        }

        // GET: CommentModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommentModel commentModel = db.CommentModels.Find(id);
            if (commentModel == null)
            {
                return HttpNotFound();
            }
            return View(commentModel);
        }

        // GET: CommentModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CommentModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Comment,VideoId")] CommentModel commentModel)
        {
            if (ModelState.IsValid)
            {
                db.CommentModels.Add(commentModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(commentModel);
        }

        // GET: CommentModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommentModel commentModel = db.CommentModels.Find(id);
            if (commentModel == null)
            {
                return HttpNotFound();
            }
            return View(commentModel);
        }

        // POST: CommentModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Comment,VideoId")] CommentModel commentModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(commentModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(commentModel);
        }

        // GET: CommentModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommentModel commentModel = db.CommentModels.Find(id);
            if (commentModel == null)
            {
                return HttpNotFound();
            }
            return View(commentModel);
        }

        // POST: CommentModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CommentModel commentModel = db.CommentModels.Find(id);
            db.CommentModels.Remove(commentModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
