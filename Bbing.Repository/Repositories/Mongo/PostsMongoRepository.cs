using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bbing.Domain.Model;
using MongoDB.Driver;

namespace Bbing.Repository.Repositories.Mongo
{
    public partial class PostsMongoRepository
    {
        /// <summary>
        /// 添加评论
        /// </summary>
        /// <param name="comment"></param>
        /// <param name="postId"></param>
        public void AddComment(Comment comment, string postId)
        {
            FilterDefinition<Posts> filter = Filter.Where(m => m.ObjId == postId);
            var update = UpdateBuilder.Push("Comments", comment);
            Collection.UpdateOne(filter, update);
        }

        /// <summary>
        /// 点赞
        /// </summary>
        /// <param name="praise"></param>
        /// <param name="postId"></param>
        public void AddPraise(Praise praise, string postId)
        {
            FilterDefinition<Posts> filter = Filter.Where(m => m.ObjId == postId);
            var update = UpdateBuilder.Push("Praises", praise);
            Collection.UpdateOne(filter, update);
        }
    }
}
