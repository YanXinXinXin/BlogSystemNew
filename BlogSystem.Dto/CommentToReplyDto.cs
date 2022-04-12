using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Dto
{
   public  class CommentToReplyDto
    {
        public Guid Id { get; set; }

        public  String AContent { get; set; }

        public String RContent { get; set; }

        public Guid[] CommentId { get; set; }

        public Guid[] ReplyId { get; set; }
    }
}
