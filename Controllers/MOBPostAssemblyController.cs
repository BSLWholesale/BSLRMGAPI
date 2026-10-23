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
    public class MOBPostAssemblyController : ApiController
    {
        // GET: MOBPostAssembly

        MOBDALPostAssembly _MOBDALPostAssembly = new MOBDALPostAssembly();

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/MOBPostAssembly/Fn_Fetch_AllOrderNumbers")]
        public List<clsPostAssemblyOrderList> Fn_Fetch_AllOrderNumbers(clsPostAssemblyOrderList objReq)
        {
            var objResp = new List<clsPostAssemblyOrderList>();
            objResp = _MOBDALPostAssembly.Fn_Fetch_AllOrderNumbers(objReq);
            return objResp;
        }


        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/MOBPostAssembly/Fn_Fetch_AllLineNames")]
        public List<clsPostAssemblyLines> Fn_Fetch_AllLineNames(clsPostAssemblyLines objReq)
        {
            var objResp = new List<clsPostAssemblyLines>();
            objResp = _MOBDALPostAssembly.Fn_Fetch_AllLineNames(objReq);
            return objResp;
        }


        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("api/MOBPostAssembly/Fn_Update_PostAssemblySinglePcsAssign")]
        public clsPostAssembly Fn_Update_PostAssemblySinglePcsAssign(clsPostAssembly objReq)
        {
            var objResp = new clsPostAssembly();
            objResp = _MOBDALPostAssembly.Fn_Update_PostAssemblySinglePcsAssign(objReq);
            return objResp;
        }


        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("api/MOBPostAssembly/Fn_Update_PostAssemblySinglePcsScan")]
        public clsPostAssembly Fn_Update_PostAssemblySinglePcsScan(clsPostAssembly objReq)
        {
            var objResp = new clsPostAssembly();
            objResp = _MOBDALPostAssembly.Fn_Update_PostAssemblySinglePcsScan(objReq);
            return objResp;
        }


        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/MOBPostAssembly/Fn_Fetch_OperatorFinishedPostAssemblyBundleID")]
        public List<clsPostAssembly> Fn_Fetch_OperatorFinishedPostAssemblyBundleID(clsPostAssembly objReq)
        {
            var objResp = new List<clsPostAssembly>();
            objResp = _MOBDALPostAssembly.Fn_Fetch_OperatorFinishedPostAssemblyBundleID(objReq);
            return objResp;
        }


    }
}