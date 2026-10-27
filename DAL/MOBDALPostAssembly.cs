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
                        //obj.Product = Convert.ToString(ds.Tables[0].Rows[i]["Product"]);
                        obj.Qty = Convert.ToInt64(ds.Tables[0].Rows[i]["Qty"]);
                        obj.ColorName = Convert.ToString(ds.Tables[0].Rows[i]["ColorName"]);
                        obj.SizeName = Convert.ToString(ds.Tables[0].Rows[i]["SizeName"]);

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
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Fetch_AllLineNames");
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


        public List<clsPostAssemblyLines> Fn_Fetch_LineNameByOperatorID(clsPostAssemblyLines objReq)
        {
            var objResp = new List<clsPostAssemblyLines>();
            var obj = new clsPostAssemblyLines();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Fetch_LineNameByOperatorID");
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_MobilePostAssemblyApp", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AppEmpID", objReq.AppEmpID);
                cmd.Parameters.AddWithValue("@QueryType", "FetchOperatorLineName");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;

                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {

                        obj = new clsPostAssemblyLines();
                        obj.LineName = Convert.ToString(ds.Tables[0].Rows[i]["LineName"]);
                        obj.AppEmpID = Convert.ToInt32(ds.Tables[0].Rows[i]["AppEmpID"]);

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
                Logger.WriteLog("Function Name : Fn_Fetch_LineNameByOperatorID", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Fetch_LineNameByOperatorID");
            return objResp;
        }


        public List<clsPostAssemblyLines> Fn_Fetch_DescByOpIDLineName(clsPostAssemblyLines objReq)
        {
            var objResp = new List<clsPostAssemblyLines>();
            var obj = new clsPostAssemblyLines();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Fetch_DescByOpIDLineName");
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_MobilePostAssemblyApp", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AppEmpID", objReq.AppEmpID);
                cmd.Parameters.AddWithValue("@LineName", objReq.LineName);
                cmd.Parameters.AddWithValue("@QueryType", "FetchDescByOpIDLineName");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;

                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {

                        obj = new clsPostAssemblyLines();
                        obj.AppEmpID = Convert.ToInt32(ds.Tables[0].Rows[i]["AppEmpID"]);
                        obj.LineName = Convert.ToString(ds.Tables[0].Rows[i]["LineName"]);
                        obj.OperationNo = Convert.ToInt64(ds.Tables[0].Rows[i]["OperationNo"]);
                        obj.OperationName = Convert.ToString(ds.Tables[0].Rows[i]["Descriptions"]);

                        obj.vErrorCode = 200;
                        obj.vErrorMsg = "Success";
                        objResp.Add(obj);
                        i++;
                    }
                }
                else
                {
                    obj.vErrorCode = 404;
                    obj.vErrorMsg = "Operation Name/Descriptions records are not found.";
                    objResp.Add(obj);
                }
            }
            catch (Exception exp)
            {
                obj.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Fetch_DescByOpIDLineName", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Fetch_DescByOpIDLineName");
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
                else if (String.IsNullOrWhiteSpace(objReq.LineName))
                {
                    objResp.vErrorMsg = "Please Pass the Line Name";
                    objResp.vErrorCode = 300;
                }
                else if (objReq.OperationNo == null || objReq.OperationNo == 0)
                {
                    objResp.vErrorMsg = "Please Pass the Operation Number";
                    objResp.vErrorCode = 300;
                }
                else if (objReq.BundleID == null || objReq.BundleID == 0)
                {
                    objResp.vErrorMsg = "Please Pass the Valid Bundle ID or QR Code";
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
                    cmd.Parameters.AddWithValue("@LineName", objReq.LineName);
                    cmd.Parameters.AddWithValue("@OperationNo", objReq.OperationNo);
                    cmd.Parameters.AddWithValue("@BundleID", objReq.BundleID);
                    cmd.Parameters.AddWithValue("@QueryType", "UpdatePostAssemblyBundleID");

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            objResp.vErrorCode = Convert.ToInt32(dr["ErrorCode"]);
                            objResp.vErrorMsg = dr["ErrorMessage"].ToString();
                        }
                        else
                        {
                            objResp.vErrorCode = 400;
                            objResp.vErrorMsg = "Supervisor needs to assigned the Bundle ID to Operator/Worker/Employee";
                        }
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
                cmd.Parameters.AddWithValue("@QueryType", "FetchScanPostAssemBundleID");

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
                        obj.AppEndTime = Convert.ToString(ds.Tables[0].Rows[i]["AppEndTime"]);
                        obj.StyleCode = Convert.ToString(ds.Tables[0].Rows[i]["StyleCode"]);
                        obj.OrderNo = Convert.ToString(ds.Tables[0].Rows[i]["OrderNo"]);
                        obj.ColorName = Convert.ToString(ds.Tables[0].Rows[i]["ColorName"]);
                        obj.SizeName = Convert.ToString(ds.Tables[0].Rows[i]["SizeName"]);
                        obj.Qty = Convert.ToInt32(ds.Tables[0].Rows[i]["Qty"]);
                        obj.BundleNo = Convert.ToInt32(ds.Tables[0].Rows[i]["BundleNo"]);
                        obj.OperationNo = Convert.ToInt64(ds.Tables[0].Rows[i]["OperationNo"]);
                        obj.AppEmpID = Convert.ToInt32(ds.Tables[0].Rows[i]["AppEmpID"]);
                        obj.BundleIDStatus = Convert.ToString(ds.Tables[0].Rows[i]["BundleIDStatus"]);

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


        public List<clsPostAssembly> Fn_Fetch_OperatorOrderColorSizeWiseDetails(clsPostAssembly objReq)
        {
            var objResp = new List<clsPostAssembly>();
            var obj = new clsPostAssembly();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Fetch_OperatorOrderColorSizeWiseDetails");
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_MobilePostAssemblyApp", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AppEmpID", objReq.AppEmpID);
                cmd.Parameters.AddWithValue("@OrderNo", objReq.OrderNo);
                cmd.Parameters.AddWithValue("@ColorName", objReq.ColorName);
                cmd.Parameters.AddWithValue("@SizeName", objReq.SizeName);
                cmd.Parameters.AddWithValue("@FromDate", objReq.FromDate);
                cmd.Parameters.AddWithValue("@ToDate", objReq.ToDate);
                cmd.Parameters.AddWithValue("@QueryType", "FetchOrderColorSizeWise");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;

                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsPostAssembly();
                        obj.StyleCode = Convert.ToString(ds.Tables[0].Rows[i]["StyleCode"]);
                        obj.OrderNo = Convert.ToString(ds.Tables[0].Rows[i]["OrderNo"]);
                        obj.ColorName = Convert.ToString(ds.Tables[0].Rows[i]["ColorName"]);
                        obj.SizeName = Convert.ToString(ds.Tables[0].Rows[i]["SizeName"]);
                        obj.Qty = Convert.ToInt32(ds.Tables[0].Rows[i]["Qty"]);

                        obj.vErrorCode = 200;
                        obj.vErrorMsg = "Success";
                        objResp.Add(obj);
                        i++;
                    }
                }
                else
                {
                    obj.vErrorCode = 404;
                    obj.vErrorMsg = "No records found.";
                    objResp.Add(obj);
                }
            }
            catch (Exception exp)
            {
                obj.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Fetch_OperatorOrderColorSizeWiseDetails", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Fetch_OperatorOrderColorSizeWiseDetails");
            return objResp;
        }



        public List<clsPostAssmeblyCount> Fn_Get_OperatorPostAssemblyCount(clsPostAssmeblyCount objReq)
        {
            var objResp = new List<clsPostAssmeblyCount>();
            var obj = new clsPostAssmeblyCount();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Get_OperatorPostAssemblyCount");
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_MobilePostAssemblyApp", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AppEmpID", objReq.AppEmpID);
                cmd.Parameters.AddWithValue("@CurrentDate", objReq.CurrentDate);
                cmd.Parameters.AddWithValue("@QueryType", "GetPostAssemblyQtyCount");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;

                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsPostAssmeblyCount();
                        obj.AppEmpID = Convert.ToInt32(ds.Tables[0].Rows[i]["AppEmpID"]);
                        obj.TotalQty = Convert.ToInt32(ds.Tables[0].Rows[i]["Qty"]);

                        obj.vErrorCode = 200;
                        obj.vErrorMsg = "Success";
                        objResp.Add(obj);
                        i++;
                    }
                }
                else
                {
                    obj.vErrorCode = 404;
                    obj.vErrorMsg = "No Total Quantity records found.";
                    objResp.Add(obj);
                }
            }
            catch (Exception exp)
            {
                obj.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Get_OperatorPostAssemblyCount", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Get_OperatorPostAssemblyCount");
            return objResp;
        }



    }
}