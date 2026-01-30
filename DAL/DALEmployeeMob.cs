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
    public class DALEmployeeMob
    {

        SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["BSL"].ConnectionString);
        

        public clsEmployee Fn_Login_Employee(clsEmployee objReq)
        {
            var objResp = new clsEmployee();
            try
            {
                if (objReq.nEmpId == null || objReq.nEmpId == 0)
                {
                    objResp.vErrorMsg = "Please Enter an Employee ID";
                }
                else if (String.IsNullOrWhiteSpace(objReq.vEmpPassword))
                {
                    objResp.vErrorMsg = "Please Enter the Password";
                }
                else
                {
                    if (Con.State == ConnectionState.Broken)
                    { Con.Close(); }
                    if (Con.State == ConnectionState.Closed)
                    { Con.Open(); }

                    string encryptPassword = Generic.EncryptText(objReq.vEmpPassword);

                    SqlCommand cmd = new SqlCommand("", Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("", objReq.nEmpId);
                    cmd.Parameters.AddWithValue("", encryptPassword);
                    cmd.Parameters.AddWithValue("", "");

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    int i = 0;
                    
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        string decryptPassword = Generic.DecryptText(Convert.ToString(ds.Tables[0].Rows[i]["EmpPassword"]));
                        objResp.nEmpId = Convert.ToInt32(ds.Tables[0].Rows[i][""]);
                        objResp.EmpRole = Convert.ToString(ds.Tables[0].Rows[i][""]);
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

                SqlCommand cmd = new SqlCommand("", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("", objReq.nEmpId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    objResp.nEmpId = Convert.ToInt32(ds.Tables[0].Rows[0][""]);
                    objResp.vEmpName = Convert.ToString(ds.Tables[0].Rows[0][""]);
                    objResp.vEmpMobile = Convert.ToString(ds.Tables[0].Rows[0][""]);
                    objResp.EmpRole = Convert.ToString(ds.Tables[0].Rows[0][""]);
                    objResp.vEmpGrade = Convert.ToString(ds.Tables[0].Rows[0][""]);
                    objResp.OperatorType = Convert.ToString(ds.Tables[0].Rows[0][""]);
                    objResp.EmpGender = Convert.ToString(ds.Tables[0].Rows[0][""]);

                    objResp.vErrorMsg = "Success";
                }
                else
                {
                    objResp.vErrorMsg = "Employee details not found.";
                }
            }
            catch (Exception exp)
            {
                Logger.WriteLog("Function Name : Fn_Fetch_EmployeeDetail_ById", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
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