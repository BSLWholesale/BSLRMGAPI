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

            IEnumerable<string> headervalues;
            string tokenid = "";

            if (Request.Headers.TryGetValues("TokenId", out headervalues))
            {
                tokenid = headervalues.FirstOrDefault();
            }
            else
            {
                objResp.vErrorMsg = "Token ID is missing in header";
                objResp.vErrorCode = 401;
                return objResp;
            }

            objResp = _MOBDALEmployee.Fn_Fetch_EmployeeDetail_ById(objReq, tokenid);
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


        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/MOBEmployee/Fn_Check_EmployeeTokenID")]
        public clsMOBEmployee Fn_Check_EmployeeTokenID(clsMOBEmployee objReq)
        {
            var objResp = new clsMOBEmployee();
            IEnumerable<string> headervalues;
            string tokenid = "";
            if (Request.Headers.TryGetValues("TokenId", out headervalues))
            {
                tokenid = headervalues.FirstOrDefault();
            }
            else
            {
                objResp.vErrorMsg = "Token ID is missing in header.";
                objResp.vErrorCode = 401;
                return objResp;
            }

            objResp = _MOBDALEmployee.Fn_Check_EmployeeTokenID(objReq, tokenid);
            return objResp;
        }



        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/MOBEmployee/Fn_Get_All_EmployeeList")]
        public List<clsMOBEmployee> Fn_Get_All_EmployeeList(clsMOBEmployee objReq)
        {
            var objResp = new List<clsMOBEmployee>();
            objResp = _MOBDALEmployee.Fn_Get_All_EmployeeList(objReq);
            return objResp;
        }


        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/MOBEmployee/Fn_Get_All_OperatorDetails")]
        public List<clsMOBEmployee> Fn_Get_All_OperatorDetails(Int64? nEmpId = null)
        {
            clsMOBEmployee objReq = new clsMOBEmployee();

            if (nEmpId.HasValue)
            {
                objReq.nEmpId = nEmpId.Value;
            }
            else
            {
                objReq.nEmpId = 0;
            }

            var objResp = _MOBDALEmployee.Fn_Get_All_OperatorDetails(objReq);
            return objResp;
        }


        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/MOBEmployee/Fn_Get_OperatorCount")]
        public List<clsMOBEmployee> Fn_Get_OperatorCount(clsMOBEmployee objReq)
        {
            var objResp = new List<clsMOBEmployee>();
            objResp = _MOBDALEmployee.Fn_Get_OperatorCount(objReq);
            return objResp;
        }


        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/MOBEmployee/Fn_Get_All_SupervisorDetails")]
        public List<clsMOBEmployee> Fn_Get_All_SupervisorDetails(Int64? nEmpId = null)
        {
            clsMOBEmployee objReq = new clsMOBEmployee();

            if (nEmpId.HasValue)
            {
                objReq.nEmpId = nEmpId.Value;
            }
            else
            {
                objReq.nEmpId = 0;
            }

            var objResp = _MOBDALEmployee.Fn_Get_All_SupervisorDetails(objReq);
            return objResp;
        }



    }
}