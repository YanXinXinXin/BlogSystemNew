using BlogSystem.BLL;
using BlogSystem.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Webdiyer.WebControls.Mvc;

namespace BlogSystem.Mvc.Controllers
{
    public class FansController : Controller
    {
        // GET: Fans
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<JsonResult>Create(Guid userId,Guid focusUserId) 
        {
            var fancmanger = new FansManger();
            if (userId==focusUserId)
            {
                return Json(new { result = "no" });
            }
            else
            {
                if (await fancmanger.AnyFan(userId, focusUserId))
                {
                    return Json(new { result = "no" });
                }
                else
                {

                    await fancmanger.CreateFan(userId, focusUserId);
                    return Json("", JsonRequestBehavior.AllowGet);
                }
            }
           

         
        }
    }
}