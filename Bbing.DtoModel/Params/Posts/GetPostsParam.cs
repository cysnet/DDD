using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bbing.DtoModel.BaseData;

namespace Bbing.DtoModel.Params.Posts
{
    public class GetPostsParam: PageList
    {
        /// <summary>
        /// 帖子类型
        /// </summary>
        public string Type { get; set; }
    }
}
