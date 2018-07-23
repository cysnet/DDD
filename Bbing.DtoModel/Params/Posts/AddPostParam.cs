using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bbing.DtoModel.Params.Posts
{
    public class AddPostParam
    {
        [Required(ErrorMessage = "{0}不能为空")]
        [Display(Name = "标题")]
        public string Title { get; set; }

        [Required(ErrorMessage = "{0}不能为空")]
        [Display(Name = "内容")]
        public string Content { get; set; }

        [Required(ErrorMessage = "{0}不能为空")]
        [Display(Name = "类型")]
        public string Type { get; set; }
    }
}
