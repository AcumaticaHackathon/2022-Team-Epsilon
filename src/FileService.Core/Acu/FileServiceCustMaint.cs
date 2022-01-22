#region #Copyright
//  ----------------------------------------------------------------------------------
//   COPYRIGHT (c) 2022 CONTOU CONSULTING
//   ALL RIGHTS RESERVED
//   AUTHOR: Kyle Vanderstoep
//   CREATED DATE: 2022/01/22
// ----------------------------------------------------------------------------------
#endregion

using System.Linq;
using Customization;
using FileService.Acu.DAC;
using FileService.Acu.Decorator;
using PX.Data;

namespace FileService.Acu
{
    public class FileServiceCustMaint : CustomizationPlugin
    {
        public override void UpdateDatabase()
        {
            WriteLog("---- ADDING EXTERNAL FILE PROVIDER ENTITIES ----");
            var graph = PXGraph.CreateInstance<FileServicesPreferencesMaint>();
            var existing = graph.Mappings.SelectMain();

            foreach (string value in new EntityTypes().GetAllowedValues(graph.Mappings.Cache))
            {
                if (existing.All(e => e.Entity != value))
                {
                    graph.Mappings.Insert(new FileEntityMapping()
                    {
                        Active = false,
                        Entity = value
                    });
                }
            }

            graph.Persist();
        }
    }
}