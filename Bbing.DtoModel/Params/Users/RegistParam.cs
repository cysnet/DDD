using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bbing.DtoModel.Params.Users
{
    public class RegistParam
    {
        [Required(ErrorMessage = "{0}不能为空")]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "{0}不能为空")]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [Required(ErrorMessage = "{0}不能为空")]
        [Compare("Password", ErrorMessage = "密码必须一致")]
        [Display(Name = "确认密码")]
        public string ConfirmPassword { get; set; }
    }
}
