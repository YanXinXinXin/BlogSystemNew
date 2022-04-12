using BlogSystem.Dto;
using BlogSystem.IBLL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.BLL
{
    public class FansManger : IFansManger
    {
        public async  Task<bool> AnyFan(Guid userId, Guid focusUserId)
        {
            using (DAL.FansService fansService = new DAL.FansService())
            {
           var ka=   await  fansService.GetAllAsync().Where(s => s.UserId == userId).AnyAsync(s => s.FocusUserId == focusUserId);
                return ka;
            }
            }

        public async Task CreateFan(Guid userId, Guid focusUserId)
        {

            if (await AnyFan(userId, focusUserId))
            {
                return;
            }
            else
            {
                using (IDAL.IFansService fansService = new DAL.FansService())
                {

                    var data = new Models.Fans()
                    {
                        UserId = userId,
                        FocusUserId = focusUserId
                    };
                    await fansService.CreateAsync(data);
                };
            }
               
              
            }

        public async Task<List<FansDto>> FancList(Guid userId)
        {
            using (IDAL.IFansService fansService = new DAL.FansService())
            {
            
               return  await fansService.GetAllAsync().Where(s => s.UserId == userId)
                       .Select(s => new Dto.FansDto()
                       {
                           UserId = userId
                       }).ToListAsync();

            };
        }

    }

        //public async Task<List<UserInformationDto>> FancList(Guid userId, Guid focusUserId)
        //{
        //    using (DAL.FansService fansService = new DAL.FansService())
        //    {
        //        fansService.GetAllOrder().Where(s=>s.UserId==userId).ToList().Select

        //    }
        //}

        //public async Task<List<FansDto>> FancList(Guid userId)
        //{
        //    using (IDAL.IFansService  fansService = new DAL.FansService())
        //    {
        //        var data = await fansService.GetAllOrder().Where(s => s.UserId == userId).ToList();

        //    }
        //}

        //public async Task FanList(Guid userId, Guid focusUserId)
        //{
        //    using (DAL.FansService fansService=new DAL.FansService())
        //    {
        //        fansService.GetAllAsync().Where(s=>s.UserId==userId).ToList
        //    }
        //}
        //public async Task<List<FansDto>> FancList(Guid userId)
        //{
        //    using (IDAL.IFansService fansService = new DAL.FansService())
        //    {
        //        var list = await fansService.GetAllOrder().Where(s => s.UserId == userId)
        //             .Select(s => new Dto.FansDto()
        //             {
        //                 UserId = s.UserId

        //             }).ToListAsync();
        //        return list;
        //    }
        //}
    }

