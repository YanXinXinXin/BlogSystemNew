using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogSystem.Mvc.Models.UserViewModels
{
    public class LoginViewModel
    {
        [Required,EmailAddress,Display(Name ="电子邮箱")]
        public string Email  { get; set; }
        [Required,Display(Name ="密码"),StringLength(50,MinimumLength =6),DataType(DataType.Password)]
        public string LoginPwd { get; set; }
        [Display(Name ="记住我")]
        public bool RememberMe { get; set; }
    }
}