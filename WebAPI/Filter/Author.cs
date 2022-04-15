using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using WebAPI.Tools;

namespace WebAPI.Filter
{
    public class Author : Attribute, IAuthorizationFilter
    {
        public bool AllowMultiple => throw new NotImplementedException();

        public async Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            if (actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Count>0)
            {
                return await continuation();
            }
            IEnumerable<string> headers;
            if (actionContext.Request.Headers.TryGetValues("token", out headers))
            {
                var LoginName = JwtTools.Decode(headers.First())["LoginName"].ToString();
                return await continuation();
            }
            return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
        }
    }
}