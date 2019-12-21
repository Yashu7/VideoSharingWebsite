using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VideoSharingWebApp.Models;

namespace VideoSharingWebApp.Controllers
{
    public class HomeController : Controller
    {
        List<VideoModel> models = new List<VideoModel>();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            VideoModel model = new VideoModel();
            
            string path = Path.Combine(Server.MapPath("~/Videos"),
                                         Path.GetFileName(file.FileName));
            file.SaveAs(path);
            model.Path = path;
            ViewBag.Message = "File uploaded successfully";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}