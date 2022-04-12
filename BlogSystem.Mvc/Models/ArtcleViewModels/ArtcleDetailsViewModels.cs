using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogSystem.Mvc.Models.ArtcleViewModels
{
    //如果该ViewModel和dto中的类一样，可以省略直接使用dto中的类，如果不同，必须创建 
    //查询的时候不用特效验证过滤  [Required]
    public class ArtcleDetailsViewModels
    {
      
        public Guid Id { get; set; }
        //标题
        public string Title { get; set; }
        //类容
        [Display(Name ="类容")]
        public string Content { get; set; }
        public DateTime CreateTime { get; set; }
        [Display(Name ="")]
        public string Email { get; set; }
        public int GoodCount { get; set; }
        public int BadCount { get; set; }
        public string ImagePath { get; set; }
        //标签名字
        public string[] CategoryNames { get; set; }
        //标签Id
        public Guid[] CategoryIds { get; set; }
    }
}