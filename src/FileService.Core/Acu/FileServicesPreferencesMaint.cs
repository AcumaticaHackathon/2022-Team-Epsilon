#region #Copyright
//  ----------------------------------------------------------------------------------
//   COPYRIGHT (c) 2022 CONTOU CONSULTING
//   ALL RIGHTS RESERVED
//   AUTHOR: Kyle Vanderstoep
//   CREATED DATE: 2022/01/22
// ----------------------------------------------------------------------------------
#endregion


using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FileService.Acu.DAC;
using PX.Data;


namespace FileService.Acu
{
    public class FileServicesPreferencesMaint : PXGraph<FileServicesPreferencesMaint>
    {
        public PXCancel<FileServicePreferences> Cancel;
        public PXSave<FileServicePreferences> Save;

        public PXSelect<FileServicePreferences> Preferences;
        public PXSelect<FileEntityMapping> Mappings;

        public FileServicesPreferencesMaint()
        {
            Mappings.AllowInsert = false;
            Mappings.AllowDelete = false;
        }
    }
}