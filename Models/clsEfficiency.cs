using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BSLDaman.Models
{
    public class clsEfficiency
    {
    }

    public class clsEfficiencyReq
    {
        public string OrderNo { get; set; }
        public string Division { get; set; }
        public string LineName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public Int64 Code { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string QueryType { get; set; }
        public int vErrorCode { get; set; }
        public string vErrorMsg { get; set; }
    }

    public class clsEmployeeWiseEfficiency
    {
        public string Division { get; set; }
        public string LineName { get; set; }
        public string WorkDate { get; set; }
        public Int64 Code { get; set; }
        public string EmpName { get; set; }
        public string OrderNo { get; set; }
        public int OpNo { get; set; }
        public string Descriptions { get; set; }
        public decimal StdRate { get; set; }
        public decimal StdMin { get; set; }
        public int Qty { get; set; }
        public decimal ProductionMinute { get; set; }
        public decimal Amount { get; set; }
        public decimal Efficiency { get; set; }
        public string FirstBundleScanTime { get; set; }
        public string LastBundleScanTime { get; set; }
        public int ManualQty { get; set; }
        public int QrScanQty { get; set; }
        public int vErrorCode { get; set; }
        public string vErrorMsg { get; set; }
    }

    public class LinewiseEmployeeMonthlyEfficiency
    {
        public string LineName { get; set; }
        public string WorkDate { get; set; }
        public Int64 Code { get; set; }
        public string EmpName { get; set; }
        public int vErrorCode { get; set; }
        public string vErrorMsg { get; set; }
    }
}