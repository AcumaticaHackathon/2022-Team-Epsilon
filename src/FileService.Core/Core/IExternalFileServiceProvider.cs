
using System.IO;

namespace FileService.Core
{
    public interface IExternalFileServiceProvider
    {
        void UploadFile(string path, Stream fileStream);
        void OpenFile(string path);
        Stream GetFile(string path);
        void CreateDirectory(string path);
    }
}