using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogSystem.Mvc.Models.ArtcleViewModels
{
    public class CreateCategoryViewModels
    {
        /// <summary>
        /// 类型名字
        /// </summary>
        [Required,StringLength(200,MinimumLength =2),Display(Name = "类型名字")]
        public string CategoryName  { get; set; }
    }
}