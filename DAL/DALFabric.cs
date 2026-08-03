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
                        cmd.Parameters.AddWithValue("@StyleCode", objReq.StyleCode);
                        cmd.Parameters.AddWithValue("@ItemCode", objReq.ItemCode);
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

                string strSql = "Select StyleCode, ItemCode, Descriptions, Contents, Mill, FabricColor, FabricCC, Width, ";
                strSql = strSql + " WidthTolerance, OrderRollLength, OrderRollLengthTolerance, GSM, GSMTolerance, ";
                strSql = strSql + " OrderShrinkageWarpLength, OrderShrinkageWaftWidth, TotalQuantity, Unit, MarkerType,";
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

                string strSql = "Select InHouseId, StyleCode, ItemCode, RollNo, Quantity, Unit, Width, ShadeName, GSM, Shrinkage,";
                strSql = strSql + " CreatedBy, Format(CreatedOn, 'dd-MMM-yyyy') AS CreatedOn from Fabric_Inhouse WHERE 1=1 ";
                if (!String.IsNullOrWhiteSpace(objReq.StyleCode))
                {
                    strSql = strSql + " AND StyleCode = @StyleCode ";
                }
                if (!String.IsNullOrWhiteSpace(objReq.ItemCode))
                {
                    strSql = strSql + " AND ItemCode = @ItemCode ";
                }
                strSql = strSql + " ORDER BY StyleCode, ItemCode, RollNo ";

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
                        obj = new FabricInhouseList();
                       // obj.StyleCode = Convert.ToString(ds.Tables[0].Rows[i]["StyleCode"]);
                       // obj.ItemCode = Convert.ToString(ds.Tables[0].Rows[i]["ItemCode"]);
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
    }
}