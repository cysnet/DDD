using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bbing.Domain.Model
{
    public class Praise
    {
        /// <summary>
        /// 点赞Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 点赞时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 帖子表外键
        /// </summary>
        public int Posts_Id { get; set; }
        [ForeignKey("Posts_Id")]
        public virtual Posts Posts { get; set; }
    }
}
