using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bbing.Domain.BaseModel;
using MongoDB.Bson.Serialization.Attributes;

namespace Bbing.Domain.Model
{
    /// <summary>
    /// 帖子
    /// </summary>
    [BsonIgnoreExtraElements]
    public class Posts : AggregateRoot
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 摘要
        /// </summary>
        public string Introduce { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 评论：只有一层
        /// </summary>
        public List<Comment> Comments { get; set; }

        /// <summary>
        /// 点赞
        /// </summary>
        public List<Praise> Praises { get; set; }
    }


}
