using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bbing.DtoModel.Params.Posts
{
    public class AddPraiseParam
    {
        /// <summary>
        /// 帖子Id
        /// </summary>
        [Required(ErrorMessage = "{0}不能为空")]
        [Display(Name = "帖子")]
        public string PostId { get; set; }
    }
}
