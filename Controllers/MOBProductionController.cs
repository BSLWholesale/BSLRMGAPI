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


    }
}