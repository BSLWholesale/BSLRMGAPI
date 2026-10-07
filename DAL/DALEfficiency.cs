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
    public class DALEfficiency
    {
        SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["BSL"].ConnectionString);

        public List<clsEmployeeWiseEfficiency> Fn_Get_EmployeeWiseEfficiency(clsEfficiencyReq objReq)
        {
            var objResp = new List<clsEmployeeWiseEfficiency>();
            var obj = new clsEmployeeWiseEfficiency();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Get_EmployeeWiseEfficiency");
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }
               
                SqlCommand cmd = new SqlCommand("USP_EmployeeWise_Effiency_Report", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StartDate", objReq.StartDate);
                cmd.Parameters.AddWithValue("@EndDate", objReq.EndDate);
                cmd.Parameters.AddWithValue("@OrderNo", objReq.OrderNo);
                cmd.Parameters.AddWithValue("@LineName", objReq.LineName);
                cmd.Parameters.AddWithValue("@EmpID", objReq.Code);
                cmd.Parameters.AddWithValue("@QueryType", objReq.QueryType);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsEmployeeWiseEfficiency();
                        obj.Division = Convert.ToString(ds.Tables[0].Rows[i]["Division"]);
                        obj.LineName = Convert.ToString(ds.Tables[0].Rows[i]["LineName"]);
                        obj.WorkDate = Convert.ToString(ds.Tables[0].Rows[i]["WorkDate"]);
                        obj.Code = Convert.ToInt64(ds.Tables[0].Rows[i]["Code"]);
                        obj.EmpName = Convert.ToString(ds.Tables[0].Rows[i]["EmpName"]);
                        obj.OrderNo = Convert.ToString(ds.Tables[0].Rows[i]["OrderNo"]);
                        obj.OpNo = Convert.ToInt32(ds.Tables[0].Rows[i]["OpNo"]);
                        obj.Descriptions = Convert.ToString(ds.Tables[0].Rows[i]["Descriptions"]);
                        obj.StdRate = Convert.ToDecimal(ds.Tables[0].Rows[i]["StdRate"]);
                        obj.StdMin = Convert.ToDecimal(ds.Tables[0].Rows[i]["StdMin"]);
                        obj.Qty = Convert.ToInt32(ds.Tables[0].Rows[i]["Qty"]);
                        obj.ProductionMinute = Convert.ToDecimal(ds.Tables[0].Rows[i]["ProductionMinute"]);
                        obj.Amount = Convert.ToDecimal(ds.Tables[0].Rows[i]["Amount"]);
                        obj.Efficiency = Convert.ToDecimal(ds.Tables[0].Rows[i]["Efficiency"]);
                        obj.FirstBundleScanTime = Convert.ToString(ds.Tables[0].Rows[i]["FirstBundleScanTime"]);
                        obj.LastBundleScanTime = Convert.ToString(ds.Tables[0].Rows[i]["LastBundleScanTime"]);
                        obj.ManualQty = Convert.ToInt32(ds.Tables[0].Rows[i]["ManualQty"]);
                        obj.QrScanQty = Convert.ToInt32(ds.Tables[0].Rows[i]["QrScanQty"]);
                        

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
    }
}