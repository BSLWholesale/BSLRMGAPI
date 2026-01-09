using BSLDaman.DAL;
using BSLDaman.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace BSLDaman.Controllers
{
    public class MasterEntryController : ApiController
    {
        // GET: MasterEntry
        DALMasterEntry _DALMasterEntry = new DALMasterEntry();

        #region Start Division 05-Jan-2026

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/MasterEntry/Fn_Add_New_Division")]
        public clsDivision Fn_Add_New_Division(clsDivision objReq)
        {
            var objResp = new clsDivision();
            objResp = _DALMasterEntry.Fn_Add_New_Division(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/MasterEntry/Fn_Update_Division")]
        public clsDivision Fn_Update_Division(clsDivision objReq)
        {
            var objResp = new clsDivision();
            objResp = _DALMasterEntry.Fn_Update_Division(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/MasterEntry/Fn_Delete_Division")]
        public clsDivision Fn_Delete_Division(clsDivision objReq)
        {
            var objResp = new clsDivision();
            objResp = _DALMasterEntry.Fn_Delete_Division(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/MasterEntry/Fn_Get_All_Division")]
        public List<clsDivision> Fn_Get_All_Division(clsDivision objReq)
        {
            var objResp = new List<clsDivision>();
            objResp = _DALMasterEntry.Fn_Get_All_Division(objReq);
            return objResp;
        }

        #endregion End Division 05-Jan-2026

        #region Start Section 05-Jan-2026

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/MasterEntry/Fn_Add_New_Section")]
        public clsSection Fn_Add_New_Section(clsSection objReq)
        {
            var objResp = new clsSection();
            objResp = _DALMasterEntry.Fn_Add_New_Section(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/MasterEntry/Fn_Update_Section")]
        public clsSection Fn_Update_Section(clsSection objReq)
        {
            var objResp = new clsSection();
            objResp = _DALMasterEntry.Fn_Update_Section(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/MasterEntry/Fn_Delete_Section")]
        public clsSection Fn_Delete_Section(clsSection objReq)
        {
            var objResp = new clsSection();
            objResp = _DALMasterEntry.Fn_Delete_Section(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/MasterEntry/Fn_Get_All_Section")]
        public List<clsSection> Fn_Get_All_Section(clsSection objReq)
        {
            var objResp = new List<clsSection>();
            objResp = _DALMasterEntry.Fn_Get_All_Section(objReq);
            return objResp;
        }

        #endregion End Section 05-Jan-2026

        #region Start Line 05-Jan-2026

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/MasterEntry/Fn_Add_New_Line")]
        public clsLine Fn_Add_New_Line(clsLine objReq)
        {
            var objResp = new clsLine();
            objResp = _DALMasterEntry.Fn_Add_New_Line(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/MasterEntry/Fn_Update_Line")]
        public clsLine Fn_Update_Line(clsLine objReq)
        {
            var objResp = new clsLine();
            objResp = _DALMasterEntry.Fn_Update_Line(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/MasterEntry/Fn_Delete_Line")]
        public clsLine Fn_Delete_Line(clsLine objReq)
        {
            var objResp = new clsLine();
            objResp = _DALMasterEntry.Fn_Delete_Line(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/MasterEntry/Fn_Get_All_Line")]
        public List<clsLine> Fn_Get_All_Line(clsLine objReq)
        {
            var objResp = new List<clsLine>();
            objResp = _DALMasterEntry.Fn_Get_All_Line(objReq);
            return objResp;
        }

        #endregion End Line 05-Jan-2026

        #region Start Shift 05-Jan-2026

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/MasterEntry/Fn_Add_New_Shift")]
        public clsShift Fn_Add_New_Shift(clsShift objReq)
        {
            var objResp = new clsShift();
            objResp = _DALMasterEntry.Fn_Add_New_Shift(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/MasterEntry/Fn_Update_Shift")]
        public clsShift Fn_Update_Shift(clsShift objReq)
        {
            var objResp = new clsShift();
            objResp = _DALMasterEntry.Fn_Update_Shift(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/MasterEntry/Fn_Delete_Shift")]
        public clsShift Fn_Delete_Shift(clsShift objReq)
        {
            var objResp = new clsShift();
            objResp = _DALMasterEntry.Fn_Delete_Shift(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/MasterEntry/Fn_Get_All_Shift")]
        public List<clsShift> Fn_Get_All_Shift(clsShift objReq)
        {
            var objResp = new List<clsShift>();
            objResp = _DALMasterEntry.Fn_Get_All_Shift(objReq);
            return objResp;
        }

        #endregion End Shift 05-Jan-2026

        #region Start Customer 07-Jan-2026

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/MasterEntry/Fn_Add_New_Customer")]
        public clsCustomer Fn_Add_New_Customer(clsCustomer objReq)
        {
            var objResp = new clsCustomer();
            objResp = _DALMasterEntry.Fn_Add_New_Customer(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/MasterEntry/Fn_Update_Customer")]
        public clsCustomer Fn_Update_Customer(clsCustomer objReq)
        {
            var objResp = new clsCustomer();
            objResp = _DALMasterEntry.Fn_Update_Customer(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/MasterEntry/Fn_Delete_Customer")]
        public clsCustomer Fn_Delete_Customer(clsCustomer objReq)
        {
            var objResp = new clsCustomer();
            objResp = _DALMasterEntry.Fn_Delete_Customer(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/MasterEntry/Fn_Get_All_Customer")]
        public List<clsCustomer> Fn_Get_All_Customer(clsCustomer objReq)
        {
            var objResp = new List<clsCustomer>();
            objResp = _DALMasterEntry.Fn_Get_All_Customer(objReq);
            return objResp;
        }

        #endregion End Customer 07-Jan-2026

        #region Start SizeMaster 07-Jan-2026

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/MasterEntry/Fn_Add_New_SizeName")]
        public clsSizeMaster Fn_Add_New_SizeName(clsSizeMaster objReq)
        {
            var objResp = new clsSizeMaster();
            objResp = _DALMasterEntry.Fn_Add_New_SizeName(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/MasterEntry/Fn_Update_SizeName")]
        public clsSizeMaster Fn_Update_SizeName(clsSizeMaster objReq)
        {
            var objResp = new clsSizeMaster();
            objResp = _DALMasterEntry.Fn_Update_SizeName(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/MasterEntry/Fn_Delete_Customer")]
        public clsSizeMaster Fn_Delete_Customer(clsSizeMaster objReq)
        {
            var objResp = new clsSizeMaster();
            objResp = _DALMasterEntry.Fn_Delete_Customer(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/MasterEntry/Fn_Get_All_SizeName")]
        public List<clsSizeMaster> Fn_Get_All_SizeName(clsSizeMaster objReq)
        {
            var objResp = new List<clsSizeMaster>();
            objResp = _DALMasterEntry.Fn_Get_All_SizeName(objReq);
            return objResp;
        }

        #endregion End SizeMaster 07-Jan-2026

        #region Start Operator 07-Jan-2026

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/MasterEntry/Fn_Add_New_Operator")]
        public clsOperator Fn_Add_New_Operator(clsOperator objReq)
        {
            var objResp = new clsOperator();
            objResp = _DALMasterEntry.Fn_Add_New_Operator(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/MasterEntry/Fn_Get_All_Operator")]
        public List<clsOperator> Fn_Get_All_Operator(clsOperator objReq)
        {
            var objResp = new List<clsOperator>();
            objResp = _DALMasterEntry.Fn_Get_All_Operator(objReq);
            return objResp;
        }

        #endregion End Operator 07-Jan-2026

        #region Start LostTimeMaster 07-Jan-2026

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/MasterEntry/Fn_Insert_LostTimeMaster")]
        public clsLostTimeMaster Fn_Insert_LostTimeMaster(clsLostTimeMaster objReq)
        {
            var objResp = new clsLostTimeMaster();
            objResp = _DALMasterEntry.Fn_Insert_LostTimeMaster(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/MasterEntry/Fn_Get_All_LostTime")]
        public List<clsLostTimeMaster> Fn_Get_All_LostTime(clsLostTimeMaster objReq)
        {
            var objResp = new List<clsLostTimeMaster>();
            objResp = _DALMasterEntry.Fn_Get_All_LostTime(objReq);
            return objResp;
        }

        #endregion End LostTimeMaster 07-Jan-2026

        #region Start ProductionRepairCodeMaster 07-Jan-2026

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/MasterEntry/Fn_Insert_Production_RepairCode")]
        public clsProductionRepairCodeMaster Fn_Insert_Production_RepairCode(clsProductionRepairCodeMaster objReq)
        {
            var objResp = new clsProductionRepairCodeMaster();
            objResp = _DALMasterEntry.Fn_Insert_Production_RepairCode(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/MasterEntry/Fn_Get_All_Production_RepairCode")]
        public List<clsProductionRepairCodeMaster> Fn_Get_All_Production_RepairCode(clsProductionRepairCodeMaster objReq)
        {
            var objResp = new List<clsProductionRepairCodeMaster>();
            objResp = _DALMasterEntry.Fn_Get_All_Production_RepairCode(objReq);
            return objResp;
        }

        #endregion End ProductionRepairCodeMaster 07-Jan-2026

        #region Start MachineProblem 07-Jan-2026

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/MasterEntry/Fn_Insert_MachineProblem")]
        public clsMachineProblem Fn_Insert_MachineProblem(clsMachineProblem objReq)
        {
            var objResp = new clsMachineProblem();
            objResp = _DALMasterEntry.Fn_Insert_MachineProblem(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/MasterEntry/Fn_Get_All_MachineProblem")]
        public List<clsMachineProblem> Fn_Get_All_MachineProblem(clsMachineProblem objReq)
        {
            var objResp = new List<clsMachineProblem>();
            objResp = _DALMasterEntry.Fn_Get_All_MachineProblem(objReq);
            return objResp;
        }

        #endregion End MachineProblem 07-Jan-2026

        #region Start MachineServicesType 07-Jan-2026

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/MasterEntry/Fn_Insert_MachineServicesType")]
        public clsMachineServicesType Fn_Insert_MachineServicesType(clsMachineServicesType objReq)
        {
            var objResp = new clsMachineServicesType();
            objResp = _DALMasterEntry.Fn_Insert_MachineServicesType(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/MasterEntry/Fn_Get_All_MachineServicesType")]
        public List<clsMachineServicesType> Fn_Get_All_MachineServicesType(clsMachineServicesType objReq)
        {
            var objResp = new List<clsMachineServicesType>();
            objResp = _DALMasterEntry.Fn_Get_All_MachineServicesType(objReq);
            return objResp;
        }

        #endregion End MachineServicesType 07-Jan-2026

        #region Start SamplingCheckListPoint  07-Jan-2026

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/MasterEntry/Fn_Insert_SamplingCheckListPoint")]
        public clsSamplingCheckListPoint Fn_Insert_SamplingCheckListPoint(clsSamplingCheckListPoint objReq)
        {
            var objResp = new clsSamplingCheckListPoint();
            objResp = _DALMasterEntry.Fn_Insert_SamplingCheckListPoint(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/MasterEntry/Fn_Get_All_SamplingCheckListPoint")]
        public List<clsSamplingCheckListPoint> Fn_Get_All_SamplingCheckListPoint(clsSamplingCheckListPoint objReq)
        {
            var objResp = new List<clsSamplingCheckListPoint>();
            objResp = _DALMasterEntry.Fn_Get_All_SamplingCheckListPoint(objReq);
            return objResp;
        }

        #endregion End SamplingCheckListPoint  07-Jan-2026

        #region Start QualityAuditorTable 07-Jan-2026

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/MasterEntry/Fn_Insert_QualityAuditorTable")]
        public clsQualityAuditorTable Fn_Insert_QualityAuditorTable(clsQualityAuditorTable objReq)
        {
            var objResp = new clsQualityAuditorTable();
            objResp = _DALMasterEntry.Fn_Insert_QualityAuditorTable(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/MasterEntry/Fn_Get_All_QualityAuditorTable")]
        public List<clsQualityAuditorTable> Fn_Get_All_QualityAuditorTable(clsQualityAuditorTable objReq)
        {
            var objResp = new List<clsQualityAuditorTable>();
            objResp = _DALMasterEntry.Fn_Get_All_QualityAuditorTable(objReq);
            return objResp;
        }

        #endregion End QualityAuditorTable 07-Jan-2026

        #region Start FabricDefectMaster 07-Jan-2026

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/MasterEntry/Fn_Insert_FabricDefect")]
        public clsFabricDefectMaster Fn_Insert_FabricDefect(clsFabricDefectMaster objReq)
        {
            var objResp = new clsFabricDefectMaster();
            objResp = _DALMasterEntry.Fn_Insert_FabricDefect(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/MasterEntry/Fn_Get_All_FabricDefect")]
        public List<clsFabricDefectMaster> Fn_Get_All_FabricDefect(clsFabricDefectMaster objReq)
        {
            var objResp = new List<clsFabricDefectMaster>();
            objResp = _DALMasterEntry.Fn_Get_All_FabricDefect(objReq);
            return objResp;
        }

        #endregion End FabricDefectMaster 07-Jan-2026

        #region Start Maintenance 07-Jan-2026

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/MasterEntry/Fn_Insert_Maintenance")]
        public clsMaintenance Fn_Insert_Maintenance(clsMaintenance objReq)
        {
            var objResp = new clsMaintenance();
            objResp = _DALMasterEntry.Fn_Insert_Maintenance(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/MasterEntry/Fn_Get_All_Maintenance")]
        public List<clsMaintenance> Fn_Get_All_Maintenance(clsMaintenance objReq)
        {
            var objResp = new List<clsMaintenance>();
            objResp = _DALMasterEntry.Fn_Get_All_Maintenance(objReq);
            return objResp;
        }

        #endregion End Maintenance 07-Jan-2026

        #region Start AcceptableQualityLevel 07-Jan-2026

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/MasterEntry/Fn_Insert_AcceptableQualityLevel")]
        public clsAcceptableQualityLevel Fn_Insert_AcceptableQualityLevel(clsAcceptableQualityLevel objReq)
        {
            var objResp = new clsAcceptableQualityLevel();
            objResp = _DALMasterEntry.Fn_Insert_AcceptableQualityLevel(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/MasterEntry/Fn_Get_All_AcceptableQualityLevel")]
        public List<clsAcceptableQualityLevel> Fn_Get_All_AcceptableQualityLevel(clsAcceptableQualityLevel objReq)
        {
            var objResp = new List<clsAcceptableQualityLevel>();
            objResp = _DALMasterEntry.Fn_Get_All_AcceptableQualityLevel(objReq);
            return objResp;
        }

        #endregion End AcceptableQualityLevel 07-Jan-2026

        #region Start MaintenanceRemarks 07-Jan-2026

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/MasterEntry/Fn_Insert_MaintenanceRemarks")]
        public clsMaintenanceRemarks Fn_Insert_MaintenanceRemarks(clsMaintenanceRemarks objReq)
        {
            var objResp = new clsMaintenanceRemarks();
            objResp = _DALMasterEntry.Fn_Insert_MaintenanceRemarks(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/MasterEntry/Fn_Get_All_AcceptableQualityLevel")]
        public List<clsMaintenanceRemarks> Fn_Get_All_AcceptableQualityLevel(clsMaintenanceRemarks objReq)
        {
            var objResp = new List<clsMaintenanceRemarks>();
            objResp = _DALMasterEntry.Fn_Get_All_AcceptableQualityLevel(objReq);
            return objResp;
        }

        #endregion End MaintenanceRemarks 07-Jan-2026

        #region Start Holiday 07-Jan-2026

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/MasterEntry/Fn_Insert_Holiday")]
        public clsHoliday Fn_Insert_Holiday(clsHoliday objReq)
        {
            var objResp = new clsHoliday();
            objResp = _DALMasterEntry.Fn_Insert_Holiday(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/MasterEntry/Fn_Get_All_Holiday")]
        public List<clsHoliday> Fn_Get_All_Holiday(clsHoliday objReq)
        {
            var objResp = new List<clsHoliday>();
            objResp = _DALMasterEntry.Fn_Get_All_Holiday(objReq);
            return objResp;
        }

        #endregion End Holiday 07-Jan-2026
    }
}