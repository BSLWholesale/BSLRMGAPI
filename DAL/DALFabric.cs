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
    public class DALFabric
    {
        SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["BSL"].ConnectionString);

        Int64 mxID = 0;

        public Int64 Fn_Get_MXID(string strTBLName, string strFieldName)
        {
            try
            {
                //if (Con.State == ConnectionState.Broken)
                //{ Con.Close(); }
                //if (Con.State == ConnectionState.Closed)
                //{ Con.Open(); }

                string strSql = "SELECT MAX(" + strFieldName + ") AS ID FROM " + strTBLName + "";
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
                        string strMXID = Convert.ToString(ds.Tables[0].Rows[i]["ID"]);
                        if (strMXID == "")
                        {
                            mxID = 1;
                        }
                        else
                        {
                            mxID = Convert.ToInt64(ds.Tables[0].Rows[i]["ID"]) + 1;
                        }
                        i++;
                    }
                }
                else
                {
                    mxID = 1;
                }
            }
            catch (Exception exp)
            {
                Logger.WriteLog("Function Name : Fn_Get_MXID", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                exp.Message.ToString();
            }
            return mxID;
        }

        public FabricInhouse Fn_Upload_Fabirc_Inhouse(FabricInhouse objReq)
        {
            var objResp = new FabricInhouse();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Upload_Fabirc_Inhouse");
            try
            {

                if (objReq.FabricOrderId == 0)
                {
                    objResp.vErrorCode = 400;
                    objResp.vErrorMsg = "FabricOrderId is empty";
                    return objResp;
                }

                if (!String.IsNullOrWhiteSpace(objReq.vErrorMsg))
                {
                    objResp.vErrorCode = objReq.vErrorCode;
                    objResp.vErrorMsg = objReq.vErrorMsg;
                    return objResp;
                }
                else if (objReq._oInhouseList == null || objReq._oInhouseList.Count == 0)
                {
                    objResp.vErrorCode = 400;
                    objResp.vErrorMsg = "Please select List";
                    return objResp;
                }
                else
                {
                    if (Con.State == ConnectionState.Broken)
                    { Con.Close(); }
                    if (Con.State == ConnectionState.Closed)
                    { Con.Open(); }

                    foreach (FabricInhouseList _oList in objReq._oInhouseList)
                    {

                        SqlCommand cmd = new SqlCommand("USP_FABRIC_ORDER", Con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FabricOrderId", objReq.FabricOrderId);
                        cmd.Parameters.AddWithValue("@LotNo", objReq.LotNo);
                        cmd.Parameters.AddWithValue("@RollNo", _oList.RollNo);
                        cmd.Parameters.AddWithValue("@TotalQuantity", _oList.Quantity);
                        cmd.Parameters.AddWithValue("@Unit", _oList.Unit);
                        cmd.Parameters.AddWithValue("@Width", _oList.Width);
                        cmd.Parameters.AddWithValue("@ShadeName", _oList.ShadeName);
                        cmd.Parameters.AddWithValue("@GSM", _oList.GSM);
                        cmd.Parameters.AddWithValue("@Shrinkage", _oList.Shrinkage);
                        cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                        cmd.Parameters.AddWithValue("@QueryType", "Insert_Fabric_InHouseId");
                        int j = cmd.ExecuteNonQuery();
                        if (j > 0)
                        {
                            objResp.vErrorCode = 200;
                            objResp.vErrorMsg = "Success";
                        }
                        else
                        {
                            objResp.vErrorCode = 400;
                            objResp.vErrorMsg = "File Uploading failed ";
                            return objResp;
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                objResp.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Upload_Fabirc_Inhouse", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Upload_Fabirc_Inhouse");
            return objResp;
        }

        public List<clsFabricOrder> Fn_Get_Fabric_Order(clsFabricOrder objReq)
        {
            var objResp = new List<clsFabricOrder>();
            var obj = new clsFabricOrder();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Get_Fabric_Order");
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "Select FabricOrderId, StyleCode, ItemCode, Descriptions, Contents, Mill, FabricColor, FabricCC, Width, ";
                strSql = strSql + " WidthTolerance, OrderRollLength, OrderRollLengthTolerance, GSM, GSMTolerance, ";
                strSql = strSql + " OrderShrinkageWarpLength, OrderShrinkageWaftWidth, TotalQuantity, Unit, MarkerType,";
                strSql = strSql + " TotalRollNo, LotNo, SupplierQty, ";
                strSql = strSql + " Price, Format(CreatedOn, 'dd-MMM-yyyy') AS CreatedOn from Fabric_Order WHERE 1=1 ";
                if (!String.IsNullOrWhiteSpace(objReq.StyleCode))
                {
                    strSql = strSql + " AND StyleCode = @StyleCode ";
                }
                if (!String.IsNullOrWhiteSpace(objReq.ItemCode))
                {
                    strSql = strSql + " AND ItemCode = @ItemCode ";
                }
                strSql = strSql + " ORDER BY StyleCode, ItemCode ";

                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;

                if (!String.IsNullOrWhiteSpace(objReq.StyleCode))
                {
                    cmd.Parameters.AddWithValue("@StyleCode", objReq.StyleCode);
                }
                if (!String.IsNullOrWhiteSpace(objReq.ItemCode))
                {
                    cmd.Parameters.AddWithValue("@ItemCode", objReq.ItemCode);
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsFabricOrder();
                        obj.FabricOrderId = Convert.ToInt32(ds.Tables[0].Rows[i]["FabricOrderId"]);
                        obj.StyleCode = Convert.ToString(ds.Tables[0].Rows[i]["StyleCode"]);
                        obj.ItemCode = Convert.ToString(ds.Tables[0].Rows[i]["ItemCode"]);
                        obj.Descriptions = Convert.ToString(ds.Tables[0].Rows[i]["Descriptions"]);
                        obj.Contents = Convert.ToString(ds.Tables[0].Rows[i]["Contents"]);
                        obj.Mill = Convert.ToString(ds.Tables[0].Rows[i]["Mill"]);
                        obj.FabricColor = Convert.ToString(ds.Tables[0].Rows[i]["FabricColor"]);
                        obj.FabricCC = Convert.ToDecimal(ds.Tables[0].Rows[i]["FabricCC"]);
                        obj.Width = Convert.ToDecimal(ds.Tables[0].Rows[i]["Width"]);
                        obj.WidthTolerance = Convert.ToDecimal(ds.Tables[0].Rows[i]["WidthTolerance"]);
                        obj.OrderRollLength = Convert.ToDecimal(ds.Tables[0].Rows[i]["OrderRollLength"]);
                        obj.OrderRollLengthTolerance = Convert.ToDecimal(ds.Tables[0].Rows[i]["OrderRollLengthTolerance"]);
                        obj.GSM = Convert.ToDecimal(ds.Tables[0].Rows[i]["GSM"]);
                        obj.GSMTolerance = Convert.ToDecimal(ds.Tables[0].Rows[i]["GSMTolerance"]);
                        obj.OrderShrinkageWarpLength = Convert.ToDecimal(ds.Tables[0].Rows[i]["OrderShrinkageWarpLength"]);
                        obj.OrderShrinkageWaftWidth = Convert.ToDecimal(ds.Tables[0].Rows[i]["OrderShrinkageWaftWidth"]);
                        obj.TotalQuantity = Convert.ToDecimal(ds.Tables[0].Rows[i]["TotalQuantity"]);
                        obj.Unit = Convert.ToString(ds.Tables[0].Rows[i]["Unit"]);
                        obj.MarkerType = Convert.ToString(ds.Tables[0].Rows[i]["MarkerType"]);
                        obj.Price = Convert.ToDecimal(ds.Tables[0].Rows[i]["Price"]);
                        obj.CreatedOn = Convert.ToString(ds.Tables[0].Rows[i]["CreatedOn"]);

                        //obj.LotNo = Convert.ToInt32(ds.Tables[0].Rows[i]["LotNo"]);
                        obj.LotNo = ds.Tables[0].Rows[i]["LotNo"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[i]["LotNo"]) : 0; // Default value
                        obj.RollNo = ds.Tables[0].Rows[i]["TotalRollNo"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[i]["TotalRollNo"]) : 0;
                        obj.SupplierQty = ds.Tables[0].Rows[i]["SupplierQty"] != DBNull.Value ? Convert.ToDecimal(ds.Tables[0].Rows[i]["SupplierQty"]) : 0;

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
                Logger.WriteLog("Function Name : Fn_Get_Fabric_Order", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Get_Fabric_Order");
            return objResp;
        }

        public List<FabricInhouseList> Fn_Get_Fabric_Roll(FabricInhouse objReq)
        {
            var objResp = new List<FabricInhouseList>();
            var obj = new FabricInhouseList();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Get_Fabric_Roll");
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "Select InHouseId, FabricOrderId, RollNo, Quantity, Unit, Width, ShadeName, GSM, Shrinkage, LotNo,";
                strSql = strSql + " CreatedBy, Format(CreatedOn, 'dd-MMM-yyyy') AS CreatedOn from Fabric_Inhouse WHERE 1=1 ";
                if (objReq.FabricOrderId != 0)
                {
                    strSql = strSql + " AND FabricOrderId = @FabricOrderId ";
                }
                if (objReq.LotNo != 0)
                {
                    strSql = strSql + " AND LotNo = @LotNo ";
                }
                strSql = strSql + " ORDER BY RollNo ";

                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;

                if (objReq.FabricOrderId != 0)
                {
                    cmd.Parameters.AddWithValue("@FabricOrderId", objReq.FabricOrderId);
                }
                if (objReq.LotNo != 0)
                {
                    cmd.Parameters.AddWithValue("@LotNo", objReq.LotNo);
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new FabricInhouseList();
                        // obj.FabricOrderId = Convert.ToInt32(ds.Tables[0].Rows[i]["FabricOrderId"]);
                        obj.InHouseId = Convert.ToInt64(ds.Tables[0].Rows[i]["InHouseId"]);
                        obj.RollNo = Convert.ToDecimal(ds.Tables[0].Rows[i]["RollNo"]);
                        obj.Quantity = Convert.ToDecimal(ds.Tables[0].Rows[i]["Quantity"]);
                        obj.Unit = Convert.ToString(ds.Tables[0].Rows[i]["Unit"]);
                        obj.Width = Convert.ToDecimal(ds.Tables[0].Rows[i]["Width"]);
                        obj.ShadeName = Convert.ToString(ds.Tables[0].Rows[i]["ShadeName"]);
                        obj.GSM = Convert.ToDecimal(ds.Tables[0].Rows[i]["GSM"]);
                        obj.Shrinkage = Convert.ToDecimal(ds.Tables[0].Rows[i]["Shrinkage"]);
                        obj.CreatedBy = Convert.ToInt32(ds.Tables[0].Rows[i]["CreatedBy"]);
                        obj.CreatedOn = Convert.ToString(ds.Tables[0].Rows[i]["CreatedOn"]);
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
                objResp[0].vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Get_Fabric_Roll", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp[0].vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Get_Fabric_Roll");
            return objResp;
        }

        public FabricInhouseList Fn_Update_Fabric_RollNo(FabricInhouseList objReq)
        {
            var objResp = new FabricInhouseList();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Update_Fabric_RollNo");
            try
            {

                if (objReq.InHouseId == 0 || objReq.InHouseId == null)
                {
                    objResp.vErrorCode = 400;
                    objResp.vErrorMsg = "InHouseId is empty";
                    return objResp;
                }
                else
                {
                    if (Con.State == ConnectionState.Broken)
                    { Con.Close(); }
                    if (Con.State == ConnectionState.Closed)
                    { Con.Open(); }

                    SqlCommand cmd = new SqlCommand("USP_FABRIC_ORDER", Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@InHouseId", objReq.InHouseId);
                    cmd.Parameters.AddWithValue("@TotalQuantity", objReq.Quantity);
                    cmd.Parameters.AddWithValue("@Width", objReq.Width);
                    cmd.Parameters.AddWithValue("@ShadeName", objReq.ShadeName);
                    cmd.Parameters.AddWithValue("@GSM", objReq.GSM);
                    cmd.Parameters.AddWithValue("@Shrinkage", objReq.Shrinkage);
                    cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                    cmd.Parameters.AddWithValue("@QueryType", "Update_Fabric_RollNo");
                    int j = cmd.ExecuteNonQuery();
                    if (j > 0)
                    {
                        objResp.vErrorCode = 200;
                        objResp.vErrorMsg = "Success";
                    }
                    else
                    {
                        objResp.vErrorCode = 400;
                        objResp.vErrorMsg = "RollNo Updating failed ";
                        return objResp;
                    }
                }
            }
            catch (Exception exp)
            {
                objResp.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Update_Fabric_RollNo", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Update_Fabric_RollNo");
            return objResp;
        }

        public clsFabricOrder Fn_Update_Fabric_LotNo(clsFabricOrder objReq)
        {
            var objResp = new clsFabricOrder();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Update_Fabric_LotNo");
            try
            {

                if (String.IsNullOrWhiteSpace(objReq.StyleCode))
                {
                    objResp.vErrorCode = 400;
                    objResp.vErrorMsg = "StyleCode is empty";
                    return objResp;
                }
                else if (String.IsNullOrWhiteSpace(objReq.ItemCode))
                {
                    objResp.vErrorCode = 400;
                    objResp.vErrorMsg = "ItemCode is empty";
                    return objResp;
                }
                else if (objReq.LotNo == 0)
                {
                    objResp.vErrorCode = 400;
                    objResp.vErrorMsg = "LotNo is zero";
                    return objResp;
                }
                else if (objReq.RollNo == 0)
                {
                    objResp.vErrorCode = 400;
                    objResp.vErrorMsg = "Total RollNo is zero";
                    return objResp;
                }
                else if (objReq.SupplierQty == 0)
                {
                    objResp.vErrorCode = 400;
                    objResp.vErrorMsg = "SupplierQty is zero";
                    return objResp;
                }
                else
                {
                    if (Con.State == ConnectionState.Broken)
                    { Con.Close(); }
                    if (Con.State == ConnectionState.Closed)
                    { Con.Open(); }

                    SqlCommand cmd = new SqlCommand("USP_FABRIC_ORDER", Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StyleCode", objReq.StyleCode);
                    cmd.Parameters.AddWithValue("@ItemCode", objReq.ItemCode);
                    cmd.Parameters.AddWithValue("@LotNo", objReq.LotNo);
                    cmd.Parameters.AddWithValue("@RollNo", objReq.RollNo);
                    cmd.Parameters.AddWithValue("@SupplierQty", objReq.SupplierQty);
                    cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                    cmd.Parameters.AddWithValue("@QueryType", "Update_Fabric_LotNo");
                    int j = cmd.ExecuteNonQuery();
                    if (j > 0)
                    {
                        objResp.vErrorCode = 200;
                        objResp.vErrorMsg = "Success";
                    }
                    else
                    {
                        objResp.vErrorCode = 400;
                        objResp.vErrorMsg = "LotNo Updating failed ";
                        return objResp;
                    }
                }
            }
            catch (Exception exp)
            {
                objResp.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Update_Fabric_LotNo", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Update_Fabric_LotNo");
            return objResp;
        }

        public clsBatch Fn_Make_New_Batch(clsBatch objReq)
        {
            var objResp = new clsBatch();
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Request", "Fn_Make_New_Batch");
            try
            {
                if (objReq.LotNo == 0)
                {
                    objResp.vErrorCode = 400;
                    objResp.vErrorMsg = "LotNo is empty";
                }
                else if (objReq.CreatedBy == 0)
                {
                    objResp.vErrorCode = 400;
                    objResp.vErrorMsg = "Login id is empty";
                }
                else
                {
                    if (Con.State == ConnectionState.Broken)
                    { Con.Close(); }
                    if (Con.State == ConnectionState.Closed)
                    { Con.Open(); }

                    Int64 mxId = Fn_Get_MXID("Fabric_Batch", "BatchNo");

                    objReq.BatchNo = Convert.ToInt32(mxId);

                    SqlCommand cmd = new SqlCommand("USP_FABRIC_BATCH", Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@LotNo", objReq.LotNo);
                    cmd.Parameters.AddWithValue("@BatchNo", objReq.BatchNo);
                    cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                    cmd.Parameters.AddWithValue("@QueryType", "INSERT_BATCH");
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        foreach (clsBatchList _oList in objReq._OBatchList)
                        {

                            SqlCommand cmd1 = new SqlCommand("USP_FABRIC_BATCH", Con);
                            cmd1.CommandType = CommandType.StoredProcedure;
                            cmd1.Parameters.AddWithValue("@BatchNo", objReq.BatchNo);
                            cmd1.Parameters.AddWithValue("@RollNo", _oList.RollNo);
                            cmd1.Parameters.AddWithValue("@Quantity", _oList.Quantity);
                            cmd1.Parameters.AddWithValue("@BatchStatus", _oList.BatchStatus);
                            cmd1.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                            cmd1.Parameters.AddWithValue("@QueryType", "INSERT_BATCH_LIST");
                            int j = cmd1.ExecuteNonQuery();
                            if (j > 0)
                            {
                                objResp.vErrorCode = 200;
                                objResp.vErrorMsg = "Success";
                            }
                            else
                            {
                                objResp.vErrorCode = 400;
                                objResp.vErrorMsg = "Batch List inserting Failed";
                                return objResp;
                            }
                        }
                        objResp.vErrorCode = 200;
                        objResp.vErrorMsg = "Success";
                    }
                    else
                    {
                        objResp.vErrorCode = 400;
                        objResp.vErrorMsg = "Batch creating failed ";
                        return objResp;
                    }
                }
            }
            catch (Exception exp)
            {
                objResp.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Make_New_Batch", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Make_New_Batch");
            return objResp;
        }

        public clsBatch Fn_Delete_Batch(clsBatch objReq)
        {
            var objResp = new clsBatch();
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Request", "Fn_Delete_Batch");
            try
            {
                if (objReq.LotNo == 0)
                {
                    objResp.vErrorCode = 400;
                    objResp.vErrorMsg = "LotNo is empty";
                }
                else if (objReq.CreatedBy == 0)
                {
                    objResp.vErrorCode = 400;
                    objResp.vErrorMsg = "Login id is empty";
                }
                else
                {
                    if (Con.State == ConnectionState.Broken)
                    { Con.Close(); }
                    if (Con.State == ConnectionState.Closed)
                    { Con.Open(); }



                    SqlCommand cmd = new SqlCommand("USP_FABRIC_BATCH", Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BatchNo", objReq.BatchNo);
                    cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                    cmd.Parameters.AddWithValue("@QueryType", "DELETE_BATCH");
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {

                        objResp.vErrorCode = 200;
                        objResp.vErrorMsg = "Success";
                    }
                    else
                    {
                        objResp.vErrorCode = 400;
                        objResp.vErrorMsg = "Batch deleting failed ";
                        return objResp;
                    }
                }
            }
            catch (Exception exp)
            {
                objResp.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Delete_Batch", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Delete_Batch");
            return objResp;
        }

        public List<clsBatch> Fn_Get_Batch(clsBatch objReq)
        {
            var objResp = new List<clsBatch>();
            var obj = new clsBatch();
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Request", "Fn_Get_Batch");
            try
            {
                string strSql = "SELECT BatchNo, LotNo, CreatedBy, CreatedOn FROM Fabric_Batch WHERE 1=1";
                if (objReq.LotNo != 0)
                {
                    strSql = strSql + " AND LotNo = @LotNo";
                }
                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;

                if (objReq.LotNo != 0)
                {
                    cmd.Parameters.AddWithValue("@LotNo", objReq.LotNo);
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsBatch();
                        // obj.StyleCode = Convert.ToString(ds.Tables[0].Rows[i]["StyleCode"]);
                        // obj.ItemCode = Convert.ToString(ds.Tables[0].Rows[i]["ItemCode"]);
                        obj.BatchNo = Convert.ToInt32(ds.Tables[0].Rows[i]["BatchNo"]);
                        obj.LotNo = Convert.ToInt32(ds.Tables[0].Rows[i]["LotNo"]);
                        obj.CreatedBy = Convert.ToInt32(ds.Tables[0].Rows[i]["CreatedBy"]);
                        obj.CreatedOn = Convert.ToString(ds.Tables[0].Rows[i]["CreatedOn"]);
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
                Logger.WriteLog("Function Name : Fn_Get_Batch", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorCode = 500;
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Get_Batch");
            return objResp;
        }

        public List<clsBatchList> Fn_Get_BatchList(clsBatchList objReq)
        {
            var objResp = new List<clsBatchList>();
            var obj = new clsBatchList();
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Request", "Fn_Get_BatchList");
            try
            {
                string strSql = "Select BatchDetailId, BatchNo, RollNo, Quantity, BatchStatus, CreatedBy, CreatedOn from Fabric_BatchList WHERE 1=1";
                if (objReq.BatchNo != 0)
                {
                    strSql = strSql + " AND BatchNo = @BatchNo";
                }
                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;

                if (objReq.BatchNo != 0)
                {
                    cmd.Parameters.AddWithValue("@BatchNo", objReq.BatchNo);
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsBatchList();

                        obj.BatchDetailId = Convert.ToInt32(ds.Tables[0].Rows[i]["BatchDetailId"]);
                        obj.BatchNo = Convert.ToInt32(ds.Tables[0].Rows[i]["BatchNo"]);
                        obj.RollNo = Convert.ToInt32(ds.Tables[0].Rows[i]["RollNo"]);
                        obj.Quantity = Convert.ToInt32(ds.Tables[0].Rows[i]["Quantity"]);
                        obj.BatchStatus = Convert.ToString(ds.Tables[0].Rows[i]["BatchStatus"]);
                        obj.CreatedBy = Convert.ToInt32(ds.Tables[0].Rows[i]["CreatedBy"]);
                        obj.CreatedOn = Convert.ToString(ds.Tables[0].Rows[i]["CreatedOn"]);
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
                Logger.WriteLog("Function Name : Fn_Get_Batch", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorCode = 500;
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Get_Batch");
            return objResp;
        }

        public List<clsQADefects> Fn_Get_Fabric_Defects_Master(clsQADefects objReq)
        {
            var objResp = new List<clsQADefects>();
            var obj = new clsQADefects();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Get_Fabric_Defects_Master");
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT DISTINCT DefectID, Defect from FabricDefectMaster WHERE 1=1";
                if (!String.IsNullOrWhiteSpace(objReq.Defects))
                {
                    strSql = strSql + " AND Defect = @Defect";
                }

                strSql = strSql + " ORDER BY Defect ";


                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;
                if (!String.IsNullOrWhiteSpace(objReq.Defects))
                {
                    cmd.Parameters.AddWithValue("@Defect", objReq.Defects);
                }


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;

                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsQADefects();
                        obj.ID = Convert.ToInt64(ds.Tables[0].Rows[i]["DefectID"]);
                        obj.Defects = Convert.ToString(ds.Tables[0].Rows[i]["Defect"]);

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
                Logger.WriteLog("Function Name : Fn_Get_Fabric_Defects_Master", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Get_Fabric_Defects_Master");
            return objResp;
        }

        #region Start Fn_Add_Fabric_Defect_Inspection 07-SEP_2026

        public Fabric_Defect_Inspection Fn_Add_Fabric_Defect_Inspection(Fabric_Defect_Inspection objReq)
        {
            var objResp = new Fabric_Defect_Inspection();
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Request", "Fn_Add_Fabric_Defect_Inspection");
            try
            {
                if (String.IsNullOrWhiteSpace(objReq.DefectLocation))
                {
                    objResp.vErrorCode = 400;
                    objResp.vErrorMsg = "Please select location";
                }
                else if (string.IsNullOrWhiteSpace(objReq.DefectList))
                {
                    objResp.vErrorCode = 400;
                    objResp.vErrorMsg = "Please select one defect";
                }
                else if (objReq.PositionMTR == 0)
                {
                    objResp.vErrorCode = 400;
                    objResp.vErrorMsg = "Please enter Position";
                }
                else if (objReq.PenaltyPoints == 0)
                {
                    objResp.vErrorCode = 400;
                    objResp.vErrorMsg = "Please enter penalty points";
                }
                else
                {
                    if (Con.State == ConnectionState.Broken) { Con.Close(); }
                    if (Con.State == ConnectionState.Closed) { Con.Open(); }

                    string arrDefect = objReq.DefectList;

                    // Split comma-separated Defect IDs
                    string[] defectIds = arrDefect.Split(',');

                    foreach (string defectId in defectIds)
                    {
                        //if (string.IsNullOrWhiteSpace(defectId))
                        //    continue;
                        Int64 mxId = Fn_Get_MXID("Fabric_Defect_Inspection", "FabDefectId");
                        objReq.FabDefectId = Convert.ToInt32(mxId);

                        SqlCommand cmd = new SqlCommand("USP_FABRIC_BATCH", Con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FabDefectId", objReq.FabDefectId);
                        cmd.Parameters.AddWithValue("@BatchDetailId", objReq.BatchDetailId);
                        cmd.Parameters.AddWithValue("@DefectID", Convert.ToInt32(defectId));
                        cmd.Parameters.AddWithValue("@PositionMTR", objReq.PositionMTR);
                        cmd.Parameters.AddWithValue("@PenaltyPoints", objReq.PenaltyPoints);
                        cmd.Parameters.AddWithValue("@DefectLocation", objReq.DefectLocation);
                        cmd.Parameters.AddWithValue("@FabDefect_Image", objReq.FabDefect_Image);
                        cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                        cmd.Parameters.AddWithValue("@QueryType", "INSERT_FAB_DEFECT");
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
                            objResp.vErrorMsg = "Inserting error";
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                objResp.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Add_Fabric_Defect_Inspection", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Add_Fabric_Defect_Inspection");
            return objResp;
        }

        #endregion End Fn_Add_Fabric_Defect_Inspection 07-SEP_2026

        #region Start Fn_Get_Fabric_Defect_Inspection 08-SEP_2026

        public List<Fabric_Defect_Inspection> Fn_Get_Fabric_Defect_Inspection(Fabric_Defect_Inspection objReq)
        {
            var objResp = new List<Fabric_Defect_Inspection>();
            var obj = new Fabric_Defect_Inspection();
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Request", "Fn_Get_Fabric_Defect_Inspection");
            try
            {
                if (Con.State == ConnectionState.Broken) { Con.Close(); }
                if (Con.State == ConnectionState.Closed) { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_FABRIC_BATCH", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BatchDetailId", objReq.BatchDetailId);
                cmd.Parameters.AddWithValue("@QueryType", "SELECT_FAB_DEFECT");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;

                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new Fabric_Defect_Inspection();
                        obj.FabDefectId = Convert.ToInt32(ds.Tables[0].Rows[i]["FabDefectId"]);
                        obj.BatchDetailId = Convert.ToInt32(ds.Tables[0].Rows[i]["BatchDetailId"]);
                        obj.DefectLocation = Convert.ToString(ds.Tables[0].Rows[i]["DefectLocation"]);
                        obj.DefectList = Convert.ToString(ds.Tables[0].Rows[i]["Defect"]);
                        obj.DefectID = Convert.ToInt32(ds.Tables[0].Rows[i]["DefectID"]);
                        obj.PositionMTR = Convert.ToDecimal(ds.Tables[0].Rows[i]["PositionMTR"]);
                        obj.PenaltyPoints = Convert.ToInt32(ds.Tables[0].Rows[i]["PenaltyPoints"]);
                        obj.FabDefect_Image = Convert.ToString(ds.Tables[0].Rows[i]["FabDefect_Image"]);
                        obj.CreatedBy = Convert.ToInt32(ds.Tables[0].Rows[i]["CreatedBy"]);
                        obj.CreatedOn = Convert.ToString(ds.Tables[0].Rows[i]["CreatedOn"]);

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
                Logger.WriteLog("Function Name : Fn_Get_Fabric_Defect_Inspection", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Get_Fabric_Defect_Inspection");
            return objResp;
        }

        #endregion End Fn_Get_Fabric_Defect_Inspection 08-SEP_2026

        public Fabric_Defect_Inspection Fn_Delete_Fabric_Defect_Inspection(Fabric_Defect_Inspection objReq)
        {
            var objResp = new Fabric_Defect_Inspection();
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Request", "Fn_Delete_Fabric_Defect_Inspection");
            try
            {
                if (objReq.FabDefectId == 0)
                {
                    objResp.vErrorCode = 400;
                    objResp.vErrorMsg = "Please send FabDefectId";
                }
                else if (objReq.BatchDetailId == 0)
                {
                    objResp.vErrorCode = 400;
                    objResp.vErrorMsg = "Please send BatchDetailId";
                }
                else
                {
                    if (Con.State == ConnectionState.Broken) { Con.Close(); }
                    if (Con.State == ConnectionState.Closed) { Con.Open(); }

                    SqlCommand cmd = new SqlCommand("USP_FABRIC_BATCH", Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FabDefectId", objReq.FabDefectId);
                    cmd.Parameters.AddWithValue("@BatchDetailId", objReq.BatchDetailId);
                    cmd.Parameters.AddWithValue("@QueryType", "DELETE_FAB_DEFECT");
                    int i = cmd.ExecuteNonQuery();
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
            }
            catch (Exception exp)
            {
                objResp.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Delete_Fabric_Defect_Inspection", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Delete_Fabric_Defect_Inspection");
            return objResp;
        }

        #region Start Fn_Add_Fabric_Defect_CheckPoint 09-SEP-2026

        public Fabric_Defect_CheckPoint Fn_Add_Fabric_Defect_CheckPoint(Fabric_Defect_CheckPoint objReq)
        {
            var objResp = new Fabric_Defect_CheckPoint();
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Request", "Fn_Add_Fabric_Defect_CheckPoint");
            try
            {
                if (objReq.BatchDetailId == 0)
                {
                    objResp.vErrorCode = 400;
                    objResp.vErrorMsg = "Please Send BatchDetailId";
                }
                else if (objReq.ActualLength == 0)
                {
                    objResp.vErrorCode = 400;
                    objResp.vErrorMsg = "Please Enter Actual Length";
                }
                else if (objReq.ActualWidth == 0)
                {
                    objResp.vErrorCode = 400;
                    objResp.vErrorMsg = "Please Enter Actual Width";
                }
                else if (objReq.SupplierLength == 0)
                {
                    objResp.vErrorCode = 400;
                    objResp.vErrorMsg = "Please Enter Supplier Length";
                }
                else if (objReq.CutStart == 0)
                {
                    objResp.vErrorCode = 400;
                    objResp.vErrorMsg = "Please Enter Cut Start";
                }
                else if (objReq.CutMid == 0)
                {
                    objResp.vErrorCode = 400;
                    objResp.vErrorMsg = "Please Enter Cut Mid";
                }
                else if (objReq.CutEnd == 0)
                {
                    objResp.vErrorCode = 400;
                    objResp.vErrorMsg = "Please Enter CutEnd";
                }
                else
                {
                    if (Con.State == ConnectionState.Broken) { Con.Close(); }
                    if (Con.State == ConnectionState.Closed) { Con.Open(); }

                    Int64 mxId = Fn_Get_MXID("Fabric_Defect_CheckPoint", "InfoId");
                    objReq.InfoId = Convert.ToInt32(mxId);

                    SqlCommand cmd = new SqlCommand("USP_FABRIC_BATCH", Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@InfoId", objReq.InfoId);
                    cmd.Parameters.AddWithValue("@BatchDetailId", objReq.BatchDetailId);
                    cmd.Parameters.AddWithValue("@ActualLength", objReq.ActualLength);
                    cmd.Parameters.AddWithValue("@ActualWeight", objReq.ActualWeight);
                    cmd.Parameters.AddWithValue("@ActualWidth", objReq.ActualWidth);
                    cmd.Parameters.AddWithValue("@GSM", objReq.GSM);
                    cmd.Parameters.AddWithValue("@SupplierWeight", objReq.SupplierWeight);
                    cmd.Parameters.AddWithValue("@SupplierLength", objReq.SupplierLength);
                    cmd.Parameters.AddWithValue("@CutStart", objReq.CutStart);
                    cmd.Parameters.AddWithValue("@CutMid", objReq.CutMid);
                    cmd.Parameters.AddWithValue("@CutEnd", objReq.CutEnd);
                    cmd.Parameters.AddWithValue("@Bowing", objReq.Bowing);
                    cmd.Parameters.AddWithValue("@Skewing", objReq.Skewing);
                    cmd.Parameters.AddWithValue("@Shade", objReq.Shade);
                    cmd.Parameters.AddWithValue("@Descriptions", objReq.Descriptions);
                    cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                    cmd.Parameters.AddWithValue("@QueryType", "INSERT_FAB_DEFECT_CHECK_POINT");
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
                        objResp.vErrorMsg = "Inserting error";
                    }
                }
            }
            catch (Exception exp)
            {
                objResp.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Add_Fabric_Defect_CheckPoint", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Add_Fabric_Defect_CheckPoint");
            return objResp;
        }

        #endregion End Fn_Add_Fabric_Defect_CheckPoint 09-SEP-2026
    }
}