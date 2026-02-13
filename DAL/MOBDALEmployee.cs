using BSLDaman.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;


namespace BSLDaman.DAL
{
    public class MOBDALEmployee
    {

        SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["BSL"].ConnectionString);


        public clsEmployee Fn_Insert_Employee(clsEmployee objReq)
        {
            var objResp = new clsEmployee();
            try
            {
                if (objReq.nEmpId == null || objReq.nEmpId == 0)
                {
                    objResp.vErrorMsg = "Please Enter an Employee ID";
                    objResp.vErrorCode = 300;
                }
                else if (String.IsNullOrWhiteSpace(objReq.vEmpName))
                {
                    objResp.vErrorMsg = "Please Enter the Employee Name";
                    objResp.vErrorCode = 300;
                }
                else if (String.IsNullOrWhiteSpace(objReq.EmpGender))
                {
                    objResp.vErrorMsg = "Please Select the Gender";
                    objResp.vErrorCode = 300;
                }
                else if (String.IsNullOrWhiteSpace(objReq.vEmpGrade))
                {
                    objResp.vErrorMsg = "Please Enter the Grade";
                    objResp.vErrorCode = 300;
                }
                else if (String.IsNullOrWhiteSpace(objReq.vEmpPassword))
                {
                    objResp.vErrorMsg = "Please Enter the Password";
                    objResp.vErrorCode = 300;
                }
                else if (String.IsNullOrWhiteSpace(objReq.OperatorType))
                {
                    objResp.vErrorMsg = "Please Enter the Operator Type";
                    objResp.vErrorCode = 300;
                }
                else if (String.IsNullOrWhiteSpace(objReq.vEmpMobile))
                {
                    objResp.vErrorMsg = "Please Enter the Mobile Number";
                    objResp.vErrorCode = 300;
                }
                else if (String.IsNullOrWhiteSpace(objReq.EmpRole))
                {
                    objResp.vErrorMsg = "Please Enter the Employee Role";
                    objResp.vErrorCode = 300;
                }
                else if (String.IsNullOrWhiteSpace(objReq.vEmpLocation))
                {
                    objResp.vErrorMsg = "Please Enter the Employee Location";
                    objResp.vErrorCode = 300;
                }
                else if (String.IsNullOrWhiteSpace(objReq.DeviceId))
                {
                    objResp.vErrorMsg = "Please Enter the Device ID";
                    objResp.vErrorCode = 300;
                }
                else
                {
                    if (Con.State == ConnectionState.Broken)
                    { Con.Close(); }
                    if (Con.State == ConnectionState.Closed)
                    { Con.Open(); }

                    string encryptPassword = Generic.EncryptText(objReq.vEmpPassword);
                    SqlCommand cmd = new SqlCommand("USP_EmployeeMob", Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmpId", objReq.nEmpId);
                    cmd.Parameters.AddWithValue("@EmpName", objReq.vEmpName);
                    cmd.Parameters.AddWithValue("@EmpGender", objReq.EmpGender);
                    cmd.Parameters.AddWithValue("@EmpGrade", objReq.vEmpGrade);
                    cmd.Parameters.AddWithValue("@EmpPassword", encryptPassword);
                    cmd.Parameters.AddWithValue("@OperatorType", objReq.OperatorType);
                    cmd.Parameters.AddWithValue("@EmpMobile", objReq.vEmpMobile);
                    cmd.Parameters.AddWithValue("@EmpRole", objReq.EmpRole);
                    cmd.Parameters.AddWithValue("@EmpLocation", objReq.vEmpLocation);
                    cmd.Parameters.AddWithValue("@DeviceId", objReq.DeviceId);
                    cmd.Parameters.AddWithValue("@QueryType", "Insert");

                    int i = 0;
                    i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        objResp.vErrorMsg = "Success";
                        objResp.vErrorCode = 200;
                    }
                    else
                    {
                        objResp.vErrorMsg = "Employee Registration Failed.";
                        objResp.vErrorCode = 300;
                    }
                }
            }
            catch (Exception exp)
            {
                Logger.WriteLog("Function Name : Fn_Insert_Employee", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
                objResp.vErrorCode = 500;
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }



        public clsEmployee Fn_Login_Employee(clsEmployee objReq)
        {
            var objResp = new clsEmployee();
            try
            {
                if (objReq.nEmpId == null || objReq.nEmpId == 0)
                {
                    objResp.vErrorMsg = "Please Enter an Employee ID";
                    objResp.vErrorCode = 300;
                }
                else if (String.IsNullOrWhiteSpace(objReq.vEmpPassword))
                {
                    objResp.vErrorMsg = "Please Enter the Password";
                    objResp.vErrorCode = 300;
                }
                else
                {
                    if (Con.State == ConnectionState.Broken)
                    { Con.Close(); }
                    if (Con.State == ConnectionState.Closed)
                    { Con.Open(); }

                    string encryptPassword = Generic.EncryptText(objReq.vEmpPassword);

                    SqlCommand cmd = new SqlCommand("USP_EmployeeMob", Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmpId", objReq.nEmpId);
                    cmd.Parameters.AddWithValue("@EmpPassword", encryptPassword);
                    cmd.Parameters.AddWithValue("@QueryType", "LogIn");

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    int i = 0;

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        string decryptPassword = Generic.DecryptText(Convert.ToString(ds.Tables[0].Rows[i]["EmpPassword"]));
                        objResp.nEmpId = Convert.ToInt32(ds.Tables[0].Rows[i]["EmpId"]);
                        objResp.vEmpName = Convert.ToString(ds.Tables[0].Rows[i]["EmpName"]);
                        objResp.EmpRole = Convert.ToString(ds.Tables[0].Rows[i]["EmpRole"]);
                        objResp.DeviceId = Convert.ToString(ds.Tables[0].Rows[i]["DeviceId"]);
                        objResp.vEmpPassword = decryptPassword;

                        objResp.vErrorMsg = "Success";
                        objResp.vErrorCode = 200;
                    }
                    else
                    {
                        objResp.vErrorMsg = "Entered Credentials has been invalid.";
                        objResp.vErrorCode = 300;
                    }
                }
            }
            catch (Exception exp)
            {
                Logger.WriteLog("Function Name : Fn_Login_Employee", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
                objResp.vErrorCode = 500;
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }


        public clsEmployee Fn_Fetch_EmployeeDetail_ById(clsEmployee objReq)
        {
            var objResp = new clsEmployee();
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_EmployeeMob", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmpId", objReq.nEmpId);
                cmd.Parameters.AddWithValue("@QueryType", "FetchDetailsById");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    objResp.nEmpId = Convert.ToInt32(ds.Tables[0].Rows[0]["EmpId"]);
                    objResp.vEmpName = Convert.ToString(ds.Tables[0].Rows[0]["EmpName"]);
                    objResp.vEmpMobile = Convert.ToString(ds.Tables[0].Rows[0]["EmpMobile"]);
                    objResp.EmpRole = Convert.ToString(ds.Tables[0].Rows[0]["EmpRole"]);
                    objResp.vEmpGrade = Convert.ToString(ds.Tables[0].Rows[0]["EmpGrade"]);
                    objResp.OperatorType = Convert.ToString(ds.Tables[0].Rows[0]["OperatorType"]);
                    objResp.EmpGender = Convert.ToString(ds.Tables[0].Rows[0]["EmpGender"]);
                    objResp.DeviceId = Convert.ToString(ds.Tables[0].Rows[0]["DeviceId"]);

                    objResp.vErrorMsg = "Success";
                    objResp.vErrorCode = 200;
                }
                else
                {
                    objResp.vErrorMsg = "Employee details not found.";
                    objResp.vErrorCode = 400;
                }
            }
            catch (Exception exp)
            {
                Logger.WriteLog("Function Name : Fn_Fetch_EmployeeDetail_ById", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
                objResp.vErrorCode = 500;
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }



    }
}