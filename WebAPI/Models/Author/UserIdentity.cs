using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace WebAPI.Models.Author
{
    public class UserIdentity : IIdentity
    {
        public string Name { get; }

        public string AuthenticationType { get; }

        public bool IsAuthenticated { get; }

        public int Number => 3 + 1;
    }
}

