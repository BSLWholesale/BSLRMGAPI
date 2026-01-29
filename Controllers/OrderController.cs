using BSLDaman.DAL;
using BSLDaman.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BSLDaman.Controllers
{
    public class OrderController : Controller
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
    }
}