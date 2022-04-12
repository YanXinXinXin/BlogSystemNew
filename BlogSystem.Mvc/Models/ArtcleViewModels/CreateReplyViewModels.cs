using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogSystem.Mvc.Models.ArtcleViewModels
{
    public class CreateReplyViewModels
    {
        [Required, StringLength(500)]
        public string Content { get; set; }
    }
}