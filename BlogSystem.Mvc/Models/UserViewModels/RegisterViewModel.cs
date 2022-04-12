using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogSystem.Mvc.Models.UserViewModels
{
    public class RegisterViewModel
    {
    
        [ Display(Name ="邮箱"),Required,EmailAddress]
        public string  Email { get; set; }
        [Display(Name = "密码"),Required,StringLength(50),MinLength(6)]
        public string Password  { get; set; }
        //确认密码
        [Display(Name = "确定密码"),Required,Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}