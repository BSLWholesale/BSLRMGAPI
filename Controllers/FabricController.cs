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
    public class FabricController : ApiController
    {
        // GET: Fabric

        DALFabric _DALFabric = new DALFabric();

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Fabric/Fn_Insert_Order_Master")]
        public FabricInhouse Fn_Upload_Fabirc_Inhouse(FabricInhouse objReq)
        {
            var objResp = new FabricInhouse();
            objResp = _DALFabric.Fn_Upload_Fabirc_Inhouse(objReq);
            return objResp;
        }
    }
}