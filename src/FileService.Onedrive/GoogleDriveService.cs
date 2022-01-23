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
            var id = GetParentId(path, service);
            service.Files.Create(new File()
            {
                Name = fileName,
                Parents = new List<string>()
                {
                    id
                }
            }, fileStream, $"application/{fileName.Split('.')[1]}").Upload();

            fileStream.Dispose();
        }

        public string OpenFile(string path)
        {
            throw new System.NotImplementedException();
        }

        public Stream GetFile(string path)
        {
            var service = GetService();
            var id = GetParentId(path, service);
            return service.Files.Get(id).ExecuteAsStream();
        }

        public void CreateDirectory(string path)
        {
            GetParentId(path, GetService());
        }

        public IEnumerable<DirectoryListing> ListDirectory(string path)
        {
            string[] folders = path.Split('\\');
            var service = GetService();
            string currentId = GetParentId(path, service);

            IEnumerable<DirectoryListing> ReturnItems()
            {
                // Get Files
                var listRequest = service.Files.List();
                listRequest.Q = $"'{currentId}' in parents and mimeType != 'application/vnd.google-apps.folder'";
                var items = listRequest.Execute().Files;

                foreach (File item in items)
                {
                    yield return new DirectoryListing()
                    {
                        Directory = path,
                        FileName = item.Name
                    };
                }

                // Get folders
                listRequest.Q = $"'{currentId}' in parents and mimeType = 'application/vnd.google-apps.folder'";
                items = listRequest.Execute().Files;

                foreach (var folder in items)
                {
                    currentId = folder.Id;
                    foreach (var item in ReturnItems())
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

        public static string GetParentId(string path, DriveService service)
        {
            var listRequest = service.Files.List();
            listRequest.Q = "name = 'Hackathon2022'";
            var result = listRequest.Execute().Files.First();
            string lastId = result.Id;

            foreach (string s in path.Trim('\\').Split('\\'))
            {
                listRequest = service.Files.List();
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
                    lastId = service.Files.Create(fileMetadata).Execute().Id;
                }
                else
                {
                    lastId = result.Id;
                }
            }

            return lastId;
        }
    }
}