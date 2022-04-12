using BlogSystem.BLL;
using BlogSystem.IBLL;
using BlogSystem.Mvc.Filters;
using BlogSystem.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BlogSystem.Mvc.Controllers
{
    public class HomeController : Controller
    {

        // GET: Home

        //[BlogSystemAuth]
      
        public ActionResult Index()
        {
        




            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(Models.UserViewModels.RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                IBLL.IUserManger userManger = new UserManger();
                await userManger.Register(model.Email, model.Password);
                return Content("注册成功");
            }
            return View(model);
        }
       /// <summary>
       /// 登录
       /// </summary>
       /// <returns></returns>
        [HttpGet]

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
   
        public ActionResult Login(Models.UserViewModels.LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                IBLL.IUserManger userManger = new UserManger();
                Guid userId;
                int userType;
                if ( userManger.Login(model.Email, model.LoginPwd, out userId,out userType))
                {
                   // 跳转 判断是用Session还是cookie
                    if (model.RememberMe)
                    {
                        Response.Cookies.Add(new HttpCookie("loginName")
                        {
                            Value = model.Email,
                            Expires = DateTime.Now.AddHours(1)
                        });
                        Response.Cookies.Add(new HttpCookie("userId")
                        {
                            Value = userId.ToString(),
                            Expires = DateTime.Now.AddHours(1)
                        });
                    }
                    else
                    {
                        Session["loginName"] = model.Email;
                        Session["userId"] = userId;
                    }

                    if (userType==0)
                    {
                        return RedirectToAction("index","home");
                    }
                    else
                    {
                        return RedirectToAction("index", "backstage");
                    }
                    //return RedirectToAction(nameof(Index));
           
             
                }
                else
                {
                    ModelState.AddModelError("", "你的账户密码有误");
                }

            }
            return View(model);
        }
        [HttpGet]
        public ActionResult AjaxIndex()
        {
           
            return View();
        }
        [HttpPost]
        public ActionResult AjaxIndex(Student stu) {
       
            return View(stu);
        }
        [HttpGet]
        public ActionResult  Cheshi()
        {
            return View();
        }
        [HttpGet]
        public async Task<ActionResult> Cheshi2()
        {
            IBLL.IUserManger userManger = new UserManger();
            var result = await userManger.GetAllUsers();
            return Json(new {
            code=0,msg="",count=1000,data= result
            },JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public async Task<ActionResult> Cheshi3()
        {
            //IBLL.IArticleManger  articleManger=new ArticleManger(0);
            //var result = await userManger.GetAllUsers();
            return Json(new
            {
                code = 0,
                msg = "",
                count = 1000
            }, JsonRequestBehavior.AllowGet); 
        }
        [HttpGet]
        public ActionResult CheshiNew()
        {
            //IBLL.IArticleManger  articleManger=new ArticleManger(0);
            //var result = await userManger.GetAllUsers();
            return View();
        }
    }
}