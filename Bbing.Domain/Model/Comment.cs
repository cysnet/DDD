using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace Bbing.Domain.Model
{
    /// <summary>
    /// 评论
    /// </summary>
    public class Comment
    {
        /// <summary>
        /// 评论Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 评论内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 评论时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 父评论Id
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 帖子表外键
        /// </summary>
        public int Posts_Id { get; set; }
        [ForeignKey("Posts_Id")]
        public virtual Posts Posts { get; set; }
    }
}
