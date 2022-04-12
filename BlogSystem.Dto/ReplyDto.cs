using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Dto
{
    public  class ReplyDto
    {
        public Guid Id { get; set; }

        public string Content { get; set; }

        public Guid UserId { get; set; }

        public string Email { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
