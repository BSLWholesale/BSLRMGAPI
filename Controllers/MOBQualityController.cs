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
        [System.Web.Http.Route("api/MOBQuality/Fn_Get_QA_checkPoint")]
        public List<clsQACheckPoint> Fn_Get_QA_checkPoint(clsQACheckPoint objReq)
        {
            var objResp = new List<clsQACheckPoint>();
            objResp = _MOBDALQuality.Fn_Get_QA_checkPoint(objReq);
            return objResp;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/MOBQuality/Fn_Get_QA_Defects")]
        public List<clsQADefects> Fn_Get_QA_Defects(clsQADefects objReq)
        {
            var objResp = new List<clsQADefects>();
            objResp = _MOBDALQuality.Fn_Get_QA_Defects(objReq);
            return objResp;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/MOBQuality/Fn_Get_QA_Order_Color")]
        public List<clsQAColors> Fn_Get_QA_Order_Color(clsQAColors objReq)
        {
            var objResp = new List<clsQAColors>();
            objResp = _MOBDALQuality.Fn_Get_QA_Order_Color(objReq);
            return objResp;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/MOBQuality/Fn_Get_QA_Order_Size")]
        public List<clsQASize> Fn_Get_QA_Order_Size(clsQASize objReq)
        {
            var objResp = new List<clsQASize>();
            objResp = _MOBDALQuality.Fn_Get_QA_Order_Size(objReq);
            return objResp;
        }
    }
}