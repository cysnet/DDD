using Bbing.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bbing.DtoModel.DBModel
{
    public class GetPostsModel
    {
        public string ObjId { get; set; }
        public string Title { get; set; }
        public string Introduce { get; set; }
        public DateTime CreateTime { get; set; }
        public string UserName { get; set; }
        public int CommentCount { get; set; }
        public int PraiseCount { get; set; }
    }
}
