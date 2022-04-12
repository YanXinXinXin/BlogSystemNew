using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogSystem.Models;
using System.Data.Entity;
using BlogSystem.IDAL;
namespace BlogSystem.DAL
{
    //实现接口
    public class BaseService<T> : IBaseService<T> where T : BaseEntity, new()
    {
       
        public readonly BlogContext _db;
        public BaseService(Models.BlogContext db)
        {
            this._db = db;

        }
        public async Task CreateAsync(T model, bool saved = true)
        {
            _db.Set<T>().Add(model);
            if (saved) await _db.SaveChangesAsync();
        }

        public async Task EditAsync(T model, bool saved = true)
        {
            //修改之前先关闭它 就不会去校验
            _db.Configuration.ValidateOnSaveEnabled = false;
            _db.Entry(model).State = EntityState.Modified;
            if (saved)
            {
                await _db.SaveChangesAsync();
                _db.Configuration.ValidateOnSaveEnabled = true;
            }
        }

        public async Task RemoveAsync(Guid id, bool saved = true)
        {
            _db.Configuration.ValidateOnSaveEnabled = false;
            var t = new T() { Id = id };
            _db.Entry(t).State = EntityState.Unchanged;
            t.IsRemoved = true;
            if (saved)
            {
                await _db.SaveChangesAsync();
                _db.Configuration.ValidateOnSaveEnabled = true;
            };
        }

        public async Task RemoveAsync(T model, bool saved = true)
        {
            await RemoveAsync(model.Id, saved);
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
            _db.Configuration.ValidateOnSaveEnabled = true;
        }
        /// <summary>
        /// 返回所有未被删除的数据（没有真的执行sql语句） IQueryable没有得到数据就不需要异步
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetAllAsync()
        {
            return _db.Set<T>().Where(s => !s.IsRemoved).AsNoTracking();
        }

        public async Task<T> GetOneByIdAsync(Guid id)
        {
            return await GetAllAsync().FirstAsync(s => s.Id == id);
        }
        public IQueryable<T> GetAllByPage(int pageSize = 10, int pageIndex = 0)
        {
            return GetAllAsync().Skip(pageSize * pageIndex).Take(pageSize);
        }
        public IQueryable<T> GetAllOrder(bool asc = true)
        {
            var datas = GetAllAsync();
            datas = asc ? datas.OrderBy(s => s.CreateTime) : datas.OrderByDescending(s => s.CreateTime);
            return datas;
        }
        public IQueryable<T> GetAllByPageOrder(int pageSize = 10, int pageIndex = 0, bool asc = true)
        {
            return GetAllOrder(asc).Skip(pageSize * pageIndex).Take(pageSize);

        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
