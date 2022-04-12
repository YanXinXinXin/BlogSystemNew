using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//用户类
namespace BlogSystem.Models
{
   public  class User:BaseEntity
    {/// <summary>
    /// 邮箱
    /// </summary>
        [Required,StringLength(40),Column(TypeName ="varchar")]
        public String Email { get; set; }
      
        /// <summary>
        /// 密码
        /// </summary>
        [Required,StringLength(60),Column(TypeName ="varchar")]
        public string Password { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        [Required, StringLength(300), Column(TypeName = "varchar")]
        public string ImagePath { get; set; }
        /// <summary>
        /// 粉丝数量
        /// </summary>
        public int FansConunt { get; set; }
        /// <summary>
        /// 关注数
        /// </summary>
        public int FocusCount { get; set; }
        /// <summary>
        /// 网站名
        /// </summary>
        public string  SitName { get; set; }

        public UserType UserType { get; set; }
    }
    public enum UserType
    {
        GeneralUser,
        Administrator
    }

}
