using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BSLDaman.Models
{
    public class clsMasterEntry
    {
    }

    public class clsDivision
    {
        public Int64 ID { get; set; }
        public string Division { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
        public string vErrorMsg { get; set; }
        public int vErrorCode { get; set; }

    }

    public class clsSection
    {
        public Int64 DivisionID { get; set; }
        public Int64 SectionID { get; set; }

        public string SectionName { get; set; }
        public string SectionHead { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
        public string vErrorMsg { get; set; }
        public int vErrorCode { get; set; }
    }

    public class clsLine
    {
        public Int64 DivisionID { get; set; }
        public Int64 LineId { get; set; }
        public int SeqNo { get; set; }
        public string LineCode { get; set; }
        public string LineName { get; set; }
        public string SuperVisor { get; set; }
        public string SectionName { get; set; }
        public int SuperMarketCode { get; set; }
        public bool IsQuality { get; set; }
        public bool IsFinishing { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
        public string vErrorMsg { get; set; }
        public int vErrorCode { get; set; }
    }
    public class clsShift
    {
        public int ShifID { get; set; }
        public string ShiftName { get; set; }
        public string StartTime { get; set; }
        public string LunchStart { get; set; }
        public string LunchEnd { get; set; }
        public string EndTime { get; set; }
        public string OTStart { get; set; }
        public string OTEnd { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
        public string vErrorMsg { get; set; }
        public int vErrorCode { get; set; }
    }

    public class clsCustomer
    {
        public int ID { get; set; }
        public string vName { get; set; }
        public string CodeNo { get; set; }
        public string vAddress { get; set; }
        public string vContact { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
        public string vErrorMsg { get; set; }
        public int vErrorCode { get; set; }
    }

    public class clsSizeMaster
    {
        public int ID { get; set; }
        public string SizeName { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
        public string vErrorMsg { get; set; }
        public int vErrorCode { get; set; }

    }

    public class clsOperator
    {
        public int ID { get; set; }
        public string CodeNO { get; set; }
        public string OpName { get; set; }
        public string FatherName { get; set; }
        public string Gender { get; set; }
        public string Grade { get; set; }
        public string Shift { get; set; }
        public string PIN { get; set; }
        public string OperatorType { get; set; }
        public string Mobile { get; set; }
        public string Notification { get; set; }
        public string Contractor { get; set; }
        public string PayRoll { get; set; }
        public string PermanentSection { get; set; }
        public bool IsTrainee { get; set; }
        public string IsTemporary { get; set; }
        public string DOJ { get; set; }

        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
        public string vErrorMsg { get; set; }
        public int vErrorCode { get; set; }
    }

    public class clsLostTimeMaster
    {
        public int ID { get; set; }
        public string CodeNO { get; set; }
        public string Description { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
        public string vErrorMsg { get; set; }
        public int vErrorCode { get; set; }
    }

    public class clsProductionRepairCodeMaster
    {
        public int ID { get; set; }
        public int SrNo { get; set; }
        public string CodeNO { get; set; }
        public string Description { get; set; }
        public bool IgnoreInDHU { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
        public string vErrorMsg { get; set; }
        public int vErrorCode { get; set; }
    }

    public class clsMachineProblem
    {
        public int ID { get; set; }
        public string CodeNO { get; set; }
        public string Description { get; set; }
        public bool Needle { get; set; }
        public bool OilLeak { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
        public string vErrorMsg { get; set; }
        public int vErrorCode { get; set; }
    }

    public class clsMachineServicesType
    {
        public int ID { get; set; }
        public string vName { get; set; }
        public string Display { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
        public string vErrorMsg { get; set; }
        public int vErrorCode { get; set; }
    }
    public class clsSamplingCheckListPoint
    {
        public int SeqNo { get; set; }
        public string QualityCheckList { get; set; }        
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
        public string vErrorMsg { get; set; }
        public int vErrorCode { get; set; }
    }
    public class clsQualityAuditorTable
    {
        public int ID { get; set; }
        public string AuditorTable { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
        public string vErrorMsg { get; set; }
        public int vErrorCode { get; set; }
    }

    public class clsFabricDefectMaster
    {
        public int ID { get; set; }
        public string CodeNO { get; set; }
        public string Description { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
        public string vErrorMsg { get; set; }
        public int vErrorCode { get; set; }
    }

    public class clsMaintenance
    {
        public int ID { get; set; }
        public string CodeNO { get; set; }
        public string vName { get; set; }
        public string SortName { get; set; }
        public string Category { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
        public string vErrorMsg { get; set; }
        public int vErrorCode { get; set; }
    }

    public class clsAcceptableQualityLevel
    {
        public int ID { get; set; }
        public string PCSFrom { get; set; }
        public string PCSTo { get; set; }
        public string SampleSize { get; set; }
        public string Accepted { get; set; }
        public string Rejected { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
        public string vErrorMsg { get; set; }
        public int vErrorCode { get; set; }
    }

    public class clsMaintenanceRemarks
    {
        public int ID { get; set; }
        public string Remarks { get; set; }
        public bool IsAsset { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
        public string vErrorMsg { get; set; }
        public int vErrorCode { get; set; }
    }
    public class clsHoliday
    {
        public int ID { get; set; }
        public string HoliDayDate { get; set; }
        public string Day { get; set; }
        public string Month { get; set; }   
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
        public string vErrorMsg { get; set; }
        public int vErrorCode { get; set; }
    }
}