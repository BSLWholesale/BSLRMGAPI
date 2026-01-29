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
    public class DALOrder
    {
        SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["BSL"].ConnectionString);

        Int64 mxID = 0;

        public Int64 Fn_Get_MXID(string strTBLName, string strFieldName)
        {
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

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
            finally
            {
                Con.Close();
            }
            return mxID;
        }

        public clsOrderMaster Fn_Insert_Order_Naster(clsOrderMaster objReq)
        {
            var objResp = new clsOrderMaster();
            if (objReq.ID == 0 || objReq.ID == null)
            {
                Fn_Get_MXID("OrderMaster", "ID");
                objReq.ID = mxID;
            }
            
            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_PRODUCTION", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", objReq.ID);
                cmd.Parameters.AddWithValue("@OrderNo", objReq.OrderNo);
                cmd.Parameters.AddWithValue("@Qty", objReq.Qty);
                cmd.Parameters.AddWithValue("@IsFinished", objReq.IsFinished);
                cmd.Parameters.AddWithValue("@IsStkr", objReq.IsStkr);
                cmd.Parameters.AddWithValue("@BundleQty", objReq.BundleQty);
                cmd.Parameters.AddWithValue("@OrderDate", objReq.OrderDate);
                cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                cmd.Parameters.AddWithValue("@QueryType", "InsertOrderMaster");
                int i = 0;
                i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    objResp.vErrorCode = 200;
                    objResp.vErrorMsg = "Success";

                    foreach (clsOrderDetail _oList in objReq.oDetail)
                    {
                        if (_oList.DetailID == 0 || _oList.DetailID == null)
                        {
                            Fn_Get_MXID("OrderOetail", "DetailID");
                            _oList.DetailID = mxID;
                        }

                        SqlCommand cm1 = new SqlCommand("USP_PRODUCTION", Con);
                        cm1.CommandType = CommandType.StoredProcedure;
                        cm1.Parameters.AddWithValue("@DetailID", _oList.DetailID);
                        cm1.Parameters.AddWithValue("@OrderNo", objReq.ID);
                        cm1.Parameters.AddWithValue("@Color", _oList.Color);
                        cm1.Parameters.AddWithValue("@Size", _oList.Size);
                        cm1.Parameters.AddWithValue("@Qty", _oList.Qty);
                        cm1.Parameters.AddWithValue("@ExtraQty", _oList.ExtraQty);
                        cm1.Parameters.AddWithValue("@CreatedBy", _oList.CreatedBy);
                        cm1.Parameters.AddWithValue("@QueryType", "InsertOrderDetail");
                        int j = cm1.ExecuteNonQuery();
                        if (j > 0)
                        {
                            objResp.vErrorMsg = "Success";
                        }
                        else
                        {
                            objResp.vErrorMsg = "Order detail inserting failed ";
                            return objResp;
                        }
                    }
                }
                else
                {
                    objResp.vErrorCode = 400;
                    objResp.vErrorMsg = "Order master inserting failed";
                }
            }
            catch (Exception exp)
            {
                objResp.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Insert_Order_Naster", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }

        public List<clsOrderMaster> Fn_Get_Order_Master(clsOrderMaster objReq)
        {
            var objResp = new List<clsOrderMaster>();
            var obj = new clsOrderMaster();
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT ID, OrderNo, Qty, IsFinished, IsStkr, BundleQty, FORMAT(OrderDate, 'dd-MMM-yyy') AS OrderDate,";
                 strSql = strSql + " CreatedBy, FORMAT(CreatedOn, 'dd-MMM-yyy') AS CreatedOn  FROM OrderMaster WHERE 1=1";
                if (objReq.ID != 0 && objReq.ID != null)
                {
                    strSql = strSql + " AND ID = @ID";
                }
                

                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;
                if (objReq.ID != 0 && objReq.ID != null)
                {
                    cmd.Parameters.AddWithValue("@ID", objReq.ID);
                }
                
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsOrderMaster();
                        obj.ID = Convert.ToInt64(ds.Tables[0].Rows[i]["ID"]);
                        obj.OrderNo = Convert.ToString(ds.Tables[0].Rows[i]["OrderNo"]);
                        obj.Qty = Convert.ToInt16(ds.Tables[0].Rows[i]["Qty"]);

                        obj.IsFinished = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsFinished"]);
                        obj.IsStkr = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsStkr"]);
                        obj.BundleQty = Convert.ToInt16(ds.Tables[0].Rows[i]["BundleQty"]);
                        obj.OrderDate = Convert.ToString(ds.Tables[0].Rows[i]["OrderDate"]);
                        obj.CreatedBy = Convert.ToInt16(ds.Tables[0].Rows[i]["CreatedBy"]);
                        obj.CreatedOn = Convert.ToString(ds.Tables[0].Rows[i]["CreatedOn"]);

                        var objpDetail = new clsOrderDetail();
                        objpDetail.OrderNo = Convert.ToString(obj.ID);
                        obj.oDetail = Fn_Get_Order_Detail(objpDetail);

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
                Logger.WriteLog("Function Name : Fn_Get_Order_Master", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp[0].vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }

        public List<clsOrderDetail> Fn_Get_Order_Detail(clsOrderDetail objReq)
        {
            var objResp = new List<clsOrderDetail>();
            var obj = new clsOrderDetail();
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT DetailID, OrderNo, Color, Size, Qty, ExtraQty, CreatedBy, FORMAT(CreatedOn, 'dd-MMM-yyy') AS CreatedOn FROM OrderDetail WHERE 1=1";

                if (objReq.DetailID != 0 && objReq.DetailID != null)
                {
                    strSql = strSql + " AND DetailID = @DetailID";
                }
                if (!String.IsNullOrWhiteSpace(objReq.OrderNo))
                {
                    strSql = strSql + " AND OrderNo = @OrderNo";
                }

                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;
                if (objReq.DetailID != 0 && objReq.DetailID != null)
                {
                    cmd.Parameters.AddWithValue("@DetailID", objReq.DetailID);
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
                        obj = new clsOrderDetail();
                        obj.DetailID = Convert.ToInt64(ds.Tables[0].Rows[i]["DetailID"]);
                        obj.OrderNo = Convert.ToString(ds.Tables[0].Rows[i]["OrderNo"]);
                        obj.Color = Convert.ToString(ds.Tables[0].Rows[i]["Color"]);
                        obj.Size = Convert.ToString(ds.Tables[0].Rows[i]["Size"]);

                        obj.Color = Convert.ToString(ds.Tables[0].Rows[i]["Color"]);
                        obj.Qty = Convert.ToInt32(ds.Tables[0].Rows[i]["Qty"]);
                        obj.ExtraQty = Convert.ToInt32(ds.Tables[0].Rows[i]["ExtraQty"]);
                        obj.CreatedBy = Convert.ToInt16(ds.Tables[0].Rows[i]["CreatedBy"]);
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
                Logger.WriteLog("Function Name : Fn_Get_Order_Detail", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp[0].vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }
    }
}