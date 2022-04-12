using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.DAL
{
    public class FansService : BaseService<Models.Fans>, IDAL.IFansService
    {
        public FansService():base(new Models.BlogContext())
        {

        }
    }
}
