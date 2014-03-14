using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Tools
{
    internal static class Utils
    {
        /// <summary>
        /// This hash function is used in social network on client side.
        /// </summary>
        /// <param name="src">Hash value recieved from server</param>
        /// <returns></returns>
        internal static String GetHash(string src)
        {
            var arr = new int[] { 4, 3, 5, 6, 1, 2, 8, 7, 2, 9, 3, 5, 7, 1, 4, 8, 8, 3, 4, 3, 1, 7, 3, 5, 9, 8, 1, 4, 3, 7, 2, 8 };
            var a = new List<Int32>();
            for (var i = 0; i < src.Length; i++)
            {
                var c = src[i];
                a.Add(Int32.Parse(c.ToString(), System.Globalization.NumberStyles.HexNumber));
            }
            var res = new List<Int32>();
            a.Add(a[31]);
            Int32 Sum = 0;
            for (var i = a.Count - 2; i >= 0; i--)
            {
                Sum += a[i];
            }
            for (var i = 0; i < a.Count - 1; i++)
            {
                res.Add(Math.Abs(Sum - a[i + 1] * a[i] * arr[i]));
            }
            String hash = "";
            foreach (var t in res)
            {
                hash += t.ToString();
            }
            return hash;
        }

        internal static String GetMd5Hash(String src)
        {
            byte[] hashed;
            using(var hasher = System.Security.Cryptography.MD5.Create())
            {
                hashed = hasher.ComputeHash(System.Text.Encoding.UTF8.GetBytes(src));
            }
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < hashed.Length; i++)
            {
                sBuilder.Append(hashed[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
}
