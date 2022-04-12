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
    //文章
    [BlogSystemAuth]
    public class ArtcleController : Controller
    {
        // GET: Artcle
        public ActionResult Index()
        {
            return View();
        }
        //分类
        [HttpGet]
        public ActionResult CreateCategory()
        {
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult CreateCategory(Models.ArtcleViewModels.CreateCategoryViewModels model)
        {
            if (ModelState.IsValid)
            {
                IBLL.IArticleManger articleManger = new ArticleManger();
             
                articleManger.CreateCategory(model.CategoryName, Guid.Parse(Session["userId"].ToString()));
                return RedirectToAction("CategoryList");

            }

            ModelState.AddModelError("", "你录入的信息有误");
            return View(model);

        }
        //显示文章
        [HttpGet]
         
        public async Task<ActionResult> CategoryList()
        {

            return View(await new ArticleManger().GetAllCategories(Guid.Parse(Session["userId"].ToString())));

        }
        //创建文章 有一个多对多关系
        [HttpGet]
        [BlogSystemAuth]
        public async Task<ActionResult> CreateArtcle()
        {
            //当前用户的分类
            ViewBag.CategoryIds = await new ArticleManger().GetAllCategories(Guid.Parse(Session["userId"].ToString()));
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]//不做录入的校验
        public async Task<ActionResult> CreateArtcle(Models.ArtcleViewModels.CreateArtcleViewModel model)
        {
            if (ModelState.IsValid)
             {
                await new ArticleManger().CreateArticle(model.Title,model.Content, model.CategoryIds, Guid.Parse(Session["userId"].ToString()));
                return RedirectToAction("ArtcleList2");
            }
            ModelState.AddModelError("", "添加失败");
            return View();
        }

        [HttpGet]

        public async Task<ActionResult> ArtcleList(int pageIndex = 0, int pageSize = 5)
        {
            //需要给页面前端，总页码数，当前页码，可显示总页码数量
            var artcleMgr = new ArticleManger();
            var artcles = await artcleMgr.GetAllArticlesByUserId(Guid.Parse(Session["userId"].ToString()), pageIndex, pageSize);
            var dataCount = await artcleMgr.GetDataCount(Guid.Parse(Session["userId"].ToString()));
            ViewBag.PageCount = dataCount % pageSize == 0 ? dataCount / pageSize : dataCount / pageSize + 1;
            ViewBag.PageIndex = pageIndex;
            return View(artcles);
        }
        [HttpGet]
        public async Task<ActionResult> ArtcleList2(int pageIndex = 1, int pageSize = 5)
        {
            //需要给页面前端，总页码数，当前页码，可显示总页码数量
            var artcleMgr = new ArticleManger();
            //当前用户第n页数据
            var artcles = await artcleMgr.GetAllArticlesByUserId(Guid.Parse(Session["userId"].ToString()), pageIndex - 1, pageSize);
            //获取当前用户文章总数
            var dataCount = await artcleMgr.GetDataCount(Guid.Parse(Session["userId"].ToString()));

            return View(new PagedList<Dto.ArticleDto>(artcles, pageIndex, pageSize, dataCount));
        }
        //详情页面
        public async Task<ActionResult> ArtcleDetails(Guid? id)
        {
            var artcleMgr = new ArticleManger();
            if (id == null || !await artcleMgr.ExistsArticle(id.Value))
            
                return RedirectToAction(nameof(ArtcleList2));
            
            ViewBag.Comments = await artcleMgr.GetCommentsByArtcleId(id.Value);

            return View(await artcleMgr.GetOneArticleById(id.Value));
        }
        //详情页面
        public async Task<ActionResult> ArtcleDetails2(Guid id)
        {
           
           
            var artcleMgr = new ArticleManger();
            var fancMgr = new FansManger();
           
          ViewBag.Reply=  await artcleMgr.GetAllReply(id);
            var data = await artcleMgr.ExistsArticle(id);
            ViewBag.Comments = await artcleMgr.GetCommentsByArtcleId(id);
            ViewBag.userId = Guid.Parse(Session["userId"].ToString());
            return View(await artcleMgr.GetOneArticleById(id));
       
            //var artcleMgr = new ArticleManger();
            //if (id == null || !await artcleMgr.ExistsArticle(id.Value))

            //    return RedirectToAction(nameof(ArtcleList2));

            //ViewBag.Comments = await artcleMgr.GetCommentsByArtcleId(id.Value);

            //return View(await artcleMgr.GetOneArticleById(id.Value));
        } 
        [HttpGet]
        public async Task<ActionResult> EditArtcle(Guid id)
        {

            var artcleManger = new ArticleManger();
            var data = await artcleManger.GetOneArticleById(id);

            ViewBag.CategoryIds = await new ArticleManger().GetAllCategories(Guid.Parse(Session["userId"].ToString()));
            return View(new Models.ArtcleViewModels.EditArtcleViewModel()
            {
                Title = data.Title,
                Content = data.Content,
                CategoryIds = data.CategoryIds,
                Id = data.Id
            });
        }
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> EditArtcle(Models.ArtcleViewModels.EditArtcleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var artcleManger = new ArticleManger();
                await artcleManger.EditArticle(model.Id, model.Title, model.Content, model.CategoryIds);
                return RedirectToAction("ArtcleList2");
            }
            else
            {
                ViewBag.CategoryIds = await new ArticleManger().GetAllCategories(Guid.Parse(Session["userId"].ToString()));
                return View(model);
            }


        }
        [HttpGet]
        public async Task<ActionResult> DelArtcle(Guid id)
        {
            var artcleManger = new ArticleManger();
            var data = await artcleManger.GetOneArticleById(id);
            return View(data);
        }
        //删除文章
        [HttpPost]
        public async Task<ActionResult> DelArtcle(Models.ArtcleViewModels.DelArtcleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var artcleManger = new ArticleManger();
                await artcleManger.RemoveArticle(model.Id);
             
            }
            return RedirectToAction(nameof(ArtcleList2));
        }

        //添加评论

  
        [HttpPost]
        public async Task<ActionResult> AddComment(Models.ArtcleViewModels.CreateCommentViewModel model)
        {
            var userId = Guid.Parse(Session["userId"].ToString());
            if (ModelState.IsValid)
            {
                var artcleManger = new ArticleManger();
                await artcleManger.CreateComment(userId, model.Id, model.Content);

            }
            return Json(new { result = "Ok" });
        }

        [HttpPost]
        public async Task<ActionResult> GoodCount(Guid Id) {
            IBLL.IArticleManger articleManger = new ArticleManger();
           await articleManger.GoodCountAdd(Id);
            return Json(new { result = "ok" });
        }

        [HttpPost]
        public async Task<ActionResult> BadCount(Guid Id)
        {
            IBLL.IArticleManger articleManger = new ArticleManger();
            await articleManger.GoodCountAdd(Id);
            return Json(new { result = "ok" });
        }


        [HttpGet]
        public async Task<ActionResult> Artcleall() {

            IBLL.IArticleManger articleManger = new ArticleManger();
         var data=  await  articleManger.GetArticle();

            return View(data); 
        }

        [HttpPost]
        public async Task<ActionResult> CreateReply(Models.ArtcleViewModels.CreateReplyViewModels model,Guid arecleId,Guid [] commentIds,string content) {

            IBLL.IArticleManger articleManger = new ArticleManger();
            await articleManger.CreateReply(Guid.Parse(Session["userId"].ToString()), arecleId, commentIds, content);
            return Json(new { result = "ok" });
        }
        [HttpGet]
        public async Task<ActionResult> GetAllReply(Guid artcleId) {

         
            return Json(new { result = "ok" });
        }

         
    }
}