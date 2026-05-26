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

        #region Start Fn_Get_BundleStatus_Report 26-May-2026

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Report/Fn_Get_BundleStatus_Report")]
        public List<clsBundleStatusReportResp> Fn_Get_BundleStatus_Report(clsBundleStatusReportReq objReq)
        {
            var objResp = new List<clsBundleStatusReportResp>();
            objResp = _DALReport.Fn_Get_BundleStatus_Report(objReq);
            return objResp;
        }

        #endregion End Fn_Get_BundleStatus_Report 26-May-2026
    }
}