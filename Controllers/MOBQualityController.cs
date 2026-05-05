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
        public List<clsQAOrderList> Fn_Fetch_AllOrderNumbers(clsQAOrderList objReq)
        {
            var objResp = new List<clsQAOrderList>();
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

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/MOBQuality/Fn_Get_QA_Order_SubSection")]
        public List<clsQASubSection> Fn_Get_QA_Order_SubSection(clsQASubSection objReq)
        {
            var objResp = new List<clsQASubSection>();
            objResp = _MOBDALQuality.Fn_Get_QA_Order_SubSection(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/MOBQuality/Fn_Insert_QA_Orderwise")]
        public clsQAOrder Fn_Insert_QA_Orderwise(clsQAOrder objReq)
        {
            var objResp = new clsQAOrder();
            objResp = _MOBDALQuality.Fn_Insert_QA_Order_Defect(objReq);
            return objResp;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/MOBQuality/Fn_Get_QA_Order_Defect")]
        public List<clsQAOrder> Fn_Get_QA_Order_Defect(clsQAOrder objReq)
        {
            var objResp = new List<clsQAOrder>();
            objResp = _MOBDALQuality.Fn_Get_QA_Order_Defect(objReq);
            return objResp;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/MOBQuality/Fn_Get_QA_Order_DefectList")]
        public List<clsQAOrderDefectList> Fn_Get_QA_Order_DefectList(clsQAOrderDefectList objReq)
        {
            var objResp = new List<clsQAOrderDefectList>();
            objResp = _MOBDALQuality.Fn_Get_QA_Order_DefectList(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("api/MOBQuality/Fn_Update_QA_Order_Defect")]
        public clsQAOrder Fn_Update_QA_Order_Defect(clsQAOrder objReq)
        {
            var objResp = new clsQAOrder();
            objResp = _MOBDALQuality.Fn_Update_QA_Order_Defect(objReq);
            return objResp;
        }

        [System.Web.Http.HttpDelete]
        [System.Web.Http.Route("api/MOBQuality/Fn_Delete_QA_Order_Defect")]
        public clsQAOrder Fn_Delete_QA_Order_Defect(clsQAOrder objReq)
        {
            var objResp = new clsQAOrder();
            objResp = _MOBDALQuality.Fn_Delete_QA_Order_Defect(objReq);
            return objResp;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/MOBQuality/Fn_Get_QAReport")]
        public List<clsQAReport> Fn_Get_QAReport(clsQAReport objReq)
        {
            var objResp = new List<clsQAReport>();
            objResp = _MOBDALQuality.Fn_Get_QAReport(objReq);
            return objResp;
        }
    }
}