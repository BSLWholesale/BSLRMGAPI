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
        [System.Web.Http.Route("api/Production/Fn_Insert_Production_Order")]
        public clsProductionMaster Fn_Insert_Production_Order(clsProductionMaster objReq)
        {
            var objResp = new clsProductionMaster();
            objResp = _DALProduction.Fn_Insert_Production_Order(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Production/Fn_Get_Production_Order")]
        public List<clsProductionMaster> Fn_Get_Production_Order(clsProductionMaster objReq)
        {
            var objResp = new List<clsProductionMaster>();
            objResp = _DALProduction.Fn_Get_Production_Order(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Production/Fn_Get_Production_Detail")]
        public List<clsProductionDetail> Fn_Get_Production_Detail(clsProductionDetail objReq)
        {
            var objResp = new List<clsProductionDetail>();
            objResp = _DALProduction.Fn_Get_Production_Detail(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("api/Production/Fn_Update_Production")]
        public clsProductionMaster Fn_Update_Production(clsProductionMaster objReq)
        {
            var objResp = new clsProductionMaster();
            objResp = _DALProduction.Fn_Update_Production(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Production/Fn_Insert_Style")]
        public clsStyle Fn_Insert_Style(clsStyle objReq)
        {
            var objResp = new clsStyle();
            objResp = _DALProduction.Fn_Insert_Style(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Production/Fn_Get_Style")]
        public List<clsStyle> Fn_Get_Style(clsStyle objReq)
        {
            var objResp = new List<clsStyle>();
            objResp = _DALProduction.Fn_Get_Style(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Production/Fn_AutoComplete_Textbox")]
        public List<clsAutoCompliteResponse> Fn_AutoComplete_Textbox(clsAutoCompliteRequest objReq)
        {
            var objResp = new List<clsAutoCompliteResponse>();
            objResp = _DALProduction.Fn_AutoComplete_Textbox(objReq);
            return objResp;
        }

        #region Start Layer- Bundle 6-Feb-2026

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Production/Fn_Insert_Bundle_Layer")]
        public clsBundleLayerMaster Fn_Insert_Bundle_Layer(clsBundleLayerMaster objReq)
        {
            var objResp = new clsBundleLayerMaster();
            objResp = _DALProduction.Fn_Insert_Bundle_Layer(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Production/Fn_Delete_Bundle_Layer")]
        public clsBundleLayerMaster Fn_Delete_Bundle_Layer(clsBundleLayerMaster objReq)
        {
            var objResp = new clsBundleLayerMaster();
            objResp = _DALProduction.Fn_Delete_Bundle_Layer(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Production/Fn_Get_Bundle_Layer")]
        public List<clsBundleLayerMaster> Fn_Get_Bundle_Layer(clsBundleLayerMaster objReq)
        {
            var objResp = new List<clsBundleLayerMaster>();
            objResp = _DALProduction.Fn_Get_Bundle_Layer(objReq);
            return objResp;
        }


        #endregion End Layer- Bundle 6-Feb-2026

        #region Start Size- Bundle 7-Feb-2026

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Production/Fn_Insert_Bundle_Size")]
        public clsBundleSize Fn_Insert_Bundle_Size(clsBundleSize objReq)
        {
            var objResp = new clsBundleSize();
            objResp = _DALProduction.Fn_Insert_Bundle_Size(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Production/Fn_Delete_Bundle_Size")]
        public clsBundleSize Fn_Delete_Bundle_Size(clsBundleSize objReq)
        {
            var objResp = new clsBundleSize();
            objResp = _DALProduction.Fn_Delete_Bundle_Size(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Production/Fn_Get_Bundle_Size")]
        public List<clsBundleSize> Fn_Get_Bundle_Size(clsBundleSize objReq)
        {
            var objResp = new List<clsBundleSize>();
            objResp = _DALProduction.Fn_Get_Bundle_Size(objReq);
            return objResp;
        }

        #endregion End Size- Bundle 8-Feb-2026

        #region Start Color- Bundle 8-Feb-2026

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Production/Fn_Insert_Bundle_Color")]
        public clsBundleColor Fn_Insert_Bundle_Color(clsBundleColor objReq)
        {
            var objResp = new clsBundleColor();
            objResp = _DALProduction.Fn_Insert_Bundle_Color(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Production/Fn_Delete_Bundle_Color")]
        public clsBundleColor Fn_Delete_Bundle_Color(clsBundleColor objReq)
        {
            var objResp = new clsBundleColor();
            objResp = _DALProduction.Fn_Delete_Bundle_Color(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Production/Fn_Get_Bundle_Color")]
        public List<clsBundleColor> Fn_Get_Bundle_Color(clsBundleColor objReq)
        {
            var objResp = new List<clsBundleColor>();
            objResp = _DALProduction.Fn_Get_Bundle_Color(objReq);
            return objResp;
        }

        #endregion End Color- Bundle 8-Feb-2026

        #region Start Shade- Bundle 8-Feb-2026

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Production/Fn_Insert_Bundle_Shade")]
        public clsBundleShade Fn_Insert_Bundle_Shade(clsBundleShade objReq)
        {
            var objResp = new clsBundleShade();
            objResp = _DALProduction.Fn_Insert_Bundle_Shade(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Production/Fn_Delete_Bundle_Shade")]
        public clsBundleShade Fn_Delete_Bundle_Shade(clsBundleShade objReq)
        {
            var objResp = new clsBundleShade();
            objResp = _DALProduction.Fn_Delete_Bundle_Shade(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Production/Fn_Get_Bundle_Shade")]
        public List<clsBundleShade> Fn_Get_Bundle_Shade(clsBundleShade objReq)
        {
            var objResp = new List<clsBundleShade>();
            objResp = _DALProduction.Fn_Get_Bundle_Shade(objReq);
            return objResp;
        }

        #endregion End Shade- Bundle 8-Feb-2026

        #region Start Compile- Bundle 8-Feb-2026

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Production/Fn_Insert_Bundle_Compile")]
        public clsBundleCompile Fn_Insert_Bundle_Compile(clsBundleCompile objReq)
        {
            var objResp = new clsBundleCompile();
            objResp = _DALProduction.Fn_Insert_Bundle_Compile(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Production/Fn_Delete_Bundle_Compile")]
        public clsBundleCompile Fn_Delete_Bundle_Compile(clsBundleCompile objReq)
        {
            var objResp = new clsBundleCompile();
            objResp = _DALProduction.Fn_Delete_Bundle_Compile(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Production/Fn_Get_Bundle_Compile")]
        public List<clsBundleCompile> Fn_Get_Bundle_Compile(clsBundleCompile objReq)
        {
            var objResp = new List<clsBundleCompile>();
            objResp = _DALProduction.Fn_Get_Bundle_Compile(objReq);
            return objResp;
        }

        #endregion End Compile- Bundle 8-Feb-2026

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Production/Fn_Get_Order_SizeName")]
        public List<clsSizeMaster> Fn_Get_Order_SizeName(clsSizeMaster objReq)
        {
            var objResp = new List<clsSizeMaster>();
            objResp = _DALProduction.Fn_Get_Order_SizeName(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Production/Fn_Get_Order_Color")]
        public List<clsBundleColor> Fn_Get_Order_Color(clsBundleColor objReq)
        {
            var objResp = new List<clsBundleColor>();
            objResp = _DALProduction.Fn_Get_Order_Color(objReq);
            return objResp;
        }

        #region Start Sectionwise compile data for QR 23-FEB-2026

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Production/Fn_Get_SectionWis_Compile_QR_Data")]
        public List<clsBundleCompile> Fn_Get_SectionWis_Compile_QR_Data(clsBundleCompile objReq)
        {
            var objResp = new List<clsBundleCompile>();
            objResp = _DALProduction.Fn_Get_SectionWis_Compile_QR_Data(objReq);
            return objResp;
        }

        #endregion End Sectionwise compile data for QR 23-FEB-2026
    }
}