

using System;
using FileService.Acu.Decorator;
using PX.Data;
using PX.Data.BQL;

namespace FileService.Acu.DAC
{
    [Serializable]
    [PXCacheName("File Entity Mapping")]
    public class FileEntityMapping : IBqlTable
    {
        #region Active
        [PXDBBool]
        [PXUIField(DisplayName = "Active")]
        public virtual bool? Active { get; set; }
        public abstract class active : BqlBool.Field<active> { }
        #endregion

        #region Entity
        [PXDBString(EntityTypes.Length, IsKey = true)]
        [EntityTypes]
        [PXUIField(DisplayName = "Entity")]
        public virtual string Entity { get; set; }
        public abstract class entity : BqlString.Field<entity> { }
        #endregion

        #region Mapping
        [PXDBString(512,IsUnicode = true)]
        [PXUIField(DisplayName = "Mapping")]
        public virtual string Mapping { get; set; }
        public abstract class mapping : BqlString.Field<mapping> { }
        #endregion

    }
}