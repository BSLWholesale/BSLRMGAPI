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
    public class MOBQualityController : ApiController
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

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/MOBQuality/Fn_Get_QA_checkPoint_Master")]
        public List<clsQACheckPoint> Fn_Get_QA_checkPoint_Master(clsQACheckPoint objReq)
        {
            var objResp = new List<clsQACheckPoint>();
            objResp = _MOBDALQuality.Fn_Get_QA_checkPoint_Master(objReq);
            return objResp;
        }

    }
}