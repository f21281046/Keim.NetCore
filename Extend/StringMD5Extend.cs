using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace System
{
    public static class StringMD5Extend
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Encrypt(this string str)
        {
            MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(str);
            bs = md5.ComputeHash(bs);
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2").ToUpper());
            }
            string password = s.ToString();
            return password;
        }


        /// <summary>
        /// MD5加密,返回小写
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Encrypt32MD5(this string str)
        {
            MD5 md5 = MD5.Create();
            //加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] bs = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            StringBuilder ps = new StringBuilder();
            foreach (byte b in bs)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
                ps.Append(b.ToString("x2"));
            }
            return ps.ToString();
        }

        /// <summary>
        /// HMACMD5签名算法
        /// MD5--Hash加密算法（本质上说不是加密算法，因为无法解密，准确来说是一种签名算法）
        /// </summary>
        /// <param name="str">数据</param>
        /// <param name="key">key</param>
        /// <returns></returns>
        public static string HMACMD5(this string str, string key)
        {
            using (HMACMD5 hmd5 = new HMACMD5())
            {
                Encoding ec = Encoding.UTF8;
                hmd5.Key = ec.GetBytes(key);
                byte[] hashBytes = hmd5.ComputeHash(ec.GetBytes(str));
                //string result = Convert.ToBase64String(hashBytes);

                //把二进制转化为大写的十六进制
                StringBuilder result = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    result.Append(hashBytes[i].ToString("x2"));
                }

                return result.ToString();
            }
        }

        /// <summary>
        /// HMACSHA1签名算法
        /// </summary>
        /// <param name="str"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string HMACSHA1(this string str, byte[] key)
        {
            using (HMACSHA1 hmacsha1 = new HMACSHA1())
            {
                Encoding ec = Encoding.UTF8;
                hmacsha1.Key = key;
                byte[] hashBytes = hmacsha1.ComputeHash(ec.GetBytes(str));
                string s = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
                return s;
            }
        }

        //SHA1
        public static string SHA1_Hash(this string str_sha1_in)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            byte[] bytes_sha1_in = UTF8Encoding.UTF8.GetBytes(str_sha1_in);
            byte[] bytes_sha1_out = sha1.ComputeHash(bytes_sha1_in);

            string str_sha1_out = BitConverter.ToString(bytes_sha1_out);
            str_sha1_out = str_sha1_out.Replace("-", "").ToLower();
            return str_sha1_out;
        }

        /// <summary>
        /// 创建随机号
        /// </summary>
        /// <param name="numberFlag"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string RandNumberHelper(bool numberFlag, int length)
        {
            string strTable = numberFlag ? "1234567890" : "1234567890abcdefghijkmnpqrstuvwxyz";
            System.Text.StringBuilder newRandom = new System.Text.StringBuilder(strTable.Length);
            Random rd = new Random();
            for (int i = 0; i < length; i++)
            {
                newRandom.Append(strTable[rd.Next(strTable.Length)]);
            }
            return newRandom.ToString();
        }
    }
}
