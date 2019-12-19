using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VideoSharingWebApp.Models
{
    public class VideoModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Path { get; set; }
        public DateTime UploadTime { get; set; }
    }
}