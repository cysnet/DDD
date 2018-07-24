using Bbing.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bbing.EF.Repository.Repositories.SqlServer
{
    public partial class PostsEFRepository
    {
        public void AddComment(Comment comment, string postId)
        {
           
        }

        /// <summary>
        /// 点赞
        /// </summary>
        /// <param name="praise"></param>
        /// <param name="postId"></param>
        public void AddPraise(Praise praise, string postId)
        {

        }
    }
}
