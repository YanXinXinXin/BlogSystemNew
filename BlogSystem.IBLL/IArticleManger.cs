using BlogSystem.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.IBLL
{
    public interface IArticleManger
    {
        //添加
        Task CreateArticle(string title, string content, Guid[] categoryIds, Guid userId);

        Task CreateCategory(string name, Guid userId);
        //查看某个用户的所有类别
        Task<List<Dto.BlogCategoryDto>> GetAllCategories(Guid userId);

        //根据用户来找文章
        Task<List<Dto.ArticleDto>> GetAllArticlesByUserId(Guid userId,int pageIndex,int pageSize);
        //获取总页码
        Task<int> GetDataCount(Guid userId);

        Task<List<Dto.ArticleDto>> GetAllArticlesByEmail(string email);
        //根据类别来找文章
        Task<List<Dto.ArticleDto>> GetAllArticlesByCategoryId(Guid categoryIds);
        //
        Task RemoveCategory(string name);

        Task EditCategory(Guid categoryId, string newCategoryName);

        Task RemoveArticle(Guid articleId);
        //修改
        Task EditArticle(Guid articleId, string title, string content, Guid[] categoryIds);
        //是否存在文章
        Task<bool> ExistsArticle(Guid artcleId);
        //通过id得到文章类容
        Task<Dto.ArticleDto> GetOneArticleById(Guid artcleId);

        Task CreateComment(Guid userId, Guid artcleId, string content);

        Task<List<Dto.CommentDto>> GetCommentsByArtcleId(Guid artcleId);
        //点赞

        Task GoodCountAdd(Guid artcleId);

        //反对
        Task BadCountAdd(Guid artcleId);

        //查询所有的文章
        Task<List<Dto.ArticleDto>> GetArticle();
        Task<List<Dto.ArticleDto>> GetArticleByStatusFalse();
        //添加回复
        Task CreateReply(Guid userId, Guid artcleId,Guid[]commentIds, string content);
        ////显示回复
        //Task<List<ReplyDto>> GetAllReply(Guid userId, Guid artcleId, Guid commentIds);
        //Task<List<Dto.CommentToReplyDto>> GetAllCommentToReply(Guid commentIds, Guid artcleId);
        Task<List<ReplyDto>> GetAllReply( Guid artcleId);
      Task<List<Dto.CommentToReplyDto>> GetAllCommentToReply(Guid[] commentIds, Guid artcleId);
        //Task<List<Dto.FansDto>> FancList(Guid userId);

        Task UpdateArtcleStatusByArtcleId(Guid id);
    }
}
