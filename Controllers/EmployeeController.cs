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
    public class EmployeeController : ApiController
    {
        // GET: Employee
        DALEmployee _DALEmployee = new DALEmployee();

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Employee/Fn_Add_Employee_Detail")]
        public clsEmployeeDetail Fn_Add_Employee_Detail(clsEmployeeDetail objReq)
        {
            var objResp = new clsEmployeeDetail();
            objResp = _DALEmployee.Fn_Add_Employee_Detail(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("api/Employee/Fn_Update_Employee_Detail")]
        public clsEmployeeDetail Fn_Update_Employee_Detail(clsEmployeeDetail objReq)
        {
            var objResp = new clsEmployeeDetail();
            objResp = _DALEmployee.Fn_Update_Employee_Detail(objReq);
            return objResp;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Employee/Fn_Get_All_Employee_Detail")]
        public List<clsEmployeeDetail> Fn_Get_All_Employee_Detail(clsEmployeeDetail objReq)
        {
            var objResp = new List<clsEmployeeDetail>();
            objResp = _DALEmployee.Fn_Get_All_Employee_Detail(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Employee/Fn_Delete_Employee_Detail")]
        public clsEmployeeDetail Fn_Delete_Employee_Detail(clsEmployeeDetail objReq)
        {
            var objResp = new clsEmployeeDetail();
            objResp = _DALEmployee.Fn_Delete_Employee_Detail(objReq);
            return objResp;
        }

        #region Start For Color 

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Employee/Fn_Add_Color")]
        public clsColorMaster Fn_Add_Color(clsColorMaster objReq)
        {
            var objResp = new clsColorMaster();
            objResp = _DALEmployee.Fn_Add_Color(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("api/Employee/Fn_Update_Color")]
        public clsColorMaster Fn_Update_Color(clsColorMaster objReq)
        {
            var objResp = new clsColorMaster();
            objResp = _DALEmployee.Fn_Update_Color(objReq);
            return objResp;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Employee/Fn_Get_Color_List")]
        public List<clsColorMaster> Fn_Get_Color_List(clsColorMaster objReq)
        {
            var objResp = new List<clsColorMaster>();
            objResp = _DALEmployee.Fn_Get_Color_List(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Employee/Fn_Delete_Color")]
        public clsColorMaster Fn_Delete_Color(clsColorMaster objReq)
        {
            var objResp = new clsColorMaster();
            objResp = _DALEmployee.Fn_Delete_Color(objReq);
            return objResp;
        }

        #endregion

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Employee/Fn_LogIn_Employee")]
        public clsEmployee Fn_LogIn_Employee(clsEmployee objReq)
        {
            var objResp = new clsEmployee();
            objResp = _DALEmployee.Fn_LogIn_Employee(objReq);
            return objResp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Employee/Fn_Fill_DropdownList")]
        public List<clsResponseDropdown> Fn_Fill_DropdownList(clsRequestDropdown objReq)
        {
            var objResp = new List<clsResponseDropdown>();
            objResp = _DALEmployee.Fn_Fill_DropdownList(objReq);
            return objResp;
        }


        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Employee/Fn_Get_DashboardAttendenceEmpOperatorCount")]
        public List<clsDashboardEmployeeCount> Fn_Get_DashboardAttendenceEmpOperatorCount(clsDashboardEmployeeCount objReq)
        {
            var objResp = new List<clsDashboardEmployeeCount>();
            objResp = _DALEmployee.Fn_Get_DashboardAttendenceEmpOperatorCount(objReq);
            return objResp;
        }


        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Employee/Fn_Employee_BirthdayWishes")]
        public clsEmployee Fn_Employee_BirthdayWishes(clsEmployee objReq)
        {
            var objResp = new clsEmployee();
            objResp = _DALEmployee.Fn_Employee_BirthdayWishes(objReq);
            return objResp;
        }




    }
}