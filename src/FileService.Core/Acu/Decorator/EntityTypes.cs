
using System;
using PX.Data;
using PX.Objects.PM;
using PX.Objects.PO;

namespace FileService.Acu.Decorator
{
    public class EntityTypes : PXStringListAttribute
    {
        public const int Length = 4;
        public const string Project = "PROJ";
        public const string PurchaseOrder = "PO";
        
        public EntityTypes() : base(new []
        {
            Pair(Project, "Project"),
            Pair(PurchaseOrder, "Purchase Order")
        })
        {
            
        }

        public static string GetType(IBqlTable row, bool throwException = false)
        {
            switch (row)
            {
                case PMProject:
                    return Project;
                case POOrder:
                    return PurchaseOrder;
                default:
                    if (throwException)
                        throw new NotImplementedException($"{row.GetType().Name} is not implemented");
                    break;
            }

            return "";
        }
    }
}