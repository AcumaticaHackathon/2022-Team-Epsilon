#region #Copyright
//  ----------------------------------------------------------------------------------
//   COPYRIGHT (c) 2022 CONTOU CONSULTING
//   ALL RIGHTS RESERVED
//   AUTHOR: Kyle Vanderstoep
//   CREATED DATE: 2022/01/22
// ----------------------------------------------------------------------------------
#endregion

using System.Collections.Generic;
using System.IO;

namespace FileService.Core
{
    public class TestProvider : IExternalFileServiceProvider
    {
        public void UploadFile(string path, Stream fileStream)
        {
            
        }

        public string OpenFile(string path)
        {
            return "https://www.google.com";
        }

        public Stream GetFile(string path)
        {
            return new MemoryStream();
        }

        public void CreateDirectory(string path)
        {
            
        }

        public IEnumerable<DirectoryListing> ListDirectory(string path)
        {
            return new[]
            {
                new DirectoryListing()
                {
                    Directory = @"\test\",
                    FileName = "test.txt",
                },
                new DirectoryListing()
                {
                    Directory = @"\test\",
                    FileName = "test2.txt",
                },
                new DirectoryListing()
                {
                    Directory = @"\test\",
                    FileName = "test3.txt",
                }
            };
        }
    }
}