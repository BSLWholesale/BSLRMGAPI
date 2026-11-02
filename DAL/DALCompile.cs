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
    public class DALCompile
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

                SqlCommand cmd = new SqlCommand("USP_BUNDLE_LAYER_TEST", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LayID", objReq.LayID);
                cmd.Parameters.AddWithValue("@SizeSelectionID", objReq.SizeSelectionID);
                cmd.Parameters.AddWithValue("@SizeName", objReq.SizeName);
                cmd.Parameters.AddWithValue("@SizeID", objReq.SizeID);
                cmd.Parameters.AddWithValue("@Freq", objReq.Freq);
                cmd.Parameters.AddWithValue("@OrderNo", objReq.OrderNo);
                cmd.Parameters.AddWithValue("@Marker", objReq.Marker);
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

                SqlCommand cmd = new SqlCommand("USP_BUNDLE_LAYER_TEST", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ColorSelectionID", objReq.ColorSelectionID);
                cmd.Parameters.AddWithValue("@LayID", objReq.LayID);
                cmd.Parameters.AddWithValue("@ColorName", objReq.ColorName);
                cmd.Parameters.AddWithValue("@OrderNo", objReq.OrderNo);
                cmd.Parameters.AddWithValue("@Marker", objReq.Marker);
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
                    string strCriteria = " AND OrderNo = '" + objReq.OrderNo + "' AND Marker = '" + objReq.Marker + "'";
                    Fn_Get_MXID("BundleLayerMaster", "LayID", strCriteria);
                    objReq.LayID = mxID;
                }

                SqlCommand cmd = new SqlCommand("USP_BUNDLE_LAYER_TEST", Con);
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

                SqlCommand cmd = new SqlCommand("USP_BUNDLE_LAYER_TEST", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LayID", objReq.LayID);
                cmd.Parameters.AddWithValue("@OrderNo", objReq.OrderNo);
                cmd.Parameters.AddWithValue("@Marker", objReq.Marker);
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

                SqlCommand cmd = new SqlCommand("USP_BUNDLE_LAYER_TEST", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SizeSelectionID", objReq.SizeSelectionID);
                cmd.Parameters.AddWithValue("@OrderNo", objReq.OrderNo);
                cmd.Parameters.AddWithValue("@Marker", objReq.Marker);
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
                if (!String.IsNullOrWhiteSpace(objReq.Marker))
                {
                    strSql = strSql + " AND Marker = @Marker";
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
                if (!String.IsNullOrWhiteSpace(objReq.Marker))
                {
                    cmd.Parameters.AddWithValue("@Marker", objReq.Marker);
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
                if (!String.IsNullOrWhiteSpace(objReq.Marker))
                {
                    strSql = strSql + " AND Marker = @Marker ";
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
                if (!String.IsNullOrWhiteSpace(objReq.Marker))
                {
                    cmd.Parameters.AddWithValue("@Marker", objReq.Marker);
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

                SqlCommand cmd = new SqlCommand("USP_BUNDLE_LAYER_TEST", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ColorSelectionID", objReq.ColorSelectionID);
                cmd.Parameters.AddWithValue("@OrderNo", objReq.OrderNo);
                cmd.Parameters.AddWithValue("@Marker", objReq.Marker);
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

                SqlCommand cmd = new SqlCommand("USP_BUNDLE_LAYER_TEST", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ColorSelectionID", objReq.ColorSelectionID);
                cmd.Parameters.AddWithValue("@ShadeSelectionID", objReq.ShadeSelectionID);
                cmd.Parameters.AddWithValue("@ShadeName", objReq.ShadeName);
                cmd.Parameters.AddWithValue("@Plies", objReq.Plies);
                cmd.Parameters.AddWithValue("@OrderNo", objReq.OrderNo);
                cmd.Parameters.AddWithValue("@Marker", objReq.Marker);
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
                if (!String.IsNullOrWhiteSpace(objReq.Marker))
                {
                    strSql = strSql + " AND Marker = @Marker ";
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
                if (!String.IsNullOrWhiteSpace(objReq.Marker))
                {
                    cmd.Parameters.AddWithValue("@Marker", objReq.Marker);
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

                SqlCommand cmd = new SqlCommand("USP_BUNDLE_LAYER_TEST", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ShadeSelectionID", objReq.ShadeSelectionID);
                cmd.Parameters.AddWithValue("@OrderNo", objReq.OrderNo);
                cmd.Parameters.AddWithValue("@Marker", objReq.Marker);
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

        public clsBundleCompile Fn_Insert_Bundle_Compile(clsBundleCompile objReq)
        {
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Insert_Bundle_Compile");
            var objResp = new clsBundleCompile();
            string strCriteria = "";

            try
            {
                // 1. Get subsection list
                var subSectionList = Fn_Get_Subsection_List(objReq);

                if (subSectionList == null || subSectionList.Count == 0 || subSectionList[0].vErrorMsg != "Success")
                {
                    objResp.vErrorCode = 404;
                    objResp.vErrorMsg = "Please upload subsection, then compile.";
                    return objResp;
                }

                // 2. Check operation breakdown
                var checkOPBreakDown = new clsOPBreackDownMaster
                {
                    StyleCode = objReq.StyleCode,
                    CreatedBy = objReq.CreatedBy
                };

                checkOPBreakDown = _DALOrder.Fn_Check_Exist_style_In_Master(checkOPBreakDown);

                if (checkOPBreakDown.vErrorMsg != "Success" && checkOPBreakDown.vErrorCode != 200)
                {
                    objResp.vErrorCode = 404;
                    objResp.vErrorMsg = "Please upload subsection, then compile.";
                    return objResp;
                }

                // 3. Get color and shade list
                var colorshade = new clsColorShade
                {
                    OrderNo = objReq.OrderNo,
                    LayID = objReq.LayID,
                    MarkerType = objReq.Marker
                };

                var objColorShadeList = Fn_Get_Color_With_Shade(colorshade);

                if (objColorShadeList == null || objColorShadeList.Count == 0 || objColorShadeList[0].vErrorMsg != "Success")
                {
                    objResp.vErrorCode = 404;
                    objResp.vErrorMsg = "Color and shade not found";
                    return objResp;
                }

                // 4. Validate size list
                if (objReq._oSizeList == null || objReq._oSizeList.Count == 0)
                {
                    objResp.vErrorCode = 400;
                    objResp.vErrorMsg = "Size list not found";
                    return objResp;
                }

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }

                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                int originalBundleQty = objReq.BunleQty;

                if (originalBundleQty <= 0)
                {
                    objResp.vErrorCode = 400;
                    objResp.vErrorMsg = "Bundle quantity must be greater than zero.";
                    return objResp;
                }

                long lastCompiledPly = 0;

                using (SqlCommand plyCmd = new SqlCommand(@"SELECT ISNULL(MAX(TRY_CONVERT(BIGINT, PlyTo)), 0) FROM BundleCompile WHERE OrderNo = @OrderNo AND Marker = @Marker;", Con))
                {
                    plyCmd.Parameters.AddWithValue("@OrderNo", objReq.OrderNo);
                    plyCmd.Parameters.AddWithValue("@Marker", objReq.Marker);

                    object plyResult = plyCmd.ExecuteScalar();

                    if (plyResult != null && plyResult != DBNull.Value)
                        lastCompiledPly = Convert.ToInt64(plyResult);
                }

                // Starts at 1 when there are no previous records.
                long nextPly = lastCompiledPly + 1;

                int processedBundles = 0;

                //  Loop through sizes
                foreach (clsBundleSizeList osize in objReq._oSizeList)
                {
                    string size = osize.SizeName;
                    int vFreq = Convert.ToInt32(osize.Freq);

                    if (vFreq <= 0)
                        continue;

                    //  Loop through frequency
                    for (int freqLoop = 1; freqLoop <= vFreq; freqLoop++)
                    {
                        strCriteria = " AND OrderNo = '" + objReq.OrderNo + "' AND Marker = '" + objReq.Marker + "'";
                        long mxLotNo = Fn_Get_MXID("BundleCompile", "LotNo", strCriteria);

                        objReq.LotNo = Convert.ToInt32(mxLotNo);
                        objReq.BunleQty = originalBundleQty;

                        //  Loop through color and shade
                        foreach (var colorShade in objColorShadeList)
                        {
                            int plies = Convert.ToInt32(colorShade.Plies);

                            if (plies <= 0)
                                continue;

                            int bundleQty = originalBundleQty;
                            int lastQty = plies % bundleQty;

                            int totalBundles = plies / bundleQty;

                            if (lastQty > 0)
                                totalBundles++;

                            long mxBundleNo = Fn_Get_MXID("BundleCompile", "BundleNo", strCriteria);
                            int bundleStart = Convert.ToInt32(mxBundleNo);
                            int finalBundleNo = bundleStart + totalBundles - 1;

                            //  Loop through bundles
                            while (bundleStart <= finalBundleNo)
                            {
                                int currentQty;

                                if (bundleStart == finalBundleNo && lastQty == 0)
                                {
                                    currentQty = bundleQty;
                                }
                                else if (bundleStart == finalBundleNo && lastQty > 0)
                                {
                                    currentQty = lastQty;
                                }
                                else if (bundleStart == finalBundleNo - 1 && lastQty > 0 && lastQty <= 5)
                                {
                                    int remainingQty = bundleQty + lastQty;

                                    currentQty = remainingQty / 2;
                                    lastQty = remainingQty - currentQty;
                                }
                                else
                                {
                                    currentQty = bundleQty;
                                }

                                objReq.BundleNo = bundleStart;


                                long bundlePlyFrom = nextPly;
                                long bundlePlyTo = bundlePlyFrom + currentQty - 1;

                                foreach (var subSection in subSectionList)
                                {
                                    bool isPostAssembly = string.Equals(subSection.SubSection, "POST ASSEMBLY", StringComparison.OrdinalIgnoreCase);
                                    int insertCount = isPostAssembly ? currentQty : 1;

                                    for (int pCount = 0; pCount < insertCount; pCount++)
                                    {
                                        if (isPostAssembly)
                                        {
                                            objReq.Qty = 1;
                                            objReq.PlyFrom = Convert.ToInt32(bundlePlyFrom + pCount);
                                            objReq.PlyTo = objReq.PlyFrom;
                                            objReq.BunleQty = 1;
                                        }
                                        else
                                        {
                                            objReq.Qty = currentQty;
                                            objReq.PlyFrom = Convert.ToInt32(bundlePlyFrom);
                                            objReq.PlyTo = Convert.ToInt32(bundlePlyTo);
                                            objReq.BunleQty = currentQty;
                                        }

                                        // Generate BundleID
                                        var objMxId = Fn_Get_Max_ID_With_DATE(
                                            new clsMAXID
                                            {
                                                TBLName = "BundleCompile",
                                                IDField = "BundleID",
                                                DateField = "CreatedOn",
                                                strPreFix = ""
                                            }
                                        );

                                        objReq.BundleID = objMxId.nMAXID;

                                        using (SqlCommand cmd = new SqlCommand("USP_BUNDLE_LAYER_TEST", Con))
                                        {
                                            if (Con.State == ConnectionState.Broken) { Con.Close(); }
                                            if (Con.State == ConnectionState.Closed) { Con.Open(); }

                                            cmd.CommandType = CommandType.StoredProcedure;
                                            cmd.Parameters.AddWithValue("@BundleID", objReq.BundleID);
                                            cmd.Parameters.AddWithValue("@LayID", objReq.LayID);
                                            cmd.Parameters.AddWithValue("@BundleNo", objReq.BundleNo);
                                            cmd.Parameters.AddWithValue("@SizeName", size);
                                            cmd.Parameters.AddWithValue("@ColorName", colorShade.ColorName);
                                            cmd.Parameters.AddWithValue("@ShadeName", colorShade.ShadeName);
                                            cmd.Parameters.AddWithValue("@Qty", objReq.Qty);
                                            cmd.Parameters.AddWithValue("@PlyFrom", objReq.PlyFrom);
                                            cmd.Parameters.AddWithValue("@PlyTo", objReq.PlyTo);
                                            cmd.Parameters.AddWithValue("@LotNo", objReq.LotNo);
                                            cmd.Parameters.AddWithValue("@SubSection", subSection.SubSection);
                                            cmd.Parameters.AddWithValue("@IsDispatch", objReq.IsDispatch);
                                            cmd.Parameters.AddWithValue("@StyleCode", objReq.StyleCode);
                                            cmd.Parameters.AddWithValue("@OrderNo", objReq.OrderNo);
                                            cmd.Parameters.AddWithValue("@Marker", objReq.Marker);
                                            cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                                            cmd.Parameters.AddWithValue("@BundleQty", objReq.BunleQty);
                                            cmd.Parameters.AddWithValue("@QueryType", "InsertBundleCompile");
                                            int result = cmd.ExecuteNonQuery();

                                            if (result <= 0)
                                            {
                                                objResp.vErrorCode = 400;
                                                objResp.vErrorMsg = "Insert Failed";
                                                return objResp;
                                            }
                                        }
                                    }
                                }

                                nextPly = bundlePlyTo + 1;

                                bundleStart++;
                                processedBundles++;
                            }
                        }
                    }
                }

                //  Update layer quantity
                if (processedBundles > 0)
                {
                    var obj = new clsBundleLayerMaster
                    {
                        LayID = objReq.LayID,
                        Qty = objReq.CompileQty,
                        OrderNo = objReq.OrderNo,
                        Marker = objReq.Marker,
                        CreatedBy = objReq.CreatedBy
                    };

                    Fn_Update_Layer_Qty(obj);

                    objResp.vErrorCode = 200;
                    objResp.vErrorMsg = "Success";
                }
                else
                {
                    objResp.vErrorCode = 400;
                    objResp.vErrorMsg = "No bundles were generated.";
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
                if (Con.State != ConnectionState.Closed)
                    Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Insert_Bundle_Compile");
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
                if (!String.IsNullOrWhiteSpace(objReq.Marker))
                {
                    strSql = strSql + " AND Marker = @Marker";
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
                if (!String.IsNullOrWhiteSpace(objReq.Marker))
                {
                    cmd.Parameters.AddWithValue("@Marker", objReq.Marker);
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
                strSql = strSql + " CreatedBy, FORMAT(CreatedOn, 'dd-MMM-yyy') AS CreatedOn, ShadeName FROM BundleCompile WHERE 1=1";
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
                if (objReq.BundleNo != 0 && objReq.BundleNo != null)
                {
                    strSql = strSql + " AND BundleNo = @BundleNo";
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
                if (!String.IsNullOrWhiteSpace(objReq.Marker))
                {
                    cmd.Parameters.AddWithValue("@Marker", objReq.Marker);
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
                if (objReq.BundleNo != 0 && objReq.BundleNo != null)
                {
                    cmd.Parameters.AddWithValue("@BundleNo", objReq.BundleNo);
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
                        obj.ShadeName = Convert.ToString(ds.Tables[0].Rows[i]["ShadeName"]);
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
                if (!String.IsNullOrWhiteSpace(objReq.Marker))
                {
                    strSql = strSql + " AND M.ProcessName = @Marker";
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
                if (!String.IsNullOrWhiteSpace(objReq.Marker))
                {
                    cmd.Parameters.AddWithValue("@Marker", objReq.Marker);
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
                if (!String.IsNullOrWhiteSpace(objReq.Marker))
                {
                    strSql = strSql + " AND Marker = @Marker ";
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
                if (!String.IsNullOrWhiteSpace(objReq.Marker))
                {
                    cmd.Parameters.AddWithValue("@Marker", objReq.Marker);
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

        public clsBundleLayerMaster Fn_Update_Print_layarDate(clsBundleLayerMaster objReq)
        {
            var objResp = new clsBundleLayerMaster();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Update_Print_layarDate");
            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_BUNDLE_LAYER_TEST", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LayID", objReq.LayID);
                cmd.Parameters.AddWithValue("@OrderNo", objReq.OrderNo);
                cmd.Parameters.AddWithValue("@Marker", objReq.Marker);
                cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                cmd.Parameters.AddWithValue("@QueryType", "UpdateLayerPrintDate");
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
                    objResp.vErrorMsg = "Layer print date updating failed";
                }
            }
            catch (Exception exp)
            {
                objResp.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Update_Print_layarDate", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Update_Print_layarDate");
            return objResp;
        }

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

                string strSql = "Select BCS.OrderNo, BCS.LayID, BCS.ColorSelectionID, BCS.ColorName, BSS.ShadeName, BSS.Plies, BCS.Marker from BundleColorSelection BCS ";
                strSql = strSql + " INNER JOIN BundleShadeSelection BSS ON BCS.ColorSelectionID = BSS.ColorSelectionID AND BCS.LayID = BSS.LayID WHERE 1=1 ";

                if (!String.IsNullOrWhiteSpace(objReq.OrderNo))
                {
                    strSql = strSql + " AND BCS.OrderNo = @OrderNo";
                }
                if (!String.IsNullOrWhiteSpace(objReq.MarkerType))
                {
                    strSql = strSql + " AND BCS.Marker = @MarkerType";
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
                if (!String.IsNullOrWhiteSpace(objReq.MarkerType))
                {
                    cmd.Parameters.AddWithValue("@MarkerType", objReq.MarkerType);
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

                SqlCommand cmd = new SqlCommand("USP_BUNDLE_LAYER_TEST", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LayID", objReq.LayID);
                cmd.Parameters.AddWithValue("@Qty", objReq.Qty);
                cmd.Parameters.AddWithValue("@OrderNo", objReq.OrderNo);
                cmd.Parameters.AddWithValue("@Marker", objReq.Marker);
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
        

    }
}