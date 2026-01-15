using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BSLDaman.Models
{
    public class clsEmployee
    {
        public Nullable<Int64> nEmpId { get; set; }
        public string vEmpEmailId { get; set; }
        public string vEmpMobile { get; set; }
        public string vEmpName { get; set; }
        public string vEmpLocation { get; set; }
        public int nBSLTravelDesk { get; set; }
        public string vEmpPassword { get; set; }
        public string DOB { get; set; }
        public string vEmpDivision { get; set; }
        public bool bEmpActiveStatus { get; set; }
        public string vEmpType { get; set; }
        public string vEmpGrade { get; set; }
        public string vEmpDesignation { get; set; }
        public string vAadharNumber { get; set; }
        public string vPassportNumber { get; set; }
        public string PassportValid { get; set; }
        public string vDocAadhar { get; set; }
        public string vDocPassport { get; set; }
        public Int64 nL1ManagerCode { get; set; }
        public string vL1ManagerName { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public DateTime DateOfJoin { get; set; }
        public string Confirmation { get; set; }
        public Int64 nL2ManagerCode { get; set; }
        public string vL2ManagerName { get; set; }
        public string EmpDept { get; set; }
        public string ConfirmationStatus { get; set; }
        public string TravelDeskDescription { get; set; }
        public string AppraisalFormStatus { get; set; }
        public string AppraisalCriteria { get; set; }
        public string BusinessDivision { get; set; }
        public string AssessmentYear { get; set; }
        public string EmpGender { get; set; }
        public string EmpRole { get; set; }
        public string QueryType { get; set; }
        public string vErrorMsg { get; set; }
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
        public string ProductionDeliveryDate { get; set; }
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
        public List<clsProductionDetail> _ODetail { get; set; }
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

    public class clsRequestDropdown
    {
        public string vFieldName { get; set; }
        public string vValueField { get; set; }
        public string vTBLName { get; set; }
        public string vCriteria { get; set; }
        public string vErrorMsg { get; set; }
    }
    public class clsResponseDropdown
    {
        public string vFieldName { get; set; }
        public string vValueField { get; set; }
        public string vErrorMsg { get; set; }
    }
}