using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

  //dto 用来传输数据
namespace BlogSystem.Dto
{
  public   class ArticleDto
    {
        public Guid Id  { get; set; }
        //标题
        public string Title  { get; set; }
        //类容
        public string Content  { get; set; }
        public DateTime  CreateTime { get; set; }

        public string Email  { get; set; }
        public int GoodCount { get; set; }
    
        public int BadCount { get; set; }
        public string  ImagePath { get; set; }
        public bool Status { get; set; }
        //标签名字
        public string [] CategoryNames { get; set; }
        //标签Id
        public Guid[] CategoryIds { get; set; }
        public Guid UserId { get; set; }
    }
}
