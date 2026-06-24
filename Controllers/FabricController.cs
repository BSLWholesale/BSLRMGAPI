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
    public class FabricController : ApiController
    {
        // GET: Fabric

        DALFabric _DALFabric = new DALFabric();

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Fabric/Fn_Upload_Fabirc_Inhouse")]
        public FabricInhouse Fn_Upload_Fabirc_Inhouse(FabricInhouse objReq)
        {
            var objResp = new FabricInhouse();
            objResp = _DALFabric.Fn_Upload_Fabirc_Inhouse(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Fabric/Fn_Get_Fabric_Order")]
        public List<clsFabricOrder> Fn_Get_Fabric_Order(clsFabricOrder objReq)
        {
            var objResp = new List<clsFabricOrder>();
            objResp = _DALFabric.Fn_Get_Fabric_Order(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Fabric/Fn_Get_Fabric_Roll")]
        public List<FabricInhouseList> Fn_Get_Fabric_Roll(FabricInhouse objReq)
        {
            var objResp = new List<FabricInhouseList>();
            objResp = _DALFabric.Fn_Get_Fabric_Roll(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Fabric/Fn_Update_Fabric_RollNo")]
        public FabricInhouseList Fn_Update_Fabric_RollNo(FabricInhouseList objReq)
        {
            var objResp = new FabricInhouseList();
            objResp = _DALFabric.Fn_Update_Fabric_RollNo(objReq);
            return objResp;
        }
    }
}