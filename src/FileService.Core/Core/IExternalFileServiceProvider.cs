#region #Copyright
//  ----------------------------------------------------------------------------------
//   COPYRIGHT (c) 2022 CONTOU CONSULTING
//   ALL RIGHTS RESERVED
//   AUTHOR: Kyle Vanderstoep
//   CREATED DATE: 2022/01/22
// ----------------------------------------------------------------------------------
#endregion

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