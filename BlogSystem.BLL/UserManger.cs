using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogSystem.DAL;
using BlogSystem.IDAL;
using BlogSystem.Dto;
using BlogSystem.IBLL;
using BlogSystem.Models;
using System.Data.Entity;
using Library;

namespace BlogSystem.BLL
{
    public class UserManger : IUserManger
    {
        //注册
        public async Task Register(string email, string password)
        {
            using (IDAL.IUserService userService = new DAL.UserService())
            {
                var pwd = sysEncrypt.GetMD5(password);
                await userService.CreateAsync(new User
                {
                    Email = email,
                    Password = pwd,
                    SitName = "默认的小站",
                    ImagePath = "1.jpg"
                });

             
            }
        }
        //登录     
        public bool Login(string email, string password,out Guid userId,out int userType)
        {       //不用异步达到异步同步化的效果
            using (IDAL.IUserService userService = new DAL.UserService())
            {
                var pwd = sysEncrypt.GetMD5(password);

                var user= userService.GetAllAsync().FirstOrDefaultAsync(s => s.Email == email && s.Password == pwd);//task任务
                user.Wait();//等待着
                var data = user.Result;//任务完成后得到结果
               
                if (data==null)
                {
                    userId = new Guid();
                    userType = 3;
                    return false;
                }
                else
                {
                      
                    userId = data.Id;
                    userType=(int) data.UserType;
                    return true;                                       
                }
            }

        }
        //修改密码
        public async Task ChangePassword(string email, string oldPwd, string newPwd)
        {
            using (IDAL.IUserService userService = new DAL.UserService())
            {
                if (await userService.GetAllAsync().AnyAsync(s => s.Email == email && s.Password == oldPwd))
                {
                       var user = await userService.GetAllAsync().FirstAsync(m => m.Email == email);
                    user.Password = newPwd;
                    await userService.EditAsync(user);
                }
            }
        }
        public async Task<UserInformationDto> GetUserByEmail(string email)
        {
            using (IDAL.IUserService userService = new DAL.UserService()) 
            {
                if (await userService.GetAllAsync().AnyAsync(s => s.Email == email))
                {
                    return await userService.GetAllAsync().Where(s => s.Email == email).Select(s => new Dto.UserInformationDto()
                    {
                        Id = s.Id,
                        Email = s.Email,
                        FacusCount = s.FansConunt,
                        FansCount = s.FansConunt,
                        ImagePath = s.ImagePath,
                        SiteName = s.SitName
                    }).FirstAsync();
                }
                else
                {
                    throw new ArgumentException("邮箱地址不存在");
                }
            }
        }
        public async Task ChangeUserInformation(string email, string siteName, string ImagePath)
        {                                                                                                                                                          
            using (IDAL.IUserService userService = new DAL.UserService())
            {

                var user = await userService.GetAllAsync().FirstAsync(m => m.Email == email);
                user.SitName = siteName; user.ImagePath = ImagePath;
                await userService.EditAsync(user);
            }
        }

        public async Task<List<UserInformationDto>> GetAllUsers()
        {
            using (IDAL.IUserService userService = new DAL.UserService())
            {
             var data=  await userService.GetAllAsync().Where(s=>s.UserType.ToString()== "GeneralUser").Select(s=> new Dto.UserInformationDto
                {
                    Id = s.Id,
                    Email = s.Email,
                    FacusCount = s.FansConunt,
                    FansCount = s.FansConunt,
                    ImagePath = s.ImagePath,
                    SiteName = s.SitName,
                    UserType =(int) s.UserType
                }
                    
                    ).ToListAsync() ;
                return data; 
            }
           
        }
    }
}



