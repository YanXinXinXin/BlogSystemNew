﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogSystem.Mvc.Models.ArtcleViewModels
{
    public class EditArtcleViewModel
    {
        public Guid Id { get; set; }
        [Required]
        public string Title{ get; set; }
        [Required]
        public string Content  { get; set; }
        public Guid[] CategoryIds  { get; set; }
    }
}