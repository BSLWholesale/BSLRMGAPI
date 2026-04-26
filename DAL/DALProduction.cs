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
    public class DALProduction
    {
        SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["BSL"].ConnectionString);
        DALOrder _DALOrder = new DALOrder();
        Int64 mxID = 0;

        public Int64 Fn_Get_MXID(string strTBLName, string strFieldName, string strCriteria)
        {
            try
            {
                //if (Con.State == ConnectionState.Broken)
                //{ Con.Close(); }
                //if (Con.State == ConnectionState.Closed)
                //{ Con.Open(); }

                string strSql = "SELECT MAX(" + strFieldName + ") AS ID FROM " + strTBLName + " WHERE 1=1";
                if (!String.IsNullOrWhiteSpace(strCriteria))
                {
                    strSql = strSql + strCriteria;
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

        public clsProductionMaster Fn_Insert_Production_Order(clsProductionMaster objReq)
        {
            var objResp = new clsProductionMaster();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Insert_Production_Order");
            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_PRODUCTION", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProductionOrderNo", objReq.ProductionOrderNo);
                cmd.Parameters.AddWithValue("@OrderDate", objReq.OrderDate);
                cmd.Parameters.AddWithValue("@ProductionDeliveryDate", objReq.ProductionDeliveryDate);
                cmd.Parameters.AddWithValue("@Merchandiser", objReq.Merchandiser);
                cmd.Parameters.AddWithValue("@SalesOrderNo", objReq.SalesOrderNo);
                cmd.Parameters.AddWithValue("@PONo", objReq.PONo);
                cmd.Parameters.AddWithValue("FabIndNo", objReq.FabIndNo);
                cmd.Parameters.AddWithValue("@OrderQty", objReq.OrderQty);
                cmd.Parameters.AddWithValue("@StyleNo", objReq.StyleNo);
                cmd.Parameters.AddWithValue("@StyleName", objReq.StyleName);
                cmd.Parameters.AddWithValue("@Buyer", objReq.Buyer);
                cmd.Parameters.AddWithValue("@Brand", objReq.Brand);
                cmd.Parameters.AddWithValue("@PlantName", objReq.PlantName);
                cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                cmd.Parameters.AddWithValue("@QueryType", "InsertMaster");
                int i = 0;
                i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    objResp.vErrorCode = 200;
                    objResp.vErrorMsg = "Success";

                    foreach (clsProductionDetail _oList in objReq._ODetail)
                    {
                        SqlCommand cm1 = new SqlCommand("USP_PRODUCTION", Con);
                        cm1.CommandType = CommandType.StoredProcedure;
                        cm1.Parameters.AddWithValue("@ID", _oList.ID);
                        cm1.Parameters.AddWithValue("@ProductionOrderNo", objReq.ProductionOrderNo);
                        cm1.Parameters.AddWithValue("@ShadeNo", _oList.ShadeNo);
                        cm1.Parameters.AddWithValue("@QualityNo", _oList.QualityNo);
                        cm1.Parameters.AddWithValue("@Color", _oList.Color);
                        cm1.Parameters.AddWithValue("@SizeName", _oList.SizeName);
                        cm1.Parameters.AddWithValue("@Qty", _oList.Qty);
                        cm1.Parameters.AddWithValue("@CreatedBy", _oList.CreatedBy);
                        cm1.Parameters.AddWithValue("@QueryType", "InsertDetail");
                        int j = cm1.ExecuteNonQuery();
                        if (j > 0)
                        {
                            objResp.vErrorMsg = "Success";
                        }
                        else
                        {
                            objResp.vErrorMsg = "Production detail inserting failed ";
                            return objResp;
                        }
                    }
                }
                else
                {
                    objResp.vErrorCode = 400;
                    objResp.vErrorMsg = "Production order inserting failed";
                }
            }
            catch (Exception exp)
            {
                objResp.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Insert_Production_Order", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Insert_Production_Order");
            return objResp;
        }

        public List<clsProductionMaster> Fn_Get_Production_Order(clsProductionMaster objReq)
        {
            var objResp = new List<clsProductionMaster>();
            var obj = new clsProductionMaster();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Get_Production_Order");
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT ProductionOrderNo, FORMAT(OrderDate, 'dd-MMM-yyy') AS OrderDate, ProductionDeliveryDate, Merchandiser, SalesOrderNo, PONo,";
                strSql = strSql + " FabIndNo, OrderQty, StyleNo, StyleName, Buyer, Brand, PlantName FROM ProductionMaster WHERE 1=1";
                if (objReq.ProductionOrderNo != 0 && objReq.ProductionOrderNo != null)
                {
                    strSql = strSql + " AND ProductionOrderNo = @ProductionOrderNo";
                }
                if (!String.IsNullOrWhiteSpace(objReq.OrderDate))
                {
                    strSql = strSql + " AND OrderDate = @OrderDate";
                }
                if (!String.IsNullOrWhiteSpace(objReq.SalesOrderNo))
                {
                    strSql = strSql + " AND SalesOrderNo = @SalesOrderNo";
                }
                if (!String.IsNullOrWhiteSpace(objReq.StyleNo))
                {
                    strSql = strSql + " AND StyleNo = @StyleNo";
                }
                if (!String.IsNullOrWhiteSpace(objReq.StyleName))
                {
                    strSql = strSql + " AND StyleName = @StyleName";
                }
                if (!String.IsNullOrWhiteSpace(objReq.Buyer))
                {
                    strSql = strSql + " AND Buyer = @Buyer";
                }
                if (!String.IsNullOrWhiteSpace(objReq.Brand))
                {
                    strSql = strSql + " AND Brand = @Brand";
                }

                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;
                if (objReq.ProductionOrderNo != 0 && objReq.ProductionOrderNo != null)
                {
                    cmd.Parameters.AddWithValue("@ProductionOrderNo", objReq.ProductionOrderNo);
                }
                if (!String.IsNullOrWhiteSpace(objReq.OrderDate))
                {
                    cmd.Parameters.AddWithValue("@OrderDate", objReq.OrderDate);
                }
                if (!String.IsNullOrWhiteSpace(objReq.SalesOrderNo))
                {
                    cmd.Parameters.AddWithValue("@SalesOrderNo", objReq.SalesOrderNo);
                }
                if (!String.IsNullOrWhiteSpace(objReq.StyleNo))
                {
                    cmd.Parameters.AddWithValue("@StyleNo", objReq.StyleNo);
                }
                if (!String.IsNullOrWhiteSpace(objReq.StyleName))
                {
                    cmd.Parameters.AddWithValue("@StyleName", objReq.StyleName);
                }
                if (!String.IsNullOrWhiteSpace(objReq.Buyer))
                {
                    cmd.Parameters.AddWithValue("@Buyer", objReq.Buyer);
                }
                if (!String.IsNullOrWhiteSpace(objReq.Brand))
                {
                    cmd.Parameters.AddWithValue("@Brand", objReq.Brand);
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsProductionMaster();
                        obj.ProductionOrderNo = Convert.ToInt64(ds.Tables[0].Rows[i]["ProductionOrderNo"]);
                        obj.OrderDate = Convert.ToString(ds.Tables[0].Rows[i]["OrderDate"]);
                        obj.ProductionDeliveryDate = Convert.ToString(ds.Tables[0].Rows[i]["ProductionDeliveryDate"]);

                        obj.Merchandiser = Convert.ToString(ds.Tables[0].Rows[i]["Merchandiser"]);
                        obj.SalesOrderNo = Convert.ToString(ds.Tables[0].Rows[i]["SalesOrderNo"]);
                        obj.PONo = Convert.ToString(ds.Tables[0].Rows[i]["PONo"]);
                        obj.FabIndNo = Convert.ToInt16(ds.Tables[0].Rows[i]["FabIndNo"]);
                        obj.OrderQty = Convert.ToInt16(ds.Tables[0].Rows[i]["OrderQty"]);
                        obj.StyleNo = Convert.ToString(ds.Tables[0].Rows[i]["StyleNo"]);
                        obj.StyleName = Convert.ToString(ds.Tables[0].Rows[i]["StyleName"]);
                        obj.Buyer = Convert.ToString(ds.Tables[0].Rows[i]["Buyer"]);
                        obj.Brand = Convert.ToString(ds.Tables[0].Rows[i]["Brand"]);
                        obj.PlantName = Convert.ToString(ds.Tables[0].Rows[i]["PlantName"]);

                        var objpDetail = new clsProductionDetail();
                        objpDetail.ProductionOrderNo = obj.ProductionOrderNo;
                        obj._ODetail = Fn_Get_Production_Detail(objpDetail);

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
                Logger.WriteLog("Function Name : Fn_Get_Production_Order", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp[0].vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Get_Production_Order");
            return objResp;
        }

        public List<clsProductionDetail> Fn_Get_Production_Detail(clsProductionDetail objReq)
        {
            var objResp = new List<clsProductionDetail>();
            var obj = new clsProductionDetail();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Get_Production_Detail");
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT ID, ProductionOrderNo, ShadeNo, QualityNo, Color, SizeName, Qty FROM ProductionDetail WHERE 1=1";

                if (objReq.ProductionOrderNo != 0)
                {
                    strSql = strSql + " AND ProductionOrderNo = @ProductionOrderNo";
                }

                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;
                if (objReq.ProductionOrderNo != 0)
                {
                    cmd.Parameters.AddWithValue("@ProductionOrderNo", objReq.ProductionOrderNo);
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsProductionDetail();
                        obj.ID = Convert.ToInt32(ds.Tables[0].Rows[i]["ID"]);
                        obj.ProductionOrderNo = Convert.ToInt64(ds.Tables[0].Rows[i]["ProductionOrderNo"]);
                        obj.ShadeNo = Convert.ToString(ds.Tables[0].Rows[i]["ShadeNo"]);
                        obj.QualityNo = Convert.ToString(ds.Tables[0].Rows[i]["QualityNo"]);

                        obj.Color = Convert.ToString(ds.Tables[0].Rows[i]["Color"]);
                        obj.SizeName = Convert.ToString(ds.Tables[0].Rows[i]["SizeName"]);
                        obj.Qty = Convert.ToInt16(ds.Tables[0].Rows[i]["Qty"]);

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
                Logger.WriteLog("Function Name : Fn_Get_Production_Detail", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp[0].vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Get_Production_Detail");
            return objResp;
        }

        public clsProductionMaster Fn_Update_Production(clsProductionMaster objReq)
        {
            var objResp = new clsProductionMaster();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Update_Production");
            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_PRODUCTION", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProductionOrderNo", objReq.ProductionOrderNo);
                cmd.Parameters.AddWithValue("@OrderDate", objReq.OrderDate);
                cmd.Parameters.AddWithValue("@Code", objReq.ProductionDeliveryDate);
                cmd.Parameters.AddWithValue("@Merchandiser", objReq.Merchandiser);
                cmd.Parameters.AddWithValue("@SalesOrderNo", objReq.SalesOrderNo);
                cmd.Parameters.AddWithValue("@Merchandiser", objReq.PONo);
                cmd.Parameters.AddWithValue("@Merchandiser", objReq.FabIndNo);
                cmd.Parameters.AddWithValue("@Merchandiser", objReq.OrderQty);
                cmd.Parameters.AddWithValue("@Merchandiser", objReq.StyleNo);
                cmd.Parameters.AddWithValue("@Merchandiser", objReq.StyleName);
                cmd.Parameters.AddWithValue("@Merchandiser", objReq.Buyer);
                cmd.Parameters.AddWithValue("@Merchandiser", objReq.Brand);
                cmd.Parameters.AddWithValue("@Merchandiser", objReq.PlantName);
                cmd.Parameters.AddWithValue("@ModifiedBy", objReq.ModifiedBy);
                cmd.Parameters.AddWithValue("@QueryType", "InsertUpdate");
                int i = 0;
                i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    objResp.vErrorCode = 200;
                    objResp.vErrorMsg = "Success";

                    foreach (clsProductionDetail _oList in objReq._ODetail)
                    {
                        SqlCommand cm1 = new SqlCommand("USP_PRODUCTION", Con);
                        cm1.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ProductionOrderNo", objReq.ProductionOrderNo);
                        cm1.Parameters.AddWithValue("@SummaryofAchievement", _oList.ShadeNo);
                        cm1.Parameters.AddWithValue("@MonitorySavings", _oList.QualityNo);
                        cm1.Parameters.AddWithValue("@NonMonetary", _oList.Color);
                        cm1.Parameters.AddWithValue("@SummaryofAchievement", _oList.SizeName);
                        cm1.Parameters.AddWithValue("@MonitorySavings", _oList.Qty);
                        cm1.Parameters.AddWithValue("@NonMonetary", _oList.CreatedBy);
                        cm1.Parameters.AddWithValue("@QueryType", "InsertDetail");
                        int j = cm1.ExecuteNonQuery();
                        if (j > 0)
                        {
                            objResp.vErrorMsg = "Success";
                        }
                        else
                        {
                            objResp.vErrorMsg = "Achievements list-1 inserting failed ";
                            return objResp;
                        }
                    }
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
                Logger.WriteLog("Function Name : Fn_Update_Production", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Update_Production");
            return objResp;
        }

        #region Start style 19-Jan-2026

        public clsStyle Fn_Insert_Style(clsStyle objReq)
        {
            var objResp = new clsStyle();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Insert_Style");
            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_PRODUCTION", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StyleNo", objReq.StyleCode);
                cmd.Parameters.AddWithValue("@StyleName", objReq.StyleName);
                cmd.Parameters.AddWithValue("@Description", objReq.Description);
                cmd.Parameters.AddWithValue("@CustomerID", objReq.CustomerID);
                cmd.Parameters.AddWithValue("@GridName", objReq.GridName);
                cmd.Parameters.AddWithValue("@CategoryID", objReq.CategoryID);
                cmd.Parameters.AddWithValue("@SeasonID", objReq.SeasonID);
                cmd.Parameters.AddWithValue("@StyleNotes", objReq.StyleNotes);
                cmd.Parameters.AddWithValue("@Merchant", objReq.Merchant);
                cmd.Parameters.AddWithValue("@PatternMaster", objReq.PatternMaster);
                cmd.Parameters.AddWithValue("@DesignNo", objReq.DesignNo);
                cmd.Parameters.AddWithValue("@StyleType", objReq.StyleType);
                cmd.Parameters.AddWithValue("@MultiFitOB", objReq.MultiFitOB);
                cmd.Parameters.AddWithValue("@IsActive", objReq.IsActive);
                cmd.Parameters.AddWithValue("@FabricWashtype", objReq.FabricWashtype);
                cmd.Parameters.AddWithValue("@GarmentWashtype", objReq.GarmentWashtype);
                cmd.Parameters.AddWithValue("@BundleType", objReq.BundleType);
                cmd.Parameters.AddWithValue("@AssemblyType", objReq.AssemblyType);
                cmd.Parameters.AddWithValue("@AssemblyPCS", objReq.AssemblyPCS);
                cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                cmd.Parameters.AddWithValue("@QueryType", "InsertStyle");
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
                Logger.WriteLog("Function Name : Fn_Insert_Style", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Insert_Style");
            return objResp;
        }

        public List<clsStyle> Fn_Get_Style(clsStyle objReq)
        {
            var objResp = new List<clsStyle>();
            var obj = new clsStyle();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Get_Style");
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "Select SM.StyleCode, SM.StyleName, SM.Descriptions, SM.CustomerID, CM.vName AS Customer, SM.GridName,";
                strSql = strSql + " SM.CategoryID, CAT.Category,SM.SeasonID,S.Season,";
                strSql = strSql + " SM.StyleNotes, SM.Merchant, SM.PatternMaster, SM.DesignNo, SM.StyleType, SM.MultiFitOB, SM.IsActive,";
                strSql = strSql + " SM.FabricWashtype, SM.GarmentWashtype, SM.BundleType, SM.AssemblyType, SM.AssemblyPCS, SM.CreatedBy, FORMAT(SM.CreatedOn, 'dd-MMM-yyyy') AS CreatedOn";
                strSql = strSql + " FROM StyleMaster SM INNER JOIN CustomerMaster CM ON SM.CustomerID = CM.ID";
                strSql = strSql + " INNER JOIN CategoryMaster CAT ON SM.CategoryID = CAT.ID";
                strSql = strSql + " INNER JOIN SeasonMaster S ON SM.SeasonID = S.ID WHERE 1 = 1";

                if (!String.IsNullOrWhiteSpace(objReq.StyleCode))
                {
                    strSql = strSql + " AND SM.StyleCode = @StyleCode";
                }
                if (!String.IsNullOrWhiteSpace(objReq.StyleName))
                {
                    strSql = strSql + " AND SM.StyleName LIKE '%@StyleName%'";
                }
                strSql = strSql + " ORDER BY SM.CreatedOn DESC ";

                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;
                if (!String.IsNullOrWhiteSpace(objReq.StyleCode))
                {
                    cmd.Parameters.AddWithValue("@StyleCode", objReq.StyleCode);
                }
                if (!String.IsNullOrWhiteSpace(objReq.StyleName))
                {
                    cmd.Parameters.AddWithValue("@StyleName", objReq.StyleName);
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsStyle();
                        obj.StyleCode = Convert.ToString(ds.Tables[0].Rows[i]["StyleCode"]);
                        obj.StyleName = Convert.ToString(ds.Tables[0].Rows[i]["StyleName"]);
                        obj.Description = Convert.ToString(ds.Tables[0].Rows[i]["Descriptions"]);
                        obj.CustomerID = Convert.ToInt16(ds.Tables[0].Rows[i]["CustomerID"]);
                        obj.Customer = Convert.ToString(ds.Tables[0].Rows[i]["Customer"]);
                        obj.GridName = Convert.ToString(ds.Tables[0].Rows[i]["GridName"]);
                        obj.CategoryID = Convert.ToInt16(ds.Tables[0].Rows[i]["CategoryID"]);
                        obj.Category = Convert.ToString(ds.Tables[0].Rows[i]["Category"]);
                        obj.SeasonID = Convert.ToInt16(ds.Tables[0].Rows[i]["SeasonID"]);
                        obj.Season = Convert.ToString(ds.Tables[0].Rows[i]["Season"]);
                        obj.StyleNotes = Convert.ToString(ds.Tables[0].Rows[i]["StyleNotes"]);
                        obj.Merchant = Convert.ToString(ds.Tables[0].Rows[i]["Merchant"]);
                        obj.PatternMaster = Convert.ToString(ds.Tables[0].Rows[i]["PatternMaster"]);
                        obj.DesignNo = Convert.ToString(ds.Tables[0].Rows[i]["DesignNo"]);
                        obj.StyleType = Convert.ToBoolean(ds.Tables[0].Rows[i]["StyleType"]);
                        obj.MultiFitOB = Convert.ToBoolean(ds.Tables[0].Rows[i]["MultiFitOB"]);
                        obj.IsActive = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsActive"]);
                        obj.FabricWashtype = Convert.ToString(ds.Tables[0].Rows[i]["FabricWashtype"]);
                        obj.GarmentWashtype = Convert.ToString(ds.Tables[0].Rows[i]["GarmentWashtype"]);
                        obj.BundleType = Convert.ToString(ds.Tables[0].Rows[i]["BundleType"]);
                        obj.AssemblyType = Convert.ToString(ds.Tables[0].Rows[i]["AssemblyType"]);
                        obj.AssemblyPCS = Convert.ToString(ds.Tables[0].Rows[i]["AssemblyPCS"]);
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
                Logger.WriteLog("Function Name : Fn_Get_Style", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Get_Style");
            return objResp;
        }

        #endregion End Style 19-Jan-2026

        public List<clsAutoCompliteResponse> Fn_AutoComplete_Textbox(clsAutoCompliteRequest obj)
        {
            var objResp = new List<clsAutoCompliteResponse>();
            Logger.ErrorLog(JsonConvert.SerializeObject(obj), "Request", "Fn_AutoComplete_Textbox");
            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT Distinct " + obj.FieldName + " FROM " + obj.TableName + " WHERE 1=1";
                strSql = strSql + " AND " + obj.FieldName + " LIKE '%" + obj.SearchKeyword + "%' ";

                SqlCommand cmd = new SqlCommand(strSql, Con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        var objItem = new clsAutoCompliteResponse();
                        objItem.SearchKeyword = Convert.ToString(ds.Tables[0].Rows[i][0]);
                        objResp.Add(objItem);
                        i++;
                    }
                }
                else
                {

                }
                cmd.Dispose();
            }
            catch (Exception exp)
            {
                Logger.WriteLog("Function Name : Fn_AutoComplete_Textbox", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_AutoComplete_Textbox");
            return objResp;
        }

        #region Start Layer- Bundle 6-Feb-2026

        public clsBundleLayerMaster Fn_Insert_Bundle_Layer(clsBundleLayerMaster objReq)
        {
            var objResp = new clsBundleLayerMaster();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Insert_Bundle_Layer");
            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                if (objReq.LayID == 0 || objReq.LayID == null)
                {
                    string strCriteria = " AND OrderNo = '" + objReq.OrderNo + "'";
                    Fn_Get_MXID("BundleLayerMaster", "LayID", strCriteria);
                    objReq.LayID = mxID;
                }

                SqlCommand cmd = new SqlCommand("USP_BUNDLE_LAYER", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LayID", objReq.LayID);
                cmd.Parameters.AddWithValue("@Qty", objReq.Qty);
                cmd.Parameters.AddWithValue("@BundleLen", objReq.BundleLen);
                cmd.Parameters.AddWithValue("@CompileDate", objReq.CompileDate);
                cmd.Parameters.AddWithValue("@PrintDate", objReq.PrintDate);
                cmd.Parameters.AddWithValue("@StyleCode", objReq.StyleCode);
                cmd.Parameters.AddWithValue("@OrderNo", objReq.OrderNo);
                cmd.Parameters.AddWithValue("@Marker", objReq.Marker);
                cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                cmd.Parameters.AddWithValue("@QueryType", "InsertBundleLayer");
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
                    objResp.vErrorMsg = "Layer inserting Failed";
                }
            }
            catch (Exception exp)
            {
                objResp.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Insert_Bundle_Layer", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Insert_Bundle_Layer");
            return objResp;
        }

        public clsBundleLayerMaster Fn_Delete_Bundle_Layer(clsBundleLayerMaster objReq)
        {
            var objResp = new clsBundleLayerMaster();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Delete_Bundle_Layer");
            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_BUNDLE_LAYER", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LayID", objReq.LayID);
                cmd.Parameters.AddWithValue("@OrderNo", objReq.OrderNo);
                cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                cmd.Parameters.AddWithValue("@QueryType", "DeleteBundleLayer");
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
                Logger.WriteLog("Function Name : Fn_Delete_Bundle_Layer", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Delete_Bundle_Layer");
            return objResp;
        }

        public List<clsBundleLayerMaster> Fn_Get_Bundle_Layer(clsBundleLayerMaster objReq)
        {
            var objResp = new List<clsBundleLayerMaster>();
            var obj = new clsBundleLayerMaster();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Get_Bundle_Layer");
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT LayID, Qty, BundleLen, CompileDate, PrintDate, StyleCode, OrderNo, Marker,";
                strSql = strSql + " CreatedBy, FORMAT(CreatedOn, 'dd-MMM-yyy') AS CreatedOn FROM BundleLayerMaster WHERE 1=1";
                if (!String.IsNullOrWhiteSpace(objReq.StyleCode))
                {
                    strSql = strSql + " AND StyleCode = @StyleCode";
                }
                if (!String.IsNullOrWhiteSpace(objReq.OrderNo))
                {
                    strSql = strSql + " AND OrderNo = @OrderNo";
                }
                if (!String.IsNullOrWhiteSpace(objReq.Marker))
                {
                    strSql = strSql + " AND Marker = @Marker";
                }
                if (objReq.LayID != 0 && objReq.LayID != null)
                {
                    strSql = strSql + " AND LayID = @LayID ";
                }
                strSql = strSql + " ORDER BY LayID DESC ";

                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;
                if (!String.IsNullOrWhiteSpace(objReq.StyleCode))
                {
                    cmd.Parameters.AddWithValue("@StyleCode", objReq.StyleCode);
                }
                if (!String.IsNullOrWhiteSpace(objReq.OrderNo))
                {
                    cmd.Parameters.AddWithValue("@OrderNo", objReq.OrderNo);
                }
                if (!String.IsNullOrWhiteSpace(objReq.Marker))
                {
                    cmd.Parameters.AddWithValue("@Marker", objReq.Marker);
                }
                if (objReq.LayID != 0 && objReq.LayID != null)
                {
                    cmd.Parameters.AddWithValue("@LayID", objReq.LayID);
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsBundleLayerMaster();
                        obj.LayID = Convert.ToInt64(ds.Tables[0].Rows[i]["LayID"]);
                        obj.Qty = Convert.ToInt16(ds.Tables[0].Rows[i]["Qty"]);
                        string BundleLen = Convert.ToString(ds.Tables[0].Rows[i]["BundleLen"]);
                        if (BundleLen != "")
                        {
                            obj.BundleLen = Convert.ToDouble(ds.Tables[0].Rows[i]["BundleLen"]);
                        }
                        obj.CompileDate = Convert.ToString(ds.Tables[0].Rows[i]["CompileDate"]);
                        obj.PrintDate = Convert.ToString(ds.Tables[0].Rows[i]["PrintDate"]);
                        obj.StyleCode = Convert.ToString(ds.Tables[0].Rows[i]["StyleCode"]);
                        obj.OrderNo = Convert.ToString(ds.Tables[0].Rows[i]["OrderNo"]);
                        obj.CreatedBy = Convert.ToInt32(ds.Tables[0].Rows[i]["CreatedBy"]);
                        obj.CreatedOn = Convert.ToString(ds.Tables[0].Rows[i]["CreatedOn"]);
                        obj.Marker = Convert.ToString(ds.Tables[0].Rows[i]["Marker"]);
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
                Logger.WriteLog("Function Name : Fn_Get_Bundle_Layer", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Get_Bundle_Layer");
            return objResp;
        }

        #endregion End Layer- Bundle 6-Feb-2026

        #region Start Size- Bundle 7-Feb-2026

        public clsBundleSize Fn_Insert_Bundle_Size(clsBundleSize objReq)
        {
            var objResp = new clsBundleSize();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Insert_Bundle_Size");
            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                if (objReq.SizeSelectionID == 0 || objReq.SizeSelectionID == null)
                {
                    Fn_Get_MXID("BundleSizeSelection", "SizeSelectionID", "");
                    objReq.SizeSelectionID = mxID;
                }

                SqlCommand cmd = new SqlCommand("USP_BUNDLE_LAYER", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LayID", objReq.LayID);
                cmd.Parameters.AddWithValue("@SizeSelectionID", objReq.SizeSelectionID);
                cmd.Parameters.AddWithValue("@SizeName", objReq.SizeName);
                cmd.Parameters.AddWithValue("@SizeID", objReq.SizeID);
                cmd.Parameters.AddWithValue("@Freq", objReq.Freq);
                cmd.Parameters.AddWithValue("@OrderNo", objReq.OrderNo);
                cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                cmd.Parameters.AddWithValue("@QueryType", "InsertBundleSize");
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
                    objResp.vErrorMsg = "Size inserting Failed";
                }
            }
            catch (Exception exp)
            {
                objResp.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Insert_Bundle_Size", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Insert_Bundle_Size");
            return objResp;
        }

        public clsBundleSize Fn_Delete_Bundle_Size(clsBundleSize objReq)
        {
            var objResp = new clsBundleSize();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Delete_Bundle_Size");
            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_BUNDLE_LAYER", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SizeSelectionID", objReq.SizeSelectionID);
                cmd.Parameters.AddWithValue("@OrderNo", objReq.OrderNo);
                cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                cmd.Parameters.AddWithValue("@QueryType", "DeleteBundleSize");
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
                Logger.WriteLog("Function Name : Fn_Delete_Bundle_Size", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Delete_Bundle_Size");
            return objResp;
        }

        public List<clsBundleSize> Fn_Get_Bundle_Size(clsBundleSize objReq)
        {
            var objResp = new List<clsBundleSize>();
            var obj = new clsBundleSize();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Get_Bundle_Size");
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT SizeSelectionID, LayID, SizeName, Freq, SizeID,";
                strSql = strSql + " CreatedBy, FORMAT(CreatedOn, 'dd-MMM-yyy') AS CreatedOn FROM BundleSizeSelection WHERE 1=1";
                if (!String.IsNullOrWhiteSpace(objReq.SizeName))
                {
                    strSql = strSql + " AND SizeName = @SizeName";
                }
                if (objReq.LayID != 0 && objReq.LayID != null)
                {
                    strSql = strSql + " AND LayID = @LayID ";
                }
                if (objReq.SizeSelectionID != 0 && objReq.SizeSelectionID != null)
                {
                    strSql = strSql + " AND LayID = @LayID ";
                }
                if (!String.IsNullOrWhiteSpace(objReq.OrderNo))
                {
                    strSql = strSql + " AND OrderNo = @OrderNo";
                }
                strSql = strSql + " ORDER BY SizeName ASC ";

                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;
                if (!String.IsNullOrWhiteSpace(objReq.SizeName))
                {
                    cmd.Parameters.AddWithValue("@SizeName", objReq.SizeName);
                }
                if (objReq.LayID != 0 && objReq.LayID != null)
                {
                    cmd.Parameters.AddWithValue("@LayID", objReq.LayID);
                }
                if (objReq.SizeSelectionID != 0 && objReq.SizeSelectionID != null)
                {
                    cmd.Parameters.AddWithValue("@SizeSelectionID", objReq.SizeSelectionID);
                }
                if (!String.IsNullOrWhiteSpace(objReq.OrderNo))
                {
                    cmd.Parameters.AddWithValue("@OrderNo", objReq.OrderNo);
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsBundleSize();
                        obj.LayID = Convert.ToInt64(ds.Tables[0].Rows[i]["LayID"]);
                        obj.SizeSelectionID = Convert.ToInt64(ds.Tables[0].Rows[i]["SizeSelectionID"]);
                        obj.SizeID = Convert.ToInt32(ds.Tables[0].Rows[i]["SizeID"]);
                        obj.SizeName = Convert.ToString(ds.Tables[0].Rows[i]["SizeName"]);
                        obj.Freq = Convert.ToInt32(ds.Tables[0].Rows[i]["Freq"]);
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
                Logger.WriteLog("Function Name : Fn_Get_Bundle_Size", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Get_Bundle_Size");
            return objResp;
        }

        #endregion End Size- Bundle 7-Feb-2026

        #region Start Color- Bundle 7-Feb-2026

        public clsBundleColor Fn_Insert_Bundle_Color(clsBundleColor objReq)
        {
            var objResp = new clsBundleColor();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Insert_Bundle_Color");
            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                if (objReq.ColorSelectionID == 0 || objReq.ColorSelectionID == null)
                {
                    Fn_Get_MXID("BundleColorSelection", "ColorSelectionID", "");
                    objReq.ColorSelectionID = mxID;
                }

                SqlCommand cmd = new SqlCommand("USP_BUNDLE_LAYER", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ColorSelectionID", objReq.ColorSelectionID);
                cmd.Parameters.AddWithValue("@LayID", objReq.LayID);
                cmd.Parameters.AddWithValue("@ColorName", objReq.ColorName);
                cmd.Parameters.AddWithValue("@OrderNo", objReq.OrderNo);
                cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                cmd.Parameters.AddWithValue("@QueryType", "InsertBundleColor");
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
                    objResp.vErrorMsg = "Size inserting Failed";
                }
            }
            catch (Exception exp)
            {
                objResp.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Insert_Bundle_Color", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Insert_Bundle_Color");
            return objResp;
        }

        public clsBundleColor Fn_Delete_Bundle_Color(clsBundleColor objReq)
        {
            var objResp = new clsBundleColor();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Delete_Bundle_Color");
            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_BUNDLE_LAYER", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ColorSelectionID", objReq.ColorSelectionID);
                cmd.Parameters.AddWithValue("@OrderNo", objReq.OrderNo);
                cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                cmd.Parameters.AddWithValue("@QueryType", "DeleteBundleColor");
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
                Logger.WriteLog("Function Name : Fn_Delete_Bundle_Color", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Delete_Bundle_Color");
            return objResp;
        }

        public List<clsBundleColor> Fn_Get_Bundle_Color(clsBundleColor objReq)
        {
            var objResp = new List<clsBundleColor>();
            var obj = new clsBundleColor();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Get_Bundle_Color");
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT ColorSelectionID, LayID, ColorName,";
                strSql = strSql + " CreatedBy, FORMAT(CreatedOn, 'dd-MMM-yyy') AS CreatedOn FROM BundleColorSelection WHERE 1=1";
                if (!String.IsNullOrWhiteSpace(objReq.ColorName))
                {
                    strSql = strSql + " AND ColorName = @ColorName";
                }
                if (objReq.ColorSelectionID != 0 && objReq.ColorSelectionID != null)
                {
                    strSql = strSql + " AND ColorSelectionID = @ColorSelectionID ";
                }
                if (objReq.LayID != 0 && objReq.LayID != null)
                {
                    strSql = strSql + " AND LayID = @LayID ";
                }
                if (!String.IsNullOrWhiteSpace(objReq.OrderNo))
                {
                    strSql = strSql + " AND OrderNo = @OrderNo ";
                }
                strSql = strSql + " ORDER BY ColorSelectionID ASC ";

                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;
                if (!String.IsNullOrWhiteSpace(objReq.ColorName))
                {
                    cmd.Parameters.AddWithValue("@ColorName", objReq.ColorName);
                }
                if (objReq.ColorSelectionID != 0 && objReq.ColorSelectionID != null)
                {
                    cmd.Parameters.AddWithValue("@ColorSelectionID", objReq.ColorSelectionID);
                }
                if (objReq.LayID != 0 && objReq.LayID != null)
                {
                    cmd.Parameters.AddWithValue("@LayID", objReq.LayID);
                }
                if (!String.IsNullOrWhiteSpace(objReq.OrderNo))
                {
                    cmd.Parameters.AddWithValue("@OrderNo", objReq.OrderNo);
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsBundleColor();
                        obj.ColorSelectionID = Convert.ToInt64(ds.Tables[0].Rows[i]["ColorSelectionID"]);
                        obj.LayID = Convert.ToInt64(ds.Tables[0].Rows[i]["LayID"]);
                        obj.ColorName = Convert.ToString(ds.Tables[0].Rows[i]["ColorName"]);
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
                Logger.WriteLog("Function Name : Fn_Get_Bundle_Color", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Get_Bundle_Color");
            return objResp;
        }

        #endregion End Color- Bundle 7-Feb-2026

        #region Start Shade- Bundle 7-Feb-2026

        public clsBundleShade Fn_Insert_Bundle_Shade(clsBundleShade objReq)
        {
            var objResp = new clsBundleShade();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Insert_Bundle_Shade");
            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                if (objReq.ShadeSelectionID == 0 || objReq.ShadeSelectionID == null)
                {
                    Fn_Get_MXID("BundleShadeSelection", "ShadeSelectionID", "");
                    objReq.ShadeSelectionID = mxID;
                }

                SqlCommand cmd = new SqlCommand("USP_BUNDLE_LAYER", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ColorSelectionID", objReq.ColorSelectionID);
                cmd.Parameters.AddWithValue("@ShadeSelectionID", objReq.ShadeSelectionID);
                cmd.Parameters.AddWithValue("@ShadeName", objReq.ShadeName);
                cmd.Parameters.AddWithValue("@Plies", objReq.Plies);
                cmd.Parameters.AddWithValue("@OrderNo", objReq.OrderNo);
                cmd.Parameters.AddWithValue("@LayID", objReq.LayID);
                cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                cmd.Parameters.AddWithValue("@QueryType", "InsertBundleShade");
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
                    objResp.vErrorMsg = "Shade inserting Failed";
                }
            }
            catch (Exception exp)
            {
                objResp.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Insert_Bundle_Shade", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Insert_Bundle_Shade");
            return objResp;
        }

        public clsBundleShade Fn_Delete_Bundle_Shade(clsBundleShade objReq)
        {
            var objResp = new clsBundleShade();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Delete_Bundle_Shade");
            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_BUNDLE_LAYER", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ShadeSelectionID", objReq.ShadeSelectionID);
                cmd.Parameters.AddWithValue("@OrderNo", objReq.OrderNo);
                cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                cmd.Parameters.AddWithValue("@QueryType", "DeleteBundleShade");
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
                Logger.WriteLog("Function Name : Fn_Delete_Bundle_Shade", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Delete_Bundle_Shade");
            return objResp;
        }

        public List<clsBundleShade> Fn_Get_Bundle_Shade(clsBundleShade objReq)
        {
            var objResp = new List<clsBundleShade>();
            var obj = new clsBundleShade();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Get_Bundle_Shade");
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT ColorSelectionID, ShadeSelectionID, ShadeName, Plies, LayID, ";
                strSql = strSql + " CreatedBy, FORMAT(CreatedOn, 'dd-MMM-yyy') AS CreatedOn FROM BundleShadeSelection WHERE 1=1";
                if (!String.IsNullOrWhiteSpace(objReq.ShadeName))
                {
                    strSql = strSql + " AND ShadeName = @ShadeName";
                }
                if (objReq.ColorSelectionID != 0 && objReq.ColorSelectionID != null)
                {
                    strSql = strSql + " AND ColorSelectionID = @ColorSelectionID ";
                }
                if (objReq.ShadeSelectionID != 0 && objReq.ShadeSelectionID != null)
                {
                    strSql = strSql + " AND ShadeSelectionID = @ShadeSelectionID ";
                }
                if (objReq.LayID != 0 && objReq.LayID != null)
                {
                    strSql = strSql + " AND LayID = @LayID ";
                }
                if (!String.IsNullOrWhiteSpace(objReq.OrderNo))
                {
                    strSql = strSql + " AND OrderNo = @OrderNo ";
                }
                strSql = strSql + " ORDER BY ShadeSelectionID ASC ";

                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;
                if (!String.IsNullOrWhiteSpace(objReq.ShadeName))
                {
                    cmd.Parameters.AddWithValue("@ShadeName", objReq.ShadeName);
                }
                if (objReq.ColorSelectionID != 0 && objReq.ColorSelectionID != null)
                {
                    cmd.Parameters.AddWithValue("@ColorSelectionID", objReq.ColorSelectionID);
                }
                if (objReq.ShadeSelectionID != 0 && objReq.ShadeSelectionID != null)
                {
                    cmd.Parameters.AddWithValue("@ShadeSelectionID", objReq.ShadeSelectionID);
                }
                if (objReq.LayID != 0 && objReq.LayID != null)
                {
                    cmd.Parameters.AddWithValue("@LayID", objReq.LayID);
                }
                if (!String.IsNullOrWhiteSpace(objReq.OrderNo))
                {
                    cmd.Parameters.AddWithValue("@OrderNo", objReq.OrderNo);
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsBundleShade();
                        obj.ColorSelectionID = Convert.ToInt64(ds.Tables[0].Rows[i]["ColorSelectionID"]);
                        obj.ShadeSelectionID = Convert.ToInt64(ds.Tables[0].Rows[i]["ShadeSelectionID"]);
                        obj.LayID = Convert.ToInt64(ds.Tables[0].Rows[i]["LayID"]);
                        obj.ShadeName = Convert.ToString(ds.Tables[0].Rows[i]["ShadeName"]);
                        obj.CreatedBy = Convert.ToInt32(ds.Tables[0].Rows[i]["CreatedBy"]);
                        obj.Plies = Convert.ToInt32(ds.Tables[0].Rows[i]["Plies"]);
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
                Logger.WriteLog("Function Name : Fn_Get_Bundle_Shade", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Get_Bundle_Shade");
            return objResp;
        }

        #endregion End Shade- Bundle 7-Feb-2026

        #region Start Compile- Bundle 7-Feb-2026

        public clsBundleCompile Fn_Insert_Bundle_Compile(clsBundleCompile objReq)
        {
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Insert_Bundle_Compile");
            var objResp = new clsBundleCompile();
            var subSectionList = Fn_Get_Subsection_List(objReq);
            string strCriteria = "";

            var colorshade = new clsColorShade();
            colorshade.OrderNo = objReq.OrderNo;
            colorshade.LayID = objReq.LayID;
            var objColorShadeList = new List<clsColorShade>();
            objColorShadeList = Fn_Get_Color_With_Shade(colorshade);

            var checkOPBreakDown = new clsOPBreackDownMaster();
            checkOPBreakDown.StyleCode = objReq.StyleCode;
            checkOPBreakDown.CreatedBy = objReq.CreatedBy;

            checkOPBreakDown = _DALOrder.Fn_Check_Exist_style_In_Master(checkOPBreakDown);
            if (checkOPBreakDown.vErrorMsg != "Success" && checkOPBreakDown.vErrorCode != 200)
            {
                objResp.vErrorCode = 404;
                objResp.vErrorMsg = "Please upload subsection, then compile.";
                return objResp;
            }
            if (subSectionList[0].vErrorMsg != "Success" && subSectionList[0].vErrorCode != 200)
            {
                objResp.vErrorCode = 404;
                objResp.vErrorMsg = "Please upload subsection, then compile.";
                return objResp;
            }
            if (objColorShadeList[0].vErrorMsg != "Success" && objColorShadeList[0].vErrorCode != 200)
            {
                objResp.vErrorCode = 404;
                objResp.vErrorMsg = "Color and shade not found";
                return objResp;
            }

            try
            {
                if (Con.State == ConnectionState.Broken)
                {
                    Con.Close();
                }
                if (Con.State == ConnectionState.Closed)
                {
                    Con.Open();
                }

                // Split Size once

                var sizeArray = objReq.SizeName?
                    .TrimEnd(',')
                    .Split(',')
                    .OrderBy(x => x)
                    .ToArray();

                // Split Color once
                var colorArray = objReq.ColorName?.TrimEnd(',').Split(',');

                // objReq.ShadeName = objReq.ShadeName?.TrimEnd(',');

                string prevSize = "";
                string prevColor = "";
                long plyFrom = 0;
                long plyTo = 0;
                int prevBunleQty = objReq.BunleQty;

                foreach (var size in sizeArray)
                {
                    foreach (var oColorShadeList in objColorShadeList)
                    {
                        bool checkPostAssembly = false;
                        strCriteria = "";
                        strCriteria = " AND OrderNo = '" + objReq.OrderNo + "'";

                        if (size != prevSize)
                        {
                            long mxLotNo = Fn_Get_MXID("BundleCompile", "LotNo", strCriteria);
                            objReq.LotNo = Convert.ToInt32(mxLotNo);
                            prevSize = size;
                            objReq.BunleQty = prevBunleQty;
                        }
                        if (oColorShadeList.ColorName != prevColor)
                        {
                            prevColor = oColorShadeList.ColorName;
                            objReq.BunleQty = prevBunleQty;
                        }

                        long mxBundleNo = Fn_Get_MXID("BundleCompile", "BundleNo", strCriteria);

                        int NumOfSize = sizeArray.Length;
                        int Plies = oColorShadeList.Plies;
                        int lastQty = Plies % objReq.BunleQty;
                        float TotalBundle = Plies / objReq.BunleQty;
                        int secondLastBundle = 0;
                        TotalBundle = TotalBundle + mxBundleNo;
                        int bundleStart = 0;
                        bundleStart = bundleStart + Convert.ToInt32(mxBundleNo);

                        while (bundleStart <= TotalBundle)
                        {
                            secondLastBundle = Convert.ToInt32(TotalBundle) - 1;
                            if (bundleStart == TotalBundle)
                            {
                                objReq.Qty = lastQty;
                                objReq.BunleQty = lastQty;
                            }
                            if (bundleStart == secondLastBundle && lastQty <= 5)
                            {
                                float remainingQty = objReq.BunleQty + lastQty;

                                int secondlastQty = (int)(remainingQty / 2);
                                int lastQtyNew = (int)remainingQty - secondlastQty;
                                objReq.Qty = secondlastQty;
                                lastQty = lastQtyNew;
                                objReq.BunleQty = secondlastQty;
                            }
                            else
                            {
                                objReq.Qty = objReq.BunleQty;
                            }

                            foreach (var subSection in subSectionList)
                            {
                                strCriteria = "";
                                strCriteria = " AND OrderNo = '" + objReq.OrderNo + "'";
                                strCriteria = strCriteria + " AND SubSection ='POST ASSEMBLY'";
                                plyFrom = Fn_Get_MXID("BundleCompile", "PlyFrom", strCriteria);
                                plyTo = Fn_Get_MXID("BundleCompile", "PlyTo", strCriteria);

                                objReq.BundleNo = Convert.ToInt32(bundleStart);

                                if (subSection.SubSection != "POST ASSEMBLY")
                                {

                                    if (checkPostAssembly == true)
                                    {
                                        objReq.PlyFrom = Convert.ToInt32(plyFrom);
                                        objReq.PlyTo = Convert.ToInt32(objReq.Qty) + Convert.ToInt32(plyTo) - 1;
                                        objReq.PlyFrom = Convert.ToInt32(plyTo);
                                        //checkPostAssembly = false;
                                    }
                                    else
                                    {
                                        objReq.PlyFrom = Convert.ToInt32(plyFrom);
                                        objReq.PlyTo = Convert.ToInt32(objReq.Qty) + Convert.ToInt32(plyTo) - 1;
                                        //checkPostAssembly = false;
                                    }

                                    var objMxId = Fn_Get_Max_ID_With_DATE(new clsMAXID
                                    {
                                        TBLName = "BundleCompile",
                                        IDField = "BundleID",
                                        DateField = "CreatedOn",
                                        strPreFix = ""
                                    });

                                    objReq.BundleID = objMxId.nMAXID;
                                    using (SqlCommand cmd = new SqlCommand("USP_BUNDLE_LAYER", Con))
                                    {

                                        if (Con.State == ConnectionState.Broken)
                                        { Con.Close(); }
                                        if (Con.State == ConnectionState.Closed)
                                        { Con.Open(); }
                                        cmd.CommandType = CommandType.StoredProcedure;

                                        cmd.Parameters.AddWithValue("@BundleID", objReq.BundleID);
                                        cmd.Parameters.AddWithValue("@LayID", objReq.LayID);
                                        cmd.Parameters.AddWithValue("@BundleNo", objReq.BundleNo);
                                        cmd.Parameters.AddWithValue("@SizeName", size);
                                        cmd.Parameters.AddWithValue("@ColorName", oColorShadeList.ColorName);
                                        cmd.Parameters.AddWithValue("@ShadeName", oColorShadeList.ShadeName);
                                        cmd.Parameters.AddWithValue("@Qty", objReq.Qty);
                                        cmd.Parameters.AddWithValue("@PlyFrom", objReq.PlyFrom);
                                        cmd.Parameters.AddWithValue("@PlyTo", objReq.PlyTo);
                                        cmd.Parameters.AddWithValue("@LotNo", objReq.LotNo);
                                        cmd.Parameters.AddWithValue("@SubSection", subSection.SubSection);
                                        cmd.Parameters.AddWithValue("@IsDispatch", objReq.IsDispatch);
                                        cmd.Parameters.AddWithValue("@StyleCode", objReq.StyleCode);
                                        cmd.Parameters.AddWithValue("@OrderNo", objReq.OrderNo);
                                        cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                                        cmd.Parameters.AddWithValue("@BundleQty", objReq.BunleQty);
                                        cmd.Parameters.AddWithValue("@QueryType", "InsertBundleCompile");

                                        int result = cmd.ExecuteNonQuery();

                                        if (result > 0)
                                        {
                                            objResp.vErrorCode = 200;
                                            objResp.vErrorMsg = "Success";
                                        }
                                        else
                                        {
                                            objResp.vErrorCode = 400;
                                            objResp.vErrorMsg = "Insert Failed";
                                        }

                                    }
                                }
                                else
                                {
                                    checkPostAssembly = true;
                                    int pCount = 1;
                                    strCriteria = "";
                                    strCriteria = " AND OrderNo = '" + objReq.OrderNo + "'";
                                    strCriteria = strCriteria + " AND SubSection ='POST ASSEMBLY'";
                                    plyTo = Fn_Get_MXID("BundleCompile", "PlyTo", strCriteria);
                                    while (pCount <= objReq.BunleQty)
                                    {

                                        plyFrom = plyTo;
                                        objReq.Qty = 1;
                                        objReq.PlyFrom = Convert.ToInt32(plyFrom);
                                        objReq.PlyTo = Convert.ToInt32(plyTo);

                                        var objMxId = Fn_Get_Max_ID_With_DATE(new clsMAXID
                                        {
                                            TBLName = "BundleCompile",
                                            IDField = "BundleID",
                                            DateField = "CreatedOn",
                                            strPreFix = ""
                                        });

                                        objReq.BundleID = objMxId.nMAXID;

                                        using (SqlCommand cmd = new SqlCommand("USP_BUNDLE_LAYER", Con))
                                        {

                                            if (Con.State == ConnectionState.Broken)
                                            { Con.Close(); }
                                            if (Con.State == ConnectionState.Closed)
                                            { Con.Open(); }
                                            cmd.CommandType = CommandType.StoredProcedure;

                                            cmd.Parameters.AddWithValue("@BundleID", objReq.BundleID);
                                            cmd.Parameters.AddWithValue("@LayID", objReq.LayID);
                                            cmd.Parameters.AddWithValue("@BundleNo", objReq.BundleNo);
                                            cmd.Parameters.AddWithValue("@SizeName", size);
                                            cmd.Parameters.AddWithValue("@ColorName", oColorShadeList.ColorName);
                                            cmd.Parameters.AddWithValue("@ShadeName", oColorShadeList.ShadeName);
                                            cmd.Parameters.AddWithValue("@Qty", objReq.Qty);
                                            cmd.Parameters.AddWithValue("@PlyFrom", objReq.PlyFrom);
                                            cmd.Parameters.AddWithValue("@PlyTo", objReq.PlyTo);
                                            cmd.Parameters.AddWithValue("@LotNo", objReq.LotNo);
                                            cmd.Parameters.AddWithValue("@SubSection", subSection.SubSection);
                                            cmd.Parameters.AddWithValue("@IsDispatch", objReq.IsDispatch);
                                            cmd.Parameters.AddWithValue("@StyleCode", objReq.StyleCode);
                                            cmd.Parameters.AddWithValue("@OrderNo", objReq.OrderNo);
                                            cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                                            cmd.Parameters.AddWithValue("@BundleQty", objReq.BunleQty);
                                            cmd.Parameters.AddWithValue("@QueryType", "InsertBundleCompile");

                                            int result = cmd.ExecuteNonQuery();

                                            if (result > 0)
                                            {
                                                objResp.vErrorCode = 200;
                                                objResp.vErrorMsg = "Success";
                                            }
                                            else
                                            {
                                                objResp.vErrorCode = 400;
                                                objResp.vErrorMsg = "Insert Failed";
                                            }

                                        }
                                        pCount++;
                                        plyTo = plyTo + 1;
                                    }
                                }
                            }

                            bundleStart++;
                        }

                    }
                }
                if (objResp.vErrorMsg == "Success")
                {
                    var obj = new clsBundleLayerMaster();
                    obj.LayID = objReq.LayID;
                    obj.Qty = objReq.CompileQty;
                    obj.OrderNo = objReq.OrderNo;
                    obj.CreatedBy = objReq.CreatedBy;
                    Fn_Update_Layer_Qty(obj);
                }
            }
            catch (Exception exp)
            {
                objResp.vErrorCode = 500;
                objResp.vErrorMsg = exp.Message;
                Logger.WriteLog("Fn_Insert_Bundle_Compile", "Error Msg: " + exp.Message, new StackTrace(exp, true));
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Insert_Bundle_Compile");
            return objResp;
        }


        public clsBundleCompile Fn_Delete_Bundle_Compile(clsBundleCompile objReq)
        {
            var objResp = new clsBundleCompile();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Delete_Bundle_Compile");
            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_BUNDLE_LAYER", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BundleID", objReq.BundleID);
                cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                cmd.Parameters.AddWithValue("@QueryType", "DeleteBundleCompile");
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
                Logger.WriteLog("Function Name : Fn_Delete_Bundle_Compile", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Delete_Bundle_Compile");
            return objResp;
        }

        public List<clsBundleCompile> Fn_Get_Bundle_Compile(clsBundleCompile objReq)
        {
            var objResp = new List<clsBundleCompile>();
            var obj = new clsBundleCompile();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Get_Bundle_Compile");
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT BundleID, LayID, BundleNo, SizeName, ColorName, ShadeName,";
                strSql = strSql + " Qty, PlyFrom, PlyTo, LotNo, SubSection, Dispatch, StyleCode, OrderNo, BundleQty,";
                strSql = strSql + " SupervisorID, SupervisorAssignedDate, UpdateType, ";
                strSql = strSql + " CreatedBy, FORMAT(CreatedOn, 'dd-MMM-yyy') AS CreatedOn FROM BundleCompile WHERE 1=1";
                if (!String.IsNullOrWhiteSpace(objReq.StyleCode))
                {
                    strSql = strSql + " AND StyleCode = @StyleCode";
                }
                if (!String.IsNullOrWhiteSpace(objReq.OrderNo))
                {
                    strSql = strSql + " AND OrderNo = @OrderNo";
                }
                if (objReq.BundleID != 0 && objReq.BundleID != null)
                {
                    strSql = strSql + " AND BundleID = @BundleID ";
                }
                if (objReq.LayID != 0 && objReq.LayID != null && objReq.PlyFrom == 0 && objReq.PlyTo == 0)
                {
                    strSql = strSql + " AND LayID = @LayID ";
                }
                if (objReq.PlyFrom != 0 && objReq.PlyTo != 0)
                {
                    strSql = strSql + " AND LayID BETWEEN @PlyFrom AND @PlyTo ";
                }
                if (!String.IsNullOrWhiteSpace(objReq.SubSection))
                {
                    strSql = strSql + " AND SubSection IN (" + objReq.SubSection + ") ";
                }

                strSql = strSql + " ORDER BY BundleID ASC ";

                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;
                if (!String.IsNullOrWhiteSpace(objReq.StyleCode))
                {
                    cmd.Parameters.AddWithValue("@StyleCode", objReq.StyleCode);
                }
                if (!String.IsNullOrWhiteSpace(objReq.OrderNo))
                {
                    cmd.Parameters.AddWithValue("@OrderNo", objReq.OrderNo);
                }
                if (objReq.BundleID != 0 && objReq.BundleID != null)
                {
                    cmd.Parameters.AddWithValue("@BundleID", objReq.BundleID);
                }
                if (objReq.LayID != 0 && objReq.LayID != null && objReq.PlyFrom == 0 && objReq.PlyTo == 0)
                {
                    cmd.Parameters.AddWithValue("@LayID", objReq.LayID);
                }
                if (objReq.PlyFrom != 0 && objReq.PlyTo != 0)
                {
                    cmd.Parameters.AddWithValue("@PlyFrom", objReq.PlyFrom);
                    cmd.Parameters.AddWithValue("@PlyTo", objReq.PlyTo);
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsBundleCompile();
                        obj.BundleID = Convert.ToInt64(ds.Tables[0].Rows[i]["BundleID"]);
                        obj.LayID = Convert.ToInt64(ds.Tables[0].Rows[i]["LayID"]);
                        obj.BundleNo = Convert.ToInt32(ds.Tables[0].Rows[i]["BundleNo"]);
                        obj.SizeName = Convert.ToString(ds.Tables[0].Rows[i]["SizeName"]);
                        obj.ColorName = Convert.ToString(ds.Tables[0].Rows[i]["ColorName"]);
                        obj.ShadeName = Convert.ToString(ds.Tables[0].Rows[i]["ShadeName"]);
                        obj.Qty = Convert.ToInt32(ds.Tables[0].Rows[i]["Qty"]);
                        obj.PlyFrom = Convert.ToInt32(ds.Tables[0].Rows[i]["PlyFrom"]);
                        obj.PlyTo = Convert.ToInt32(ds.Tables[0].Rows[i]["PlyTo"]);
                        obj.LotNo = Convert.ToInt32(ds.Tables[0].Rows[i]["LotNo"]);
                        obj.SubSection = Convert.ToString(ds.Tables[0].Rows[i]["SubSection"]);
                        obj.IsDispatch = Convert.ToBoolean(ds.Tables[0].Rows[i]["Dispatch"]);
                        obj.StyleCode = Convert.ToString(ds.Tables[0].Rows[i]["StyleCode"]);
                        obj.OrderNo = Convert.ToString(ds.Tables[0].Rows[i]["OrderNo"]);
                        obj.CreatedBy = Convert.ToInt32(ds.Tables[0].Rows[i]["CreatedBy"]);
                        obj.CreatedOn = Convert.ToString(ds.Tables[0].Rows[i]["CreatedOn"]);
                        string strBundleQty = Convert.ToString(ds.Tables[0].Rows[i]["BundleQty"]);
                        if (strBundleQty != "")
                        {
                            obj.BunleQty = Convert.ToInt32(ds.Tables[0].Rows[i]["BundleQty"]);
                        }
                        obj.UpdateType = Convert.ToString(ds.Tables[0].Rows[i]["UpdateType"]);
                        obj.SupervisorAssignedDate = Convert.ToString(ds.Tables[0].Rows[i]["SupervisorAssignedDate"]);
                        string SupervisorID = Convert.ToString(ds.Tables[0].Rows[i]["SupervisorID"]);
                        if (SupervisorID != "")
                        {
                            obj.SupervisorID = Convert.ToInt32(ds.Tables[0].Rows[i]["SupervisorID"]);
                        }
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
                Logger.WriteLog("Function Name : Fn_Get_Bundle_Compile", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Get_Bundle_Compile");
            return objResp;
        }

        #endregion End Compile- Bundle 7-Feb-2026

        public List<clsSizeMaster> Fn_Get_Order_SizeName(clsSizeMaster objReq)
        {
            var objResp = new List<clsSizeMaster>();
            var obj = new clsSizeMaster();
            string strSql = "";
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Get_Order_SizeName");
            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                strSql = "SELECT Distinct SizeID, Size, CreatedBy, FORMAT(CreatedOn, 'dd-MMM-yyyy') AS CreatedOn FROM OrderDetail WHERE 1=1";
                if (objReq.ID != 0)
                {
                    strSql = strSql + " AND OrderNo = @OrderNo";
                }
                strSql = strSql + " ORDER BY Size ASC ";

                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;
                if (objReq.ID != 0)
                {
                    cmd.Parameters.AddWithValue("@OrderNo", objReq.ID);
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsSizeMaster();

                        //obj.ID = Convert.ToInt16(ds.Tables[0].Rows[i]["DetailID"]);
                        obj.SizeName = Convert.ToString(ds.Tables[0].Rows[i]["Size"]);
                        obj.ID = Convert.ToInt32(ds.Tables[0].Rows[i]["SizeID"]);
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
                Logger.WriteLog("Function Name : Fn_Get_Order_SizeName", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Get_Order_SizeName");
            return objResp;
        }

        public List<clsBundleColor> Fn_Get_Order_Color(clsBundleColor objReq)
        {
            var objResp = new List<clsBundleColor>();
            var obj = new clsBundleColor();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Get_Order_Color");
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT Distinct Color FROM OrderDetail WHere 1=1";

                if (objReq.ColorSelectionID != 0 && objReq.ColorSelectionID != null)
                {
                    strSql = strSql + " AND OrderNo = @OrderNo ";
                }
                strSql = strSql + " ORDER BY Color DESC ";

                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;

                if (objReq.ColorSelectionID != 0 && objReq.ColorSelectionID != null)
                {
                    cmd.Parameters.AddWithValue("@OrderNo", objReq.ColorSelectionID);
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsBundleColor();
                        //obj.ColorSelectionID = Convert.ToInt64(ds.Tables[0].Rows[i]["DetailID"]);
                        obj.ColorName = Convert.ToString(ds.Tables[0].Rows[i]["Color"]);
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
                Logger.WriteLog("Function Name : Fn_Get_Order_Color", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Get_Order_Color");
            return objResp;
        }

        public List<clsBundleCompile> Fn_Get_Subsection_List(clsBundleCompile objReq)
        {
            var objResp = new List<clsBundleCompile>();
            var obj = new clsBundleCompile();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Get_Subsection_List");
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT D.SubSection From OperationBreackDownDetail D INNER JOIN OperationBreackDownMaster M ";
                strSql = strSql + " ON D.MID = M.ID WHERE 1=1 ";
                if (!String.IsNullOrWhiteSpace(objReq.StyleCode))
                {
                    strSql = strSql + " AND M.StyleCode = @StyleCode";
                }

                // strSql = strSql + " ORDER BY D.CreatedOn ASC ";
                strSql = strSql + " ORDER BY ";
                strSql = strSql + "CASE";
                strSql = strSql + "    WHEN D.SubSection IN('Post Assembly', 'POST ASSEMBLY') THEN 1";
                strSql = strSql + "    ELSE 0";
                strSql = strSql + " END,";
                strSql = strSql + " D.CreatedOn ASC; ";

                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;
                if (!String.IsNullOrWhiteSpace(objReq.StyleCode))
                {
                    cmd.Parameters.AddWithValue("@StyleCode", objReq.StyleCode);
                }


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string strSubSection = "";
                    HashSet<string> addedSubSections = new HashSet<string>();

                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsBundleCompile();
                        strSubSection = Convert.ToString(ds.Tables[0].Rows[i]["SubSection"]);
                        if (!addedSubSections.Contains(strSubSection))
                        {
                            obj.SubSection = strSubSection;
                            obj.vErrorCode = 200;
                            obj.vErrorMsg = "Success";
                            objResp.Add(obj);
                            addedSubSections.Add(strSubSection);
                        }
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
                Logger.WriteLog("Function Name : Fn_Get_Subsection_List", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Get_Subsection_List");
            return objResp;
        }

        public clsMAXID Fn_Get_Max_ID_With_DATE(clsMAXID objReq)
        {
            var objResp = new clsMAXID();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Get_Max_ID_With_DATE");
            try
            {
                //if (Con.State == ConnectionState.Broken)
                //{ Con.Close(); }
                //if (Con.State == ConnectionState.Closed)
                //{ Con.Open(); }
                string strSql = "";

                if (String.IsNullOrWhiteSpace(objReq.strPreFix))
                {
                    strSql = "SELECT CONCAT(FORMAT(GETDATE(),'ddMMyyyy'),SUBSTRING(FORMAT(ISNULL(MAX(" + objReq.IDField + ") + 1, 1),'00000000000000'),9,6)) FROM " + objReq.TBLName + " WHERE CONVERT(DATE," + objReq.DateField + ")= CONVERT(DATE, GETDATE())";
                }
                else
                {
                    strSql = "Select Concat(format(getdate(),'ddMMyyyy'), FORMAT(ISNULL(max(cast(substring(" + objReq.IDField + ",13,6) as int))+1,1),'000000')) from " + objReq.TBLName + " where Convert(date," + objReq.DateField + ")=Convert(date,getdate())";
                }

                SqlCommand cmd = new SqlCommand(strSql, Con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    objResp.vMAXID = objReq.strPreFix + dr[0].ToString();
                    objResp.nMAXID = Convert.ToInt64(dr[0].ToString());
                    objResp.vErrorMsg = "Success";
                }
                else
                {
                    string dt = DateTime.Now.ToString("ddMMyyyy");
                    objResp.vMAXID = objReq.strPreFix + dt + "000001";
                    objResp.nMAXID = Convert.ToInt64(dt + "000001");
                    objResp.vErrorMsg = "Success";
                }
                dr.Close();
                cmd.Dispose();
                Con.Close();
            }
            catch (Exception exp)
            {
                Logger.WriteLog("Function Name : Fn_Get_Max_ID", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                //Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Get_Max_ID_With_DATE");
            return objResp;
        }

        public clsBundleLayerMaster Fn_Update_Layer_Qty(clsBundleLayerMaster objReq)
        {
            var objResp = new clsBundleLayerMaster();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Update_Layer_Qty");
            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_BUNDLE_LAYER", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LayID", objReq.LayID);
                cmd.Parameters.AddWithValue("@Qty", objReq.Qty);
                cmd.Parameters.AddWithValue("@OrderNo", objReq.OrderNo);
                cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                cmd.Parameters.AddWithValue("@QueryType", "UpdateLayerQty");
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
                    objResp.vErrorMsg = "Layer qty updating failed";
                }
            }
            catch (Exception exp)
            {
                objResp.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Update_Layer_Qty", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Update_Layer_Qty");
            return objResp;
        }

        #region Start Sectionwise compile data for QR 23-FEB-2026

        public List<clsBundleCompile> Fn_Get_SectionWis_Compile_QR_Data(clsBundleCompile objReq)
        {
            var objResp = new List<clsBundleCompile>();
            var obj = new clsBundleCompile();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Get_SectionWis_Compile_QR_Data");
            try
            {
                string[] arrSubSection = objReq.SubSection.Split(',');

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT Distinct BundleID, ColorName, Qty, BundleNo, SubSection, StyleCode, OrderNo, PlyFrom, PlyTo, SizeName, LayID,";
                strSql = strSql + " CreatedBy, FORMAT(CreatedOn, 'dd-MMM-yyy') AS CreatedOn FROM BundleCompile WHERE 1=1";
                if (!String.IsNullOrWhiteSpace(objReq.StyleCode))
                {
                    strSql = strSql + " AND StyleCode = @StyleCode";
                }
                if (!String.IsNullOrWhiteSpace(objReq.OrderNo))
                {
                    strSql = strSql + " AND OrderNo = @OrderNo";
                }
                if (objReq.LayID != 0 && objReq.LayID != null && objReq.PlyFrom == 0 && objReq.PlyTo == 0)
                {
                    strSql = strSql + " AND LayID = @LayID ";
                }
                if (objReq.PlyFrom != 0 && objReq.PlyTo != 0)
                {
                    strSql = strSql + " AND LayID BETWEEN @PlyFrom AND @PlyTo ";
                }
                if (!String.IsNullOrWhiteSpace(objReq.SubSection))
                {
                    // strSql = strSql + " AND SubSection IN (@SubSection) ";
                    strSql = strSql + " AND SubSection IN (" + objReq.SubSection + ") ";
                }

                strSql = strSql + " ORDER BY LayID, BundleNo, SubSection ASC ";

                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;
                if (!String.IsNullOrWhiteSpace(objReq.StyleCode))
                {
                    cmd.Parameters.AddWithValue("@StyleCode", objReq.StyleCode);
                }
                if (!String.IsNullOrWhiteSpace(objReq.OrderNo))
                {
                    cmd.Parameters.AddWithValue("@OrderNo", objReq.OrderNo);
                }
                if (objReq.LayID != 0 && objReq.LayID != null && objReq.PlyFrom == 0 && objReq.PlyTo == 0)
                {
                    cmd.Parameters.AddWithValue("@LayID", objReq.LayID);
                }
                if (objReq.PlyFrom != 0 && objReq.PlyTo != 0)
                {
                    cmd.Parameters.AddWithValue("@PlyFrom", objReq.PlyFrom);
                    cmd.Parameters.AddWithValue("@PlyTo", objReq.PlyTo);
                }
                //if (!String.IsNullOrWhiteSpace(objReq.SubSection))
                //{
                //    cmd.Parameters.AddWithValue("@SubSection",  objReq.SubSection);
                //}

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsBundleCompile();
                        obj.BundleID = Convert.ToInt64(ds.Tables[0].Rows[i]["BundleID"]);
                        obj.BundleNo = Convert.ToInt32(ds.Tables[0].Rows[i]["BundleNo"]);
                        obj.SubSection = Convert.ToString(ds.Tables[0].Rows[i]["SubSection"]);
                        obj.StyleCode = Convert.ToString(ds.Tables[0].Rows[i]["StyleCode"]);
                        obj.OrderNo = Convert.ToString(ds.Tables[0].Rows[i]["OrderNo"]);
                        //obj.LotNo = Convert.ToInt32(ds.Tables[0].Rows[i]["LotNo"]);
                        obj.PlyFrom = Convert.ToInt32(ds.Tables[0].Rows[i]["PlyFrom"]);
                        obj.PlyTo = Convert.ToInt32(ds.Tables[0].Rows[i]["PlyTo"]);
                        obj.SizeName = Convert.ToString(ds.Tables[0].Rows[i]["SizeName"]);
                        obj.LayID = Convert.ToInt32(ds.Tables[0].Rows[i]["LayID"]);
                        obj.ColorName = Convert.ToString(ds.Tables[0].Rows[i]["ColorName"]);
                        obj.Qty = Convert.ToInt32(ds.Tables[0].Rows[i]["Qty"]);
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
                Logger.WriteLog("Function Name : Fn_Get_SectionWis_Compile_QR_Data", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Get_SectionWis_Compile_QR_Data");
            return objResp;
        }

        #endregion End Sectionwise compile data for QR 23-FEB-2026

        #region Start Update Fn_Update_AppEmpId 26-FEB-2026

        public clsBundleCompile Fn_Update_AppEmpId(clsBundleCompile objReq)
        {
            var objResp = new clsBundleCompile();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Update_AppEmpId");
            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_BUNDLE_LAYER", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BundleID", objReq.BundleID);
                cmd.Parameters.AddWithValue("@AppEmpID", objReq.AppEmpID);
                cmd.Parameters.AddWithValue("@OpNo", objReq.OperationNo);
                cmd.Parameters.AddWithValue("@AppStartTime", objReq.AppStartTime);
                cmd.Parameters.AddWithValue("@AppEndTime", objReq.AppEndTime);
                cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                cmd.Parameters.AddWithValue("@QueryType", "Update_AppEmpId");
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
                    objResp.vErrorMsg = "AppEmpId updating Failed";
                }
            }
            catch (Exception exp)
            {
                objResp.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Update_AppEmpId", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Update_AppEmpId");
            return objResp;
        }

        #endregion End Update BundleID by AppEmpId 26-FEB-2026

        #region Start Fn_Get_Sum_Laywise_Plies 23-APR-2026

        public clsBundleShade Fn_Get_Sum_Laywise_Plies(clsBundleShade objReq)
        {
            var objResp = new clsBundleShade();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Get_Sum_Laywise_Plies");
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT SUM(Plies) AS Plies FROM BundleShadeSelection WHERE 1=1";
                if (objReq.LayID != 0 && objReq.LayID != null)
                {
                    strSql = strSql + " AND LayID = @LayID ";
                }
                if (!String.IsNullOrWhiteSpace(objReq.OrderNo))
                {
                    strSql = strSql + " AND OrderNo = @OrderNo ";
                }
                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;
                if (objReq.LayID != 0 && objReq.LayID != null)
                {
                    cmd.Parameters.AddWithValue("@LayID", objReq.LayID);
                }
                if (!String.IsNullOrWhiteSpace(objReq.OrderNo))
                {
                    cmd.Parameters.AddWithValue("@OrderNo", objReq.OrderNo);
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    objResp.Plies = Convert.ToInt32(ds.Tables[0].Rows[i]["Plies"]);
                    objResp.vErrorCode = 200;
                    objResp.vErrorMsg = "Success";
                }
                else
                {
                    objResp.vErrorCode = 404;
                    objResp.vErrorMsg = "No Record found";
                }

            }
            catch (Exception exp)
            {
                objResp.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Get_Sum_Laywise_Plies", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Get_Sum_Laywise_Plies");
            return objResp;
        }

        #endregion End Fn_Get_Sum_Laywise_Plies 23-APR-2026

        public List<clsColorShade> Fn_Get_Color_With_Shade(clsColorShade objReq)
        {
            var objResp = new List<clsColorShade>();
            var obj = new clsColorShade();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Get_Color_With_Shade");
            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "Select BCS.OrderNo, BCS.LayID, BCS.ColorSelectionID, BCS.ColorName, BSS.ShadeName, BSS.Plies from BundleColorSelection BCS ";
                strSql = strSql + " INNER JOIN BundleShadeSelection BSS ON BCS.ColorSelectionID = BSS.ColorSelectionID AND BCS.LayID = BSS.LayID WHERE 1=1 ";

                if (!String.IsNullOrWhiteSpace(objReq.OrderNo))
                {
                    strSql = strSql + " AND BCS.OrderNo = @OrderNo";
                }
                if (objReq.LayID != 0 && objReq.LayID != null)
                {
                    strSql = strSql + " AND BCS.LayID = @LayID ";
                }

                strSql = strSql + " ORDER BY BCS.ColorSelectionID ASC ";

                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;

                if (!String.IsNullOrWhiteSpace(objReq.OrderNo))
                {
                    cmd.Parameters.AddWithValue("@OrderNo", objReq.OrderNo);
                }
                if (objReq.LayID != 0 && objReq.LayID != null)
                {
                    cmd.Parameters.AddWithValue("@LayID", objReq.LayID);
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsColorShade();
                        obj.OrderNo = Convert.ToString(ds.Tables[0].Rows[i]["OrderNo"]);
                        obj.LayID = Convert.ToInt64(ds.Tables[0].Rows[i]["LayID"]);
                        obj.ColorName = Convert.ToString(ds.Tables[0].Rows[i]["ColorName"]);
                        obj.ShadeName = Convert.ToString(ds.Tables[0].Rows[i]["ShadeName"]);
                        obj.Plies = Convert.ToInt32(ds.Tables[0].Rows[i]["Plies"]);

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
                Logger.WriteLog("Function Name : Fn_Get_Color_With_Shade", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Get_Color_With_Shade");
            return objResp;
        }       
    }
}