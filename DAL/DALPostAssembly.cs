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
    public class DALPostAssembly
    {

        SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["BSL"].ConnectionString);

        public clsPostAssembly Fn_Insert_PostAssemblySinglePcsAssign(clsPostAssembly objReq)
        {
            var objResp = new clsPostAssembly();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Insert_PostAssemblySinglePcsAssign");
            try
            {
                if (objReq.OperationNo == null || objReq.OperationNo == 0)
                {
                    objResp.vErrorMsg = "Please Select the Valid Operation Number";
                    objResp.vErrorCode = 300;
                }
                else if (objReq.AppEmpID == null || objReq.AppEmpID == 0)
                {
                    objResp.vErrorMsg = "Please Select the Valid Worker/Operator ID";
                    objResp.vErrorCode = 300;
                }
                else if (String.IsNullOrWhiteSpace(objReq.LineName))
                {
                    objResp.vErrorMsg = "Please Select the Valid Line Name";
                    objResp.vErrorCode = 300;
                }
                else
                {
                    if (Con.State == ConnectionState.Broken)
                    { Con.Close(); }
                    if (Con.State == ConnectionState.Closed)
                    { Con.Open(); }

                    SqlCommand cmd = new SqlCommand("USP_MobilePostAssemblyApp", Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@OperationNo", objReq.OperationNo);
                    cmd.Parameters.AddWithValue("@AppEmpID", objReq.AppEmpID);
                    cmd.Parameters.AddWithValue("@LineName", objReq.LineName);
                    cmd.Parameters.AddWithValue("@SupervisorID", objReq.SupervisorID);
                    cmd.Parameters.AddWithValue("@QueryType", "InsertPostAssemblyOpNo");

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
                        objResp.vErrorMsg = "Failed";
                    }
                }
            }
            catch (Exception exp)
            {
                objResp.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Insert_PostAssemblySinglePcsAssign", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Insert_PostAssemblySinglePcsAssign");
            return objResp;
        }


        public List<clsPostAssembly> Fn_Fetch_OperatorIDWisePostAssemOpNo(clsPostAssembly objReq)
        {
            var objResp = new List<clsPostAssembly>();
            var obj = new clsPostAssembly();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Fetch_OperatorIDWisePostAssemOpNo");
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_MobilePostAssemblyApp", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AppEmpID", objReq.AppEmpID);
                cmd.Parameters.AddWithValue("@QueryType", "FetchPostAssemblyOpNo");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;

                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsPostAssembly();
                        obj.AppEmpID = Convert.ToInt32(ds.Tables[0].Rows[i]["AppEmpID"]);
                        obj.AppEmpName = Convert.ToString(ds.Tables[0].Rows[i]["AppEmpName"]);
                        obj.OperationNo = Convert.ToInt64(ds.Tables[0].Rows[i]["OperationNo"]);
                        obj.OperationName = Convert.ToString(ds.Tables[0].Rows[i]["Descriptions"]);
                        obj.LineName = Convert.ToString(ds.Tables[0].Rows[i]["LineName"]);
                        obj.SupervisorID = Convert.ToInt32(ds.Tables[0].Rows[i]["SupervisorID"]);
                        obj.SupervisorName = Convert.ToString(ds.Tables[0].Rows[i]["SupervisorName"]);
                        obj.SupervisorAssignedDate = Convert.ToString(ds.Tables[0].Rows[i]["SupAssignedDate"]);

                        obj.vErrorCode = 200;
                        obj.vErrorMsg = "Success";
                        objResp.Add(obj);
                        i++;
                    }
                }
                else
                {
                    obj.vErrorCode = 404;
                    obj.vErrorMsg = "Post Assembly Op Numbers records are not found.";
                    objResp.Add(obj);
                }
            }
            catch (Exception exp)
            {
                obj.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Fetch_OperatorIDWisePostAssemOpNo", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Fetch_OperatorIDWisePostAssemOpNo");
            return objResp;
        }



    }
}