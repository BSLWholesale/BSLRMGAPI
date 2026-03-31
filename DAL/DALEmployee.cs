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
    public class DALEmployee
    {
        SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["BSL"].ConnectionString);

        public clsEmployeeDetail Fn_Add_Employee_Detail(clsEmployeeDetail objReq)
        {
            var objResp = new clsEmployeeDetail();

            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_Employee", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Units", objReq.Units);
                cmd.Parameters.AddWithValue("@LineName", objReq.LineName);
                cmd.Parameters.AddWithValue("@Code", objReq.Code);
                cmd.Parameters.AddWithValue("@EmpName", objReq.EmpName);
                cmd.Parameters.AddWithValue("@Remarks", objReq.Remarks);
                cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                cmd.Parameters.AddWithValue("@QueryType", "InsertEmpDetail");
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
                    objResp.vErrorMsg = "Inserting Failed";
                }
            }
            catch (Exception exp)
            {
                objResp.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Add_Employee_Detail", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }

        public clsEmployeeDetail Fn_Update_Employee_Detail(clsEmployeeDetail objReq)
        {
            var objResp = new clsEmployeeDetail();

            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_Employee", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SrNo", objReq.SrNo);
                cmd.Parameters.AddWithValue("@Units", objReq.Units);
                cmd.Parameters.AddWithValue("@LineName", objReq.LineName);
                cmd.Parameters.AddWithValue("@Code", objReq.Code);
                cmd.Parameters.AddWithValue("@EmpName", objReq.EmpName);
                cmd.Parameters.AddWithValue("@Remarks", objReq.Remarks);
                cmd.Parameters.AddWithValue("@ModifiedBy", objReq.ModifiedBy);
                cmd.Parameters.AddWithValue("@QueryType", "UpdateEmpDetail");
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
                    objResp.vErrorMsg = "Updating Failed";
                }
            }
            catch (Exception exp)
            {
                objResp.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Update_Employee_Detail", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }

        public List<clsEmployeeDetail> Fn_Get_All_Employee_Detail(clsEmployeeDetail objReq)
        {
            var objResp = new List<clsEmployeeDetail>();
            var obj = new clsEmployeeDetail();
            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_Employee", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SrNo", objReq.SrNo);
                cmd.Parameters.AddWithValue("@QueryType", "SelectEmpDetail");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsEmployeeDetail();
                        obj.SrNo = Convert.ToInt64(ds.Tables[0].Rows[i]["SrNo"]);
                        obj.Units = Convert.ToString(ds.Tables[0].Rows[i]["Units"]);
                        obj.LineName = Convert.ToString(ds.Tables[0].Rows[i]["LineName"]);
                        string strCode = Convert.ToString(ds.Tables[0].Rows[i]["Code"]);
                        if(strCode != "") {
                            obj.Code = Convert.ToInt64(ds.Tables[0].Rows[i]["Code"]);
                        }
                        obj.EmpName = Convert.ToString(ds.Tables[0].Rows[i]["EmpName"]);
                        obj.Remarks = Convert.ToString(ds.Tables[0].Rows[i]["Remarks"]);

                        obj.vErrorCode = 200;
                        obj.vErrorMsg = "Success";
                        objResp.Add(obj);
                        i++;
                    }
                }
                else
                {
                    obj.vErrorCode = 404;
                    obj.vErrorMsg = "No Record found";
                    objResp.Add(obj);
                }

               // Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Get_All_Employee_Detail");
            }
            catch (Exception exp)
            {
                obj.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Get_All_Employee_Detail", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }

        public clsEmployeeDetail Fn_Delete_Employee_Detail(clsEmployeeDetail objReq)
        {
            var objResp = new clsEmployeeDetail();

            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_Employee", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SrNo", objReq.SrNo);
                cmd.Parameters.AddWithValue("@QueryType", "DeleteEmpDetail");
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
                    objResp.vErrorMsg = "Deleting Failed";
                }
            }
            catch (Exception exp)
            {
                objResp.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Delete_Employee_Detail", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }

        #region Start For Color 

        public clsColorMaster Fn_Add_Color(clsColorMaster objReq)
        {
            var objResp = new clsColorMaster();

            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_Employee", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ColorName", objReq.ColorName);
                cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                cmd.Parameters.AddWithValue("@QueryType", "InsertColor");
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
                    objResp.vErrorMsg = "Inserting Failed";
                }
            }
            catch (Exception exp)
            {
                objResp.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Add_Color", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }

        public clsColorMaster Fn_Update_Color(clsColorMaster objReq)
        {
            var objResp = new clsColorMaster();

            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_Employee", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", objReq.Id);
                cmd.Parameters.AddWithValue("@ColorName", objReq.ColorName);
                cmd.Parameters.AddWithValue("@ModifiedBy", objReq.ModifiedBy);
                cmd.Parameters.AddWithValue("@QueryType", "UpdateColor");
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
                    objResp.vErrorMsg = "Updating Failed";
                }
            }
            catch (Exception exp)
            {
                objResp.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Update_Color", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }

        public List<clsColorMaster> Fn_Get_Color_List(clsColorMaster objReq)
        {
            var objResp = new List<clsColorMaster>();
            var obj = new clsColorMaster();
            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_Employee", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", objReq.Id);
                cmd.Parameters.AddWithValue("@QueryType", "SelectColor");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsColorMaster();
                        obj.Id = Convert.ToInt16(ds.Tables[0].Rows[i]["Id"]);
                        obj.ColorName = Convert.ToString(ds.Tables[0].Rows[i]["ColorName"]);

                        obj.vErrorCode = 200;
                        obj.vErrorMsg = "Success";
                        objResp.Add(obj);
                        i++;
                    }
                }
                else
                {
                    obj.vErrorCode = 404;
                    obj.vErrorMsg = "No Record found";
                    objResp.Add(obj);
                }
            }
            catch (Exception exp)
            {
                obj.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Get_Color_List", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }

        public clsColorMaster Fn_Delete_Color(clsColorMaster objReq)
        {
            var objResp = new clsColorMaster();

            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_Employee", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", objReq.Id);
                cmd.Parameters.AddWithValue("@QueryType", "DeleteColor");
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
                    objResp.vErrorMsg = "Deleting Failed";
                }
            }
            catch (Exception exp)
            {
                objResp.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Delete_Color", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }

        #endregion

        public clsEmployee Fn_LogIn_Employee(clsEmployee objReq)
        {
            var objResp = new clsEmployee();
            try
            {
                if (objReq.nEmpId == null || objReq.nEmpId == 0 && objReq.vEmpMobile == null || objReq.vEmpMobile == "0")
                {
                    objResp.vErrorMsg = "Please Enter Employee Id";
                }
                else if (String.IsNullOrWhiteSpace(objReq.vEmpPassword))
                {
                    objResp.vErrorMsg = "Please Enter Password";
                }
                else
                {
                    if (Con.State == ConnectionState.Broken)
                    { Con.Close(); }
                    if (Con.State == ConnectionState.Closed)
                    { Con.Open(); }

                    string encriptPassword = Generic.EncryptText(objReq.vEmpPassword);

                    SqlCommand cmd = new SqlCommand("USP_Employee", Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmpId", objReq.nEmpId);
                    cmd.Parameters.AddWithValue("@EmpMobile", objReq.vEmpMobile);
                    cmd.Parameters.AddWithValue("@EmpPassword", encriptPassword);
                    cmd.Parameters.AddWithValue("@QueryType", "LogIn");

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    int i = 0;

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        string decryptTextPassword = Generic.DecryptText(Convert.ToString(ds.Tables[0].Rows[i]["EmpPassword"]));
                        objResp.nEmpId = Convert.ToInt32(ds.Tables[0].Rows[i]["EmpId"]);
                        objResp.vEmpName = Convert.ToString(ds.Tables[0].Rows[i]["EmpName"]);
                        objResp.vEmpMobile = Convert.ToString(ds.Tables[0].Rows[i]["EmpMobile"]);
                        objResp.vEmpPassword = decryptTextPassword;
                        objResp.EmpRole = Convert.ToString(ds.Tables[0].Rows[i]["EmpRole"]);

                        objResp.vErrorMsg = "Success";
                    }
                    else
                    {
                        objResp.vErrorMsg = "Entered Credentials has been invalid.";
                    }
                }
            }
            catch (Exception exp)
            {
                Logger.WriteLog("Function Name : Fn_LogIn_Employee", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }

        public List<clsResponseDropdown> Fn_Fill_DropdownList(clsRequestDropdown objReq)
        {
            var objResp = new List<clsResponseDropdown>();
            try
            {
                string strSql = "";
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                if (String.IsNullOrWhiteSpace(objReq.vValueField))
                {
                    strSql = "select Distinct " + objReq.vFieldName + " from " + objReq.vTBLName + " where 1=1";
                    if (!String.IsNullOrWhiteSpace(objReq.vCriteria))
                    {
                        strSql = strSql + objReq.vCriteria;
                    }
                    strSql = strSql + " order by " + objReq.vFieldName + "";
                }
                else
                {
                    strSql = "select Distinct " + objReq.vValueField + ", " + objReq.vFieldName + " from " + objReq.vTBLName + " where 1=1";
                    if (!String.IsNullOrWhiteSpace(objReq.vCriteria))
                    {
                        strSql = strSql + objReq.vCriteria;
                    }
                    strSql = strSql + " order by " + objReq.vValueField + "";
                }

                SqlDataAdapter da = new SqlDataAdapter(strSql, Con);
                DataSet ds = new DataSet();
                da.Fill(ds);

                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        var obj = new clsResponseDropdown();

                        if (String.IsNullOrWhiteSpace(objReq.vValueField))
                        {
                            obj.vFieldName = Convert.ToString(ds.Tables[0].Rows[i][0]);
                        }
                        else
                        {
                            obj.vValueField = Convert.ToString(ds.Tables[0].Rows[i][0]);
                            obj.vFieldName = Convert.ToString(ds.Tables[0].Rows[i][1]);
                        }

                        obj.vErrorMsg = "Success";
                        objResp.Add(obj);
                        i++;
                    }
                }
                else
                {

                }
            }
            catch (Exception exp)
            {
                Logger.WriteLog("Function Name : Fn_Fill_DropdownList", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                var obj = new clsResponseDropdown();
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }


        public List<clsDashboardEmployeeCount> Fn_Get_DashboardAttendenceEmpOperatorCount(clsDashboardEmployeeCount objReq)
        {
            var objResp = new List<clsDashboardEmployeeCount>();
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_DashboardEmployeeCount", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        var objItem = new clsDashboardEmployeeCount();
                        objItem.UnitName = Convert.ToString(ds.Tables[0].Rows[i]["UnitName"]);
                        objItem.EmployeeCount = Convert.ToInt32(ds.Tables[0].Rows[i]["EmployeeCount"]);
                        objItem.vErrorMsg = "Success";
                        objResp.Add(objItem);
                        i++;
                    }
                }
                else
                {
                    var objItem = new clsDashboardEmployeeCount();
                    objItem.vErrorMsg = "No Employee Count found.";
                    objResp.Add(objItem);
                }
            }
            catch (Exception exp)
            {
                Logger.WriteLog("Function Name : Fn_Get_DashboardAttendenceEmpOperatorCount", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                var objItem = new clsDashboardEmployeeCount();
                objItem.vErrorMsg = exp.Message.ToString();
                objResp.Add(objItem);
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }


    }
}