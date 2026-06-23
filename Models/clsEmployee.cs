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
        public string OperatorType { get; set; }
        public string DeviceId { get; set; }
        public string QueryType { get; set; }
        public string TokenId { get; set; }
        public string vErrorMsg { get; set; }
        public int vErrorCode { get; set; }

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


    public class clsProductionMaster
    {
        public Int64 ProductionOrderNo { get; set; }
        public string OrderDate { get; set; }
        public string ProductionDeliveryDate { get; set; }
        public string Merchandiser { get; set; }
        public string SalesOrderNo { get; set; }
        public string PONo { get; set; }
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


    public class clsAutoCompliteRequest
    {
        public string SearchKeyword { get; set; }
        public string TableName { get; set; }
        public string FieldName { get; set; }
    }


    public class clsAutoCompliteResponse
    {
        public string SearchKeyword { get; set; }
    }


    public class clsOrderMaster
    {
        public Int64 ID { get; set; }
        public string OrderNo { get; set; }
        public int Qty { get; set; }
        public bool IsFinished { get; set; }
        public bool IsStkr { get; set; }
        public int BundleQty { get; set; }
        public string OrderDate { get; set; }
        public string StyleCode { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
        public string OrderStatus { get; set; }
        public string vErrorMsg { get; set; }
        public int vErrorCode { get; set; }
        public List<clsOrderDetail> oDetail { get; set; }
    }


    public class clsOrderDetail
    {
        public Int64 DetailID { get; set; }
        public string OrderNo { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public int Qty { get; set; }
        public int ExtraQty { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
        public string vErrorMsg { get; set; }
        public int vErrorCode { get; set; }

    }


    public class clsProcessMaster
    {
        public Int64 ID { get; set; }
        public string ProcessName { get; set; }
        public bool IsProduction { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
        public string vErrorMsg { get; set; }
        public int vErrorCode { get; set; }

    }

    public class clsOPBreackDownMaster
    {
        public Int64 ID { get; set; }
        public string StyleCode { get; set; }
        public string ProcessName { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
        public string vErrorMsg { get; set; }
        public int vErrorCode { get; set; }
        public List<clsOPBreackDownDetail> oList { get; set; }
        public Int64 nEmpId { get; set; }
        public int OpNo { get; set; }
    }


    public class clsOPBreackDownDetail
    {
        public Int64 DetailID { get; set; }
        public Int64 MID { get; set; }
        public int SeqNo { get; set; }
        public int OpNo { get; set; }
        public string Descriptions { get; set; }
        public string Machine { get; set; }
        public string SubSection { get; set; }
        public decimal StdMin { get; set; }
        public decimal Rate { get; set; }
        public string Product { get; set; }
        public string Skill { get; set; }
        public string Grade { get; set; }
        public string Folder { get; set; }
        public string Seamlength { get; set; }
        public bool IsDirect { get; set; }
        public string ProgressPoint { get; set; }
        public bool IsDispatch { get; set; }
        public string DependOPNO { get; set; }
        public bool IsDS { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
        public string vErrorMsg { get; set; }
        public int vErrorCode { get; set; }
    }


    public class clsBundleLayerMaster
    {
        public Int64 LayID { get; set; }
        public int Qty { get; set; }
        public double BundleLen { get; set; }
        public string CompileDate { get; set; }
        public string PrintDate { get; set; }
        public string StyleCode { get; set; }
        public string Marker { get; set; }
        public string OrderNo { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
        public string vErrorMsg { get; set; }
        public int vErrorCode { get; set; }
    }


    public class clsBundleSize
    {
        public Int64 LayID { get; set; }
        public Int64 SizeSelectionID { get; set; }
        public string SizeName { get; set; }
        public int SizeID { get; set; }
        public int Freq { get; set; }
        public string OrderNo { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
        public string vErrorMsg { get; set; }
        public int vErrorCode { get; set; }
    }


    public class clsBundleColor
    {
        public Int64 ColorSelectionID { get; set; }
        public Int64 LayID { get; set; }
        public string ColorName { get; set; }
        public string OrderNo { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
        public string vErrorMsg { get; set; }
        public int vErrorCode { get; set; }
    }


    public class clsBundleShade
    {
        public Int64 ShadeSelectionID { get; set; }
        public Int64 ColorSelectionID { get; set; }
        public Int64 LayID { get; set; }
        public string ShadeName { get; set; }
        public int CreatedBy { get; set; }
        public int Plies { get; set; }
        public string OrderNo { get; set; }
        public string CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
        public string vErrorMsg { get; set; }
        public int vErrorCode { get; set; }
    }


    public class clsBundleCompile
    {
        public Int64 LayID { get; set; }
        public Int64 BundleID { get; set; }
        public string BundleIDs { get; set; }
        public int BundleNo { get; set; }
        public string SizeName { get; set; }
        public string Freq { get; set; }
        public List<clsBundleSizeList> _oSizeList { get; set; }
        public string ColorName { get; set; }
        public string ShadeName { get; set; }
        public int Qty { get; set; }
        public int PlyFrom { get; set; }
        public int PlyTo { get; set; }
        public string Ply { get; set; }
        public int LotNo { get; set; }
        public int BunleQty { get; set; }
        public int CompileQty { get; set; }
        public string SubSection { get; set; }
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
        public string AppEmpIDs { get; set; }
        public string AppStartTime { get; set; }
        public string AppEndTime { get; set; }
        public string BundleIDStatus { get; set; }
        public string TotalBundleIdCount { get; set; }
        public string AppEmpName { get; set; }
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
        public decimal Earnings { get; set; }
        public string Months { get; set; }
        public decimal TotalAmount { get; set; }
        public string CurrentDate { get; set; }
        public string TimePeriod { get; set; }
        public string Descriptions { get; set; }
        public string OperationName { get; set; }
        public string SubProduct { get; set; }
        public Int32 PageNumber { get; set; }
        public Int32 PageSize { get; set; }
        public string SortBy { get; set; }
        public string SortDirection { get; set; }

        //public string SortBy { get; set; } = "BundleID";        // Default sort column
        //public string SortDirection { get; set; } = "ASC";      // ASC or DESC        
        //public Int32 TotalRecords { get; set; }                 // Pagination metadata (for response)
        //public Int32 TotalPages { get; set; }
        //public bool HasNextPage { get; set; }
        //public bool HasPreviousPage { get; set; }
        public string vErrorMsg { get; set; }
        public int vErrorCode { get; set; }
    }


    public class clsMAXID
    {
        public Int64 nMAXID { get; set; }
        public string vMAXID { get; set; }
        public string TBLName { get; set; }
        public string IDField { get; set; }
        public string DateField { get; set; }
        public string strPreFix { get; set; }
        public string vErrorMsg { get; set; }
    }


    public class clsMachineLogMaster
    {
        public int MachineLogId { get; set; }
        public string MachineLogName { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
        public string vErrorMsg { get; set; }
        public int vErrorCode { get; set; }
    }


    public class clsMachineLogLostTimeTransactions
    {
        public Int64 ID { get; set; }
        public Int64 LineId { get; set; }
        public string LineName { get; set; }
        public string MachineId { get; set; }
        public string MachineLogDescription { get; set; }
        public int EmpId { get; set; }
        public Int32 SupervisorID { get; set; }
        public Int32 AppEmpID { get; set; }
        public string MachineStatus { get; set; }
        public bool Needle { get; set; }
        public bool Oiling { get; set; }
        public string RepairDate { get; set; }
        public string RepairRemark { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
        public string TimeSpentDifference { get; set; }
        public string TotalMachineLogLostTime { get; set; }
        public string LostTimeInMinutes { get; set; }
        public string CurrentDate { get; set; }
        public string vErrorMsg { get; set; }
        public int vErrorCode { get; set; }

    }


    public class clsMOBEmployee
    {
        public Nullable<Int64> nEmpId { get; set; }
        public string vEmpName { get; set; }
        public string EmpGender { get; set; }
        public string vEmpGrade { get; set; }
        public string vEmpPassword { get; set; }
        public string OperatorType { get; set; }
        public string vEmpMobile { get; set; }
        public string Contractor { get; set; }
        public string EmpRole { get; set; }
        public string EmpLocation { get; set; }
        public string L1ManagerCode { get; set; }
        public string L1ManagerName { get; set; }
        public string EmpShift { get; set; }
        public Int64 ID { get; set; }
        public string DeviceId { get; set; }
        public string TokenId { get; set; }
        public bool IsActive { get; set; }
        public Int64 EmpOperatorCount { get; set; }
        public string EmpOperatorStatus { get; set; }
        public string LineName { get; set; }
        public Int64 LineId { get; set; }
        public string CreatedOn { get; set; }
        public Int32 CreatedBy { get; set; }
        public string ModifiedOn { get; set; }
        public Int32 ModifiedBy { get; set; }
        public string Units { get; set; }
        public string DOJ { get; set; }
        public string DOB { get; set; }
        public bool IsBirthdayWishes { get; set; }
        public bool IsWorkAnniversaryWishes { get; set; }
        public string vErrorMsg { get; set; }
        public int vErrorCode { get; set; }

    }


    public class clsDashboardEmployeeCount
    {
        public string EmpInDateTime { get; set; }
        public string UnitName { get; set; }
        public Int32 EmployeeCount { get; set; }
        public string vErrorMsg { get; set; }
    }

    public class clsOperationwiswReport
    {
        public string WorkDate { get; set; }
        public string OrderNo { get; set; }
        public string LineName { get; set; }
        public string Code { get; set; }
        public string EmpName { get; set; }
        public int Qty { get; set; }
        public string UpdateType { get; set; }
        public string vErrorMsg { get; set; }
        public int vErrorCode { get; set; }

    }

    public class clsEarningReport
    {
        public Int64 BundleID { get; set; }
        public string OrderNo { get; set; }
        public int OpNo { get; set; }
        public string SubSection { get; set; }
        public string LineName { get; set; }
        public string Code { get; set; }
        public string EmpName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int Qty { get; set; }
        public decimal StdMin { get; set; }
        public decimal StdRate { get; set; }
        public string UpdateType { get; set; }
        public string vErrorMsg { get; set; }
        public int vErrorCode { get; set; }

    }

    public class ApiResponse<T>
    {
        public List<T> Data { get; set; }
        public clsPagination Pagination { get; set; }
        public string vErrorMsg { get; set; }
        public int vErrorCode { get; set; }
    }

    public class clsPagination
    {
        public Int32 PageNumber { get; set; }
        public Int32 PageSize { get; set; }
        public Int32 TotalRecords { get; set; }
        public Int32 TotalPages { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }
    }

    public class clsColorShade
    {
        public string OrderNo { get; set; }
        public Int64 LayID { get; set; }
        public string ColorName { get; set; }
        public string ShadeName { get; set; }
        public int Plies { get; set; }
        public string vErrorMsg { get; set; }
        public int vErrorCode { get; set; }
    }

    public class clsBundleSizeList
    {
        public string SizeName { get; set; }
        public Int32 Freq { get; set; }
    }

    public class clsPieceRateReportReq
    {
        public string LineName { get; set; }
        public string StyleCode { get; set; }
        public string Code { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string OrderBy { get; set; }
        public string QueryType { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

    }
    public class clsPieceRateReportResp
    {
        public string LineName { get; set; }
        public string StyleCode { get; set; }
        public string Code { get; set; }
        public string EmpName { get; set; }
        public int OpNo { get; set; }
        public string OpName { get; set; }
        public string WorkDate { get; set; }
        public double Qty { get; set; }
        public double Rate { get; set; }
        public double Amount { get; set; }
        public string UpdateType { get; set; }
        public Int64 TotalRows { get; set; }
        public int TotalEmp { get; set; }
        public double TotalAmount { get; set; }
        public string vErrorMsg { get; set; }
        public int vErrorCode { get; set; }
    }

    public class clsPieceRateIncentive
    {
        public string LineName { get; set; }
        public string StyleCode { get; set; }
        public string Code { get; set; }
        public string EmpName { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public Int64 WorkingDays { get; set; }
        public Int64 TotalQty { get; set; }
        public double StdRate { get; set; }
        public double EarningPerDay { get; set; }
        public double TotalEarning { get; set; }
        public Int64 TotalRows { get; set; }
        public string vErrorMsg { get; set; }
        public int vErrorCode { get; set; }
    }

    public class clsBundleStatusReportReq
    {
        public string OrderNo { get; set; }
        public string SizeName { get; set; }
        public string SubSection { get; set; }
        public string BundleIDStatus { get; set; }
        public int AppEmpID { get; set; }
        public bool IsPilot { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class clsBundleStatusReportResp
    {
        public int OpNo { get; set; }
        public string OpName { get; set; }
        public Int64 BundleID { get; set; }
        public int BundleNo { get; set; }
        public string SizeName { get; set; }
        public string ColorName { get; set; }
        public string ShadeName { get; set; }
        public int Qty { get; set; }
        public int PlyFrom { get; set; }
        public int PlyTo { get; set; }
        public int LotNo { get; set; }
        public string SubSection { get; set; }
        public string StyleCode { get; set; }
        public string OrderNo { get; set; }
        public int AppEmpID { get; set; }
        public string EmpName { get; set; }
        public string AppStartTime { get; set; }
        public string AppEndTime { get; set; }
        public string BundleStatus { get; set; }
        public int SupervisorID { get; set; }
        public string SupervisorName { get; set; }
        public string AssignedDate { get; set; }
        public Int64 TotalRows { get; set; }
        public bool IsPilot { get; set; }
        public string vErrorMsg { get; set; }
        public int vErrorCode { get; set; }
    }

    public class clsPilot
    {
        public string BundleList { get; set; }
        public int CreatedBy { get; set; }
        public string vErrorMsg { get; set; }
        public int vErrorCode { get; set; }
    }

    public class clsManualEntry
    {
        public string BundleList { get; set; }
        public int AppEmpID { get; set; }
        public int CreatedBy { get; set; }
        public string AppStartTime { get; set; }
        public string AppEndTime { get; set; }
        public string vErrorMsg { get; set; }
        public int vErrorCode { get; set; }
    }

    public class clsFabricOrder
    {
        public string StyleCode { get; set; }
        public string ItemCode { get; set; }
        public string Descriptions { get; set; }
        public string Contents { get; set; }
        public string Mill { get; set; }
        public string FabricColor { get; set; }
        public decimal FabricCC { get; set; }
        public decimal Width { get; set; }
        public decimal WidthTolerance { get; set; }
        public decimal OrderRollLength { get; set; }
        public decimal OrderRollLengthTolerance { get; set; }
        public decimal GSM { get; set; }
        public decimal GSMTolerance { get; set; }
        public decimal OrderShrinkageWarpLength { get; set; }
        public decimal OrderShrinkageWaftWidth { get; set; }
        public decimal TotalQuantity { get; set; }
        public string Unit { get; set; }
        public string MarkerType { get; set; }
        public decimal Price { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public string QueryType { get; set; }
        public string vErrorMsg { get; set; }
        public int vErrorCode { get; set; }
    }

    public class clsFabricColor
    {
        public string StyleCode { get; set; }
        public string ColorName { get; set; }
        public string vErrorMsg { get; set; }
        public int vErrorCode { get; set; }
    }

    public class FabricInhouse
    {
        public string StyleCode { get; set; }
        public string ItemCode { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public string vErrorMsg { get; set; }
        public int vErrorCode { get; set; }
        public List<FabricInhouseList> _oInhouseList { get; set; }
    }
    public class FabricInhouseList
    {
        public Int64 InHouseId { get; set; }
        public int Lotno { get; set; }
        public decimal RollNo { get; set; }
        public decimal Quantity { get; set; }
        public string Unit { get; set; }
        public decimal Width { get; set; }
        public string ShadeName { get; set; }
        public decimal GSM { get; set; }
        public decimal Shrinkage { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public string QueryType { get; set; }
        public string vErrorMsg { get; set; }
        public int vErrorCode { get; set; }
    }
}