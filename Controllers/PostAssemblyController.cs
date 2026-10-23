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
    public class PostAssemblyController : ApiController
    {
        // GET: PostAssembly

        DALPostAssembly _DALPostAssembly = new DALPostAssembly();

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/PostAssembly/Fn_Insert_PostAssemblySinglePcsAssign")]
        public clsPostAssembly Fn_Insert_PostAssemblySinglePcsAssign(clsPostAssembly objReq)
        {
            var objResp = new clsPostAssembly();
            objResp = _DALPostAssembly.Fn_Insert_PostAssemblySinglePcsAssign(objReq);
            return objResp;
        }


        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/PostAssembly/Fn_Fetch_OperatorIDWisePostAssemOpNo")]
        public List<clsPostAssembly> Fn_Fetch_OperatorIDWisePostAssemOpNo(clsPostAssembly objReq)
        {
            var objResp = new List<clsPostAssembly>();
            objResp = _DALPostAssembly.Fn_Fetch_OperatorIDWisePostAssemOpNo(objReq);
            return objResp;
        }



    }
}