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
        public clsMOBEmployee Fn_Login_Employee(clsMOBEmployee objReq)
        {
            var objResp = new clsMOBEmployee();
            objResp = _MOBDALEmployee.Fn_Login_Employee(objReq);
            return objResp;
        }


        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/MOBEmployee/Fn_Fetch_EmployeeDetail_ById")]
        public clsMOBEmployee Fn_Fetch_EmployeeDetail_ById(clsMOBEmployee objReq)
        {
            var objResp = new clsMOBEmployee();
            objResp = _MOBDALEmployee.Fn_Fetch_EmployeeDetail_ById(objReq);
            return objResp;
        }


        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/MOBEmployee/Fn_LogOut_EmployeeSession")]
        public clsMOBEmployee Fn_LogOut_EmployeeSession(clsMOBEmployee objReq)
        {
            var objResp = new clsMOBEmployee();
            objResp = _MOBDALEmployee.Fn_LogOut_EmployeeSession(objReq);
            return objResp;
        }


    }
}