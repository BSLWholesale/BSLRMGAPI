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
    }
}