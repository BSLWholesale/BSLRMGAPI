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


        //[System.Web.Http.HttpGet]
        //[System.Web.Http.Route("api/MOBProduction/Fn_Get_ActiveBundle")]
        //public List<clsBundleCompile> Fn_Get_ActiveBundle(string OrderNo = null, Int64 ? OperationNo = null, Int64 ? BundleID = null, string BundleIDStatus = null)
        //{
        //    clsBundleCompile objReq = new clsBundleCompile();

        //    if (!string.IsNullOrWhiteSpace(OrderNo))
        //    {
        //        objReq.OrderNo = OrderNo;
        //    }
        //    else
        //    {
        //        objReq.OrderNo = null;
        //    }

        //    if (OperationNo.HasValue)
        //    {
        //        objReq.OperationNo = OperationNo.Value;
        //    }
        //    else
        //    {
        //        objReq.OperationNo = 0;
        //    }

        //    if (BundleID.HasValue)
        //    {
        //        objReq.BundleID = BundleID.Value;
        //    }
        //    else
        //    {
        //        objReq.BundleID = 0;
        //    }

        //    if (!string.IsNullOrWhiteSpace(BundleIDStatus))
        //    {
        //        objReq.BundleIDStatus = BundleIDStatus;
        //    }
        //    else
        //    {
        //        objReq.BundleIDStatus = null;
        //    }

        //    var objResp = _MOBDALProduction.Fn_Get_ActiveBundle(objReq);
        //    return objResp;
        //}


        //[System.Web.Http.HttpGet]
        //[System.Web.Http.Route("api/MOBProduction/Fn_Get_ActiveBundle")]
        //public ApiResponse<clsBundleCompile> Fn_Get_ActiveBundle(string OrderNo = null, Int32? PageNumber = null, Int32? PageSize = null, string SortBy = null, string SortDirection = null)
        //{
        //    clsBundleCompile objReq = new clsBundleCompile();

        //    if (!string.IsNullOrWhiteSpace(OrderNo))
        //    {
        //        objReq.OrderNo = OrderNo;
        //    }
        //    else
        //    {
        //        objReq.OrderNo = null;
        //    }

        //    if (PageNumber.HasValue)
        //    {
        //        objReq.PageNumber = PageNumber.Value;
        //    }
        //    else
        //    {
        //        objReq.PageNumber = 0;
        //    }

        //    if (PageSize.HasValue)
        //    {
        //        objReq.PageSize = PageSize.Value;
        //    }
        //    else
        //    {
        //        objReq.PageSize = 0;
        //    }

        //    if (!string.IsNullOrWhiteSpace(SortBy))
        //    {
        //        objReq.SortBy = SortBy;
        //    }
        //    else
        //    {
        //        objReq.SortBy = null;
        //    }

        //    if (!string.IsNullOrWhiteSpace(SortDirection))
        //    {
        //        objReq.SortDirection = SortDirection;
        //    }
        //    else
        //    {
        //        objReq.SortDirection = null;
        //    }

        //    var objResp = _MOBDALProduction.Fn_Get_ActiveBundle(objReq);
        //    return objResp;
        //}


        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/MOBProduction/Fn_Get_ActiveBundle")]
        public List<clsBundleCompile> Fn_Get_ActiveBundle(string OrderNo = null)
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

            //if (PageNumber.HasValue)
            //{
            //    objReq.PageNumber = PageNumber.Value;
            //}
            //else
            //{
            //    objReq.PageNumber = 0;
            //}

            //if (PageSize.HasValue)
            //{
            //    objReq.PageSize = PageSize.Value;
            //}
            //else
            //{
            //    objReq.PageSize = 0;
            //}

            //if (!string.IsNullOrWhiteSpace(SortBy))
            //{
            //    objReq.SortBy = SortBy;
            //}
            //else
            //{
            //    objReq.SortBy = null;
            //}

            //if (!string.IsNullOrWhiteSpace(SortDirection))
            //{
            //    objReq.SortDirection = SortDirection;
            //}
            //else
            //{
            //    objReq.SortDirection = null;
            //}

            var objResp = _MOBDALProduction.Fn_Get_ActiveBundle(objReq);
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
        public List<clsLine> Fn_Get_ActiveLineWiseOrderNoDetails(Int64? LineId = null, string OrderNo = null)
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

            if (!string.IsNullOrWhiteSpace(OrderNo))
            {
                objReq.OrderNo = OrderNo;
            }
            else
            {
                objReq.OrderNo = null;
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

            var objResp = _MOBDALProduction.Fn_Get_All_OrderNoLineIDWise(objReq);
            return objResp;
        }


        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/MOBProduction/Fn_Fetch_OperatorIDWiseBundleIDDetails")]
        public List<clsBundleCompile> Fn_Fetch_OperatorIDWiseBundleIDDetails(Int32? AppEmpID = null, string BundleIDStatus = null)
        {
            clsBundleCompile objReq = new clsBundleCompile();

            if (AppEmpID.HasValue)
            {
                objReq.AppEmpID = AppEmpID.Value;
            }
            else
            {
                objReq.AppEmpID = 0;
            }

            if (!string.IsNullOrWhiteSpace(BundleIDStatus))
            {
                objReq.BundleIDStatus = BundleIDStatus;
            }
            else
            {
                objReq.BundleIDStatus = null;
            }

            var objResp = _MOBDALProduction.Fn_Fetch_OperatorIDWiseBundleIDDetails(objReq);
            return objResp;
        }


        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/MOBProduction/Fn_Fetch_All_OrderNumbers")]
        public List<clsOrderMaster> Fn_Fetch_All_OrderNumbers(string OrderNo = null)
        {
            clsOrderMaster objReq = new clsOrderMaster();

            if (!string.IsNullOrWhiteSpace(OrderNo))
            {
                objReq.OrderNo = OrderNo;
            }
            else
            {
                objReq.OrderNo = null;
            }

            var objResp = _MOBDALProduction.Fn_Fetch_All_OrderNumbers(objReq);
            return objResp;
        }


        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/MOBProduction/Fn_Fetch_AssignedTenBundleDetails")]
        public List<clsBundleCompile> Fn_Fetch_AssignedTenBundleDetails(Int32? AppEmpID = null)
        {
            clsBundleCompile objReq = new clsBundleCompile();

            if (AppEmpID.HasValue)
            {
                objReq.AppEmpID = AppEmpID.Value;
            }
            else
            {
                objReq.AppEmpID = 0;
            }

            var objResp = _MOBDALProduction.Fn_Fetch_AssignedTenBundleDetails(objReq);
            return objResp;
        }


        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/MOBProduction/Fn_Fetch_FinishedTenBundleDetails")]
        public List<clsBundleCompile> Fn_Fetch_FinishedTenBundleDetails(Int32? AppEmpID = null)
        {
            clsBundleCompile objReq = new clsBundleCompile();

            if (AppEmpID.HasValue)
            {
                objReq.AppEmpID = AppEmpID.Value;
            }
            else
            {
                objReq.AppEmpID = 0;
            }

            var objResp = _MOBDALProduction.Fn_Fetch_FinishedTenBundleDetails(objReq);
            return objResp;
        }


        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/MOBProduction/Fn_Fetch_OperationNumberWiseDetails")]
        public List<clsBundleCompile> Fn_Fetch_OperationNumberWiseDetails(Int64? OperationNo = null)
        {
            clsBundleCompile objReq = new clsBundleCompile();

            if (OperationNo.HasValue)
            {
                objReq.OperationNo = OperationNo.Value;
            }
            else
            {
                objReq.OperationNo = 0;
            }

            var objResp = _MOBDALProduction.Fn_Fetch_OperationNumberWiseDetails(objReq);
            return objResp;
        }


        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/MOBProduction/Fn_Fetch_TotalEarningDetails")]
        public List<clsBundleCompile> Fn_Fetch_TotalEarningDetails(Int32? AppEmpID = null, string CurrentDate = null)
        {
            clsBundleCompile objReq = new clsBundleCompile();

            if (AppEmpID.HasValue)
            {
                objReq.AppEmpID = AppEmpID.Value; 
            }
            else
            {
                objReq.AppEmpID = 0;
            }

            if (!string.IsNullOrWhiteSpace(CurrentDate))
            {
                objReq.CurrentDate = CurrentDate;
            }
            else
            {
                objReq.CurrentDate = null;
            }

            var objResp = _MOBDALProduction.Fn_Fetch_TotalEarningDetails(objReq);
            return objResp;
        }


        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/MOBProduction/Fn_Fetch_TotalEarningDetailsByOpNo")]
        public List<clsBundleCompile> Fn_Fetch_TotalEarningDetailsByOpNo(Int32? AppEmpID = null, string CurrentDate = null, string LineName = null)
        {
            clsBundleCompile objReq = new clsBundleCompile();

            if (AppEmpID.HasValue)
            {
                objReq.AppEmpID = AppEmpID.Value;
            }
            else
            {
                objReq.AppEmpID = 0;
            }

            if (!string.IsNullOrWhiteSpace(CurrentDate))
            {
                objReq.CurrentDate = CurrentDate;
            }
            else
            {
                objReq.CurrentDate = null;
            }

            if (!string.IsNullOrWhiteSpace(LineName))
            {
                objReq.LineName = LineName;
            }
            else
            {
                objReq.LineName = null;
            }

            var objResp = _MOBDALProduction.Fn_Fetch_TotalEarningDetailsByOpNo(objReq);
            return objResp;
        }


        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/MOBProduction/Fn_Get_SupervisorAssignToOperator")]
        public List<clsBundleCompile> Fn_Get_SupervisorAssignToOperator(string OrderNo = null)
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

            var objResp = _MOBDALProduction.Fn_Get_SupervisorAssignToOperator(objReq);
            return objResp;
        }


        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/MOBProduction/Fn_Get_AssignedOperationNumberDetails")]
        public List<clsBundleCompile> Fn_Get_AssignedOperationNumberDetails(Int32? AppEmpID = null)
        {
            clsBundleCompile objReq = new clsBundleCompile();

            if (AppEmpID.HasValue)
            {
                objReq.AppEmpID = AppEmpID.Value;
            }
            else
            {
                objReq.AppEmpID = 0;
            }

            var objResp = _MOBDALProduction.Fn_Get_AssignedOperationNumberDetails(objReq);
            return objResp;
        }


        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/MOBProduction/Fn_Fetch_OrderNumberDetails")]
        public List<clsOrderMaster> Fn_Fetch_OrderNumberDetails(string OrderNo = null)
        {
            clsOrderMaster objReq = new clsOrderMaster();

            if (!string.IsNullOrWhiteSpace(OrderNo))
            {
                objReq.OrderNo = OrderNo;
            }
            else
            {
                objReq.OrderNo = null;
            }

            var objResp = _MOBDALProduction.Fn_Fetch_OrderNumberDetails(objReq);
            return objResp;
        }


        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/MOBProduction/Fn_Fetch_OperatorAssignOpNumbers")]
        public List<clsBundleCompile> Fn_Fetch_OperatorAssignOpNumbers(Int32? AppEmpID = null, string BundleIDStatus = null)
        {
            clsBundleCompile objReq = new clsBundleCompile();

            if (AppEmpID.HasValue)
            {
                objReq.AppEmpID = AppEmpID.Value;
            }
            else
            {
                objReq.AppEmpID = 0;
            }

            if (!string.IsNullOrWhiteSpace(BundleIDStatus))
            {
                objReq.BundleIDStatus = BundleIDStatus;
            }
            else
            {
                objReq.BundleIDStatus = null;
            }

            var objResp = _MOBDALProduction.Fn_Fetch_OperatorAssignOpNumbers(objReq);
            return objResp;
        }


        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/MOBProduction/Fn_Fetch_SupervisorAssignOpNoToOperators")]
        public List<clsBundleCompile> Fn_Fetch_SupervisorAssignOpNoToOperators(Int32? SupervisorID = null, Int32? AppEmpID = null, string OrderNo = null, Int64? OperationNo = null)
        {
            clsBundleCompile objReq = new clsBundleCompile();

            if (SupervisorID.HasValue)
            {
                objReq.SupervisorID = SupervisorID.Value;
            }
            else
            {
                objReq.SupervisorID = 0;
            }

            if (AppEmpID.HasValue)
            {
                objReq.AppEmpID = AppEmpID.Value;
            }
            else
            {
                objReq.AppEmpID = 0;
            }

            if (!string.IsNullOrWhiteSpace(OrderNo))
            {
                objReq.OrderNo = OrderNo;
            }
            else
            {
                objReq.OrderNo = null;
            }

            if (OperationNo.HasValue)
            {
                objReq.OperationNo = OperationNo.Value;
            }
            else
            {
                objReq.OperationNo = 0;
            }

            var objResp = _MOBDALProduction.Fn_Fetch_SupervisorAssignOpNoToOperators(objReq);
            return objResp;
        }



    }
}