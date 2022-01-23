
using System;
using System.IO;
using System.Linq;
using FileService.Acu.DAC;
using FileService.Acu.Decorator;
using FileService.Acu.Extensions;
using FileService.Acu.Helper;
using FileService.Core;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.WorkflowAPI;
using PX.SM;
using PX.Web.UI;

namespace FileService.Acu
{
    public class FileGraphExtension2<T, U, V> : PXGraphExtension<T>
    where T : PXGraph
    where U : class, IBqlTable, new()
    where V : BqlGuid.Field<V>
    {
        private const string UploadFilesSessionKey = "LekkerExternalFileProvider";

        public PXSelect<ExternalFile, Where<ExternalFile.refNoteId, Equal<Current<V>>>> ExternalFiles2;
        public PXSetup<FileServicePreferences> FileServicePreferences;

        [InjectDependency]
        public PathBuilder PathBuilder { get; set; }

        public override void Initialize()
        {
            ExternalFiles2.AllowUpdate = false;
        }

        protected void _(Events.RowSelected<U> e)
        {
            if (e.Row is null) return;
            string entityType = EntityTypes.GetType(e.Row);
            bool enabled = Base.Select<FileEntityMapping>().Any(m => m.Entity == entityType && m.Active == true);

            // ActionOpenFiles.SetVisible(enabled);
        }

        protected void _(Events.RowInserting<ExternalFile> e)
        {
            if (e.Row is null) return;
            e.Row.RefNoteId = Base.Caches<U>().GetValue<V>(Base.Caches<U>().Current) as Guid?;
        }

        public PXAction<U> ActionOpenFilesWindow2;
        [PXButton(CommitChanges = true), PXUIField(DisplayName = "Attachments")]
        public void actionOpenFilesWindow2()
        {
            ActionRefreshFileList2.Press();

            if (ExternalFiles2.AskExt(true) != WebDialogResult.OK) return;

            Base.Actions.PressSave();
        }

        public PXAction<U> ActionRedirectToFile2;
        [PXButton(CommitChanges = true), PXUIField(DisplayName = "View File")]
        public void actionRedirectToFile2()
        {
            var file = ExternalFiles2.Current;
            IExternalFileServiceProvider provider = FileServicePreferences.Current.GetProvider();
            string url = provider.OpenFile(file.FileName, file.Path);

            throw new PXRedirectToUrlException(url, "");
        }

        public PXAction<U> ActionRedirectToDirectory2;
        [PXButton(CommitChanges = true), PXUIField(DisplayName = "View Directory")]
        public void actionRedirectToDirectory2()
        {
            var file = ExternalFiles2.Current;
            IExternalFileServiceProvider provider = FileServicePreferences.Current.GetProvider();
            string url = provider.OpenFile(file.FileName, file.Path);

            throw new PXRedirectToUrlException(url, "");
        }

        public PXAction<U> ActionDownloadFile2;
        [PXButton(CommitChanges = true), PXUIField(DisplayName = "Download File")]
        public void actionDownloadFile2()
        {
            var fileRecord = ExternalFiles2.Current;
            IExternalFileServiceProvider provider = FileServicePreferences.Current.GetProvider();
            var fileStream = provider.GetFile(fileRecord.Path);
            using var memoryStream = new MemoryStream();
            fileStream.CopyTo(memoryStream);

            throw new PXRedirectToFileException(
                new PX.SM.FileInfo(fileRecord.FileName, default, memoryStream.ToArray()), true);
        }

        public PXAction<U> ActionUploadFile2;
        [PXButton(CommitChanges = true, ImageKey = Sprite.Main.AddNew), PXUIField(DisplayName = "Upload")]
        public void actionUploadFile2()
        {
            if (FileServicePreferences.AskExt() != WebDialogResult.OK) return;

            PX.SM.FileInfo info = PXContext.SessionTyped<PXSessionStatePXData>().FileInfo[UploadFilesSessionKey] as PX.SM.FileInfo;
            MemoryStream stream = new MemoryStream(info.BinData);

            IExternalFileServiceProvider provider = FileServicePreferences.Current.GetProvider();
            string path = PathBuilder.GetPath(GetEntityType(), Base);
            provider.UploadFile(info.FullName, path, stream);

            ExternalFiles2.Insert(new ExternalFile()
            {
                Path = path,
                FileName = info.FullName,
                Entity = GetEntityType()
            });

            //Removing file from session to save memory
            System.Web.HttpContext.Current.Session.Remove(UploadFilesSessionKey);
        }

        public PXAction<U> ActionRefreshFileList2;
        [PXButton(CommitChanges = true, ImageKey = Sprite.Main.Refresh), PXUIField(DisplayName = "Refresh")]
        public void actionRefreshFileList2()
        {
            IExternalFileServiceProvider provider = FileServicePreferences.Current.GetProvider();
            string path = PathBuilder.GetPath(GetEntityType(), Base);

            var acuFiles = ExternalFiles2.SelectMain().ToList();

            foreach (DirectoryListing listing in provider.ListDirectory(path))
            {
                var match = acuFiles.FirstOrDefault(a => a.FileName == listing.FileName && a.Path == listing.Directory);
                if (match is null)
                {
                    ExternalFiles2.Insert(new ExternalFile()
                    {
                        FileName = listing.FileName,
                        Path = listing.Directory
                    });
                }
                else
                {
                    acuFiles.Remove(match);
                }
            }

            foreach (ExternalFile file in acuFiles)
            {
                ExternalFiles2.Delete(file);
            }
        }

        private string GetEntityType()
        {
            var row = Base.Caches<U>().Current as IBqlTable;
            return EntityTypes.GetType(row);
        }
    }
}