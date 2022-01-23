#region #Copyright
//  ----------------------------------------------------------------------------------
//   COPYRIGHT (c) 2022 CONTOU CONSULTING
//   ALL RIGHTS RESERVED
//   AUTHOR: Kyle Vanderstoep
//   CREATED DATE: 2022/01/22
// ----------------------------------------------------------------------------------
#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using FileService.Core;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using File = Google.Apis.Drive.v3.Data.File;


namespace FileService.OneDrive
{
    public class GoogleDriveService : IExternalFileServiceProvider
    {
        public const string rootId = "";
        
        public void UploadFile(string fileName, string path, Stream fileStream)
        {
            var service = GetService();
            var file = GetParentFile(path, service);
            service.Files.Create(new File()
            {
                Name = fileName,
                Parents = new List<string>()
                {
                    file.Id
                }
            }, fileStream, $"application/{fileName.Split('.')[1]}").Upload();

            fileStream.Dispose();
        }

        public string OpenFile(string fileName, string path)
        {
            var service = GetService();
            var parentId = GetParentFile(path, service);

            var listRequest = service.Files.List();
            listRequest.Fields = "files/webViewLink";
            listRequest.Q = $"'{parentId}' in parents and name = '{fileName}'";
            var items = listRequest.Execute().Files.FirstOrDefault() ?? throw new InvalidOperationException("File does not exist");

            return items.WebViewLink;
        }

        public string OpenDirectory(string path)
        {
            var service = GetService();
            var parentId = GetParentFile(path, service);
            return parentId.WebViewLink;
        }

        public Stream GetFile(string path)
        {
            var service = GetService();
            var file = GetParentFile(path, service);
            return service.Files.Get(file.Id).ExecuteAsStream();
        }

        public void CreateDirectory(string path)
        {
            GetParentFile(path, GetService());
        }

        public IEnumerable<DirectoryListing> ListDirectory(string path)
        {
            string[] folders = path.Split('\\');
            var service = GetService();
            File currentFile = GetParentFile(path, service);

            IEnumerable<DirectoryListing> ReturnItems(string dirToAppend = "")
            {
                // Get Files
                var listRequest = service.Files.List();
                listRequest.Q = $"'{currentFile.Id}' in parents and mimeType != 'application/vnd.google-apps.folder'";
                var items = listRequest.Execute().Files;

                foreach (File item in items)
                {
                    yield return new DirectoryListing()
                    {
                        Directory = string.IsNullOrWhiteSpace(dirToAppend) 
                            ? path 
                            : path.TrimEnd('\\') + $"\\{dirToAppend}\\",
                        FileName = item.Name
                    };
                }

                // Get folders
                listRequest.Q = $"'{currentFile.Id}' in parents and mimeType = 'application/vnd.google-apps.folder'";
                items = listRequest.Execute().Files;

                foreach (var folder in items)
                {
                    currentFile = folder;
                    foreach (var item in ReturnItems(folder.Name))
                    {
                        yield return item;
                    }
                }
            }

            return ReturnItems();
        }

        public static DriveService GetService()
        {
            string[] scopes = new string[] { DriveService.Scope.Drive,
                DriveService.Scope.DriveFile};

            var serviceAccountEmail = "kyle-442@hackathon-2022-339109.iam.gserviceaccount.com";

            var credential = GoogleCredential.FromFile(@"C:\Acumatica\Sites\Hackathon2022\Keys\hackathon-2022-339109-a098a77e802b.json")
                .CreateScoped(DriveService.ScopeConstants.Drive);

            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential
            });
            service.HttpClient.Timeout = TimeSpan.FromMinutes(100);
            return service;
        }

        public static File GetParentFile(string path, DriveService service)
        {
            var listRequest = service.Files.List();
            listRequest.Q = "name = 'Hackathon2022'";
            var result = listRequest.Execute().Files.First();
            string lastId = result.Id;
            File lastfile = null;

            foreach (string s in path.Trim('\\').Split('\\'))
            {
                listRequest = service.Files.List();
                listRequest.Fields = "files/webViewLink, id";
                listRequest.Q = $"name = '{s}' and '{lastId}' in parents";
                result = listRequest.Execute().Files.FirstOrDefault();

                if (result is null)
                {
                    File fileMetadata = new File
                    {
                        Name = s,
                        MimeType = "application/vnd.google-apps.folder",
                        Parents = new List<string>()
                        {
                            lastId
                        },
                        
                    };
                    lastfile = service.Files.Create(fileMetadata).Execute();
                    lastId = lastfile.Id;
                }
                else
                {
                    lastfile = result;
                    lastId = result.Id;
                }
            }

            return lastfile;
        }
    }
}