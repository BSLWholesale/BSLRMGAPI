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
    public class EfficiencyController : ApiController
    {
        // GET: Efficiency

        DALEfficiency _DALEfficiency = new DALEfficiency();

        #region Start Fn_Get_EmployeeWiseEfficiency 07-OCT-2026

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Efficiency/Fn_Get_EmployeeWiseEfficiency")]
        public List<clsEmployeeWiseEfficiency> Fn_Get_EmployeeWiseEfficiency(clsEfficiencyReq objReq)
        {
            var objResp = new List<clsEmployeeWiseEfficiency>();
            objResp = _DALEfficiency.Fn_Get_EmployeeWiseEfficiency(objReq);
            return objResp;
        }

        #endregion End Fn_Get_EmployeeWiseEfficiency 07-OCT-2026
    }
}