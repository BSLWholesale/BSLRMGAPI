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
        public Int64 PlyFrom { get; set; }
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
}