using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Models
{
    //评论回复
   public  class Reply:BaseEntity
    {
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public User User { get; set; }
         
        [Required, StringLength(500)]
        public string Content { get; set; }

        [ForeignKey(nameof(Article))]
        public Guid AritcleId { get; set; }
        public Article Article { get; set; }
       
        //     [ForeignKey(nameof(Comment))]
        //public Guid CommentId { get; set; }
        //public Comment Comment { get; set; }


    }
}
