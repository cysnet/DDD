using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bbing.DtoModel.Params.Users
{
    public class ChangePwdParam
    {
        [Required(ErrorMessage = "{0}不能为空")]
        [Display(Name = "旧密码")]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "{0}不能为空")]
        [Display(Name = "新密码")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "{0}不能为空")]
        [Compare("NewPassword", ErrorMessage = "两次新密码必须一致")]
        [Display(Name = "确认新密码")]
        public string ConfirmNewPassword { get; set; }
    }
}
