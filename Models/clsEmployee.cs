using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BSLDaman.Models
{
    public class clsEmployee
    {
    }

    public class clsEmployeeDetail
    {
        public Int64 SrNo { get; set; }
        public string Units { get; set; }
        public string LineName { get; set; }
        public Int64 Code { get; set; }
        public string EmpName { get; set; }
        public string Remarks { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
        public string vErrorMsg { get; set; }
        public int vErrorCode { get; set; }
        public string vQueryType { get; set; }
    }

    public class clsColorMaster
    {
        public int Id { get; set; }
        public string ColorName { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
        public string vErrorMsg { get; set; }
        public int vErrorCode { get; set; }
        public string vQueryType { get; set; }
    }
    public class clsProductionMastr
    {
        public Int64 ProductionOrderNo { get; set; }
        public string OrderDate { get; set; }
        public string @ProductionDeliveryDate { get; set; }
        public string Merchandiser { get; set; }
        public int SalesOrderNo { get; set; }
        public int PONo { get; set; }
        public int FabIndNo { get; set; }
        public int OrderQty { get; set; }
        public string StyleNo { get; set; }
        public string StyleName { get; set; }
        public string Buyer { get; set; }
        public string Brand { get; set; }
        public string PlantName { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
        public string vErrorMsg { get; set; }
        public int vErrorCode { get; set; }
        public string vQueryType { get; set; }
    }

    public class clsProductionDetail
    {
        public Int64 ID { get; set; }
        public Int64 ProductionOrderNo { get; set; }
        public string ShadeNo { get; set; }
        public string QualityNo { get; set; }
        public string Color { get; set; }
        public string SizeName { get; set; }
        public int Qty { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
        public string vErrorMsg { get; set; }
        public int vErrorCode { get; set; }
        public string vQueryType { get; set; }
    }
}