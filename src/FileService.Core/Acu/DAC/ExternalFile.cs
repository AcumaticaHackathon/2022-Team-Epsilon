#region #Copyright
//  ----------------------------------------------------------------------------------
//   COPYRIGHT (c) 2022 CONTOU CONSULTING
//   ALL RIGHTS RESERVED
//   AUTHOR: Kyle Vanderstoep
//   CREATED DATE: 2022/01/22
// ----------------------------------------------------------------------------------
#endregion

using System;
using PX.Data;
using PX.Data.BQL;

namespace FileService.Acu.DAC
{
    [Serializable]
    [PXCacheName("External File")]
    public class ExternalFile : IBqlTable
    {
        #region RefNoteId
        [PXDBGuid()]
        public virtual Guid? RefNoteId { get; set; }
        public abstract class refNoteId : BqlGuid.Field<refNoteId> { }
        #endregion

        #region FileName
        [PXDBString(256, IsUnicode = true)]
        [PXUIField(DisplayName = "File Name")]
        public virtual string FileName { get; set; }
        public abstract class fileName : BqlString.Field<fileName> { }
        #endregion

        #region Path
        [PXDBString(256, IsUnicode = true)]
        [PXUIField(DisplayName = "DisplayName")]
        public virtual string Path { get; set; }
        public abstract class path : BqlString.Field<path> { }
        #endregion

    }
}