using BlogSystem.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace BlogSystem.Mvc.Controllers
{
    public class BackStageController : Controller
    {

        public async Task<ActionResult> Default() {
            IBLL.IArticleManger articleManger =new  BLL.ArticleManger(); 
            var data = await articleManger.GetArticleByStatusFalse();
            ViewBag.ArtcleCount = data.Count();
           
            return View();
        }
        // GET: BackStage
        public async Task<ActionResult> Index()
        
        {
            IBLL.IArticleManger art =new  BLL.ArticleManger();
              
            var data =await art.GetArticleByStatusFalse();

            return View(data);
        }
        [HttpGet]
        public async Task<ActionResult> UserInfo()
        {
            IBLL.IUserManger userManger = new UserManger();
            var data = await userManger.GetAllUsers();
            return View(data);
        }

        #region 审核文章
        [HttpGet]
        public async Task< ActionResult >Review(Guid id)
        {
            IBLL.IArticleManger art = new BLL.ArticleManger();
            await art.UpdateArtcleStatusByArtcleId(id);
            return Json(new { code=200},JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}