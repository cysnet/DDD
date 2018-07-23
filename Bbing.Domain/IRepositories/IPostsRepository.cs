using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bbing.Domain.Model;

namespace Bbing.Domain.IRepositories
{
    public partial interface IPostsRepository
    {
        /// <summary>
        /// 添加评论
        /// </summary>
        /// <param name="comment"></param>
        /// <param name="postId"></param>
        void AddComment(Comment comment, string postId);
        /// <summary>
        /// 点赞
        /// </summary>
        /// <param name="praise"></param>
        /// <param name="postId"></param>
        void AddPraise(Praise praise, string postId);
    }
}
