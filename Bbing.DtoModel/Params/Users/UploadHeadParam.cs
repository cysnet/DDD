using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bbing.DtoModel.Params.Users
{
    public class UploadHeadParam
    {
        [Required(ErrorMessage = "{0}不能为空")]
        [Display(Name = "头像地址")]
        public string Url { get; set; }
    }
}
