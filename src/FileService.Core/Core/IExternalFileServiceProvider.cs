
using System.Collections.Generic;
using System.IO;

namespace FileService.Core
{
    public interface IExternalFileServiceProvider
    {
        /// <summary>
        /// Takes a stream and saves a file to a given path
        /// </summary>
        /// <param name="path">The path where to save the file</param>
        /// <param name="fileStream">The file's stream of data</param>
        void UploadFile(string fileName, string path, Stream fileStream);

        /// <summary>
        /// Returns a URL where the file can be viewed
        /// </summary>
        /// <param name="path">The location of the file on the external resource</param>
        /// <returns></returns>
        string OpenFile(string path);

        /// <summary>
        /// Gets a files data so it can be downloaded
        /// </summary>
        /// <param name="path">The location of the file on the external resource</param>
        /// <returns></returns>
        Stream GetFile(string path);

        /// <summary>
        /// Creates a directory on the external resource
        /// </summary>
        /// <param name="path"></param>
        void CreateDirectory(string path);

        /// <summary>
        /// List the contents of a directory
        /// </summary>
        /// <param name="path">The location of the directory on the external resource</param>
        /// <returns></returns>
        IEnumerable<DirectoryListing> ListDirectory(string path);
    }
}