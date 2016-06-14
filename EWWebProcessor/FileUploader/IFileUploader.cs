using EWWebProcessor.FileUploader.Model;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace EWWebProcessor.FileUploader
{
    interface IFileUploader
    {
        byte[] UploadFiles(string address, IEnumerable<object> files, NameValueCollection values);
        void CloseStreams(IEnumerable<object> files);
    }
}
