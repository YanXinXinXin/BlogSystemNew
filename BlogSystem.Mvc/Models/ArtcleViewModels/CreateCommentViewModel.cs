using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogSystem.Mvc.Models.ArtcleViewModels
{
    public class CreateCommentViewModel
    {
        public Guid  Id { get; set; }
        [Required,StringLength(500,MinimumLength =2)]
        public string Content   { get; set; }
    }
}