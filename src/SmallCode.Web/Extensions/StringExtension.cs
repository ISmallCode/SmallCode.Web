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


        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="s"></param>
        /// <param name="l"></param>
        /// <param name="endStr"></param>
        /// <returns></returns>
        public static string SubString(this string s, int l, string endStr)
        {
            string temp = s.Substring(0, (s.Length < l + 1) ? s.Length : l + 1);
            byte[] encodedBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(temp);

            string outputStr = "";
            int count = 0;

            for (int i = 0; i < temp.Length; i++)
            {
                if ((int)encodedBytes[i] == 63)
                    count += 2;
                else
                    count += 1;

                if (count <= l - endStr.Length)
                    outputStr += temp.Substring(i, 1);
                else if (count > l)
                    break;
            }

            if (count <= l)
            {
                outputStr = temp;
                endStr = "";
            }

            outputStr += endStr;

            return outputStr;
        }

    }
}
