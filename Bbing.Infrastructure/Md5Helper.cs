using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Bbing.Infrastructure
{
    public static class Md5Helper
    {
        /// <summary>
        /// string => md5
        /// </summary>
        public static string ToMD5(this string str, string key = "Bbing2018")
        {
            StringBuilder sb = new StringBuilder(32);

            str = str + key;

            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] t = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            for (int i = 0; i < t.Length; i++)
            {
                sb.Append(t[i].ToString("x").PadLeft(2, '0'));
            }

            return sb.ToString();
        }
    }
}
