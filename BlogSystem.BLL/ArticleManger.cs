using BlogSystem.DAL;
using BlogSystem.Dto;
using BlogSystem.IBLL;
using BlogSystem.IDAL;
using BlogSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.BLL
{
    public class ArticleManger : IArticleManger
    {
        public async Task CreateArticle(string title, string content, Guid[] categoryIds, Guid userId)
        {
            using (var articleSvc = new ArticleService())
            {
                var article = new Article()
                {
                    Title = title,
                    Content = content,
                    UserId = userId
                };

                await articleSvc.CreateAsync(article);
                Guid articleId = article.Id;
                using (var articleToCategorySvc = new ArticleToCategoryService())
                {
                    foreach (var categoryId in categoryIds)
                    {
                        await articleToCategorySvc.CreateAsync(new ArticleToCategory()
                        {
                            ArticleId = articleId,
                            BlogCategoryId = categoryId

                        }, saved: false);
                    }
                    await articleToCategorySvc.Save();
                }
            }
        }
        #region 添加类型
        
        public async Task CreateCategory(string name, Guid userId)
        {
            using (var categorySvc = new BlogCategoryService())
            {
                await categorySvc.CreateAsync(new BlogCategory
                {
                    CategoryName = name,
                    UserId = userId

                });
            }
        }
        #endregion
        #region 编辑文章
        public async Task EditArticle(Guid articleId, string title, string content, Guid[] categoryIds)
        {
            using (IDAL.IArticleService articleService = new ArticleService())
            {
                //得到文章
                var artcle = await articleService.GetOneByIdAsync(articleId);
                artcle.Title = title; artcle.Content = content;
                await articleService.EditAsync(artcle);

                //类别
                using (IDAL.IArticleToCategoryService articleToCategoryService = new ArticleToCategoryService())
                {
                    //删除原有的类别  再添加现有的类别
                    foreach (var categoryId in articleToCategoryService.GetAllAsync().Where(s => s.ArticleId == articleId))
                    {
                        await articleToCategoryService.RemoveAsync(categoryId, false); //false先不保存做的操作很多
                    }

                    foreach (var categoryId in categoryIds)
                    {
                        await articleToCategoryService.CreateAsync(new ArticleToCategory { ArticleId = articleId, BlogCategoryId = categoryId }, false);
                    }
                    await articleToCategoryService.Save();
                }

            }
        }
        #endregion
        public async Task EditCategory(Guid categoryId, string newCategoryName)
        {
            throw new NotImplementedException();
        }
        //删除文章
        public async Task RemoveArticle(Guid articleId)
        {

            //    using (IDAL.IArticleToCategoryService articleToCategoryService = new ArticleToCategoryService())
            //{
            //    await articleToCategoryService.RemoveAsync(articleId,false);
            //}
            using (var articleSvc = new ArticleService())
            {
                await articleSvc.RemoveAsync(articleId, true);
            }
        }

        public Task RemoveCategory(string name)
        {
            throw new NotImplementedException();
        }
        public async Task<List<ArticleDto>> GetAllArticlesByCategoryId(Guid categoryIds)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ArticleDto>> GetAllArticlesByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ArticleDto>> GetAllArticlesByUserId(Guid userId, int pageIndex, int pageSize)
        {
            using (DAL.ArticleService articleService = new ArticleService())
            {
                var list = await articleService.GetAllByPageOrder(pageSize, pageIndex, false).Include(s => s.User).Where(s => s.UserId == userId)
                       .Select(s => new Dto.ArticleDto()
                       {
                           Title = s.Title,
                           BadCount = s.BadCount,
                           GoodCount = s.GoodCount,
                           Email = s.User.Email,
                           Content = s.Content,
                           CreateTime = s.CreateTime,
                           Id = s.Id,
                           ImagePath = s.User.ImagePath
                   
                       }).ToListAsync();
                using (IDAL.IArticleToCategoryService articleToCategoryService = new ArticleToCategoryService())
                {
                    foreach (var artcle in list)
                    {    //得到当前的所有分类
                        var nums = await articleToCategoryService.GetAllAsync().Include(s => s.BlogCategory).Where(s => s.ArticleId == artcle.Id).ToListAsync();
                        artcle.CategoryIds = nums.Select(s => s.BlogCategoryId).ToArray();
                        artcle.CategoryNames = nums.Select(s => s.BlogCategory.CategoryName).ToArray();
                    }
                    return list;
                }
            }
        }
        //获取某个用户的Category
        public async Task<List<BlogCategoryDto>> GetAllCategories(Guid userId)
        {
            using (IDAL.IBlogCategoryService categoryService = new BlogCategoryService())
            {
                return await categoryService.GetAllAsync().Where(s => s.UserId == userId).Select(
                         s => new Dto.BlogCategoryDto()
                         {
                             Id = s.Id,
                             CategoryName = s.CategoryName
                         }).ToListAsync();
            }
        }
        //是否存在文章
        public async Task<bool> ExistsArticle(Guid artcleId)
        {
            using (DAL.ArticleService articleService = new ArticleService())
            {
                return await articleService.GetAllAsync().AnyAsync(s => s.Id == artcleId);
            }
        }

        public async Task<ArticleDto> GetOneArticleById(Guid artcleId)
        {
            using (DAL.ArticleService articleService = new ArticleService())
            {
                var data = await articleService.GetAllAsync().Include(s => s.User)
                    .Where(s => s.Id == artcleId)
                    .Select(s => new Dto.ArticleDto()
                    {
                        Id = s.Id,
                        BadCount = s.BadCount,
                        Title = s.Title,
                        Content = s.Content,
                        CreateTime = s.CreateTime,
                        Email = s.User.Email,
                        GoodCount = s.GoodCount,
                        ImagePath = s.User.ImagePath,
                        //
                        UserId=s.User.Id
                    }).FirstAsync();
                using (IDAL.IArticleToCategoryService articleToCategoryService = new ArticleToCategoryService())
                {

                    var nums = await articleToCategoryService.GetAllAsync().Include(s => s.BlogCategory).Where(s => s.ArticleId == data.Id).ToListAsync();
                    data.CategoryIds = nums.Select(s => s.BlogCategoryId).ToArray();
                    data.CategoryNames = nums.Select(s => s.BlogCategory.CategoryName).ToArray();
                    return data;
                }

            }

        }
        //获取总页码数
        public async Task<int> GetDataCount(Guid userId)
        {
            using (IDAL.IArticleService articleService = new ArticleService())
            {
                //总条数
                return await articleService.GetAllAsync().CountAsync(s => s.UserId == userId);
            }
        }
        //获取总页码数
        public async Task<int> GetDataCount()
        {
            using (IDAL.IArticleService articleService = new ArticleService())
            {
                //总条数
                return await articleService.GetAllAsync().CountAsync();
            }
        }

        public async Task CreateComment(Guid userId, Guid artcleId, string Content)
        {
            using (IDAL.ICommentService commentService = new CommentService())
            {
                await commentService.CreateAsync(new Comment()
                {
                    UserId = userId,
                    AritcleId = artcleId,
                    Content = Content

                });
            }
        }


        //public async Task<List<Dto.CommentDto>> GetCommentByArtcleId(Guid artcleId)
        //{
        //    using (IDAL.ICommentService commentService = new CommentService())
        //    {
        //        return await commentService.GetAllAsync().Where(m => m.AritcleId == artcleId)
        //             .Include(m => m.User)
        //             .Select(m => new Dto.CommentDto()
        //             {
        //                 Id = m.Id,
        //                 ArtcleId = m.AritcleId,
        //                 UserId = m.UserId,
        //                 Email = m.User.Email,
        //                 Content = m.Content,
        //                 CreateTime = m.CreateTime
        //             }).ToListAsync();
        //    }
        //}

        //查看文章=对应的评论
        public async Task<List<Dto.CommentDto>> GetCommentsByArtcleId(Guid artcleId)
        {
            using (IDAL.ICommentService commentService = new CommentService())
            {
                return await commentService.GetAllOrder(false).Where(m => m.AritcleId == artcleId)
                     .Include(m => m.User)
                     .Select(m => new Dto.CommentDto()
                     {
                         Id = m.Id,
                         ArtcleId = m.AritcleId,
                         UserId = m.UserId,
                         Email = m.User.Email,
                         Content = m.Content,
                         CreateTime = m.CreateTime
                     }).ToListAsync();
            }
        }

        public async Task GoodCountAdd(Guid artcleId)
        {
            using (IDAL.IArticleService articleService = new ArticleService())
            {
                var artcle = await articleService.GetOneByIdAsync(artcleId);
                artcle.GoodCount++;
                await articleService.EditAsync(artcle);
            }
        }

        public async Task BadCountAdd(Guid artcleId)
        {
            using (IDAL.IArticleService articleService = new ArticleService())
            {
                var artcle = await articleService.GetOneByIdAsync(artcleId);
                artcle.BadCount++;
                await articleService.EditAsync(artcle);
            }
        }
        //查询所有文章
        public async Task<List<ArticleDto>> GetArticle()
        {
            using (DAL.ArticleService articleService = new ArticleService())
            {
                var list = await articleService.GetAllAsync().Where(s=>s.Status).Include(s => s.User)
                       .Select(s => new Dto.ArticleDto()
                       {
                           Title = s.Title,
                           Content = s.Content,
                           //BadCount = s.BadCount,
                           //GoodCount = s.GoodCount,
                           //Email = s.User.Email,

                           CreateTime = s.CreateTime,
                           Id = s.Id,
                           Status=s.Status
                           //ImagePath = s.User.ImagePath
                       }).ToListAsync();
                using (IDAL.IArticleToCategoryService articleToCategoryService = new ArticleToCategoryService())
                {
                    foreach (var artcle in list)
                    {    //得到当前的所有分类
                        var nums = await articleToCategoryService.GetAllAsync().Include(s => s.BlogCategory).Where(s => s.ArticleId == artcle.Id).ToListAsync();
                        artcle.CategoryIds = nums.Select(s => s.BlogCategoryId).ToArray();
                        artcle.CategoryNames = nums.Select(s => s.BlogCategory.CategoryName).ToArray();
                    }
                    return list;
                }
            }
        }

        //public async Task CreateReply(Guid userId, Guid artcleId, Guid commentId, string content)
        //{
        //    using ( IDAL.IReplyService replyService =new ReplyService())
        //    {
        //        var reply = new Reply()
        //        {
        //            AritcleId = artcleId,
        //            UserId = userId,
        //            Content = content
        //        };
        //        await replyService.CreateAsync(reply);
        //        Guid replyId = reply.Id;
        //        using (IDAL.ICommentToReplyService CommentToReplySvc = new DAL.CommentToReplyService())
        //        {
        //            foreach (var commentId in commentIds)
        //            {
        //                await CommentToReplySvc.CreateAsync(new CommentToReply()
        //                {
        //                    CommentId = commentId,
        //                    ReplyId = replyId
        //                }, saved: false);

        //            }
        //            await CommentToReplySvc.Save();
        //        }
        //    }

        //}
        //添加
        public async Task CreateReply(Guid userId, Guid artcleId, Guid[] commentId, string content)
        {
            using (IDAL.IReplyService replyService = new ReplyService())
            {
                var reply = new Reply()
                {
                    AritcleId = artcleId,
                    UserId = userId,
                    Content = content
                };
                await replyService.CreateAsync(reply);
                Guid replyId = reply.Id;
                using (IDAL.ICommentToReplyService commenttoreplysvc = new DAL.CommentToReplyService())
                {
                    foreach (var cmd in commentId)
                    {
                        await commenttoreplysvc.CreateAsync(new CommentToReply()
                        {
                            CommentId = cmd,
                            ReplyId = replyId

                        }, saved: false);

                    }
                    await commenttoreplysvc.Save();
                }
            }

        }



        public async Task<List<ReplyDto>> GetAllReply( Guid artcleId)
        {
            using (IDAL.IReplyService replyService = new ReplyService())
            {
                var list = await replyService.GetAllOrder(true)
                    .Where(s => s.AritcleId == artcleId)
                    .Include(s=>s.User)
                    .Select(s => new ReplyDto()
                    {
                        Id = s.Id,
                        Content = s.Content,
                        UserId=s.UserId,
                        Email=s.User.Email,
                        CreateTime=s.CreateTime
                    }).ToListAsync();
                return list;
            }

        }

        public async Task<List<CommentToReplyDto>> GetAllCommentToReply(Guid[] commentIds, Guid artcleId)
        {
            throw new NotImplementedException();
        }

        public  async Task  UpdateArtcleStatusByArtcleId(Guid id)
        {
            using (IDAL.IArticleService artSvc=new  DAL.ArticleService())
            {
                var data=await artSvc.GetOneByIdAsync(id);
                data.Status = true;
                await artSvc.EditAsync(data);
            }
        }

        public async Task<List<ArticleDto>> GetArticleByStatusFalse()
        {
            using (DAL.ArticleService articleService = new ArticleService())
            {
                var list = await articleService.GetAllAsync().Where(s=>!s.Status).Include(s => s.User)
                       .Select(s => new Dto.ArticleDto()
                       {
                           Title = s.Title,
                           Content = s.Content,
                            
                           //BadCount = s.BadCount,
                           //GoodCount = s.GoodCount,
                           Email = s.User.Email,

                           //CreateTime = s.CreateTime,

                           Id = s.Id,
                           Status = s.Status
                           //ImagePath = s.User.ImagePath
                       }).ToListAsync();
                using (IDAL.IArticleToCategoryService articleToCategoryService = new ArticleToCategoryService())
                {
                    foreach (var artcle in list)
                    {    //得到当前的所有分类
                        var nums = await articleToCategoryService.GetAllAsync().Include(s => s.BlogCategory).Where(s => s.ArticleId == artcle.Id).ToListAsync();
                        artcle.CategoryIds = nums.Select(s => s.BlogCategoryId).ToArray();
                        artcle.CategoryNames = nums.Select(s => s.BlogCategory.CategoryName).ToArray();
                    }
                    return list;
                }
            }
        }



        //public async Task<List<CommentToReplyDto>> GetAllCommentToReply(Guid commentIds,Guid artcleId)
        //{
        //    //using (IDAL.ICommentToReplyService commentToReplyService=new DAL.CommentToReplyService())
        //    //{
        //    // await   commentToReplyService.GetAllAsync().Where(a=>a.CommentId==commentIds)
        //    //        .Select(a=>new Dto.CommentToReplyDto() { 
        //    //        CommentId=a.CommentId,


        //    //        })    
        //    //}
        //}

        //public async Task<List<ReplyDto>> GetAllReply(Guid userId, Guid artcleId, Guid commentIds)
        //{

        //  var  commentdata=  await GetCommentsByArtcleId(artcleId);
        //    using (IDAL.ICommentToReplyService commentToReplySvc = new DAL.CommentToReplyService())
        //    {
        //        var nums = await commentToReplySvc.GetAllAsync().Include(s => s.CommentId).Where(s => s.CommentId == commentIds).ToListAsync();

        //      return 

        //        }
        //    //using (DAL.ReplyService replyService=new ReplyService())
        //    //{
        //    //    var data = replyService.GetAllAsync().Where(a => a.AritcleId == artcleId)
        //    //            .Select(a => new Dto.ReplyDto()
        //    //            { 
        //    //                Content = a.Content
        //    //            }).ToArrayAsync();
        //    //    using (IDAL.ICommentToReplyService commentToReplySvc=new DAL.CommentToReplyService())
        //    //    {
        //    //  var nums=      await  commentToReplySvc.GetAllAsync().Include(s => s.CommentId).Where(s => s.CommentId == commentIds).ToListAsync();
        //    //        data.
        //    //    }
        //    //}
        //}

        //public async Task<List<ReplyDto>> GetAllReply(Guid userId, Guid artcleId, Guid commentIds)
        //{
        //    //using (IDAL.ICommentService commentService=new DAL.CommentService())
        //    //{
        //    //  var data=   await commentService.GetAllAsync().Where(s => s.UserId == userId && s.AritcleId == artcleId).ToListAsync();
        //    //    using (IDAL.ICommentToReplyService commentToReplySvc=new DAL.CommentToReplyService())
        //    //    {
        //    //        foreach (var item in data)
        //    //        {

        //    //        }
        //    //    }
        //    //}
        //}
    }
}
