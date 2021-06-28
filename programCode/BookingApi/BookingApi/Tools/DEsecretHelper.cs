using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace BookingApi.Tools
{
    public class DEsecretHelper
    {
        /// <summary>
        /// 默认密钥-密钥的长度必须是32
        /// </summary>
        private const string PublicKey = "1F3654624ED64879";

        /// <summary>
        /// 默认向量
        /// </summary>
        private const string Iv = "1F3654624ED64879";
        /// <summary>  
        /// AES加密  
        /// </summary>  
        /// <param name="str">需要加密字符串</param>  
        /// <returns>加密后字符串</returns>  
        public static String Encrypt(string str)
        {
            return Encrypt(str, PublicKey);
        }

        /// <summary>  
        /// AES解密  
        /// </summary>  
        /// <param name="str">需要解密字符串</param>  
        /// <returns>解密后字符串</returns>  
        public static String Decrypt(string str)
        {
            return Decrypt(str, PublicKey);
        }
        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="str">需要加密的字符串</param>
        /// <param name="key">32位密钥</param>
        /// <returns>加密后的字符串</returns>
        public static string Encrypt(string str, string key)
        {
            Byte[] keyArray = Encoding.UTF8.GetBytes(key);
            Byte[] toEncryptArray = Encoding.UTF8.GetBytes(str);
            var rijndael = new RijndaelManaged();
            rijndael.Key = keyArray;
            rijndael.Mode = CipherMode.ECB;
            rijndael.Padding = PaddingMode.PKCS7;
            rijndael.IV = System.Text.Encoding.UTF8.GetBytes(Iv);
            ICryptoTransform cTransform = rijndael.CreateEncryptor();
            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            var sb = new StringBuilder();
            foreach (byte b in resultArray)
            {
                sb.AppendFormat("{0:X2}", b);
            }
            return sb.ToString();
            //return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="str">需要解密的字符串</param>
        /// <param name="key">32位密钥</param>
        /// <returns>解密后的字符串</returns>
        public static string Decrypt(string str, string key)
        {
            Byte[] keyArray = Encoding.UTF8.GetBytes(key);
            Byte[] toEncryptArray = Convert.FromBase64String(str);
            var rijndael = new RijndaelManaged();
            rijndael.Key = keyArray;
            rijndael.Mode = CipherMode.ECB;
            rijndael.Padding = PaddingMode.PKCS7;
            rijndael.IV = System.Text.Encoding.UTF8.GetBytes(Iv);
            ICryptoTransform cTransform = rijndael.CreateDecryptor();
            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return System.Text.Encoding.UTF8.GetString(resultArray);
        }
    }
}