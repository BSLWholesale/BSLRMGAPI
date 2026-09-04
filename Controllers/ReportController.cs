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
    public class ReportController : ApiController
    {
        // GET: Report
        DALReport _DALReport = new DALReport();

        #region Start Fn_Get_Bundle_Report 06-APR-2026

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Report/Fn_Get_Bundle_Report")]
        public List<clsBundleCompile> Fn_Get_Bundle_Report(clsBundleCompile objReq)
        {
            var objResp = new List<clsBundleCompile>();
            objResp = _DALReport.Fn_Get_Bundle_Report(objReq);
            return objResp;
        }

        #endregion End Fn_Get_Bundle_Report 06-APR-2026

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Report/Fn_Get_OperationWise_OrderDetail_Report")]
        public List<clsOperationwiswReport> Fn_Get_OperationWise_OrderDetail_Report(clsOperationwiswReport objReq)
        {
            var objResp = new List<clsOperationwiswReport>();
            objResp = _DALReport.Fn_Get_OperationWise_OrderDetail_Report(objReq);
            return objResp;
        }

        #region Start Fn_Get_Earning_Report 17-APR-2026

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Report/Fn_Get_Earning_Report")]
        public List<clsEarningReport> Fn_Get_Earning_Report(clsEarningReport objReq)
        {
            var objResp = new List<clsEarningReport>();
            objResp = _DALReport.Fn_Get_Earning_Report(objReq);
            return objResp;
        }

        #endregion End Fn_Get_Earning_Report 17-APR-2026

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Report/Fn_Get_EfficiencyReport")]
        public List<clsEfficiencyReportResp> Fn_Get_EfficiencyReport(clsEfficiencyReportReq objReq)
        {
            var objResp = new List<clsEfficiencyReportResp>();
            objResp = _DALReport.Fn_Get_EfficiencyReport(objReq);
            return objResp;
        }

        #region Start Fn_Get_Piece_Rate_Report 20-May-2026

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Report/Fn_Get_Piece_Rate_Report")]
        public List<clsPieceRateReportResp> Fn_Get_Piece_Rate_Report(clsPieceRateReportReq objReq)
        {
            var objResp = new List<clsPieceRateReportResp>();
            objResp = _DALReport.Fn_Get_Piece_Rate_Report(objReq);
            return objResp;
        }

        #endregion End Fn_Get_Piece_Rate_Report 20-May-2026

        #region Start Fn_Get_Peice_Rate_Incentive 21-May-2026

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Report/Fn_Get_Peice_Rate_Incentive")]
        public List<clsPieceRateIncentive> Fn_Get_Peice_Rate_Incentive(clsPieceRateReportReq objReq)
        {
            var objResp = new List<clsPieceRateIncentive>();
            objResp = _DALReport.Fn_Get_Peice_Rate_Incentive(objReq);
            return objResp;
        }

        #endregion End Fn_Get_Peice_Rate_Incentive 21-May-2026

        #region Start Fn_Get_Pending_BundleStatus 26-May-2026

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Report/Fn_Get_Pending_BundleStatus")]
        public List<clsBundleStatusReportResp> Fn_Get_Pending_BundleStatus(clsBundleStatusReportReq objReq)
        {
            var objResp = new List<clsBundleStatusReportResp>();
            objResp = _DALReport.Fn_Get_Pending_BundleStatus(objReq);
            return objResp;
        }

        #endregion End Fn_Get_Pending_BundleStatus 26-May-2026

        #region Start Fn_Get_Assign_Finish_BundleStatus 28-May-2026

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Report/Fn_Get_Assign_Finish_BundleStatus")]
        public List<clsBundleStatusReportResp> Fn_Get_Assign_Finish_BundleStatus(clsBundleStatusReportReq objReq)
        {
            var objResp = new List<clsBundleStatusReportResp>();
            objResp = _DALReport.Fn_Get_Finish_BundleStatus(objReq);
            return objResp;
        }

        #endregion End Fn_Get_Assign_Finish_BundleStatus 26-May-2026

        #region Start Fn_Set_AS_Pilot 02-Jun-2026

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Report/Fn_Set_AS_Pilot")]
        public clsPilot Fn_Set_AS_Pilot(clsPilot objReq)
        {
            var objResp = new clsPilot();
            objResp = _DALReport.Fn_Set_AS_Pilot(objReq);
            return objResp;
        }

        #endregion End Fn_Set_AS_Pilot 02-Jun-2026

        #region Start Fn_Add_Multiple_Manual_Entry 04-JUN-2026

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Report/Fn_Add_Multiple_Manual_Entry")]
        public clsManualEntry Fn_Add_Multiple_Manual_Entry(clsManualEntry objReq)
        {
            var objResp = new clsManualEntry();
            objResp = _DALReport.Fn_Add_Multiple_Manual_Entry(objReq);
            return objResp;
        }

        #endregion End Fn_Add_Multiple_Manual_Entry 04-JUN-2026

        #region End Fn_Get_Pilot_Report 08-JUN_2026

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Report/Fn_Get_Pilot_Report")]
        public List<clsBundleStatusReportResp> Fn_Get_Pilot_Report(clsBundleStatusReportReq objReq)
        {
            var objResp = new List<clsBundleStatusReportResp>();
            objResp = _DALReport.Fn_Get_Pilot_Report(objReq);
            return objResp;
        }

        #endregion End Fn_Get_Pilot_Report 08-JUN_2026

        #region Start Fn_Manual_Entry_QuantityWise 17-AUG_2026

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Report/Fn_Manual_Entry_QuantityWise")]
        public clsQuantityManualEntry Fn_Manual_Entry_QuantityWise(clsQuantityManualEntry objReq)
        {
            var objResp = new clsQuantityManualEntry();
            objResp = _DALReport.Fn_Manual_Entry_QuantityWise(objReq);
            return objResp;
        }

        #endregion End Fn_Manual_Entry_QuantityWise 17-AUG_2026

        #region Start Fn_Remove_Manual_Quantity 18-AUG_2026

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Report/Fn_Remove_Manual_Quantity")]
        public clsQuantityManualEntry Fn_Remove_Manual_Quantity(clsQuantityManualEntry objReq)
        {
            var objResp = new clsQuantityManualEntry();
            objResp = _DALReport.Fn_Remove_Manual_Quantity(objReq);
            return objResp;
        }

        #endregion End Fn_Remove_Manual_Quantity 18-AUG_2026


        #region Start Fn_Get_QAQCDHUReport 20-AUG-2026

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Report/Fn_Get_QAQCDHUReport")]
        public List<clsQADHUReport> Fn_Get_QAQCDHUReport(clsQADHUReport objReq)
        {
            var objResp = new List<clsQADHUReport>();
            objResp = _DALReport.Fn_Get_QAQCDHUReport(objReq);
            return objResp;
        }

        #endregion End Fn_Get_QAQCDHUReport 20-AUG-2026


        #region Start Fn_Get_Rate_By_OpNo 03-SEP-2026 Added by Ankit

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Report/Fn_Get_Rate_By_OpNo")]
        public clsOPBreackDownDetail Fn_Get_Rate_By_OpNo(clsOPBreackDownMaster objReq)
        {
            var objResp = new clsOPBreackDownDetail();
            objResp = _DALReport.Fn_Get_Rate_By_OpNo(objReq);
            return objResp;
        }

        #endregion End Fn_Get_Rate_By_OpNo 03-SEP-2026 Added by Ankit
    }
}