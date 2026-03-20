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
    public class MOBDALProduction
    {

        SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["BSL"].ConnectionString);

        public List<clsBundleCompile> Fn_Get_ActiveBundle(clsBundleCompile objReq)
        {
            var objResp = new List<clsBundleCompile>();
            var obj = new clsBundleCompile();
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT BC.BundleID AS BundleID, BC.LayID AS LayID, BC.BundleNo AS BundleNo,";
                strSql = strSql + " BC.SizeName AS SizeName, BC.ColorName AS ColorName, BC.ShadeName AS ShadeName,";
                strSql = strSql + " BC.Qty AS Qty, BC.PlyTo AS PlyTo, BC.PlyFrom AS PlyFrom, BC.LotNo AS LotNo,";
                strSql = strSql + " BC.SubSection AS SubSection, BC.Dispatch AS Dispatch, OM.StyleCode AS StyleCode, OM.OrderNo AS OrderNo";
                strSql = strSql + " FROM BundleCompile AS BC";
                strSql = strSql + " INNER JOIN OrderMaster AS OM";
                strSql = strSql + " ON BC.OrderNo = OM.OrderNo WHERE 1=1";

                if (!String.IsNullOrWhiteSpace(objReq.OrderNo))
                {
                    strSql = strSql + " AND OM.OrderNo = '" + objReq.OrderNo + "'";
                }
                if (objReq.BundleID > 0)
                {
                    strSql = strSql + " AND BC.BundleID = " + objReq.BundleID;
                }

                strSql = strSql + " ORDER BY BC.BundleID ASC";

                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;

                //if (objReq.BundleID > 0)
                //{
                //    cmd.Parameters.AddWithValue("@BundleID", objReq.BundleID);
                //}
                //if (!String.IsNullOrWhiteSpace(objReq.OrderNo))
                //{
                //    cmd.Parameters.AddWithValue("@OrderNo", objReq.OrderNo);
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
                        obj.LayID = Convert.ToInt64(ds.Tables[0].Rows[i]["LayID"]);
                        obj.BundleNo = Convert.ToInt32(ds.Tables[0].Rows[i]["BundleNo"]);
                        obj.SizeName = Convert.ToString(ds.Tables[0].Rows[i]["SizeName"]);
                        obj.ColorName = Convert.ToString(ds.Tables[0].Rows[i]["ColorName"]);
                        obj.ShadeName = Convert.ToString(ds.Tables[0].Rows[i]["ShadeName"]);
                        obj.Qty = Convert.ToInt32(ds.Tables[0].Rows[i]["Qty"]);
                        obj.PlyTo = Convert.ToInt32(ds.Tables[0].Rows[i]["PlyTo"]);
                        obj.PlyFrom = Convert.ToInt32(ds.Tables[0].Rows[i]["PlyFrom"]);
                        obj.LotNo = Convert.ToInt32(ds.Tables[0].Rows[i]["LotNo"]);
                        obj.SubSection = Convert.ToString(ds.Tables[0].Rows[i]["SubSection"]);
                        obj.IsDispatch = Convert.ToBoolean(ds.Tables[0].Rows[i]["Dispatch"]);
                        obj.StyleCode = Convert.ToString(ds.Tables[0].Rows[i]["StyleCode"]);
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
                    obj.vErrorMsg = "No Records are found.";
                    objResp.Add(obj);
                }
            }
            catch (Exception exp)
            {
                obj.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Get_ActiveBundle", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }


        public clsBundleCompile Fn_Update_BundleID_By_EmpID(clsBundleCompile objReq)
        {
            var objResp = new clsBundleCompile();
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_MobileBundleApp", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@BundleID", objReq.BundleID);
                cmd.Parameters.AddWithValue("@AppEmpID", objReq.AppEmpID);
                cmd.Parameters.AddWithValue("@AppStartTime", objReq.AppStartTime);
                cmd.Parameters.AddWithValue("@ModifiedBy", objReq.ModifiedBy);
                cmd.Parameters.AddWithValue("@QueryType", "UpdateBundleDetails");

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        objResp.vErrorCode = Convert.ToInt32(dr["StatusCode"]);
                        objResp.vErrorMsg = dr["Message"].ToString();
                    }
                    else
                    {
                        objResp.vErrorCode = 400;
                        objResp.vErrorMsg = "Updating Failed.";
                    }
                }
            }
            catch (Exception exp)
            {
                objResp.vErrorCode = 500;
                objResp.vErrorMsg = exp.Message.ToString();
                Logger.WriteLog("Function Name : Fn_Update_BundleID_By_EmpID", "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
            }
            finally
            {
                Con.Close();
            }
            return objResp;           
        }


        public List<clsOPBreackDownDetail> Fn_Get_OperationNumber(clsOPBreackDownDetail objReq)
        {
            var objResp = new List<clsOPBreackDownDetail>();
            var obj = new clsOPBreackDownDetail();
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT OpNo, Descriptions, Machine, SubSection, StdMin, Rate, Product,";
                strSql = strSql + " Rate, Product, Skill, Grade, Folder, Seamlength, IsDirect, MID";
                strSql = strSql + " FROM OperationBreackDown WHERE 1=1";

                if (objReq.OpNo > 0)
                {
                    strSql = strSql + " AND OpNo = @OpNo";
                }

                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;

                if (objReq.OpNo > 0)
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
                        //obj.ProgressPoint = Convert.ToString(ds.Tables[0].Rows[i]["ProgressPoint"]);
                        obj.MID = Convert.ToInt64(ds.Tables[0].Rows[i]["MID"]);

                        obj.vErrorCode = 200;
                        obj.vErrorMsg = "Success";
                        objResp.Add(obj);
                        i++;
                    }
                }
                else
                {
                    obj.vErrorCode = 404;
                    obj.vErrorMsg = "No Records are found.";
                    objResp.Add(obj);
                }
            }
            catch (Exception exp)
            {
                obj.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Get_OperationNumber", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }


        public List<clsMachineLogMaster> Fn_Get_MachineLogMaster(clsMachineLogMaster objReq)
        {
            var objResp = new List<clsMachineLogMaster>();
            var obj = new clsMachineLogMaster();
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT MachineLogId, MachineLogName, CreatedBy, FORMAT(CreatedOn, 'dd-MMM-yyyy') AS CreatedOn";
                strSql = strSql + " FROM MachineLogMaster WHERE 1=1";

                if (objReq.MachineLogId > 0)
                {
                    strSql = strSql + " AND MachineLogId = @MachineLogId";
                }
                if (!String.IsNullOrWhiteSpace(objReq.MachineLogName))
                {
                    strSql = strSql + " AND MachineLogName LIKE '%@MachineLogName%'";
                }

                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;

                if (objReq.MachineLogId > 0)
                {
                    cmd.Parameters.AddWithValue("@MachineLogId", objReq.MachineLogId);
                }
                if (!String.IsNullOrWhiteSpace(objReq.MachineLogName))
                {
                    cmd.Parameters.AddWithValue("@MachineLogName", objReq.MachineLogName);
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsMachineLogMaster();
                        obj.MachineLogId = Convert.ToInt32(ds.Tables[0].Rows[i]["MachineLogId"]);
                        obj.MachineLogName = Convert.ToString(ds.Tables[0].Rows[i]["MachineLogName"]);
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
                    obj.vErrorMsg = "No Records found.";
                    objResp.Add(obj);
                }
            }        
            catch (Exception exp)
            {
                obj.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Get_MachineLogMaster", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }

        
        public clsMachineLogLostTimeTransactions Fn_Insert_MachineLogTransaction(clsMachineLogLostTimeTransactions objReq)
        {
            var objResp = new clsMachineLogLostTimeTransactions();
            try
            {
                if (objReq.EmpId == null || objReq.EmpId == 0)
                {
                    objResp.vErrorMsg = "Please Pass Employee Id";
                    objResp.vErrorCode = 300;
                }
                else if (objReq.MachineId == null || objReq.MachineId == 0)
                {
                    objResp.vErrorMsg = "Please Pass Machine Id.";
                    objResp.vErrorCode = 300;
                }
                else if (String.IsNullOrWhiteSpace(objReq.MachineLogDescription))
                {
                    objResp.vErrorMsg = "Please Enter the Machine Log Description";
                    objResp.vErrorCode = 300;
                }
                else if (String.IsNullOrWhiteSpace(objReq.MachineStatus))
                {
                    objResp.vErrorMsg = "Please Pass the Machine Status";
                    objResp.vErrorCode = 300;
                }
                else
                {
                    if (Con.State == ConnectionState.Broken)
                    { Con.Close(); }
                    if (Con.State == ConnectionState.Closed)
                    { Con.Open(); }

                    SqlCommand cmd = new SqlCommand("USP_MobileMachineLogTransactions", Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@LineId", objReq.LineId);
                    cmd.Parameters.AddWithValue("@MachineId", objReq.MachineId);
                    cmd.Parameters.AddWithValue("@MachineLogDescription", objReq.MachineLogDescription);
                    cmd.Parameters.AddWithValue("@EmpId", objReq.EmpId);
                    cmd.Parameters.AddWithValue("@MachineStatus", objReq.MachineStatus);
                    cmd.Parameters.AddWithValue("@Needle", objReq.Needle);
                    cmd.Parameters.AddWithValue("@Oiling", objReq.Oiling);
                    cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                    cmd.Parameters.AddWithValue("@QueryType", "InsertMachineLog");

                    int i = 0;
                    i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        objResp.vErrorMsg = "Success";
                        objResp.vErrorCode = 200;
                    }
                    else
                    {
                        objResp.vErrorMsg = "Machine Log Issue insertion failed.";
                        objResp.vErrorCode = 300;
                    }
                }
            }
            catch (Exception exp)
            {
                Logger.WriteLog("Function Name : Fn_Insert_MachineLogTransaction", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
                objResp.vErrorCode = 500;
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }


        public clsMachineLogLostTimeTransactions Fn_Update_MachineLogTransaction(clsMachineLogLostTimeTransactions objReq)
        {
            var objResp = new clsMachineLogLostTimeTransactions();
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_MobileMachineLogTransactions", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LineId", objReq.LineId);
                cmd.Parameters.AddWithValue("@MachineId", objReq.MachineId);
                cmd.Parameters.AddWithValue("@EmpId", objReq.EmpId);
                cmd.Parameters.AddWithValue("@RepairedDate", objReq.RepairDate);
                cmd.Parameters.AddWithValue("@RepairRemark", objReq.RepairRemark);
                cmd.Parameters.AddWithValue("@MachineStatus", objReq.MachineStatus);
                cmd.Parameters.AddWithValue("@ModifiedBy", objReq.ModifiedBy);
                cmd.Parameters.AddWithValue("@QueryType", "UpdateMachineLog");

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
                    objResp.vErrorMsg = "Updating the Machine Log Transactions failed.";
                }
            }
            catch (Exception exp)
            {
                objResp.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Update_MachineLogTransaction", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }


        public List<clsMachineLogLostTimeTransactions> Fn_Get_All_MachineLogTransactions(clsMachineLogLostTimeTransactions objReq)
        {
            var objResp = new List<clsMachineLogLostTimeTransactions>();
            var obj = new clsMachineLogLostTimeTransactions();
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT ID, LineId, MachineId, MachineLogDescription, EmpId,";
                strSql = strSql + " MachineStatus, Needle, Oiling, CreatedBy, FORMAT(CreatedOn, 'dd-MMM-yyyy') AS CreatedOn";
                strSql = strSql + " FROM MachineLogLostTimeTransactions WHERE 1=1";

                if (objReq.ID > 0)
                {
                    strSql = strSql + " AND ID = @ID";
                }

                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;

                if (objReq.ID > 0)
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
                        obj = new clsMachineLogLostTimeTransactions();
                        obj.ID = Convert.ToInt64(ds.Tables[0].Rows[i]["ID"]);
                        obj.LineId = Convert.ToInt64(ds.Tables[0].Rows[i]["LineId"]);
                        obj.MachineId = Convert.ToInt64(ds.Tables[0].Rows[i]["MachineId"]);
                        obj.MachineLogDescription = Convert.ToString(ds.Tables[0].Rows[i]["MachineLogDescription"]);
                        obj.EmpId = Convert.ToInt32(ds.Tables[0].Rows[i]["EmpId"]);
                        obj.MachineStatus = Convert.ToString(ds.Tables[0].Rows[i]["MachineStatus"]);
                        obj.Needle = Convert.ToBoolean(ds.Tables[0].Rows[i]["Needle"]);
                        obj.Oiling = Convert.ToBoolean(ds.Tables[0].Rows[i]["Oiling"]);
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
                    obj.vErrorMsg = "No Records found.";
                    objResp.Add(obj);
                }
            }
            catch (Exception exp)
            {
                obj.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Get_All_MachineLogTransactions", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }


        public clsMachineLogLostTimeTransactions Fn_Get_MachineLogLostTime(clsMachineLogLostTimeTransactions objReq)
        {
            var objResp = new clsMachineLogLostTimeTransactions();
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_MobileMachineLogTransactions", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LineId", objReq.LineId);
                cmd.Parameters.AddWithValue("@MachineId", objReq.MachineId);
                cmd.Parameters.AddWithValue("@EmpId", objReq.EmpId);
                cmd.Parameters.AddWithValue("@QueryType", "SelectMachineLog");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                int i = 0;

                if (ds.Tables[0].Rows.Count > 0)
                {
                    objResp.ID = Convert.ToInt64(ds.Tables[0].Rows[i]["ID"]);
                    objResp.LineId = Convert.ToInt64(ds.Tables[0].Rows[i]["LineId"]);
                    objResp.MachineId = Convert.ToInt64(ds.Tables[0].Rows[i]["MachineId"]);
                    objResp.MachineLogDescription = Convert.ToString(ds.Tables[0].Rows[i]["MachineLogDescription"]);
                    objResp.EmpId = Convert.ToInt32(ds.Tables[0].Rows[i]["EmpId"]);
                    objResp.Needle = Convert.ToBoolean(ds.Tables[0].Rows[i]["Needle"]);
                    objResp.Oiling = Convert.ToBoolean(ds.Tables[0].Rows[i]["Oiling"]);
                    objResp.CreatedOn = Convert.ToString(ds.Tables[0].Rows[i]["CreatedOn"]);
                    objResp.RepairDate = Convert.ToString(ds.Tables[0].Rows[i]["RepairDate"]);
                    objResp.RepairRemark = Convert.ToString(ds.Tables[0].Rows[i]["RepairRemark"]);

                    TimeSpan difference = Convert.ToDateTime(objResp.RepairDate) - Convert.ToDateTime(objResp.CreatedOn);

                    string formattedDifference = string.Format("{0} Days, {1} Hours, {2} Minutes",
                        difference.Days,
                        difference.Hours,
                        difference.Minutes);

                    objResp.TimeSpentDifference = formattedDifference;

                    objResp.vErrorCode = 200;
                    objResp.vErrorMsg = "Success";
                }
                else
                {
                    objResp.vErrorCode = 404;
                    objResp.vErrorMsg = "Machine Log not found.";
                }
            }
            catch (Exception exp)
            {
                Logger.WriteLog("Function Name : Fn_Get_MachineLogLostTime", " " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }



        public clsBundleCompile Fn_Update_SupervisorAssignedBundleIDEmp(clsBundleCompile objReq)
        {
            var objResp = new clsBundleCompile();
            try
            {
                if (objReq.SupervisorID == null || objReq.SupervisorID == 0)
                {
                    objResp.vErrorMsg = "Please Pass the Valid Supervisor Employee ID";
                    objResp.vErrorCode = 300;
                }
                else if (objReq.BundleID == null || objReq.BundleID == 0)
                {
                    objResp.vErrorMsg = "Please Pass the Valid Bundle ID";
                    objResp.vErrorCode = 300;
                }
                else if (objReq.AppEmpID == null || objReq.AppEmpID == 0)
                {
                    objResp.vErrorMsg = "Please Pass the Valid App Employee/Worker ID";
                    objResp.vErrorCode = 300;
                }
                else
                {
                    if (Con.State == ConnectionState.Broken)
                    { Con.Close(); }
                    if (Con.State == ConnectionState.Closed)
                    { Con.Open(); }

                    SqlCommand cmd = new SqlCommand("USP_MobileBundleApp", Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BundleID", objReq.BundleID);
                    cmd.Parameters.AddWithValue("@SupervisorID", objReq.SupervisorID);
                    cmd.Parameters.AddWithValue("@AppEmpID", objReq.AppEmpID);
                    cmd.Parameters.AddWithValue("@QueryType", "UpdateAssignedBundleID");

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            objResp.vErrorCode = Convert.ToInt32(dr["StatusCode"]);
                            objResp.vErrorMsg = dr["Message"].ToString();
                        }
                        else
                        {
                            objResp.vErrorCode = 400;
                            objResp.vErrorMsg = "Assigned Bundle ID allocation failed.";
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                objResp.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Update_SupervisorAssignedBundleIDEmp", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }


        public clsBundleCompile Fn_Update_AppEmpStartBundleIDStatus(clsBundleCompile objReq)
        {
            Boolean ConfigField = Convert.ToBoolean(ConfigurationManager.AppSettings["BundleCompileValue"]);
            var objResp = new clsBundleCompile();
            try
            {
                if (ConfigField)
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

                        SqlCommand cmd = new SqlCommand("USP_MobileBundleApp", Con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@AppEmpID", objReq.AppEmpID);
                        cmd.Parameters.AddWithValue("@BundleID", objReq.BundleID);
                        cmd.Parameters.AddWithValue("@QueryType", "UpdateAppEmpStartBundleIDStatus");

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
                                objResp.vErrorMsg = "Supervisor needs to assigned the Bundle ID to Worker/Employee.";
                            }
                        }
                    }
                }
                else
                {
                    MOBDALProduction _MOBDALProduction = new MOBDALProduction();
                    var objBundleCompile = new clsBundleCompile();
                    objBundleCompile.AppEmpID = objReq.AppEmpID;
                    objBundleCompile.BundleID = objReq.BundleID;
                    objBundleCompile = _MOBDALProduction.Fn_Update_AppEmpStartEndBundleIDStatus(objBundleCompile);
                    objResp.vErrorCode = objBundleCompile.vErrorCode;
                    objResp.vErrorMsg = objBundleCompile.vErrorMsg;
                }
            }
            catch (Exception exp)
            {
                objResp.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Update_AppEmpStartBundleIDStatus", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }


        public clsBundleCompile Fn_Update_AppEmpEndBundleIDStatus(clsBundleCompile objReq)
        {
            var objResp = new clsBundleCompile();
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

                    SqlCommand cmd = new SqlCommand("USP_MobileBundleApp", Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AppEmpID", objReq.AppEmpID);
                    cmd.Parameters.AddWithValue("@BundleID", objReq.BundleID);
                    cmd.Parameters.AddWithValue("@QueryType", "UpdateAppEmpEndBundleIDStatus");

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
                        objResp.vErrorMsg = "App Employee Bundle ID Finished Status updation failed.";
                    }
                }
            }
            catch (Exception exp)
            {
                objResp.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Update_AppEmpEndBundleIDStatus", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }


        public List<clsLine> Fn_Get_ActiveLineDetails(clsLine objReq)
        {
            var objResp = new List<clsLine>();
            var obj = new clsLine();
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                //string strSql = "SELECT LineId, SeqNo, LineCode, LineName, SuperVisor, SectionName, SuperMarketCode,";
                //strSql = strSql + " DivisionID, LineStatus FROM LineMaster WHERE 1=1 AND LineStatus = 'Active'";

                string strSql = "SELECT LM.LineId AS LineId, LM.LineName AS LineName,";
                strSql = strSql + " LM.LineStatus AS LineStatus, COUNT(ED.Code) AS OperatorCount,";
                strSql = strSql + " LM.SeqNo AS SeqNo, LM.LineCode AS LineCode, LM.SuperVisor AS SuperVisor,";
                strSql = strSql + " LM.SuperMarketCode AS SuperMarketCode, LM.SectionName AS SectionName, LM.DivisionID AS DivisionID";
                strSql = strSql + " FROM EmployeeMaster AS EM";
                strSql = strSql + " INNER JOIN EmployeeDetail AS ED";
                strSql = strSql + " ON EM.EmpId = ED.Code";
                strSql = strSql + " INNER JOIN LineMaster AS LM";
                strSql = strSql + " ON LM.LineName = ED.LineName";
                strSql = strSql + " WHERE LM.LineStatus = 'Active' AND EM.IsActive = 1 AND EM.EmpRole = 'Operator'";
                strSql = strSql + " GROUP BY LM.LineId, LM.LineName, LM.LineStatus,";
                strSql = strSql + " LM.SeqNo, LM.LineCode, LM.SuperVisor, LM.SuperMarketCode, LM.SectionName, LM.DivisionID";
                strSql = strSql + " ORDER BY LM.LineId";

                //if (objReq.LineId > 0)
                //{
                //    strSql = strSql + " AND LineId = @LineId";
                //}

                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;

                //if (objReq.LineId > 0)
                //{
                //    cmd.Parameters.AddWithValue("@LineId", objReq.LineId);
                //}

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;

                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsLine();
                        obj.LineId = Convert.ToInt64(ds.Tables[0].Rows[i]["LineId"]);
                        obj.LineName = Convert.ToString(ds.Tables[0].Rows[i]["LineName"]);
                        obj.LineStatus = Convert.ToString(ds.Tables[0].Rows[i]["LineStatus"]);
                        obj.OperatorCount = Convert.ToString(ds.Tables[0].Rows[i]["OperatorCount"]);

                        if (ds.Tables[0].Rows[i]["SeqNo"] == DBNull.Value)
                        {
                            obj.SeqNo = 0;
                        }
                        else
                        {
                            obj.SeqNo = Convert.ToInt32(ds.Tables[0].Rows[i]["SeqNo"]);
                        }

                        if (ds.Tables[0].Rows[i]["LineCode"] == null)
                        {
                            obj.LineCode = string.Empty;
                        }
                        else
                        {
                            obj.LineCode = Convert.ToString(ds.Tables[0].Rows[i]["LineCode"]);
                        }

                        if (ds.Tables[0].Rows[i]["SuperVisor"] == null)
                        {
                            obj.SuperVisor = string.Empty;
                        }
                        else
                        {
                            obj.SuperVisor = Convert.ToString(ds.Tables[0].Rows[i]["SuperVisor"]);
                        }

                        if (ds.Tables[0].Rows[i]["SectionName"] == null)
                        {
                            obj.SectionName = string.Empty;
                        }
                        else
                        {
                            obj.SectionName = Convert.ToString(ds.Tables[0].Rows[i]["SectionName"]);
                        }

                        if (ds.Tables[0].Rows[i]["SuperMarketCode"] == DBNull.Value)
                        {
                            obj.SuperMarketCode = 0;
                        }
                        else
                        {
                            obj.SuperMarketCode = Convert.ToInt32(ds.Tables[0].Rows[i]["SuperMarketCode"]);
                        }

                        if (ds.Tables[0].Rows[i]["DivisionID"] == DBNull.Value)
                        {
                            obj.DivisionID = 0;
                        }
                        else
                        {
                            obj.DivisionID = Convert.ToInt64(ds.Tables[0].Rows[i]["DivisionID"]);
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
                    obj.vErrorMsg = "No Line Records are found.";
                    objResp.Add(obj);
                }
            }
            catch (Exception exp)
            {
                obj.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Get_ActiveLineDetails", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }


        public List<clsLine> Fn_Get_ActiveLineCount(clsLine objReq)
        {
            var objResp = new List<clsLine>();
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_MobileLinesApp", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@QueryType", "GetLineCounts");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        var objItem = new clsLine();
                        objItem.LineCount = Convert.ToInt64(ds.Tables[0].Rows[i]["LineCount"]);
                        objItem.LineStatus = Convert.ToString(ds.Tables[0].Rows[i]["LineStatus"]);
                        objItem.vErrorMsg = "Success";
                        objItem.vErrorCode = 200;
                        objResp.Add(objItem);
                        i++;
                    }
                }
                else
                {
                    var objItem = new clsLine();
                    objItem.vErrorMsg = "No Line Counts Found";
                    objResp.Add(objItem);
                }
            }
            catch (Exception exp)
            {
                var objItem = new clsLine();
                objItem.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Get_ActiveLineCount", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objItem.vErrorMsg = exp.Message.ToString();
                objResp.Add(objItem);
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }


        public List<clsBundleCompile> Fn_Get_TotalBundleIdCount(clsBundleCompile objReq)
        {
            var objResp = new List<clsBundleCompile>();
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_MobileBundleApp", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@QueryType", "GetTotalBundleIdCount");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        var objItem = new clsBundleCompile();
                        objItem.TotalBundleIdCount = Convert.ToString(ds.Tables[0].Rows[i]["TotalBundleIdCount"]);
                        objItem.vErrorMsg = "Success";
                        objItem.vErrorCode = 200;
                        objResp.Add(objItem);
                        i++;
                    }
                }
                else
                {
                    var objItem = new clsBundleCompile();
                    objItem.vErrorMsg = "Bundle ID Count not found.";
                    objResp.Add(objItem);
                }
            }
            catch (Exception exp)
            {
                var objItem = new clsBundleCompile();
                objItem.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Get_TotalBundleIdCount", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objItem.vErrorMsg = exp.Message.ToString();
                objResp.Add(objItem);
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }



        public List<clsMachineLogLostTimeTransactions> Fn_Get_MachineLogLostTimeInDaysHrMin(clsMachineLogLostTimeTransactions objReq)
        {
            var objResp = new List<clsMachineLogLostTimeTransactions>();
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_MobileMachineLogTransactions", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@QueryType", "GetMachineLogLostTotalTime");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        var objItem = new clsMachineLogLostTimeTransactions();
                        objItem.TotalMachineLogLostTime = Convert.ToString(ds.Tables[0].Rows[i]["TotalMachineLogLostTime"]);
                        objItem.vErrorMsg = "Success";
                        objItem.vErrorCode = 200;
                        objResp.Add(objItem);
                        i++;
                    }
                }
                else
                {
                    var objItem = new clsMachineLogLostTimeTransactions();
                    objItem.vErrorMsg = "No Machine Log Lost time count not found.";
                    objResp.Add(objItem);
                }
            }
            catch (Exception exp)
            {
                var objItem = new clsMachineLogLostTimeTransactions();
                objItem.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Get_MachineLogLostTimeInDaysHrMin", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objItem.vErrorMsg = exp.Message.ToString();
                objResp.Add(objItem);
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }


        public clsBundleCompile Fn_Update_AppEmpStartEndBundleIDStatus(clsBundleCompile objReq)
        {
            var objResp = new clsBundleCompile();
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

                    SqlCommand cmd = new SqlCommand("USP_MobileBundleApp", Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AppEmpID", objReq.AppEmpID);
                    cmd.Parameters.AddWithValue("@BundleID", objReq.BundleID);
                    cmd.Parameters.AddWithValue("@QueryType", "UpdateAppEmpStartEndBundleIDStatus");

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
                            objResp.vErrorMsg = "Supervisor needs to assigned the Bundle ID to Worker/Employee";
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                objResp.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Update_AppEmpStartEndBundleIDStatus", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }



        public List<clsLine> Fn_Get_All_LinewiseOperatorCount(clsLine objReq)
        {
            var objResp = new List<clsLine>();
            var obj = new clsLine();
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT LM.LineId, LM.LineName AS LineName, COUNT(EM.EmpId) AS OperatorCount";
                strSql = strSql + " FROM EmployeeMaster AS EM";
                strSql = strSql + " INNER JOIN EmployeeDetail AS ED";
                strSql = strSql + " ON ED.Code = EM.EmpId";
                strSql = strSql + " INNER JOIN LineMaster AS LM";
                strSql = strSql + " ON LM.LineName=ED.LineName";
                strSql = strSql + " WHERE EM.IsActive=1 AND EM.EmpRole='Operator' AND LM.LineStatus='Active'";
                strSql = strSql + " GROUP BY LM.LineName, LM.LineId";
                strSql = strSql + " ORDER BY LM.LineId ASC";

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
                        obj = new clsLine();
                        obj.LineName = Convert.ToString(ds.Tables[0].Rows[i]["LineName"]);
                        obj.OperatorCount = Convert.ToString(ds.Tables[0].Rows[i]["OperatorCount"]);

                        obj.vErrorCode = 200;
                        obj.vErrorMsg = "Success";
                        objResp.Add(obj);
                        i++;
                    }
                }
                else
                {
                    obj.vErrorCode = 404;
                    obj.vErrorMsg = "No Active Lineswise Operator not found.";
                    objResp.Add(obj);
                }
            }
            catch (Exception exp)
            {
                obj.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Get_All_LinewiseOperatorCount", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }


        public List<clsBundleCompile> Fn_Get_LineBundleIDCountOperator(clsBundleCompile objReq)
        {
            var objResp = new List<clsBundleCompile>();
            var obj = new clsBundleCompile();
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT BC.BundleID AS BundleID, EM.EmpName AS EmpName, BC.BundleIDStatus AS BundleIDStatus";
                strSql = strSql + " FROM BundleCompile AS BC";
                strSql = strSql + " INNER JOIN EmployeeMaster AS EM";
                strSql = strSql + " ON BC.AppEmpID = EM.EmpId";
                strSql = strSql + " INNER JOIN EmployeeDetail AS ED";
                strSql = strSql + " ON ED.Code = EM.EmpId";
                strSql = strSql + " INNER JOIN LineMaster AS LM";
                strSql = strSql + " ON LM.LineName=ED.LineName";
                strSql = strSql + " WHERE EM.IsActive=1 AND EM.EmpRole='Operator' AND LM.LineStatus='Active'";
                strSql = strSql + " ORDER BY BC.BundleID ASC";

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
                        obj = new clsBundleCompile();
                        obj.AppEmpName = Convert.ToString(ds.Tables[0].Rows[i]["EmpName"]);
                        obj.BundleID = Convert.ToInt64(ds.Tables[0].Rows[i]["BundleID"]);
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
                    obj.vErrorMsg = "Bundle ID wise operator details are not found.";
                    objResp.Add(obj);
                }
            }
            catch (Exception exp)
            {
                obj.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Get_LineBundleIDCountOperator", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }


        public List<clsBundleCompile> Fn_Get_OperatorBundleIDQtyStyleDetails(clsBundleCompile objReq)
        {
            var objResp = new List<clsBundleCompile>();
            var obj = new clsBundleCompile();
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT EM.EmpId AS EmpId, EM.EmpName AS EmpName, ED.LineName AS LineName, BC.BundleID AS BundleID,";
                strSql = strSql + " BC.Qty AS Qty, BC.StyleCode AS StyleCode, BC.OrderNo AS OrderNo,";
                strSql = strSql + " BC.BundleIDStatus AS BundleIDStatus";
                strSql = strSql + " FROM BundleCompile AS BC";
                strSql = strSql + " INNER JOIN EmployeeMaster AS EM";
                strSql = strSql + " ON BC.AppEmpID = EM.EmpId";
                strSql = strSql + " INNER JOIN EmployeeDetail AS ED";
                strSql = strSql + " ON ED.Code = EM.EmpId";
                strSql = strSql + " INNER JOIN LineMaster AS LM";
                strSql = strSql + " ON LM.LineName = ED.LineName";
                strSql = strSql + " WHERE LM.LineStatus = 'Active'";
                strSql = strSql + " ORDER BY BC.BundleID ASC";

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
                        obj = new clsBundleCompile();
                        obj.AppEmpID = Convert.ToInt32(ds.Tables[0].Rows[i]["EmpId"]);
                        obj.AppEmpName = Convert.ToString(ds.Tables[0].Rows[i]["EmpName"]);
                        obj.LineName = Convert.ToString(ds.Tables[0].Rows[i]["LineName"]);
                        obj.BundleID = Convert.ToInt64(ds.Tables[0].Rows[i]["BundleID"]);
                        obj.Qty = Convert.ToInt32(ds.Tables[0].Rows[i]["Qty"]);
                        obj.StyleCode = Convert.ToString(ds.Tables[0].Rows[i]["StyleCode"]);
                        obj.OrderNo = Convert.ToString(ds.Tables[0].Rows[i]["OrderNo"]);
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
                    obj.vErrorMsg = "Operator wise Bundle ID details are not found.";
                    objResp.Add(obj);
                }
            }
            catch (Exception exp)
            {
                obj.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Get_OperatorBundleIDQtyStyleDetails", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }


        public List<clsBundleCompile> Fn_Get_OperatorIDWiseBundleDetails(clsBundleCompile objReq)
        {
            var objResp = new List<clsBundleCompile>();
            var obj = new clsBundleCompile();
            try
            {
                if (objReq.AppEmpID == 0)
                {
                    obj.vErrorMsg = "Please Pass the Valid App Employee/Operator ID";
                    obj.vErrorCode = 300;
                }
                else
                {
                    if (Con.State == ConnectionState.Broken)
                    { Con.Close(); }
                    if (Con.State == ConnectionState.Closed)
                    { Con.Open(); }

                    string strSql = "SELECT BC.AppEmpID AS EmpID, EM.EmpName AS EmpName, BC.BundleID AS BundleID, ED.LineName AS LineName,";
                    strSql = strSql + " BC.SizeName AS SizeName, BC.Qty AS Qty, BC.SubSection AS SubSection, BC.StyleCode AS StyleCode,";
                    strSql = strSql + " BC.OrderNo AS OrderNo, BC.ColorName AS ColorName, BC.BundleIDStatus AS BundleIDStatus";
                    strSql = strSql + " FROM BundleCompile AS BC";
                    strSql = strSql + " INNER JOIN EmployeeMaster AS EM";
                    strSql = strSql + " ON BC.AppEmpID = EM.EmpId";
                    strSql = strSql + " INNER JOIN EmployeeDetail AS ED";
                    strSql = strSql + " ON ED.Code = EM.EmpId";
                    strSql = strSql + " INNER JOIN LineMaster AS LM";
                    strSql = strSql + " ON LM.LineName = ED.LineName";
                    strSql = strSql + " WHERE 1=1";

                    if (objReq.AppEmpID > 0)
                    {
                        strSql = strSql + " AND BC.AppEmpID = @AppEmpID";
                    }

                    SqlCommand cmd = new SqlCommand(strSql, Con);
                    cmd.CommandType = CommandType.Text;

                    if (objReq.AppEmpID > 0)
                    {
                        cmd.Parameters.AddWithValue("@AppEmpID", objReq.AppEmpID);
                    }

                    strSql = strSql + " ORDER BY BC.BundleID";

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    int i = 0;
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        while (ds.Tables[0].Rows.Count > i)
                        {
                            obj = new clsBundleCompile();
                            obj.AppEmpID = Convert.ToInt32(ds.Tables[0].Rows[i]["EmpID"]);
                            obj.AppEmpName = Convert.ToString(ds.Tables[0].Rows[i]["EmpName"]);
                            obj.BundleID = Convert.ToInt64(ds.Tables[0].Rows[i]["BundleID"]);
                            obj.LineName = Convert.ToString(ds.Tables[0].Rows[i]["LineName"]);
                            obj.SizeName = Convert.ToString(ds.Tables[0].Rows[i]["SizeName"]);
                            obj.Qty = Convert.ToInt32(ds.Tables[0].Rows[i]["Qty"]);
                            obj.SubSection = Convert.ToString(ds.Tables[0].Rows[i]["SubSection"]);
                            obj.StyleCode = Convert.ToString(ds.Tables[0].Rows[i]["StyleCode"]);
                            obj.OrderNo = Convert.ToString(ds.Tables[0].Rows[i]["OrderNo"]);
                            obj.ColorName = Convert.ToString(ds.Tables[0].Rows[i]["ColorName"]);
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
                        obj.vErrorMsg = "Operator/Worker/Employee Wise records are not found.";
                        objResp.Add(obj);
                    }
                }
            }
            catch (Exception exp)
            {
                obj.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Get_OperatorIDWiseBundleDetails", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }



        public List<clsLine> Fn_Get_LineOverviewDetails(clsLine objReq)
        {
            var objResp = new List<clsLine>();
            var obj = new clsLine();
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT LM.LineId AS LineId, ED.LineName AS LineName, ED.Units AS Units,";
                strSql = strSql + " COUNT(EM.EmpId) AS Operators, LM.LineStatus AS LineStatus";
                strSql = strSql + " FROM EmployeeMaster AS EM";
                strSql = strSql + " INNER JOIN EmployeeDetail AS ED";
                strSql = strSql + " ON EM.EmpId = ED.Code";
                strSql = strSql + " INNER JOIN LineMaster AS LM";
                strSql = strSql + " ON ED.LineName = LM.LineName";
                strSql = strSql + " WHERE EM.IsActive = 1 AND 1=1";
                strSql = strSql + " GROUP BY LM.LineId, ED.LineName, ED.Units, LM.LineStatus";

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
                        obj = new clsLine();
                        obj.LineId = Convert.ToInt64(ds.Tables[0].Rows[i]["LineId"]);
                        obj.LineName = Convert.ToString(ds.Tables[0].Rows[i]["LineName"]);
                        obj.Units = Convert.ToString(ds.Tables[0].Rows[i]["Units"]);
                        obj.OperatorCount = Convert.ToString(ds.Tables[0].Rows[i]["Operators"]);
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
                    obj.vErrorMsg = "Line Overview details are not found.";
                    objResp.Add(obj);
                }
            }
            catch (Exception exp)
            {
                obj.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Get_LineOverviewDetails", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }



        public List<clsBundleCompile> Fn_Get_ActiveLineIDWiseOperatorBundleDetails(clsBundleCompile objReq)
        {
            var objResp = new List<clsBundleCompile>();
            var obj = new clsBundleCompile();
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT LM.LineId AS LineId, LM.LineName AS LineName, BC.BundleID AS BundleID,";
                strSql = strSql + " EM.EmpName AS EmpName, BC.BundleIDStatus AS BundleIDStatus";
                strSql = strSql + " FROM BundleCompile AS BC";
                strSql = strSql + " INNER JOIN EmployeeMaster AS EM";
                strSql = strSql + " ON BC.AppEmpID = EM.EmpId";
                strSql = strSql + " INNER JOIN EmployeeDetail AS ED";
                strSql = strSql + " ON ED.Code = EM.EmpId";
                strSql = strSql + " INNER JOIN LineMaster AS LM";
                strSql = strSql + " ON LM.LineName=ED.LineName";
                strSql = strSql + " WHERE EM.IsActive=1 AND EM.EmpRole='Operator'";
                strSql = strSql + " AND LM.LineStatus='Active' AND BC.BundleIDStatus = 'In Process'";

                if (objReq.LineId > 0)
                {
                    strSql = strSql + " AND LM.LineId = @LineId";
                }

                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;

                if (objReq.LineId > 0)
                {
                    cmd.Parameters.AddWithValue("@LineId", objReq.LineId);
                }

                strSql = strSql + " ORDER BY BC.BundleID ASC";

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsBundleCompile();
                        obj.LineId = Convert.ToInt64(ds.Tables[0].Rows[i]["LineId"]);
                        obj.LineName = Convert.ToString(ds.Tables[0].Rows[i]["LineName"]);
                        obj.AppEmpName = Convert.ToString(ds.Tables[0].Rows[i]["EmpName"]);
                        obj.BundleID = Convert.ToInt64(ds.Tables[0].Rows[i]["BundleID"]);
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
                    obj.vErrorMsg = "Active Line ID wise Operator Bundle ID details are not found.";
                    objResp.Add(obj);
                }
            }
            catch (Exception exp)
            {
                obj.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Get_ActiveLineIDWiseOperatorBundleDetails", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }


        public List<clsLine> Fn_Get_LineOverviewOperatorDetailsByLineID(clsLine objReq)
        {
            var objResp = new List<clsLine>();
            var obj = new clsLine();
            try
            {
                if (objReq.LineId < 0)
                {
                    obj.vErrorMsg = "Please Pass the Valid Line ID";
                    obj.vErrorCode = 300;
                }
                else
                {
                    if (Con.State == ConnectionState.Broken)
                    { Con.Close(); }
                    if (Con.State == ConnectionState.Closed)
                    { Con.Open(); }

                    string strSql = "SELECT EM.EmpName AS EmpName, BC.BundleID AS BundleID,";
                    strSql = strSql + " BC.SubSection AS SubSection, BC.Qty AS Qty,";
                    strSql = strSql + " SUM(DATEDIFF(MINUTE, BC.AppStartTime, BC.AppEndTime))% 1440 AS Hours,";
                    strSql = strSql + " SUM(DATEDIFF(MINUTE, BC.AppStartTime, BC.AppEndTime))% 60 AS Minutes";
                    strSql = strSql + " FROM EmployeeMaster AS EM";
                    strSql = strSql + " INNER JOIN EmployeeDetail AS ED";
                    strSql = strSql + " ON EM.EmpId = ED.Code";
                    strSql = strSql + " INNER JOIN BundleCompile AS BC";
                    strSql = strSql + " ON BC.AppEmpID = ED.Code";
                    strSql = strSql + " INNER JOIN LineMaster AS LM";
                    strSql = strSql + " ON LM.LineName = ED.LineName";                    
                    strSql = strSql + " WHERE BC.BundleIDStatus = 'In Process' AND LM.LineId = " + objReq.LineId;
                    strSql = strSql + " GROUP BY EM.EmpName, BC.BundleID, BC.SubSection, BC.Qty";

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
                            obj = new clsLine();
                            obj.AppEmpName = Convert.ToString(ds.Tables[0].Rows[i]["EmpName"]);
                            obj.BundleID = Convert.ToInt64(ds.Tables[0].Rows[i]["BundleID"]);
                            obj.SubSection = Convert.ToString(ds.Tables[0].Rows[i]["SubSection"]);
                            obj.Qty = Convert.ToInt32(ds.Tables[0].Rows[i]["Qty"]);
                            obj.Hours = Convert.ToString(ds.Tables[0].Rows[i]["Hours"]);
                            obj.Minutes = Convert.ToString(ds.Tables[0].Rows[i]["Minutes"]);

                            obj.vErrorCode = 200;
                            obj.vErrorMsg = "Success";
                            objResp.Add(obj);
                            i++;
                        }
                    }
                    else
                    {
                        obj.vErrorCode = 404;
                        obj.vErrorMsg = "Line ID wise Operator/Worker/Employee details are not found.";
                        objResp.Add(obj);
                    }
                }
            }
            catch (Exception exp)
            {
                obj.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Get_LineOverviewOperatorDetailsByLineID", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }



        public List<clsLine> Fn_Get_ActiveLineDetailsOrderNo(clsLine objReq)
        {
            var objResp = new List<clsLine>();
            var obj = new clsLine();
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT LM.LineId AS LineId, ED.LineName AS LineName,";
                strSql = strSql + " LM.LineStatus AS LineStatus, OM.OrderNo AS OrderNo";
                strSql = strSql + " FROM BundleCompile AS BC";
                strSql = strSql + " INNER JOIN EmployeeDetail AS ED";
                strSql = strSql + " ON BC.AppEmpID = ED.Code";
                strSql = strSql + " INNER JOIN LineMaster AS LM";
                strSql = strSql + " ON LM.LineName = ED.LineName";
                strSql = strSql + " INNER JOIN OrderMaster AS OM";
                strSql = strSql + " ON OM.OrderNo = BC.OrderNo";
                strSql = strSql + " WHERE LM.LineStatus = 'Active'";
                //strSql = strSql + " WHERE LM.LineStatus = 'Active' AND LM.LineId = " + objReq.LineId;
                strSql = strSql + " GROUP BY LM.LineId, ED.LineName, LM.LineStatus, OM.OrderNo";

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
                        obj = new clsLine();
                        obj.LineId = Convert.ToInt64(ds.Tables[0].Rows[i]["LineId"]);
                        obj.LineName = Convert.ToString(ds.Tables[0].Rows[i]["LineName"]);
                        obj.LineStatus = Convert.ToString(ds.Tables[0].Rows[i]["LineStatus"]);
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
                    obj.vErrorMsg = "Line ID wise Order No Records are not found.";
                    objResp.Add(obj);
                }
            }
            catch (Exception exp)
            {
                obj.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Get_ActiveLineDetailsOrderNo", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }


    }
}