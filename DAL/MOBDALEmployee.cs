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

        public clsMOBEmployee Fn_Login_Employee(clsMOBEmployee objReq)
        {
            var objResp = new clsMOBEmployee();
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
                        objResp.TokenId = Convert.ToString(ds.Tables[0].Rows[i]["TokenId"]);
                        objResp.vEmpPassword = decryptPassword;

                        if (objResp.TokenId == null || objResp.TokenId == "")
                        {
                            objResp.TokenId = Fn_Generate_EmployeeTokenId(Convert.ToInt64(objReq.nEmpId), objResp.TokenId);
                        }
                        else
                        {
                            objResp.TokenId = Convert.ToString(ds.Tables[0].Rows[i]["TokenId"]);
                        }

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


        public clsMOBEmployee Fn_Fetch_EmployeeDetail_ById(clsMOBEmployee objReq)
        {
            var objResp = new clsMOBEmployee();
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
                    objResp.TokenId = Convert.ToString(ds.Tables[0].Rows[0]["TokenId"]);

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


        public string Fn_Generate_EmployeeTokenId(Int64 EmpId, string TokenId)
        {
            var objResp = new clsMOBEmployee();
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                Random random = new Random();
                var randomNumber = random.Next(1, 1000001);

                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                var stringChars = new char[15];
                for (int j = 0; j < stringChars.Length; j++)
                {
                    stringChars[j] = chars[random.Next(chars.Length)];
                }

                string finalString = new String(stringChars);

                TokenId = finalString;

                SqlCommand cmd = new SqlCommand("USP_EmployeeMob", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmpId", EmpId);
                cmd.Parameters.AddWithValue("@TokenId", TokenId);
                cmd.Parameters.AddWithValue("@QueryType", "UpdateTokenId");

                int i = 0;
                i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    objResp.vErrorMsg = "Success";
                    objResp.vErrorCode = 200;                     
                }
                else
                {
                    objResp.vErrorMsg = "Employee token genration failed.";
                    objResp.vErrorCode = 300;
                }
            }
            catch (Exception exp)
            {
                Logger.WriteLog("Function Name : Fn_Generate_EmployeeTokenId", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
                objResp.vErrorCode = 500;
            }
            finally
            {
                Con.Close();
            }
            return TokenId;
        }


        public clsMOBEmployee Fn_LogOut_EmployeeSession(clsMOBEmployee objReq)
        {
            var objResp = new clsMOBEmployee();
            try
            {
                if (objReq.nEmpId == null || objReq.nEmpId == 0)
                {
                    objResp.vErrorMsg = "Please Pass the Employee ID";
                    objResp.vErrorCode = 300;
                }
                else
                {
                    if (Con.State == ConnectionState.Broken)
                    { Con.Close(); }
                    if (Con.State == ConnectionState.Closed)
                    { Con.Open(); }

                    SqlCommand cmd = new SqlCommand("USP_EmployeeMob", Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmpId", objReq.nEmpId);
                    cmd.Parameters.AddWithValue("@QueryType", "LogoutTokenIdUpdate");

                    int i = 0;
                    i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        objResp.vErrorCode = 200;
                        objResp.vErrorMsg = "Success";
                    }
                    else
                    {
                        objResp.vErrorCode = 400;
                        objResp.vErrorMsg = "Logout Failed.";
                    }
                }
            }
            catch (Exception exp)
            {
                objResp.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_LogOut_EmployeeSession", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }



    }
}