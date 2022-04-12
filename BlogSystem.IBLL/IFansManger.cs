using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.IBLL
{
    public interface IFansManger
    {
        Task<List<Dto.FansDto> >FancList(Guid userId);
        //Task<List<Dto.UserInformationDto>> Fancdetails(Guid userId);
        Task CreateFan(Guid userId,Guid focusUserId);
        //包含粉丝
        Task<bool> AnyFan(Guid userId, Guid focusUserId);
    }
}
