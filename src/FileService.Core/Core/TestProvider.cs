
using System.Collections.Generic;
using System.IO;

namespace FileService.Core
{
    public class TestProvider : IExternalFileServiceProvider
    {
        public void UploadFile(string fileName, string path, Stream fileStream)
        {
            throw new System.NotImplementedException();
        }

        public string OpenFile(string fileName, string path)
        {
            throw new System.NotImplementedException();
        }

        public string OpenDirectory(string path)
        {
            throw new System.NotImplementedException();
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
                    Directory = @"\test\othertest",
                    FileName = "test3.txt",
                }
            };
        }
    }

    public class OneDriveProvider : IExternalFileServiceProvider
    {
        public void UploadFile(string fileName, string path, Stream fileStream)
        {
            throw new System.NotImplementedException();
        }

        public string OpenFile(string fileName, string path)
        {
            throw new System.NotImplementedException();
        }

        public string OpenDirectory(string path)
        {
            throw new System.NotImplementedException();
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
                    Directory = @"\test\othertest",
                    FileName = "test3.txt",
                }
            };
        }
    }

    public class DropboxProvider : IExternalFileServiceProvider
    {
        public void UploadFile(string fileName, string path, Stream fileStream)
        {
            throw new System.NotImplementedException();
        }

        public string OpenFile(string fileName, string path)
        {
            throw new System.NotImplementedException();
        }

        public string OpenDirectory(string path)
        {
            throw new System.NotImplementedException();
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
                    Directory = @"\test\othertest",
                    FileName = "test3.txt",
                }
            };
        }
    }
}