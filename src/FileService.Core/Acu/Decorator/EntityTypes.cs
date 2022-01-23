
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
        public const string PurchaseOrderChangeOrders = "POC";
        public const string PurchaseOrderDetails = "POD";
        
        public EntityTypes() : base(new []
        {
            Pair(Project, "Project"),
            Pair(PurchaseOrder, "Purchase Order"),
            Pair(PurchaseOrderDetails, "Purchase Order -> Details"),
            Pair(PurchaseOrderChangeOrders, "Purchase Order -> Change Orders"),
        })
        {
            
        }

        public static string GetType(IBqlTable row, IBqlTable detailRow = null, bool throwException = false)
        {
            switch (row)
            {
                case PMProject:
                    return Project;
                case POOrder:
                    return PurchaseOrder;
                case PMChangeOrder:
                    return PurchaseOrderChangeOrders;
                case POLine:
                    return PurchaseOrderDetails;
                default:
                    if (throwException)
                        throw new NotImplementedException($"{row.GetType().Name} is not implemented");
                    break;
            }

            return "";
        }
    }
}