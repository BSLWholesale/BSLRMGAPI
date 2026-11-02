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
    public class CompileController : ApiController
    {
        // GET: Compile

        DALCompile _DALCompile = new DALCompile();

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Compile/Fn_Insert_Bundle_Size")]
        public clsBundleSize Fn_Insert_Bundle_Size(clsBundleSize objReq)
        {
            var objResp = new clsBundleSize();
            objResp = _DALCompile.Fn_Insert_Bundle_Size(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Compile/Fn_Insert_Bundle_Color")]
        public clsBundleColor Fn_Insert_Bundle_Color(clsBundleColor objReq)
        {
            var objResp = new clsBundleColor();
            objResp = _DALCompile.Fn_Insert_Bundle_Color(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Compile/Fn_Insert_Bundle_Layer")]
        public clsBundleLayerMaster Fn_Insert_Bundle_Layer(clsBundleLayerMaster objReq)
        {
            var objResp = new clsBundleLayerMaster();
            objResp = _DALCompile.Fn_Insert_Bundle_Layer(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Compile/Fn_Delete_Bundle_Layer")]
        public clsBundleLayerMaster Fn_Delete_Bundle_Layer(clsBundleLayerMaster objReq)
        {
            var objResp = new clsBundleLayerMaster();
            objResp = _DALCompile.Fn_Delete_Bundle_Layer(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Compile/Fn_Get_Bundle_Layer")]
        public List<clsBundleLayerMaster> Fn_Get_Bundle_Layer(clsBundleLayerMaster objReq)
        {
            var objResp = new List<clsBundleLayerMaster>();
            objResp = _DALCompile.Fn_Get_Bundle_Layer(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Compile/Fn_Delete_Bundle_Size")]
        public clsBundleSize Fn_Delete_Bundle_Size(clsBundleSize objReq)
        {
            var objResp = new clsBundleSize();
            objResp = _DALCompile.Fn_Delete_Bundle_Size(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Compile/Fn_Get_Bundle_Size")]
        public List<clsBundleSize> Fn_Get_Bundle_Size(clsBundleSize objReq)
        {
            var objResp = new List<clsBundleSize>();
            objResp = _DALCompile.Fn_Get_Bundle_Size(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Compile/Fn_Get_Bundle_Color")]
        public List<clsBundleColor> Fn_Get_Bundle_Color(clsBundleColor objReq)
        {
            var objResp = new List<clsBundleColor>();
            objResp = _DALCompile.Fn_Get_Bundle_Color(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Compile/Fn_Delete_Bundle_Color")]
        public clsBundleColor Fn_Delete_Bundle_Color(clsBundleColor objReq)
        {
            var objResp = new clsBundleColor();
            objResp = _DALCompile.Fn_Delete_Bundle_Color(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Compile/Fn_Insert_Bundle_Shade")]
        public clsBundleShade Fn_Insert_Bundle_Shade(clsBundleShade objReq)
        {
            var objResp = new clsBundleShade();
            objResp = _DALCompile.Fn_Insert_Bundle_Shade(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Compile/Fn_Get_Bundle_Shade")]
        public List<clsBundleShade> Fn_Get_Bundle_Shade(clsBundleShade objReq)
        {
            var objResp = new List<clsBundleShade>();
            objResp = _DALCompile.Fn_Get_Bundle_Shade(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Compile/Fn_Delete_Bundle_Shade")]
        public clsBundleShade Fn_Delete_Bundle_Shade(clsBundleShade objReq)
        {
            var objResp = new clsBundleShade();
            objResp = _DALCompile.Fn_Delete_Bundle_Shade(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Compile/Fn_Insert_Bundle_Compile")]
        public clsBundleCompile Fn_Insert_Bundle_Compile(clsBundleCompile objReq)
        {
            var objResp = new clsBundleCompile();
            objResp = _DALCompile.Fn_Insert_Bundle_Compile(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Compile/Fn_Get_Bundle_Compile")]
        public List<clsBundleCompile> Fn_Get_Bundle_Compile(clsBundleCompile objReq)
        {
            var objResp = new List<clsBundleCompile>();
            objResp = _DALCompile.Fn_Get_Bundle_Compile(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Compile/Fn_Get_SectionWis_Compile_QR_Data")]
        public List<clsBundleCompile> Fn_Get_SectionWis_Compile_QR_Data(clsBundleCompile objReq)
        {
            var objResp = new List<clsBundleCompile>();
            objResp = _DALCompile.Fn_Get_SectionWis_Compile_QR_Data(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Compile/Fn_Get_Subsection_List")]
        public List<clsBundleCompile> Fn_Get_Subsection_List(clsBundleCompile objReq)
        {
            var objResp = new List<clsBundleCompile>();
            objResp = _DALCompile.Fn_Get_Subsection_List(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Compile/Fn_Get_Sum_Laywise_Plies")]
        public clsBundleShade Fn_Get_Sum_Laywise_Plies(clsBundleShade objReq)
        {
            var objResp = new clsBundleShade();
            objResp = _DALCompile.Fn_Get_Sum_Laywise_Plies(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Compile/Fn_Update_Print_layarDate")]
        public clsBundleLayerMaster Fn_Update_Print_layarDate(clsBundleLayerMaster objReq)
        {
            var objResp = new clsBundleLayerMaster();
            objResp = _DALCompile.Fn_Update_Print_layarDate(objReq);
            return objResp;
        }
        
    }
}