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
    public class DALReport
    {
        SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["BSL"].ConnectionString);

        #region Start Fn_Get_Bundle_Report 06-APR-2026

        public List<clsBundleCompile> Fn_Get_Bundle_Report(clsBundleCompile objReq)
        {
            var objResp = new List<clsBundleCompile>();
            var obj = new clsBundleCompile();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Get_Bundle_Report");
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }


                string strSql = "SELECT BundleID, LayID, BundleNo, SizeName, ColorName, ShadeName, Qty, PlyFrom, PlyTo, LotNo,";
                strSql = strSql + " SubSection, Dispatch, StyleCode, OrderNo, BundleQty, CreatedBy,";
                strSql = strSql + " FORMAT(CreatedOn, 'dd-MMM-yyyy') AS CreatedOn FROM BundleCompile WHERE 1 = 1";

                if (!String.IsNullOrWhiteSpace(objReq.StyleCode))
                {
                    strSql = strSql + " AND StyleCode = @StyleCode";
                }
                if (!String.IsNullOrWhiteSpace(objReq.OrderNo))
                {
                    strSql = strSql + " AND OrderNo = @OrderNo";
                }
                if (!String.IsNullOrWhiteSpace(objReq.SizeName))
                {
                    strSql = strSql + " AND SizeName = @SizeName";
                }
                if (objReq.BundleID != 0 && objReq.BundleID != null)
                {
                    strSql = strSql + " AND BundleID = @BundleID ";
                }
                if (objReq.PlyFrom != 0 && objReq.PlyTo != 0)
                {
                    strSql = strSql + " AND LayID BETWEEN @PlyFrom AND @PlyTo ";
                }
                if (!String.IsNullOrWhiteSpace(objReq.SubSection))
                {
                    strSql = strSql + " AND SubSection = @SubSection ";
                }
                strSql = strSql + " ORDER BY LayID ASC, SubSection ASC, BundleNo ASC ";

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
                if (!String.IsNullOrWhiteSpace(objReq.SizeName))
                {
                    cmd.Parameters.AddWithValue("@SizeName", objReq.SizeName);
                }
                if (objReq.BundleID != 0 && objReq.BundleID != null)
                {
                    cmd.Parameters.AddWithValue("@BundleID", objReq.BundleID);
                }
                if (objReq.PlyFrom != 0 && objReq.PlyTo != 0)
                {
                    cmd.Parameters.AddWithValue("@PlyFrom", objReq.PlyFrom);
                    cmd.Parameters.AddWithValue("@PlyTo", objReq.PlyTo);
                }
                if (!String.IsNullOrWhiteSpace(objReq.SubSection))
                {
                    cmd.Parameters.AddWithValue("@SubSection", objReq.SubSection);
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
                Logger.WriteLog("Function Name : Fn_Get_Bundle_Report", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Get_Bundle_Report");
            return objResp;
        }

        #endregion End Fn_Get_Bundle_Report 06-APR-2026

        public List<clsOperationwiswReport> Fn_Get_OperationWise_OrderDetail_Report(clsOperationwiswReport objReq)
        {
            var objResp = new List<clsOperationwiswReport>();
            var obj = new clsOperationwiswReport();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Get_OperationWise_OrderDetail_Report");
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT FORMAT(BD.AppStartTime, 'dd-MMM-yyyy') AS WorkDate, BC.OrderNo, ED.LineName,";
                strSql = strSql + " BD.AppEmpID AS Code, ED.EmpName, SUM(BC.Qty) AS TotalQty, BC.UpdateType FROM BundleCompile BC";
                strSql = strSql + " INNER JOIN BundleCompileDetail BD ON BC.BundleID = BD.BundleID";
                strSql = strSql + " INNER JOIN EmployeeDetail ED ON ED.Code = BD.AppEmpID WHERE 1=1 AND BD.BundleIDStatus = 'Finished' AND BC.UpdateType <>'NULL' ";
                if (!String.IsNullOrWhiteSpace(objReq.OrderNo))
                {
                    strSql = strSql + " AND BC.OrderNo = @OrderNo ";
                }
                if (!String.IsNullOrWhiteSpace(objReq.Code))
                {
                    strSql = strSql + " AND BD.AppEmpID = @Code ";
                }
                if (!String.IsNullOrWhiteSpace(objReq.LineName))
                {
                    strSql = strSql + " AND ED.LineName = @LineName ";
                }
                if (!String.IsNullOrWhiteSpace(objReq.WorkDate))
                {
                    strSql = strSql + " AND FORMAT(BD.AppStartTime, 'dd-MMM-yyyy') = @WorkDate ";
                }
                strSql = strSql + " GROUP BY  FORMAT(BD.AppStartTime, 'dd-MMM-yyyy'), BC.OrderNo,ED.LineName, BD.AppEmpID, ED.EmpName, BC.UpdateType";
                strSql = strSql + " ORDER BY WorkDate";

                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;

                if (!String.IsNullOrWhiteSpace(objReq.OrderNo))
                {
                    cmd.Parameters.AddWithValue("@OrderNo", objReq.OrderNo);
                }
                if (!String.IsNullOrWhiteSpace(objReq.Code))
                {
                    cmd.Parameters.AddWithValue("@Code", objReq.Code);
                }
                if (!String.IsNullOrWhiteSpace(objReq.LineName))
                {
                    cmd.Parameters.AddWithValue("@LineName", objReq.LineName);
                }
                if (!String.IsNullOrWhiteSpace(objReq.WorkDate))
                {
                    cmd.Parameters.AddWithValue("@WorkDate", objReq.WorkDate);
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsOperationwiswReport();
                        obj.WorkDate = Convert.ToString(ds.Tables[0].Rows[i]["WorkDate"]);
                        obj.OrderNo = Convert.ToString(ds.Tables[0].Rows[i]["OrderNo"]);
                        obj.LineName = Convert.ToString(ds.Tables[0].Rows[i]["LineName"]);
                        obj.Code = Convert.ToString(ds.Tables[0].Rows[i]["Code"]);
                        obj.EmpName = Convert.ToString(ds.Tables[0].Rows[i]["EmpName"]);
                        obj.Qty = Convert.ToInt32(ds.Tables[0].Rows[i]["TotalQty"]);
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
                    obj.vErrorMsg = "No Record found";
                    objResp.Add(obj);
                }

            }
            catch (Exception exp)
            {
                obj.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Get_OperationWise_OrderDetail_Report", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Get_OperationWise_OrderDetail_Report");
            return objResp;
        }

        #region Start Fn_Get_Earning_Report 17-APR-2026

        public List<clsEarningReport> Fn_Get_Earning_Report(clsEarningReport objReq)
        {
            var objResp = new List<clsEarningReport>();
            var obj = new clsEarningReport();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Get_Earning_Report");
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "Select BC.OrderNo, BD.BundleID, BD.OperationNo, BD.SubSection, ED.LineName, BD.AppEmpID, ED.EmpName, FORMAT(BD.AppStartTime, 'dd-MMM-yyyy') AS AppStartTime,";
                strSql = strSql + " FORMAT(BD.AppEndTime, 'dd-MMM-yyyy') AS AppEndTime, BC.Qty As Qty, BD.StdRate, BD.StdMin, BC.UpdateType from BundleCompileDetail BD";
                strSql = strSql + " INNER JOIN EmployeeDetail ED ON BD.AppEmpID = ED.Code";
                strSql = strSql + " INNER JOIN BundleCompile BC ON BC.BundleID = BD.BundleID WHERE 1=1 AND BD.BundleIDStatus = 'Finished'  ";
                if (!String.IsNullOrWhiteSpace(objReq.OrderNo))
                {
                    strSql = strSql + " AND BC.OrderNo = @OrderNo ";
                }
                if (!String.IsNullOrWhiteSpace(objReq.Code))
                {
                    strSql = strSql + " AND BD.AppEmpID = @Code ";
                }
                if (!String.IsNullOrWhiteSpace(objReq.LineName))
                {
                    strSql = strSql + " AND ED.LineName = @LineName ";
                }
                if (!String.IsNullOrWhiteSpace(objReq.StartDate) && String.IsNullOrWhiteSpace(objReq.EndDate))
                {
                    strSql = strSql + " AND BD.AppStartTime = @StartDate ";
                }
                if (!String.IsNullOrWhiteSpace(objReq.StartDate) && !String.IsNullOrWhiteSpace(objReq.EndDate))
                {
                    strSql = strSql + " AND FORMAT(BD.AppStartTime, 'dd-MMM-yyyy') BETWEEN '" + objReq.StartDate + "' AND '" + objReq.EndDate + "'";
                }


                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;

                if (!String.IsNullOrWhiteSpace(objReq.OrderNo))
                {
                    cmd.Parameters.AddWithValue("@OrderNo", objReq.OrderNo);
                }
                if (!String.IsNullOrWhiteSpace(objReq.Code))
                {
                    cmd.Parameters.AddWithValue("@Code", objReq.Code);
                }
                if (!String.IsNullOrWhiteSpace(objReq.LineName))
                {
                    cmd.Parameters.AddWithValue("@LineName", objReq.LineName);
                }
                if (!String.IsNullOrWhiteSpace(objReq.StartDate))
                {
                    cmd.Parameters.AddWithValue("@StartDate", objReq.StartDate);
                }


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsEarningReport();
                        obj.OrderNo = Convert.ToString(ds.Tables[0].Rows[i]["OrderNo"]);
                        obj.BundleID = Convert.ToInt64(ds.Tables[0].Rows[i]["BundleID"]);
                        obj.OpNo = Convert.ToInt32(ds.Tables[0].Rows[i]["OperationNo"]);
                        obj.SubSection = Convert.ToString(ds.Tables[0].Rows[i]["SubSection"]);
                        obj.LineName = Convert.ToString(ds.Tables[0].Rows[i]["LineName"]);
                        obj.Code = Convert.ToString(ds.Tables[0].Rows[i]["AppEmpID"]);
                        obj.EmpName = Convert.ToString(ds.Tables[0].Rows[i]["EmpName"]);
                        obj.StartDate = Convert.ToString(ds.Tables[0].Rows[i]["AppStartTime"]);
                        obj.EndDate = Convert.ToString(ds.Tables[0].Rows[i]["AppEndTime"]);
                        obj.Qty = Convert.ToInt32(ds.Tables[0].Rows[i]["Qty"]);
                        string StdRate = Convert.ToString(ds.Tables[0].Rows[i]["StdRate"]);
                        if(StdRate != "")
                        {
                            obj.StdRate = Convert.ToDecimal(ds.Tables[0].Rows[i]["StdRate"]);
                        }

                        string StdMin = Convert.ToString(ds.Tables[0].Rows[i]["StdMin"]);
                        if(StdMin != "")
                        {
                            obj.StdMin = Convert.ToDecimal(ds.Tables[0].Rows[i]["StdMin"]);
                        }
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
                    obj.vErrorMsg = "No Record found";
                    objResp.Add(obj);
                }

            }
            catch (Exception exp)
            {
                obj.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Get_Earning_Report", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Get_Earning_Report");
            return objResp;
        }

        #endregion End Fn_Get_OperationWise_Earning_Report 17-APR-2026

        #region Start Fn_Get_EfficiencyReport 11-MAY-2026

        public List<clsEfficiencyReportResp> Fn_Get_EfficiencyReport(clsEfficiencyReportReq objReq)
        {
            var objResp = new List<clsEfficiencyReportResp>();
            var obj = new clsEfficiencyReportResp();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Get_EfficiencyReport");
            try
            {
                if (objReq.Month == 0)
                {
                    obj.vErrorCode = 400;
                    obj.vErrorMsg = "Please send Month";
                    objResp.Add(obj);
                }
                else if (objReq.Year == 0)
                {
                    obj.vErrorCode = 400;
                    obj.vErrorMsg = "Please send Year";
                    objResp.Add(obj);
                }
                else
                {
                    if (Con.State == ConnectionState.Broken)
                    { Con.Close(); }
                    if (Con.State == ConnectionState.Closed)
                    { Con.Open(); }

                    SqlCommand cmd = new SqlCommand("USP_Effiency_Report", Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Month", objReq.Month);
                    cmd.Parameters.AddWithValue("@Year", objReq.Year);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    int i = 0;
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataTable dt = ds.Tables[0];

                        while (dt.Rows.Count > i)
                        {
                            obj = new clsEfficiencyReportResp();

                            obj.Code = Convert.ToString(dt.Rows[i]["AppEmpID"]);
                            obj.EmpName = Convert.ToString(dt.Rows[i]["EmpName"]);

                            for (int col = 2; col < dt.Columns.Count; col++)
                            {
                                string columnName = dt.Columns[col].ColumnName;
                                string value = Convert.ToString(dt.Rows[i][col]);

                                obj.DynamicColumns.Add(columnName, value);
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
            }
            catch (Exception exp)
            {
                obj.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Get_EfficiencyReport", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Get_EfficiencyReport");
            return objResp;
        }

        #endregion End Fn_Get_EfficiencyReport 11-MAY-2026

        #region Start Fn_Get_Piece_Rate_Report 20-May-2026

        public List<clsPieceRateReportResp> Fn_Get_Piece_Rate_Report(clsPieceRateReportReq objReq)
        {
            var objResp = new List<clsPieceRateReportResp>();
            var obj = new clsPieceRateReportResp();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Get_Piece_Rate_Report");
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT LineName, StyleCode, OrderNo, SubSection, Code, EmpName, OperationNo, Descriptions, FORMAT(CAST(WorkDate AS DATE), 'dd-MMM-yyyy') AS WorkDate,";
                strSql = strSql + " SUM(Qty) AS Qty, StdRate, SUM(Qty) * StdRate AS Amount,  UpdateType, BundleIDStatus, COUNT(*) OVER() AS TotalRows,";
                strSql = strSql + " SUM(SUM(Qty) * StdRate) OVER() AS TotalAMT,";
                strSql = strSql + " ( SELECT COUNT(DISTINCT Code) FROM vPieceRateReport WHERE 1=1";
                if (!String.IsNullOrWhiteSpace(objReq.StyleCode))
                {
                    strSql = strSql + " AND StyleCode = @StyleCode ";
                }
                if (!String.IsNullOrWhiteSpace(objReq.Code))
                {
                    strSql = strSql + " AND Code = @Code ";
                }
                if (!String.IsNullOrWhiteSpace(objReq.LineName))
                {
                    strSql = strSql + " AND LineName = @LineName ";
                }
                if (!String.IsNullOrWhiteSpace(objReq.StartDate) && String.IsNullOrWhiteSpace(objReq.EndDate))
                {
                    strSql = strSql + " AND WorkDate = @StartDate ";
                }
                if (!String.IsNullOrWhiteSpace(objReq.StartDate) && !String.IsNullOrWhiteSpace(objReq.EndDate))
                {
                    strSql = strSql + " AND CAST(WorkDate AS DATE) BETWEEN '" + objReq.StartDate + "' AND '" + objReq.EndDate + "'";
                }
                strSql = strSql + " ) AS TotalEmp FROM vPieceRateReport WHERE 1=1";             

                if (!String.IsNullOrWhiteSpace(objReq.StyleCode))
                {
                    strSql = strSql + " AND StyleCode = @StyleCode ";
                }
                if (!String.IsNullOrWhiteSpace(objReq.Code))
                {
                    strSql = strSql + " AND Code = @Code ";
                }
                if (!String.IsNullOrWhiteSpace(objReq.LineName))
                {
                    strSql = strSql + " AND LineName = @LineName ";
                }
                if (!String.IsNullOrWhiteSpace(objReq.StartDate) && String.IsNullOrWhiteSpace(objReq.EndDate))
                {
                    strSql = strSql + " AND WorkDate = @StartDate ";
                }
                if (!String.IsNullOrWhiteSpace(objReq.StartDate) && !String.IsNullOrWhiteSpace(objReq.EndDate))
                {
                    strSql = strSql + " AND CAST(WorkDate AS DATE) BETWEEN '" + objReq.StartDate + "' AND '" + objReq.EndDate + "'";
                }
                strSql = strSql + " GROUP BY LineName, StyleCode,  OrderNo, SubSection, Code, EmpName, OperationNo,  Descriptions,";
                strSql = strSql + " CAST(WorkDate AS DATE), StdRate, UpdateType, BundleIDStatus ";
                strSql = strSql + " ORDER BY " + objReq.OrderBy + " , CAST(WorkDate AS DATE) ASC ";
                strSql = strSql + " OFFSET (@PageNumber - 1) * @PageSize ROWS ";
                strSql = strSql + " FETCH NEXT @PageSize ROWS ONLY ";

                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@PageNumber", objReq.PageNumber);
                cmd.Parameters.AddWithValue("@PageSize", objReq.PageSize);

                if (!String.IsNullOrWhiteSpace(objReq.StyleCode))
                {
                    cmd.Parameters.AddWithValue("@StyleCode", objReq.StyleCode);
                }
                if (!String.IsNullOrWhiteSpace(objReq.Code))
                {
                    cmd.Parameters.AddWithValue("@Code", objReq.Code);
                }
                if (!String.IsNullOrWhiteSpace(objReq.LineName))
                {
                    cmd.Parameters.AddWithValue("@LineName", objReq.LineName);
                }
                if (!String.IsNullOrWhiteSpace(objReq.StartDate) && String.IsNullOrWhiteSpace(objReq.EndDate))
                {
                    cmd.Parameters.AddWithValue("@StartDate", objReq.StartDate);
                }


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsPieceRateReportResp();
                        obj.LineName = Convert.ToString(ds.Tables[0].Rows[i]["LineName"]);
                        obj.StyleCode = Convert.ToString(ds.Tables[0].Rows[i]["StyleCode"]);
                        obj.OrderNo = Convert.ToString(ds.Tables[0].Rows[i]["OrderNo"]);
                        obj.SubSection = Convert.ToString(ds.Tables[0].Rows[i]["SubSection"]);
                        obj.Code = Convert.ToString(ds.Tables[0].Rows[i]["Code"]);
                        obj.EmpName = Convert.ToString(ds.Tables[0].Rows[i]["EmpName"]);
                        obj.OpNo = Convert.ToInt32(ds.Tables[0].Rows[i]["OperationNo"]);
                        obj.OpName = Convert.ToString(ds.Tables[0].Rows[i]["Descriptions"]);
                        obj.WorkDate = Convert.ToString(ds.Tables[0].Rows[i]["WorkDate"]);
                        obj.Qty = Convert.ToDouble(ds.Tables[0].Rows[i]["Qty"]);
                        obj.Rate = Convert.ToDouble(ds.Tables[0].Rows[i]["StdRate"]);
                        obj.Amount = Convert.ToDouble(ds.Tables[0].Rows[i]["Amount"]);
                        obj.UpdateType = Convert.ToString(ds.Tables[0].Rows[i]["UpdateType"]);
                        obj.TotalRows = Convert.ToInt64(ds.Tables[0].Rows[i]["TotalRows"]);
                        obj.TotalAmount = Convert.ToDouble(ds.Tables[0].Rows[i]["TotalAMT"]);
                        obj.TotalEmp = Convert.ToInt32(ds.Tables[0].Rows[i]["TotalEmp"]);
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
                Logger.WriteLog("Function Name : Fn_Get_Piece_Rate_Report", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Get_Piece_Rate_Report");
            return objResp;
        }

        #endregion End Fn_Get_Piece_Rate_Report 20-May-2026

        #region Start Fn_Get_Peice_Rate_Incentive 21-May-2026

        public List<clsPieceRateIncentive> Fn_Get_Peice_Rate_Incentive(clsPieceRateReportReq objReq)
        {
            var objResp = new List<clsPieceRateIncentive>();
            var obj = new clsPieceRateIncentive();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Get_Peice_Rate_Incentive");
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT Code, EmpName, MIN(WorkDate) AS FromDate,  MAX(WorkDate) AS ToDate, ";
                strSql = strSql + " COUNT(DISTINCT CAST(WorkDate AS DATE)) AS WorkingDays, SUM(Qty) AS TotalQty, ";
                strSql = strSql + " SUM(Qty * StdRate) AS TotalEarning, ";
                strSql = strSql + " SUM(Qty * StdRate) * 1.0 / NULLIF(COUNT(DISTINCT CAST(WorkDate AS DATE)), 0) AS EarningPerDay FROM vIncentive WHERE 1=1 ";                
                if (!String.IsNullOrWhiteSpace(objReq.Code))
                {
                    strSql = strSql + " AND Code = @Code ";
                }
                if (!String.IsNullOrWhiteSpace(objReq.StartDate) && String.IsNullOrWhiteSpace(objReq.EndDate))
                {
                    strSql = strSql + " AND WorkDate = @StartDate ";
                }
                if (!String.IsNullOrWhiteSpace(objReq.StartDate) && !String.IsNullOrWhiteSpace(objReq.EndDate))
                {
                    strSql = strSql + " AND CAST(WorkDate AS DATE) BETWEEN '" + objReq.StartDate + "' AND '" + objReq.EndDate + "'";
                }
                strSql = strSql + " GROUP BY Code, EmpName ";
                strSql = strSql + " ORDER BY EmpName ";
                strSql = strSql + " OFFSET (@PageNumber - 1) * @PageSize ROWS ";
                strSql = strSql + " FETCH NEXT @PageSize ROWS ONLY ";

                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@PageNumber", objReq.PageNumber);
                cmd.Parameters.AddWithValue("@PageSize", objReq.PageSize);

                if (!String.IsNullOrWhiteSpace(objReq.Code))
                {
                    cmd.Parameters.AddWithValue("@Code", objReq.Code);
                }
                if (!String.IsNullOrWhiteSpace(objReq.StartDate) && String.IsNullOrWhiteSpace(objReq.EndDate))
                {
                    cmd.Parameters.AddWithValue("@StartDate", objReq.StartDate);
                }


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsPieceRateIncentive();
                        obj.Code = Convert.ToString(ds.Tables[0].Rows[i]["Code"]);
                        obj.EmpName = Convert.ToString(ds.Tables[0].Rows[i]["EmpName"]);
                        obj.FromDate = Convert.ToString(ds.Tables[0].Rows[i]["FromDate"]);
                        obj.ToDate = Convert.ToString(ds.Tables[0].Rows[i]["ToDate"]);
                        obj.WorkingDays = Convert.ToInt64(ds.Tables[0].Rows[i]["WorkingDays"]);
                        obj.TotalQty = Convert.ToInt64(ds.Tables[0].Rows[i]["TotalQty"]);
                        obj.EarningPerDay = Convert.ToDouble(ds.Tables[0].Rows[i]["EarningPerDay"]);
                        obj.TotalEarning = Convert.ToDouble(ds.Tables[0].Rows[i]["TotalEarning"]);
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
                Logger.WriteLog("Function Name : Fn_Get_Peice_Rate_Incentive", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Get_Peice_Rate_Incentive");
            return objResp;
        }

        #endregion End Fn_Get_Peice_Rate_Incentive 21-May-2026

        #region Start Fn_Get_Pending_BundleStatus 26-May-2026

        public List<clsBundleStatusReportResp> Fn_Get_Pending_BundleStatus(clsBundleStatusReportReq objReq)
        {
            var objResp = new List<clsBundleStatusReportResp>();
            var obj = new clsBundleStatusReportResp();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Get_Pending_BundleStatus");
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT OpNo, OpName, BundleID, BundleNo, SizeName, ColorName, ShadeName, Qty, PlyFrom, PlyTo,";
                strSql = strSql + " LotNo, SubSection, StyleCode, OrderNo, AppEmpID, EmpName, AppStartTime, AppEndTime, BundleStatus,";
                strSql = strSql + " IsPilot, COUNT(*) OVER() AS TotalRows FROM vPending_BundleStatus WHERE 1=1";                                
                if (!String.IsNullOrWhiteSpace(objReq.OrderNo))
                {
                    strSql = strSql + " AND OrderNo = @OrderNo ";
                }
                if (objReq.AppEmpID != 0)
                {
                    strSql = strSql + " AND AppEmpID = @AppEmpID ";
                }
                if (!String.IsNullOrWhiteSpace(objReq.SizeName))
                {
                    strSql = strSql + " AND SizeName = @SizeName ";
                }
                if (!String.IsNullOrWhiteSpace(objReq.SubSection))
                {
                    strSql = strSql + " AND SubSection = @SubSection ";
                }
                
                strSql = strSql + " GROUP BY OpNo, OpName, BundleID, BundleNo, SizeName, ColorName, ShadeName, Qty, PlyFrom, PlyTo, LotNo, ";
                strSql = strSql + " SubSection, StyleCode, OrderNo, AppEmpID, EmpName, AppStartTime, AppEndTime, BundleStatus, IsPilot";
                strSql = strSql + " ORDER BY BundleID ASC ";
                //strSql = strSql + " ORDER BY BundleID, BundleNo, SizeName, ColorName ";
                strSql = strSql + " OFFSET (@PageNumber - 1) * @PageSize ROWS ";
                strSql = strSql + " FETCH NEXT @PageSize ROWS ONLY ";

                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@PageNumber", objReq.PageNumber);
                cmd.Parameters.AddWithValue("@PageSize", objReq.PageSize);
                
                if (!String.IsNullOrWhiteSpace(objReq.OrderNo))
                {
                    cmd.Parameters.AddWithValue("@OrderNo", objReq.OrderNo);
                }
                if (objReq.AppEmpID != 0)
                {
                    cmd.Parameters.AddWithValue("@AppEmpID", objReq.AppEmpID);
                }
                if (!String.IsNullOrWhiteSpace(objReq.SizeName))
                {
                    cmd.Parameters.AddWithValue("@SizeName", objReq.SizeName);
                }
                if (!String.IsNullOrWhiteSpace(objReq.SubSection))
                {
                    cmd.Parameters.AddWithValue("@SubSection", objReq.SubSection);
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsBundleStatusReportResp();
                        obj.OpNo = Convert.ToInt32(ds.Tables[0].Rows[i]["OpNo"]);
                        obj.OpName = Convert.ToString(ds.Tables[0].Rows[i]["OpName"]);
                        obj.BundleID = Convert.ToInt64(ds.Tables[0].Rows[i]["BundleID"]);
                        obj.BundleNo = Convert.ToInt32(ds.Tables[0].Rows[i]["BundleNo"]);
                        obj.SizeName = Convert.ToString(ds.Tables[0].Rows[i]["SizeName"]);
                        obj.ColorName = Convert.ToString(ds.Tables[0].Rows[i]["ColorName"]);
                        obj.ShadeName = Convert.ToString(ds.Tables[0].Rows[i]["ShadeName"]);
                        obj.Qty = Convert.ToInt32(ds.Tables[0].Rows[i]["Qty"]);
                        obj.PlyFrom = Convert.ToInt32(ds.Tables[0].Rows[i]["PlyFrom"]);
                        obj.PlyTo = Convert.ToInt32(ds.Tables[0].Rows[i]["PlyTo"]);
                        obj.LotNo = Convert.ToInt32(ds.Tables[0].Rows[i]["LotNo"]);
                        obj.SubSection = Convert.ToString(ds.Tables[0].Rows[i]["SubSection"]);
                        obj.StyleCode = Convert.ToString(ds.Tables[0].Rows[i]["StyleCode"]);
                        obj.OrderNo = Convert.ToString(ds.Tables[0].Rows[i]["OrderNo"]);
                        obj.EmpName = Convert.ToString(ds.Tables[0].Rows[i]["EmpName"]);
                        string strAppEmpID = Convert.ToString(ds.Tables[0].Rows[i]["AppEmpID"]);
                        if(strAppEmpID != "")
                        {
                            obj.AppEmpID = Convert.ToInt32(ds.Tables[0].Rows[i]["AppEmpID"]);
                        }
                       
                        obj.AppStartTime = Convert.ToString(ds.Tables[0].Rows[i]["AppStartTime"]);
                        obj.AppEndTime = Convert.ToString(ds.Tables[0].Rows[i]["AppEndTime"]);
                        obj.BundleStatus = Convert.ToString(ds.Tables[0].Rows[i]["BundleStatus"]);
                        obj.TotalRows = Convert.ToInt64(ds.Tables[0].Rows[i]["TotalRows"]);
                        obj.IsPilot = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsPilot"]);
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
                Logger.WriteLog("Function Name : Fn_Get_Pending_BundleStatus", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Get_Pending_BundleStatus");
            return objResp;
        }

        #endregion End Fn_Get_Pending_BundleStatus 26-May-2026

        #region Start Fn_Get_Finish_BundleStatus 26-May-2026

        public List<clsBundleStatusReportResp> Fn_Get_Finish_BundleStatus(clsBundleStatusReportReq objReq)
        {
            var objResp = new List<clsBundleStatusReportResp>();
            var obj = new clsBundleStatusReportResp();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Get_Finish_BundleStatus");
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT OpNo, OpName, BundleID, BundleNo, SizeName, ColorName, ShadeName, Qty, PlyFrom, PlyTo,";
                strSql = strSql + " LotNo, SubSection, StyleCode, OrderNo, AppEmpID, EmpName, FORMAT(AppStartTime, 'dd-MMM-yyyy') AS AppStartTime, FORMAT(AppEndTime, 'dd-MMM-yyyy') AS AppEndTime,";
                strSql = strSql + " BundleStatus, SupervisorID, SupervisorName, FORMAT(AssignedDate, 'dd-MMM-yyyy') AS AssignedDate,";
                strSql = strSql + " IsPilot, COUNT(*) OVER() AS TotalRows FROM vFinish_BundleStatus WHERE 1=1";
                
                if (!String.IsNullOrWhiteSpace(objReq.OrderNo))
                {
                    strSql = strSql + " AND OrderNo = @OrderNo ";
                }
                if (objReq.AppEmpID != 0)
                {
                    strSql = strSql + " AND AppEmpID = @AppEmpID ";
                }
                if (!String.IsNullOrWhiteSpace(objReq.SizeName))
                {
                    strSql = strSql + " AND SizeName = @SizeName ";
                }
                if (!String.IsNullOrWhiteSpace(objReq.SubSection))
                {
                    strSql = strSql + " AND SubSection = @SubSection ";
                }                
                strSql = strSql + " GROUP BY OpNo, OpName, BundleID, BundleNo, SizeName, ColorName, ShadeName, Qty, PlyFrom, PlyTo,";
                strSql = strSql + " LotNo, SubSection, StyleCode, OrderNo, AppEmpID, EmpName, AppStartTime, AppEndTime,";
                strSql = strSql + " BundleStatus, SupervisorID, SupervisorName, AssignedDate, IsPilot";
                strSql = strSql + " ORDER BY BundleID ASC ";
                strSql = strSql + " OFFSET (@PageNumber - 1) * @PageSize ROWS ";
                strSql = strSql + " FETCH NEXT @PageSize ROWS ONLY ";

                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@PageNumber", objReq.PageNumber);
                cmd.Parameters.AddWithValue("@PageSize", objReq.PageSize);

                if (!String.IsNullOrWhiteSpace(objReq.OrderNo))
                {
                    cmd.Parameters.AddWithValue("@OrderNo", objReq.OrderNo);
                }
                if (objReq.AppEmpID != 0)
                {
                    cmd.Parameters.AddWithValue("@AppEmpID", objReq.AppEmpID);
                }
                if (!String.IsNullOrWhiteSpace(objReq.SizeName))
                {
                    cmd.Parameters.AddWithValue("@SizeName", objReq.SizeName);
                }
                if (!String.IsNullOrWhiteSpace(objReq.SubSection))
                {
                    cmd.Parameters.AddWithValue("@SubSection", objReq.SubSection);
                }
                
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsBundleStatusReportResp();
                        obj.OpNo = Convert.ToInt32(ds.Tables[0].Rows[i]["OpNo"]);
                        obj.OpName = Convert.ToString(ds.Tables[0].Rows[i]["OpName"]);
                        obj.BundleID = Convert.ToInt64(ds.Tables[0].Rows[i]["BundleID"]);
                        obj.BundleNo = Convert.ToInt32(ds.Tables[0].Rows[i]["BundleNo"]);
                        obj.SizeName = Convert.ToString(ds.Tables[0].Rows[i]["SizeName"]);
                        obj.ColorName = Convert.ToString(ds.Tables[0].Rows[i]["ColorName"]);
                        obj.ShadeName = Convert.ToString(ds.Tables[0].Rows[i]["ShadeName"]);
                        obj.Qty = Convert.ToInt32(ds.Tables[0].Rows[i]["Qty"]);
                        obj.PlyFrom = Convert.ToInt32(ds.Tables[0].Rows[i]["PlyFrom"]);
                        obj.PlyTo = Convert.ToInt32(ds.Tables[0].Rows[i]["PlyTo"]);
                        obj.LotNo = Convert.ToInt32(ds.Tables[0].Rows[i]["LotNo"]);
                        obj.SubSection = Convert.ToString(ds.Tables[0].Rows[i]["SubSection"]);
                        obj.StyleCode = Convert.ToString(ds.Tables[0].Rows[i]["StyleCode"]);
                        obj.OrderNo = Convert.ToString(ds.Tables[0].Rows[i]["OrderNo"]);
                        obj.EmpName = Convert.ToString(ds.Tables[0].Rows[i]["EmpName"]);
                        string strAppEmpID = Convert.ToString(ds.Tables[0].Rows[i]["AppEmpID"]);
                        if (strAppEmpID != "")
                        {
                            obj.AppEmpID = Convert.ToInt32(ds.Tables[0].Rows[i]["AppEmpID"]);
                        }

                        obj.AppStartTime = Convert.ToString(ds.Tables[0].Rows[i]["AppStartTime"]);
                        obj.AppEndTime = Convert.ToString(ds.Tables[0].Rows[i]["AppEndTime"]);
                        obj.BundleStatus = Convert.ToString(ds.Tables[0].Rows[i]["BundleStatus"]);
                        string strSupervisorID = Convert.ToString(ds.Tables[0].Rows[i]["SupervisorID"]);
                        if (strSupervisorID != "")
                        {
                            obj.SupervisorID = Convert.ToInt32(ds.Tables[0].Rows[i]["SupervisorID"]);
                        }
                        obj.SupervisorName = Convert.ToString(ds.Tables[0].Rows[i]["SupervisorName"]);
                        obj.AssignedDate = Convert.ToString(ds.Tables[0].Rows[i]["AssignedDate"]);
                        obj.TotalRows = Convert.ToInt64(ds.Tables[0].Rows[i]["TotalRows"]);
                        obj.IsPilot = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsPilot"]);
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
                Logger.WriteLog("Function Name : Fn_Get_Finish_BundleStatus", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Get_Finish_BundleStatus");
            return objResp;
        }

        #endregion End Fn_Get_Finish_BundleStatus 26-May-2026

        #region Start Fn_Set_AS_Pilot 02-Jun-2026

        public clsPilot Fn_Set_AS_Pilot(clsPilot objReq)
        {
            var objResp = new clsPilot();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Set_AS_Pilot");
            try
            {
                
                if (String.IsNullOrWhiteSpace(objReq.BundleList))
                {
                    objResp.vErrorCode = 400;
                    objResp.vErrorMsg = "BundleList is empty";
                }                
                else
                {
                    string text = objReq.BundleList;

                    string[] _List = text.Split(',');

                    if(Con.State == ConnectionState.Broken) { Con.Close(); }
                    if (Con.State == ConnectionState.Closed) { Con.Open(); }

                    foreach (string list in _List)
                    {
                        Int64 BundleId = 0;
                        int OPNo = 0;
                        string opList = list;

                        string[] _opList = opList.Split('-');
                        BundleId = Convert.ToInt64(_opList[0]);
                        OPNo = Convert.ToInt32(_opList[1]);

                        string strSql = "UPDATE BundleCompileDetail SET IsPilot = 1 WHERE BundleID=" + BundleId + " AND OperationNo=" + OPNo + "";
                        SqlCommand cmd = new SqlCommand(strSql, Con);
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
                            objResp.vErrorMsg = "Pilot updating failed";
                        }
                    }

                    
                }
            }
            catch (Exception exp)
            {
                objResp.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Set_AS_Pilot", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Set_AS_Pilot");
            return objResp;
        }

        #endregion End Fn_Set_AS_Pilot 02-Jun-2026


        #region Start Fn_Add_Multiple_Manual_Entry 04-JUN-2026

        public clsManualEntry Fn_Add_Multiple_Manual_Entry(clsManualEntry objReq)
        {
            var objResp = new clsManualEntry();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Add_Multiple_Manual_Entry");
            try
            {
                
                string text = objReq.BundleList;

                string[] _List = text.Split(',');

                if (Con.State == ConnectionState.Broken) { Con.Close(); }
                if (Con.State == ConnectionState.Closed) { Con.Open(); }

                foreach (string list in _List)
                {
                    Int64 BundleId = 0;
                    int OPNo = 0;
                    string opList = list;

                    string[] _opList = opList.Split('-');
                    BundleId = Convert.ToInt64(_opList[0]);
                    OPNo = Convert.ToInt32(_opList[1]);

                    SqlCommand cmd = new SqlCommand("USP_BUNDLE_LAYER", Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BundleID", BundleId);
                    cmd.Parameters.AddWithValue("@AppEmpID", objReq.AppEmpID);
                    cmd.Parameters.AddWithValue("@OpNo", OPNo);
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
            }
            catch (Exception exp)
            {
                objResp.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Add_Multiple_Manual_Entry", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Add_Multiple_Manual_Entry");
            return objResp;
        }

        #endregion End Fn_Add_Multiple_Manual_Entry 04-JUN-2026

        #region Start Fn_Get_Pilot_Report 08-JUN_2026
        public List<clsBundleStatusReportResp> Fn_Get_Pilot_Report(clsBundleStatusReportReq objReq)
        {
            var objResp = new List<clsBundleStatusReportResp>();
            var obj = new clsBundleStatusReportResp();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Get_Pilot_Report");
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "Select BundleID, OpNo, OpName, BundleNo, SizeName, ColorName, ShadeName, Qty, PlyFrom, PlyTo, LotNo,";
                strSql = strSql + " SubSection, StyleCode, OrderNo, AppEmpID, EmpName, AppStartTime, AppEndTime, BundleStatus,";
                strSql = strSql + " SupervisorID, SupervisorName, AssignedDate, IsPilot, COUNT(*) OVER() AS TotalRows From vPilotReport WHERE 1=1";

                if (!String.IsNullOrWhiteSpace(objReq.OrderNo))
                {
                    strSql = strSql + " AND OrderNo = @OrderNo ";
                }
                if (objReq.AppEmpID != 0)
                {
                    strSql = strSql + " AND AppEmpID = @AppEmpID ";
                }
                if (!String.IsNullOrWhiteSpace(objReq.SizeName))
                {
                    strSql = strSql + " AND SizeName = @SizeName ";
                }
                if (!String.IsNullOrWhiteSpace(objReq.SubSection))
                {
                    strSql = strSql + " AND SubSection = @SubSection ";
                }
                
                strSql = strSql + " ORDER BY BundleID ASC ";
                strSql = strSql + " OFFSET (@PageNumber - 1) * @PageSize ROWS ";
                strSql = strSql + " FETCH NEXT @PageSize ROWS ONLY ";

                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@PageNumber", objReq.PageNumber);
                cmd.Parameters.AddWithValue("@PageSize", objReq.PageSize);

                if (!String.IsNullOrWhiteSpace(objReq.OrderNo))
                {
                    cmd.Parameters.AddWithValue("@OrderNo", objReq.OrderNo);
                }
                if (objReq.AppEmpID != 0)
                {
                    cmd.Parameters.AddWithValue("@AppEmpID", objReq.AppEmpID);
                }
                if (!String.IsNullOrWhiteSpace(objReq.SizeName))
                {
                    cmd.Parameters.AddWithValue("@SizeName", objReq.SizeName);
                }
                if (!String.IsNullOrWhiteSpace(objReq.SubSection))
                {
                    cmd.Parameters.AddWithValue("@SubSection", objReq.SubSection);
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsBundleStatusReportResp();
                        obj.OpNo = Convert.ToInt32(ds.Tables[0].Rows[i]["OpNo"]);
                        obj.OpName = Convert.ToString(ds.Tables[0].Rows[i]["OpName"]);
                        obj.BundleID = Convert.ToInt64(ds.Tables[0].Rows[i]["BundleID"]);
                        obj.BundleNo = Convert.ToInt32(ds.Tables[0].Rows[i]["BundleNo"]);
                        obj.SizeName = Convert.ToString(ds.Tables[0].Rows[i]["SizeName"]);
                        obj.ColorName = Convert.ToString(ds.Tables[0].Rows[i]["ColorName"]);
                        obj.ShadeName = Convert.ToString(ds.Tables[0].Rows[i]["ShadeName"]);
                        obj.Qty = Convert.ToInt32(ds.Tables[0].Rows[i]["Qty"]);
                        obj.PlyFrom = Convert.ToInt32(ds.Tables[0].Rows[i]["PlyFrom"]);
                        obj.PlyTo = Convert.ToInt32(ds.Tables[0].Rows[i]["PlyTo"]);
                        obj.LotNo = Convert.ToInt32(ds.Tables[0].Rows[i]["LotNo"]);
                        obj.SubSection = Convert.ToString(ds.Tables[0].Rows[i]["SubSection"]);
                        obj.StyleCode = Convert.ToString(ds.Tables[0].Rows[i]["StyleCode"]);
                        obj.OrderNo = Convert.ToString(ds.Tables[0].Rows[i]["OrderNo"]);
                        obj.EmpName = Convert.ToString(ds.Tables[0].Rows[i]["EmpName"]);
                        string strAppEmpID = Convert.ToString(ds.Tables[0].Rows[i]["AppEmpID"]);
                        if (strAppEmpID != "")
                        {
                            obj.AppEmpID = Convert.ToInt32(ds.Tables[0].Rows[i]["AppEmpID"]);
                        }

                        obj.AppStartTime = Convert.ToString(ds.Tables[0].Rows[i]["AppStartTime"]);
                        obj.AppEndTime = Convert.ToString(ds.Tables[0].Rows[i]["AppEndTime"]);
                        obj.BundleStatus = Convert.ToString(ds.Tables[0].Rows[i]["BundleStatus"]);
                        string strSupervisorID = Convert.ToString(ds.Tables[0].Rows[i]["SupervisorID"]);
                        if (strSupervisorID != "")
                        {
                            obj.SupervisorID = Convert.ToInt32(ds.Tables[0].Rows[i]["SupervisorID"]);
                        }
                        obj.SupervisorName = Convert.ToString(ds.Tables[0].Rows[i]["SupervisorName"]);
                        obj.AssignedDate = Convert.ToString(ds.Tables[0].Rows[i]["AssignedDate"]);
                        obj.TotalRows = Convert.ToInt64(ds.Tables[0].Rows[i]["TotalRows"]);
                        obj.IsPilot = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsPilot"]);
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
                Logger.WriteLog("Function Name : Fn_Get_Pilot_Report", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Get_Pilot_Report");
            return objResp;
        }

        #endregion End Fn_Get_Pilot_Report 08-JUN_2026
    }
}