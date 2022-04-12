using BlogSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.IDAL
{  //接口是个规范 ，实现接口里面的方法
    public interface IBaseService<T>:IDisposable where T : BaseEntity
    {
        Task CreateAsync(T model, bool saved = true); //,执行这个方法会自动保存,bool saved=true

        Task EditAsync(T model, bool saved = true);

        Task RemoveAsync(Guid id, bool saved = true);

        Task RemoveAsync(T model, bool saved = true);

        //保存方法  
        Task Save();
        //单独一个对象
        Task<T> GetOneByIdAsync(Guid id);

            IQueryable<T> GetAllAsync();

        IQueryable<T> GetAllByPage(int pageSize = 10, int pageIndex = 0);

        IQueryable<T> GetAllOrder(bool asc = true);

        IQueryable<T> GetAllByPageOrder(int pageSize = 10, int pageIndex = 0, bool asc = true);
    }
}
