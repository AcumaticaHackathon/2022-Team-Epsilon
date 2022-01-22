
using System;
using FileService.Core;
using PX.Data;
using PX.Data.BQL;
using PX.SM;

namespace FileService.Acu.DAC
{
    [Serializable]
    [PXCacheName("FileServicePreferences")]
    public class FileServicePreferences : IBqlTable
    {
        #region PluginType
        [PXDBString(516)]
        [PXProviderTypeSelector(typeof(IExternalFileServiceProvider))]
        [PXUIField(DisplayName = "Plugin Type")]
        public virtual string PluginType { get; set; }
        public abstract class pluginType : BqlString.Field<pluginType> { }
        #endregion

    }
}