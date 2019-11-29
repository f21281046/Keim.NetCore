using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Keim.Helper;

namespace Keim.Encry
{


    /// <summary> 
    /// AES加密/解密Helper
    /// </summary> 
    public class AESCryptoHelper
    {
        /// <summary>
        /// 默认密钥向量，16字节（128位）
        /// </summary>
        private static byte[] embeddedIvKeys = { 0x41, 0x72, 0x65, 0x79, 0x6F, 0x75, 0x6D, 0x79, 0x53, 0x6E, 0x6F, 0x77, 0x6D, 0x61, 0x6E, 0x3F };

        #region CBC模式**

        /// <summary>
        /// 加密字符串，使用内置128位密钥向量
        /// </summary>
        /// <param name="data">待解密字符串</param>
        /// <param name="key">密钥，16字节（128位）</param>
        /// <returns></returns>
        public static string EncodeCBC(string data, string key)
        {
            return EncodeCBCWithIv(data, key, embeddedIvKeys);
        }


        /// <summary>
        /// 加密字符串，使用自定义密钥向量
        /// </summary>
        /// <param name="data">待解密字符串</param>
        /// <param name="key">密钥，16字节（128位）</param>
        /// <param name="ivKeys">向量，16字节（128位）</param>
        /// <returns></returns>
        public static string EncodeCBCWithIv(string data, string key, byte[] ivKeys)
        {
            try
            {
                if (key.Length != 16)
                    throw new Exception("密码长度需要为16字节（128位）");

                RijndaelManaged rijndaelProvider = new RijndaelManaged();
                rijndaelProvider.Key = Encoding.UTF8.GetBytes(key);
                rijndaelProvider.Padding = PaddingMode.PKCS7;
                rijndaelProvider.Mode = CipherMode.CBC;
                rijndaelProvider.IV = ivKeys; //使用CBC模式，需要一个向量增加加密算法的强度

                ICryptoTransform rijndaelEncrypt = rijndaelProvider.CreateEncryptor();

                byte[] inputData = Encoding.UTF8.GetBytes(data);
                byte[] encryptedData = rijndaelEncrypt.TransformFinalBlock(inputData, 0, inputData.Length);

                return Convert.ToBase64String(encryptedData);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static string EncodeCBCWithIvToHex(string data, string key, byte[] ivKeys)
        {
            string str = EncodeCBCWithIv(data, key, ivKeys);
            return ToHex(str, "utf-8");
        }

        /// <summary>
        /// 解密字符串，使用内置128位密钥向量
        /// </summary>
        /// <param name="data">待解密字符串</param>
        /// <param name="key">密钥，16字节（128位）</param>
        /// <returns></returns>
        public static string DecodeCBC(string data, string key)
        {
            return DecodeCBCWithIv(data, key, embeddedIvKeys);
        }

        /// <summary>
        /// 解密字符串，使用自定义密钥向量
        /// </summary>
        /// <param name="data">待解密字符串</param>
        /// <param name="key">密钥，16字节（128位）</param>
        /// <param name="ivKeys">向量，16字节（128位）</param>
        /// <returns></returns>
        public static string DecodeCBCWithIv(string data, string key, byte[] ivKeys)
        {
            try
            {
                if (key.Length != 16)
                    throw new Exception("密码长度需要为16字节（128位）");

                RijndaelManaged rijndaelProvider = new RijndaelManaged();
                rijndaelProvider.Key = Encoding.UTF8.GetBytes(key);
                rijndaelProvider.Padding = PaddingMode.PKCS7;
                rijndaelProvider.Mode = CipherMode.CBC;
                rijndaelProvider.IV = ivKeys; //使用CBC模式，需要一个向量增加加密算法的强度

                ICryptoTransform rijndaelDecrypt = rijndaelProvider.CreateDecryptor();

                byte[] inputData = Convert.FromBase64String(data);
                byte[] decryptedData = rijndaelDecrypt.TransformFinalBlock(inputData, 0, inputData.Length);

                return Encoding.UTF8.GetString(decryptedData);
            }
            catch (Exception e)
            {
                throw e;
            }
        }// end
        #endregion

        public static string DecodeCBCWithIvUnHex(string data, string key, byte[] ivKeys)
        {
            string str = UnHex(data, "utf-8");
            return DecodeCBCWithIv(str, key, ivKeys);
        }

        // <summary> 
        /// 从汉字转换到16进制 
        /// </summary> 
        /// <param name="strVal"></param> 
        /// <param name="charset">编码,如"utf-8","gb2312"</param>
        /// <returns></returns> 
        public static string ToHex(string strVal, string charset = "utf-8")
        {
            //if ((strVal.Length % 2) != 0)
            //{
            //    strVal += " ";//空格 
            //    //throw new ArgumentException("s is not valid chinese string!"); 
            //}
            System.Text.Encoding chs = System.Text.Encoding.GetEncoding(charset);
            byte[] bytes = chs.GetBytes(strVal);

            StringBuilder sb = new StringBuilder();
            foreach (byte item in bytes)
            {
                sb.Append(item.ToString("x2"));
            }
            return sb.ToString();
        }

        public static string UnHex(string hex, string charset = "utf-8")
        {
            if (hex == null)
                throw new ArgumentNullException("hex");
            hex = hex.Replace(",", "");
            hex = hex.Replace("\n", "");
            hex = hex.Replace("\\", "");
            hex = hex.Replace(" ", "");
            if (hex.Length % 2 != 0)
            {
                hex += "20";//空格 
            }
            // 需要将 hex 转换成 byte 数组。 
            byte[] bytes = new byte[hex.Length / 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                try
                {
                    //每两个字符是一个 byte。 
                    bytes[i] = byte.Parse(hex.Substring(i * 2, 2),
                    System.Globalization.NumberStyles.HexNumber);
                }
                catch
                {
                    // Rethrow an exception with custom message. 
                    throw new ArgumentException("hex is not a valid hex number!", "hex");
                }
            }
            System.Text.Encoding chs = System.Text.Encoding.GetEncoding(charset);
            return chs.GetString(bytes);
        }

    }//endclass

    /// <summary> 
    /// DES加密/解密Helper
    /// </summary> 
    public class DESCryptoHelper
    {
        /// <summary>
        /// 默认密钥向量，8字节（64位）
        /// </summary>
        private static byte[] embeddedIvKeys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

        #region CBC模式**

        /// <summary>
        /// DES加密字符串，使用内置64位密钥向量
        /// </summary>
        /// <param name="data">待加密的字符串</param>
        /// <param name="key">密钥,8字节（64位）</param>
        /// <returns>加密成功返回加密后的字符串,失败返回源串</returns>
        public static string Encode(string data, string key)
        {
            key = StringHelper.GetSubString(key, 8, "");
            key = key.PadRight(8, ' ');
            byte[] rgbKey = Encoding.UTF8.GetBytes(key.Substring(0, 8));
            byte[] rgbIV = embeddedIvKeys;
            byte[] inputByteArray = Encoding.UTF8.GetBytes(data);

            DESCryptoServiceProvider dcsp = new DESCryptoServiceProvider();
            dcsp.Mode = CipherMode.CBC;             //默认值
            dcsp.Padding = PaddingMode.PKCS7;       //默认值

            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream,
                dcsp.CreateEncryptor(rgbKey, rgbIV),
                CryptoStreamMode.Write);

            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            return Convert.ToBase64String(mStream.ToArray());

        }

        /// <summary>
        /// DES解密字符串，使用内置64位密钥向量
        /// </summary>
        /// <param name="data">待解密的字符串</param>
        /// <param name="key">密钥,8字节（64位）,和加密密钥相同</param>
        /// <returns>解密成功返回解密后的字符串,失败返源串</returns>
        public static string Decode(string data, string key)
        {
            try
            {
                key = StringHelper.GetSubString(key, 8, "");
                key = key.PadRight(8, ' ');
                byte[] rgbKey = Encoding.UTF8.GetBytes(key);
                byte[] rgbIV = embeddedIvKeys;
                byte[] inputByteArray = Convert.FromBase64String(data);

                DESCryptoServiceProvider dcsp = new DESCryptoServiceProvider(); //默认为CipherMode.CBC,PaddingMode.PKCS7
                dcsp.Mode = CipherMode.CBC;             //默认值
                dcsp.Padding = PaddingMode.PKCS7;       //默认值

                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream,
                    dcsp.CreateDecryptor(rgbKey, rgbIV),
                    CryptoStreamMode.Write);

                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();

                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

    }//endclass

    /// <summary> 
    ///  3DES加密/解密Helper
    /// </summary> 
    public class DES3CryptoHelper
    {
        /// <summary>
        /// 默认密钥向量，8字节（64位）
        /// </summary>
        private static byte[] embeddedIvKeys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

        #region CBC模式**

        /// <summary>
        /// DES3 CBC模式加密,使用自定义向量
        /// </summary>
        /// <param name="data">待加密数据，明文的byte数组</param>
        /// <param name="key">密钥, byte数组, 24字节（192位）</param>
        /// <param name="ivKeys">向量,byte数组, 8字节（64位）</param>
        /// <returns>密文的byte数组</returns>
        public static byte[] EncodeCBCWithIv(byte[] data, byte[] key, byte[] ivKeys)
        {
            //复制于MSDN

            try
            {
                if (key.Length != 24)
                    throw new Exception("密码长度需要为24字节（192位）");
                // Create a MemoryStream.
                MemoryStream mStream = new MemoryStream();

                TripleDESCryptoServiceProvider tdsp = new TripleDESCryptoServiceProvider();
                tdsp.Mode = CipherMode.CBC;             //默认值
                tdsp.Padding = PaddingMode.PKCS7;       //默认值

                // Create a CryptoStream using the MemoryStream 
                // and the passed key and initialization vector (IV).
                CryptoStream cStream = new CryptoStream(mStream,
                    tdsp.CreateEncryptor(key, ivKeys),
                    CryptoStreamMode.Write);

                // Write the byte array to the crypto stream and flush it.
                cStream.Write(data, 0, data.Length);
                cStream.FlushFinalBlock();

                // Get an array of bytes from the 
                // MemoryStream that holds the 
                // encrypted data.
                byte[] ret = mStream.ToArray();

                // Close the streams.
                cStream.Close();
                mStream.Close();

                // Return the encrypted buffer.
                return ret;
            }
            catch (CryptographicException e)
            {
                throw e;
            }
        }

        /// <summary>
        /// DES3 CBC模式解密,使用自定义向量
        /// </summary>
        /// <param name="data">待解密数据，密文的byte数组</param>
        /// <param name="key">密钥, byte数组, 24字节（192位）</param>
        /// <param name="ivKeys">向量,byte数组, 8字节（64位）</param>
        /// <returns>明文的byte数组</returns>
        public static byte[] DecodeCBCWithIv(byte[] data, byte[] key, byte[] ivKeys)
        {
            try
            {
                if (key.Length != 24)
                    throw new Exception("密码长度需要为24字节（192位）");
                // Create a new MemoryStream using the passed 
                // array of encrypted data.
                MemoryStream msDecrypt = new MemoryStream(data);

                TripleDESCryptoServiceProvider tdsp = new TripleDESCryptoServiceProvider();
                tdsp.Mode = CipherMode.CBC;
                tdsp.Padding = PaddingMode.PKCS7;

                // Create a CryptoStream using the MemoryStream 
                // and the passed key and initialization vector (IV).
                CryptoStream csDecrypt = new CryptoStream(msDecrypt,
                    tdsp.CreateDecryptor(key, ivKeys),
                    CryptoStreamMode.Read);

                // Create buffer to hold the decrypted data.
                byte[] fromEncrypt = new byte[data.Length];

                // Read the decrypted data out of the crypto stream
                // and place it into the temporary buffer.
                csDecrypt.Read(fromEncrypt, 0, fromEncrypt.Length);

                //Convert the buffer into a string and return it.
                return fromEncrypt;
            }
            catch (CryptographicException e)
            {
                throw e;
            }
        }

        #endregion

        #region ECB模式**

        /// <summary>
        /// DES3 ECB模式加密,使用自定义向量
        /// </summary>
        /// <param name="data">待加密数据，明文的byte数组</param>
        /// <param name="key">密钥, byte数组, 24字节（192位）</param>
        /// <param name="ivKeys">向量,byte数组, 8字节（64位）</param>
        /// <returns>密文的byte数组</returns>
        public static byte[] EncodeECBWithIv(byte[] data, byte[] key, byte[] ivKeys)
        {
            try
            {
                if (key.Length != 24)
                    throw new Exception("密码长度需要为24字节（192位）");
                // Create a MemoryStream.
                MemoryStream mStream = new MemoryStream();

                TripleDESCryptoServiceProvider tdsp = new TripleDESCryptoServiceProvider();
                tdsp.Mode = CipherMode.ECB;
                tdsp.Padding = PaddingMode.PKCS7;
                // Create a CryptoStream using the MemoryStream 
                // and the passed key and initialization vector (IV).
                CryptoStream cStream = new CryptoStream(mStream,
                    tdsp.CreateEncryptor(key, ivKeys),
                    CryptoStreamMode.Write);

                // Write the byte array to the crypto stream and flush it.
                cStream.Write(data, 0, data.Length);
                cStream.FlushFinalBlock();

                // Get an array of bytes from the 
                // MemoryStream that holds the 
                // encrypted data.
                byte[] ret = mStream.ToArray();

                // Close the streams.
                cStream.Close();
                mStream.Close();

                // Return the encrypted buffer.
                return ret;
            }
            catch (CryptographicException e)
            {
                throw e;
            }

        }

        /// <summary>
        /// DES3 ECB模式解密,使用自定义向量
        /// </summary>
        /// <param name="data">待解密数据，密文的byte数组</param>
        /// <param name="key">密钥, byte数组, 24字节（192位）</param>
        /// <param name="ivKeys">向量,byte数组, 8字节（64位）</param>
        /// <returns>明文的byte数组</returns>
        public static byte[] DecodeECBWithIv(byte[] data, byte[] key, byte[] ivKeys)
        {
            try
            {
                if (key.Length != 24)
                    throw new Exception("密码长度需要为24字节（192位）");
                // Create a new MemoryStream using the passed 
                // array of encrypted data.
                MemoryStream msDecrypt = new MemoryStream(data);

                TripleDESCryptoServiceProvider tdsp = new TripleDESCryptoServiceProvider();
                tdsp.Mode = CipherMode.ECB;
                tdsp.Padding = PaddingMode.PKCS7;

                // Create a CryptoStream using the MemoryStream 
                // and the passed key and initialization vector (IV).
                CryptoStream csDecrypt = new CryptoStream(msDecrypt,
                    tdsp.CreateDecryptor(key, ivKeys),
                    CryptoStreamMode.Read);

                // Create buffer to hold the decrypted data.
                byte[] fromEncrypt = new byte[data.Length];

                // Read the decrypted data out of the crypto stream
                // and place it into the temporary buffer.
                csDecrypt.Read(fromEncrypt, 0, fromEncrypt.Length);

                //Convert the buffer into a string and return it.
                return fromEncrypt;
            }
            catch (CryptographicException e)
            {
                throw e;
            }
        }

        #endregion

    }//endclass

    /// <summary>
    /// MD5Helper
    /// BASE64是编码算法（不算是真正的加密算法）
    /// MD5、SHA、HMAC这三种加密算法，可谓是非可逆加密，就是不可解密的加密方法，我们称之为单向加密算法。
    /// </summary>
    public class MD5Helper
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Encrypt(string str)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
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
        public static string Encrypt32MD5(string str)
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
        public static string HMACMD5(string str, string key)
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
        public static string HMACSHA1(string str, byte[] key)
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
        public static string SHA1_Hash(string str_sha1_in)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            byte[] bytes_sha1_in = UTF8Encoding.UTF8.GetBytes(str_sha1_in);
            byte[] bytes_sha1_out = sha1.ComputeHash(bytes_sha1_in);

            string str_sha1_out = BitConverter.ToString(bytes_sha1_out);
            str_sha1_out = str_sha1_out.Replace("-", "").ToLower();
            return str_sha1_out;
        }

    }//endclass
}
