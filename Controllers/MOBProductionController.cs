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
        public List<clsBundleCompile> Fn_Get_ActiveBundle(string OrderNo = null, Int64 ? BundleID = null)
        {
            clsBundleCompile objReq = new clsBundleCompile();

            if (!string.IsNullOrWhiteSpace(OrderNo))
            {
                objReq.OrderNo = OrderNo;
            }
            else
            {
                objReq.OrderNo = null;
            }

            if (BundleID.HasValue)
            {
                objReq.BundleID = BundleID.Value;
            }
            else
            {
                objReq.BundleID = 0;
            }

            var objResp = _MOBDALProduction.Fn_Get_ActiveBundle(objReq);
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
        public List<clsOPBreackDownDetail> Fn_Get_OperationNumber(int? OpNo = null)
        {
            clsOPBreackDownDetail objReq = new clsOPBreackDownDetail();

            if (OpNo.HasValue)
            {
                objReq.OpNo = OpNo.Value;
            }
            else
            {
                objReq.OpNo = 0;
            }

            var objResp = _MOBDALProduction.Fn_Get_OperationNumber(objReq);
            return objResp;
        }


        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/MOBProduction/Fn_Get_MachineLogMaster")]
        public List<clsMachineLogMaster> Fn_Get_MachineLogMaster(int? MachineLogId = null)
        {
            clsMachineLogMaster objReq = new clsMachineLogMaster();

            if (MachineLogId.HasValue)
            {
                objReq.MachineLogId = MachineLogId.Value;
            }
            else
            {
                objReq.MachineLogId = 0;
            }

            var objResp = _MOBDALProduction.Fn_Get_MachineLogMaster(objReq);
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
        public List<clsMachineLogLostTimeTransactions> Fn_Get_All_MachineLogTransactions(Int64? ID = null)
        {
            clsMachineLogLostTimeTransactions objReq = new clsMachineLogLostTimeTransactions();

            if (ID.HasValue)
            {
                objReq.ID = ID.Value;
            }
            else
            {
                objReq.ID = 0;
            }

            var objResp = _MOBDALProduction.Fn_Get_All_MachineLogTransactions(objReq);
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


        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("api/MOBProduction/Fn_Update_AppEmpEndBundleIDStatus")]
        public clsBundleCompile Fn_Update_AppEmpEndBundleIDStatus(clsBundleCompile objReq)
        {
            var objResp = new clsBundleCompile();
            objResp = _MOBDALProduction.Fn_Update_AppEmpEndBundleIDStatus(objReq);
            return objResp;
        }


        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/MOBProduction/Fn_Get_ActiveLineDetails")]
        public List<clsLine> Fn_Get_ActiveLineDetails(Int64? LineId = null)
        {
            clsLine objReq = new clsLine();

            if (LineId.HasValue)
            {
                objReq.LineId = LineId.Value;
            }
            else
            {
                objReq.LineId = 0;
            }

            var objResp = _MOBDALProduction.Fn_Get_ActiveLineDetails(objReq);
            return objResp;
        }


        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/MOBProduction/Fn_Get_ActiveLineCount")]
        public List<clsLine> Fn_Get_ActiveLineCount(clsLine objReq)
        {
            var objResp = new List<clsLine>();
            objResp = _MOBDALProduction.Fn_Get_ActiveLineCount(objReq);
            return objResp;
        }


        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/MOBProduction/Fn_Get_TotalBundleIdCount")]
        public List<clsBundleCompile> Fn_Get_TotalBundleIdCount(clsBundleCompile objReq)
        {
            var objResp = new List<clsBundleCompile>();
            objResp = _MOBDALProduction.Fn_Get_TotalBundleIdCount(objReq);
            return objResp;
        }


        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/MOBProduction/Fn_Get_MachineLogLostTimeInDaysHrMin")]
        public List<clsMachineLogLostTimeTransactions> Fn_Get_MachineLogLostTimeInDaysHrMin(clsMachineLogLostTimeTransactions objReq)
        {
            var objResp = new List<clsMachineLogLostTimeTransactions>();
            objResp = _MOBDALProduction.Fn_Get_MachineLogLostTimeInDaysHrMin(objReq);
            return objResp;
        }


        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("api/MOBProduction/Fn_Update_AppEmpStartEndBundleIDStatus")]
        public clsBundleCompile Fn_Update_AppEmpStartEndBundleIDStatus(clsBundleCompile objReq)
        {
            var objResp = new clsBundleCompile();
            objResp = _MOBDALProduction.Fn_Update_AppEmpStartEndBundleIDStatus(objReq);
            return objResp;
        }


        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/MOBProduction/Fn_Get_All_LinewiseOperatorCount")]
        public List<clsLine> Fn_Get_All_LinewiseOperatorCount(clsLine objReq)
        {
            var objResp = new List<clsLine>();
            objResp = _MOBDALProduction.Fn_Get_All_LinewiseOperatorCount(objReq);
            return objResp;
        }


        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/MOBProduction/Fn_Get_LineBundleIDCountOperator")]
        public List<clsBundleCompile> Fn_Get_LineBundleIDCountOperator(clsBundleCompile objReq)
        {
            var objResp = new List<clsBundleCompile>();
            objResp = _MOBDALProduction.Fn_Get_LineBundleIDCountOperator(objReq);
            return objResp;
        }


        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/MOBProduction/Fn_Get_OperatorBundleIDQtyStyleDetails")]
        public List<clsBundleCompile> Fn_Get_OperatorBundleIDQtyStyleDetails(clsBundleCompile objReq)
        {
            var objResp = new List<clsBundleCompile>();
            objResp = _MOBDALProduction.Fn_Get_OperatorBundleIDQtyStyleDetails(objReq);
            return objResp;
        }

        

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/MOBProduction/Fn_Get_OperatorIDWiseBundleDetails")]
        public List<clsBundleCompile> Fn_Get_OperatorIDWiseBundleDetails(clsBundleCompile objReq)
        {
            var objResp = new List<clsBundleCompile>();
            objResp = _MOBDALProduction.Fn_Get_OperatorIDWiseBundleDetails(objReq);
            return objResp;
        }



        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/MOBProduction/Fn_Get_LineOverviewDetails")]
        public List<clsLine> Fn_Get_LineOverviewDetails(clsLine objReq)
        {
            var objResp = new List<clsLine>();
            objResp = _MOBDALProduction.Fn_Get_LineOverviewDetails(objReq);
            return objResp;
        }


        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/MOBProduction/Fn_Get_ActiveLineIDWiseOperatorBundleDetails")]
        public List<clsBundleCompile> Fn_Get_ActiveLineIDWiseOperatorBundleDetails(clsBundleCompile objReq)
        {
            var objResp = new List<clsBundleCompile>();
            objResp = _MOBDALProduction.Fn_Get_ActiveLineIDWiseOperatorBundleDetails(objReq);
            return objResp;
        }



        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/MOBProduction/Fn_Get_LineOverviewOperatorDetailsByLineID")]
        public List<clsLine> Fn_Get_LineOverviewOperatorDetailsByLineID(clsLine objReq)
        {
            var objResp = new List<clsLine>();
            objResp = _MOBDALProduction.Fn_Get_LineOverviewOperatorDetailsByLineID(objReq);
            return objResp;
        }


        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/MOBProduction/Fn_Get_ActiveLineDetailsOrderNo")]
        public List<clsLine> Fn_Get_ActiveLineDetailsOrderNo(clsLine objReq)
        {
            var objResp = new List<clsLine>();
            objResp = _MOBDALProduction.Fn_Get_ActiveLineDetailsOrderNo(objReq);
            return objResp;
        }


        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/MOBProduction/Fn_Get_ActiveLineWiseOrderNoDetails")]
        public List<clsLine> Fn_Get_ActiveLineWiseOrderNoDetails(Int64? LineId = null)
        {
            clsLine objReq = new clsLine();

            if (LineId.HasValue)
            {
                objReq.LineId = LineId.Value;
            }
            else
            {
                objReq.LineId = 0;
            }

            var objResp = _MOBDALProduction.Fn_Get_ActiveLineWiseOrderNoDetails(objReq);
            return objResp;
        }


        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/MOBProduction/Fn_Get_All_OrderNoLineIDWise")]
        public List<clsLine> Fn_Get_All_OrderNoLineIDWise(Int64? LineId = null)
        {
            clsLine objReq = new clsLine();

            if (LineId.HasValue)
            {
                objReq.LineId = LineId.Value;
            }
            else
            {
                objReq.LineId = 0;
            }

            var objResp = _MOBDALProduction.Fn_Get_ActiveLineWiseOrderNoDetails(objReq);
            return objResp;
        }


    }
}