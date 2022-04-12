using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogSystem.Models;
namespace BlogSystem.DAL
{
    public class CommentToReplyService : BaseService<Models.CommentToReply>, IDAL.ICommentToReplyService
    {
        public CommentToReplyService() : base(new BlogContext())
        {
        }
    }
}
