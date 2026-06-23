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
    public class DALFabric
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

        public FabricInhouse Fn_Upload_Fabirc_Inhouse(FabricInhouse objReq)
        {
            var objResp = new FabricInhouse();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Upload_Fabirc_Inhouse");
            try
            {

                if (String.IsNullOrWhiteSpace(objReq.StyleCode))
                {
                    objResp.vErrorCode = 400;
                    objResp.vErrorMsg = "StyleCode is empty";
                    return objResp;
                }
                else if (String.IsNullOrWhiteSpace(objReq.ItemCode))
                {
                    objResp.vErrorCode = 400;
                    objResp.vErrorMsg = "ItemCode is empty";
                    return objResp;
                }
                else if (objReq._oInhouseList == null || objReq._oInhouseList.Count == 0)
                {
                    objResp.vErrorCode = 400;
                    objResp.vErrorMsg = "Please select List";
                    return objResp;
                }
                else
                {
                    if (Con.State == ConnectionState.Broken)
                    { Con.Close(); }
                    if (Con.State == ConnectionState.Closed)
                    { Con.Open(); }

                    foreach (FabricInhouseList _oList in objReq._oInhouseList)
                    {
                        if (_oList.InHouseId == 0 || _oList.InHouseId == null)
                        {
                            mxID = Fn_Get_MXID("Fabric_Inhouse", "InHouseId");
                            _oList.InHouseId = mxID;
                        }
                        SqlCommand cmd = new SqlCommand("USP_FABRIC_ORDER", Con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@InHouseId", _oList.InHouseId);
                        cmd.Parameters.AddWithValue("@StyleCode", objReq.StyleCode);
                        cmd.Parameters.AddWithValue("@ItemCode", objReq.ItemCode);
                        cmd.Parameters.AddWithValue("@Lotno", _oList.Lotno);
                        cmd.Parameters.AddWithValue("@RollNo", _oList.RollNo);
                        cmd.Parameters.AddWithValue("@TotalQuantity", _oList.Quantity);
                        cmd.Parameters.AddWithValue("@Unit", _oList.Unit);
                        cmd.Parameters.AddWithValue("@Width", _oList.Width);
                        cmd.Parameters.AddWithValue("@ShadeName", _oList.ShadeName);
                        cmd.Parameters.AddWithValue("@GSM", _oList.GSM);
                        cmd.Parameters.AddWithValue("@Shrinkage", _oList.Shrinkage);
                        cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                        cmd.Parameters.AddWithValue("@QueryType", "Insert_Fabric_InHouseId");
                        int j = cmd.ExecuteNonQuery();
                        if (j > 0)
                        {
                            objResp.vErrorCode = 200;
                            objResp.vErrorMsg = "Success";
                        }
                        else
                        {
                            objResp.vErrorCode = 400;
                            objResp.vErrorMsg = "File Uploading failed ";
                            return objResp;
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                objResp.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Upload_Fabirc_Inhouse", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Upload_Fabirc_Inhouse");
            return objResp;
        }
    }
}