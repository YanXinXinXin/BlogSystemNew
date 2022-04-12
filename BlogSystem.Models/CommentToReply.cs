using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Models
{
   // 留言《---》回复
   public  class CommentToReply:BaseEntity
    {
        [ForeignKey(nameof(Comment))]
        public Guid CommentId { get; set; }
        public Comment Comment  { get; set; }


        [ForeignKey(nameof(Reply))]
        public Guid ReplyId { get; set; }
        public Reply Reply  { get; set; }                  
        
    }
}
