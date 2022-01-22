#region #Copyright
//  ----------------------------------------------------------------------------------
//   COPYRIGHT (c) 2022 CONTOU CONSULTING
//   ALL RIGHTS RESERVED
//   AUTHOR: Kyle Vanderstoep
//   CREATED DATE: 2022/01/22
// ----------------------------------------------------------------------------------
#endregion

using PX.Data;

namespace FileService.Acu.Decorator
{
    public class EntityTypes : PXStringListAttribute
    {
        public const int Length = 4;
        public const string Project = "PROJ";
        
        public EntityTypes() : base(new []
        {
            Pair(Project, "Project")
        })
        {
            
        }
    }
}