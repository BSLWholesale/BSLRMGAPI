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

                string strSql = "SELECT BD.BundleID AS BundleID, BD.OperationNo AS OperationNo, BC.LayID AS LayID,";
                strSql = strSql + " BC.BundleNo AS BundleNo, BC.SizeName AS SizeName, BC.ColorName AS ColorName, BC.ShadeName AS ShadeName,";
                strSql = strSql + " BC.Qty AS Qty, BC.PlyTo AS PlyTo, BC.PlyFrom AS PlyFrom, BC.LotNo AS LotNo, BD.SubSection AS SubSection,";
                strSql = strSql + " BC.Dispatch AS Dispatch, OM.StyleCode AS StyleCode, OM.OrderNo AS OrderNo, BC.SupervisorID AS SupervisorID,";
                strSql = strSql + " FORMAT(BC.SupervisorAssignedDate, 'dd-MMM-yyyy HH:mm:ss') AS SupervisorAssignedDate, BD.AppEmpID AS AppEmpID,";
                strSql = strSql + " FORMAT(BD.AppStartTime, 'dd-MMM-yyyy HH:mm:ss') AS AppStartTime, FORMAT(BD.AppEndTime, 'dd-MMM-yyyy HH:mm:ss') AS AppEndTime,";
                strSql = strSql + " BD.BundleIDStatus AS BundleIDStatus, BD.CreatedBy AS CreatedBy, FORMAT(BD.CreatedOn, 'dd-MMM-yyyy HH:mm:ss') AS CreatedOn,";
                strSql = strSql + " BD.ModifiedBy AS ModifiedBy, FORMAT(BD.ModifiedOn, 'dd-MMM-yyyy HH:mm:ss') AS ModifiedOn";
                strSql = strSql + " FROM BundleCompile AS BC";
                strSql = strSql + " INNER JOIN OrderMaster AS OM";
                strSql = strSql + " ON BC.OrderNo = OM.OrderNo";
                strSql = strSql + " INNER JOIN BundleCompileDetail AS BD";
                strSql = strSql + " ON BD.BundleID = BC.BundleID";
                strSql = strSql + " WHERE 1=1";

                if (!String.IsNullOrWhiteSpace(objReq.OrderNo))
                {
                    strSql = strSql + " AND OM.OrderNo = '" + objReq.OrderNo + "'";
                }
                if (objReq.OperationNo > 0)
                {
                    strSql = strSql + " AND BD.OperationNo = " + objReq.OperationNo;
                }
                if (objReq.BundleID > 0)
                {
                    strSql = strSql + " AND BC.BundleID = " + objReq.BundleID;
                }
                if (!String.IsNullOrWhiteSpace(objReq.BundleIDStatus))
                {
                    strSql = strSql + " AND BC.BundleIDStatus = '" + objReq.BundleIDStatus + "'";
                }

                //strSql = strSql + " ORDER BY BC.BundleID ASC";

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
                        obj.BundleID = Convert.ToInt64(ds.Tables[0].Rows[i]["BundleID"]);
                        obj.OperationNo = Convert.ToInt64(ds.Tables[0].Rows[i]["OperationNo"]);
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
                        
                        if (ds.Tables[0].Rows[i]["SupervisorID"] == DBNull.Value)
                        {
                            obj.SupervisorID = 0;
                        }
                        else
                        {
                            obj.SupervisorID = Convert.ToInt32(ds.Tables[0].Rows[i]["SupervisorID"]);
                        }

                        if (ds.Tables[0].Rows[i]["SupervisorAssignedDate"] == null)
                        {
                            obj.SupervisorAssignedDate = string.Empty;
                        }
                        else
                        {
                            obj.SupervisorAssignedDate = Convert.ToString(ds.Tables[0].Rows[i]["SupervisorAssignedDate"]);
                        }

                        if (ds.Tables[0].Rows[i]["AppEmpID"] == DBNull.Value)
                        {
                            obj.AppEmpID = 0;
                        }
                        else
                        {
                            obj.AppEmpID = Convert.ToInt32(ds.Tables[0].Rows[i]["AppEmpID"]);
                        }

                        if (ds.Tables[0].Rows[i]["AppStartTime"] == null)
                        {
                            obj.AppStartTime = string.Empty;
                        }
                        else
                        {
                            obj.AppStartTime = Convert.ToString(ds.Tables[0].Rows[i]["AppStartTime"]);
                        }

                        if (ds.Tables[0].Rows[i]["AppEndTime"] == null)
                        {
                            obj.AppEndTime = string.Empty;
                        }
                        else
                        {
                            obj.AppEndTime = Convert.ToString(ds.Tables[0].Rows[i]["AppEndTime"]);
                        }

                        if (ds.Tables[0].Rows[i]["BundleIDStatus"] == null)
                        {
                            obj.BundleIDStatus = string.Empty;
                        }
                        else
                        {
                            obj.BundleIDStatus = Convert.ToString(ds.Tables[0].Rows[i]["BundleIDStatus"]);
                        }

                        obj.CreatedBy = Convert.ToInt32(ds.Tables[0].Rows[i]["CreatedBy"]);
                        obj.CreatedOn = Convert.ToString(ds.Tables[0].Rows[i]["CreatedOn"]);

                        if (ds.Tables[0].Rows[i]["ModifiedBy"] == DBNull.Value)
                        {
                            obj.ModifiedBy = 0;
                        }
                        else
                        {
                            obj.ModifiedBy = Convert.ToInt32(ds.Tables[0].Rows[i]["ModifiedBy"]);
                        }

                        if (ds.Tables[0].Rows[i]["ModifiedOn"] == null)
                        {
                            obj.ModifiedOn = string.Empty;
                        }
                        else
                        {
                            obj.ModifiedOn = Convert.ToString(ds.Tables[0].Rows[i]["ModifiedOn"]);
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
                strSql = strSql + " MachineStatus, Needle, Oiling, CreatedBy, FORMAT(CreatedOn, 'dd-MMM-yyyy HH:mm:ss') AS CreatedOn,";
                strSql = strSql + " ModifiedBy, FORMAT(ModifiedOn, 'dd-MMM-yyyy HH:mm:ss') AS ModifiedOn";
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

                        if (ds.Tables[0].Rows[i]["ModifiedBy"] == DBNull.Value)
                        {
                            obj.ModifiedBy = 0;
                        }
                        else
                        {
                            obj.ModifiedBy = Convert.ToInt32(ds.Tables[0].Rows[i]["ModifiedBy"]);
                        }

                        if (ds.Tables[0].Rows[i]["ModifiedOn"] == null)
                        {
                            obj.ModifiedOn = string.Empty;
                        }
                        else
                        {
                            obj.ModifiedOn = Convert.ToString(ds.Tables[0].Rows[i]["ModifiedOn"]);
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
                //else if (objReq.OrderNo == null || objReq.OrderNo == "")
                //{
                //    objResp.vErrorMsg = "Please Pass the Valid Order No";
                //    objResp.vErrorCode = 300;
                //}
                else if (objReq.BundleID == null || objReq.BundleID == 0)
                {
                    objResp.vErrorMsg = "Please Pass the Valid Bundle ID";
                    objResp.vErrorCode = 300;
                }
                //else if (objReq.OperationNo == null || objReq.OperationNo == 0)
                //{
                //    objResp.vErrorMsg = "Please Pass the Valid Operation Number.";
                //    objResp.vErrorCode = 300;
                //}
                else if (string.IsNullOrWhiteSpace(objReq.OperationNos))
                {
                    objResp.vErrorMsg = "Please Pass the Operation Numbers";
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
                    cmd.Parameters.AddWithValue("@SupervisorID", objReq.SupervisorID);
                    //cmd.Parameters.AddWithValue("@OrderNo", objReq.OrderNo);
                    cmd.Parameters.AddWithValue("@BundleID", objReq.BundleID);
                    //cmd.Parameters.AddWithValue("@OperationNo", objReq.OperationNo);
                    cmd.Parameters.AddWithValue("@OperationNos", objReq.OperationNos);
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
                    //else if (objReq.OrderNo == null || objReq.OrderNo == "")
                    //{
                    //    objResp.vErrorMsg = "Please Pass the Valid Order No";
                    //    objResp.vErrorCode = 300;
                    //}
                    else if (objReq.BundleID == null || objReq.BundleID == 0)
                    {
                        objResp.vErrorMsg = "Please Pass the Valid Bundle ID";
                        objResp.vErrorCode = 300;
                    }
                    //else if (objReq.OperationNo == null || objReq.OperationNo == 0)
                    //{
                    //    objResp.vErrorMsg = "Please Pass the Valid Operation Number";
                    //    objResp.vErrorCode = 300;
                    //}
                    else if (string.IsNullOrWhiteSpace(objReq.OperationNos))
                    {
                        objResp.vErrorMsg = "Please Pass the Valid Operation Number";
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
                        //cmd.Parameters.AddWithValue("@OrderNo", objReq.OrderNo);
                        cmd.Parameters.AddWithValue("@BundleID", objReq.BundleID);
                        //cmd.Parameters.AddWithValue("@OperationNo", objReq.OperationNo);
                        cmd.Parameters.AddWithValue("@OperationNos", objReq.OperationNos);
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
                    //objBundleCompile.OrderNo = objReq.OrderNo;
                    objBundleCompile.BundleID = objReq.BundleID;
                    //objBundleCompile.OperationNo = objReq.OperationNo;
                    objBundleCompile.OperationNos = objReq.OperationNos;
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
                else if (objReq.OperationNo == null || objReq.OperationNo == 0)
                {
                    objResp.vErrorMsg = "Please Pass the Valid Operation Number";
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
                    cmd.Parameters.AddWithValue("@OperationNo", objReq.OperationNo);
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

                string strSql = "SELECT LM.LineId AS LineId, LM.LineName AS LineName,";
                strSql = strSql + " LM.LineStatus AS LineStatus, COUNT(ED.Code) AS OperatorCount,";
                strSql = strSql + " LM.SeqNo AS SeqNo, LM.LineCode AS LineCode, LM.SuperVisor AS SuperVisor,";
                strSql = strSql + " LM.SuperMarketCode AS SuperMarketCode, LM.SectionName AS SectionName, LM.DivisionID AS DivisionID,";
                strSql = strSql + " FORMAT(LM.CreatedOn, 'dd-MMM-yyyy HH:mm:ss') AS CreatedOn, LM.CreatedBy,";
                strSql = strSql + " FORMAT(LM.ModifiedOn, 'dd-MMM-yyyy HH:mm:ss') AS ModifiedOn, LM.ModifiedBy";
                strSql = strSql + " FROM EmployeeMaster AS EM";
                strSql = strSql + " INNER JOIN EmployeeDetail AS ED";
                strSql = strSql + " ON EM.EmpId = ED.Code";
                strSql = strSql + " INNER JOIN LineMaster AS LM";
                strSql = strSql + " ON LM.LineName = ED.LineName";
                strSql = strSql + " WHERE LM.LineStatus = 'Active' AND EM.IsActive = 1 AND EM.EmpRole = 'Operator'";
                strSql = strSql + " GROUP BY LM.LineId, LM.LineName, LM.LineStatus,";
                strSql = strSql + " LM.SeqNo, LM.LineCode, LM.SuperVisor, LM.SuperMarketCode, LM.SectionName, LM.DivisionID,";
                strSql = strSql + " LM.CreatedOn, LM.CreatedBy, LM.ModifiedOn, LM.ModifiedBy";
                strSql = strSql + " ORDER BY LM.LineId";

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

                        if (ds.Tables[0].Rows[i]["CreatedOn"] == null)
                        {
                            obj.CreatedOn = string.Empty;
                        }
                        else
                        {
                            obj.CreatedOn = Convert.ToString(ds.Tables[0].Rows[i]["CreatedOn"]);
                        }

                        if (ds.Tables[0].Rows[i]["CreatedBy"] == DBNull.Value)
                        {
                            obj.CreatedBy = 0;
                        }
                        else
                        {
                            obj.CreatedBy = Convert.ToInt32(ds.Tables[0].Rows[i]["CreatedBy"]);
                        }

                        if (ds.Tables[0].Rows[i]["ModifiedOn"] == null)
                        {
                            obj.ModifiedOn = string.Empty;
                        }
                        else
                        {
                            obj.ModifiedOn = Convert.ToString(ds.Tables[0].Rows[i]["ModifiedOn"]);
                        }

                        if (ds.Tables[0].Rows[i]["ModifiedBy"] == DBNull.Value)
                        {
                            obj.ModifiedBy = 0;
                        }
                        else
                        {
                            obj.ModifiedBy = Convert.ToInt32(ds.Tables[0].Rows[i]["ModifiedBy"]);
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
                //else if (objReq.OrderNo == null || objReq.OrderNo == "")
                //{
                //    objResp.vErrorMsg = "Please Pass the Valid Order No";
                //    objResp.vErrorCode = 300;
                //}
                else if (objReq.BundleID == null || objReq.BundleID == 0)
                {
                    objResp.vErrorMsg = "Please Pass the Valid Bundle ID";
                    objResp.vErrorCode = 300;
                }
                //else if (objReq.OperationNo == null || objReq.OperationNo == 0)
                //{
                //    objResp.vErrorMsg = "Please Pass the Valid Operation Number";
                //    objResp.vErrorCode = 300;
                //}
                else if (string.IsNullOrWhiteSpace(objReq.OperationNos))
                {
                    objResp.vErrorMsg = "Please Pass the Valid Operation Number";
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
                    //cmd.Parameters.AddWithValue("@OrderNo", objReq.OrderNo);
                    cmd.Parameters.AddWithValue("@BundleID", objReq.BundleID);
                    //cmd.Parameters.AddWithValue("@OperationNo", objReq.OperationNo);
                    cmd.Parameters.AddWithValue("@OperationNos", objReq.OperationNos);
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
                            objResp.vErrorMsg = "Supervisor needs to assigned the Bundle ID to Operator/Worker/Employee";
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



        public List<clsLine> Fn_Get_ActiveLineWiseOrderNoDetails(clsLine objReq)
        {
            var objResp = new List<clsLine>();
            var obj = new clsLine();
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                //string strSql = "SELECT OM.Qty AS TotalQty, BC.Qty AS FinishedQty,";
                //strSql = strSql + " ED.LineName AS LineName, ED.EmpName AS EmpName, BC.OrderNo AS OrderNo";
                //strSql = strSql + " FROM BundleCompile AS BC";
                //strSql = strSql + " INNER JOIN EmployeeDetail AS ED";
                //strSql = strSql + " ON ED.Code = BC.AppEmpID";
                //strSql = strSql + " INNER JOIN OrderMaster AS OM";
                //strSql = strSql + " ON OM.OrderNo = BC.OrderNo AND OM.OrderNo = '" + objReq.OrderNo + "'";

                string strSql = "SELECT LM.LineId AS LineId, ED.LineName AS LineName,";
                strSql = strSql + " LM.LineStatus AS LineStatus, BC.OrderNo AS OrderNo,";
                strSql = strSql + " BC.AppEmpID AS AppEmpID, ED.EmpName AS AppEmpName,";
                strSql = strSql + " BC.BundleID AS BundleID, BC.BundleIDStatus AS BundleIDStatus";
                strSql = strSql + " FROM BundleCompile AS BC";
                strSql = strSql + " INNER JOIN EmployeeDetail AS ED";
                strSql = strSql + " ON BC.AppEmpID = ED.Code";
                strSql = strSql + " INNER JOIN LineMaster AS LM";
                strSql = strSql + " ON LM.LineName = ED.LineName";
                strSql = strSql + " INNER JOIN OrderMaster AS OM";
                strSql = strSql + " ON OM.OrderNo = BC.OrderNo";
                strSql = strSql + " WHERE 1=1";

                if (objReq.LineId > 0)
                {
                    strSql = strSql + " AND LM.LineId = " + objReq.LineId;
                }
                if (!String.IsNullOrWhiteSpace(objReq.OrderNo))
                {
                    strSql = strSql + " AND BC.OrderNo = '" + objReq.OrderNo + "'";
                }

                strSql = strSql + " GROUP BY LM.LineId, ED.LineName, LM.LineStatus, BC.OrderNo,";
                strSql = strSql + " BC.AppEmpID, ED.EmpName, BC.BundleID, BC.BundleIDStatus";

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
                        //obj.Qty = Convert.ToInt32(ds.Tables[0].Rows[i]["TotalQty"]);
                        //obj.FinishedQty = Convert.ToInt32(ds.Tables[0].Rows[i]["FinishedQty"]);
                        obj.LineId = Convert.ToInt64(ds.Tables[0].Rows[i]["LineId"]);
                        obj.LineName = Convert.ToString(ds.Tables[0].Rows[i]["LineName"]);
                        obj.LineStatus = Convert.ToString(ds.Tables[0].Rows[i]["LineStatus"]);
                        obj.OrderNo = Convert.ToString(ds.Tables[0].Rows[i]["OrderNo"]);
                        obj.AppEmpID = Convert.ToInt32(ds.Tables[0].Rows[i]["AppEmpID"]);
                        obj.AppEmpName = Convert.ToString(ds.Tables[0].Rows[i]["AppEmpName"]);
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
                    obj.vErrorMsg = "Line ID/Name wise Order No details are not found.";
                    objResp.Add(obj);
                }                
            }
            catch (Exception exp)
            {
                obj.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Get_ActiveLineWiseOrderNoDetails", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }


        public List<clsLine> Fn_Get_All_OrderNoLineIDWise(clsLine objReq)
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
                strSql = strSql + " LM.LineStatus AS LineStatus, BC.OrderNo AS OrderNo";
                strSql = strSql + " FROM BundleCompile AS BC";
                strSql = strSql + " INNER JOIN EmployeeDetail AS ED";
                strSql = strSql + " ON BC.AppEmpID = ED.Code";
                strSql = strSql + " INNER JOIN LineMaster AS LM";
                strSql = strSql + " ON LM.LineName = ED.LineName";
                strSql = strSql + " INNER JOIN OrderMaster AS OM";
                strSql = strSql + " ON OM.OrderNo = BC.OrderNo";
                strSql = strSql + " WHERE LM.LineId = " + objReq.LineId;
                strSql = strSql + " GROUP BY LM.LineId, ED.LineName, LM.LineStatus, BC.OrderNo";

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
                        obj.OrderNo = Convert.ToString(ds.Tables[0].Rows[i]["OrderNo"]);
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
                    obj.vErrorMsg = "Line ID wise Order No Records are not found.";
                    objResp.Add(obj);
                }
            }
            catch (Exception exp)
            {
                obj.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Get_All_OrderNoLineIDWise", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }



        public List<clsBundleCompile> Fn_Fetch_OperatorIDWiseBundleIDDetails(clsBundleCompile objReq)
        {
            var objResp = new List<clsBundleCompile>();
            var obj = new clsBundleCompile();
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT LM.LineId AS LineId, ED.LineName AS LineName, LM.LineStatus AS LineStatus, BC.OrderNo AS OrderNo,";
                strSql = strSql + " BC.BundleID AS BundleID, BC.StyleCode AS StyleCode, BD.BundleIDStatus AS BundleIDStatus, BC.Qty AS Qty,";
                strSql = strSql + " BD.AppEmpID AS AppEmpID, BC.BundleNo AS BundleNo, BC.SizeName AS SizeName, BC.ColorName AS ColorName,";
                strSql = strSql + " BC.ShadeName AS ShadeName, BC.PlyFrom AS PlyFrom, BC.PlyTo AS PlyTo, BC.LotNo AS LotNo,";
                strSql = strSql + " BC.SubSection AS SubSection, BC.LayID AS LayID, BD.OperationNo AS OperationNo";
                strSql = strSql + " FROM BundleCompile AS BC";
                strSql = strSql + " INNER JOIN BundleCompileDetail AS BD";
                strSql = strSql + " ON BC.BundleID = BD.BundleID";
                strSql = strSql + " INNER JOIN OrderMaster AS OM";
                strSql = strSql + " ON OM.OrderNo = BC.OrderNo";
                strSql = strSql + " INNER JOIN EmployeeDetail AS ED";
                strSql = strSql + " ON BD.AppEmpID = ED.Code";
                strSql = strSql + " INNER JOIN LineMaster AS LM";
                strSql = strSql + " ON LM.LineName = ED.LineName";
                strSql = strSql + " WHERE 1=1";

                if (objReq.AppEmpID > 0)
                {
                    strSql = strSql + " AND BD.AppEmpID = " + objReq.AppEmpID;
                }
                if (objReq.BundleIDStatus == null || objReq.BundleIDStatus == "")
                {
                    strSql = strSql + " AND BD.BundleIDStatus = 'Assigned'";
                }
                else
                {
                    strSql = strSql + " AND BD.BundleIDStatus = '" + objReq.BundleIDStatus + "'";
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
                        obj = new clsBundleCompile();
                        obj.LineId = Convert.ToInt64(ds.Tables[0].Rows[i]["LineId"]);
                        obj.LineName = Convert.ToString(ds.Tables[0].Rows[i]["LineName"]);
                        obj.LineStatus = Convert.ToString(ds.Tables[0].Rows[i]["LineStatus"]);
                        obj.OrderNo = Convert.ToString(ds.Tables[0].Rows[i]["OrderNo"]);
                        obj.BundleID = Convert.ToInt64(ds.Tables[0].Rows[i]["BundleID"]);
                        obj.StyleCode = Convert.ToString(ds.Tables[0].Rows[i]["StyleCode"]);
                        obj.BundleIDStatus = Convert.ToString(ds.Tables[0].Rows[i]["BundleIDStatus"]);
                        obj.Qty = Convert.ToInt32(ds.Tables[0].Rows[i]["Qty"]);
                        obj.AppEmpID = Convert.ToInt32(ds.Tables[0].Rows[i]["AppEmpID"]);
                        obj.BundleNo = Convert.ToInt32(ds.Tables[0].Rows[i]["BundleNo"]);
                        obj.SizeName = Convert.ToString(ds.Tables[0].Rows[i]["SizeName"]);
                        obj.ColorName = Convert.ToString(ds.Tables[0].Rows[i]["ColorName"]);
                        obj.ShadeName = Convert.ToString(ds.Tables[0].Rows[i]["ShadeName"]);
                        obj.PlyFrom = Convert.ToInt32(ds.Tables[0].Rows[i]["PlyFrom"]);
                        obj.PlyTo = Convert.ToInt32(ds.Tables[0].Rows[i]["PlyTo"]);
                        obj.LotNo = Convert.ToInt32(ds.Tables[0].Rows[i]["LotNo"]);
                        obj.SubSection = Convert.ToString(ds.Tables[0].Rows[i]["SubSection"]);
                        obj.LayID = Convert.ToInt64(ds.Tables[0].Rows[i]["LayID"]);
                        obj.OperationNo = Convert.ToInt64(ds.Tables[0].Rows[i]["OperationNo"]);

                        obj.vErrorCode = 200;
                        obj.vErrorMsg = "Success";
                        objResp.Add(obj);
                        i++;
                    }
                }
                else
                {
                    obj.vErrorCode = 404;
                    obj.vErrorMsg = "Operator/Worker ID Bundle ID details records are not found.";
                    objResp.Add(obj);
                }
            }
            catch (Exception exp)
            {
                obj.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Fetch_OperatorIDWiseBundleIDDetails", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }


        public List<clsOrderMaster> Fn_Fetch_All_OrderNumbers(clsOrderMaster objReq)
        {
            var objResp = new List<clsOrderMaster>();
            var obj = new clsOrderMaster();
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT OrderNo, Qty, BundleQty, FORMAT(OrderDate, 'dd-MMM-yyyy HH:mm:ss') AS OrderDate,";
                strSql = strSql + " StyleCode, FORMAT(CreatedOn, 'dd-MMM-yyyy HH:mm:ss') AS CreatedOn, CreatedBy,";
                strSql = strSql + " FORMAT(ModifiedOn, 'dd-MMM-yyyy HH:mm:ss') AS ModifiedOn, ModifiedBy, OrderStatus";
                strSql = strSql + " FROM OrderMaster WHERE 1=1";

                if (!String.IsNullOrWhiteSpace(objReq.OrderNo))
                {
                    strSql = strSql + " AND OrderNo = '" + objReq.OrderNo + "'";
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
                        obj = new clsOrderMaster();
                        obj.OrderNo = Convert.ToString(ds.Tables[0].Rows[i]["OrderNo"]);
                        obj.Qty = Convert.ToInt32(ds.Tables[0].Rows[i]["Qty"]);
                        obj.BundleQty = Convert.ToInt32(ds.Tables[0].Rows[i]["BundleQty"]);
                        obj.OrderDate = Convert.ToString(ds.Tables[0].Rows[i]["OrderDate"]);
                        obj.StyleCode = Convert.ToString(ds.Tables[0].Rows[i]["StyleCode"]);
                        obj.CreatedOn = Convert.ToString(ds.Tables[0].Rows[i]["CreatedOn"]);
                        obj.CreatedBy = Convert.ToInt32(ds.Tables[0].Rows[i]["CreatedBy"]);

                        if (ds.Tables[0].Rows[i]["ModifiedOn"] == null)
                        {
                            obj.ModifiedOn = string.Empty;
                        }
                        else
                        {
                            obj.ModifiedOn = Convert.ToString(ds.Tables[0].Rows[i]["ModifiedOn"]);
                        }

                        if (ds.Tables[0].Rows[i]["ModifiedBy"] == DBNull.Value)
                        {
                            obj.ModifiedBy = 0;
                        }
                        else
                        {
                            obj.ModifiedBy = Convert.ToInt32(ds.Tables[0].Rows[i]["ModifiedBy"]);
                        }

                        obj.OrderStatus = Convert.ToString(ds.Tables[0].Rows[i]["OrderStatus"]);

                        obj.vErrorCode = 200;
                        obj.vErrorMsg = "Success";
                        objResp.Add(obj);
                        i++;
                    }
                }
                else
                {
                    obj.vErrorCode = 404;
                    obj.vErrorMsg = "Order Number records are not found.";
                    objResp.Add(obj);
                }
            }
            catch (Exception exp)
            {
                obj.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Fetch_All_OrderNumbers", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }


        public List<clsBundleCompile> Fn_Fetch_AssignedTenBundleDetails(clsBundleCompile objReq)
        {
            var objResp = new List<clsBundleCompile>();
            var obj = new clsBundleCompile();
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT TOP 10 BCD.BundleID AS BundleID, BC.BundleNo AS BundleNo, BCD.OperationNo AS OperationNo, BC.SizeName AS SizeName,";
                strSql = strSql + " BC.ColorName AS ColorName, BC.Qty AS Qty, BC.PlyFrom AS PlyFrom, BC.PlyTo AS PlyTo, BC.LotNo AS LotNo,";
                strSql = strSql + " BC.SubSection AS SubSection, BC.StyleCode AS StyleCode, OM.OrderNo AS OrderNo,";
                strSql = strSql + " BC.LayID AS LayID, BC.BundleIDStatus AS BundleIDStatus, BC.UpdateType AS UpdateType";
                strSql = strSql + " FROM OrderMaster AS OM";
                strSql = strSql + " INNER JOIN BundleCompile AS BC";
                strSql = strSql + " ON OM.OrderNo = BC.OrderNo";
                strSql = strSql + " INNER JOIN BundleCompileDetail AS BCD";
                strSql = strSql + " ON BC.BundleID = BCD.BundleID";
                strSql = strSql + " WHERE BCD.BundleIDStatus = 'Assigned' AND BCD.AppEmpID = " + objReq.AppEmpID;
                strSql = strSql + " ORDER BY BCD.OperationNo DESC";
                //strSql = strSql + " ORDER BY BC.BundleID DESC";

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
                        obj.BundleID = Convert.ToInt64(ds.Tables[0].Rows[i]["BundleID"]);
                        obj.BundleNo = Convert.ToInt32(ds.Tables[0].Rows[i]["BundleNo"]);
                        obj.OperationNo = Convert.ToInt32(ds.Tables[0].Rows[i]["OperationNo"]);
                        obj.SizeName = Convert.ToString(ds.Tables[0].Rows[i]["SizeName"]);
                        obj.ColorName = Convert.ToString(ds.Tables[0].Rows[i]["ColorName"]);
                        obj.Qty = Convert.ToInt32(ds.Tables[0].Rows[i]["Qty"]);
                        obj.PlyFrom = Convert.ToInt32(ds.Tables[0].Rows[i]["PlyFrom"]);
                        obj.PlyTo = Convert.ToInt32(ds.Tables[0].Rows[i]["PlyTo"]);
                        obj.LotNo = Convert.ToInt32(ds.Tables[0].Rows[i]["LotNo"]);
                        obj.SubSection = Convert.ToString(ds.Tables[0].Rows[i]["SubSection"]);
                        obj.StyleCode = Convert.ToString(ds.Tables[0].Rows[i]["StyleCode"]);
                        obj.OrderNo = Convert.ToString(ds.Tables[0].Rows[i]["OrderNo"]);
                        obj.LayID = Convert.ToInt64(ds.Tables[0].Rows[i]["LayID"]);
                        obj.BundleIDStatus = Convert.ToString(ds.Tables[0].Rows[i]["BundleIDStatus"]);
                        obj.UpdateType = Convert.ToString(ds.Tables[0].Rows[i]["UpdateType"]);

                        obj.vErrorCode = 200;
                        obj.vErrorMsg = "Success";
                        objResp.Add(obj);
                        i++;
                    }
                }
                else
                {
                    obj.vErrorCode = 404;
                    obj.vErrorMsg = "Bundles ID assigned records are not found.";
                    objResp.Add(obj);
                }
            }
            catch (Exception exp)
            {
                obj.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Fetch_AssignedTenBundleDetails", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }


        public List<clsBundleCompile> Fn_Fetch_FinishedTenBundleDetails(clsBundleCompile objReq)
        {
            var objResp = new List<clsBundleCompile>();
            var obj = new clsBundleCompile();
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT TOP 10 BCD.BundleID AS BundleID, BC.BundleNo AS BundleNo, BCD.OperationNo AS OperationNo,";
                strSql = strSql + " OBM.SubSection AS SubSection, OBM.OperationName AS OperationName, OBM.SubProduct AS SubProduct, BC.SizeName AS SizeName,";
                strSql = strSql + " BC.ColorName AS ColorName, BC.Qty AS Qty, BC.PlyFrom AS PlyFrom, BC.PlyTo AS PlyTo, BC.LotNo AS LotNo,";
                strSql = strSql + " BC.SubSection AS SubSection, BC.StyleCode AS StyleCode, OM.OrderNo AS OrderNo,";
                strSql = strSql + " BC.LayID AS LayID, BCD.BundleIDStatus AS BundleIDStatus, BC.UpdateType AS UpdateType";
                strSql = strSql + " FROM OrderMaster AS OM";
                strSql = strSql + " INNER JOIN BundleCompile AS BC";
                strSql = strSql + " ON OM.OrderNo = BC.OrderNo";
                strSql = strSql + " INNER JOIN BundleCompileDetail AS BCD";
                strSql = strSql + " ON BC.BundleID = BCD.BundleID";
                strSql = strSql + " INNER JOIN OBMainMasterNew AS OBM";
                strSql = strSql + " ON OBM.OperationNo = BCD.OperationNo";
                strSql = strSql + " WHERE BC.BundleIDStatus = 'Finished' AND BCD.AppEmpID = " + objReq.AppEmpID;
                strSql = strSql + " ORDER BY BCD.OperationNo DESC";

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
                        obj.BundleID = Convert.ToInt64(ds.Tables[0].Rows[i]["BundleID"]);
                        obj.BundleNo = Convert.ToInt32(ds.Tables[0].Rows[i]["BundleNo"]);
                        obj.OperationNo = Convert.ToInt32(ds.Tables[0].Rows[i]["OperationNo"]);
                        obj.SizeName = Convert.ToString(ds.Tables[0].Rows[i]["SizeName"]);
                        obj.ColorName = Convert.ToString(ds.Tables[0].Rows[i]["ColorName"]);
                        obj.Qty = Convert.ToInt32(ds.Tables[0].Rows[i]["Qty"]);
                        obj.PlyFrom = Convert.ToInt32(ds.Tables[0].Rows[i]["PlyFrom"]);
                        obj.PlyTo = Convert.ToInt32(ds.Tables[0].Rows[i]["PlyTo"]);
                        obj.LotNo = Convert.ToInt32(ds.Tables[0].Rows[i]["LotNo"]);
                        obj.SubSection = Convert.ToString(ds.Tables[0].Rows[i]["SubSection"]);
                        obj.StyleCode = Convert.ToString(ds.Tables[0].Rows[i]["StyleCode"]);
                        obj.OrderNo = Convert.ToString(ds.Tables[0].Rows[i]["OrderNo"]);
                        obj.LayID = Convert.ToInt64(ds.Tables[0].Rows[i]["LayID"]);
                        obj.BundleIDStatus = Convert.ToString(ds.Tables[0].Rows[i]["BundleIDStatus"]);
                        obj.UpdateType = Convert.ToString(ds.Tables[0].Rows[i]["UpdateType"]);

                        obj.vErrorCode = 200;
                        obj.vErrorMsg = "Success";
                        objResp.Add(obj);
                        i++;
                    }
                }
                else
                {
                    obj.vErrorCode = 400;
                    obj.vErrorMsg = "Bundles ID finished records are not found.";
                    objResp.Add(obj);
                }
            }
            catch (Exception exp)
            {
                obj.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Fetch_FinishedTenBundleDetails", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }


        public List<clsBundleCompile> Fn_Fetch_OperationNumberWiseDetails(clsBundleCompile objReq)
        {
            var objResp = new List<clsBundleCompile>();
            var obj = new clsBundleCompile();
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT LM.LineId AS LineId, ED.LineName AS LineName, BC.OrderNo AS OrderNo,";
                strSql = strSql + " BD.BundleID AS BundleID, BC.SupervisorID AS SupervisorID, BD.BundleIDStatus AS BundleIDStatus,";
                strSql = strSql + " BD.AppEmpID AS AppEmpID, BD.OperationNo AS OperationNo";
                strSql = strSql + " FROM BundleCompile AS BC";
                strSql = strSql + " LEFT JOIN BundleCompileDetail AS BD";
                strSql = strSql + " ON BC.BundleID = BD.BundleID";
                strSql = strSql + " LEFT JOIN OrderMaster AS OM";
                strSql = strSql + " ON OM.OrderNo = BC.OrderNo";
                strSql = strSql + " LEFT JOIN EmployeeDetail AS ED";
                strSql = strSql + " ON BD.AppEmpID = ED.Code";
                strSql = strSql + " LEFT JOIN LineMaster AS LM";
                strSql = strSql + " ON LM.LineName = ED.LineName";
                strSql = strSql + " WHERE 1=1";

                if (objReq.OperationNo > 0)
                {
                    strSql = strSql + " AND BD.OperationNo = " + objReq.OperationNo;
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
                        obj = new clsBundleCompile();
                        if (ds.Tables[0].Rows[i]["LineId"] == DBNull.Value)
                        {
                            obj.LineId = 0;
                        }
                        else
                        {
                            obj.LineId = Convert.ToInt64(ds.Tables[0].Rows[i]["LineId"]);
                        }

                        if (ds.Tables[0].Rows[i]["LineName"] == null)
                        {
                            obj.LineName = string.Empty;
                        }
                        else
                        {
                            obj.LineName = Convert.ToString(ds.Tables[0].Rows[i]["LineName"]);
                        }

                        obj.OrderNo = Convert.ToString(ds.Tables[0].Rows[i]["OrderNo"]);
                        obj.BundleID = Convert.ToInt64(ds.Tables[0].Rows[i]["BundleID"]);

                        if (ds.Tables[0].Rows[i]["SupervisorID"] == DBNull.Value)
                        {
                            obj.SupervisorID = 0;
                        }
                        else
                        {
                            obj.SupervisorID = Convert.ToInt32(ds.Tables[0].Rows[i]["SupervisorID"]);
                        }

                        if (ds.Tables[0].Rows[i]["BundleIDStatus"] == null)
                        {
                            obj.BundleIDStatus = string.Empty;
                        }
                        else
                        {
                            obj.BundleIDStatus = Convert.ToString(ds.Tables[0].Rows[i]["BundleIDStatus"]);
                        }

                        if (ds.Tables[0].Rows[i]["AppEmpID"] == DBNull.Value)
                        {
                            obj.AppEmpID = 0;
                        }
                        else
                        {
                            obj.AppEmpID = Convert.ToInt32(ds.Tables[0].Rows[i]["AppEmpID"]);
                        }
                        
                        obj.OperationNo = Convert.ToInt64(ds.Tables[0].Rows[i]["OperationNo"]);

                        obj.vErrorCode = 200;
                        obj.vErrorMsg = "Success";
                        objResp.Add(obj);
                        i++;
                    }
                }
                else
                {
                    obj.vErrorCode = 400;
                    obj.vErrorMsg = "Operation Number details records are not found";
                    objResp.Add(obj);
                }
            }
            catch (Exception exp)
            {
                obj.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Fetch_OperationNumberWiseDetails", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }


        public List<clsBundleCompile> Fn_Fetch_TotalEarningDetails(clsBundleCompile objReq)
        {
            var objResp = new List<clsBundleCompile>();
            var obj = new clsBundleCompile();
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "DECLARE @CurDate DATE = '" + objReq.CurrentDate + "'";
                strSql = strSql + " SELECT SUM(BD.StdRate*BC.Qty) AS Earnings, CONVERT(VARCHAR, @CurDate) AS Months,'Today' AS TimePeriod";
                strSql = strSql + " FROM BundleCompileDetail AS BD";
                strSql = strSql + " INNER JOIN BundleCompile AS BC";
                strSql = strSql + " ON BC.BundleID = BD.BundleID";
                strSql = strSql + " WHERE CONVERT(DATE, BD.CreatedOn) = CONVERT(DATE, @CurDate) AND BD.AppEmpId = " + objReq.AppEmpID;
                strSql = strSql + " UNION";
                strSql = strSql + " SELECT SUM(BD.StdRate*BC.Qty) AS Earnings, DATENAME(MONTH, @CurDate) AS Months,'Month' AS TimePeriod";
                strSql = strSql + " FROM BundleCompileDetail AS BD";
                strSql = strSql + " INNER JOIN BundleCompile AS BC";
                strSql = strSql + " ON BC.BundleID = BD.BundleID";
                strSql = strSql + " WHERE MONTH(BD.CreatedOn) = MONTH(@CurDate) AND BD.AppEmpId = " + objReq.AppEmpID;

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
                        if (ds.Tables[0].Rows[i]["Earnings"] == DBNull.Value)
                        {
                            obj.Earnings = 0;
                        }
                        else
                        {
                            obj.Earnings = Convert.ToDecimal(ds.Tables[0].Rows[i]["Earnings"]);
                        }
                        obj.Months = Convert.ToString(ds.Tables[0].Rows[i]["Months"]);
                        obj.TimePeriod = Convert.ToString(ds.Tables[0].Rows[i]["TimePeriod"]);

                        obj.vErrorCode = 200;
                        obj.vErrorMsg = "Success";
                        objResp.Add(obj);
                        i++;
                    }
                }
                else
                {
                    obj.vErrorCode = 400;
                    obj.vErrorMsg = "Total Earning records are not found";
                    objResp.Add(obj);
                }
            }
            catch (Exception exp)
            {
                obj.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Fetch_TotalEarningDetails", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }


        public List<clsBundleCompile> Fn_Fetch_TotalEarningDetailsByOpNo(clsBundleCompile objReq)
        {
            var objResp = new List<clsBundleCompile>();
            var obj = new clsBundleCompile();
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "DECLARE @CurrentDate DATE = '" + objReq.CurrentDate + "'";
                strSql = strSql + " SELECT BD.OperationNo AS OperationNo, BD.SubSection AS SubSection, BD.BundleID AS BundleID,";
                strSql = strSql + " BD.StdRate AS StdRate, BC.Qty AS Qty, (BD.StdRate*BC.Qty) AS TotalAmount, ED.LineName AS LineName,";
                strSql = strSql + " BC.StyleCode AS StyleCode, OB.Descriptions AS Descriptions";                  
                strSql = strSql + " FROM BundleCompileDetail AS BD";
                strSql = strSql + " INNER JOIN BundleCompile AS BC";
                strSql = strSql + " ON BC.BundleID = BD.BundleID";
                strSql = strSql + " INNER JOIN EmployeeDetail AS ED";
                strSql = strSql + " ON ED.Code = BD.AppEmpID";
                strSql = strSql + " INNER JOIN OperationBreackDownDetail AS OB";
                strSql = strSql + " ON OB.OpNo = BD.OperationNo";
                strSql = strSql + " INNER JOIN LineMaster AS LM";
                strSql = strSql + " ON LM.LineName = ED.LineName";
                strSql = strSql + " WHERE BD.AppEmpID = " + objReq.AppEmpID;
                strSql = strSql + " AND BD.CreatedOn >= @CurrentDate AND BD.CreatedOn < DATEADD(DAY, 1, @CurrentDate) AND LM.LineName = '" + objReq.LineName + "'";

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
                        obj.OperationNo = Convert.ToInt64(ds.Tables[0].Rows[i]["OperationNo"]);
                        obj.SubSection = Convert.ToString(ds.Tables[0].Rows[i]["SubSection"]);
                        obj.BundleID = Convert.ToInt64(ds.Tables[0].Rows[i]["BundleID"]);
                        obj.StdRate = Convert.ToDecimal(ds.Tables[0].Rows[i]["StdRate"]);
                        obj.Qty = Convert.ToInt32(ds.Tables[0].Rows[i]["Qty"]);
                        obj.TotalAmount = Convert.ToDecimal(ds.Tables[0].Rows[i]["TotalAmount"]);
                        obj.LineName = Convert.ToString(ds.Tables[0].Rows[i]["LineName"]);
                        obj.StyleCode = Convert.ToString(ds.Tables[0].Rows[i]["StyleCode"]);
                        obj.Descriptions = Convert.ToString(ds.Tables[0].Rows[i]["Descriptions"]);

                        obj.vErrorCode = 200;
                        obj.vErrorMsg = "Success";
                        objResp.Add(obj);
                        i++;
                    }
                }
                else
                {
                    obj.vErrorCode = 400;
                    obj.vErrorMsg = "Operation Number wise Earning records are not found";
                    objResp.Add(obj);
                }
            }
            catch (Exception exp)
            {
                obj.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Fetch_TotalEarningDetailsByOpNo", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
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