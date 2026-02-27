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
    public class MOBProductionController : ApiController
    {
        // GET: MOBProduction

        MOBDALProduction _MOBDALProduction = new MOBDALProduction();


        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/MOBProduction/Fn_Get_ActiveBundle")]
        public List<clsBundleCompile> Fn_Get_ActiveBundle(clsBundleCompile objReq)
        {
            var objResp = new List<clsBundleCompile>();
            objResp = _MOBDALProduction.Fn_Get_ActiveBundle(objReq);
            return objResp;
        }


        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("api/MOBProduction/Fn_Update_BundleID_By_EmpID")]
        public clsBundleCompile Fn_Update_BundleID_By_EmpID(clsBundleCompile objReq)
        {
            var objResp = new clsBundleCompile();
            objResp = _MOBDALProduction.Fn_Update_BundleID_By_EmpID(objReq);
            return objResp;
        }


        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/MOBProduction/Fn_Get_OperationNumber")]
        public List<clsOPBreackDownDetail> Fn_Get_OperationNumber(clsOPBreackDownDetail objReq)
        {
            var objResp = new List<clsOPBreackDownDetail>();
            objResp = _MOBDALProduction.Fn_Get_OperationNumber(objReq);
            return objResp;
        }


        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/MOBProduction/Fn_Get_MachineLogMaster")]
        public List<clsMachineLogMaster> Fn_Get_MachineLogMaster(clsMachineLogMaster objReq)
        {
            var objResp = new List<clsMachineLogMaster>();
            objResp = _MOBDALProduction.Fn_Get_MachineLogMaster(objReq);
            return objResp;
        }


        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/MOBProduction/Fn_Insert_MachineLogTransaction")]
        public clsMachineLogLostTimeTransactions Fn_Insert_MachineLogTransaction(clsMachineLogLostTimeTransactions objReq)
        {
            var objResp = new clsMachineLogLostTimeTransactions();
            objResp = _MOBDALProduction.Fn_Insert_MachineLogTransaction(objReq);
            return objResp;
        }


        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/MOBProduction/Fn_Update_MachineLogTransaction")]
        public clsMachineLogLostTimeTransactions Fn_Update_MachineLogTransaction(clsMachineLogLostTimeTransactions objReq)
        {
            var objResp = new clsMachineLogLostTimeTransactions();
            objResp = _MOBDALProduction.Fn_Update_MachineLogTransaction(objReq);
            return objResp;
        }


        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/MOBProduction/Fn_Get_All_MachineLogTransactions")]
        public List<clsMachineLogLostTimeTransactions> Fn_Get_All_MachineLogTransactions(clsMachineLogLostTimeTransactions objReq)
        {
            var objResp = new List<clsMachineLogLostTimeTransactions>();
            objResp = _MOBDALProduction.Fn_Get_All_MachineLogTransactions(objReq);
            return objResp;
        }


        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/MOBProduction/Fn_Get_MachineLogLostTime")]
        public clsMachineLogLostTimeTransactions Fn_Get_MachineLogLostTime(clsMachineLogLostTimeTransactions objReq)
        {
            var objResp = new clsMachineLogLostTimeTransactions();
            objResp = _MOBDALProduction.Fn_Get_MachineLogLostTime(objReq);
            return objResp;
        }


        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("api/MOBProduction/Fn_Update_SupervisorAssignedBundleIDEmp")]
        public clsBundleCompile Fn_Update_SupervisorAssignedBundleIDEmp(clsBundleCompile objReq)
        {
            var objResp = new clsBundleCompile();
            objResp = _MOBDALProduction.Fn_Update_SupervisorAssignedBundleIDEmp(objReq);
            return objResp;
        }


        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("api/MOBProduction/Fn_Update_AppEmpStartBundleIDStatus")]
        public clsBundleCompile Fn_Update_AppEmpStartBundleIDStatus(clsBundleCompile objReq)
        {
            var objResp = new clsBundleCompile();
            objResp = _MOBDALProduction.Fn_Update_AppEmpStartBundleIDStatus(objReq);
            return objResp;
        }


    }
}