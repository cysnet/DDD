using Bbing.Domain.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Bbing.Domain.Model
{
    /// <summary>
    /// 用户
    /// </summary>
    [BsonIgnoreExtraElements]
    public class Users : AggregateRoot
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 头像地址
        /// </summary>
        public string HeadUrl { get; set; }
    }
}
