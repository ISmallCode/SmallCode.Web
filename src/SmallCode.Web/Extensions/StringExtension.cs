using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SmallCode.Web.Extensions
{
    public static class StringExtension
    {
        /// <summary>
        /// 字符串转Enum
        /// </summary>
        /// <param name="inputValue"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this string inputValue)
        {
            return (T)Enum.Parse(typeof(T), inputValue);
        }

        /// <summary>
        /// Md5 加密
        /// </summary>
        /// <param name="inputValue"></param>
        /// <returns></returns>
        public static string ToMD5Hash(this string inputValue)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.UTF8.GetBytes(inputValue));
                var strResult = BitConverter.ToString(result);
                return strResult.Replace("-", "");
            }
        }

    }
}
