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


        //[System.Web.Http.HttpPost]
        //[System.Web.Http.Route("api/MasterEntryMob/Fn_Get_ActiveLine")]
        //public List<clsLine> Fn_Get_ActiveLine(clsLine objReq)
        //{
        //    var objResp = new List<clsLine>();
        //    objResp = _DALMasterEntryMob.Fn_Get_ActiveLine(objReq);
        //    return objResp;
        //}




    }
}