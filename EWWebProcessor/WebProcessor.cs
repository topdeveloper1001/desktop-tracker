using EWWebProcessor.FileUploader;
using EWWebProcessor.FileUploader.Factories;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Text;

namespace EWWebProcessor
{
    public class WebProcessor
    {
        public static string MakeGetRequest(string url, Dictionary<string, string> arguments)
        {
            string text = string.Empty;
            string fullUrl = string.Format("{0}?", url);

            foreach (KeyValuePair<string, string> entry in arguments)
                fullUrl = string.Format("{0}&{1}={2}", fullUrl, 
                    Uri.EscapeDataString(entry.Key), Uri.EscapeDataString(entry.Value));

            return MakeGetRequest(fullUrl);
        }

        public static string MakeGetRequest(string url)
        {
            string text = string.Empty;

            using (WebClient client = new WebClient())
            {
                try
                {
                    text = client.DownloadString(url);
                }
                catch (WebException ex)
                {
                    text = "Connection Error";
                }
            }

            return text;
        }
    
        public static string UploadFileWithParams(string address, IEnumerable<object> files, NameValueCollection values)
        {
            IFileUploader uploader = FileUploaderFactory.CreateFileUploader();
            byte[] response = uploader.UploadFiles(address, files, values);
            uploader.CloseStreams(files);
            return Encoding.Default.GetString(response);
        }

        public static string MakePostRequest(string address, NameValueCollection values)
        {
            IFileUploader uploader = FileUploaderFactory.CreateFileUploader();
            byte[] response = uploader.UploadFiles(address, new List<object>(), values);
            return Encoding.Default.GetString(response);
        }

        public static bool CheckInternetConnection()
        {
            try
            {
                using (var client = new TimeoutWebClient())
                {
                    using (var stream = client.OpenRead("http://www.google.com"))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

    }
}
