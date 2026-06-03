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
            var objEmp = new clsMOBEmployee();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Login_Employee");
            try
            {
                if (objReq.nEmpId == null)
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

                        if (ds.Tables[0].Rows[0]["CreatedOn"] == null)
                        {
                            objResp.CreatedOn = string.Empty;
                        }
                        else
                        {
                            objResp.CreatedOn = Convert.ToString(ds.Tables[0].Rows[0]["CreatedOn"]);
                        }

                        if (ds.Tables[0].Rows[0]["CreatedBy"] == DBNull.Value)
                        {
                            objResp.CreatedBy = 0;
                        }
                        else
                        {
                            objResp.CreatedBy = Convert.ToInt32(ds.Tables[0].Rows[0]["CreatedBy"]);
                        }

                        if (ds.Tables[0].Rows[0]["ModifiedOn"] == null)
                        {
                            objResp.ModifiedOn = string.Empty;
                        }
                        else
                        {
                            objResp.ModifiedOn = Convert.ToString(ds.Tables[0].Rows[0]["ModifiedOn"]);
                        }

                        if (ds.Tables[0].Rows[0]["ModifiedBy"] == DBNull.Value)
                        {
                            objResp.ModifiedBy = 0;
                        }
                        else
                        {
                            objResp.ModifiedBy = Convert.ToInt32(ds.Tables[0].Rows[0]["ModifiedBy"]);
                        }

                        objResp.IsActive = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsActive"]);

                        if (ds.Tables[0].Rows[i]["DOB"] == DBNull.Value)
                        {
                            objResp.DOB = string.Empty;
                            objResp.IsBirthdayWishes = false;
                        }
                        else
                        {
                            objResp.DOB = Convert.ToString(ds.Tables[0].Rows[i]["DOB"]);
                            objResp.IsBirthdayWishes = true;

                        }

                        if (ds.Tables[0].Rows[i]["DOJ"] == DBNull.Value)
                        {
                            objResp.DOJ = string.Empty;
                            objResp.IsWorkAnniversaryWishes = false;
                        }
                        else
                        {
                            objResp.DOJ = Convert.ToString(ds.Tables[0].Rows[i]["DOJ"]);
                            objResp.IsWorkAnniversaryWishes = true;
                        }

                        if (objResp.IsActive == true)
                        {
                            objResp.vErrorMsg = "Success";
                            objResp.vErrorCode = 200;
                        }
                        else
                        {
                            objResp.vErrorMsg = "Operator/Employee ID is Inactive.";
                            objResp.vErrorCode = 300;
                        }
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
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Login_Employee");
            return objResp;
        }


        public clsMOBEmployee Fn_Fetch_EmployeeDetail_ById(clsMOBEmployee objReq, string tokenid)
        {
            var objResp = new clsMOBEmployee();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Fetch_EmployeeDetail_ById");
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmdToken = new SqlCommand("USP_EmployeeMob", Con);
                cmdToken.CommandType = CommandType.StoredProcedure;
                cmdToken.Parameters.AddWithValue("@EmpId", objReq.nEmpId);
                cmdToken.Parameters.AddWithValue("@TokenId", tokenid);
                cmdToken.Parameters.AddWithValue("@QueryType", "ValidateTokenID");

                object result = cmdToken.ExecuteScalar();

                if (result == null)
                {
                    objResp.vErrorMsg = "Invalid Token ID or Expired Token ID.";
                    objResp.vErrorCode = 401;
                    return objResp;
                }

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
                    
                    if (ds.Tables[0].Rows[0]["EmpMobile"] == null)
                    {
                        objResp.vEmpMobile = string.Empty;
                    }
                    else
                    {
                        objResp.vEmpMobile = Convert.ToString(ds.Tables[0].Rows[0]["EmpMobile"]);
                    }
                    
                    objResp.EmpRole = Convert.ToString(ds.Tables[0].Rows[0]["EmpRole"]);
                    
                    if (ds.Tables[0].Rows[0]["EmpGrade"] == null)
                    {
                        objResp.vEmpGrade = string.Empty;
                    }
                    else
                    {
                        objResp.vEmpGrade = Convert.ToString(ds.Tables[0].Rows[0]["EmpGrade"]);
                    }

                    objResp.EmpGender = Convert.ToString(ds.Tables[0].Rows[0]["EmpGender"]);
                    objResp.DeviceId = Convert.ToString(ds.Tables[0].Rows[0]["DeviceId"]);

                    if (ds.Tables[0].Rows[0]["TokenId"] == null)
                    {
                        objResp.TokenId = string.Empty;
                    }
                    else
                    {
                        objResp.TokenId = Convert.ToString(ds.Tables[0].Rows[0]["TokenId"]);
                    }

                    if (ds.Tables[0].Rows[0]["LineName"] == null)
                    {
                        objResp.LineName = string.Empty;
                    }
                    else
                    {
                        objResp.LineName = Convert.ToString(ds.Tables[0].Rows[0]["LineName"]);
                    }

                    if (ds.Tables[0].Rows[0]["Units"] == null)
                    {
                        objResp.Units = string.Empty;
                    }
                    else
                    {
                        objResp.Units = Convert.ToString(ds.Tables[0].Rows[0]["Units"]);
                    }

                    if (ds.Tables[0].Rows[0]["EmpLocation"] == null)
                    {
                        objResp.EmpLocation = string.Empty;
                    }
                    else
                    {
                        objResp.EmpLocation = Convert.ToString(ds.Tables[0].Rows[0]["EmpLocation"]);
                    }

                    if (ds.Tables[0].Rows[0]["CreatedOn"] == null)
                    {
                        objResp.CreatedOn = string.Empty;
                    }
                    else
                    {
                        objResp.CreatedOn = Convert.ToString(ds.Tables[0].Rows[0]["CreatedOn"]);
                    }

                    if (ds.Tables[0].Rows[0]["CreatedBy"] == DBNull.Value)
                    {
                        objResp.CreatedBy = 0;
                    }
                    else
                    {
                        objResp.CreatedBy = Convert.ToInt32(ds.Tables[0].Rows[0]["CreatedBy"]);
                    }

                    if (ds.Tables[0].Rows[0]["ModifiedOn"] == null)
                    {
                        objResp.ModifiedOn = string.Empty;
                    }
                    else
                    {
                        objResp.ModifiedOn = Convert.ToString(ds.Tables[0].Rows[0]["ModifiedOn"]);
                    }

                    if (ds.Tables[0].Rows[0]["ModifiedBy"] == DBNull.Value)
                    {
                        objResp.ModifiedBy = 0;
                    }
                    else
                    {
                        objResp.ModifiedBy = Convert.ToInt32(ds.Tables[0].Rows[0]["ModifiedBy"]);
                    }

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
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Fetch_EmployeeDetail_ById");
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
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_LogOut_EmployeeSession");
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
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_LogOut_EmployeeSession");
            return objResp;
        }


        public clsMOBEmployee Fn_Check_EmployeeTokenID(clsMOBEmployee objReq, string tokenid)
        {
            var objResp = new clsMOBEmployee();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Check_EmployeeTokenID");
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                using (SqlCommand cmd = new SqlCommand("USP_EmployeeMob", Con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmpId", objReq.nEmpId);
                    cmd.Parameters.AddWithValue("@TokenId", tokenid);
                    cmd.Parameters.AddWithValue("@QueryType", "ValidateTokenID");

                    object result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        objResp.nEmpId = objReq.nEmpId;
                        objResp.TokenId = tokenid;
                        objResp.vErrorMsg = "Success";
                        objResp.vErrorCode = 200;
                    }
                    else
                    {
                        objResp.vErrorMsg = "Invalid Token ID or Expired Token ID";
                        objResp.vErrorCode = 401;
                    }
                }
            }
            catch (Exception exp)
            {
                Logger.WriteLog("Function Name : Fn_Check_EmployeeTokenID", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
                objResp.vErrorCode = 500;
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Check_EmployeeTokenID");
            return objResp;
        }


        public clsMOBEmployee Fn_Check_EmployeeID_Exists(Int64 EmpID, string Password)
        {
            var objResp = new clsMOBEmployee();
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string EncryptPassword = Generic.EncryptText(Password);

                SqlCommand cmd = new SqlCommand("USP_EmployeeMob", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmpId", EmpID);
                cmd.Parameters.AddWithValue("@EmpPassword", EncryptPassword);
                cmd.Parameters.AddWithValue("@QueryType", "CheckEmployeeIDExists");

                object result = cmd.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    objResp.nEmpId = EmpID;
                    objResp.vEmpPassword = EncryptPassword;
                    objResp.vErrorMsg = "Success";
                    objResp.vErrorCode = 200;
                }
                else
                {
                    objResp.vErrorMsg = "Invalid Employee ID and Employee Password";
                    objResp.vErrorCode = 401;
                }

            }
            catch (Exception exp)
            {
                Logger.WriteLog("Function Name : Fn_Check_EmployeeID_Exists", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
                objResp.vErrorCode = 500;
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }
        

        public List<clsMOBEmployee> Fn_Get_All_EmployeeList(clsMOBEmployee objReq)
        {
            var objRespList = new List<clsMOBEmployee>();
            var objResp = new clsMOBEmployee();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Get_All_EmployeeList");
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_EmployeeMob", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@QueryType", "SelectAll");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        objResp = new clsMOBEmployee();

                        objResp.nEmpId = Convert.ToInt64(ds.Tables[0].Rows[i]["EmpId"]);
                        objResp.vEmpName = Convert.ToString(ds.Tables[0].Rows[i]["EmpName"]);                        

                        if (ds.Tables[0].Rows[0]["EmpMobile"] == null)
                        {
                            objResp.vEmpMobile = string.Empty;
                        }
                        else
                        {
                            objResp.vEmpMobile = Convert.ToString(ds.Tables[0].Rows[0]["EmpMobile"]);
                        }

                        if (ds.Tables[0].Rows[0]["EmpRole"] == null)
                        {
                            objResp.EmpRole = string.Empty;
                        }
                        else
                        {
                            objResp.EmpRole = Convert.ToString(ds.Tables[0].Rows[0]["EmpRole"]);
                        }

                        if (ds.Tables[0].Rows[0]["EmpGrade"] == null)
                        {
                            objResp.vEmpGrade = string.Empty;
                        }
                        else
                        {
                            objResp.vEmpGrade = Convert.ToString(ds.Tables[0].Rows[0]["EmpGrade"]);
                        }

                        if (ds.Tables[0].Rows[0]["EmpGender"] == null)
                        {
                            objResp.EmpGender = string.Empty;
                        }
                        else
                        {
                            objResp.EmpGender = Convert.ToString(ds.Tables[0].Rows[0]["EmpGender"]);
                        }

                        objResp.EmpLocation = Convert.ToString(ds.Tables[0].Rows[i]["EmpLocation"]);
                        objResp.LineName = Convert.ToString(ds.Tables[0].Rows[i]["LineName"]);
                        objResp.Units = Convert.ToString(ds.Tables[0].Rows[i]["Units"]);

                        if (ds.Tables[0].Rows[0]["CreatedOn"] == null)
                        {
                            objResp.CreatedOn = string.Empty;
                        }
                        else
                        {
                            objResp.CreatedOn = Convert.ToString(ds.Tables[0].Rows[0]["CreatedOn"]);
                        }

                        if (ds.Tables[0].Rows[0]["CreatedBy"] == DBNull.Value)
                        {
                            objResp.CreatedBy = 0;
                        }
                        else
                        {
                            objResp.CreatedBy = Convert.ToInt32(ds.Tables[0].Rows[0]["CreatedBy"]);
                        }

                        if (ds.Tables[0].Rows[0]["ModifiedOn"] == null)
                        {
                            objResp.ModifiedOn = string.Empty;
                        }
                        else
                        {
                            objResp.ModifiedOn = Convert.ToString(ds.Tables[0].Rows[0]["ModifiedOn"]);
                        }

                        if (ds.Tables[0].Rows[0]["ModifiedBy"] == DBNull.Value)
                        {
                            objResp.ModifiedBy = 0;
                        }
                        else
                        {
                            objResp.ModifiedBy = Convert.ToInt32(ds.Tables[0].Rows[0]["ModifiedBy"]);
                        }

                        objResp.IsActive = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsActive"]);

                        objResp.vErrorMsg = "Success";
                        objResp.vErrorCode = 200;
                        objRespList.Add(objResp);
                        i++;
                    }
                }
                else
                {
                    objResp.vErrorMsg = "No Employee Records found.";
                    objRespList.Add(objResp);
                    objResp.vErrorCode = 300;
                }               
            }
            catch (Exception exp)
            {
                Logger.WriteLog("Function Name : Fn_Get_All_EmployeeList", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
                objRespList.Add(objResp);
                objResp.vErrorCode = 500;
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objRespList), "Response", "Fn_Get_All_EmployeeList");
            return objRespList;
        }


        public List<clsMOBEmployee> Fn_Get_All_OperatorDetails(clsMOBEmployee objReq)
        {
            var objResp = new List<clsMOBEmployee>();
            var obj = new clsMOBEmployee();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Get_All_OperatorDetails");
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT EM.EmpId AS EmpId, EM.EmpName AS EmpName, EM.EmpGender AS EmpGender,";
                strSql = strSql + " EM.EmpMobile AS EmpMobile, EM.EmpGrade AS EmpGrade, EM.EmpRole AS EmpRole,";
                strSql = strSql + " ED.EmpLocation AS EmpLocation, ED.Units AS Units, ED.LineName AS LineName, LM.LineId AS LineId,";
                strSql = strSql + " FORMAT(EM.CreatedOn, 'dd-MMM-yyyy HH:mm:ss') AS CreatedOn, EM.CreatedBy AS CreatedBy,";
                strSql = strSql + " FORMAT(EM.ModifiedOn, 'dd-MMM-yyyy HH:mm:ss') AS ModifiedOn, EM.ModifiedBy AS ModifiedBy";
                strSql = strSql + " FROM EmployeeMaster AS EM";
                strSql = strSql + " INNER JOIN EmployeeDetail AS ED";
                strSql = strSql + " ON EM.EmpId = ED.Code";
                strSql = strSql + " INNER JOIN LineMaster AS LM";
                strSql = strSql + " ON LM.LineName = ED.LineName";
                strSql = strSql + " WHERE EM.IsActive = 1 AND EM.EmpRole = 'Operator'";

                if (objReq.nEmpId > 0)
                {
                    strSql = strSql + " AND EM.EmpId = " + objReq.nEmpId;
                }

                if (objReq.LineId > 0)
                {
                    strSql = strSql + " AND LM.LineId = " + objReq.LineId;
                }

                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;

                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsMOBEmployee();
                        obj.nEmpId = Convert.ToInt64(ds.Tables[0].Rows[i]["EmpId"]);
                        obj.vEmpName = Convert.ToString(ds.Tables[0].Rows[i]["EmpName"]);

                        if (ds.Tables[0].Rows[0]["EmpGender"] == null)
                        {
                            obj.EmpGender = string.Empty;
                        }
                        else
                        {
                            obj.EmpGender = Convert.ToString(ds.Tables[0].Rows[0]["EmpGender"]);
                        }

                        if (ds.Tables[0].Rows[0]["EmpMobile"] == null)
                        {
                            obj.vEmpMobile = string.Empty;
                        }
                        else
                        {
                            obj.vEmpMobile = Convert.ToString(ds.Tables[0].Rows[i]["EmpMobile"]);
                        }

                        if (ds.Tables[0].Rows[0]["EmpGrade"] == null)
                        {
                            obj.vEmpGrade = string.Empty;
                        }
                        else
                        {
                            obj.vEmpGrade = Convert.ToString(ds.Tables[0].Rows[i]["EmpGrade"]);
                        }

                        obj.EmpRole = Convert.ToString(ds.Tables[0].Rows[i]["EmpRole"]);
                        obj.EmpLocation = Convert.ToString(ds.Tables[0].Rows[i]["EmpLocation"]);
                        obj.Units = Convert.ToString(ds.Tables[0].Rows[i]["Units"]);
                        obj.LineName = Convert.ToString(ds.Tables[0].Rows[i]["LineName"]);
                        obj.LineId = Convert.ToInt64(ds.Tables[0].Rows[i]["LineId"]);

                        if (ds.Tables[0].Rows[0]["CreatedOn"] == null)
                        {
                            obj.CreatedOn = string.Empty;
                        }
                        else
                        {
                            obj.CreatedOn = Convert.ToString(ds.Tables[0].Rows[0]["CreatedOn"]);
                        }

                        if (ds.Tables[0].Rows[0]["CreatedBy"] == DBNull.Value)
                        {
                            obj.CreatedBy = 0;
                        }
                        else
                        {
                            obj.CreatedBy = Convert.ToInt32(ds.Tables[0].Rows[0]["CreatedBy"]);
                        }

                        if (ds.Tables[0].Rows[0]["ModifiedOn"] == null)
                        {
                            obj.ModifiedOn = string.Empty;
                        }
                        else
                        {
                            obj.ModifiedOn = Convert.ToString(ds.Tables[0].Rows[0]["ModifiedOn"]);
                        }

                        if (ds.Tables[0].Rows[0]["ModifiedBy"] == DBNull.Value)
                        {
                            obj.ModifiedBy = 0;
                        }
                        else
                        {
                            obj.ModifiedBy = Convert.ToInt32(ds.Tables[0].Rows[0]["ModifiedBy"]);
                        }

                        obj.vErrorMsg = "Success";
                        obj.vErrorCode = 200;
                        objResp.Add(obj);
                        i++;
                    }
                }
                else
                {
                    obj.vErrorMsg = "No Operator Records found.";
                    objResp.Add(obj);
                    obj.vErrorCode = 300;
                }
            }
            catch (Exception exp)
            {
                Logger.WriteLog("Function Name : Fn_Get_All_OperatorDetails", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
                obj.vErrorCode = 500;
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Get_All_OperatorDetails");
            return objResp;
        }


        public List<clsMOBEmployee> Fn_Get_OperatorCount(clsMOBEmployee objReq)
        {
            var objResp = new List<clsMOBEmployee>();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Get_OperatorCount");
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_EmployeeMob", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@QueryType", "GetOperatorCount");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        var objOperator = new clsMOBEmployee();
                        objOperator.EmpOperatorCount = Convert.ToInt64(ds.Tables[0].Rows[i]["EmpOperatorCount"]);
                        objOperator.EmpOperatorStatus = Convert.ToString(ds.Tables[0].Rows[i]["EmpOperatorStatus"]);
                        objOperator.vErrorMsg = "Success";
                        objOperator.vErrorCode = 200;
                        objResp.Add(objOperator);
                        i++;
                    }
                }
                else
                {
                    var objOperator = new clsMOBEmployee();
                    objOperator.vErrorMsg = "No Operator Count found.";
                    objResp.Add(objOperator);
                }
            }
            catch (Exception exp)
            {
                var objOperator = new clsMOBEmployee();
                objOperator.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Get_OperatorCount", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objOperator.vErrorMsg = exp.Message.ToString();
                objResp.Add(objOperator);
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Get_OperatorCount");
            return objResp;
        }


        public List<clsMOBEmployee> Fn_Get_All_SupervisorDetails(clsMOBEmployee objReq)
        {
            var objResp = new List<clsMOBEmployee>();
            var obj = new clsMOBEmployee();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Get_All_SupervisorDetails");
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                //string strSql = "SELECT EmpId, EmpName, EmpGender, EmpMobile, EmpGrade, EmpRole, EmpLocation,";
                //strSql = strSql + " IsActive FROM EmployeeMaster WHERE 1=1 AND EmpRole = 'Supervisor' AND IsActive = 1";

                string strSql = "SELECT EM.EmpId AS EmpId, EM.EmpName AS EmpName, EM.EmpGender AS EmpGender,";
                strSql = strSql + " EM.EmpMobile AS EmpMobile, EM.EmpGrade AS EmpGrade, EM.EmpRole AS EmpRole,";
                strSql = strSql + " ED.EmpLocation AS EmpLocation, ED.Units AS Units, ED.LineName AS LineName, LM.LineId AS LineId,";
                strSql = strSql + " FORMAT(EM.CreatedOn, 'dd-MMM-yyyy HH:mm:ss') AS CreatedOn, EM.CreatedBy AS CreatedBy,";
                strSql = strSql + " FORMAT(EM.ModifiedOn, 'dd-MMM-yyyy HH:mm:ss') AS ModifiedOn, EM.ModifiedBy AS ModifiedBy";
                strSql = strSql + " FROM EmployeeMaster AS EM";
                strSql = strSql + " INNER JOIN EmployeeDetail AS ED";
                strSql = strSql + " ON EM.EmpId = ED.Code";
                strSql = strSql + " INNER JOIN LineMaster AS LM";
                strSql = strSql + " ON LM.LineName = ED.LineName";
                strSql = strSql + " WHERE EM.IsActive = 1 AND EM.EmpRole = 'Supervisor'";

                if (objReq.nEmpId > 0)
                {
                    strSql = strSql + " AND EM.EmpId = " + objReq.nEmpId;
                }

                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;

                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsMOBEmployee();
                        obj.nEmpId = Convert.ToInt64(ds.Tables[0].Rows[i]["EmpId"]);
                        obj.vEmpName = Convert.ToString(ds.Tables[0].Rows[i]["EmpName"]);

                        if (ds.Tables[0].Rows[i]["EmpGender"] == null)
                        {
                            obj.EmpGender = string.Empty;
                        }
                        else
                        {
                            obj.EmpGender = Convert.ToString(ds.Tables[0].Rows[i]["EmpGender"]);
                        }

                        if (ds.Tables[0].Rows[i]["EmpMobile"] == null)
                        {
                            obj.vEmpMobile = string.Empty;
                        }
                        else
                        {
                            obj.vEmpMobile = Convert.ToString(ds.Tables[0].Rows[i]["EmpMobile"]);
                        }

                        if (ds.Tables[0].Rows[i]["EmpGrade"] == null)
                        {
                            obj.vEmpGrade = string.Empty;
                        }
                        else
                        {
                            obj.vEmpGrade = Convert.ToString(ds.Tables[0].Rows[i]["EmpGrade"]);
                        }

                        obj.EmpRole = Convert.ToString(ds.Tables[0].Rows[i]["EmpRole"]);
                        obj.EmpLocation = Convert.ToString(ds.Tables[0].Rows[i]["EmpLocation"]);
                        obj.Units = Convert.ToString(ds.Tables[0].Rows[i]["Units"]);
                        obj.LineName = Convert.ToString(ds.Tables[0].Rows[i]["LineName"]);
                        obj.LineId = Convert.ToInt64(ds.Tables[0].Rows[i]["LineId"]);

                        if (ds.Tables[0].Rows[i]["CreatedOn"] == null)
                        {
                            obj.CreatedOn = string.Empty;
                        }
                        else
                        {
                            obj.CreatedOn = Convert.ToString(ds.Tables[0].Rows[i]["CreatedOn"]);
                        }

                        if (ds.Tables[0].Rows[i]["CreatedBy"] == DBNull.Value)
                        {
                            obj.CreatedBy = 0;
                        }
                        else
                        {
                            obj.CreatedBy = Convert.ToInt32(ds.Tables[0].Rows[i]["CreatedBy"]);
                        }

                        if (ds.Tables[0].Rows[i]["ModifiedOn"] == null)
                        {
                            obj.ModifiedOn = string.Empty;
                        }
                        else
                        {
                            obj.ModifiedOn = Convert.ToString(ds.Tables[0].Rows[i]["ModifiedOn"]);
                        }

                        if (ds.Tables[0].Rows[i]["ModifiedBy"] == DBNull.Value)
                        {
                            obj.ModifiedBy = 0;
                        }
                        else
                        {
                            obj.ModifiedBy = Convert.ToInt32(ds.Tables[0].Rows[i]["ModifiedBy"]);
                        }

                        //obj.IsActive = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsActive"]);

                        obj.vErrorMsg = "Success";
                        obj.vErrorCode = 200;
                        objResp.Add(obj);
                        i++;
                    }
                }
                else
                {
                    obj.vErrorMsg = "No Supervisor Records found.";
                    objResp.Add(obj);
                    obj.vErrorCode = 300;
                }
            }
            catch (Exception exp)
            {
                Logger.WriteLog("Function Name : Fn_Get_All_SupervisorDetails", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
                obj.vErrorCode = 500;
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Get_All_SupervisorDetails");
            return objResp;
        }



        public clsMOBEmployee Fn_Forgot_Password(clsMOBEmployee objReq)
        {
            var objResp = new clsMOBEmployee();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Forgot_Password");
            try
            {
                if (objReq.nEmpId == null || objReq.nEmpId == 0)
                {
                    objResp.vErrorMsg = "Please Pass the Valid Operator ID";
                    objResp.vErrorCode = 300;
                }
                else if (String.IsNullOrWhiteSpace(objReq.vEmpPassword))
                {
                    objResp.vErrorMsg = "Please Enter Operator/Employee Password.";
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
                    cmd.Parameters.AddWithValue("@QueryType", "ForgotPassword");

                    int i = 0;
                    i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        objResp.vErrorMsg = "Your Password has been change";
                        objResp.vErrorCode = 200;
                    }
                    else
                    {
                        objResp.vErrorMsg = "Failed";
                        objResp.vErrorCode = 400;
                    }
                }
            }
            catch (Exception exp)
            {
                Logger.WriteLog("Function Name : Fn_Forgot_Password", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
                objResp.vErrorCode = 500;
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Forgot_Password");
            return objResp;
        }


        public clsWorker Fn_Fetch_WorkerDetailsByID(clsWorker objReq)
        {
            var objResp = new clsWorker();
            try
            {
                if (objReq.AppEmpID == 0)
                {
                    objResp.vErrorMsg = "Please Pass the Valid Employee/Worker ID";
                }
                else
                {
                    if (Con.State == ConnectionState.Broken)
                    { Con.Close(); }
                    if (Con.State == ConnectionState.Closed)
                    { Con.Open(); }

                    SqlCommand cmd = new SqlCommand("USP_EmployeeMob", Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmpId", objReq.AppEmpID);
                    cmd.Parameters.AddWithValue("@QueryType", "FetchWorkerDetails");

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    int i = 0;
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        objResp.AppEmpID = Convert.ToInt32(ds.Tables[0].Rows[i]["EmpId"]);
                        objResp.Name = Convert.ToString(ds.Tables[0].Rows[i]["EmpName"]);
                        objResp.Unit = Convert.ToString(ds.Tables[0].Rows[i]["Units"]);
                        objResp.LineName = Convert.ToString(ds.Tables[0].Rows[i]["LineName"]);

                        objResp.vErrorMsg = "Success";
                        objResp.vErrorCode = 200;
                    }
                    else
                    {
                        objResp.vErrorMsg = "Employee/Worker details are not found.";
                        objResp.vErrorCode = 404;
                    }
                }
            }
            catch (Exception exp)
            {
                Logger.WriteLog("Function Name : Fn_Fetch_WorkerDetailsByID", " " + exp.Message.ToString(), new StackTrace(exp, true));
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