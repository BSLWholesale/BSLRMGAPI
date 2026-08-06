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
    public class MOBDALProduction
    {

        SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["BSL"].ConnectionString);

        Generic gn = new Generic();

        //public List<clsBundleCompile> Fn_Get_ActiveBundle(clsBundleCompile objReq)
        //{
        //    var objResp = new List<clsBundleCompile>();
        //    var obj = new clsBundleCompile();
        //    try
        //    {
        //        if (Con.State == ConnectionState.Broken)
        //        { Con.Close(); }
        //        if (Con.State == ConnectionState.Closed)
        //        { Con.Open(); }

        //        string strSql = "SELECT BD.BundleID AS BundleID, BD.OperationNo AS OperationNo, BC.LayID AS LayID,";
        //        strSql = strSql + " BC.BundleNo AS BundleNo, BC.SizeName AS SizeName, BC.ColorName AS ColorName, BC.ShadeName AS ShadeName,";
        //        strSql = strSql + " BC.Qty AS Qty, BC.PlyTo AS PlyTo, BC.PlyFrom AS PlyFrom, BC.LotNo AS LotNo, BD.SubSection AS SubSection,";
        //        strSql = strSql + " BC.Dispatch AS Dispatch, OM.StyleCode AS StyleCode, OM.OrderNo AS OrderNo, BC.SupervisorID AS SupervisorID,";
        //        strSql = strSql + " FORMAT(BC.SupervisorAssignedDate, 'dd-MMM-yyyy HH:mm:ss') AS SupervisorAssignedDate, BD.AppEmpID AS AppEmpID,";
        //        strSql = strSql + " FORMAT(BD.AppStartTime, 'dd-MMM-yyyy HH:mm:ss') AS AppStartTime, FORMAT(BD.AppEndTime, 'dd-MMM-yyyy HH:mm:ss') AS AppEndTime,";
        //        strSql = strSql + " BD.BundleIDStatus AS BundleIDStatus, BD.CreatedBy AS CreatedBy, FORMAT(BD.CreatedOn, 'dd-MMM-yyyy HH:mm:ss') AS CreatedOn,";
        //        strSql = strSql + " BD.ModifiedBy AS ModifiedBy, FORMAT(BD.ModifiedOn, 'dd-MMM-yyyy HH:mm:ss') AS ModifiedOn";
        //        strSql = strSql + " FROM BundleCompile AS BC";
        //        strSql = strSql + " INNER JOIN OrderMaster AS OM";
        //        strSql = strSql + " ON BC.OrderNo = OM.OrderNo";
        //        strSql = strSql + " INNER JOIN BundleCompileDetail AS BD";
        //        strSql = strSql + " ON BD.BundleID = BC.BundleID";
        //        strSql = strSql + " WHERE 1=1";

        //        if (!String.IsNullOrWhiteSpace(objReq.OrderNo))
        //        {
        //            strSql = strSql + " AND OM.OrderNo = '" + objReq.OrderNo + "'";
        //        }
        //        if (objReq.OperationNo > 0)
        //        {
        //            strSql = strSql + " AND BD.OperationNo = " + objReq.OperationNo;
        //        }
        //        if (objReq.BundleID > 0)
        //        {
        //            strSql = strSql + " AND BC.BundleID = " + objReq.BundleID;
        //        }
        //        if (!String.IsNullOrWhiteSpace(objReq.BundleIDStatus))
        //        {
        //            strSql = strSql + " AND BD.BundleIDStatus = '" + objReq.BundleIDStatus + "'";
        //        }

        //        //strSql = strSql + " ORDER BY BC.BundleID ASC";

        //        SqlCommand cmd = new SqlCommand(strSql, Con);
        //        cmd.CommandType = CommandType.Text;

        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataSet ds = new DataSet();
        //        da.Fill(ds);
        //        int i = 0;

        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            while (ds.Tables[0].Rows.Count > i)
        //            {
        //                obj = new clsBundleCompile();
        //                obj.BundleID = Convert.ToInt64(ds.Tables[0].Rows[i]["BundleID"]);
        //                obj.OperationNo = Convert.ToInt64(ds.Tables[0].Rows[i]["OperationNo"]);
        //                obj.LayID = Convert.ToInt64(ds.Tables[0].Rows[i]["LayID"]);
        //                obj.BundleNo = Convert.ToInt32(ds.Tables[0].Rows[i]["BundleNo"]);
        //                obj.SizeName = Convert.ToString(ds.Tables[0].Rows[i]["SizeName"]);
        //                obj.ColorName = Convert.ToString(ds.Tables[0].Rows[i]["ColorName"]);
        //                obj.ShadeName = Convert.ToString(ds.Tables[0].Rows[i]["ShadeName"]);
        //                obj.Qty = Convert.ToInt32(ds.Tables[0].Rows[i]["Qty"]);
        //                obj.PlyTo = Convert.ToInt32(ds.Tables[0].Rows[i]["PlyTo"]);
        //                obj.PlyFrom = Convert.ToInt32(ds.Tables[0].Rows[i]["PlyFrom"]);
        //                obj.LotNo = Convert.ToInt32(ds.Tables[0].Rows[i]["LotNo"]);
        //                obj.SubSection = Convert.ToString(ds.Tables[0].Rows[i]["SubSection"]);
        //                obj.IsDispatch = Convert.ToBoolean(ds.Tables[0].Rows[i]["Dispatch"]);
        //                obj.StyleCode = Convert.ToString(ds.Tables[0].Rows[i]["StyleCode"]);
        //                obj.OrderNo = Convert.ToString(ds.Tables[0].Rows[i]["OrderNo"]);

        //                if (ds.Tables[0].Rows[i]["SupervisorID"] == DBNull.Value)
        //                {
        //                    obj.SupervisorID = 0;
        //                }
        //                else
        //                {
        //                    obj.SupervisorID = Convert.ToInt32(ds.Tables[0].Rows[i]["SupervisorID"]);
        //                }

        //                if (ds.Tables[0].Rows[i]["SupervisorAssignedDate"] == null)
        //                {
        //                    obj.SupervisorAssignedDate = string.Empty;
        //                }
        //                else
        //                {
        //                    obj.SupervisorAssignedDate = Convert.ToString(ds.Tables[0].Rows[i]["SupervisorAssignedDate"]);
        //                }

        //                if (ds.Tables[0].Rows[i]["AppEmpID"] == DBNull.Value)
        //                {
        //                    obj.AppEmpID = 0;
        //                }
        //                else
        //                {
        //                    obj.AppEmpID = Convert.ToInt32(ds.Tables[0].Rows[i]["AppEmpID"]);
        //                }

        //                if (ds.Tables[0].Rows[i]["AppStartTime"] == null)
        //                {
        //                    obj.AppStartTime = string.Empty;
        //                }
        //                else
        //                {
        //                    obj.AppStartTime = Convert.ToString(ds.Tables[0].Rows[i]["AppStartTime"]);
        //                }

        //                if (ds.Tables[0].Rows[i]["AppEndTime"] == null)
        //                {
        //                    obj.AppEndTime = string.Empty;
        //                }
        //                else
        //                {
        //                    obj.AppEndTime = Convert.ToString(ds.Tables[0].Rows[i]["AppEndTime"]);
        //                }

        //                if (ds.Tables[0].Rows[i]["BundleIDStatus"] == null)
        //                {
        //                    obj.BundleIDStatus = string.Empty;
        //                }
        //                else
        //                {
        //                    obj.BundleIDStatus = Convert.ToString(ds.Tables[0].Rows[i]["BundleIDStatus"]);
        //                }

        //                obj.CreatedBy = Convert.ToInt32(ds.Tables[0].Rows[i]["CreatedBy"]);
        //                obj.CreatedOn = Convert.ToString(ds.Tables[0].Rows[i]["CreatedOn"]);

        //                if (ds.Tables[0].Rows[i]["ModifiedBy"] == DBNull.Value)
        //                {
        //                    obj.ModifiedBy = 0;
        //                }
        //                else
        //                {
        //                    obj.ModifiedBy = Convert.ToInt32(ds.Tables[0].Rows[i]["ModifiedBy"]);
        //                }

        //                if (ds.Tables[0].Rows[i]["ModifiedOn"] == null)
        //                {
        //                    obj.ModifiedOn = string.Empty;
        //                }
        //                else
        //                {
        //                    obj.ModifiedOn = Convert.ToString(ds.Tables[0].Rows[i]["ModifiedOn"]);
        //                }

        //                obj.vErrorCode = 200;
        //                obj.vErrorMsg = "Success";
        //                objResp.Add(obj);
        //                i++;
        //            }
        //        }
        //        else
        //        {
        //            obj.vErrorCode = 404;
        //            obj.vErrorMsg = "No Records are found.";
        //            objResp.Add(obj);
        //        }
        //    }
        //    catch (Exception exp)
        //    {
        //        obj.vErrorCode = 500;
        //        Logger.WriteLog("Function Name : Fn_Get_ActiveBundle", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
        //        obj.vErrorMsg = exp.Message.ToString();
        //        objResp.Add(obj);
        //    }
        //    finally
        //    {
        //        Con.Close();
        //    }
        //    return objResp;
        //}


        //public ApiResponse<clsBundleCompile> Fn_Get_ActiveBundle(clsBundleCompile objReq)
        //{
        //    var objResp = new List<clsBundleCompile>();
        //    var response = new ApiResponse<clsBundleCompile>();
        //    try
        //    {
        //        if (Con.State == ConnectionState.Broken)
        //        { Con.Close(); }
        //        if (Con.State == ConnectionState.Closed)
        //        { Con.Open(); }

        //        using (SqlCommand cmd = new SqlCommand("USP_MobileGetActiveBundle", Con))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;

        //            // ===== Parameters =====
        //            cmd.Parameters.AddWithValue("@OrderNo", string.IsNullOrWhiteSpace(objReq.OrderNo) ? (object)DBNull.Value : objReq.OrderNo);
        //            cmd.Parameters.AddWithValue("@PageNumber", objReq.PageNumber > 0 ? objReq.PageNumber : 1);
        //            cmd.Parameters.AddWithValue("@PageSize", objReq.PageSize > 0 ? objReq.PageSize : 10);
        //            cmd.Parameters.AddWithValue("@SortBy", string.IsNullOrWhiteSpace(objReq.SortBy) ? "BundleID" : objReq.SortBy);
        //            cmd.Parameters.AddWithValue("@SortDirection", string.IsNullOrWhiteSpace(objReq.SortDirection) ? "ASC" : objReq.SortDirection.ToUpper());

        //            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
        //            {
        //                DataSet ds = new DataSet();
        //                da.Fill(ds);

        //                // ================= Data Fetching =================
        //                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //                {
        //                    foreach (DataRow row in ds.Tables[0].Rows)
        //                    {
        //                        var obj = new clsBundleCompile
        //                        {
        //                            BundleID = Convert.ToInt64(row["BundleID"]),
        //                            OperationNo = Convert.ToInt64(row["OperationNo"]),
        //                            LayID = Convert.ToInt64(row["LayID"]),
        //                            BundleNo = Convert.ToInt32(row["BundleNo"]),
        //                            SizeName = Convert.ToString(row["SizeName"]),
        //                            ColorName = Convert.ToString(row["ColorName"]),
        //                            ShadeName = Convert.ToString(row["ShadeName"]),
        //                            Qty = Convert.ToInt32(row["Qty"]),
        //                            PlyTo = Convert.ToInt32(row["PlyTo"]),
        //                            PlyFrom = Convert.ToInt32(row["PlyFrom"]),
        //                            LotNo = Convert.ToInt32(row["LotNo"]),
        //                            SubSection = Convert.ToString(row["SubSection"]),
        //                            IsDispatch = Convert.ToBoolean(row["IsDispatch"]),
        //                            StyleCode = Convert.ToString(row["StyleCode"]),
        //                            OrderNo = Convert.ToString(row["OrderNo"]),
        //                            SupervisorID = row["SupervisorID"] == DBNull.Value ? 0 : Convert.ToInt32(row["SupervisorID"]),
        //                            SupervisorAssignedDate = row["SupervisorAssignedDate"]?.ToString() ?? "",
        //                            AppEmpID = row["AppEmpID"] == DBNull.Value ? 0 : Convert.ToInt32(row["AppEmpID"]),
        //                            AppStartTime = row["AppStartTime"]?.ToString() ?? "",
        //                            AppEndTime = row["AppEndTime"]?.ToString() ?? "",
        //                            BundleIDStatus = row["BundleIDStatus"]?.ToString() ?? "",
        //                            CreatedBy = Convert.ToInt32(row["CreatedBy"]),
        //                            CreatedOn = row["CreatedOn"]?.ToString() ?? "",
        //                            ModifiedBy = row["ModifiedBy"] == DBNull.Value ? 0 : Convert.ToInt32(row["ModifiedBy"]),
        //                            ModifiedOn = row["ModifiedOn"]?.ToString() ?? ""
        //                        };
        //                        objResp.Add(obj);
        //                    }
        //                    response.vErrorCode = 200;
        //                    response.vErrorMsg = "Success";
        //                }
        //                else
        //                {
        //                    // No data case
        //                    response.Data = new List<clsBundleCompile>();
        //                    response.Pagination = new clsPagination();
        //                    response.vErrorCode = 404;
        //                    response.vErrorMsg = "No Records Found";
        //                    return response;
        //                }

        //                // ================= Pagination =================
        //                if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
        //                {
        //                    DataRow pg = ds.Tables[1].Rows[0];

        //                    response.Pagination = new clsPagination
        //                    {
        //                        PageNumber = Convert.ToInt32(pg["PageNumber"]),
        //                        PageSize = Convert.ToInt32(pg["PageSize"]),
        //                        TotalRecords = Convert.ToInt32(pg["TotalRecords"]),
        //                        TotalPages = Convert.ToInt32(pg["TotalPages"]),
        //                        HasNextPage = Convert.ToBoolean(pg["HasNextPage"]),
        //                        HasPreviousPage = Convert.ToBoolean(pg["HasPreviousPage"])
        //                    };
        //                }
        //                else
        //                {
        //                    // fallback pagination
        //                    response.Pagination = new clsPagination
        //                    {
        //                        PageNumber = objReq.PageNumber,
        //                        PageSize = objReq.PageSize,
        //                        TotalRecords = objResp.Count,
        //                        TotalPages = 1,
        //                        HasNextPage = false,
        //                        HasPreviousPage = false
        //                    };
        //                }

        //                response.Data = objResp;
        //            }
        //        }
        //    }
        //    catch (Exception exp)
        //    {
        //        response.Data = new List<clsBundleCompile>();
        //        response.Pagination = new clsPagination();
        //        response.vErrorCode = 500;
        //        response.vErrorMsg = exp.Message.ToString();
        //        Logger.WriteLog("Function Name : Fn_Get_ActiveBundle", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
        //    }
        //    finally
        //    {
        //        Con.Close();
        //    }
        //    return response;
        //}



        public List<clsBundleCompile> Fn_Get_ActiveBundle(clsBundleCompile objReq)
        {
            var objResp = new List<clsBundleCompile>();
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                using (SqlCommand cmd = new SqlCommand("USP_MobileGetSubsectionWiseOpNo", Con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@OrderNo", string.IsNullOrWhiteSpace(objReq.OrderNo) ? (object)DBNull.Value : objReq.OrderNo);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataSet ds = new DataSet();
                        da.Fill(ds);

                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                var obj = new clsBundleCompile
                                {
                                    OperationNo = Convert.ToInt64(row["OperationNo"]),
                                    OperationName = Convert.ToString(row["OperationName"]),
                                    StdMin = Convert.ToDecimal(row["StdMin"]),
                                    SubSection = Convert.ToString(row["SubSection"]),
                                    OrderNo = Convert.ToString(row["OrderNo"]),
                                };
                                obj.vErrorCode = 200;
                                obj.vErrorMsg = "Success";
                                objResp.Add(obj);
                            }
                        }
                        else
                        {
                            var obj = new clsBundleCompile();
                            obj.vErrorCode = 404;
                            obj.vErrorMsg = "No Records Found";
                            objResp.Add(obj);
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                var obj = new clsBundleCompile();
                obj.vErrorCode = 500;
                obj.vErrorMsg = exp.Message.ToString();
                Logger.WriteLog("Function Name : Fn_Get_ActiveBundle", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
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
            var objWorker = new clsWorker();
            objWorker.AppEmpID = objReq.AppEmpID;
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Insert_MachineLogTransaction");
            try
            {
                if (objReq.AppEmpID == null || objReq.AppEmpID == 0)
                {
                    objResp.vErrorMsg = "Please Pass the App Employee/Operator ID";
                    objResp.vErrorCode = 300;
                }
                else if (String.IsNullOrWhiteSpace(objReq.LineName))
                {
                    objResp.vErrorMsg = "Please Pass Line Name.";
                    objResp.vErrorCode = 300;
                }
                else if (String.IsNullOrWhiteSpace(objReq.MachineId))
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
                    cmd.Parameters.AddWithValue("@LineName", objReq.LineName);
                    cmd.Parameters.AddWithValue("@MachineId", objReq.MachineId);
                    cmd.Parameters.AddWithValue("@MachineLogDescription", objReq.MachineLogDescription);
                    cmd.Parameters.AddWithValue("@AppEmpID", objReq.AppEmpID);
                    cmd.Parameters.AddWithValue("@MachineStatus", objReq.MachineStatus);
                    cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                    cmd.Parameters.AddWithValue("@QueryType", "InsertMachineLog");

                    int i = 0;
                    i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        objResp.vErrorMsg = "Success";
                        objResp.vErrorCode = 200;

                        objResp.MachineStatus = objReq.MachineStatus;
                        if (objReq.MachineStatus == "Pause")
                        {
                            var objMachineLog = new clsMachineLogLostTimeTransactions();

                            var objWorkerDetails = new clsWorker();
                            MOBDALEmployee _MOBDALEmployee = new MOBDALEmployee();
                            objWorkerDetails = _MOBDALEmployee.Fn_Fetch_WorkerDetailsByID(objWorker);

                            string strSubject = "Machine Id " + objReq.MachineId + " Send for repair purpose.";
                            string strMsg = "Dear Paridhana Team," + "<br><br> Machine Id <b>" + objReq.MachineId + "</b> is send for repair purpose" + "<br>";
                            strMsg += "Kindly repair the machine asap. <br><br>";
                            strMsg += "Machine Issue and Employee Details :- <br>";
                            strMsg += "<b>Employee ID : </b>" + objWorkerDetails.AppEmpID + "<br>";
                            strMsg += "<b>Name : </b>" + objWorkerDetails.Name + "<br>";
                            strMsg += "<b>Unit : </b>" + objWorkerDetails.Unit + "<br>";
                            strMsg += "<b>Line Name : </b>" + objWorkerDetails.LineName + "<br>";
                            strMsg += "<b>Machine Issue : </b>" + objReq.MachineLogDescription + "<br><br>";
                            strMsg += "Thanks & Regards,<br>";
                            strMsg += "Banswara Syntex Ltd.";

                            string ToEmail = objWorkerDetails.UnitEmailId;
                            string CcEmail = "kanchanparab@banswarasyntex.com";

                            gn.TriggerEmailOnly("", strSubject, ToEmail, CcEmail, strMsg);
                        }
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
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Insert_MachineLogTransaction");
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
                    objResp.MachineId = Convert.ToString(ds.Tables[0].Rows[i]["MachineId"]);
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


        public List<clsMachineLogMaster> Fn_Fetch_MachineLogList(clsMachineLogMaster objReq)
        {
            var objResp = new List<clsMachineLogMaster>();
            var obj = new clsMachineLogMaster();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Fetch_MachineLogList");
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_MobileMachineLogTransactions", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@QueryType", "FetchMachineLogList");

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

                        obj.vErrorCode = 200;
                        obj.vErrorMsg = "Success";
                        objResp.Add(obj);
                        i++;
                    }
                }
                else
                {
                    obj.vErrorCode = 404;
                    obj.vErrorMsg = "Machine Log list records not found.";
                    objResp.Add(obj);
                }
            }
            catch (Exception exp)
            {
                obj.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Fetch_MachineLogList", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Fetch_MachineLogList");
            return objResp;
        }


        public clsMachineLogLostTimeTransactions Fn_Fetch_MachineLogStatusByOperatorID(clsMachineLogLostTimeTransactions objReq)
        {
            var objResp = new clsMachineLogLostTimeTransactions();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Fetch_MachineLogStatusByOperatorID");
            try
            {
                if (objReq.AppEmpID == null || objReq.AppEmpID == 0)
                {
                    objResp.vErrorMsg = "Please Pass the Valid App Employee/Operator ID";
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
                    cmd.Parameters.AddWithValue("@AppEmpID", objReq.AppEmpID);
                    cmd.Parameters.AddWithValue("@QueryType", "FetchMachineLogStatusByOperatorID");

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    int i = 0;
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        objResp.AppEmpID = Convert.ToInt32(ds.Tables[0].Rows[i]["AppEmpID"]);
                        objResp.MachineStatus = Convert.ToString(ds.Tables[0].Rows[i]["MachineStatus"]);

                        objResp.vErrorCode = 200;
                        objResp.vErrorMsg = "Success";
                    }
                    else
                    {
                        objResp.vErrorCode = 404;
                        objResp.vErrorMsg = "Machine Log Status is not found.";
                    }
                }
            }
            catch (Exception exp)
            {
                Logger.WriteLog("Function Name : Fn_Fetch_MachineLogStatusByOperatorID", " " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
                objResp.vErrorCode = 500;
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Fetch_MachineLogStatusByOperatorID");
            return objResp;
        }


        public clsMachineLogLostTimeTransactions Fn_Fetch_MachineLogLostTimeByOperatorID(clsMachineLogLostTimeTransactions objReq)
        {
            var objResp = new clsMachineLogLostTimeTransactions();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Fetch_MachineLogLostTimeByOperatorID");
            try
            {
                if (objReq.AppEmpID == null || objReq.AppEmpID == 0)
                {
                    objResp.vErrorMsg = "Please Pass the Valid App Employee/Operator ID";
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
                    cmd.Parameters.AddWithValue("@AppEmpID", objReq.AppEmpID);
                    cmd.Parameters.AddWithValue("@CurrentDate", objReq.CurrentDate);
                    cmd.Parameters.AddWithValue("@QueryType", "FetchMachineBreakdownLostTime");

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    int i = 0;
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        objResp.LostTimeInMinutes = Convert.ToString(ds.Tables[0].Rows[i]["LostTimeInMinutes"]);

                        objResp.vErrorCode = 200;
                        objResp.vErrorMsg = "Success";
                    }
                    else
                    {
                        objResp.vErrorCode = 404;
                        objResp.vErrorMsg = "Machine Breakdown Lost time is not found.";
                    }
                }
            }
            catch (Exception exp)
            {
                Logger.WriteLog("Function Name : Fn_Fetch_MachineLogLostTimeByOperatorID", " " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
                objResp.vErrorCode = 500;
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Fetch_MachineLogLostTimeByOperatorID");
            return objResp;
        }


        public clsBundleCompile Fn_Update_SupervisorAssignedBundleIDEmp(clsBundleCompile objReq)
        {
            var objResp = new clsBundleCompile();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Update_SupervisorAssignedBundleIDEmp");
            try
            {
                if (objReq.SupervisorID == null || objReq.SupervisorID == 0)
                {
                    objResp.vErrorMsg = "Please Pass the Valid Supervisor Employee ID";
                    objResp.vErrorCode = 300;
                }
                else if (objReq.OrderNo == null || objReq.OrderNo == "")
                {
                    objResp.vErrorMsg = "Please Pass the Valid Order No";
                    objResp.vErrorCode = 300;
                }
                else if (string.IsNullOrWhiteSpace(objReq.OperationNos))
                {
                    objResp.vErrorMsg = "Please Pass the Operation Numbers";
                    objResp.vErrorCode = 300;
                }
                else if (string.IsNullOrWhiteSpace(objReq.SubSection))
                {
                    objResp.vErrorMsg = "Please Pass the Sub Section";
                    objResp.vErrorCode = 300;
                }
                else if (string.IsNullOrWhiteSpace(objReq.AppEmpIDs))
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
                    cmd.Parameters.AddWithValue("@OrderNo", objReq.OrderNo);
                    cmd.Parameters.AddWithValue("@OperationNos", objReq.OperationNos);
                    cmd.Parameters.AddWithValue("@SubSection", objReq.SubSection);
                    cmd.Parameters.AddWithValue("@AppEmpIDs", objReq.AppEmpIDs);
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
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Update_SupervisorAssignedBundleIDEmp");
            return objResp;
        }


        public clsBundleCompile Fn_Update_AppEmpStartBundleIDStatus(clsBundleCompile objReq)
        {
            Boolean ConfigField = Convert.ToBoolean(ConfigurationManager.AppSettings["BundleCompileValue"]);
            var objResp = new clsBundleCompile();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Update_AppEmpStartBundleIDStatus");
            try
            {
                if (ConfigField)
                {
                    if (objReq.AppEmpID == null || objReq.AppEmpID == 0)
                    {
                        objResp.vErrorMsg = "Please Pass the Valid App Employee ID";
                        objResp.vErrorCode = 300;
                    }
                    else if (objReq.OrderNo == null || objReq.OrderNo == "")
                    {
                        objResp.vErrorMsg = "Please Pass the Valid Order No";
                        objResp.vErrorCode = 300;
                    }
                    else if (objReq.BundleID == null || objReq.BundleID == 0)
                    {
                        objResp.vErrorMsg = "Please Pass the Valid Bundle ID";
                        objResp.vErrorCode = 300;
                    }
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
                        cmd.Parameters.AddWithValue("@OrderNo", objReq.OrderNo);
                        cmd.Parameters.AddWithValue("@BundleID", objReq.BundleID);
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
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Update_AppEmpStartBundleIDStatus");
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


        public clsBundleCompile Fn_Update_AppEmpStartEndBundleIDStatus(clsBundleCompile objReq)
        {
            var objResp = new clsBundleCompile();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Update_AppEmpStartEndBundleIDStatus");
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
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Update_AppEmpStartEndBundleIDStatus");
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
                strSql = strSql + " BC.SubSection AS SubSection, BC.LayID AS LayID, BD.OperationNo AS OperationNo, BC.SupervisorID AS SupervisorID, EM.EmpName AS SupervisorName,";
                strSql = strSql + " FORMAT(BD.CreatedOn, 'dd-MMM-yyyy HH:mm:ss') AS CreatedOn, BD.CreatedBy AS CreatedBy,";
                strSql = strSql + " FORMAT(BD.ModifiedOn, 'dd-MMM-yyyy HH:mm:ss') AS ModifiedOn, BD.ModifiedBy AS ModifiedBy";
                strSql = strSql + " FROM BundleCompile AS BC";
                strSql = strSql + " INNER JOIN BundleCompileDetail AS BD";
                strSql = strSql + " ON BC.BundleID = BD.BundleID";
                strSql = strSql + " INNER JOIN EmployeeDetail AS ED";
                strSql = strSql + " ON BD.AppEmpID = ED.Code";
                strSql = strSql + " INNER JOIN LineMaster AS LM";
                strSql = strSql + " ON LM.LineName = ED.LineName";
                strSql = strSql + " INNER JOIN EmployeeMaster AS EM";
                strSql = strSql + " ON EM.EmpId = BC.SupervisorID";
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
                        obj.SupervisorID = Convert.ToInt32(ds.Tables[0].Rows[i]["SupervisorID"]);
                        obj.SupervisorName = Convert.ToString(ds.Tables[0].Rows[i]["SupervisorName"]);

                        if (ds.Tables[0].Rows[0]["CreatedOn"] == null)
                        {
                            obj.CreatedOn = string.Empty;
                        }
                        else
                        {
                            obj.CreatedOn = Convert.ToString(ds.Tables[0].Rows[0]["CreatedOn"]);
                        }

                        if (ds.Tables[0].Rows[0]["CreatedBy"] == DBNull.Value)
                        {
                            obj.CreatedBy = 0;
                        }
                        else
                        {
                            obj.CreatedBy = Convert.ToInt32(ds.Tables[0].Rows[0]["CreatedBy"]);
                        }

                        if (ds.Tables[0].Rows[0]["ModifiedOn"] == null)
                        {
                            obj.ModifiedOn = string.Empty;
                        }
                        else
                        {
                            obj.ModifiedOn = Convert.ToString(ds.Tables[0].Rows[0]["ModifiedOn"]);
                        }

                        if (ds.Tables[0].Rows[0]["ModifiedBy"] == DBNull.Value)
                        {
                            obj.ModifiedBy = 0;
                        }
                        else
                        {
                            obj.ModifiedBy = Convert.ToInt32(ds.Tables[0].Rows[0]["ModifiedBy"]);
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

                string strSql = "SELECT DISTINCT OrderNo FROM BundleCompile AS BC";
                strSql = strSql + " INNER JOIN BundleCompileDetail AS BD";
                strSql = strSql + " ON BD.BundleID = BC.BundleID";

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

                string strSql = "SELECT BD.BundleID AS BundleID, BC.BundleNo AS BundleNo, BD.OperationNo AS OperationNo, BC.SizeName AS SizeName,";
                strSql = strSql + " BC.ColorName AS ColorName, BC.Qty AS Qty, BC.PlyFrom AS PlyFrom, BC.PlyTo AS PlyTo, BC.LotNo AS LotNo,";
                strSql = strSql + " BD.SubSection AS SubSection, BC.StyleCode AS StyleCode, BC.OrderNo AS OrderNo,";
                strSql = strSql + " BC.LayID AS LayID, BD.BundleIDStatus AS BundleIDStatus, BC.UpdateType AS UpdateType,";
                strSql = strSql + " FORMAT(BD.CreatedOn, 'dd-MMM-yyyy HH:mm:ss') AS CreatedOn, BD.CreatedBy AS CreatedBy,";
                strSql = strSql + " FORMAT(BD.ModifiedOn,'dd-MMM-yyyy HH:mm:ss') AS ModifiedOn, BD.ModifiedBy AS ModifiedBy";
                strSql = strSql + " FROM BundleCompileDetail AS BD";
                strSql = strSql + " INNER JOIN BundleCompile AS BC";
                strSql = strSql + " ON BD.BundleID = BC.BundleID";
                strSql = strSql + " WHERE BD.BundleIDStatus = 'Assigned' AND BD.AppEmpID = " + objReq.AppEmpID;

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

                        if (ds.Tables[0].Rows[i]["UpdateType"] == null)
                        {
                            obj.UpdateType = string.Empty;
                        }
                        else
                        {
                            obj.UpdateType = Convert.ToString(ds.Tables[0].Rows[i]["UpdateType"]);
                        }

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

                string strSql = "SELECT TOP 10 BD.BundleID AS BundleID, BC.BundleNo AS BundleNo, BD.OperationNo AS OperationNo,";
                strSql = strSql + " BD.SubSection AS SubSection, OBM.OperationName AS OperationName, OBM.SubProduct AS SubProduct,";
                strSql = strSql + " BC.SizeName AS SizeName, BC.ColorName AS ColorName, BC.Qty AS Qty, BC.PlyFrom AS PlyFrom,";
                strSql = strSql + " BC.PlyTo AS PlyTo, BC.LotNo AS LotNo, BC.StyleCode AS StyleCode, BC.OrderNo AS OrderNo,";
                strSql = strSql + " BC.LayID AS LayID, BD.BundleIDStatus AS BundleIDStatus, BC.UpdateType AS UpdateType,";
                strSql = strSql + " FORMAT(BD.CreatedOn, 'dd-MMM-yyyy HH:mm:ss') AS CreatedOn, BD.CreatedBy AS CreatedBy,";
                strSql = strSql + " FORMAT(BD.ModifiedOn,'dd-MMM-yyyy HH:mm:ss') AS ModifiedOn, BD.ModifiedBy AS ModifiedBy";
                strSql = strSql + " FROM BundleCompileDetail AS BD";
                strSql = strSql + " INNER JOIN BundleCompile AS BC";
                strSql = strSql + " ON BD.BundleID = BC.BundleID";
                strSql = strSql + " INNER JOIN OBMainMasterNew AS OBM";
                strSql = strSql + " ON OBM.OperationNo = BD.OperationNo";
                strSql = strSql + " WHERE BD.BundleIDStatus = 'Finished' AND BD.AppEmpID = " + objReq.AppEmpID;

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
                        obj.SubSection = Convert.ToString(ds.Tables[0].Rows[i]["SubSection"]);
                        obj.OperationName = Convert.ToString(ds.Tables[0].Rows[i]["OperationName"]);
                        obj.SubProduct = Convert.ToString(ds.Tables[0].Rows[i]["SubProduct"]);
                        obj.SizeName = Convert.ToString(ds.Tables[0].Rows[i]["SizeName"]);
                        obj.ColorName = Convert.ToString(ds.Tables[0].Rows[i]["ColorName"]);
                        obj.Qty = Convert.ToInt32(ds.Tables[0].Rows[i]["Qty"]);
                        obj.PlyFrom = Convert.ToInt32(ds.Tables[0].Rows[i]["PlyFrom"]);
                        obj.PlyTo = Convert.ToInt32(ds.Tables[0].Rows[i]["PlyTo"]);
                        obj.LotNo = Convert.ToInt32(ds.Tables[0].Rows[i]["LotNo"]);
                        obj.StyleCode = Convert.ToString(ds.Tables[0].Rows[i]["StyleCode"]);
                        obj.OrderNo = Convert.ToString(ds.Tables[0].Rows[i]["OrderNo"]);
                        obj.LayID = Convert.ToInt64(ds.Tables[0].Rows[i]["LayID"]);
                        obj.BundleIDStatus = Convert.ToString(ds.Tables[0].Rows[i]["BundleIDStatus"]);
                        obj.UpdateType = Convert.ToString(ds.Tables[0].Rows[i]["UpdateType"]);
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
                strSql = strSql + " WHERE CONVERT(DATE, BD.ModifiedOn) = CONVERT(DATE, @CurDate) AND BD.AppEmpId = " + objReq.AppEmpID;
                strSql = strSql + " UNION";
                strSql = strSql + " SELECT SUM(BD.StdRate*BC.Qty) AS Earnings, DATENAME(MONTH, @CurDate) AS Months,'Month' AS TimePeriod";
                strSql = strSql + " FROM BundleCompileDetail AS BD";
                strSql = strSql + " INNER JOIN BundleCompile AS BC";
                strSql = strSql + " ON BC.BundleID = BD.BundleID";
                strSql = strSql + " WHERE MONTH(BD.ModifiedOn) = MONTH(@CurDate) AND BD.AppEmpId = " + objReq.AppEmpID;

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

                //string strSql = "DECLARE @CurrentDate DATE = '" + objReq.CurrentDate + "'";
                //strSql = strSql + "SELECT BD.OperationNo, MAX(BD.SubSection) AS SubSection, MAX(BD.StdRate) AS StdRate,";
                //strSql = strSql + " SUM(BC.Qty) AS Qty, (MAX(BD.StdRate) * SUM(BC.Qty)) AS TotalAmount, MAX(ED.LineName) AS LineName,";
                //strSql = strSql + " MAX(BC.StyleCode) AS StyleCode, MAX(OBD.Descriptions) AS Descriptions,";
                //strSql = strSql + " MAX(BC.OrderNo) AS OrderNo";
                //strSql = strSql + " FROM BundleCompileDetail AS BD";
                //strSql = strSql + " INNER JOIN BundleCompile AS BC";
                //strSql = strSql + " ON BD.BundleID = BC.BundleID";
                //strSql = strSql + " INNER JOIN EmployeeDetail AS ED";
                //strSql = strSql + " ON ED.Code = BD.AppEmpID";
                //strSql = strSql + " INNER JOIN LineMaster AS LM";
                //strSql = strSql + " ON LM.LineName = ED.LineName";
                //strSql = strSql + " INNER JOIN OperationBreackDownDetail AS OBD";
                //strSql = strSql + " ON OBD.OpNo = BD.OperationNo";
                //strSql = strSql + " INNER JOIN OperationBreackDownMaster AS OBM";
                //strSql = strSql + " ON OBM.ID = OBD.MID AND BC.StyleCode = OBM.StyleCode";
                //strSql = strSql + " WHERE BD.AppEmpID = " + objReq.AppEmpID;
                //strSql = strSql + " AND BD.ModifiedOn >= @CurrentDate AND BD.ModifiedOn < DATEADD(DAY, 1, @CurrentDate) AND LM.LineName = '" + objReq.LineName + "'";
                //strSql = strSql + " GROUP BY BD.OperationNo";

                string strSql = "DECLARE @CurrentDate DATE = '" + objReq.CurrentDate + "'";
                strSql = strSql + " SELECT BD.OperationNo, MAX(BD.SubSection) AS SubSection, MAX(BD.StdRate) AS StdRate,";
                strSql = strSql + " SUM(BC.Qty) AS Qty, (MAX(BD.StdRate) * SUM(BC.Qty)) AS TotalAmount, MAX(ED.LineName) AS LineName,";
                strSql = strSql + " MAX(BC.StyleCode) AS StyleCode, MAX(BD.Descriptions) AS Descriptions,";
                strSql = strSql + " MAX(BC.OrderNo) AS OrderNo";
                strSql = strSql + " FROM BundleCompileDetail AS BD";
                strSql = strSql + " INNER JOIN BundleCompile AS BC";
                strSql = strSql + " ON BD.BundleID = BC.BundleID";
                strSql = strSql + " INNER JOIN EmployeeDetail AS ED";
                strSql = strSql + " ON ED.Code = BD.AppEmpID";
                strSql = strSql + " INNER JOIN LineMaster AS LM";
                strSql = strSql + " ON LM.LineName = ED.LineName";
                strSql = strSql + " WHERE BD.AppEmpID = " + objReq.AppEmpID;
                strSql = strSql + " AND BD.ModifiedOn >= @CurrentDate AND BD.ModifiedOn < DATEADD(DAY, 1, @CurrentDate) AND LM.LineName = '" + objReq.LineName + "'";
                strSql = strSql + " GROUP BY BD.OperationNo";

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
                        obj.StdRate = Convert.ToDecimal(ds.Tables[0].Rows[i]["StdRate"]);
                        obj.Qty = Convert.ToInt32(ds.Tables[0].Rows[i]["Qty"]);

                        if (ds.Tables[0].Rows[i]["TotalAmount"] == DBNull.Value)
                        {
                            obj.TotalAmount = 0;
                        }
                        else
                        {
                            obj.TotalAmount = Convert.ToDecimal(ds.Tables[0].Rows[i]["TotalAmount"]);
                        }

                        obj.LineName = Convert.ToString(ds.Tables[0].Rows[i]["LineName"]);
                        obj.StyleCode = Convert.ToString(ds.Tables[0].Rows[i]["StyleCode"]);
                        obj.Descriptions = Convert.ToString(ds.Tables[0].Rows[i]["Descriptions"]);
                        obj.OrderNo = Convert.ToString(ds.Tables[0].Rows[i]["OrderNo"]);

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


        public List<clsBundleCompile> Fn_Get_SupervisorAssignToOperator(clsBundleCompile objReq)
        {
            var objResp = new List<clsBundleCompile>();
            var obj = new clsBundleCompile();
            try
            {
                if (string.IsNullOrWhiteSpace(objReq.OrderNo))
                {
                    obj.vErrorMsg = "Please Pass the Valid Order No";
                    obj.vErrorCode = 300;
                }
                else
                {
                    if (Con.State == ConnectionState.Broken)
                    { Con.Close(); }
                    if (Con.State == ConnectionState.Closed)
                    { Con.Open(); }

                    string strSql = "Select BC.OrderNo AS OrderNo, BC.BundleID AS BundleID, BD.OperationNo AS OperationNo,";
                    strSql = strSql + " BD.SubSection AS SubSection, BC.SupervisorID AS SupervisorID, BC.BundleIDStatus AS BundleIDStatus";
                    strSql = strSql + " FROM BundleCompile AS BC";
                    strSql = strSql + " INNER JOIN BundleCompileDetail AS BD";
                    strSql = strSql + " ON BC.BundleID = BD.BundleID";
                    strSql = strSql + " WHERE BC.SupervisorID IS NULL";
                    strSql = strSql + " AND BC.BundleIDStatus IS NULL AND BC.OrderNo = '" + objReq.OrderNo + "'";

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
                            obj.OrderNo = Convert.ToString(ds.Tables[0].Rows[i]["OrderNo"]);
                            obj.BundleID = Convert.ToInt64(ds.Tables[0].Rows[i]["BundleID"]);
                            obj.OperationNo = Convert.ToInt64(ds.Tables[0].Rows[i]["OperationNo"]);
                            obj.SubSection = Convert.ToString(ds.Tables[0].Rows[i]["SubSection"]);

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

                            obj.vErrorCode = 200;
                            obj.vErrorMsg = "Success";
                            objResp.Add(obj);
                            i++;
                        }
                    }
                    else
                    {
                        obj.vErrorCode = 404;
                        obj.vErrorMsg = "Supervisor Assign to Operator records are not found.";
                        objResp.Add(obj);
                    }
                }
            }
            catch (Exception exp)
            {
                obj.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Get_SupervisorAssignToOperator", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }



        public List<clsBundleCompile> Fn_Get_AssignedOperationNumberDetails(clsBundleCompile objReq)
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

                    string strSql = "SELECT BD.AppEmpID AS EmpID, EM.EmpName AS EmpName, BC.BundleID AS BundleID,";
                    strSql = strSql + " BD.OperationNo AS OperationNo, BC.SubSection AS SubSection, BC.OrderNo AS OrderNo,";
                    strSql = strSql + " BD.BundleIDStatus AS BundleIDStatus";
                    strSql = strSql + " FROM BundleCompile AS BC";
                    strSql = strSql + " INNER JOIN BundleCompileDetail AS BD";
                    strSql = strSql + " ON BD.BundleID = BC.BundleID";
                    strSql = strSql + " INNER JOIN EmployeeMaster AS EM";
                    strSql = strSql + " ON BD.AppEmpID = EM.EmpId";
                    strSql = strSql + " WHERE BD.BundleIDStatus = 'Assigned' AND BD.AppEmpID = " + objReq.AppEmpID;

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
                            obj.AppEmpID = Convert.ToInt32(ds.Tables[0].Rows[i]["EmpID"]);
                            obj.AppEmpName = Convert.ToString(ds.Tables[0].Rows[i]["EmpName"]);
                            obj.BundleID = Convert.ToInt64(ds.Tables[0].Rows[i]["BundleID"]);
                            obj.OperationNo = Convert.ToInt64(ds.Tables[0].Rows[i]["OperationNo"]);
                            obj.SubSection = Convert.ToString(ds.Tables[0].Rows[i]["SubSection"]);
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
                        obj.vErrorMsg = "Operator ID Wise assigned operation number records are not found.";
                        objResp.Add(obj);
                    }
                }
            }
            catch (Exception exp)
            {
                obj.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Get_AssignedOperationNumberDetails", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }



        public List<clsOrderMaster> Fn_Fetch_OrderNumberDetails(clsOrderMaster objReq)
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
                strSql = strSql + " StyleCode, FORMAT(CreatedOn, 'dd-MMM-yyyy HH:mm:ss') AS CreatedOn, CreatedBy, OrderStatus,";
                strSql = strSql + " FORMAT(ModifiedOn, 'dd-MMM-yyyy HH:mm:ss') AS ModifiedOn, ModifiedBy";
                strSql = strSql + " FROM OrderMaster WHERE OrderNo IN (SELECT DISTINCT(OrderNo) FROM BundleCompile) AND OrderNo = '" + objReq.OrderNo + "'";

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
                    obj.vErrorMsg = "Order Number details are not found.";
                    objResp.Add(obj);
                }
            }
            catch (Exception exp)
            {
                obj.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Fetch_OrderNumberDetails", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            return objResp;      
        }



        public List<clsBundleCompile> Fn_Fetch_OperatorAssignOpNumbers(clsBundleCompile objReq)
        {
            var objResp = new List<clsBundleCompile>();
            var obj = new clsBundleCompile();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Fetch_OperatorAssignOpNumbers");
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                //string strSql = "SELECT DISTINCT (BAD.OperationNo) AS OperationNo, BAD.OrderNo AS OrderNo, BAD.SubSection AS SubSection,";
                //strSql = strSql + " BAD.SupervisorID AS SupervisorID, EM.EmpName AS SupervisorName, BAD.SupAssignedDate AS SupAssignedDate,";
                //strSql = strSql + " OBD.Descriptions AS OperationName, BAD.CreatedBy AS CreatedBy,";
                //strSql = strSql + " FORMAT(BAD.CreatedOn, 'dd-MMM-yyyy HH:mm:ss') AS CreatedOn";
                //strSql = strSql + " FROM BundleCompileAssignDetail AS BAD";
                //strSql = strSql + " INNER JOIN BundleCompile AS BC";
                //strSql = strSql + " ON BAD.OrderNo = BC.OrderNo";
                //strSql = strSql + " INNER JOIN EmployeeMaster AS EM";
                //strSql = strSql + " ON BAD.SupervisorID = EM.EmpId";
                //strSql = strSql + " INNER JOIN EmployeeMaster AS EM1";
                //strSql = strSql + " ON BAD.AppEmpID = EM1.EmpId";
                //strSql = strSql + " INNER JOIN OperationBreackDownDetail AS OBD";
                //strSql = strSql + " ON BAD.OperationNo = OBD.OpNo";
                //strSql = strSql + " INNER JOIN OperationBreackDownMaster AS OBM";
                //strSql = strSql + " ON OBM.ID = OBD.MID AND BC.StyleCode = OBM.StyleCode";
                //strSql = strSql + " WHERE BAD.AppEmpID = " + objReq.AppEmpID;
                //strSql = strSql + " ORDER BY BAD.SupAssignedDate DESC, BAD.OrderNo, BAD.OperationNo";

                string strSql = "SELECT DISTINCT (BAD.OperationNo) AS OperationNo, BAD.OrderNo AS OrderNo, BAD.SubSection AS SubSection,";
                strSql = strSql + " BAD.SupervisorID AS SupervisorID, EM.EmpName AS SupervisorName, BAD.SupAssignedDate AS SupAssignedDate,";
                strSql = strSql + " BD.Descriptions AS OperationName, BAD.CreatedBy AS CreatedBy,";
                strSql = strSql + " FORMAT(BAD.CreatedOn, 'dd-MMM-yyyy HH:mm:ss') AS CreatedOn";
                strSql = strSql + " FROM BundleCompileAssignDetail AS BAD";
                strSql = strSql + " INNER JOIN BundleCompile AS BC";
                strSql = strSql + " ON BAD.OrderNo = BC.OrderNo";
                strSql = strSql + " INNER JOIN BundleCompileDetail AS BD";
                strSql = strSql + " ON BAD.OperationNo = BD.OperationNo";
                strSql = strSql + " INNER JOIN EmployeeMaster AS EM";
                strSql = strSql + " ON BAD.SupervisorID = EM.EmpId";
                strSql = strSql + " INNER JOIN EmployeeMaster AS EM1";
                strSql = strSql + " ON BAD.AppEmpID = EM1.EmpId";
                strSql = strSql + " WHERE BAD.AppEmpID = " + objReq.AppEmpID;
                strSql = strSql + " ORDER BY BAD.SupAssignedDate DESC, BAD.OrderNo, BAD.OperationNo";

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
                        obj.OrderNo = Convert.ToString(ds.Tables[0].Rows[i]["OrderNo"]);
                        obj.OperationNo = Convert.ToInt32(ds.Tables[0].Rows[i]["OperationNo"]);
                        obj.SubSection = Convert.ToString(ds.Tables[0].Rows[i]["SubSection"]);
                        obj.SupervisorID = Convert.ToInt32(ds.Tables[0].Rows[i]["SupervisorID"]);
                        obj.SupervisorName = Convert.ToString(ds.Tables[0].Rows[i]["SupervisorName"]);
                        obj.SupervisorAssignedDate = Convert.ToString(ds.Tables[0].Rows[i]["SupAssignedDate"]);
                        obj.OperationName = Convert.ToString(ds.Tables[0].Rows[i]["OperationName"]);
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
                    obj.vErrorMsg = "No records found.";
                    objResp.Add(obj);
                }
            }
            catch (Exception exp)
            {
                obj.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Fetch_OperatorAssignOpNumbers", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Fetch_OperatorAssignOpNumbers");
            return objResp;
        }


        public List<clsBundleCompile> Fn_Fetch_SupervisorAssignOpNoToOperators(clsBundleCompile objReq)
        {
            var objResp = new List<clsBundleCompile>();
            var obj = new clsBundleCompile();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Fetch_SupervisorAssignOpNoToOperators");
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                //string strSql = "SELECT BAD.OrderNo AS OrderNo, BAD.OperationNo AS OperationNo, BAD.SubSection AS SubSection,";
                //strSql = strSql + " BAD.SupervisorID AS SupervisorID, EM.EmpName AS SupervisorName, BAD.SupAssignedDate AS SupAssignedDate,";
                //strSql = strSql + " BAD.AppEmpID AS AppEmpID, EM1.EmpName AS AppEmpName, BAD.CreatedBy AS CreatedBy,";
                //strSql = strSql + " FORMAT(BAD.CreatedOn, 'dd-MMM-yyyy HH:mm:ss') AS CreatedOn,";
                //strSql = strSql + " (SELECT TOP 1 Descriptions FROM OperationBreackDownDetail WHERE OpNo = BAD.OperationNo) AS Descriptions";
                //strSql = strSql + " FROM BundleCompileAssignDetail AS BAD";
                //strSql = strSql + " INNER JOIN EmployeeMaster AS EM";
                //strSql = strSql + " ON BAD.SupervisorID = EM.EmpId";
                //strSql = strSql + " INNER JOIN EmployeeMaster AS EM1";
                //strSql = strSql + " ON BAD.AppEmpID = EM1.EmpId";
                //strSql = strSql + " WHERE EM.EmpRole = 'Supervisor'";

                string strSql = "SELECT BAD.OrderNo AS OrderNo, BAD.OperationNo AS OperationNo, BAD.SubSection AS SubSection,";
                strSql = strSql + " BAD.SupervisorID AS SupervisorID, EM.EmpName AS SupervisorName, BAD.SupAssignedDate AS SupAssignedDate,";
                strSql = strSql + " BAD.AppEmpID AS AppEmpID, EM1.EmpName AS AppEmpName, BAD.CreatedBy AS CreatedBy,";
                strSql = strSql + " FORMAT(BAD.CreatedOn, 'dd-MMM-yyyy HH:mm:ss') AS CreatedOn,";
                strSql = strSql + " (SELECT TOP 1 BCD.Descriptions FROM BundleCompileDetail AS BCD WHERE BCD.OperationNo = BAD.OperationNo) AS Descriptions";
                strSql = strSql + " FROM BundleCompileAssignDetail AS BAD";
                strSql = strSql + " INNER JOIN EmployeeMaster AS EM";
                strSql = strSql + " ON BAD.SupervisorID = EM.EmpId";
                strSql = strSql + " INNER JOIN EmployeeMaster AS EM1";
                strSql = strSql + " ON BAD.AppEmpID = EM1.EmpId";
                strSql = strSql + " WHERE EM.EmpRole = 'Supervisor'";

                if (objReq.AppEmpID > 0)
                {
                    strSql = strSql + " AND BAD.AppEmpID = " + objReq.AppEmpID;
                }
                if (!String.IsNullOrWhiteSpace(objReq.OrderNo))
                {
                    strSql = strSql + " AND BAD.OrderNo = '" + objReq.OrderNo + "'";
                }
                if (objReq.OperationNo > 0)
                {
                    strSql = strSql + " AND BAD.OperationNo = " + objReq.OperationNo;
                }

                strSql = strSql + " ORDER BY BAD.SupAssignedDate DESC, BAD.OrderNo, BAD.OperationNo";

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
                        obj.OrderNo = Convert.ToString(ds.Tables[0].Rows[i]["OrderNo"]);
                        obj.OperationNo = Convert.ToInt32(ds.Tables[0].Rows[i]["OperationNo"]);
                        obj.Descriptions = Convert.ToString(ds.Tables[0].Rows[i]["Descriptions"]);
                        obj.SubSection = Convert.ToString(ds.Tables[0].Rows[i]["SubSection"]);
                        obj.SupervisorID = Convert.ToInt32(ds.Tables[0].Rows[i]["SupervisorID"]);
                        obj.SupervisorName = Convert.ToString(ds.Tables[0].Rows[i]["SupervisorName"]);
                        obj.SupervisorAssignedDate = Convert.ToString(ds.Tables[0].Rows[i]["SupAssignedDate"]);
                        obj.AppEmpID = Convert.ToInt32(ds.Tables[0].Rows[i]["AppEmpID"]);
                        obj.AppEmpName = Convert.ToString(ds.Tables[0].Rows[i]["AppEmpName"]);
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
                    obj.vErrorMsg = "No records found.";
                    objResp.Add(obj);
                }
            }
            catch (Exception exp)
            {
                obj.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Fetch_SupervisorAssignOpNoToOperators", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Fetch_SupervisorAssignOpNoToOperators");
            return objResp;
        }


        public List<clsBundleCompile> Fn_Fetch_OperatorFinishedOpNumbers(clsBundleCompile objReq)
        {
            var objResp = new List<clsBundleCompile>();
            var obj = new clsBundleCompile();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Fetch_OperatorFinishedOpNumbers");
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                //string strSql = "SELECT DISTINCT (BD.BundleID) AS BundleID, BD.OperationNo AS OperationNo, BC.OrderNo AS OrderNo,";
                //strSql = strSql + " BD.SubSection AS SubSection, BD.SupervisorID AS SupervisorID, EM.EmpName AS SupervisorName,";
                //strSql = strSql + " BD.SupervisorAssignedDate AS SupAssignedDate, BD.BundleIDStatus AS BundleIDStatus, BD.CreatedBy AS CreatedBy,";
                //strSql = strSql + " FORMAT(BD.CreatedOn, 'dd-MMM-yyyy HH:mm:ss') AS CreatedOn, OBD.Descriptions AS OperationName,";
                //strSql = strSql + " BC.ColorName AS ColorName, BC.Qty AS Qty, BC.SizeName AS SizeName, BC.BundleNo AS BundleNo,";
                //strSql = strSql + " CONCAT(BC.PlyFrom,'-',BC.PlyTo) AS Ply, BD.AppStartTime AS AppStartTime, BD.AppEndTime AS AppEndTime";
                //strSql = strSql + " FROM BundleCompileDetail AS BD";
                //strSql = strSql + " INNER JOIN BundleCompile AS BC";
                //strSql = strSql + " ON BD.BundleID = BC.BundleID";
                //strSql = strSql + " INNER JOIN EmployeeMaster AS EM";
                //strSql = strSql + " ON BD.SupervisorID = EM.EmpId";
                //strSql = strSql + " INNER JOIN EmployeeMaster AS EM1";
                //strSql = strSql + " ON BD.AppEmpID = EM1.EmpId";
                //strSql = strSql + " INNER JOIN OperationBreackDownDetail AS OBD";
                //strSql = strSql + " ON OBD.OpNo = BD.OperationNo";
                //strSql = strSql + " INNER JOIN OperationBreackDownMaster AS OBM";
                //strSql = strSql + " ON OBM.ID = OBD.MID AND BC.StyleCode = OBM.StyleCode";
                //strSql = strSql + " WHERE BD.AppEmpID = " + objReq.AppEmpID;
                //strSql = strSql + " AND BD.BundleIDStatus = 'Finished' AND CONVERT(DATE, BD.AppEndTime) = CONVERT(DATE, GETDATE())";
                //strSql = strSql + " ORDER BY BD.AppEndTime DESC, BC.OrderNo, BD.OperationNo";

                string strSql = "SELECT DISTINCT (BD.BundleID) AS BundleID, BD.OperationNo AS OperationNo, BC.OrderNo AS OrderNo,";
                strSql = strSql + " BD.SubSection AS SubSection, BD.SupervisorID AS SupervisorID, EM.EmpName AS SupervisorName,";
                strSql = strSql + " BD.SupervisorAssignedDate AS SupAssignedDate, BD.BundleIDStatus AS BundleIDStatus, BD.CreatedBy AS CreatedBy,";
                strSql = strSql + " FORMAT(BD.CreatedOn, 'dd-MMM-yyyy HH:mm:ss') AS CreatedOn, BD.Descriptions AS OperationName,";
                strSql = strSql + " BC.ColorName AS ColorName, BC.Qty AS Qty, BC.SizeName AS SizeName, BC.BundleNo AS BundleNo,";
                strSql = strSql + " CONCAT(BC.PlyFrom,'-',BC.PlyTo) AS Ply, BD.AppStartTime AS AppStartTime, BD.AppEndTime AS AppEndTime";
                strSql = strSql + " FROM BundleCompileDetail AS BD";
                strSql = strSql + " INNER JOIN BundleCompile AS BC";
                strSql = strSql + " ON BD.BundleID = BC.BundleID";
                strSql = strSql + " INNER JOIN EmployeeMaster AS EM";
                strSql = strSql + " ON BD.SupervisorID = EM.EmpId";
                strSql = strSql + " INNER JOIN EmployeeMaster AS EM1";
                strSql = strSql + " ON BD.AppEmpID = EM1.EmpId";
                strSql = strSql + " WHERE BD.AppEmpID = " + objReq.AppEmpID;
                strSql = strSql + " AND BD.BundleIDStatus = 'Finished' AND CONVERT(DATE, BD.AppEndTime) = CONVERT(DATE, GETDATE())";
                strSql = strSql + " ORDER BY BD.AppEndTime DESC, BC.OrderNo, BD.OperationNo";

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
                        obj.OperationNo = Convert.ToInt32(ds.Tables[0].Rows[i]["OperationNo"]);
                        obj.OrderNo = Convert.ToString(ds.Tables[0].Rows[i]["OrderNo"]);
                        obj.SubSection = Convert.ToString(ds.Tables[0].Rows[i]["SubSection"]);
                        obj.SupervisorID = Convert.ToInt32(ds.Tables[0].Rows[i]["SupervisorID"]);
                        obj.SupervisorName = Convert.ToString(ds.Tables[0].Rows[i]["SupervisorName"]);
                        obj.SupervisorAssignedDate = Convert.ToString(ds.Tables[0].Rows[i]["SupAssignedDate"]);
                        obj.BundleIDStatus = Convert.ToString(ds.Tables[0].Rows[i]["BundleIDStatus"]);
                        obj.CreatedBy = Convert.ToInt32(ds.Tables[0].Rows[i]["CreatedBy"]);
                        obj.CreatedOn = Convert.ToString(ds.Tables[0].Rows[i]["CreatedOn"]);
                        obj.OperationName = Convert.ToString(ds.Tables[0].Rows[i]["OperationName"]);
                        obj.ColorName = Convert.ToString(ds.Tables[0].Rows[i]["ColorName"]);
                        obj.Qty = Convert.ToInt32(ds.Tables[0].Rows[i]["Qty"]);
                        obj.SizeName = Convert.ToString(ds.Tables[0].Rows[i]["SizeName"]);
                        obj.BundleNo = Convert.ToInt32(ds.Tables[0].Rows[i]["BundleNo"]);
                        obj.Ply = Convert.ToString(ds.Tables[0].Rows[i]["Ply"]);
                        obj.AppStartTime = Convert.ToString(ds.Tables[0].Rows[i]["AppStartTime"]);
                        obj.AppEndTime = Convert.ToString(ds.Tables[0].Rows[i]["AppEndTime"]);

                        obj.vErrorCode = 200;
                        obj.vErrorMsg = "Success";
                        objResp.Add(obj);
                        i++;
                    }
                }
                else
                {
                    obj.vErrorCode = 404;
                    obj.vErrorMsg = "No records found.";
                    objResp.Add(obj);
                }
            }
            catch (Exception exp)
            {
                obj.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Fetch_OperatorFinishedOpNumbers", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Fetch_OperatorFinishedOpNumbers");
            return objResp;
        }



        public clsBundleCompile Fn_Remove_OperationNumberBySupervisor(clsBundleCompile objReq)
        {
            var objResp = new clsBundleCompile();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Remove_OperationNumberBySupervisor");
            try
            {
                if (String.IsNullOrWhiteSpace(objReq.OrderNo))
                {
                    objResp.vErrorMsg = "Pass the Valid Order Number";
                    objResp.vErrorCode = 300;
                }
                else if (objReq.AppEmpID == null || objReq.AppEmpID == 0)
                {
                    objResp.vErrorMsg = "Please Pass the Valid Employee/Operator ID";
                    objResp.vErrorCode = 300;
                }
                else if (objReq.OperationNo == null || objReq.OperationNo == 0)
                {
                    objResp.vErrorMsg = "Please Pass the Valid Operation Number";
                    objResp.vErrorCode = 300;
                }
                else if (objReq.SupervisorID == null || objReq.SupervisorID == 0)
                {
                    objResp.vErrorMsg = "Please Pass the Valid Supervisore ID";
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
                    cmd.Parameters.AddWithValue("@OrderNo", objReq.OrderNo);
                    cmd.Parameters.AddWithValue("@AppEmpID", objReq.AppEmpID);
                    cmd.Parameters.AddWithValue("@OperationNo", objReq.OperationNo);
                    cmd.Parameters.AddWithValue("@SupervisorID", objReq.SupervisorID);
                    cmd.Parameters.AddWithValue("@QueryType", "RemoveOpNumber");
                    int i = 0;
                    i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        objResp.vErrorMsg = "The Operation Number has been remove";
                        objResp.vErrorCode = 200;
                    }
                    else
                    {
                        objResp.vErrorMsg = "Operation Number can not be remove";
                        objResp.vErrorCode = 404;
                    }
                }
            }
            catch (Exception exp)
            {
                Logger.WriteLog("Function Name : Fn_Remove_OperationNumberBySupervisor", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
                objResp.vErrorCode = 500;
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Remove_OperationNumberBySupervisor");
            return objResp;
        }


        public List<clsBundleCompile> Fn_Fetch_BundleIDHistoryDetails(clsBundleCompile objReq)
        {
            var objResp = new List<clsBundleCompile>();
            var obj = new clsBundleCompile();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Fetch_BundleIDHistoryDetails");
            try
            {
                if (objReq.BundleID == null || objReq.BundleID == 0)
                {
                    obj.vErrorMsg = "Pass the Valid Bundle ID";
                    obj.vErrorCode = 300;
                }
                else
                {
                    if (Con.State == ConnectionState.Broken)
                    { Con.Close(); }
                    if (Con.State == ConnectionState.Closed)
                    { Con.Open(); }

                    SqlCommand cmd = new SqlCommand("USP_MobileFetchBundleIDHistoryDetails", Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BundleID", objReq.BundleID);
                    cmd.Parameters.AddWithValue("@QueryType", "BundleIDHistoryDetails");

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    int i = 0;

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        while (ds.Tables[0].Rows.Count > i)
                        {
                            obj = new clsBundleCompile();

                            obj.LineName = Convert.ToString(ds.Tables[0].Rows[i]["LineName"]);
                            obj.AppEmpID = Convert.ToInt32(ds.Tables[0].Rows[i]["AppEmpID"]);
                            obj.AppEmpName = Convert.ToString(ds.Tables[0].Rows[i]["AppEmpName"]);
                            obj.OperationNo = Convert.ToInt32(ds.Tables[0].Rows[i]["OperationNo"]);
                            obj.OperationName = Convert.ToString(ds.Tables[0].Rows[i]["OperationName"]);
                            obj.AppStartTime = Convert.ToString(ds.Tables[0].Rows[i]["AppStartTime"]);
                            obj.AppEndTime = Convert.ToString(ds.Tables[0].Rows[i]["AppEndTime"]);

                            obj.vErrorCode = 200;
                            obj.vErrorMsg = "Success";
                            objResp.Add(obj);
                            i++;
                        }
                    }
                    else
                    {
                        obj.vErrorCode = 404;
                        obj.vErrorMsg = "Bundle ID scanning history details are not found.";
                        objResp.Add(obj);
                    }
                }
            }
            catch (Exception exp)
            {
                obj.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Fetch_BundleIDHistoryDetails", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Fetch_BundleIDHistoryDetails");
            return objResp;
        }



        public clsBundleCompile Fn_Fetch_BundleIDBasicDetails(clsBundleCompile objReq)
        {
            var objResp = new clsBundleCompile();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Fetch_BundleIDBasicDetails");
            try
            {
                if (objReq.BundleID == null || objReq.BundleID == 0)
                {
                    objResp.vErrorMsg = "Pass the Valid Bundle ID";
                    objResp.vErrorCode = 300;
                }
                else
                {
                    if (Con.State == ConnectionState.Broken)
                    { Con.Close(); }
                    if (Con.State == ConnectionState.Closed)
                    { Con.Open(); }

                    SqlCommand cmd = new SqlCommand("USP_MobileFetchBundleIDHistoryDetails", Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BundleID", objReq.BundleID);
                    cmd.Parameters.AddWithValue("@QueryType", "BundleIDBasicDetails");

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    int i = 0;

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        objResp.OrderNo = Convert.ToString(ds.Tables[0].Rows[i]["OrderNo"]);
                        objResp.StyleCode = Convert.ToString(ds.Tables[0].Rows[i]["StyleCode"]);
                        objResp.LayID = Convert.ToInt64(ds.Tables[0].Rows[i]["LayID"]);
                        objResp.SubSection = Convert.ToString(ds.Tables[0].Rows[i]["SubSection"]);
                        objResp.ColorName = Convert.ToString(ds.Tables[0].Rows[i]["ColorName"]);
                        objResp.SizeName = Convert.ToString(ds.Tables[0].Rows[i]["SizeName"]);
                        objResp.BundleNo = Convert.ToInt32(ds.Tables[0].Rows[i]["BundleNo"]);
                        objResp.Qty = Convert.ToInt32(ds.Tables[0].Rows[i]["Qty"]);
                        objResp.BundleID = Convert.ToInt64(ds.Tables[0].Rows[i]["BundleID"]);

                        objResp.vErrorCode = 200;
                        objResp.vErrorMsg = "Success";
                    }
                    else
                    {
                        objResp.vErrorCode = 404;
                        objResp.vErrorMsg = "Bundle ID basic details are not found.";
                    }
                }
            }
            catch (Exception exp)
            {
                objResp.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Fetch_BundleIDBasicDetails", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Fetch_BundleIDBasicDetails");
            return objResp;
        }


        public clsMOBEmployee Fn_Fetch_RateEarningsFlag(clsMOBEmployee objReq)
        {
            var objResp = new clsMOBEmployee();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Fetch_RateEarningsFlag");
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_EmployeeMob", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@QueryType", "RateEarningsFlag");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;

                if (ds.Tables[0].Rows.Count > 0)
                {

                    if (ds.Tables[0].Rows[i]["FieldStatus"].ToString() == "Show")
                    {
                        objResp.EarningRateFlag = true;
                    }
                    else
                    {
                        objResp.EarningRateFlag = false;
                    }

                    objResp.vErrorCode = 200;
                    objResp.vErrorMsg = "Success";
                }
                else
                {
                    objResp.vErrorCode = 404;
                    objResp.vErrorMsg = "Earnings or Rate flag value is not found";
                }
            }
            catch (Exception exp)
            {
                objResp.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Fetch_RateEarningsFlag", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Fetch_RateEarningsFlag");
            return objResp;
        }




    }
}