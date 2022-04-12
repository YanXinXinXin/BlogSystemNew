using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Mdoel
{
    /// <summary>
    /// 数据查询列表分页基类
    /// </summary>
    public class BasePagesModel
    {
        public BasePagesModel() { }
        public BasePagesModel(int pageindex, int pagesize, int totalcount, int code, bool ispage = false)
        {
            this.PageIndex = pageindex;
            this.PageSize = pagesize;
            this.TotalCount = totalcount;
            this.IsPage = ispage;
            this.code = code;
        }
        /// <summary>
        /// 当前页
        /// </summary>
        [NotMapped]
        public int PageIndex { get; set; }
        /// <summary>
        /// 每页条数
        /// </summary>
        [NotMapped]
        public int PageSize { get; set; }
        /// <summary>
        /// 总条数
        /// </summary>
        [NotMapped]
        public int TotalCount { get; set; }
        /// <summary>
        /// 是否分页
        /// </summary>
        [NotMapped]
        public bool IsPage { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [NotMapped]
        public int code { get; set; }
    }
}
