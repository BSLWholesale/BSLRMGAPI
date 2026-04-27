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
    public class MOBQualityController : Controller
    {

        MOBDALQuality _MOBDALQuality = new MOBDALQuality();


        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/MOBQuality/Fn_Fetch_AllOrderNumbers")]
        public List<clsOrderMaster> Fn_Fetch_AllOrderNumbers(clsOrderMaster objReq)
        {
            var objResp = new List<clsOrderMaster>();
            objResp = _MOBDALQuality.Fn_Fetch_AllOrderNumbers(objReq);
            return objResp;
        }




    }
}