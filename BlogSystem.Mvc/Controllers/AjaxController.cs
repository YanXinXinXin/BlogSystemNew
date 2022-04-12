using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogSystem.Mvc.Controllers
{
    public class AjaxController : Controller
    {
        // GET: Ajax
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login(string name, string pwd)
        {

            var doc = new Dictionary<string, string>();
          
            //var data2 = { name:name,pwd = pwd};
            var data = new List<object>();
            data.Add(new  { name=name,pwd=pwd}
            );
            return Json(data,JsonRequestBehavior.AllowGet);
        }
    }
}