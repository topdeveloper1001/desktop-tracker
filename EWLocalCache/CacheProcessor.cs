using Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace EWLocalCache
{
    public class CacheProcessor
    {
        public static bool SaveImage(Image image, string imageName, string sessionId, bool encrypt)
        {
            if (!Directory.Exists(CacheUtils.GetFullPathToSession(sessionId)))
                Directory.CreateDirectory(CacheUtils.GetFullPathToSession(sessionId));
            if (encrypt)
            {
                byte[] encrypted = CacheEncrypter.EncryptData(CacheUtils.ImageToByte(image), CommonConst.DES_KEY);
                File.WriteAllBytes(string.Format("{0}{1}",
                    CacheUtils.GetFullPathToFile(imageName, sessionId), CommonConst.ENCR_IMG_EXT), encrypted);
            }
            else
            {
                image.Save(string.Format("{0}{1}",
                    CacheUtils.GetFullPathToFile(imageName, sessionId), CommonConst.DECR_IMG_EXT));
            }
            return true;
        }

        public static string[] GetEncryptedImagePaths(string sessionId)
        {
            if (!Directory.Exists(string.Format("{0}\\{1}", CommonConst.LOCAL_CACHE_PATH, sessionId)))
                return new string[0];

            return Directory.GetFiles(string.Format("{0}\\{1}", CommonConst.LOCAL_CACHE_PATH, sessionId),
               string.Format("*{0}", CommonConst.ENCR_IMG_EXT));
        }

        public static string[] GetDecryptedImagePaths(string sessionId)
        {
            if (!Directory.Exists(string.Format("{0}\\{1}", CommonConst.LOCAL_CACHE_PATH, sessionId)))
                return new string[0];

            return Directory.GetFiles(string.Format("{0}\\{1}", CommonConst.LOCAL_CACHE_PATH, sessionId),
               string.Format("*{0}", CommonConst.DECR_IMG_EXT));
        }

        public static bool CreateEncryptedImages(string sessionId)
        {
            try
            {
                string[] filePaths = GetDecryptedImagePaths(sessionId);
                foreach (string path in filePaths)
                {
                    byte[] fileBytes = File.ReadAllBytes(path);
                    byte[] encrypted = CacheEncrypter.EncryptData(fileBytes, CommonConst.DES_KEY);
                    File.WriteAllBytes(string.Format("{0}\\{1}\\{2}", CommonConst.LOCAL_CACHE_PATH,
                        sessionId, Path.GetFileName(path).Replace(CommonConst.DECR_IMG_EXT, CommonConst.ENCR_IMG_EXT)), encrypted);
                }
            }
            catch { return false; }
            return true;
        }

        public static bool CreateDecryptedImages(string sessionId)
        {
            string[] filePaths = GetEncryptedImagePaths(sessionId);
            foreach (string path in filePaths)
            {
                try
                {
                    byte[] fileBytes = File.ReadAllBytes(path);
                    byte[] decrypted = CacheEncrypter.DecryptData(fileBytes, CommonConst.DES_KEY);
                    CacheUtils.ByteToImage(decrypted).Save(string.Format("{0}\\{1}\\{2}", CommonConst.LOCAL_CACHE_PATH,
                        sessionId, Path.GetFileName(path).Replace(CommonConst.ENCR_IMG_EXT, CommonConst.DECR_IMG_EXT)));
                }
                catch { }
            }
            return true;
        }

        public static bool DeleteDecryptedImages(string sessionId)
        {
            return DeleteImages(sessionId, CommonConst.DECR_IMG_EXT);
        }

        public static bool DeleteEncryptedImages(string sessionId)
        {
            return DeleteImages(sessionId, CommonConst.ENCR_IMG_EXT);
        }

        public static bool DeleteAllImages(string sessionId)
        {
            return DeleteImages(sessionId, CommonConst.DECR_IMG_EXT) &&
                DeleteImages(sessionId, CommonConst.ENCR_IMG_EXT);
        }

        public static IList<string> GetCachedSessionFolders()
        {
            if (!Directory.Exists(CommonConst.LOCAL_CACHE_PATH))
                Directory.CreateDirectory(CommonConst.LOCAL_CACHE_PATH);
            return new List<string>(Directory.GetDirectories(CommonConst.LOCAL_CACHE_PATH));
        }

        private static bool DeleteImages(string sessionId, string ext)
        {
            try
            {
                string[] filePaths = Directory.GetFiles(string.Format("{0}\\{1}",
                    CommonConst.LOCAL_CACHE_PATH, sessionId), string.Format("*{0}", ext));

                foreach (string path in filePaths)
                    File.Delete(path);
            }
            catch { }
            return true;
        }

        public static List<string> LoadData(string sessionId)
        {
            if (!File.Exists(GetDataDecryptedFilePath(sessionId)))
                return new List<string>();

            string[] loadedData = File.ReadAllLines(GetDataDecryptedFilePath(sessionId));
            return new List<string>(loadedData);
        }

        public static ClearCacheState ClearCache()
        {
            string[] cachedDirs = null;
            try
            {
                cachedDirs = Directory.GetDirectories(CommonConst.LOCAL_CACHE_PATH);
            }
            catch { return ClearCacheState.ERROR; }
            if (cachedDirs == null || cachedDirs.Length <= 0)
                return ClearCacheState.EMPTY_CACHE;

            foreach (string cachedDir in cachedDirs)
            {
                try
                {
                    DirectoryInfo di = new DirectoryInfo(cachedDir);
                    foreach (FileInfo file in di.GetFiles())
                    { file.Delete(); }
                    foreach (DirectoryInfo dir in di.GetDirectories())
                    { dir.Delete(true); }
                    di.Delete();

                }
                catch { }
            }
            return ClearCacheState.SUCCESS;
        }

        public static bool SaveData(IList<string> data, string sessionId, bool encrypt)
        {
            StringBuilder finalData = new StringBuilder();
            foreach (string line in data)
            {
                finalData.Append(line);
                finalData.Append(Environment.NewLine);
            }

            if (encrypt)
            {
                byte[] encrypted = CacheEncrypter.EncryptData(CacheUtils.GetBytes(finalData.ToString()), CommonConst.DES_KEY);
                File.WriteAllBytes(GetDataEncryptedFilePath(sessionId), encrypted);
            }
            else
                File.WriteAllText(GetDataDecryptedFilePath(sessionId), finalData.ToString());

            return true;
        }

        public static bool EncryptData(string sessionId)
        {
            if (!File.Exists(GetDataDecryptedFilePath(sessionId)))
                return true;

            string data = File.ReadAllText(GetDataDecryptedFilePath(sessionId));
            byte[] encrypted = CacheEncrypter.EncryptData(CacheUtils.GetBytes(data), CommonConst.DES_KEY);
            File.WriteAllBytes(GetDataEncryptedFilePath(sessionId), encrypted);
            return true;
        }

        public static bool DecryptData(string sessionId)
        {
            if (!File.Exists(GetDataEncryptedFilePath(sessionId)))
                return true;

            try
            {
                byte[] encrypted = File.ReadAllBytes(GetDataEncryptedFilePath(sessionId));
                byte[] decrypted = CacheEncrypter.DecryptData(encrypted, CommonConst.DES_KEY);
                File.WriteAllText(GetDataDecryptedFilePath(sessionId), CacheUtils.GetString(decrypted));
            } catch { }

            return true;
        }

        public static bool DeleteEncryptedData(string sessionId)
        {
            if (File.Exists(GetDataEncryptedFilePath(sessionId)))
                File.Delete(GetDataEncryptedFilePath(sessionId));
            return true;
        }

        public static bool DeleteDecryptedData(string sessionId)
        {
            if (File.Exists(GetDataDecryptedFilePath(sessionId)))
                File.Delete(GetDataDecryptedFilePath(sessionId));
            return true;
        }

        public static void DeleteAllSessions()
        {
            IList<string> dirs = GetCachedSessionFolders();
            foreach (string dir in dirs)
                Directory.Delete(dir, true);
        }

        public static void DeleteSessionFolder(string sessionId)
        {
            IList<string> dirs = GetCachedSessionFolders();
            foreach (string dir in dirs)
            {
                try
                {
                    if (dir.Contains(sessionId))
                        Directory.Delete(dir, true);
                } catch { }
            }
        }

        public static string GetDataEncryptedFilePath(string sessionId)
        {
            string filePath = string.Format("{0}\\{1}\\{2}{3}", CommonConst.LOCAL_CACHE_PATH,
                    sessionId, CommonConst.SESSION_DATA_FILE, CommonConst.ENCR_DATA_EXT);

            CreateSessionDirectory(sessionId);
            
            return filePath;
        }

        public static string GetDataDecryptedFilePath(string sessionId)
        {
            string filePath = string.Format("{0}\\{1}\\{2}{3}", CommonConst.LOCAL_CACHE_PATH,
                    sessionId, CommonConst.SESSION_DATA_FILE, CommonConst.DECR_DATA_EXT);

            CreateSessionDirectory(sessionId);
            
            return filePath; 
        }

        private static void CreateSessionDirectory(string sessionId)
        {
            string dirPath = string.Format("{0}\\{1}",
                CommonConst.LOCAL_CACHE_PATH, sessionId);

            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);
        }
    }
}
