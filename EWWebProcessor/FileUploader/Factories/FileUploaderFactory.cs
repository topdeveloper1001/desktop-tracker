using EWWebProcessor.FileUploader.Impl;
using System;
using System.Collections.Generic;
using System.Text;

namespace EWWebProcessor.FileUploader.Factories
{
    class FileUploaderFactory
    {
        public static IFileUploader CreateFileUploader()
        {
            return new DefaultFileUploader(); // SIMPLEST VARIANT NOW
        }
    }
}
