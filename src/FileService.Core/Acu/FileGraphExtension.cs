
using System;
using FileService.Acu.DAC;
using PX.Data;
using PX.Data.BQL;

namespace FileService.Acu
{
    public class FileGraphExtension<T, U, V> : PXGraphExtension<T>
    where T : PXGraph
    where U : class, IBqlTable, new()
    where V : BqlGuid.Field<V>
    {
        public PXSelect<ExternalFile, Where<ExternalFile.refNoteId, Equal<Current<V>>>> ExternalFiles;
        public PXSetup<FileServicePreferences> FileServicePreferences;

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
        }

        public PXAction<U> ActionDownloadFile;
        [PXUIField(DisplayName = "Download")]
        [PXButton(CommitChanges = true)]
        public void actionDownloadFile()
        {
            var file = ExternalFiles.Current;
        }

        public PXAction<U> ActionUploadFile;
        [PXUIField(DisplayName = "Upload")]
        [PXButton(CommitChanges = true)]
        public void actionUploadFile()
        {
            
        }

        protected void _(Events.RowInserting<ExternalFile> e)
        {
            if (e.Row is null) return;
            e.Row.RefNoteId = Base.Caches<U>().GetValue<V>(Base.Caches<U>().Current) as Guid?;
        }
    }
}