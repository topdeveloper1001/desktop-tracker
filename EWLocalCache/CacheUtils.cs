using Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace EWLocalCache
{
    public class CacheUtils
    {
        public static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }

        public static byte[] ImageToByte(Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }

        public static Image ByteToImage(byte[] bytes)
        {
            MemoryStream ms = new MemoryStream(bytes);
            return Image.FromStream(ms);
        }

        public static string GetFullPathToSession(string sessionId)
        {
            return string.Format("{0}\\{1}", CommonConst.LOCAL_CACHE_PATH, sessionId);
        }

        public static string GetFullPathToFile(string fileName, string sessionId)
        {
            return string.Format("{0}\\{1}\\{2}", CommonConst.LOCAL_CACHE_PATH, sessionId, fileName);
        }
    }
}
