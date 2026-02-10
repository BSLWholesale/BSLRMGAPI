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
    public class MasterEntryMobController : Controller
    {
        // GET: MasterEntryMob

        DALMasterEntryMob _DALMasterEntryMob = new DALMasterEntryMob();

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/MasterEntryMob/Fn_Add_New_Line")]
        public clsLine Fn_Add_New_Line(clsLine objReq)
        {
            var objResp = new clsLine();
            objResp = _DALMasterEntryMob.Fn_Add_New_Line(objReq);
            return objResp;
        }


        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/MasterEntryMob/Fn_Get_ActiveBundle")]
        public List<clsBundleCompile> Fn_Get_ActiveBundle(clsBundleCompile objReq)
        {
            var objResp = new List<clsBundleCompile>();
            objResp = _DALMasterEntryMob.Fn_Get_ActiveBundle(objReq);
            return objResp;
        }


        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("api/MasterEntryMob/Fn_Update_BundleID_By_EmpID")]
        public clsBundleCompile Fn_Update_BundleID_By_EmpID(clsBundleCompile objReq)
        {
            var objResp = new clsBundleCompile();
            objResp = _DALMasterEntryMob.Fn_Update_BundleID_By_EmpID(objReq);
            return objResp;
        }


    }
}