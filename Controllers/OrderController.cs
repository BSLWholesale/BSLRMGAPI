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
    public class OrderController : ApiController
    {
        // GET: Order

        DALOrder _DALOrder = new DALOrder();

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Order/Fn_Insert_Order_Master")]
        public clsOrderMaster Fn_Insert_Order_Master(clsOrderMaster objReq)
        {
            var objResp = new clsOrderMaster();
            objResp = _DALOrder.Fn_Insert_Order_Master(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Order/Fn_Get_Order_Master")]
        public List<clsOrderMaster> Fn_Get_Order_Master(clsOrderMaster objReq)
        {
            var objResp = new List<clsOrderMaster>();
            objResp = _DALOrder.Fn_Get_Order_Master(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Order/Fn_Get_Order_Detail")]
        public List<clsOrderDetail> Fn_Get_Order_Detail(clsOrderDetail objReq)
        {
            var objResp = new List<clsOrderDetail>();
            objResp = _DALOrder.Fn_Get_Order_Detail(objReq);
            return objResp;
        }

        #region Start Process Master 4-Feb-2026

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Order/Fn_Insert_New_Process")]
        public clsProcessMaster Fn_Insert_New_Process(clsProcessMaster objReq)
        {
            var objResp = new clsProcessMaster();
            objResp = _DALOrder.Fn_Insert_New_Process(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Order/Fn_Delete_Process")]
        public clsProcessMaster Fn_Delete_Process(clsProcessMaster objReq)
        {
            var objResp = new clsProcessMaster();
            objResp = _DALOrder.Fn_Delete_Process(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Order/Fn_Get_ProcessMaster")]
        public List<clsProcessMaster> Fn_Get_ProcessMaster(clsProcessMaster objReq)
        {
            var objResp = new List<clsProcessMaster>();
            objResp = _DALOrder.Fn_Get_ProcessMaster(objReq);
            return objResp;
        }

        #endregion End Process Master 4-Feb-2026

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Order/Fn_Upload_Operation_BreackdownFile")]
        public clsOPBreackDownMaster Fn_Upload_Operation_BreackdownFile(clsOPBreackDownMaster objReq)
        {
            var objResp = new clsOPBreackDownMaster();
            objResp = _DALOrder.Fn_Upload_Operation_BreackdownFile(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Order/Fn_Get_Operation_BreackdownFile")]
        public List<clsOPBreackDownDetail> Fn_Get_Operation_BreackdownFile(clsOPBreackDownMaster objReq)
        {
            var objResp = new List<clsOPBreackDownDetail>();
            objResp = _DALOrder.Fn_Get_Operation_BreackdownFile(objReq);
            return objResp;
        }

        #region Start 24-FEB-2026 Check_Exist_style_In_Master

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Order/Fn_Check_Exist_style_In_Master")]
        public clsOPBreackDownMaster Fn_Check_Exist_style_In_Master(clsOPBreackDownMaster objReq)
        {
            var objResp = new clsOPBreackDownMaster();
            objResp = _DALOrder.Fn_Check_Exist_style_In_Master(objReq);
            return objResp;
        }

        #endregion End 24-FEB-2026 Check_Exist_style_In_Master

        #region Start Fn_Get_OB_BY_Product 30-MAR-2026

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Order/Fn_Get_OB_BY_Product")]
        public List<clsOPBreackDownDetail> Fn_Get_OB_BY_Product(clsOPBreackDownDetail objReq)
        {
            var objResp = new List<clsOPBreackDownDetail>();
            objResp = _DALOrder.Fn_Get_OB_BY_Product(objReq);
            return objResp;
        }

        #endregion End Fn_Get_OB_BY_Product 30-MAR-2026

        #region Start Fn_Update_Rate_In_OB_Master 01-APR-2026

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Order/Fn_Update_Rate_In_OB_Master")]
        public clsOPBreackDownDetail Fn_Update_Rate_In_OB_Master(clsOPBreackDownDetail objReq)
        {
            var objResp = new clsOPBreackDownDetail();
            objResp = _DALOrder.Fn_Update_Rate_In_OB_Master(objReq);
            return objResp;
        }

        #endregion End Fn_Update_Rate_In_OB_Master 01-APR-2026

        #region Start Fn_Add_New_OpNo 03-APR-2026

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Order/Fn_Add_New_OpNo")]
        public clsOPBreackDownDetail Fn_Add_New_OpNo(clsOPBreackDownDetail objReq)
        {
            var objResp = new clsOPBreackDownDetail();
            objResp = _DALOrder.Fn_Add_New_OpNo(objReq);
            return objResp;
        }

        #endregion End Fn_Add_New_OpNo 03-APR-2026

        #region Start Fn_Get_Order_Chart 13-APR-2026

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Order/Fn_Get_Order_Chart")]
        public List<clsOrderMaster> Fn_Get_Order_Chart(clsOrderMaster objReq)
        {
            var objResp = new List<clsOrderMaster>();
            objResp = _DALOrder.Fn_Get_Order_Chart(objReq);
            return objResp;
        }

        #endregion End Fn_Get_Order_Chart 13-APR-2026
    }
}