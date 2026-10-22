using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BSLDaman.Models
{
    public class clsPostAssembly
    {
        public Int64 LayID { get; set; }
        public Int64 BundleID { get; set; }
        public string BundleIDs { get; set; }
        public int BundleNo { get; set; }
        public string SizeName { get; set; }
        public string Freq { get; set; }
        public string ColorName { get; set; }
        public string ShadeName { get; set; }
        public int Qty { get; set; }
        public int PlyFrom { get; set; }
        public int PlyTo { get; set; }
        public string Ply { get; set; }
        public int LotNo { get; set; }
        public int BundleQty { get; set; }
        public int CompileQty { get; set; }
        public bool IsDispatch { get; set; }
        public string StyleCode { get; set; }
        public string OrderNo { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
        public Int32 SupervisorID { get; set; }
        public string SupervisorAssignedDate { get; set; }
        public string SupervisorName { get; set; }
        public Int32 AppEmpID { get; set; }
        public string AppEmpName { get; set; }
        public string AppEmpIDs { get; set; }
        public string AppStartTime { get; set; }
        public string AppEndTime { get; set; }
        public string BundleIDStatus { get; set; }
        public string TotalBundleIdCount { get; set; }
        public string LineName { get; set; }
        public Int64 LineId { get; set; }
        public string LineStatus { get; set; }
        public Int64 OperationNo { get; set; }
        public string OperationNos { get; set; }
        public string UpdateType { get; set; }
        public decimal StdMin { get; set; }
        public decimal StdRate { get; set; }
        public decimal TodaysEarning { get; set; }
        public decimal MonthlyEarning { get; set; }
        public string Months { get; set; }
        public decimal TotalAmount { get; set; }
        public string CurrentDate { get; set; }
        public string TimePeriod { get; set; }
        public string OperationName { get; set; }
        public string SubProduct { get; set; }
        public Int32 PageNumber { get; set; }
        public Int32 PageSize { get; set; }
        public string SortBy { get; set; }
        public string SortDirection { get; set; }
        public string SubSection { get; set; }
        public string vErrorMsg { get; set; }
        public int vErrorCode { get; set; }
    }


    public class clsPostAssemblyOrderList
    {
        public string OrderNo { get; set; }
        public string Product { get; set; }
        public Int64 Qty { get; set; }
        public string vErrorMsg { get; set; }
        public int vErrorCode { get; set; }
    }


    public class clsPostAssemblyLines
    {
        public Int64 LineId { get; set; }
        public Int32 SeqNo { get; set; }
        public string LineName { get; set; }
        public string LineCode { get; set; }
        public string LineSupervisorName { get; set; }
        public string SectionName { get; set; }
        public string LineStatus { get; set; }
        public string vErrorMsg { get; set; }
        public int vErrorCode { get; set; }
    }

}