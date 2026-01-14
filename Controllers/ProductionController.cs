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
    public class ProductionController : ApiController
    {
        // GET: Production

        DALProduction _DALProduction = new DALProduction();

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Production/Fn_Insert_Production")]
        public clsProductionMastr Fn_Insert_Production(clsProductionMastr objReq)
        {
            var objResp = new clsProductionMastr();
            objResp = _DALProduction.Fn_Insert_Production(objReq);
            return objResp;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Production/Fn_Get_Production_Master")]
        public List<clsProductionMastr> Fn_Get_Production_Master(clsProductionMastr objReq)
        {
            var objResp = new List<clsProductionMastr>();
            objResp = _DALProduction.Fn_Get_Production_Master(objReq);
            return objResp;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Production/Fn_Get_Production_Detail")]
        public List<clsProductionDetail> Fn_Get_Production_Detail(clsProductionDetail objReq)
        {
            var objResp = new List<clsProductionDetail>();
            objResp = _DALProduction.Fn_Get_Production_Detail(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("api/Production/Fn_Update_Production")]
        public clsProductionMastr Fn_Update_Production(clsProductionMastr objReq)
        {
            var objResp = new clsProductionMastr();
            objResp = _DALProduction.Fn_Update_Production(objReq);
            return objResp;
        }
    }
}