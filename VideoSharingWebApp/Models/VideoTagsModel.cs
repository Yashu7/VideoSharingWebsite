using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VideoSharingWebApp.Models
{
    public class VideoTagsModel
    {
        [Key]
        public int Id { get; set; }

        public VideoModel Video { get; set; }
        public TagModel Tag { get; set; }
    }
}