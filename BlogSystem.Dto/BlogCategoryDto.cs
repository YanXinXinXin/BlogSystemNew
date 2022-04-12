using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Dto
{
    //文章类别dto
  public   class BlogCategoryDto
    {
        public Guid  Id { get; set; }
        public string CategoryName  { get; set; }
    }
}
