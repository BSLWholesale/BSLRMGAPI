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

        public clsProductionMastr Fn_Insert_Production(clsProductionMastr objReq)
        {
            var objResp = new clsProductionMastr();
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
                Logger.WriteLog("Function Name : Fn_Insert_Production", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }

        public List<clsProductionMastr> Fn_Get_Production_Master(clsProductionMastr objReq)
        {
            var objResp = new List<clsProductionMastr>();
            var obj = new clsProductionMastr();
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT ProductionOrderNo, OrderDate, ProductionDeliveryDate, Merchandiser, SalesOrderNo, PONo,";
                strSql = strSql + " FabIndNo, OrderQty, StyleNo, StyleName, Buyer, Brand, PlantName FROM ProductionMaster WHERE 1=1";
                if (objReq.ProductionOrderNo != 0)
                {
                    strSql = strSql + " AND ProductionOrderNo = @ProductionOrderNo";
                }
                if (objReq.OrderDate != "")
                {
                    strSql = strSql + " AND OrderDate = @OrderDate";
                }
                if (objReq.SalesOrderNo != 0)
                {
                    strSql = strSql + " AND SalesOrderNo = @SalesOrderNo";
                }
                if (objReq.StyleNo != "")
                {
                    strSql = strSql + " AND StyleNo = @StyleNo";
                }
                if (objReq.StyleName != "")
                {
                    strSql = strSql + " AND StyleName = @StyleName";
                }
                if (objReq.Buyer != "")
                {
                    strSql = strSql + " AND Buyer = @Buyer";
                }
                if (objReq.Brand != "")
                {
                    strSql = strSql + " AND Brand = @Brand";
                }

                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;
                if (objReq.ProductionOrderNo != 0)
                {
                    cmd.Parameters.AddWithValue("@ProductionOrderNo", objReq.ProductionOrderNo);
                }
                if (objReq.OrderDate != "")
                {
                    cmd.Parameters.AddWithValue("@OrderDate", objReq.OrderDate);
                }
                if (objReq.SalesOrderNo != 0)
                {
                    cmd.Parameters.AddWithValue("@SalesOrderNo", objReq.SalesOrderNo);
                }
                if (objReq.StyleNo != "")
                {
                    cmd.Parameters.AddWithValue("@StyleNo", objReq.StyleNo);
                }
                if (objReq.StyleName != "")
                {
                    cmd.Parameters.AddWithValue("@StyleName", objReq.StyleName);
                }
                if (objReq.Buyer != "")
                {
                    cmd.Parameters.AddWithValue("@Buyer", objReq.Buyer);
                }
                if (objReq.Brand != "")
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
                        obj = new clsProductionMastr();
                        obj.ProductionOrderNo = Convert.ToInt64(ds.Tables[0].Rows[i]["ProductionOrderNo"]);
                        obj.OrderDate = Convert.ToString(ds.Tables[0].Rows[i]["OrderDate"]);
                        obj.ProductionDeliveryDate = Convert.ToString(ds.Tables[0].Rows[i]["ProductionDeliveryDate"]);

                        obj.Merchandiser = Convert.ToString(ds.Tables[0].Rows[i]["Merchandiser"]);
                        obj.SalesOrderNo = Convert.ToInt16(ds.Tables[0].Rows[i]["SalesOrderNo"]);
                        obj.PONo = Convert.ToInt16(ds.Tables[0].Rows[i]["PONo"]);
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
                Logger.WriteLog("Function Name : Fn_Get_Production_Master", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
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

        public clsProductionMastr Fn_Update_Production(clsProductionMastr objReq)
        {
            var objResp = new clsProductionMastr();
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

                string strSql = "SELECT " + obj.FieldName + " FROM " + obj.TableName + " WHERE 1=1";
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
    }
}