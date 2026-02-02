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
    public class DALMasterEntryMob
    {

        SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["BSL"].ConnectionString);


        public clsLine Fn_Add_New_Line(clsLine objReq)
        {
            var objResp = new clsLine();
            try
            {
                if (String.IsNullOrWhiteSpace(objReq.LineName))
                {
                    objResp.vErrorMsg = "Please Enter the Line Name";
                    objResp.vErrorCode = 300;
                }
                else if (objReq.DivisionID == 0 || objReq.DivisionID == null)
                {
                    objResp.vErrorMsg = "Please Select the Division";
                    objResp.vErrorCode = 300;
                }
                else if (String.IsNullOrWhiteSpace(objReq.ShiftTiming))
                {
                    objResp.vErrorMsg = "Please Select the Shift Timing";
                    objResp.vErrorCode = 300;
                }
                else if (String.IsNullOrWhiteSpace(objReq.LineStatus))
                {
                    objResp.vErrorMsg = "Please Select the Status";
                    objResp.vErrorCode = 300;
                }
                else if (String.IsNullOrWhiteSpace(objReq.AssignOperator))
                {
                    objResp.vErrorMsg = "Please Select the Assigned Operator";
                    objResp.vErrorCode = 300;
                }
                else
                {
                    if (Con.State == ConnectionState.Broken)
                    { Con.Close(); }
                    if (Con.State == ConnectionState.Closed)
                    { Con.Open(); }

                    SqlCommand cmd = new SqlCommand("", Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("", objReq.LineName);
                    cmd.Parameters.AddWithValue("", objReq.DivisionID);
                    cmd.Parameters.AddWithValue("", objReq.ShiftTiming);
                    cmd.Parameters.AddWithValue("", objReq.LineStatus);
                    cmd.Parameters.AddWithValue("", objReq.AssignOperator);
                    cmd.Parameters.AddWithValue("", objReq.CreatedBy);

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
                        objResp.vErrorMsg = "Inserting Line Failed";
                    }
                }
            }
            catch (Exception exp)
            {
                objResp.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Add_New_Line", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }


        
        //public List<clsLine> Fn_Get_ActiveLine(clsLine objReq)
        //{
        //    var objResp = new List<clsLine>();
        //    var obj = new clsLine();
        //    try
        //    {
        //        if (Con.State == ConnectionState.Broken)
        //        { Con.Close(); }
        //        if (Con.State == ConnectionState.Closed)
        //        { Con.Open(); }

        //        SqlCommand cmd = new SqlCommand("", Con);
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataSet ds = new DataSet();
        //        da.Fill(ds);

        //        int i = 0;
        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            while (ds.Tables[0].Rows.Count > i)
        //            {
        //                var objItem = new clsLine();
        //                objItem.
        //            }
        //        }

                 

        //    }
        //}



    }
}