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
    public class DALOrder
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

        public clsOrderMaster Fn_Insert_Order_Master(clsOrderMaster objReq)
        {
            var objResp = new clsOrderMaster();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Insert_Order_Master");
            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                if (objReq.ID == 0 || objReq.ID == null)
                {
                    Fn_Get_MXID("OrderMaster", "ID");
                    objReq.ID = mxID;
                }

                SqlCommand cmd = new SqlCommand("USP_ORDER_MASTER", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", objReq.ID);
                cmd.Parameters.AddWithValue("@OrderNo", objReq.OrderNo);
                cmd.Parameters.AddWithValue("@Qty", objReq.Qty);
                cmd.Parameters.AddWithValue("@IsFinished", objReq.IsFinished);
                cmd.Parameters.AddWithValue("@IsStkr", objReq.IsStkr);
                cmd.Parameters.AddWithValue("@BundleQty", objReq.BundleQty);
                cmd.Parameters.AddWithValue("@OrderDate", objReq.OrderDate);
                cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                cmd.Parameters.AddWithValue("@CreatedOn", objReq.CreatedOn);
                cmd.Parameters.AddWithValue("@StyleCode", objReq.StyleCode);
                cmd.Parameters.AddWithValue("@QueryType", "InsertOrderMaster");
                int i = 0;
                i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    objResp.vErrorCode = 200;
                    objResp.vErrorMsg = "Success";

                    if (objReq.oDetail != null)
                    {
                        long DetailID = 0;
                        foreach (clsOrderDetail _oList in objReq.oDetail)
                        {
                            DetailID = Fn_Get_MXID("OrderDetail", "DetailID");
                            _oList.DetailID = DetailID;

                            SqlCommand cm1 = new SqlCommand("USP_ORDER_MASTER", Con);
                            cm1.CommandType = CommandType.StoredProcedure;
                            cm1.Parameters.AddWithValue("@DetailID", _oList.DetailID);
                            cm1.Parameters.AddWithValue("@OrderNo", objReq.ID);
                            cm1.Parameters.AddWithValue("@Color", _oList.Color);
                            cm1.Parameters.AddWithValue("@Size", _oList.Size);
                            cm1.Parameters.AddWithValue("@Qty", _oList.Qty);
                            cm1.Parameters.AddWithValue("@ExtraQty", _oList.ExtraQty);
                            cm1.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                            cmd.Parameters.AddWithValue("@StyleCode", objReq.StyleCode);
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
                Logger.WriteLog("Function Name : Fn_Insert_Order_Master", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Insert_Order_Master");
            return objResp;
        }

        public List<clsOrderMaster> Fn_Get_Order_Master(clsOrderMaster objReq)
        {
            var objResp = new List<clsOrderMaster>();
            var obj = new clsOrderMaster();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Get_Order_Master");
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT ID, OrderNo, Qty, IsFinished, IsStkr, BundleQty, FORMAT(OrderDate, 'dd-MMM-yyy') AS OrderDate,";
                strSql = strSql + " CreatedBy, CreatedOn, StyleCode  FROM OrderMaster WHERE 1=1";
                if (objReq.ID != 0 && objReq.ID != null)
                {
                    strSql = strSql + " AND ID = @ID";
                }
                if (!String.IsNullOrWhiteSpace(objReq.StyleCode))
                {
                    strSql = strSql + " AND StyleCode = @StyleCode";
                }
                strSql = strSql + " ORDER BY CreatedOn DESC";

                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;
                if (objReq.ID != 0 && objReq.ID != null)
                {
                    cmd.Parameters.AddWithValue("@ID", objReq.ID);
                }
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
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsOrderMaster();
                        obj.ID = Convert.ToInt64(ds.Tables[0].Rows[i]["ID"]);
                        obj.OrderNo = Convert.ToString(ds.Tables[0].Rows[i]["OrderNo"]);
                        obj.Qty = Convert.ToInt32(ds.Tables[0].Rows[i]["Qty"]);

                        obj.IsFinished = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsFinished"]);
                        obj.IsStkr = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsStkr"]);
                        obj.BundleQty = Convert.ToInt32(ds.Tables[0].Rows[i]["BundleQty"]);
                        obj.OrderDate = Convert.ToString(ds.Tables[0].Rows[i]["OrderDate"]);
                        obj.CreatedBy = Convert.ToInt32(ds.Tables[0].Rows[i]["CreatedBy"]);
                        obj.CreatedOn = Convert.ToString(ds.Tables[0].Rows[i]["CreatedOn"]);

                        //var objpDetail = new clsOrderDetail();
                        //objpDetail.OrderNo = Convert.ToString(obj.ID);
                        //obj.oDetail = Fn_Get_Order_Detail(objpDetail);

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
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Get_Order_Master");
            return objResp;
        }

        public List<clsOrderDetail> Fn_Get_Order_Detail(clsOrderDetail objReq)
        {
            var objResp = new List<clsOrderDetail>();
            var obj = new clsOrderDetail();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Get_Order_Detail");
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
                strSql = strSql + " ORDER BY Color, Size ASC ";

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
                        obj.Qty = Convert.ToInt32(ds.Tables[0].Rows[i]["Qty"]);
                        obj.ExtraQty = Convert.ToInt32(ds.Tables[0].Rows[i]["ExtraQty"]);
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
                Logger.WriteLog("Function Name : Fn_Get_Order_Detail", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Get_Order_Detail");
            return objResp;
        }

        #region Start Process Master 4-Feb-2026

        public clsProcessMaster Fn_Insert_New_Process(clsProcessMaster objReq)
        {
            var objResp = new clsProcessMaster();
            //if (objReq.ID == 0 || objReq.ID == null)
            //{
            //    Fn_Get_MXID("ProcessMaster", "ID");
            //    objReq.ID = mxID;
            //}
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Insert_New_Process");
            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                if (objReq.ID == 0 || objReq.ID == null)
                {
                    Fn_Get_MXID("ProcessMaster", "ID");
                    objReq.ID = mxID;
                }

                SqlCommand cmd = new SqlCommand("USP_ORDER_MASTER", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", objReq.ID);
                cmd.Parameters.AddWithValue("@ProcessName", objReq.ProcessName);
                cmd.Parameters.AddWithValue("@IsProduction", objReq.IsProduction);
                cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                cmd.Parameters.AddWithValue("@QueryType", "InsertProcessMaster");
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
                Logger.WriteLog("Function Name : Fn_Insert_New_Process", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Insert_New_Process");
            return objResp;
        }

        public clsProcessMaster Fn_Delete_Process(clsProcessMaster objReq)
        {
            var objResp = new clsProcessMaster();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Delete_Process");
            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_ORDER_MASTER", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", objReq.ID);
                cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                cmd.Parameters.AddWithValue("@QueryType", "DeleteProcessMaster");
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
                Logger.WriteLog("Function Name : Fn_Delete_Process", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Delete_Process");
            return objResp;
        }

        public List<clsProcessMaster> Fn_Get_ProcessMaster(clsProcessMaster objReq)
        {
            var objResp = new List<clsProcessMaster>();
            var obj = new clsProcessMaster();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Get_ProcessMaster");
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT ID, ProcessName, IsProduction, CreatedBy, FORMAT(CreatedOn, 'dd-MMM-yyy') AS CreatedOn FROM ProcessMaster WHERE 1=1 AND IsActiveProcess = 0 ";
                if (!String.IsNullOrWhiteSpace(objReq.ProcessName))
                {
                    strSql = strSql + " AND ProcessName = @ProcessName";
                }
                if (objReq.ID != 0 && objReq.ID != null)
                {
                    strSql = strSql + " AND ID = @ID ";
                }


                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;
                if (!String.IsNullOrWhiteSpace(objReq.ProcessName))
                {
                    cmd.Parameters.AddWithValue("@ProcessName", objReq.ProcessName);
                }
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
                        obj = new clsProcessMaster();
                        obj.ID = Convert.ToInt32(ds.Tables[0].Rows[i]["ID"]);
                        obj.ProcessName = Convert.ToString(ds.Tables[0].Rows[i]["ProcessName"]);
                        obj.IsProduction = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsProduction"]);
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
                Logger.WriteLog("Function Name : Fn_Get_ProcessMaster", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Get_ProcessMaster");
            return objResp;
        }

        #endregion End Process Master 4-Feb-2026

        public clsOPBreackDownMaster Fn_Upload_Operation_BreackdownFile(clsOPBreackDownMaster objReq)
        {
            var objResp = new clsOPBreackDownMaster();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Upload_Operation_BreackdownFile");
            var filteredList = objReq.oList.Where(x => x.SubSection == "Post Assmbly" || x.SubSection == "POST ASSEMBLY").ToList();            
            try
            {

                if(String.IsNullOrWhiteSpace(objReq.StyleCode))
                {
                    objResp.vErrorCode = 400;
                    objResp.vErrorMsg = "StyleCode is empty";
                    return objResp;
                }
                else if (String.IsNullOrWhiteSpace(objReq.ProcessName))
                {
                    objResp.vErrorCode = 400;
                    objResp.vErrorMsg = "ProcessName is empty";
                    return objResp;
                }
                else if (objReq.oList == null || objReq.oList.Count == 0)
                {
                    objResp.vErrorCode = 400;
                    objResp.vErrorMsg = "Please select List";
                    return objResp;
                }
                if (!String.IsNullOrWhiteSpace(objReq.vErrorMsg))
                {
                    objResp.vErrorCode = objReq.vErrorCode;
                    objResp.vErrorMsg = objReq.vErrorMsg;
                    return objResp;
                }
                if (filteredList.Count == 0)
                {
                    objResp.vErrorCode = 400;
                    objResp.vErrorMsg = "Please Add Post Assmbly in SubSection";
                    return objResp;
                }
                else
                {
                    if (Con.State == ConnectionState.Broken)
                    { Con.Close(); }
                    if (Con.State == ConnectionState.Closed)
                    { Con.Open(); }

                    if (objReq.ID == 0 || objReq.ID == null)
                    {
                        Fn_Get_MXID("OperationBreackDownMaster", "ID");
                        objReq.ID = mxID;
                    }

                    SqlCommand cmd = new SqlCommand("USP_OPEARTION_BREACK_DOWN", Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", objReq.ID);
                    cmd.Parameters.AddWithValue("@StyleCode", objReq.StyleCode);
                    cmd.Parameters.AddWithValue("@ProcessName", objReq.ProcessName);
                    cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                    cmd.Parameters.AddWithValue("@QueryType", "InsertOperationMaster");
                    int i = 0;
                    i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        objResp.vErrorCode = 200;
                        objResp.vErrorMsg = "Success";

                        if (objReq.oList != null)
                        {
                            foreach (clsOPBreackDownDetail _oList in objReq.oList)
                            {
                                if (_oList.DetailID == 0 || _oList.DetailID == null)
                                {
                                    Fn_Get_MXID("OperationBreackDownDetail", "DetailID");
                                    _oList.DetailID = mxID;

                                    //if (Con.State == ConnectionState.Broken)
                                    //{ Con.Close(); }
                                    //if (Con.State == ConnectionState.Closed)
                                    //{ Con.Open(); }
                                }

                                if (_oList.SeqNo == 0 || _oList.OpNo == 0)
                                {
                                }
                                else if (String.IsNullOrWhiteSpace(_oList.Descriptions) || String.IsNullOrWhiteSpace(_oList.Machine)
                                    || String.IsNullOrWhiteSpace(_oList.SubSection)
                                    || String.IsNullOrWhiteSpace(_oList.Product))
                                {

                                }
                                else
                                {
                                    SqlCommand cm1 = new SqlCommand("USP_OPEARTION_BREACK_DOWN", Con);
                                    cm1.CommandType = CommandType.StoredProcedure;
                                    cm1.Parameters.AddWithValue("@ID", objReq.ID);
                                    cm1.Parameters.AddWithValue("@DetailID", _oList.DetailID);
                                    cm1.Parameters.AddWithValue("@SeqNo", _oList.SeqNo);
                                    cm1.Parameters.AddWithValue("@OpNo", _oList.OpNo);
                                    cm1.Parameters.AddWithValue("@Descriptions", _oList.Descriptions);
                                    cm1.Parameters.AddWithValue("@Machine", _oList.Machine);
                                    cm1.Parameters.AddWithValue("@SubSection", _oList.SubSection);
                                    cm1.Parameters.AddWithValue("@StdMin", _oList.StdMin);
                                    cm1.Parameters.AddWithValue("@Rate", _oList.Rate);
                                    cm1.Parameters.AddWithValue("@Product", _oList.Product);
                                    cm1.Parameters.AddWithValue("@Skill", _oList.Skill);
                                    cm1.Parameters.AddWithValue("@Grade", _oList.Grade);
                                    cm1.Parameters.AddWithValue("@Folder", _oList.Folder);
                                    cm1.Parameters.AddWithValue("@Seamlength", _oList.Seamlength);
                                    cm1.Parameters.AddWithValue("@IsDirect", _oList.IsDirect);
                                    cm1.Parameters.AddWithValue("@ProgressPoint", _oList.ProgressPoint);
                                    cm1.Parameters.AddWithValue("@IsDispatch", _oList.IsDispatch);
                                    cm1.Parameters.AddWithValue("@IsDS", _oList.IsDS);
                                    cm1.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                                    cm1.Parameters.AddWithValue("@QueryType", "InsertOperationDetail");
                                    int j = cm1.ExecuteNonQuery();
                                    if (j > 0)
                                    {
                                        objResp.vErrorCode = 200;
                                        objResp.vErrorMsg = "Success";
                                    }
                                    else
                                    {
                                        objResp.vErrorCode = 400;
                                        objResp.vErrorMsg = "Operation detail inserting failed ";
                                        return objResp;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        objResp.vErrorCode = 400;
                        objResp.vErrorMsg = "Operation mastar inserting failed";
                    }
                }
            }
            catch (Exception exp)
            {
                objResp.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Upload_Operation_BreackdownFile", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Upload_Operation_BreackdownFile");
            return objResp;
        }

        public List<clsOPBreackDownDetail> Fn_Get_Operation_BreackdownFile(clsOPBreackDownMaster objReq)
        {
            var objResp = new List<clsOPBreackDownDetail>();
            var obj = new clsOPBreackDownDetail();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Get_Operation_BreackdownFile");
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT OD.MID, OD.DetailID, OD.SeqNo, OD.OpNo, OD.Descriptions, OD.Machine, OD.SubSection,";
                strSql = strSql + " OD.StdMin, OD.Rate, OD.Product, OD.Skill, OD.Grade, OD.Folder, OD.Seamlength, OD.IsDirect,";
                strSql = strSql + " OD.ProgressPoint, OD.IsDispatch, OD.DependOPNO, OD.IsDS, OD.CreatedBy, OM.StyleCode, OM.ProcessName,";
                strSql = strSql + " FORMAT(OD.CreatedOn, 'dd-MMM-yyy') AS CreatedOn FROM OperationBreackDownDetail OD";
                strSql = strSql + " INNER JOIN OperationBreackDownMaster OM ON OD.MID = OM.ID WHERE 1=1";
                if (!String.IsNullOrWhiteSpace(objReq.ProcessName))
                {
                    strSql = strSql + " AND OM.ProcessName = @ProcessName";
                }
                if (!String.IsNullOrWhiteSpace(objReq.StyleCode))
                {
                    strSql = strSql + " AND OM.StyleCode = @StyleCode";
                }
                if (objReq.ID != 0 && objReq.ID != null)
                {
                    strSql = strSql + " AND OM.ID = @ID ";
                }
                if (objReq.OpNo != 0 && objReq.OpNo != null)
                {
                    strSql = strSql + " AND OD.OpNo = @OpNo ";
                }
                strSql = strSql + " ORDER BY OD.SeqNo, OD.SubSection ASC ";

                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;
                if (!String.IsNullOrWhiteSpace(objReq.ProcessName))
                {
                    cmd.Parameters.AddWithValue("@ProcessName", objReq.ProcessName);
                }
                if (!String.IsNullOrWhiteSpace(objReq.StyleCode))
                {
                    cmd.Parameters.AddWithValue("@StyleCode", objReq.StyleCode);
                }
                if (objReq.ID != 0 && objReq.ID != null)
                {
                    cmd.Parameters.AddWithValue("@ID", objReq.ID);
                }
                if (objReq.OpNo != 0 && objReq.OpNo != null)
                {
                    cmd.Parameters.AddWithValue("@OpNo", objReq.OpNo);
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsOPBreackDownDetail();
                        obj.MID = Convert.ToInt64(ds.Tables[0].Rows[i]["MID"]);
                        obj.DetailID = Convert.ToInt64(ds.Tables[0].Rows[i]["DetailID"]);
                        obj.SeqNo = Convert.ToInt32(ds.Tables[0].Rows[i]["SeqNo"]);
                        obj.OpNo = Convert.ToInt32(ds.Tables[0].Rows[i]["OpNo"]);
                        obj.Descriptions = Convert.ToString(ds.Tables[0].Rows[i]["Descriptions"]);
                        obj.Machine = Convert.ToString(ds.Tables[0].Rows[i]["Machine"]);
                        obj.SubSection = Convert.ToString(ds.Tables[0].Rows[i]["SubSection"]);
                        obj.StdMin = Convert.ToDecimal(ds.Tables[0].Rows[i]["StdMin"]);
                        obj.Rate = Convert.ToDecimal(ds.Tables[0].Rows[i]["Rate"]);
                        obj.Product = Convert.ToString(ds.Tables[0].Rows[i]["Product"]);
                        obj.Skill = Convert.ToString(ds.Tables[0].Rows[i]["Skill"]);
                        obj.Grade = Convert.ToString(ds.Tables[0].Rows[i]["Grade"]);
                        obj.Folder = Convert.ToString(ds.Tables[0].Rows[i]["Folder"]);
                        obj.Seamlength = Convert.ToString(ds.Tables[0].Rows[i]["Seamlength"]);
                        obj.IsDirect = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsDirect"]);
                        obj.ProgressPoint = Convert.ToString(ds.Tables[0].Rows[i]["ProgressPoint"]);
                        obj.IsDispatch = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsDispatch"]);
                        obj.DependOPNO = Convert.ToString(ds.Tables[0].Rows[i]["DependOPNO"]);
                        obj.IsDS = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsDS"]);
                        obj.CreatedOn = Convert.ToString(ds.Tables[0].Rows[i]["CreatedOn"]);
                        obj.CreatedBy = Convert.ToInt32(ds.Tables[0].Rows[i]["CreatedBy"]);

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
                Logger.WriteLog("Function Name : Fn_Get_Operation_BreackdownFile", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Get_Operation_BreackdownFile");
            return objResp;
        }

        public clsOPBreackDownMaster Fn_Check_Exist_style_In_Master(clsOPBreackDownMaster objReq)
        {
            var objResp = new clsOPBreackDownMaster();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Check_Exist_style_In_Master");
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = " Select ID, StyleCode, ProcessName, CreatedBy,";
                strSql = strSql + " FORMAT(CreatedOn, 'dd-MMM-yyy') AS CreatedOn from OperationBreackDownMaster WHERE 1=1";
                if (!String.IsNullOrWhiteSpace(objReq.ProcessName))
                {
                    strSql = strSql + " AND ProcessName = @ProcessName";
                }
                if (!String.IsNullOrWhiteSpace(objReq.StyleCode))
                {
                    strSql = strSql + " AND StyleCode = @StyleCode";
                }
                if (objReq.ID != 0 && objReq.ID != null)
                {
                    strSql = strSql + " AND ID = @ID ";
                }

                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;
                if (!String.IsNullOrWhiteSpace(objReq.ProcessName))
                {
                    cmd.Parameters.AddWithValue("@ProcessName", objReq.ProcessName);
                }
                if (!String.IsNullOrWhiteSpace(objReq.StyleCode))
                {
                    cmd.Parameters.AddWithValue("@StyleCode", objReq.StyleCode);
                }
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
                    objResp.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["ID"]);
                    objResp.StyleCode = Convert.ToString(ds.Tables[0].Rows[0]["StyleCode"]);
                    objResp.ProcessName = Convert.ToString(ds.Tables[0].Rows[i]["ProcessName"]);
                    objResp.CreatedBy = Convert.ToInt32(ds.Tables[0].Rows[0]["CreatedBy"]);
                    objResp.CreatedOn = Convert.ToString(ds.Tables[0].Rows[0]["CreatedOn"]);
                    objResp.vErrorMsg = "Success";
                    objResp.vErrorCode = 200;
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
                Logger.WriteLog("Function Name : Fn_Check_Exist_style_In_Master", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Check_Exist_style_In_Master");
            return objResp;
        }

        #region Start Fn_Get_OB_BY_Product 30-MAR-2026

        public List<clsOPBreackDownDetail> Fn_Get_OB_BY_Product(clsOPBreackDownDetail objReq)
        {
            var objResp = new List<clsOPBreackDownDetail>();
            var obj = new clsOPBreackDownDetail();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Get_OB_BY_Product");
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "Select Distinct OperationNo AS OpNo, OperationName AS Descriptions, Machine,";
                strSql = strSql + " SubSection, StdMin, Rate, SubProduct AS Product from OBMainMasterNew WHERE 1=1 AND SubSection <> 'Null' ";
                if (!String.IsNullOrWhiteSpace(objReq.Product))
                {
                    strSql = strSql + " AND SubProduct = @Product";
                }
                
                strSql = strSql + " ORDER BY SubSection, OpNo ASC ";

                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;
                if (!String.IsNullOrWhiteSpace(objReq.Product))
                {
                    cmd.Parameters.AddWithValue("@Product", objReq.Product);
                }
                

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsOPBreackDownDetail();
                        obj.OpNo = Convert.ToInt32(ds.Tables[0].Rows[i]["OpNo"]);
                        obj.Descriptions = Convert.ToString(ds.Tables[0].Rows[i]["Descriptions"]);
                        obj.Machine = Convert.ToString(ds.Tables[0].Rows[i]["Machine"]);
                        obj.SubSection = Convert.ToString(ds.Tables[0].Rows[i]["SubSection"]);
                        string StdMin = Convert.ToString(ds.Tables[0].Rows[i]["StdMin"]);
                        if (StdMin != "")
                        {
                            obj.StdMin = Convert.ToDecimal(ds.Tables[0].Rows[i]["StdMin"]);
                        }
                        string Rate = Convert.ToString(ds.Tables[0].Rows[i]["Rate"]);
                        if (Rate != "")
                        {
                            obj.Rate = Convert.ToDecimal(ds.Tables[0].Rows[i]["Rate"]);
                        }
                        obj.Product = Convert.ToString(ds.Tables[0].Rows[i]["Product"]);

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
                Logger.WriteLog("Function Name : Fn_Get_OB_BY_Product", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Get_OB_BY_Product");
            return objResp;
        }

        #endregion End Fn_Get_OB_BY_Product 30-MAR-2026

        #region Start Fn_Update_Rate_In_OB_Master 02-APR-2026

        public clsOPBreackDownDetail Fn_Update_Rate_In_OB_Master(clsOPBreackDownDetail objReq)
        {
            var objResp = new clsOPBreackDownDetail();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Update_Rate_In_OB_Master");
            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_OPEARTION_BREACK_DOWN", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OpNo", objReq.OpNo);
                cmd.Parameters.AddWithValue("@Rate", objReq.Rate);
                cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                cmd.Parameters.AddWithValue("@QueryType", "Update_OB_Master_Rate");
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
                Logger.WriteLog("Function Name : Fn_Update_Rate_In_OB_Master", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Update_Rate_In_OB_Master");
            return objResp;
        }

        #endregion End Fn_Update_Rate_In_OB_Master 02-APR-2026

        #region Start Fn_Add_New_OpNo 03-APR-2026

        public clsOPBreackDownDetail Fn_Add_New_OpNo (clsOPBreackDownDetail objReq)
        {
            var objResp = new clsOPBreackDownDetail();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Add_New_OpNo");
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_OPEARTION_BREACK_DOWN", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Product", objReq.Product);
                cmd.Parameters.AddWithValue("@Descriptions", objReq.Descriptions);
                cmd.Parameters.AddWithValue("@OpNo", objReq.OpNo);
                cmd.Parameters.AddWithValue("@Machine", objReq.Machine);
                cmd.Parameters.AddWithValue("@SubSection", objReq.SubSection);
                cmd.Parameters.AddWithValue("@StdMin", objReq.StdMin);
                cmd.Parameters.AddWithValue("@Rate", objReq.Rate);
                cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                cmd.Parameters.AddWithValue("@QueryType", "Add_NEW_OPNo_In_OB_Master");
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
                Logger.WriteLog("Function Name : Fn_Add_New_OpNo", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Add_New_OpNo");
            return objResp;
        }

        #endregion End Fn_Add_New_OpNo 03-APR-2026

        #region Start Fn_Get_Order_Chart 13-APR-2026

        public List<clsOrderMaster> Fn_Get_Order_Chart(clsOrderMaster objReq)
        {
            var objResp = new List<clsOrderMaster>();
            var obj = new clsOrderMaster();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Get_Order_Chart");
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "Select SUM(BC.Qty) AS Qty, Format(BD.AppStartTime,'MMM') AS SMonth, BC.OrderNo from BundleCompileDetail BD";
                strSql = strSql + " INNER JOIN BundleCompile BC ON BC.BundleID = BD.BundleID where 1=1 AND BD.BundleIDStatus = 'Finished'";
               
                if (!String.IsNullOrWhiteSpace(objReq.OrderNo))
                {
                    strSql = strSql + " AND OrderNo = @OrderNo";
                }

                strSql = strSql + " GROUP BY Format(BD.AppStartTime,'MMM'), BC.OrderNo ";

                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;
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
                        obj = new clsOrderMaster();
                        obj.CreatedOn = Convert.ToString(ds.Tables[0].Rows[i]["SMonth"]);
                        obj.Qty = Convert.ToInt32(ds.Tables[0].Rows[i]["Qty"]);
                        obj.OrderNo = Convert.ToString(ds.Tables[0].Rows[i]["OrderNo"]);
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
                Logger.WriteLog("Function Name : Fn_Get_Order_Chart", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Get_Order_Chart");
            return objResp;
        }

        #endregion End Fn_Get_Order_Chart 13-APR-2026

        #region Start Fn_Filter_OP_Detail 10-JUN-2026
        public List<clsOPBreackDownDetail> Fn_Filter_OP_Detail(clsOPBreackDownDetail objReq)
        {
            var objResp = new List<clsOPBreackDownDetail>();
            var obj = new clsOPBreackDownDetail();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Filter_OP_Detail");
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT DISTINCT OpNo,Descriptions,Machine,SubSection,StdMin,Rate,Product,Skill,Grade,Folder,Seamlength,";
                strSql = strSql + " IsDirect,ProgressPoint,IsDispatch,DependOPNO,IsDS FROM OperationBreackDownDetail WHERE 1=1";
                if (objReq.OpNo != 0 && objReq.OpNo != null)
                {
                    strSql = strSql + " AND OpNo LIKE @OpNo ";
                }
                if (!String.IsNullOrWhiteSpace(objReq.Descriptions))
                {
                    strSql = strSql + " AND Descriptions LIKE @Descriptions";
                }
                if (!String.IsNullOrWhiteSpace(objReq.Machine))
                {
                    strSql = strSql + " AND Machine LIKE @Machine";
                }
                if (!String.IsNullOrWhiteSpace(objReq.SubSection))
                {
                    strSql = strSql + " AND SubSection LIKE @SubSection";
                }
                if (!String.IsNullOrWhiteSpace(objReq.Product))
                {
                    strSql = strSql + " AND Product LIKE @Product";
                }
                if (!String.IsNullOrWhiteSpace(objReq.Skill))
                {
                    strSql = strSql + " AND Skill LIKE @Skill";
                }
                if (!String.IsNullOrWhiteSpace(objReq.Grade))
                {
                    strSql = strSql + " AND Grade LIKE @Grade";
                }

                strSql = strSql + " ORDER BY OpNo ASC ";

                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;
              
                if (objReq.OpNo != 0 && objReq.OpNo != null)
                {
                    cmd.Parameters.AddWithValue("@OpNo", "%" + objReq.OpNo + "%");
                }
                if (!String.IsNullOrWhiteSpace(objReq.Descriptions))
                {
                    cmd.Parameters.AddWithValue("@Descriptions", "%" + objReq.Descriptions + "%");
                }
                if (!String.IsNullOrWhiteSpace(objReq.Machine))
                {
                    cmd.Parameters.AddWithValue("@Machine", "%" + objReq.Machine+ "%");
                }
                if (!String.IsNullOrWhiteSpace(objReq.SubSection))
                {
                    cmd.Parameters.AddWithValue("@SubSection", "%" + objReq.SubSection + "%");
                }
                if (!String.IsNullOrWhiteSpace(objReq.Product))
                {
                    cmd.Parameters.AddWithValue("@Product", "%" + objReq.Product + "%");
                }
                if (!String.IsNullOrWhiteSpace(objReq.Skill))
                {
                    cmd.Parameters.AddWithValue("@Skill", "%" + objReq.Skill + "%");
                }
                if (!String.IsNullOrWhiteSpace(objReq.Grade))
                {
                    cmd.Parameters.AddWithValue("@Grade", "%" + objReq.Grade + "%");
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsOPBreackDownDetail();
                        //obj.MID = Convert.ToInt32(ds.Tables[0].Rows[i]["MID"]);
                        obj.OpNo = Convert.ToInt32(ds.Tables[0].Rows[i]["OpNo"]);
                        obj.Descriptions = Convert.ToString(ds.Tables[0].Rows[i]["Descriptions"]);
                        obj.Machine = Convert.ToString(ds.Tables[0].Rows[i]["Machine"]);
                        obj.SubSection = Convert.ToString(ds.Tables[0].Rows[i]["SubSection"]);
                        obj.StdMin = Convert.ToDecimal(ds.Tables[0].Rows[i]["StdMin"]);
                        obj.Rate = Convert.ToDecimal(ds.Tables[0].Rows[i]["Rate"]);
                        obj.Product = Convert.ToString(ds.Tables[0].Rows[i]["Product"]);
                        obj.Skill = Convert.ToString(ds.Tables[0].Rows[i]["Skill"]);
                        obj.Grade = Convert.ToString(ds.Tables[0].Rows[i]["Grade"]);
                        obj.Folder = Convert.ToString(ds.Tables[0].Rows[i]["Folder"]);
                        obj.Seamlength = Convert.ToString(ds.Tables[0].Rows[i]["Seamlength"]);
                        obj.IsDirect = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsDirect"]);
                        obj.ProgressPoint = Convert.ToString(ds.Tables[0].Rows[i]["ProgressPoint"]);
                        obj.IsDispatch = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsDispatch"]);
                        obj.DependOPNO = Convert.ToString(ds.Tables[0].Rows[i]["DependOPNO"]);
                        obj.IsDS = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsDS"]);

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
                Logger.WriteLog("Function Name : Fn_Filter_OP_Detail", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Filter_OP_Detail");
            return objResp;
        }

        #endregion End Fn_Filter_OP_Detail 10-JUN-2026

        #region Start Fn_Append_New_OpNo 11-JUN-2026
        public clsOPBreackDownDetail Fn_Append_New_OpNo(clsOPBreackDownDetail objReq)
        {
            var objResp = new clsOPBreackDownDetail();
            var objCheckOPList = new clsOPBreackDownMaster();
            objCheckOPList.OpNo = objReq.OpNo;
            objCheckOPList.StyleCode = objReq.Folder;  // this is stylecode here
            var checkExistOP = new List<clsOPBreackDownDetail>();
            checkExistOP = Fn_Get_Operation_BreackdownFile(objCheckOPList);
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Append_New_OpNo");
            try
            {
                if (objReq.DetailID == 0 || objReq.DetailID == null)
                {
                    Fn_Get_MXID("OperationBreackDownDetail", "DetailID");
                    objReq.DetailID = mxID;
                }

                if (objReq.OpNo == 0 && objReq.OpNo == null)
                {
                    objResp.vErrorCode = 400;
                    objResp.vErrorMsg = "Please Enter OpNo";
                }
                else if (String.IsNullOrWhiteSpace(objReq.Folder))
                {
                    objResp.vErrorCode = 400;
                    objResp.vErrorMsg = "Please Enter StyleCode";
                }
                else if (checkExistOP[0].OpNo == objReq.OpNo)
                {
                    objResp.vErrorCode = 400;
                    objResp.vErrorMsg = "OpNo already exist";
                }
                else
                {
                   var objGet = new clsOPBreackDownDetail();
                    objGet.OpNo = objReq.OpNo;
                    checkExistOP = new List<clsOPBreackDownDetail>();
                    checkExistOP = Fn_Filter_OP_Detail(objGet);

                    if (Con.State == ConnectionState.Broken)
                    { Con.Close(); }
                    if (Con.State == ConnectionState.Closed)
                    { Con.Open(); }

                    SqlCommand cmd = new SqlCommand("USP_OPEARTION_BREACK_DOWN", Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DetailID", objReq.DetailID);
                    cmd.Parameters.AddWithValue("@OpNo", objReq.OpNo);
                    cmd.Parameters.AddWithValue("@Descriptions", checkExistOP[0].Descriptions);
                    cmd.Parameters.AddWithValue("@Machine", checkExistOP[0].Machine);
                    cmd.Parameters.AddWithValue("@SubSection", checkExistOP[0].SubSection);
                    cmd.Parameters.AddWithValue("@StdMin", checkExistOP[0].StdMin);
                    cmd.Parameters.AddWithValue("@Rate", checkExistOP[0].Rate);
                    cmd.Parameters.AddWithValue("@Product", checkExistOP[0].Product);
                    cmd.Parameters.AddWithValue("@Skill", checkExistOP[0].Skill);
                    cmd.Parameters.AddWithValue("@Grade", checkExistOP[0].Grade);
                    cmd.Parameters.AddWithValue("@Folder", checkExistOP[0].Folder);
                    cmd.Parameters.AddWithValue("@Seamlength", objReq.Seamlength);
                    cmd.Parameters.AddWithValue("@IsDirect", objReq.IsDirect);
                    cmd.Parameters.AddWithValue("@ProgressPoint", objReq.ProgressPoint);
                    cmd.Parameters.AddWithValue("@IsDispatch", objReq.IsDispatch);
                    cmd.Parameters.AddWithValue("@DependOPNO", objReq.DependOPNO);
                    cmd.Parameters.AddWithValue("@IsDS", objReq.IsDS);
                    cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                    cmd.Parameters.AddWithValue("@CreatedOn", objReq.Folder); // StyleCode
                    cmd.Parameters.AddWithValue("@QueryType", "Append_New_Opno");
                    int j = cmd.ExecuteNonQuery();
                    if (j > 0)
                    {
                        objResp.vErrorCode = 200;
                        objResp.vErrorMsg = "Success";
                    }
                    else
                    {
                        objResp.vErrorCode = 400;
                        objResp.vErrorMsg = "Operation inserting failed ";
                        return objResp;
                    }
                }
            }
            catch (Exception exp)
            {               
                Logger.WriteLog("Function Name : Fn_Append_New_OpNo", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
                objResp.vErrorCode = 500;
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Append_New_OpNo");
            return objResp;
        }

        #endregion End Fn_Append_New_OpNo 11-JUN-2026

        #region Start Fn_Delete_OpNo_IN_OBD 15-JUN-2026

        public clsOPBreackDownDetail Fn_Delete_OpNo_IN_OBD(clsOPBreackDownDetail objReq)
        {
            var objResp = new clsOPBreackDownDetail();
            var objCheckOPList = new clsOPBreackDownMaster();
            objCheckOPList.OpNo = objReq.OpNo;
            objCheckOPList.StyleCode = objReq.Folder;  // this is stylecode here
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Delete_OpNo_IN_OBD");
            try
            {
                

                if (objReq.OpNo == 0 && objReq.OpNo == null)
                {
                    objResp.vErrorCode = 400;
                    objResp.vErrorMsg = "Please Enter OpNo";
                }
                else if (String.IsNullOrWhiteSpace(objReq.CreatedOn))
                {
                    objResp.vErrorCode = 400;
                    objResp.vErrorMsg = "Please Enter StyleCode";
                }
                else
                {                    

                    if (Con.State == ConnectionState.Broken)
                    { Con.Close(); }
                    if (Con.State == ConnectionState.Closed)
                    { Con.Open(); }

                    SqlCommand cmd = new SqlCommand("USP_OPEARTION_BREACK_DOWN", Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DetailID", objReq.DetailID);
                    cmd.Parameters.AddWithValue("@ID", objReq.MID);
                    cmd.Parameters.AddWithValue("@OpNo", objReq.OpNo);
                    cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                    cmd.Parameters.AddWithValue("@CreatedOn", objReq.CreatedOn); // StyleCode
                    cmd.Parameters.AddWithValue("@QueryType", "DELTE_Opno_IN_OBD");
                    int j = cmd.ExecuteNonQuery();
                    if (j > 0)
                    {
                        objResp.vErrorCode = 200;
                        objResp.vErrorMsg = "Success";
                    }
                    else
                    {
                        objResp.vErrorCode = 400;
                        objResp.vErrorMsg = "OpNo deleting failed ";
                        return objResp;
                    }
                }
            }
            catch (Exception exp)
            {
                Logger.WriteLog("Function Name : Fn_Delete_OpNo_IN_OBD", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
                objResp.vErrorCode = 500;
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Delete_OpNo_IN_OBD");
            return objResp;
        }

        #endregion END Fn_Delete_OpNo_IN_OBD 15-JUN-2026

        #region Start Fn_Add_New_OpNo_IN_OBD 15-JUN-2026

        public clsOPBreackDownDetail Fn_Add_New_OpNo_IN_OBD(clsOPBreackDownDetail objReq)
        {
            var objResp = new clsOPBreackDownDetail();
            var objCheckOPList = new clsOPBreackDownMaster();
            objCheckOPList.OpNo = objReq.OpNo;
            objCheckOPList.StyleCode = objReq.CreatedOn;  // this is stylecode here
            var checkExistOP = new List<clsOPBreackDownDetail>();
            checkExistOP = Fn_Get_Operation_BreackdownFile(objCheckOPList);
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Add_New_OpNo_IN_OBD");
            try
            {
                if (objReq.DetailID == 0 || objReq.DetailID == null)
                {
                    Fn_Get_MXID("OperationBreackDownDetail", "DetailID");
                    objReq.DetailID = mxID;
                }

                if (objReq.OpNo == 0 && objReq.OpNo == null)
                {
                    objResp.vErrorCode = 400;
                    objResp.vErrorMsg = "Please Enter OpNo";
                }
                else if (String.IsNullOrWhiteSpace(objReq.Descriptions))
                {
                    objResp.vErrorCode = 400;
                    objResp.vErrorMsg = "Please Enter Descriptions";
                }
                else if (objReq.SeqNo == 0 && objReq.SeqNo == null)
                {
                    objResp.vErrorCode = 400;
                    objResp.vErrorMsg = "Please Enter SeqNo";
                }
                else if (String.IsNullOrWhiteSpace(objReq.Machine))
                {
                    objResp.vErrorCode = 400;
                    objResp.vErrorMsg = "Please Enter Machine";
                }
                else if (String.IsNullOrWhiteSpace(objReq.SubSection))
                {
                    objResp.vErrorCode = 400;
                    objResp.vErrorMsg = "Please Enter SubSection";
                }
                else if (objReq.Rate == 0 && objReq.Rate == null)
                {
                    objResp.vErrorCode = 400;
                    objResp.vErrorMsg = "Please Enter Rate";
                }
                else if (objReq.StdMin == 0 && objReq.StdMin == null)
                {
                    objResp.vErrorCode = 400;
                    objResp.vErrorMsg = "Please Enter StdMin";
                }
                else if (String.IsNullOrWhiteSpace(objReq.Product))
                {
                    objResp.vErrorCode = 400;
                    objResp.vErrorMsg = "Please Enter Product";
                }
                else if (String.IsNullOrWhiteSpace(objReq.Skill))
                {
                    objResp.vErrorCode = 400;
                    objResp.vErrorMsg = "Please Enter Skill";
                }
                else if (String.IsNullOrWhiteSpace(objReq.Grade))
                {
                    objResp.vErrorCode = 400;
                    objResp.vErrorMsg = "Please Enter Grade";
                }
                else if (String.IsNullOrWhiteSpace(objReq.CreatedOn))
                {
                    objResp.vErrorCode = 400;
                    objResp.vErrorMsg = "Please Enter StyleCode";
                }
                else if (checkExistOP[0].OpNo == objReq.OpNo)
                {
                    objResp.vErrorCode = 400;
                    objResp.vErrorMsg = "OpNo already exist";
                }
                else
                {                    

                    if (Con.State == ConnectionState.Broken)
                    { Con.Close(); }
                    if (Con.State == ConnectionState.Closed)
                    { Con.Open(); }

                    SqlCommand cmd = new SqlCommand("USP_OPEARTION_BREACK_DOWN", Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DetailID", objReq.DetailID);
                    cmd.Parameters.AddWithValue("@OpNo", objReq.OpNo);
                    cmd.Parameters.AddWithValue("@SeqNo", objReq.SeqNo);
                    cmd.Parameters.AddWithValue("@Descriptions", objReq.Descriptions);
                    cmd.Parameters.AddWithValue("@Machine", objReq.Machine);
                    cmd.Parameters.AddWithValue("@SubSection", objReq.SubSection);
                    cmd.Parameters.AddWithValue("@StdMin", objReq.StdMin);
                    cmd.Parameters.AddWithValue("@Rate", objReq.Rate);
                    cmd.Parameters.AddWithValue("@Product", objReq.Product);
                    cmd.Parameters.AddWithValue("@Skill", objReq.Skill);
                    cmd.Parameters.AddWithValue("@Grade", objReq.Grade);
                    cmd.Parameters.AddWithValue("@Folder", objReq.Folder);
                    cmd.Parameters.AddWithValue("@Seamlength", objReq.Seamlength);
                    cmd.Parameters.AddWithValue("@IsDirect", objReq.IsDirect);
                    cmd.Parameters.AddWithValue("@ProgressPoint", objReq.ProgressPoint);
                    cmd.Parameters.AddWithValue("@IsDispatch", objReq.IsDispatch);
                    cmd.Parameters.AddWithValue("@DependOPNO", objReq.DependOPNO);
                    cmd.Parameters.AddWithValue("@IsDS", objReq.IsDS);
                    cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                    cmd.Parameters.AddWithValue("@CreatedOn", objReq.CreatedOn); // StyleCode
                    cmd.Parameters.AddWithValue("@QueryType", "Add_New_OpNo_IN_OBD");
                    int j = cmd.ExecuteNonQuery();
                    if (j > 0)
                    {
                        objResp.vErrorCode = 200;
                        objResp.vErrorMsg = "Success";
                    }
                    else
                    {
                        objResp.vErrorCode = 400;
                        objResp.vErrorMsg = "Operation inserting failed ";
                        return objResp;
                    }
                }
            }
            catch (Exception exp)
            {
                Logger.WriteLog("Function Name : Fn_Add_New_OpNo_IN_OBD", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
                objResp.vErrorCode = 500;
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Add_New_OpNo_IN_OBD");
            return objResp;
        }

        #endregion END Fn_Add_New_OpNo_IN_OBD 15-JUN-2026

        #region Start Fn_Add_FabricOrder 18-JUN-2026

        public clsFabricOrder Fn_Add_FabricOrder(clsFabricOrder objReq)
        {
            var objResp = new clsFabricOrder();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Add_FabricOrder");
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                Int64 mxID = Fn_Get_MXID("Fabric_Order", "FabricOrderId");
                objReq.FabricOrderId = Convert.ToInt32(mxID);

                SqlCommand cmd = new SqlCommand("USP_FABRIC_ORDER", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FabricOrderId", objReq.FabricOrderId);
                cmd.Parameters.AddWithValue("@StyleCode", objReq.StyleCode);
                cmd.Parameters.AddWithValue("@ItemCode", objReq.ItemCode);
                cmd.Parameters.AddWithValue("@Descriptions", objReq.Descriptions);
                cmd.Parameters.AddWithValue("@Contents", objReq.Contents);
                cmd.Parameters.AddWithValue("@Mill", objReq.Mill);
                cmd.Parameters.AddWithValue("@FabricColor", objReq.FabricColor);
                cmd.Parameters.AddWithValue("@FabricCC", objReq.FabricCC);
                cmd.Parameters.AddWithValue("@Width", objReq.Width);
                cmd.Parameters.AddWithValue("@WidthTolerance", objReq.WidthTolerance);
                cmd.Parameters.AddWithValue("@OrderRollLength", objReq.OrderRollLength);
                cmd.Parameters.AddWithValue("@OrderRollLengthTolerance", objReq.OrderRollLengthTolerance);
                cmd.Parameters.AddWithValue("@GSM", objReq.GSM);
                cmd.Parameters.AddWithValue("@GSMTolerance", objReq.GSMTolerance);
                cmd.Parameters.AddWithValue("@OrderShrinkageWarpLength", objReq.OrderShrinkageWarpLength);
                cmd.Parameters.AddWithValue("@OrderShrinkageWaftWidth", objReq.OrderShrinkageWaftWidth);
                cmd.Parameters.AddWithValue("@TotalQuantity", objReq.TotalQuantity);
                cmd.Parameters.AddWithValue("@Unit", objReq.Unit);
                cmd.Parameters.AddWithValue("@MarkerType", objReq.MarkerType);
                cmd.Parameters.AddWithValue("@Price", objReq.Price);
                cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                cmd.Parameters.AddWithValue("@QueryType", objReq.QueryType);
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
                Logger.WriteLog("Function Name : Fn_Add_FabricOrder", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Add_FabricOrder");
            return objResp;
        }

        #endregion End Fn_Add_FabricOrder 18-JUN-2026

        #region Start Fn_Get_FabricOrder 18-JUN-2026

        public List<clsFabricOrder> Fn_Get_FabricOrder(clsFabricOrder objReq)
        {
            var objResp = new List<clsFabricOrder>();
            var obj = new clsFabricOrder();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Get_FabricOrder");
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT FabricOrderId, StyleCode, ItemCode, Descriptions, Contents, Mill, FabricColor, FabricCC,";
                strSql = strSql + " Width, WidthTolerance, OrderRollLength, OrderRollLengthTolerance,";
                strSql = strSql + " GSM, GSMTolerance, OrderShrinkageWarpLength, OrderShrinkageWaftWidth, TotalQuantity,";
                strSql = strSql + " Unit, MarkerType, Price, CreatedBy, CreatedOn FROM Fabric_Order WHERE 1=1";
                
                if (!String.IsNullOrWhiteSpace(objReq.StyleCode) && !String.IsNullOrWhiteSpace(objReq.FabricColor))
                {
                    strSql = strSql + " AND StyleCode = @StyleCode";
                }
                if (!String.IsNullOrWhiteSpace(objReq.ItemCode))
                {
                    strSql = strSql + " AND ItemCode = @ItemCode";
                }
                if (!String.IsNullOrWhiteSpace(objReq.Mill))
                {
                    strSql = strSql + " AND Mill = @Mill";
                }
                if (!String.IsNullOrWhiteSpace(objReq.FabricColor) && !String.IsNullOrWhiteSpace(objReq.StyleCode))
                {
                    strSql = strSql + " AND FabricColor = @FabricColor";
                }
                if (!String.IsNullOrWhiteSpace(objReq.MarkerType))
                {
                    strSql = strSql + " AND MarkerType = @MarkerType";
                }
                
                strSql = strSql + " ORDER BY StyleCode, ItemCode ASC ";

                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;
                if (!String.IsNullOrWhiteSpace(objReq.StyleCode) && !String.IsNullOrWhiteSpace(objReq.FabricColor))
                {
                    cmd.Parameters.AddWithValue("@StyleCode", objReq.StyleCode);
                }
                if (!String.IsNullOrWhiteSpace(objReq.ItemCode))
                {
                    cmd.Parameters.AddWithValue("@ItemCode", objReq.ItemCode);
                }
                if (!String.IsNullOrWhiteSpace(objReq.Mill))
                {
                    cmd.Parameters.AddWithValue("@Mill", objReq.Mill);
                }
                if (!String.IsNullOrWhiteSpace(objReq.FabricColor) && !String.IsNullOrWhiteSpace(objReq.StyleCode))
                {
                    cmd.Parameters.AddWithValue("@FabricColor", objReq.FabricColor);
                }
                if (!String.IsNullOrWhiteSpace(objReq.MarkerType))
                {
                    cmd.Parameters.AddWithValue("@MarkerType", objReq.MarkerType);
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
                        obj.CreatedBy = Convert.ToInt32(ds.Tables[0].Rows[i]["CreatedBy"]);
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
                Logger.WriteLog("Function Name : Fn_Get_FabricOrder", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Get_FabricOrder");
            return objResp;
        }

        #endregion End Fn_Get_FabricOrder 18-JUN-2026

        #region Start Fn_Delete_ItemCode 18-JUN-2026

        public clsFabricOrder Fn_Delete_ItemCode(clsFabricOrder objReq)
        {
            var objResp = new clsFabricOrder();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Delete_ItemCode");
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_FABRIC_ORDER", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StyleCode", objReq.StyleCode);
                cmd.Parameters.AddWithValue("@ItemCode", objReq.ItemCode);
                cmd.Parameters.AddWithValue("@QueryType", "DELETE_ITEMCODE");
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
                Logger.WriteLog("Function Name : Fn_Delete_ItemCode", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Delete_ItemCode");
            return objResp;
        }

        #endregion End Fn_Delete_ItemCode 18-JUN-2026

        #region Start Fn_Get_Fabric_ColorLits 22-JUN-2026

        public List<clsFabricColor> Fn_Get_Fabric_ColorLits(clsFabricColor objReq)
        {
            var objResp = new List<clsFabricColor>();
            var obj = new clsFabricColor();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Get_Fabric_ColorLits");
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT Distinct Color from OrderDetail OD";
                strSql = strSql + " INNER JOIN OrderMaster OM ON OD.OrderNo = OM.ID WHERE 1=1 ";

                if (!String.IsNullOrWhiteSpace(objReq.StyleCode))
                {
                    strSql = strSql + " AND OM.StyleCode = @StyleCode ";
                }
                strSql = strSql + " ORDER BY Color ";

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
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsFabricColor();
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
                Logger.WriteLog("Function Name : Fn_Get_Fabric_ColorLits", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Get_Fabric_ColorLits");
            return objResp;
        }

        #endregion End Fn_Get_Fabric_ColorLits 22-JUN-2026 

        #region Start Fn_Update_OpNo_IN_OBD 02-SEP-2026

        public clsOPBreackDownDetail Fn_Update_OpNo_IN_OBD(clsOPBreackDownDetail objReq)
        {
            var objResp = new clsOPBreackDownDetail();            
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Update_OpNo_IN_OBD");
            try
            {

                if (objReq.OpNo == 0 && objReq.OpNo == null)
                {
                    objResp.vErrorCode = 400;
                    objResp.vErrorMsg = "Please Enter OpNo";
                }
                else if (String.IsNullOrWhiteSpace(objReq.CreatedOn))
                {
                    objResp.vErrorCode = 400;
                    objResp.vErrorMsg = "Please Enter StyleCode";
                }
                else if (String.IsNullOrWhiteSpace(objReq.SubSection) || objReq.SubSection == "0")
                {
                    objResp.vErrorCode = 400;
                    objResp.vErrorMsg = "Please Select SubSection";
                }
                else if (objReq.Rate <= 0)
                {
                    objResp.vErrorCode = 400;
                    objResp.vErrorMsg = "Please Enter a valid Rate";
                }
                else
                {

                    if (Con.State == ConnectionState.Broken)
                    { Con.Close(); }
                    if (Con.State == ConnectionState.Closed)
                    { Con.Open(); }

                    SqlCommand cmd = new SqlCommand("USP_OPEARTION_BREACK_DOWN", Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DetailID", objReq.DetailID);
                    cmd.Parameters.AddWithValue("@ID", objReq.MID);
                    cmd.Parameters.AddWithValue("@OpNo", objReq.OpNo);
                    cmd.Parameters.AddWithValue("@Rate", objReq.Rate);
                    cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                    cmd.Parameters.AddWithValue("@CreatedOn", objReq.CreatedOn); // StyleCode
                    cmd.Parameters.AddWithValue("@QueryType", "Update_Opno_IN_OBD");
                    int j = cmd.ExecuteNonQuery();
                    if (j > 0)
                    {
                        objResp.vErrorCode = 200;
                        objResp.vErrorMsg = "Success";
                    }
                    else
                    {
                        objResp.vErrorCode = 400;
                        objResp.vErrorMsg = "OpNo Updating failed ";
                        return objResp;
                    }
                }
            }
            catch (Exception exp)
            {
                Logger.WriteLog("Function Name : Fn_Update_OpNo_IN_OBD", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
                objResp.vErrorCode = 500;
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Update_OpNo_IN_OBD");
            return objResp;
        }

        #endregion End Fn_Update_OpNo_IN_OBD 02-SEP-2026
    }
}