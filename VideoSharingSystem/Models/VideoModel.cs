using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VideoSharingSystem.Models
{
    public class VideoModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime UploadDate { get; set; }
        public int UserID { get; set; }
    }
}