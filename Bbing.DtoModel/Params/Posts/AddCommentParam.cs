using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bbing.DtoModel.Params.Posts
{
    public class AddCommentParam
    {
        /// <summary>
        /// 评论内容
        /// </summary>
        [Required(ErrorMessage = "{0}不能为空")]
        [Display(Name = "评论")]
        public string Content { get; set; }

        /// <summary>
        /// 帖子Id
        /// </summary>
        [Required(ErrorMessage = "{0}不能为空")]
        [Display(Name = "帖子")]
        public string PostId { get; set; }

        /// <summary>
        /// 父评论Id（可选）
        /// </summary>
        public string ParentId { get; set; }
    }
}
