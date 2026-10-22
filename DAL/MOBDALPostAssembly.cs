using BSLDaman.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;

namespace BSLDaman.DAL
{
    public class MOBDALPostAssembly
    {

        SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["BSL"].ConnectionString);


        public List<clsPostAssemblyOrderList> Fn_Fetch_AllOrderNumbers(clsPostAssemblyOrderList objReq)
        {
            var objResp = new List<clsPostAssemblyOrderList>();
            var obj = new clsPostAssemblyOrderList();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Fetch_AllOrderNumbers");
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_MobilePostAssemblyApp", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@QueryType", "FetchAllOrderNumbers");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;

                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsPostAssemblyOrderList();
                        obj.OrderNo = Convert.ToString(ds.Tables[0].Rows[i]["OrderNo"]);
                        obj.Product = Convert.ToString(ds.Tables[0].Rows[i]["Product"]);
                        obj.Qty = Convert.ToInt64(ds.Tables[0].Rows[i]["Qty"]);

                        obj.vErrorCode = 200;
                        obj.vErrorMsg = "Success";
                        objResp.Add(obj);
                        i++;
                    }
                }
                else
                {
                    obj.vErrorCode = 404;
                    obj.vErrorMsg = "Order Numbers records are not found.";
                    objResp.Add(obj);
                }
            }
            catch (Exception exp)
            {
                obj.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Fetch_AllOrderNumbers", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Fetch_AllOrderNumbers");
            return objResp;
        }


        public List<clsPostAssemblyLines> Fn_Fetch_AllLineNames(clsPostAssemblyLines objReq)
        {
            var objResp = new List<clsPostAssemblyLines>();
            var obj = new clsPostAssemblyLines();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Fetch_AllLine");
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_MobilePostAssemblyApp", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@QueryType", "FetchAllLineName");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;

                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsPostAssemblyLines();
                        obj.LineId = Convert.ToInt32(ds.Tables[0].Rows[i]["LineId"]);
                        obj.LineName = Convert.ToString(ds.Tables[0].Rows[i]["LineName"]);
                        obj.LineCode = Convert.ToString(ds.Tables[0].Rows[i]["LineCode"]);
                        obj.SectionName = Convert.ToString(ds.Tables[0].Rows[i]["SectionName"]);
                        obj.LineStatus = Convert.ToString(ds.Tables[0].Rows[i]["LineStatus"]);

                        obj.vErrorCode = 200;
                        obj.vErrorMsg = "Success";
                        objResp.Add(obj);
                        i++;
                    }
                }
                else
                {
                    obj.vErrorCode = 404;
                    obj.vErrorMsg = "Line Names records are not found.";
                    objResp.Add(obj);
                }
            }
            catch (Exception exp)
            {
                obj.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Fetch_AllLineNames", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Fetch_AllLineNames");
            return objResp;
        }



        public clsPostAssembly Fn_Update_PostAssemblySinglePcsAssign(clsPostAssembly objReq)
        {
            var objResp = new clsPostAssembly();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Update_PostAssemblySinglePcsAssign");
            try
            {
                if (objReq.SupervisorID == null || objReq.SupervisorID == 0)
                {
                    objResp.vErrorMsg = "Please Pass the Valid Supervisor ID";
                    objResp.vErrorCode = 300;
                }
                else if (objReq.AppEmpID == null || objReq.AppEmpID == 0)
                {
                    objResp.vErrorMsg = "Please Pass the Valid App Employee ID";
                    objResp.vErrorCode = 300;
                }
                else
                {
                    if (Con.State == ConnectionState.Broken)
                    { Con.Close(); }
                    if (Con.State == ConnectionState.Closed)
                    { Con.Open(); }

                    SqlCommand cmd = new SqlCommand("AssignPostAssemOpNo", Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SupervisorID", objReq.SupervisorID);
                    cmd.Parameters.AddWithValue("@AppEmpID", objReq.AppEmpID);
                    cmd.Parameters.AddWithValue("@OperationNo", objReq.OperationNo);
                    cmd.Parameters.AddWithValue("@QueryType", "AssignPostAssemOpNo");

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
                        objResp.vErrorMsg = "Failed";
                    }
                }
            }
            catch (Exception exp)
            {
                objResp.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Update_PostAssemblySinglePcsAssign", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Update_PostAssemblySinglePcsAssign");
            return objResp;
        }


        public clsPostAssembly Fn_Update_PostAssemblySinglePcsScan(clsPostAssembly objReq)
        {
            var objResp = new clsPostAssembly();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Update_PostAssemblySinglePcsScan");
            try
            {
                if (objReq.AppEmpID == null || objReq.AppEmpID == 0)
                {
                    objResp.vErrorMsg = "Please Pass the Valid App Employee ID";
                    objResp.vErrorCode = 300;
                }
                else if (objReq.BundleID == null || objReq.BundleID == 0)
                {
                    objResp.vErrorMsg = "Please Pass the Valid Bundle ID";
                    objResp.vErrorCode = 300;
                }
                else
                {
                    if (Con.State == ConnectionState.Broken)
                    { Con.Close(); }
                    if (Con.State == ConnectionState.Closed)
                    { Con.Open(); }

                    SqlCommand cmd = new SqlCommand("USP_MobilePostAssemblyApp", Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AppEmpID", objReq.AppEmpID);
                    cmd.Parameters.AddWithValue("@BundleID", objReq.BundleID);
                    cmd.Parameters.AddWithValue("@OperationNo", objReq.OperationNo);
                    cmd.Parameters.AddWithValue("@QueryType", "UpdatePostAssemblyBundleID");

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
                        objResp.vErrorMsg = "Failed";
                    }
                }
            }
            catch (Exception exp)
            {
                objResp.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Update_PostAssemblySinglePcsScan", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Update_PostAssemblySinglePcsScan");
            return objResp;
        }


        public List<clsPostAssembly> Fn_Fetch_OperatorFinishedPostAssemblyBundleID(clsPostAssembly objReq)
        {
            var objResp = new List<clsPostAssembly>();
            var obj = new clsPostAssembly();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Fetch_OperatorFinishedPostAssemblyBundleID");
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_MobilePostAssemblyApp", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AppEmpID", objReq.AppEmpID);
                cmd.Parameters.AddWithValue("@QueryType", "FetchScanPostAssemblyBundleID");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;

                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsPostAssembly();
                        obj.BundleID = Convert.ToInt64(ds.Tables[0].Rows[i]["BundleID"]);
                        obj.StyleCode = Convert.ToString(ds.Tables[0].Rows[i]["StyleCode"]);
                        obj.OrderNo = Convert.ToString(ds.Tables[0].Rows[i]["OrderNo"]);
                        obj.OperationNo = Convert.ToInt32(ds.Tables[0].Rows[i]["OperationNo"]);
                        obj.SubSection = Convert.ToString(ds.Tables[0].Rows[i]["SubSection"]);
                        obj.ColorName = Convert.ToString(ds.Tables[0].Rows[i]["ColorName"]);
                        obj.SizeName = Convert.ToString(ds.Tables[0].Rows[i]["SizeName"]);
                        obj.Qty = Convert.ToInt32(ds.Tables[0].Rows[i]["Qty"]);
                        obj.AppStartTime = Convert.ToString(ds.Tables[0].Rows[i]["AppStartTime"]);
                        obj.AppEndTime = Convert.ToString(ds.Tables[0].Rows[i]["AppEndTime"]);

                        obj.vErrorCode = 200;
                        obj.vErrorMsg = "Success";
                        objResp.Add(obj);
                        i++;
                    }
                }
                else
                {
                    obj.vErrorCode = 404;
                    obj.vErrorMsg = "No records found";
                    objResp.Add(obj);
                }
            }
            catch (Exception exp)
            {
                obj.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Fetch_OperatorFinishedPostAssemblyBundleID", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Fetch_OperatorFinishedPostAssemblyBundleID");
            return objResp;
        }



    }
}