﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VideoSharingWebApp.Models
{
    public class CommentModel
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public int VideoId { get; set; }
        public string UserName { get; set; }
    }
}