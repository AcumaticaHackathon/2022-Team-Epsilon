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
using PX.Objects.PM;

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

        public static string GetType(IBqlTable row, bool throwException = false)
        {
            switch (row)
            {
                case PMProject:
                    return Project;
                default:
                    if (throwException)
                        throw new NotImplementedException($"{row.GetType().Name} is not implemented");
                    break;
            }

            return "";
        }
    }
}