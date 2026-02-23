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
    public class MOBEmployeeController : ApiController
    {
        // GET: MOBEmployee

        MOBDALEmployee _MOBDALEmployee = new MOBDALEmployee();


        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/MOBEmployee/Fn_Login_Employee")]
        public clsEmployee Fn_Login_Employee(clsEmployee objReq)
        {
            var objResp = new clsEmployee();
            objResp = _MOBDALEmployee.Fn_Login_Employee(objReq);
            return objResp;
        }


        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/MOBEmployee/Fn_Fetch_EmployeeDetail_ById")]
        public clsEmployee Fn_Fetch_EmployeeDetail_ById(clsEmployee objReq)
        {
            var objResp = new clsEmployee();
            objResp = _MOBDALEmployee.Fn_Fetch_EmployeeDetail_ById(objReq);
            return objResp;
        }



    }
}