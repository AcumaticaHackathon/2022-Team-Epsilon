
using System;
using FileService.Acu.Decorator;
using PX.Data;
using PX.Data.BQL;

namespace FileService.Acu.DAC
{
    [Serializable]
    [PXCacheName("External File")]
    public class ExternalFile : IBqlTable
    {
        #region FileId
        [PXDBLongIdentity(IsKey = true)]
        public virtual long? FileId { get; set; }
        public abstract class fileId : BqlLong.Field<fileId> { }
        #endregion

        #region RefNoteId
        [PXDBGuid()]
        public virtual Guid? RefNoteId { get; set; }
        public abstract class refNoteId : BqlGuid.Field<refNoteId> { }
        #endregion

        #region Entity
        [PXDBString(EntityTypes.Length)]
        [EntityTypes]
        [PXUIField(DisplayName = "Entity")]
        public virtual string Entity { get; set; }
        public abstract class entity : BqlString.Field<entity> { }
        #endregion

        #region FileName
        [PXDBString(256, IsUnicode = true)]
        [PXUIField(DisplayName = "File Name")]
        public virtual string FileName { get; set; }
        public abstract class fileName : BqlString.Field<fileName> { }
        #endregion

        #region Path
        [PXDBString(256, IsUnicode = true)]
        [PXUIField(DisplayName = "Path")]
        public virtual string Path { get; set; }
        public abstract class path : BqlString.Field<path> { }
        #endregion

    }
}