﻿using Microsoft.AspNet.Identity;
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
using VideoSharingWebApp.ViewModels;
using NReco.VideoConverter;
using System.Threading.Tasks;

namespace VideoSharingWebApp.Controllers
{
    public class VideoModelsController : Controller
    {
        public static string path;
        private ApplicationDbContext db = new ApplicationDbContext();
       
        


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Details([Bind(Include = "videoModel,commentModel,newCommentModel")] VideoCommentsViewModel CM)
            {
            if (ModelState.IsValid)
            {
                CM.newCommentModel.UserName = User.Identity.GetUserName();
                db.CommentModels.Add(CM.newCommentModel);
                db.SaveChanges();
                return Details(CM.newCommentModel.VideoId);
            }

            return View();
        }
        
        public JsonResult GetThatJavaScript(VideoModel vm)
        {
            VideoModel videoModel = db.VideoModels.Find(vm.Id);
            videoModel.Views = videoModel.Views + 1;
            db.Entry(videoModel).State = EntityState.Modified;
            db.SaveChanges();
            return Json(db.VideoModels.Where(x=>x.Id == vm.Id), JsonRequestBehavior.AllowGet);
        }
        
       

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
            VideoCommentsViewModel vm = new VideoCommentsViewModel
            {
                videoModel = db.VideoModels.Find(id)
            };
            //VideoModel videoModel = db.VideoModels.Find(id);
            foreach (var c in db.CommentModels)
            {
                if(c.VideoId == vm.videoModel.Id)
                {
                    vm.commentModel.Add(c);
                }

            }
            if(vm.commentModel == null)
            {
                vm.commentModel.Add(new CommentModel { Id=0,Comment="",VideoId=0});
            }
            

            //if (videoModel == null)
            //{
            //    return HttpNotFound();
            //}
            return View(vm);
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
        public ActionResult Create([Bind(Include = "Id,UserId,Title,Path,UploadTime,VideoDesc")] VideoModel videoModel)
        {
            if (ModelState.IsValid)
            {
                
                
                //Grab username and date for a video.
                videoModel.UserId = User.Identity.GetUserName();
                videoModel.UploadTime = DateTime.Today;

                //Get video file data
                List<string> VideoData = new List<string>();
                VideoData = VideoConverter(videoModel.Path.ToString());
                videoModel.Thumbnail = VideoData[0];
                videoModel.Path = VideoData[1];

                //Save model to database.
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
                //where to save a file.
                path = Path.Combine(Server.MapPath("~/Videos"),
                                            Path.GetFileName(file.FileName));

                
                file.SaveAs(path);
                
                //Format path for view
                path = @"/" + Path.GetFileName(file.FileName);
                ViewBag.Message = "File uploaded successfully";
                return RedirectToAction("Create");
            }
            return View();
        }
        
        //Convert video, Create thumbnail, Pass correct paths for model back
         public List<string> VideoConverter(string output)
        {
            
            List<string> videoData = new List<string>();
            string thumbnailName = output.Substring(1) + ".jpg";
            string convertedFileName = output.Substring(1);

            //Initialize converter
            var ffconverter = new FFMpegConverter();
            ConvertSettings convertSettings = new ConvertSettings
            {
                VideoFrameSize = "320x180"
            };
            

            //Convert uploaded video to common format/size
            ffconverter.ConvertMedia((Server.MapPath("~/Videos")+output), "mp4",convertedFileName, "mp4", convertSettings);
            //Create thumbnail from converted video
            ffconverter.GetVideoThumbnail((Server.MapPath("~/") + convertedFileName), thumbnailName, 5);
            System.IO.File.Delete((Server.MapPath("~/Videos")+output));
            

            //Add correct paths to video and thumbnail for model
            videoData.Add("/" + thumbnailName);
            videoData.Add("/" + convertedFileName);

            return videoData;
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
        public ActionResult Edit([Bind(Include = "Id,UserId,Title,Path,UploadTime,VideoDesc")] VideoModel videoModel)
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
