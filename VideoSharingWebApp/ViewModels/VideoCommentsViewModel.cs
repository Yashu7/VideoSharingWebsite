using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VideoSharingWebApp.Models;

namespace VideoSharingWebApp.ViewModels
{
    public class VideoCommentsViewModel
    {
        public VideoModel videoModel { get; set; }
        public List<CommentModel> commentModel { get; set; } = new List<CommentModel>();
    }
}