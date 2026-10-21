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
        //public ActionResult Index()
        //{
        //    return View();
        //}

        MOBDALPostAssembly _MOBDALPostAssembly = new MOBDALPostAssembly();

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




    }
}