using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Bbing.Domain.IRepositories;
using Bbing.Domain.Model;
using Bbing.Domain.Service;
using Bbing.DtoModel.DBModel;
using Bbing.DtoModel.Params.Posts;
using Bbing.DtoModel.ResponseData;
using Bbing.Infrastructure;
using Bbing.WebApi.Filters;

namespace Bbing.WebApi.Controllers
{
    /// <summary>
    /// 帖子
    /// </summary>
    public class PostsController : BaseController
    {
        private IPostsService postsService;
        public PostsController(IPostsService postsService)
        {
            this.postsService = postsService;
        }

        /// <summary>
        /// 发布帖子
        /// </summary>
        [HttpPost, HttpAuth, DataValidate]
        public OutPutData AddPost([FromBody]AddPostParam param)
        {
            var post = new Posts()
            {
                Title = param.Title,
                Introduce = param.Content.ToText(),
                UserName = Loginer.UserName,
                Content = param.Content,
                Type = param.Type,
                Comments=new List<Comment>(),
                Praises=new List<Praise>()
                
            };
            if (post.Introduce.Length >= 66)
            {
                post.Introduce = post.Introduce?.Substring(0, 66) + "...";
            }
            postsService.CurrentRepository.Insert(post);
            return Success();
        }

        /// <summary>
        /// 获取帖子列表
        /// </summary>
        /// <returns></returns>
        public OutPutData GetPosts([FromUri]GetPostsParam param)
        {
            Expression<Func<Posts, bool>> expression = e => true;
            if (!string.IsNullOrEmpty(param.Type))
            {
                expression = e => e.Type == param.Type;
            }
            var result = postsService.CurrentRepository.GetPageListDesc(expression, e => new GetPostsModel
            {
                ObjId=e.ObjId,
                Title=e.Title,
                Introduce=e.Introduce,
                CreateTime = e.CreateTime,
                UserName=e.UserName,
                CommentCount = e.Comments.Count,
                PraiseCount=e.Praises.Count
            }, e => e.CreateTime, param.PageIndex, param.PageSize);
            var total = postsService.CurrentRepository.Entities.Count(expression);
            return Success("", new PageData
            {
                Total = total,
                Data = result
            });
        }

        /// <summary>
        /// 获取帖子详情
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public OutPutData GetPostDetail([FromUri]GetPostDetailParam param)
        {
            return Success("", postsService.CurrentRepository.GetOne(e => e.ObjId == param.Id, e => new { e.Content,e.Comments,e.Praises }));
        }

        /// <summary>
        /// 添加评论
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost, HttpAuth, DataValidate]
        public OutPutData AddComment([FromBody] AddCommentParam param)
        {
            Comment comment = new Comment
            {
                Content = param.Content,
                CreateTime = DateTime.Now,
                UserName = Loginer.UserName,
                ParentId = param.ParentId,
                Id = Guid.NewGuid().ToString(),
            };
            postsService.CurrentRepository.AddComment(comment, param.PostId);
            return Success();
        }

        /// <summary>
        /// 点赞
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost, HttpAuth, DataValidate]
        public OutPutData AddPraise([FromBody] AddPraiseParam param)
        {
            if (postsService.CurrentRepository.Exist(e =>e.ObjId==param.PostId&& e.Praises.Any(p => p.UserName == Loginer.UserName)))
            {
                return Fail("已点赞");
            }
            Praise praise = new Praise
            {
                CreateTime = DateTime.Now,
                UserName = Loginer.UserName,
                Id = Guid.NewGuid().ToString()
            };
            postsService.CurrentRepository.AddPraise(praise, param.PostId);
            return Success();
        }
    }
}
