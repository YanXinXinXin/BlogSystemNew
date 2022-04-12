using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Mdoel
{
    public class JsonBaseModel
    {

        private bool _Successful = false;

        public bool Successful
        {
            get { return _Successful; }
            set { _Successful = value; }
        }


        private string _Msg = string.Empty;

        public string Msg
        {
            get { return _Msg; }
            set { _Msg = value; }
        }
        /// <summary>
        /// 软件真正报错的原因详细记录
        /// </summary>
        public string _eMsg = string.Empty;
        public string eMsg
        {
            get { return _eMsg; }
            set { _eMsg = value; }
        }
        private int _Total = 0;

        public int Total
        {
            get { return _Total; }
            set { _Total = value; }
        }

        private int _code = 0;
        /// <summary>
        /// 为了根据不同的错误执行不同的操作，需要验证不同的code
        /// </summary>
        public int code
        {
            get { return _code; }
            set { _code = value; }
        }
    }



    public class JsonModel : JsonBaseModel
    {
        /// <summary>
        /// 数据结果
        /// </summary>
        public object _Data { get; set; }
        public object Data
        {
            get { return _Data; }
            set { _Data = value; }
        }
    }
    public class JsonModel<T> : JsonBaseModel where T : class
    {
        /// <summary>
        /// 数据结果
        /// </summary>
        public T _Data { get; set; }
        public T Data
        {
            get { return _Data; }
            set { _Data = value; }
        }

    }
    /// <summary>
    /// 针对数据集合的泛型载体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class JsonListModel<T> : BasePagesModel where T : class, new()
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        private bool _Successful { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        private string _Msg { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        private List<T> _Data { get; set; }
        /// <summary>
        /// 总的记录数（查出来的总条数)
        /// </summary>
        private int _Total { get; set; }
        public bool Successful
        {
            get { return _Successful; }
            set { _Successful = value; }
        }
        public string Msg
        {
            get { return _Msg; }
            set { _Msg = value; }
        }
        public int Total
        {
            get { return _Total; }
            set { _Total = value; }
        }



        /// <summary>
        /// 数据返回结果集
        /// </summary>
        public List<T> Data
        {
            get { return _Data; }
            set { _Data = value; }
        }
        /// <summary>
        /// 软件真正报错的原因详细记录
        /// </summary>
        public string eMsg { get; set; }
    }

    public class apiJsonModel
    {
        private int _Code = 0;

        public int Code
        {
            get { return _Code; }
            set { _Code = value; }
        }


        private string _Message = string.Empty;

        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }
        /// <summary>
        /// 数据结果
        /// </summary>
        public object _Data { get; set; }
        public object Data
        {
            get { return _Data; }
            set { _Data = value; }
        }


    }

    public class apiJsonListModel<T> : BasePagesModel where T : class, new()
    {
        private int _Code = 0;

        public int Code
        {
            get { return _Code; }
            set { _Code = value; }
        }


        private string _Message = string.Empty;

        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }
        /// <summary>
        /// 数据
        /// </summary>
        private List<T> _Data { get; set; }


        /// <summary>
        /// 数据返回结果集
        /// </summary>
        public List<T> Data
        {
            get { return _Data; }
            set { _Data = value; }
        }


    }
}
