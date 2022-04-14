using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WebAPI.Models;
using WebAPI.Tools;

namespace WebAPI.Controllers
{
    [RoutePrefix("api/values")]
    [EnableCors(origins: "*", headers: "*", methods: "*")] //跨域
    public class ValuesController : ApiController
    {
        [Route("")]
        public string Get()
        {
            return "world";
        }
        [HttpPost]
        [Route("login")]
        public string Login(UserViewModel data)
        {
            if (data.LoginName.Length > 2 && data.Password == "123456")
            {
                return JwtTools.Encode(new Dictionary<string, object>() {
                    {"loginName",data.LoginName} 
                }, JwtTools.key);
            }
            throw new Exception("账号密码有误");
        }
        [HttpGet]
        [Route("getInfo")]
        public string GetUserInfo()
        {
            var username = JwtTools.ValideLogined(ControllerContext.Request.Headers);
            return "用户资料"+ username;
        }
        // GET api/values
        

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
