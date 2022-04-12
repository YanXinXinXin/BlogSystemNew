using BlogSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.DAL
{
    public class ReplyService : BaseService<Models.Reply>, IDAL.IReplyService
    {
        public ReplyService() : base(new BlogContext())
        {

        }
    }
}
