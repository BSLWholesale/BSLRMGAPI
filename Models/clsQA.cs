using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BSLDaman.Models
{
    public class clsQACheckPoint
    {
        public string Products { get; set; }
        public string SubSection { get; set; }
        public string vErrorMsg { get; set; }
        public int vErrorCode { get; set; }
    }
    public class clsQADefects
    {
        public Int64 ID { get; set; }
        public string Products { get; set; }
        public string SubSection { get; set; }
        public string Defects { get; set; }
        public string ImageName { get; set; }
        public string vErrorMsg { get; set; }
        public int vErrorCode { get; set; }
    }

    public class clsQAColors
    {
        
        public string OrderNo { get; set; }
        public string ColorName { get; set; }
        public string vErrorMsg { get; set; }
        public int vErrorCode { get; set; }
    }

    public class clsQASize
    {

        public string OrderNo { get; set; }
        public string ColorName { get; set; }
        public string SizeName { get; set; }
        public string vErrorMsg { get; set; }
        public int vErrorCode { get; set; }
    }
    public class clsQASubSection
    {
        public string OrderNo { get; set; }
        public string SubSection { get; set; }
        public string vErrorMsg { get; set; }
        public int vErrorCode { get; set; }
    }

    public class clsQAOrder
    {
        public Int64 QAID { get; set; }
        public string OrderNo { get; set; }
        public string SizeName { get; set; }
        public string SubSection { get; set; }
        public Int64 Qty { get; set; }
        public string QAStatus { get; set; }
        public string PlyNo { get; set; }
        public string Opr { get; set; }
        public string DefectName { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
        public string vErrorMsg { get; set; }
        public int vErrorCode { get; set; }
        public List<clsQAOrderDefectList> _oList { get; set; }
    }
    public class clsQAOrderList
    {
        public string OrderNo { get; set; }
        public string Product { get; set; }
        public Int64 Qty { get; set; }
        public string vErrorMsg { get; set; }
        public int vErrorCode { get; set; }
    }

    public class clsQAOrderDefectList
    {
        public Int64 QADetailID { get; set; }
        public Int64 QAID { get; set; }
        public Int64 DefectID { get; set; }
        public string Defect { get; set; }
        public string ImageName { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
        public string vErrorMsg { get; set; }
        public int vErrorCode { get; set; }

    }

    public class clsQAReport
    {
        public string OrderNo { get; set; }
        public string SizeName { get; set; }
        public Int64 OrderQty { get; set; }
        public string SubSection { get; set; }
        public string ColorName { get; set; }
        public string FTP { get; set; }
        public string Reject { get; set; }
        public string Repair { get; set; }
        public string Altered { get; set; }
        public string Pass { get; set; }
        public string SendRepair { get; set; }
        public string vErrorMsg { get; set; }
        public int vErrorCode { get; set; }
    }

    public class clsEfficiencyReportReq
    {
        public int Month { get; set; }
        public int Year { get; set; }
    }

    public class clsEfficiencyReportResp
    {
        public string Code { get; set; }
        public string EmpName { get; set; }
        // Dynamic columns
        public Dictionary<string, string> DynamicColumns { get; set; }
        = new Dictionary<string, string>();

        public int vErrorCode { get; set; }
        public string vErrorMsg { get; set; }
    }
}