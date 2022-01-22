﻿
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

namespace FileService.Acu
{
    public class FileGraphExtension<T, U, V> : PXGraphExtension<T>
    where T : PXGraph
    where U : class, IBqlTable, new()
    where V : BqlGuid.Field<V>
    {
        private const string UploadFilesSessionKey = "LekkerExternalFileProvider";

        public PXSelect<ExternalFile, Where<ExternalFile.refNoteId, Equal<Current<V>>>> ExternalFiles;
        public PXSetup<FileServicePreferences> FileServicePreferences;

        [InjectDependency]
        public PathBuilder PathBuilder { get; set; }

        protected void _(Events.RowSelected<U> e)
        {
            if (e.Row is null) return;
            string entityType = EntityTypes.GetType(e.Row);
            bool enabled = Base.Select<FileEntityMapping>().Any(m => m.Entity == entityType && m.Active == true);

            ActionOpenFiles.SetVisible(enabled);
        }

        protected void _(Events.RowInserting<ExternalFile> e)
        {
            if (e.Row is null) return;
            e.Row.RefNoteId = Base.Caches<U>().GetValue<V>(Base.Caches<U>().Current) as Guid?;
        }

        public PXAction<U> ActionOpenFiles;
        [PXUIField(DisplayName = "Attachments")]
        [PXButton(CommitChanges = true)]
        public void actionOpenFilesWindow()
        {
            ExternalFiles.AskExt(true);
        }

        public PXAction<U> ActionRedirectToFile;
        [PXUIField(DisplayName = "View File")]
        [PXButton(CommitChanges = true)]
        public void actionRedirectToFile()
        {
            var file = ExternalFiles.Current;
            IExternalFileServiceProvider provider = FileServicePreferences.Current.GetProvider();
            string url = provider.OpenFile(file.Path);

            throw new PXRedirectToUrlException(url, "");
        }

        public PXAction<U> ActionDownloadFile;
        [PXUIField(DisplayName = "Download")]
        [PXButton(CommitChanges = true)]
        public void actionDownloadFile()
        {
            var fileRecord = ExternalFiles.Current;
            IExternalFileServiceProvider provider = FileServicePreferences.Current.GetProvider();
            var fileStream = provider.GetFile(fileRecord.Path);
            using var memoryStream = new MemoryStream();
            fileStream.CopyTo(memoryStream);

            throw new PXRedirectToFileException(
                new PX.SM.FileInfo(fileRecord.FileName, default, memoryStream.ToArray()), true);
        }

        public PXAction<U> ActionUploadFile;
        [PXUIField(DisplayName = "Upload")]
        [PXButton(CommitChanges = true)]
        public void actionUploadFile()
        {
            if (FileServicePreferences.AskExt() != WebDialogResult.OK) return;

            PX.SM.FileInfo info = PXContext.SessionTyped<PXSessionStatePXData>().FileInfo[UploadFilesSessionKey] as PX.SM.FileInfo;
            MemoryStream stream = new MemoryStream(info.BinData);


            IExternalFileServiceProvider provider = FileServicePreferences.Current.GetProvider();
            string path = PathBuilder.GetPath(GetEntityType(), Base);
            provider.UploadFile(path, stream);

            ExternalFiles.Insert(new ExternalFile()
            {
                Path = path,
                FileName = info.FullName
            });

            //Removing file from session to save memory
            System.Web.HttpContext.Current.Session.Remove(UploadFilesSessionKey);

            Base.Actions.PressSave();
        }

        private string GetEntityType()
        {
            var row = Base.Caches<U>().Current as IBqlTable;
            return EntityTypes.GetType(row);
        }
    }
}