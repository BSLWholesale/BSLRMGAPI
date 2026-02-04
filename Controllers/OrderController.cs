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
    }
}