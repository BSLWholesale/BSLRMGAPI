using BSLDaman.Models;
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

        public clsProductionMaster Fn_Insert_Production_Order(clsProductionMaster objReq)
        {
            var objResp = new clsProductionMaster();
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
            return objResp;
        }

        public List<clsProductionMaster> Fn_Get_Production_Order(clsProductionMaster objReq)
        {
            var objResp = new List<clsProductionMaster>();
            var obj = new clsProductionMaster();
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
            return objResp;
        }

        public List<clsProductionDetail> Fn_Get_Production_Detail(clsProductionDetail objReq)
        {
            var objResp = new List<clsProductionDetail>();
            var obj = new clsProductionDetail();
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
            return objResp;
        }

        public clsProductionMaster Fn_Update_Production(clsProductionMaster objReq)
        {
            var objResp = new clsProductionMaster();
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
            return objResp;
        }

        #region Start style 19-Jan-2026

        public clsStyle Fn_Insert_Style(clsStyle objReq)
        {
            var objResp = new clsStyle();
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
            return objResp;
        }

        public List<clsStyle> Fn_Get_Style(clsStyle objReq)
        {
            var objResp = new List<clsStyle>();
            var obj = new clsStyle();
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
            return objResp;
        }

        #endregion End Style 19-Jan-2026

        public List<clsAutoCompliteResponse> Fn_AutoComplete_Textbox(clsAutoCompliteRequest obj)
        {
            var objResp = new List<clsAutoCompliteResponse>();
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
            return objResp;
        }

        #region Start Layer- Bundle 6-Feb-2026

        public clsBundleLayerMaster Fn_Insert_Bundle_Layer(clsBundleLayerMaster objReq)
        {
            var objResp = new clsBundleLayerMaster();

            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                if (objReq.LayID == 0 || objReq.LayID == null)
                {
                    Fn_Get_MXID("BundleLayerMaster", "LayID");
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
            return objResp;
        }

        public clsBundleLayerMaster Fn_Delete_Bundle_Layer(clsBundleLayerMaster objReq)
        {
            var objResp = new clsBundleLayerMaster();
            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_BUNDLE_LAYER", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LayID", objReq.LayID);
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
            return objResp;
        }

        public List<clsBundleLayerMaster> Fn_Get_Bundle_Layer(clsBundleLayerMaster objReq)
        {
            var objResp = new List<clsBundleLayerMaster>();
            var obj = new clsBundleLayerMaster();
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
                        obj.BundleLen = Convert.ToDouble(ds.Tables[0].Rows[i]["BundleLen"]);
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
            return objResp;
        }

        #endregion End Layer- Bundle 6-Feb-2026

        #region Start Size- Bundle 7-Feb-2026

        public clsBundleSize Fn_Insert_Bundle_Size(clsBundleSize objReq)
        {
            var objResp = new clsBundleSize();

            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                if (objReq.SizeSelectionID == 0 || objReq.SizeSelectionID == null)
                {
                    Fn_Get_MXID("BundleSizeSelection", "SizeSelectionID");
                    objReq.LayID = mxID;
                }

                SqlCommand cmd = new SqlCommand("USP_BUNDLE_LAYER", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LayID", objReq.LayID);
                cmd.Parameters.AddWithValue("@SizeSelectionID", objReq.SizeSelectionID);
                cmd.Parameters.AddWithValue("@SizeName", objReq.SizeName);
                cmd.Parameters.AddWithValue("@SizeID", objReq.SizeID);
                cmd.Parameters.AddWithValue("@Freq", objReq.Freq);
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
            return objResp;
        }

        public clsBundleSize Fn_Delete_Bundle_Size(clsBundleSize objReq)
        {
            var objResp = new clsBundleSize();
            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_BUNDLE_LAYER", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SizeSelectionID", objReq.SizeSelectionID);
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
            return objResp;
        }

        public List<clsBundleSize> Fn_Get_Bundle_Size(clsBundleSize objReq)
        {
            var objResp = new List<clsBundleSize>();
            var obj = new clsBundleSize();
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
                strSql = strSql + " ORDER BY SizeSelectionID DESC ";

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
            return objResp;
        }

        #endregion End Size- Bundle 7-Feb-2026

        #region Start Color- Bundle 7-Feb-2026

        public clsBundleColor Fn_Insert_Bundle_Color(clsBundleColor objReq)
        {
            var objResp = new clsBundleColor();

            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                if (objReq.ColorSelectionID == 0 || objReq.ColorSelectionID == null)
                {
                    Fn_Get_MXID("BundleLayerMaster", "ColorSelectionID");
                    objReq.ColorSelectionID = mxID;
                }

                SqlCommand cmd = new SqlCommand("USP_BUNDLE_LAYER", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ColorSelectionID", objReq.ColorSelectionID);
                cmd.Parameters.AddWithValue("@SizeSelectionID", objReq.SizeSelectionID);
                cmd.Parameters.AddWithValue("@ColorName", objReq.ColorName);
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
            return objResp;
        }

        public clsBundleColor Fn_Delete_Bundle_Color(clsBundleColor objReq)
        {
            var objResp = new clsBundleColor();
            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_BUNDLE_LAYER", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ColorSelectionID", objReq.ColorSelectionID);
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
            return objResp;
        }

        public List<clsBundleColor> Fn_Get_Bundle_Color(clsBundleColor objReq)
        {
            var objResp = new List<clsBundleColor>();
            var obj = new clsBundleColor();
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT ColorSelectionID, SizeSelectionID, ColorName,";
                strSql = strSql + " CreatedBy, FORMAT(CreatedOn, 'dd-MMM-yyy') AS CreatedOn FROM BundleSizeSelection WHERE 1=1";
                if (!String.IsNullOrWhiteSpace(objReq.ColorName))
                {
                    strSql = strSql + " AND ColorName = @ColorName";
                }
                if (objReq.ColorSelectionID != 0 && objReq.ColorSelectionID != null)
                {
                    strSql = strSql + " AND ColorSelectionID = @ColorSelectionID ";
                }
                if (objReq.SizeSelectionID != 0 && objReq.SizeSelectionID != null)
                {
                    strSql = strSql + " AND LayID = @LayID ";
                }
                strSql = strSql + " ORDER BY ColorSelectionID DESC ";

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
                if (objReq.SizeSelectionID != 0 && objReq.SizeSelectionID != null)
                {
                    cmd.Parameters.AddWithValue("@SizeSelectionID", objReq.SizeSelectionID);
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
                        obj.SizeSelectionID = Convert.ToInt64(ds.Tables[0].Rows[i]["SizeSelectionID"]);
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
            return objResp;
        }

        #endregion End Color- Bundle 7-Feb-2026

        #region Start Shade- Bundle 7-Feb-2026

        public clsBundleShade Fn_Insert_Bundle_Shade(clsBundleShade objReq)
        {
            var objResp = new clsBundleShade();

            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                if (objReq.ColorSelectionID == 0 || objReq.ColorSelectionID == null)
                {
                    Fn_Get_MXID("BundleShadeSelection", "ShadeSelectionID");
                    objReq.ColorSelectionID = mxID;
                }

                SqlCommand cmd = new SqlCommand("USP_BUNDLE_LAYER", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ColorSelectionID", objReq.ColorSelectionID);
                cmd.Parameters.AddWithValue("@ShadeSelectionID", objReq.ShadeSelectionID);
                cmd.Parameters.AddWithValue("@ShadeName", objReq.ShadeName);
                cmd.Parameters.AddWithValue("@Piles", objReq.Piles);
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
            return objResp;
        }

        public clsBundleShade Fn_Delete_Bundle_Shade(clsBundleShade objReq)
        {
            var objResp = new clsBundleShade();
            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_BUNDLE_LAYER", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ShadeSelectionID", objReq.ShadeSelectionID);
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
            return objResp;
        }

        public List<clsBundleShade> Fn_Get_Bundle_Shade(clsBundleShade objReq)
        {
            var objResp = new List<clsBundleShade>();
            var obj = new clsBundleShade();
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT ColorSelectionID, ShadeSelectionID, ShadeName, Piles,";
                strSql = strSql + " CreatedBy, FORMAT(CreatedOn, 'dd-MMM-yyy') AS CreatedOn FROM BundleSizeSelection WHERE 1=1";
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
                strSql = strSql + " ORDER BY ShadeSelectionID DESC ";

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
                        obj.ShadeName = Convert.ToString(ds.Tables[0].Rows[i]["ShadeName"]);
                        obj.CreatedBy = Convert.ToInt32(ds.Tables[0].Rows[i]["CreatedBy"]);
                        obj.Piles = Convert.ToInt32(ds.Tables[0].Rows[i]["Piles"]);
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
            return objResp;
        }

        #endregion End Shade- Bundle 7-Feb-2026

        #region Start Compile- Bundle 7-Feb-2026

        public clsBundleCompile Fn_Insert_Bundle_Compile(clsBundleCompile objReq)
        {
            var objResp = new clsBundleCompile();

            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                if (objReq.BundleID == 0 || objReq.BundleID == null)
                {
                    Fn_Get_MXID("BundleCompile", "BundleID");
                    objReq.BundleID = mxID;
                }

                SqlCommand cmd = new SqlCommand("USP_BUNDLE_LAYER", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BundleID", objReq.BundleID);
                cmd.Parameters.AddWithValue("@ShadeSelectionID", objReq.ShadeSelectionID);
                cmd.Parameters.AddWithValue("@BundleNo", objReq.BundleNo);
                cmd.Parameters.AddWithValue("@SizeName", objReq.SizeName);
                cmd.Parameters.AddWithValue("@ColorName", objReq.ColorName);
                cmd.Parameters.AddWithValue("@ShadeName", objReq.ShadeName);
                cmd.Parameters.AddWithValue("@Qty", objReq.Qty);
                cmd.Parameters.AddWithValue("@PlyFrom", objReq.PlyFrom);
                cmd.Parameters.AddWithValue("@PlyTo", objReq.PlyTo);
                cmd.Parameters.AddWithValue("@LotNo", objReq.LotNo);
                cmd.Parameters.AddWithValue("@SubSection", objReq.SubSection);
                cmd.Parameters.AddWithValue("@Dispatch", objReq.IsDispatch);
                cmd.Parameters.AddWithValue("@StyleCode", objReq.StyleCode);
                cmd.Parameters.AddWithValue("@OrderNo", objReq.OrderNo);
                cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                cmd.Parameters.AddWithValue("@QueryType", "InsertBundleCompile");
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
                Logger.WriteLog("Function Name : Fn_Insert_Bundle_Compile", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }

        public clsBundleCompile Fn_Delete_Bundle_Compile(clsBundleCompile objReq)
        {
            var objResp = new clsBundleCompile();
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
            return objResp;
        }

        public List<clsBundleCompile> Fn_Get_Bundle_Compile(clsBundleCompile objReq)
        {
            var objResp = new List<clsBundleCompile>();
            var obj = new clsBundleCompile();
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT BundleID, ShadeSelectionID, BundleNo, SizeName, ColorName, ShadeName,";
                strSql = strSql + " Qty, PlyFrom, PlyTo, LotNo, SubSection, Dispatch, StyleCode, OrderNo,";
                strSql = strSql + " CreatedBy, FORMAT(CreatedOn, 'dd-MMM-yyy') AS CreatedOn FROM BundleSizeSelection WHERE 1=1";
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
                
                strSql = strSql + " ORDER BY BundleID DESC ";

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
                        obj.ShadeSelectionID = Convert.ToInt64(ds.Tables[0].Rows[i]["ShadeSelectionID"]);
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
            return objResp;
        }

        #endregion End Compile- Bundle 7-Feb-2026
    }
}