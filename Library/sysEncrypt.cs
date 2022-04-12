using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class sysEncrypt
    {
        /// <summary>
        /// 通用32为MD5机密算法
        /// </summary>
        /// <param name="encryptString"></param>
        /// <returns></returns>
        public static string GetMD5(string encryptString)
        {
            string cl = encryptString + "ASDFGHJKL";
            string pwd = "";
            MD5 md5 = MD5.Create(); //实例化一个md5对像
            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                pwd = pwd + s[i].ToString("X");
            }
            return pwd;
        }
    }
}
