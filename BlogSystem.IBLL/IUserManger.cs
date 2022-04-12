using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogSystem.IDAL;
using BlogSystem.Dto;
namespace BlogSystem.IBLL
{
    public interface IUserManger
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task Register(string email, string password);
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
      bool Login(string email, string password,out Guid userId,out int userType);
        //修改密码
        Task ChangePassword(string email, string oldPwd, string newPwd);
        //修改个人信息
        Task ChangeUserInformation(string email, string siteName, string ImagePath);
        //得到用户信息
        Task<Dto.UserInformationDto> GetUserByEmail(string email);
        Task<List<Dto.UserInformationDto>> GetAllUsers();

    }
}
