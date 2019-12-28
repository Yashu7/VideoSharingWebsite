using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VideoSharingWebApp.Models;

namespace VideoSharingWebApp.Controllers
{
    public class VideoModelsController : Controller
    {
        public static string path;
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: VideoModels
        public ActionResult Index(string SearchString)
        {
            if(SearchString != null)
            {

                return View(db.VideoModels.Where(x => x.Title.Contains(SearchString)));
            }
            return View(db.VideoModels.ToList());
        }

        // GET: VideoModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VideoModel videoModel = db.VideoModels.Find(id);
            if (videoModel == null)
            {
                return HttpNotFound();
            }
            return View(videoModel);
        }

        // GET: VideoModels/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: VideoModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //public string path = "";
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,Title,Path,UploadTime")] VideoModel videoModel)
        {
            if (ModelState.IsValid)
            {
                
                //videoModel.Path = path;
                videoModel.UserId = User.Identity.GetUserName();
                videoModel.UploadTime = DateTime.Today;
                db.VideoModels.Add(videoModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(videoModel);
        }
        
        public static string PathToVideo()
        {
            return path;
        }
        [Authorize]
        public ActionResult AddVideo()
        {
            return View();
        }
        //Uploading File from view to server.
        [HttpPost]
        public ActionResult AddVideo(HttpPostedFileBase file)

        {
            path = file.FileName;
            //Check if file is mp4.
            if (path.EndsWith(".mp4"))
            {
                path = Path.Combine(Server.MapPath("~/Videos"),
                                            Path.GetFileName(file.FileName));
                file.SaveAs(path);
                path = @"/Videos/" + Path.GetFileName(file.FileName);
                ViewBag.Message = "File uploaded successfully";
                return RedirectToAction("Create");
            }
            return View();
        }
        // GET: VideoModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VideoModel videoModel = db.VideoModels.Find(id);
            if (videoModel == null)
            {
                return HttpNotFound();
            }
            return View(videoModel);
        }

        // POST: VideoModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,Title,Path,UploadTime")] VideoModel videoModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(videoModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(videoModel);
        }

        // GET: VideoModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VideoModel videoModel = db.VideoModels.Find(id);
            if (videoModel == null)
            {
                return HttpNotFound();
            }
            return View(videoModel);
        }

        // POST: VideoModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VideoModel videoModel = db.VideoModels.Find(id);
            db.VideoModels.Remove(videoModel);
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
