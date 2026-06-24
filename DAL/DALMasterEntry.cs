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
    public class DALMasterEntry
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

        #region Start Division 05-Jan-2026
        public clsDivision Fn_Add_New_Division(clsDivision objReq)
        {
            var objResp = new clsDivision();

            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_MASTERENTRY", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Division", objReq.Division);
                cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                cmd.Parameters.AddWithValue("@QueryType", "InsertDivision");
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
                Logger.WriteLog("Function Name : Fn_Add_New_Division", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }

        public clsDivision Fn_Update_DivisionDetails_By_DivID(clsDivision objReq)
        {
            var objResp = new clsDivision();

            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_MASTERENTRY", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DivisionID", objReq.ID);
                cmd.Parameters.AddWithValue("@Division", objReq.Division);
                cmd.Parameters.AddWithValue("@ModifiedBy", objReq.ModifiedBy);
                cmd.Parameters.AddWithValue("@QueryType", "UpdateDivision");
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
                    objResp.vErrorMsg = "Updating Failed";
                }
            }
            catch (Exception exp)
            {
                objResp.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Update_DivisionDetails_By_DivID", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }

        public clsDivision Fn_Delete_DivisionDetails_ById(clsDivision objReq)
        {
            var objResp = new clsDivision();

            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_MASTERENTRY", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DivisionID", objReq.ID);
                //cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                cmd.Parameters.AddWithValue("@DivisionStatus", objReq.DivDeletionStatus);
                cmd.Parameters.AddWithValue("@ModifiedBy", objReq.ModifiedBy);
                cmd.Parameters.AddWithValue("@QueryType", "DeleteDivision");
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
                Logger.WriteLog("Function Name : Fn_Delete_DivisionDetails_ById", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }

        public List<clsDivision> Fn_Get_All_Division(clsDivision objReq)
        {
            var objResp = new List<clsDivision>();
            var obj = new clsDivision();
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT ID, Division, CreatedBy, FORMAT(CreatedOn, 'dd-MMM-yyyy') AS CreatedOn FROM DivisionMaster WHERE 1=1 AND DivisionStatus='Active'";
                if (objReq.ID != 0 && objReq.ID != null)
                {
                    strSql = strSql + " AND ID = @ID ";
                }
                if (!String.IsNullOrWhiteSpace(objReq.Division))
                {
                    strSql = strSql + " AND Division LIKE @Division ";
                }

                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;
                if (objReq.ID != 0 && objReq.ID != null)
                {
                    cmd.Parameters.AddWithValue("@ID", objReq.ID);
                }
                if (!String.IsNullOrWhiteSpace(objReq.Division))
                {
                    cmd.Parameters.AddWithValue("@Division", "%" + objReq.Division + "%");
                }

                //SqlCommand cmd = new SqlCommand("USP_MASTERENTRY", Con);
                //cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@QueryType", "GetAllDivision");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsDivision();
                        obj.ID = Convert.ToInt64(ds.Tables[0].Rows[i]["ID"]);
                        obj.Division = Convert.ToString(ds.Tables[0].Rows[i]["Division"]);
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
                Logger.WriteLog("Function Name : Fn_Get_All_Division", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }

        #endregion End Division 05-Jan-2026

        #region Start Section 05-Jan-2026

        public clsSection Fn_Add_New_Section(clsSection objReq)
        {
            var objResp = new clsSection();

            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_MASTERENTRY", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DivisionID", objReq.DivisionID);
                cmd.Parameters.AddWithValue("@SectionName", objReq.SectionName);
                cmd.Parameters.AddWithValue("@SectionHead", objReq.SectionHead);
                cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                cmd.Parameters.AddWithValue("@QueryType", "InsertSection");
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
                Logger.WriteLog("Function Name : Fn_Add_New_Section", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }

        public clsSection Fn_Update_SectionDetails_By_SectionID(clsSection objReq)
        {
            var objResp = new clsSection();

            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_MASTERENTRY", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SectionID", objReq.SectionID);
                cmd.Parameters.AddWithValue("@DivisionID", objReq.DivisionID);
                cmd.Parameters.AddWithValue("@SectionName", objReq.SectionName);
                cmd.Parameters.AddWithValue("@SectionHead", objReq.SectionHead);
                cmd.Parameters.AddWithValue("@ModifiedBy", objReq.ModifiedBy);
                cmd.Parameters.AddWithValue("@QueryType", "UpdateSection");
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
                    objResp.vErrorMsg = "Updating Failed";
                }
            }
            catch (Exception exp)
            {
                objResp.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Update_SectionDetails_By_SectionID", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }
        public clsSection Fn_Delete_SectionDetails_ById(clsSection objReq)
        {
            var objResp = new clsSection();

            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_MASTERENTRY", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SectionID", objReq.SectionID);
                //cmd.Parameters.AddWithValue("@DivisionID", objReq.DivisionID);
                cmd.Parameters.AddWithValue("@SectionStatus", objReq.SectionDeletionStatus);
                cmd.Parameters.AddWithValue("@ModifiedBy", objReq.ModifiedBy);
                cmd.Parameters.AddWithValue("@QueryType", "DeleteSection");
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
                Logger.WriteLog("Function Name : Fn_Delete_SectionDetails_ById", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }

        public List<clsSection> Fn_Get_All_Section(clsSection objReq)
        {
            var objResp = new List<clsSection>();
            var obj = new clsSection();
            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT SectionID, DivisionID, SectionName, SectionHead, CreatedBy,";
                strSql = strSql + " FORMAT(CreatedOn, 'dd-MMM-yyyy') AS CreatedOn FROM SectionMaster WHERE 1=1 AND SectionStatus='Active'";
                if (objReq.SectionID != 0)
                {
                    strSql = strSql + " AND SectionID = @SectionID ";
                }
                if (objReq.SectionName != "" && objReq.SectionName != null)
                {
                    strSql = strSql + " AND SectionName LIKE '%@SectionName%'";
                }
                if (objReq.SectionHead != "" && objReq.SectionHead != null)
                {
                    strSql = strSql + " AND SectionHead LIKE '%@SectionHead%'";
                }

                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;
                if (objReq.SectionID != 0)
                {
                    cmd.Parameters.AddWithValue("@SectionID", objReq.SectionID);
                }
                if (objReq.SectionName != "" && objReq.SectionName != null)
                {
                    cmd.Parameters.AddWithValue("@SectionName", objReq.SectionName);
                }
                if (objReq.SectionHead != "" && objReq.SectionHead != null)
                {
                    cmd.Parameters.AddWithValue("@SectionHead", objReq.SectionHead);
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsSection();
                        obj.SectionID = Convert.ToInt64(ds.Tables[0].Rows[i]["SectionID"]);
                        obj.DivisionID = Convert.ToInt64(ds.Tables[0].Rows[i]["DivisionID"]);
                        obj.SectionName = Convert.ToString(ds.Tables[0].Rows[i]["SectionName"]);
                        obj.SectionHead = Convert.ToString(ds.Tables[0].Rows[i]["SectionHead"]);
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
                Logger.WriteLog("Function Name : Fn_Get_All_Section", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }

        #endregion End Section 05-Jan-2026

        #region Start Line 05-Jan-2026

        public clsLine Fn_Add_New_Line(clsLine objReq)
        {
            var objResp = new clsLine();

            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_MASTERENTRY", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DivisionID", objReq.DivisionID);
                cmd.Parameters.AddWithValue("@SeqNo", objReq.SeqNo);
                cmd.Parameters.AddWithValue("@LineCode", objReq.LineCode);
                cmd.Parameters.AddWithValue("@LineName", objReq.LineName);
                cmd.Parameters.AddWithValue("@SuperVisor", objReq.SuperVisor);
                cmd.Parameters.AddWithValue("@SectionName", objReq.SectionName);
                cmd.Parameters.AddWithValue("@SuperMarketCode", objReq.SuperMarketCode);
                cmd.Parameters.AddWithValue("@IsQuality", objReq.IsQuality);
                cmd.Parameters.AddWithValue("@IsFinishing", objReq.IsFinishing);
                cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                cmd.Parameters.AddWithValue("@QueryType", "InsertLine");
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
                Logger.WriteLog("Function Name : Fn_Add_New_Line", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }

        public clsLine Fn_Update_LineDetails_By_LineID(clsLine objReq)
        {
            var objResp = new clsLine();
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_MASTERENTRY", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LineId", objReq.LineId);
                cmd.Parameters.AddWithValue("@DivisionID", objReq.DivisionID);
                cmd.Parameters.AddWithValue("@SeqNo", objReq.SeqNo);
                cmd.Parameters.AddWithValue("@LineCode", objReq.LineCode);
                cmd.Parameters.AddWithValue("@LineName", objReq.LineName);
                cmd.Parameters.AddWithValue("@SuperVisor", objReq.SuperVisor);
                cmd.Parameters.AddWithValue("@SectionName", objReq.SectionName);
                cmd.Parameters.AddWithValue("@SuperMarketCode", objReq.SuperMarketCode);
                cmd.Parameters.AddWithValue("@IsQuality", objReq.IsQuality);
                cmd.Parameters.AddWithValue("@IsFinishing", objReq.IsFinishing);
                cmd.Parameters.AddWithValue("@ModifiedBy", objReq.ModifiedBy);
                cmd.Parameters.AddWithValue("@QueryType", "UpdateLine");
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
                    objResp.vErrorMsg = "Updating Failed";
                }
            }
            catch (Exception exp)
            {
                objResp.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Update_LineDetails_By_LineID", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }
        public clsLine Fn_Delete_LineDetails_ById(clsLine objReq)
        {
            var objResp = new clsLine();

            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_MASTERENTRY", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LineId", objReq.LineId);
                cmd.Parameters.AddWithValue("@LineStatus", objReq.LineDeletionStatus);
                cmd.Parameters.AddWithValue("@ModifiedBy", objReq.ModifiedBy);
                cmd.Parameters.AddWithValue("@QueryType", "DeleteLine");
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
                Logger.WriteLog("Function Name : Fn_Delete_LineDetails_ById", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }

        public List<clsLine> Fn_Get_All_Line(clsLine objReq)
        {
            var objResp = new List<clsLine>();
            var obj = new clsLine();
            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT LineId, DivisionID, SeqNo, LineCode, LineName, SuperVisor, SectionName,";
                strSql = strSql + " SuperMarketCode, IsQuality, IsFinishing, CreatedBy, ";
                strSql = strSql + " FORMAT(CreatedOn, 'dd-MMM-yyyy') AS CreatedOn FROM LineMaster WHERE 1=1 AND LineStatus='Active'";

                if (objReq.LineCode != "" && objReq.LineCode != null)
                {
                    strSql = strSql + " AND LineCode = @LineCode";
                }
                if (objReq.LineName != "" && objReq.LineName != null)
                {
                    strSql = strSql + " AND LineName LIKE '%@LineName%'";
                }


                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;
                if (objReq.LineCode != "" && objReq.LineCode != null)
                {
                    cmd.Parameters.AddWithValue("@LineCode", objReq.LineCode);
                }
                if (objReq.LineName != "" && objReq.LineName != null)
                {
                    cmd.Parameters.AddWithValue("@LineName", objReq.LineName);
                }
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
                        obj.DivisionID = Convert.ToInt64(ds.Tables[0].Rows[i]["DivisionID"]);
                        obj.SeqNo = Convert.ToInt32(ds.Tables[0].Rows[i]["SeqNo"]);
                        obj.LineCode = Convert.ToString(ds.Tables[0].Rows[i]["LineCode"]);
                        obj.LineName = Convert.ToString(ds.Tables[0].Rows[i]["LineName"]);
                        obj.SuperVisor = Convert.ToString(ds.Tables[0].Rows[i]["SuperVisor"]);
                        obj.SectionName = Convert.ToString(ds.Tables[0].Rows[i]["SectionName"]);
                        obj.SuperMarketCode = Convert.ToInt32(ds.Tables[0].Rows[i]["SuperMarketCode"]);
                        obj.IsQuality = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsQuality"]);
                        obj.IsFinishing = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsFinishing"]);
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
                Logger.WriteLog("Function Name : Fn_Get_All_Line", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }

        #endregion End Line 05-Jan-2026

        #region Start Shift 05-Jan-2026

        public clsShift Fn_Add_New_Shift(clsShift objReq)
        {
            var objResp = new clsShift();

            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_MASTERENTRY", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ShiftName", objReq.ShiftName);
                cmd.Parameters.AddWithValue("@StartTime", objReq.StartTime);
                cmd.Parameters.AddWithValue("@LunchStart", objReq.LunchStart);
                cmd.Parameters.AddWithValue("@LunchEnd", objReq.LunchEnd);
                cmd.Parameters.AddWithValue("@EndTime", objReq.EndTime);
                cmd.Parameters.AddWithValue("@OTStart", objReq.OTStart);
                cmd.Parameters.AddWithValue("@OTEnd", objReq.OTEnd);
                cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                cmd.Parameters.AddWithValue("@QueryType", "InsertShift");
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
                Logger.WriteLog("Function Name : Fn_Add_New_Shift", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }

        public clsShift Fn_Update_Shift(clsShift objReq)
        {
            var objResp = new clsShift();

            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_MASTERENTRY", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ShifID", objReq.ShifID);
                cmd.Parameters.AddWithValue("@ShiftName", objReq.ShiftName);
                cmd.Parameters.AddWithValue("@StartTime", objReq.StartTime);
                cmd.Parameters.AddWithValue("@LunchStart", objReq.LunchStart);
                cmd.Parameters.AddWithValue("@LunchEnd", objReq.LunchEnd);
                cmd.Parameters.AddWithValue("@EndTime", objReq.EndTime);
                cmd.Parameters.AddWithValue("@OTStart", objReq.OTStart);
                cmd.Parameters.AddWithValue("@OTEnd", objReq.OTEnd);
                cmd.Parameters.AddWithValue("@ModifiedBy", objReq.ModifiedBy);
                cmd.Parameters.AddWithValue("@QueryType", "UpdateShift");
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
                    objResp.vErrorMsg = "Updating Failed";
                }
            }
            catch (Exception exp)
            {
                objResp.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Update_Shift", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }
        public clsShift Fn_Delete_Shift(clsShift objReq)
        {
            var objResp = new clsShift();

            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_MASTERENTRY", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ShifID", objReq.ShifID);
                cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                cmd.Parameters.AddWithValue("@QueryType", "DeleteShift");
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
                Logger.WriteLog("Function Name : Fn_Delete_Shift", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }

        public List<clsShift> Fn_Get_All_Shift(clsShift objReq)
        {
            var objResp = new List<clsShift>();
            var obj = new clsShift();
            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = " SELECT ShiftName, StartTime, LunchStart, LunchEnd, EndTime, OTStart, CreatedBy,";
                strSql = strSql + " OTEnd, FORMAT(CreatedOn, 'dd-MMM-yyyy') AS CreatedOn FROM ShiftMaster WHERE 1=1";
                if (objReq.ShiftName != "")
                {
                    strSql = strSql + " AND ShiftName LIKE '%ShiftName%' ";
                }

                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;
                if (objReq.ShiftName != "")
                {
                    cmd.Parameters.AddWithValue("@ShiftName", objReq.ShiftName);
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsShift();
                        obj.ShifID = Convert.ToInt16(ds.Tables[0].Rows[i]["ShifID"]);
                        obj.ShiftName = Convert.ToString(ds.Tables[0].Rows[i]["ShiftName"]);
                        obj.StartTime = Convert.ToString(ds.Tables[0].Rows[i]["StartTime"]);
                        obj.LunchStart = Convert.ToString(ds.Tables[0].Rows[i]["LunchStart"]);
                        obj.LunchEnd = Convert.ToString(ds.Tables[0].Rows[i]["LunchEnd"]);
                        obj.EndTime = Convert.ToString(ds.Tables[0].Rows[i]["EndTime"]);
                        obj.OTStart = Convert.ToString(ds.Tables[0].Rows[i]["OTStart"]);
                        obj.OTEnd = Convert.ToString(ds.Tables[0].Rows[i]["OTEnd"]);
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
                Logger.WriteLog("Function Name : Fn_Get_All_Shift", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }

        #endregion End Shift 05-Jan-2026

        #region Start Customer 07-Jan-2026

        public clsCustomer Fn_Add_New_Customer(clsCustomer objReq)
        {
            var objResp = new clsCustomer();
            if (objReq.ID == 0 || objReq.ID == null)
            {
                Fn_Get_MXID("CustomerMaster", "ID");
            }
            else
            {
                mxID = objReq.ID;
            }

            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_MASTERENTRY", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CustomerId", mxID);
                cmd.Parameters.AddWithValue("@vName", objReq.vName);
                cmd.Parameters.AddWithValue("@CodeNo", objReq.CodeNo);
                cmd.Parameters.AddWithValue("@vAddress", objReq.vAddress);
                cmd.Parameters.AddWithValue("@vContact", objReq.vContact);
                cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                cmd.Parameters.AddWithValue("@QueryType", "InsertCustomer");
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
                Logger.WriteLog("Function Name : Fn_Add_New_Shift", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }

        public clsCustomer Fn_Update_CustomerDetails_ById(clsCustomer objReq)
        {
            var objResp = new clsCustomer();

            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_MASTERENTRY", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", objReq.ID);
                cmd.Parameters.AddWithValue("@vName", objReq.vName);
                cmd.Parameters.AddWithValue("@CodeNo", objReq.CodeNo);
                cmd.Parameters.AddWithValue("@vAddress", objReq.vAddress);
                cmd.Parameters.AddWithValue("@vContact", objReq.vContact);
                cmd.Parameters.AddWithValue("@ModifiedBy", objReq.ModifiedBy);
                cmd.Parameters.AddWithValue("@QueryType", "UpdateCustomer");
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
                    objResp.vErrorMsg = "Updating Failed";
                }
            }
            catch (Exception exp)
            {
                objResp.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Update_CustomerDetails_ById", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }
        public clsCustomer Fn_Delete_CustomerDetails_ById(clsCustomer objReq)
        {
            var objResp = new clsCustomer();

            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_MASTERENTRY", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", objReq.ID);
                cmd.Parameters.AddWithValue("@CustomerStatus", objReq.CustDeletionStatus);
                cmd.Parameters.AddWithValue("@ModifiedBy", objReq.ModifiedBy);
                cmd.Parameters.AddWithValue("@QueryType", "DeleteCustomer");
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
                Logger.WriteLog("Function Name : Fn_Delete_CustomerDetails_ById", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }

        public List<clsCustomer> Fn_Get_All_Customer(clsCustomer objReq)
        {
            var objResp = new List<clsCustomer>();
            var obj = new clsCustomer();
            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT ID, vName, CodeNo, vAddress, vContact, CreatedBy,";
                strSql = strSql + " FORMAT(CreatedOn, 'dd-MMM-yyyy') AS CreatedOn FROM CustomerMaster WHERE 1=1 AND CustomerStatus='Active'";
                if (objReq.ID != 0 && objReq.ID != null)
                {
                    strSql = strSql + " AND ID = @ID ";
                }
                if (!String.IsNullOrWhiteSpace(objReq.vName))
                {
                    strSql = strSql + " AND vName = @vName ";
                }
                if (!String.IsNullOrWhiteSpace(objReq.CodeNo))
                {
                    strSql = strSql + " AND CodeNo = @CodeNo ";
                }
                if (!String.IsNullOrWhiteSpace(objReq.vContact))
                {
                    strSql = strSql + " AND vContact = @vContact ";
                }

                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;
                if (objReq.ID != 0 && objReq.ID != null)
                {
                    cmd.Parameters.AddWithValue("@ID", objReq.ID);
                }
                if (!String.IsNullOrWhiteSpace(objReq.vName))
                {
                    cmd.Parameters.AddWithValue("@vName", objReq.vName);
                }
                if (!String.IsNullOrWhiteSpace(objReq.CodeNo))
                {
                    cmd.Parameters.AddWithValue("@CodeNo", objReq.CodeNo);
                }
                if (!String.IsNullOrWhiteSpace(objReq.vContact))
                {
                    cmd.Parameters.AddWithValue("@vContact", objReq.vContact);
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsCustomer();
                        obj.ID = Convert.ToInt16(ds.Tables[0].Rows[i]["ID"]);
                        obj.vName = Convert.ToString(ds.Tables[0].Rows[i]["vName"]);
                        obj.CodeNo = Convert.ToString(ds.Tables[0].Rows[i]["CodeNo"]);
                        obj.vAddress = Convert.ToString(ds.Tables[0].Rows[i]["vAddress"]);
                        obj.vContact = Convert.ToString(ds.Tables[0].Rows[i]["vContact"]);
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
                Logger.WriteLog("Function Name : Fn_Get_All_Customer", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }

        #endregion End Customer 07-Jan-2026

        #region Start SizeMaster 07-Jan-2026

        public clsSizeMaster Fn_Add_New_SizeName(clsSizeMaster objReq)
        {
            var objResp = new clsSizeMaster();
            if (objReq.ID == 0 || objReq.ID == null)
            {
                Fn_Get_MXID("SizeMaster", "ID");
                objReq.ID = Convert.ToInt32(mxID);
            }
            
            try
            {
                if (objReq.ID == 0 || objReq.ID == null)
                {
                    objResp.vErrorCode = 400;
                    objResp.vErrorMsg = "SizeId not supply.";
                }
                else if (String.IsNullOrWhiteSpace(objReq.Grid))
                {
                    objResp.vErrorCode = 400;
                    objResp.vErrorMsg = "Please enter Grid";
                }
                else
                {

                    if (Con.State == ConnectionState.Broken)
                    { Con.Close(); }
                    if (Con.State == ConnectionState.Closed)
                    { Con.Open(); }

                    SqlCommand cmd = new SqlCommand("USP_MASTERENTRY", Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", objReq.ID);
                    cmd.Parameters.AddWithValue("@SeqNo", objReq.SeqNo);
                    cmd.Parameters.AddWithValue("@Grid", objReq.Grid);
                    cmd.Parameters.AddWithValue("@SizeName", objReq.SizeName);
                    cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                    cmd.Parameters.AddWithValue("@QueryType", "InsertSizeName");
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
            }
            catch (Exception exp)
            {
                objResp.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Add_New_SizeName", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }

        public clsSizeMaster Fn_Update_SizeName(clsSizeMaster objReq)
        {
            var objResp = new clsSizeMaster();

            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_MASTERENTRY", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", objReq.ID);
                cmd.Parameters.AddWithValue("@SeqNo", objReq.SeqNo);
                cmd.Parameters.AddWithValue("@Grid", objReq.Grid);
                cmd.Parameters.AddWithValue("@SizeName", objReq.SizeName);
                cmd.Parameters.AddWithValue("@ModifiedBy", objReq.ModifiedBy);
                cmd.Parameters.AddWithValue("@QueryType", "UpdateSizeName");
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
                    objResp.vErrorMsg = "Updating Failed";
                }
            }
            catch (Exception exp)
            {
                objResp.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Update_SizeName", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }
        public clsSizeMaster Fn_Delete_SizeName(clsSizeMaster objReq)
        {
            var objResp = new clsSizeMaster();
            
            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_MASTERENTRY", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", objReq.ID);
                cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                cmd.Parameters.AddWithValue("@QueryType", "DeleteSizeName");
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
                Logger.WriteLog("Function Name : Fn_Delete_SizeName", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }

        public List<clsSizeMaster> Fn_Get_All_SizeName(clsSizeMaster objReq)
        {
            var objResp = new List<clsSizeMaster>();
            var obj = new clsSizeMaster();
            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT ID, SizeName, SeqNo, Grid, CreatedBy, FORMAT(CreatedOn, 'dd-MMM-yyyy') AS CreatedOn FROM SizeMaster WHERE 1=1";
                if (objReq.ID != 0)
                {
                    strSql = strSql + " AND ID = @ID";
                }
                if (objReq.SeqNo != 0)
                {
                    strSql = strSql + " AND SeqNo = @SeqNo";
                }
                if (!String.IsNullOrWhiteSpace(objReq.SizeName))
                {
                    strSql = strSql + " AND SizeName LIKE @SizeName ";
                }
                if (!String.IsNullOrWhiteSpace(objReq.Grid))
                {
                    strSql = strSql + " AND Grid = @Grid";
                }
                strSql = strSql + " ORDER BY SizeName, SeqNo ASC ";
                //strSql = strSql + " ORDER BY CreatedOn DESC ";

                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;
                if (objReq.ID != 0)
                {
                    cmd.Parameters.AddWithValue("@ID", objReq.ID);
                }
                if (objReq.SeqNo != 0)
                {
                    cmd.Parameters.AddWithValue("@SeqNo", objReq.SeqNo);
                }
                if (!String.IsNullOrWhiteSpace(objReq.SizeName))
                {
                    cmd.Parameters.AddWithValue("@SizeName", "%" + objReq.SizeName + "%");
                }
                if (!String.IsNullOrWhiteSpace(objReq.Grid))
                {
                    cmd.Parameters.AddWithValue("@Grid", objReq.Grid);
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsSizeMaster();
                        obj.ID = Convert.ToInt32(ds.Tables[0].Rows[i]["ID"]);
                        obj.SeqNo = Convert.ToInt32(ds.Tables[0].Rows[i]["SeqNo"]);
                        obj.SizeName = Convert.ToString(ds.Tables[0].Rows[i]["SizeName"]);
                        obj.Grid = Convert.ToString(ds.Tables[0].Rows[i]["Grid"]);
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
                Logger.WriteLog("Function Name : Fn_Get_All_SizeName", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }

        #endregion End SizeMaster 07-Jan-2026

        #region Start Operator 07-Jan-2026

        public clsOperator Fn_Add_New_Operator(clsOperator objReq)
        {
            var objResp = new clsOperator();

            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_MASTERENTRY", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CodeNO", objReq.CodeNO);
                cmd.Parameters.AddWithValue("@OpName", objReq.OpName);
                cmd.Parameters.AddWithValue("@FatherName", objReq.FatherName);
                cmd.Parameters.AddWithValue("@Gender", objReq.Gender);
                cmd.Parameters.AddWithValue("@Grade", objReq.Grade);
                cmd.Parameters.AddWithValue("@Shift", objReq.Shift);
                cmd.Parameters.AddWithValue("@PIN", objReq.PIN);
                cmd.Parameters.AddWithValue("@OperatorType", objReq.OperatorType);
                cmd.Parameters.AddWithValue("@Mobile", objReq.Mobile);
                cmd.Parameters.AddWithValue("@Notification", objReq.Notification);
                cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                cmd.Parameters.AddWithValue("@QueryType", "InsertOperator");
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
                Logger.WriteLog("Function Name : Fn_Add_New_Operator", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }

        public List<clsOperator> Fn_Get_All_Operator(clsOperator objReq)
        {
            var objResp = new List<clsOperator>();
            var obj = new clsOperator();
            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT ID, CodeNO, OpName, FatherName, Gender, Grade, Shifts, PIN, OperatorType, Mobile,";
                strSql = strSql + " Notifications, CreatedBy, FORMAT(CreatedOn, 'dd-MMM-yyyy') AS CreatedOn FROM SizeMaster WHERE 1=1";
                if (objReq.CodeNO != "")
                {
                    strSql = strSql + " AND CodeNO = @CodeNO";
                }
                if (objReq.OpName != "")
                {
                    strSql = strSql + " AND OpName LIKE '%@OpName%' ";
                }

                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;
                if (objReq.CodeNO != "")
                {
                    cmd.Parameters.AddWithValue("@CodeNO", objReq.CodeNO);
                }
                if (objReq.OpName != "")
                {
                    cmd.Parameters.AddWithValue("@OpName", objReq.OpName);
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsOperator();
                        obj.ID = Convert.ToInt16(ds.Tables[0].Rows[i]["ID"]);
                        obj.CodeNO = Convert.ToString(ds.Tables[0].Rows[i]["CodeNO"]);
                        obj.OpName = Convert.ToString(ds.Tables[0].Rows[i]["OpName"]);
                        obj.FatherName = Convert.ToString(ds.Tables[0].Rows[i]["FatherName"]);
                        obj.Gender = Convert.ToString(ds.Tables[0].Rows[i]["Gender"]);
                        obj.Grade = Convert.ToString(ds.Tables[0].Rows[i]["Grade"]);
                        obj.Shift = Convert.ToString(ds.Tables[0].Rows[i]["Shifts"]);
                        obj.PIN = Convert.ToString(ds.Tables[0].Rows[i]["PIN"]);
                        obj.OperatorType = Convert.ToString(ds.Tables[0].Rows[i]["OperatorType"]);
                        obj.Mobile = Convert.ToString(ds.Tables[0].Rows[i]["Mobile"]);
                        obj.Notification = Convert.ToString(ds.Tables[0].Rows[i]["Notifications"]);
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
                obj.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Get_All_SizeName", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }

        #endregion End Operator 07-Jan-2026

        #region Start LostTimeMaster 07-Jan-2026

        public clsLostTimeMaster Fn_Insert_LostTimeMaster(clsLostTimeMaster objReq)
        {
            var objResp = new clsLostTimeMaster();

            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_MASTERENTRY", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CodeNO", objReq.CodeNO);
                cmd.Parameters.AddWithValue("@Description", objReq.Description);
                cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                cmd.Parameters.AddWithValue("@QueryType", "InsertLostTime");
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
                Logger.WriteLog("Function Name : Fn_Insert_LostTimeMaster", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }

        public List<clsLostTimeMaster> Fn_Get_All_LostTime(clsLostTimeMaster objReq)
        {
            var objResp = new List<clsLostTimeMaster>();
            var obj = new clsLostTimeMaster();
            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT ID, CodeNO, Descriptions, CreatedBy, FORMAT(CreatedOn, 'dd-MMM-yyyy') AS CreatedOn FROM LostTimeMaster WHERE 1=1";
                if (objReq.CodeNO != "")
                {
                    strSql = strSql + " AND CodeNO = @CodeNO";
                }
                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;
                if (objReq.CodeNO != "")
                {
                    cmd.Parameters.AddWithValue("@CodeNO", objReq.CodeNO);
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsLostTimeMaster();
                        obj.ID = Convert.ToInt16(ds.Tables[0].Rows[i]["ID"]);
                        obj.CodeNO = Convert.ToString(ds.Tables[0].Rows[i]["CodeNO"]);
                        obj.Description = Convert.ToString(ds.Tables[0].Rows[i]["Descriptions"]);
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
                obj.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Get_All_SizeName", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }

        #endregion End Operator 07-Jan-2026

        #region Start ProductionRepairCodeMaster 07-Jan-2026

        public clsProductionRepairCodeMaster Fn_Insert_Production_RepairCode(clsProductionRepairCodeMaster objReq)
        {
            var objResp = new clsProductionRepairCodeMaster();

            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_MASTERENTRY", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SrNo", objReq.SrNo);
                cmd.Parameters.AddWithValue("@CodeNO", objReq.CodeNO);
                cmd.Parameters.AddWithValue("@Description", objReq.Description);
                cmd.Parameters.AddWithValue("@IgnoreInDHU", objReq.IgnoreInDHU);
                cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                cmd.Parameters.AddWithValue("@QueryType", "InsertProductionRepairCode");
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
                Logger.WriteLog("Function Name : Fn_Insert_Production_RepairCode", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }

        public List<clsProductionRepairCodeMaster> Fn_Get_All_Production_RepairCode(clsProductionRepairCodeMaster objReq)
        {
            var objResp = new List<clsProductionRepairCodeMaster>();
            var obj = new clsProductionRepairCodeMaster();
            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT SrNo, CodeNO, Descriptions, IgnoreInDHU, CreatedBy, FORMAT(CreatedOn, 'dd-MMM-yyyy') AS CreatedOn FROM ProductionRepairCodeMaster WHERE 1=1";
                if (objReq.CodeNO != "")
                {
                    strSql = strSql + " AND CodeNO = @CodeNO";
                }
                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;
                if (objReq.CodeNO != "")
                {
                    cmd.Parameters.AddWithValue("@CodeNO", objReq.CodeNO);
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsProductionRepairCodeMaster();
                        obj.ID = Convert.ToInt16(ds.Tables[0].Rows[i]["ID"]);
                        obj.SrNo = Convert.ToInt16(ds.Tables[0].Rows[i]["SrNo"]);
                        obj.CodeNO = Convert.ToString(ds.Tables[0].Rows[i]["CodeNO"]);
                        obj.Description = Convert.ToString(ds.Tables[0].Rows[i]["Descriptions"]);
                        obj.IgnoreInDHU = Convert.ToBoolean(ds.Tables[0].Rows[i]["IgnoreInDHU"]);
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
                obj.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Get_All_Production_RepairCode", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }


        #endregion End ProductionRepairCodeMaster 07-Jan-2026

        #region Start MachineProblem 07-Jan-2026

        public clsMachineProblem Fn_Insert_MachineProblem(clsMachineProblem objReq)
        {
            var objResp = new clsMachineProblem();

            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_MASTERENTRY", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CodeNO", objReq.CodeNO);
                cmd.Parameters.AddWithValue("@Description", objReq.Description);
                cmd.Parameters.AddWithValue("@Needle", objReq.Needle);
                cmd.Parameters.AddWithValue("@OilLeak", objReq.OilLeak);
                cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                cmd.Parameters.AddWithValue("@QueryType", "InsertMachineProblem");
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
                Logger.WriteLog("Function Name : Fn_Insert_Production_RepairCode", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }

        public List<clsMachineProblem> Fn_Get_All_MachineProblem(clsMachineProblem objReq)
        {
            var objResp = new List<clsMachineProblem>();
            var obj = new clsMachineProblem();
            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT ID, CodeNO, Descriptions, Needle, OilLeak, CreatedBy, FORMAT(CreatedOn, 'dd-MMM-yyyy') AS CreatedOn FROM MachineProblem WHERE 1=1";
                if (objReq.CodeNO != "")
                {
                    strSql = strSql + " AND CodeNO = @CodeNO";
                }
                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;
                if (objReq.CodeNO != "")
                {
                    cmd.Parameters.AddWithValue("@CodeNO", objReq.CodeNO);
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsMachineProblem();
                        obj.ID = Convert.ToInt16(ds.Tables[0].Rows[i]["ID"]);
                        obj.CodeNO = Convert.ToString(ds.Tables[0].Rows[i]["CodeNO"]);
                        obj.Description = Convert.ToString(ds.Tables[0].Rows[i]["Descriptions"]);
                        obj.Needle = Convert.ToBoolean(ds.Tables[0].Rows[i]["Needle"]);
                        obj.OilLeak = Convert.ToBoolean(ds.Tables[0].Rows[i]["OilLeak"]);
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
                obj.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Get_All_MachineProblem", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }

        #endregion End MachineProblem 07-Jan-2026

        #region Start MachineServicesType 07-Jan-2026

        public clsMachineServicesType Fn_Insert_MachineServicesType(clsMachineServicesType objReq)
        {
            var objResp = new clsMachineServicesType();

            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_MASTERENTRY", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CodeNO", objReq.vName);
                cmd.Parameters.AddWithValue("@Description", objReq.Display);
                cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                cmd.Parameters.AddWithValue("@QueryType", "InsertMachineServicesType");
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
                Logger.WriteLog("Function Name : Fn_Insert_MachineServicesType", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }

        public List<clsMachineServicesType> Fn_Get_All_MachineServicesType(clsMachineServicesType objReq)
        {
            var objResp = new List<clsMachineServicesType>();
            var obj = new clsMachineServicesType();
            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT ID, vName, Display, CreatedBy, FORMAT(CreatedOn, 'dd-MMM-yyyy') AS CreatedOn FROM MachineServicesType WHERE 1=1";
                if (objReq.vName != "")
                {
                    strSql = strSql + " AND vName = @vName";
                }
                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;
                if (objReq.vName != "")
                {
                    cmd.Parameters.AddWithValue("@vName", objReq.vName);
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsMachineServicesType();
                        obj.ID = Convert.ToInt16(ds.Tables[0].Rows[i]["ID"]);
                        obj.vName = Convert.ToString(ds.Tables[0].Rows[i]["vName"]);
                        obj.Display = Convert.ToString(ds.Tables[0].Rows[i]["Display"]);
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
                obj.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Get_All_MachineProblem", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }

        #endregion End MachineServicesType 07-Jan-2026

        #region Start SamplingCheckListPoint  07-Jan-2026

        public clsSamplingCheckListPoint Fn_Insert_SamplingCheckListPoint(clsSamplingCheckListPoint objReq)
        {
            var objResp = new clsSamplingCheckListPoint();

            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_MASTERENTRY", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@QualityCheckList", objReq.QualityCheckList);
                cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                cmd.Parameters.AddWithValue("@QueryType", "InsertSamplingCheckListPoint");
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
                Logger.WriteLog("Function Name : Fn_Insert_SamplingCheckListPoint", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }

        public List<clsSamplingCheckListPoint> Fn_Get_All_SamplingCheckListPoint(clsSamplingCheckListPoint objReq)
        {
            var objResp = new List<clsSamplingCheckListPoint>();
            var obj = new clsSamplingCheckListPoint();
            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT SeqNo, QualityCheckList, CreatedBy, FORMAT(CreatedOn, 'dd-MMM-yyyy') AS CreatedOn FROM SamplingCheckListPoint WHERE 1=1";
                if (objReq.QualityCheckList != "")
                {
                    strSql = strSql + " AND QualityCheckList = @QualityCheckList";
                }
                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;
                if (objReq.QualityCheckList != "")
                {
                    cmd.Parameters.AddWithValue("@QualityCheckList", objReq.QualityCheckList);
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsSamplingCheckListPoint();
                        obj.SeqNo = Convert.ToInt16(ds.Tables[0].Rows[i]["SeqNo"]);
                        obj.QualityCheckList = Convert.ToString(ds.Tables[0].Rows[i]["QualityCheckList"]);
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
                obj.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Get_All_SamplingCheckListPoint", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }

        #endregion Start SamplingCheckListPoint  07-Jan-2026

        #region Start QualityAuditorTable 07-Jan-2026

        public clsQualityAuditorTable Fn_Insert_QualityAuditorTable(clsQualityAuditorTable objReq)
        {
            var objResp = new clsQualityAuditorTable();

            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_MASTERENTRY", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@QualityCheckList", objReq.AuditorTable);
                cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                cmd.Parameters.AddWithValue("@QueryType", "InsertQualityAuditor");
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
                Logger.WriteLog("Function Name : Fn_Insert_QualityAuditorTable", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }

        public List<clsQualityAuditorTable> Fn_Get_All_QualityAuditorTable(clsQualityAuditorTable objReq)
        {
            var objResp = new List<clsQualityAuditorTable>();
            var obj = new clsQualityAuditorTable();
            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT ID, AuditorTable, CreatedBy, FORMAT(CreatedOn, 'dd-MMM-yyyy') AS CreatedOn FROM QualityAuditorTable WHERE 1=1";
                if (objReq.AuditorTable != "")
                {
                    strSql = strSql + " AND AuditorTable = @AuditorTable";
                }
                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;
                if (objReq.AuditorTable != "")
                {
                    cmd.Parameters.AddWithValue("@AuditorTable", objReq.AuditorTable);
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsQualityAuditorTable();
                        obj.ID = Convert.ToInt16(ds.Tables[0].Rows[i]["ID"]);
                        obj.AuditorTable = Convert.ToString(ds.Tables[0].Rows[i]["AuditorTable"]);
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
                obj.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Get_All_QualityAuditorTable", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }

        #endregion Start QualityAuditorTable 07-Jan-2026

        #region Start FabricDefectMaster 07-Jan-2026

        public clsFabricDefectMaster Fn_Insert_FabricDefect(clsFabricDefectMaster objReq)
        {
            var objResp = new clsFabricDefectMaster();

            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_MASTERENTRY", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CodeNO", objReq.CodeNO);
                cmd.Parameters.AddWithValue("@Description", objReq.Description);
                cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                cmd.Parameters.AddWithValue("@QueryType", "InsertFabricDefect");
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
                Logger.WriteLog("Function Name : Fn_Insert_FabricDefect", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }

        public List<clsFabricDefectMaster> Fn_Get_All_FabricDefect(clsFabricDefectMaster objReq)
        {
            var objResp = new List<clsFabricDefectMaster>();
            var obj = new clsFabricDefectMaster();
            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT ID, CodeNO, Descriptions, CreatedBy, FORMAT(CreatedOn, 'dd-MMM-yyyy') AS CreatedOn FROM QualityAuditorTable WHERE 1=1";
                if (objReq.CodeNO != "")
                {
                    strSql = strSql + " AND CodeNO = @CodeNO";
                }
                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;
                if (objReq.CodeNO != "")
                {
                    cmd.Parameters.AddWithValue("@CodeNO", objReq.CodeNO);
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsFabricDefectMaster();
                        obj.ID = Convert.ToInt16(ds.Tables[0].Rows[i]["ID"]);
                        obj.CodeNO = Convert.ToString(ds.Tables[0].Rows[i]["CodeNO"]);
                        obj.Description = Convert.ToString(ds.Tables[0].Rows[i]["Descriptions"]);
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
                obj.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Get_All_FabricDefectMaster", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }

        #endregion Start FabricDefectMaster 07-Jan-2026

        #region Start Maintenance 07-Jan-2026

        public clsMaintenance Fn_Insert_Maintenance(clsMaintenance objReq)
        {
            var objResp = new clsMaintenance();

            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_MASTERENTRY", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CodeNO", objReq.CodeNO);
                cmd.Parameters.AddWithValue("@vName", objReq.vName);
                cmd.Parameters.AddWithValue("@SortName", objReq.SortName);
                cmd.Parameters.AddWithValue("@Category", objReq.Category);
                cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                cmd.Parameters.AddWithValue("@QueryType", "InsertMaintenance");
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
                Logger.WriteLog("Function Name : Fn_Insert_FabricDefect", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }

        public List<clsMaintenance> Fn_Get_All_Maintenance(clsMaintenance objReq)
        {
            var objResp = new List<clsMaintenance>();
            var obj = new clsMaintenance();
            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT ID, CodeNO, vName, SortName, Category, CreatedBy, FORMAT(CreatedOn, 'dd-MMM-yyyy') AS CreatedOn FROM MaintenanceMaster WHERE 1=1";
                if (objReq.CodeNO != "")
                {
                    strSql = strSql + " AND CodeNO = @CodeNO";
                }
                if (objReq.vName != "")
                {
                    strSql = strSql + " AND vName = @vName";
                }
                if (objReq.SortName != "")
                {
                    strSql = strSql + " AND SortName = @SortName";
                }
                if (objReq.Category != "")
                {
                    strSql = strSql + " AND Category = @Category";
                }
                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;
                if (objReq.CodeNO != "")
                {
                    cmd.Parameters.AddWithValue("@CodeNO", objReq.CodeNO);
                }
                if (objReq.vName != "")
                {
                    cmd.Parameters.AddWithValue("@vName", objReq.vName);
                }
                if (objReq.SortName != "")
                {
                    cmd.Parameters.AddWithValue("@SortName", objReq.SortName);
                }
                if (objReq.Category != "")
                {
                    cmd.Parameters.AddWithValue("@Category", objReq.Category);
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsMaintenance();
                        obj.ID = Convert.ToInt16(ds.Tables[0].Rows[i]["ID"]);
                        obj.CodeNO = Convert.ToString(ds.Tables[0].Rows[i]["CodeNO"]);
                        obj.vName = Convert.ToString(ds.Tables[0].Rows[i]["vName"]);
                        obj.SortName = Convert.ToString(ds.Tables[0].Rows[i]["SortName"]);
                        obj.Category = Convert.ToString(ds.Tables[0].Rows[i]["Category"]);
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
                obj.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Get_All_Maintenance", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }

        #endregion End Maintenance 07-Jan-2026

        #region Start AcceptableQualityLevel 07-Jan-2026

        public clsAcceptableQualityLevel Fn_Insert_AcceptableQualityLevel(clsAcceptableQualityLevel objReq)
        {
            var objResp = new clsAcceptableQualityLevel();

            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_MASTERENTRY", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PCSFrom", objReq.PCSFrom);
                cmd.Parameters.AddWithValue("@PCSTo", objReq.PCSTo);
                cmd.Parameters.AddWithValue("@SampleSize", objReq.SampleSize);
                cmd.Parameters.AddWithValue("@Accepted", objReq.Accepted);
                cmd.Parameters.AddWithValue("@Rejected", objReq.Rejected);
                cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                cmd.Parameters.AddWithValue("@QueryType", "InsertAcceptableQuality");
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
                Logger.WriteLog("Function Name : Fn_Insert_AcceptableQualityLevel", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }

        public List<clsAcceptableQualityLevel> Fn_Get_All_AcceptableQualityLevel(clsAcceptableQualityLevel objReq)
        {
            var objResp = new List<clsAcceptableQualityLevel>();
            var obj = new clsAcceptableQualityLevel();
            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT ID, PCSFrom, PCSTo, SampleSize, Accepted, Rejected, CreatedBy, FORMAT(CreatedOn, 'dd-MMM-yyyy') AS CreatedOn FROM AcceptableQualityLevel WHERE 1=1";
                if (objReq.PCSFrom != "")
                {
                    strSql = strSql + " AND PCSFrom = @PCSFrom";
                }
                if (objReq.PCSTo != "")
                {
                    strSql = strSql + " AND PCSTo = @PCSTo";
                }
                if (objReq.SampleSize != "")
                {
                    strSql = strSql + " AND SampleSize = @SampleSize";
                }
                if (objReq.Accepted != "")
                {
                    strSql = strSql + " AND Accepted = @Accepted";
                }
                if (objReq.Rejected != "")
                {
                    strSql = strSql + " AND Rejected = @Rejected";
                }
                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;

                if (objReq.PCSFrom != "")
                {
                    cmd.Parameters.AddWithValue("@PCSFrom", objReq.PCSFrom);
                }
                if (objReq.PCSTo != "")
                {
                    cmd.Parameters.AddWithValue("@PCSTo", objReq.PCSTo);
                }
                if (objReq.SampleSize != "")
                {
                    cmd.Parameters.AddWithValue("@SampleSize", objReq.SampleSize);
                }
                if (objReq.Accepted != "")
                {
                    cmd.Parameters.AddWithValue("@Accepted", objReq.Accepted);
                }
                if (objReq.Rejected != "")
                {
                    cmd.Parameters.AddWithValue("@Rejected", objReq.Rejected);
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsAcceptableQualityLevel();
                        obj.ID = Convert.ToInt16(ds.Tables[0].Rows[i]["ID"]);
                        obj.PCSFrom = Convert.ToString(ds.Tables[0].Rows[i]["PCSFrom"]);
                        obj.PCSTo = Convert.ToString(ds.Tables[0].Rows[i]["PCSTo"]);
                        obj.SampleSize = Convert.ToString(ds.Tables[0].Rows[i]["SampleSize"]);
                        obj.Accepted = Convert.ToString(ds.Tables[0].Rows[i]["Accepted"]);
                        obj.Rejected = Convert.ToString(ds.Tables[0].Rows[i]["Rejected"]);
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
                obj.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Get_All_AcceptableQualityLevel", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }

        #endregion End AcceptableQualityLevel 07-Jan-2026

        #region Start MaintenanceRemarks 07-Jan-2026

        public clsMaintenanceRemarks Fn_Insert_MaintenanceRemarks(clsMaintenanceRemarks objReq)
        {
            var objResp = new clsMaintenanceRemarks();

            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_MASTERENTRY", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PCSFrom", objReq.Remarks);
                cmd.Parameters.AddWithValue("@PCSTo", objReq.IsAsset);
                cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                cmd.Parameters.AddWithValue("@QueryType", "InsertMaintenanceRemarks");
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
                Logger.WriteLog("Function Name : Fn_Insert_AcceptableQualityLevel", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }

        public List<clsMaintenanceRemarks> Fn_Get_All_AcceptableQualityLevel(clsMaintenanceRemarks objReq)
        {
            var objResp = new List<clsMaintenanceRemarks>();
            var obj = new clsMaintenanceRemarks();
            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT ID, Remarks, IsAsset, CreatedBy, FORMAT(CreatedOn, 'dd-MMM-yyyy') AS CreatedOn FROM MaintenanceRemarks WHERE 1=1";

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
                        obj = new clsMaintenanceRemarks();
                        obj.ID = Convert.ToInt16(ds.Tables[0].Rows[i]["ID"]);
                        obj.Remarks = Convert.ToString(ds.Tables[0].Rows[i]["Remarks"]);
                        obj.IsAsset = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsAsset"]);
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
                obj.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Get_All_MaintenanceRemarks", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }

        #endregion End MaintenanceRemarks 07-Jan-2026

        #region Start Holiday 07-Jan-2026

        public clsHoliday Fn_Insert_Holiday(clsHoliday objReq)
        {
            var objResp = new clsHoliday();

            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_MASTERENTRY", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@HoliDayDate", objReq.HoliDayDate);
                cmd.Parameters.AddWithValue("@Day", objReq.Day);
                cmd.Parameters.AddWithValue("@Month", objReq.Month);
                cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                cmd.Parameters.AddWithValue("@QueryType", "InsertHoliday");
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
                Logger.WriteLog("Function Name : Fn_Insert_Holiday", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }

        public List<clsHoliday> Fn_Get_All_Holiday(clsHoliday objReq)
        {
            var objResp = new List<clsHoliday>();
            var obj = new clsHoliday();
            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT ID, HoliDayDate, Days, Months, CreatedBy, FORMAT(CreatedOn, 'dd-MMM-yyyy') AS CreatedOn FROM HoliDayDate WHERE 1=1";
                if (objReq.HoliDayDate != "")
                {
                    strSql = strSql + " AND HoliDayDate = @HoliDayDate";
                }
                if (objReq.Day != "")
                {
                    strSql = strSql + " AND Days = @Days";
                }
                if (objReq.Month != "")
                {
                    strSql = strSql + " AND Months = @Months";
                }

                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;
                if (objReq.HoliDayDate != "")
                {
                    cmd.Parameters.AddWithValue("@HoliDayDate", objReq.HoliDayDate);
                }
                if (objReq.Day != "")
                {
                    cmd.Parameters.AddWithValue("@Days", objReq.Day);
                }
                if (objReq.Month != "")
                {
                    cmd.Parameters.AddWithValue("@Months", objReq.Month);
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsHoliday();
                        obj.ID = Convert.ToInt16(ds.Tables[0].Rows[i]["ID"]);
                        obj.HoliDayDate = Convert.ToString(ds.Tables[0].Rows[i]["HoliDayDate"]);
                        obj.Day = Convert.ToString(ds.Tables[0].Rows[i]["Days"]);
                        obj.Month = Convert.ToString(ds.Tables[0].Rows[i]["Months"]);
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
                obj.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Get_All_Holiday", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }

        #endregion End Holiday 07-Jan-2026


        public clsDivision Fn_Fetch_DivisionDetails_By_DivID(clsDivision objReq)
        {
            var objResp = new clsDivision();
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_MASTERENTRY", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", objReq.ID);
                cmd.Parameters.AddWithValue("@QueryType", "FetchDivisionDetailsById");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    objResp.ID = Convert.ToInt64(ds.Tables[0].Rows[0]["ID"]);
                    objResp.Division = Convert.ToString(ds.Tables[0].Rows[0]["Division"]);

                    objResp.vErrorCode = 200;
                    objResp.vErrorMsg = "Success";
                }
                else
                {
                    objResp.vErrorCode = 400;
                    objResp.vErrorMsg = "Division Fetch details failed.";
                }
            }
            catch (Exception exp)
            {
                objResp.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Fetch_DivisionDetails_By_DivID", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }

        #region Start Category 16-Jan-2026

        public clsCategory Fn_Add_New_Category(clsCategory objReq)
        {
            var objResp = new clsCategory();
            if (objReq.ID == 0 || objReq.ID == null)
            {
                Fn_Get_MXID("CategoryMaster", "ID");
            }
            else
            {
                mxID = objReq.ID;
            }

            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_MASTERENTRY", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", mxID);
                cmd.Parameters.AddWithValue("@Category", objReq.Category);
                cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                cmd.Parameters.AddWithValue("@QueryType", "InsertCategory");
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
                Logger.WriteLog("Function Name : Fn_Add_New_Category", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }

        public clsCategory Fn_Update_Category(clsCategory objReq)
        {
            var objResp = new clsCategory();

            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_MASTERENTRY", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", objReq.ID);
                cmd.Parameters.AddWithValue("@vName", objReq.Category);
                cmd.Parameters.AddWithValue("@ModifiedBy", objReq.ModifiedBy);
                cmd.Parameters.AddWithValue("@QueryType", "UpdateCategory");
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
                    objResp.vErrorMsg = "Updating Failed";
                }
            }
            catch (Exception exp)
            {
                objResp.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Update_Category", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }
        public clsCategory Fn_Delete_Category(clsCategory objReq)
        {
            var objResp = new clsCategory();

            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_MASTERENTRY", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", objReq.ID);
                cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                cmd.Parameters.AddWithValue("@QueryType", "DeleteCategory");
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
                Logger.WriteLog("Function Name : Fn_Delete_Category", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }

        public List<clsCategory> Fn_Get_All_Category(clsCategory objReq)
        {
            var objResp = new List<clsCategory>();
            var obj = new clsCategory();
            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT ID, Category, CreatedBy,";
                strSql = strSql + " FORMAT(CreatedOn, 'dd-MMM-yyyy') AS CreatedOn FROM CategoryMaster WHERE 1=1";
                if (objReq.ID != 0 && objReq.ID != null)
                {
                    strSql = strSql + " AND ID = @ID ";
                }
                if (!String.IsNullOrWhiteSpace(objReq.Category))
                {
                    strSql = strSql + " AND Category = @Category ";
                }

                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;
                if (objReq.ID != 0 && objReq.ID != null)
                {
                    cmd.Parameters.AddWithValue("@ID", objReq.ID);
                }
                if (!String.IsNullOrWhiteSpace(objReq.Category))
                {
                    cmd.Parameters.AddWithValue("@Category", objReq.Category);
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsCategory();
                        obj.ID = Convert.ToInt16(ds.Tables[0].Rows[i]["ID"]);
                        obj.Category = Convert.ToString(ds.Tables[0].Rows[i]["Category"]);
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
                Logger.WriteLog("Function Name : Fn_Get_All_Category", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }

        #endregion End Category 07-Jan-2026

        #region Start Season 19-Jan-2026

        public clsSeason Fn_Add_New_Season(clsSeason objReq)
        {
            var objResp = new clsSeason();
            if (objReq.ID == 0 || objReq.ID == null)
            {
                Fn_Get_MXID("SeasonMaster", "ID");
            }
            else
            {
                mxID = objReq.ID;
            }

            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_MASTERENTRY", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", mxID);
                cmd.Parameters.AddWithValue("@Season", objReq.Season);
                cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                cmd.Parameters.AddWithValue("@QueryType", "InsertSeason");
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
                Logger.WriteLog("Function Name : Fn_Add_New_Season", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }

        public clsSeason Fn_Update_Season(clsSeason objReq)
        {
            var objResp = new clsSeason();

            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_MASTERENTRY", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", objReq.ID);
                cmd.Parameters.AddWithValue("@Season", objReq.Season);
                cmd.Parameters.AddWithValue("@ModifiedBy", objReq.ModifiedBy);
                cmd.Parameters.AddWithValue("@QueryType", "UpdateSeason");
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
                    objResp.vErrorMsg = "Updating Failed";
                }
            }
            catch (Exception exp)
            {
                objResp.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Update_Season", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }
        public clsSeason Fn_Delete_Season(clsSeason objReq)
        {
            var objResp = new clsSeason();

            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_MASTERENTRY", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", objReq.ID);
                cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                cmd.Parameters.AddWithValue("@QueryType", "DeleteSeason");
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
                Logger.WriteLog("Function Name : Fn_Delete_Season", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }

        public List<clsSeason> Fn_Get_All_Season(clsSeason objReq)
        {
            var objResp = new List<clsSeason>();
            var obj = new clsSeason();
            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT ID, Season, CreatedBy,";
                strSql = strSql + " FORMAT(CreatedOn, 'dd-MMM-yyyy') AS CreatedOn FROM SeasonMaster WHERE 1=1";
                if (objReq.ID != 0 && objReq.ID != null)
                {
                    strSql = strSql + " AND ID = @ID ";
                }
                if (!String.IsNullOrWhiteSpace(objReq.Season))
                {
                    strSql = strSql + " AND Season = @Season ";
                }

                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;
                if (objReq.ID != 0 && objReq.ID != null)
                {
                    cmd.Parameters.AddWithValue("@ID", objReq.ID);
                }
                if (!String.IsNullOrWhiteSpace(objReq.Season))
                {
                    cmd.Parameters.AddWithValue("@Season", objReq.Season);
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsSeason();
                        obj.ID = Convert.ToInt16(ds.Tables[0].Rows[i]["ID"]);
                        obj.Season = Convert.ToString(ds.Tables[0].Rows[i]["Season"]);
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
                Logger.WriteLog("Function Name : Fn_Get_All_Season", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }

        #endregion End Season 19-Jan-2026

        #region Start Worker 26-Jan-2026

        public clsWorker Fn_Insert_New_Worker(clsWorker objReq)
        {
            var objResp = new clsWorker();

            if (objReq.ID == 0 || objReq.ID == null)
            {
                Fn_Get_MXID("EmployeeMaster", "ID");
            }
            else
            {
                mxID = objReq.ID;
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Insert_New_Worker");
            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string encriptPassword = Generic.EncryptText(objReq.Pin);

                SqlCommand cmd = new SqlCommand("USP_WORKER", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@ID", mxID);
                cmd.Parameters.AddWithValue("@Code", objReq.Code);
                cmd.Parameters.AddWithValue("@Name", objReq.Name);
                cmd.Parameters.AddWithValue("@EmpDesignation", objReq.EmpDesignation);
                cmd.Parameters.AddWithValue("@EmpRole", objReq.EmpUser);
                cmd.Parameters.AddWithValue("@Unit", objReq.Unit);
                cmd.Parameters.AddWithValue("@Grouping", objReq.Grouping);
                cmd.Parameters.AddWithValue("@ProductSection", objReq.ProductSection);
                cmd.Parameters.AddWithValue("@Section", objReq.Section);
                cmd.Parameters.AddWithValue("@SubSection", objReq.SubSection);
                cmd.Parameters.AddWithValue("@Mobile", objReq.Mobile);
                cmd.Parameters.AddWithValue("@Address", objReq.Address);
                cmd.Parameters.AddWithValue("@NativeAddress", objReq.NativeAddress);
                cmd.Parameters.AddWithValue("@MainOperation", objReq.MainOperation);
                cmd.Parameters.AddWithValue("@MainOpCapHr", objReq.MainOpCapHr);
                cmd.Parameters.AddWithValue("@SecondOperation", objReq.SecondOperation);
                cmd.Parameters.AddWithValue("@SecondOpCapHr", objReq.SecondOpCapHr);
                cmd.Parameters.AddWithValue("@ThirdOperation", objReq.ThirdOperation);
                cmd.Parameters.AddWithValue("@ThirdOpCapHr", objReq.ThirdOpCapHr);
                cmd.Parameters.AddWithValue("@FourthOperation", objReq.FourthOperation);
                cmd.Parameters.AddWithValue("@FourthOpCapHr", objReq.FourthOpCapHr);
                cmd.Parameters.AddWithValue("@FifthOperation", objReq.FifthOperation);
                cmd.Parameters.AddWithValue("@FifthOpCapHr", objReq.FifthOpCapHr);
                cmd.Parameters.AddWithValue("@SixthOperation", objReq.SixthOperation);
                cmd.Parameters.AddWithValue("@SixthOpCapHr", objReq.SixthOpCapHr);
                cmd.Parameters.AddWithValue("@SeventhOperation", objReq.SeventhOperation);
                cmd.Parameters.AddWithValue("@SeventhOpCapHr", objReq.SeventhOpCapHr);
                cmd.Parameters.AddWithValue("@Gender", objReq.Gender);
                cmd.Parameters.AddWithValue("@Shift", objReq.Shift);
                cmd.Parameters.AddWithValue("@Pin", encriptPassword);
                cmd.Parameters.AddWithValue("@PayRoll", objReq.PayRoll);
                cmd.Parameters.AddWithValue("@DOJ", objReq.DOJ);
                cmd.Parameters.AddWithValue("@DOB", objReq.DOB);
                cmd.Parameters.AddWithValue("@Category", objReq.Category);
                cmd.Parameters.AddWithValue("@EmpImageFile", objReq.EmpImageFile);
                cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                cmd.Parameters.AddWithValue("@QueryType", "InsertWorker");

                //cmd.Parameters.AddWithValue("@FatherName", objReq.FatherName);
                //cmd.Parameters.AddWithValue("@Gender", objReq.Gender);
                //cmd.Parameters.AddWithValue("@Grade", objReq.Grade);
                //cmd.Parameters.AddWithValue("@Shift", objReq.Shift);
                //cmd.Parameters.AddWithValue("Pin", encriptPassword);
                //cmd.Parameters.AddWithValue("@EmpRole", objReq.OperatorType);
                //cmd.Parameters.AddWithValue("@LineName", objReq.LineName);
                //cmd.Parameters.AddWithValue("@Mobile", objReq.Mobile);
                //cmd.Parameters.AddWithValue("@Contractor", objReq.Contractor);
                //cmd.Parameters.AddWithValue("@PayRoll", objReq.PayRoll);
                //cmd.Parameters.AddWithValue("@IsTrainee", objReq.IsTrainee);
                //cmd.Parameters.AddWithValue("@IsTemporary", objReq.IsTemporary);
                //cmd.Parameters.AddWithValue("@PermanentSection", objReq.PermanentSection);
                //cmd.Parameters.AddWithValue("@DOJ", objReq.DOJ);
                //cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                //cmd.Parameters.AddWithValue("@QueryType", "InsertWorker");
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
                    objResp.vErrorMsg = "Worker Insertion Failed";
                }
            }
            catch (Exception exp)
            {
                objResp.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Insert_New_Worker", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Response", "Fn_Insert_New_Worker");
            return objResp;
        }

        public List<clsWorker> Fn_Get_Worker(clsWorker objReq)
        {
            var objResp = new List<clsWorker>();
            var obj = new clsWorker();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Get_Worker");
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                //string strSql = "SELECT EM.ID, EM.EmpId, EM.EmpName, EM.FatherName, EM.EmpGender, EM.EmpGrade, EM.EmpShift, EM.EmpPassword,";
                //strSql = strSql + " EM.EmpRole, EM.EmpMobile, EM.Contractor, EM.PayRoll, EM.IsTrainee, EM.IsTemporary, ED.LineName,";
                //strSql = strSql + " EM.PermanentSection, EM.DOJ, EM.CreatedBy, EM.CreatedOn FROM EmployeeMaster EM INNER JOIN EmployeeDetail ED ON EM.EmpId = ED.Code WHERE 1=1";

                string strSql = "SELECT TOP 1000 EmpId, EmpName, EmpGrade, EmpShift, EmpRole, Section,";
                strSql = strSql + " EmpPassword, EmpMobile, PayRoll FROM EmployeeMaster WHERE IsActive = 1";

                //if (objReq.ID != 0 && objReq.ID != null)
                //{
                //    strSql = strSql + " AND EM.ID = @ID";
                //}
                //if (!String.IsNullOrWhiteSpace(objReq.Code))
                //{
                //    strSql = strSql + " AND EM.EmpId = @Code";
                //}

                //if (!String.IsNullOrWhiteSpace(objReq.Code))
                //{
                //    strSql = strSql + " AND EmpId = " + objReq.Code;
                //}
                //if (!String.IsNullOrWhiteSpace(objReq.Name))
                //{
                //    strSql = strSql + " AND EmpName LIKE '%" + objReq.Name + "%'";  
                //}

                if (!String.IsNullOrWhiteSpace(objReq.SearchField))
                {
                    strSql = strSql + " AND (";
                    strSql = strSql + " CAST(EmpId AS VARCHAR(50)) LIKE '%" + objReq.SearchField + "%'";
                    strSql = strSql + " OR EmpName LIKE '%" + objReq.SearchField + "%'";
                    strSql = strSql + " )";
                }

                strSql = strSql + " ORDER BY CreatedOn DESC";

                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;
                //if (objReq.ID != 0 && objReq.ID != null)
                //{
                //    cmd.Parameters.AddWithValue("@ID", objReq.ID);
                //}
                //if (!String.IsNullOrWhiteSpace(objReq.Code))
                //{
                //    cmd.Parameters.AddWithValue("@Code", objReq.Code);
                //}

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsWorker();
                        // string strID = Convert.ToString(ds.Tables[0].Rows[i]["ID"]);
                        //if(strID != "") {
                        //     obj.ID = Convert.ToInt64(ds.Tables[0].Rows[i]["ID"]);
                        // }
                        // obj.Code = Convert.ToString(ds.Tables[0].Rows[i]["EmpId"]);
                        // obj.Name = Convert.ToString(ds.Tables[0].Rows[i]["EmpName"]);
                        // obj.FatherName = Convert.ToString(ds.Tables[0].Rows[i]["FatherName"]);

                        // obj.Gender = Convert.ToString(ds.Tables[0].Rows[i]["EmpGender"]);
                        // obj.Grade = Convert.ToString(ds.Tables[0].Rows[i]["EmpGrade"]);
                        // obj.Shift = Convert.ToString(ds.Tables[0].Rows[i]["EmpShift"]);
                        // string strPin = Convert.ToString(ds.Tables[0].Rows[i]["EmpPassword"]);
                        // obj.Pin = Generic.DecryptText(strPin);
                        // obj.OperatorType = Convert.ToString(ds.Tables[0].Rows[i]["EmpRole"]);
                        // obj.LineName = Convert.ToString(ds.Tables[0].Rows[i]["LineName"]);
                        // obj.Mobile = Convert.ToString(ds.Tables[0].Rows[i]["EmpMobile"]);
                        // obj.Contractor = Convert.ToString(ds.Tables[0].Rows[i]["Contractor"]);
                        // obj.PayRoll = Convert.ToString(ds.Tables[0].Rows[i]["PayRoll"]);
                        // //obj.IsTrainee = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsTrainee"]);
                        // // obj.IsTemporary = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsTemporary"]);
                        // obj.PermanentSection = Convert.ToString(ds.Tables[0].Rows[i]["PermanentSection"]);
                        // obj.DOJ = Convert.ToString(ds.Tables[0].Rows[i]["DOJ"]);
                        // obj.vErrorMsg = "Success";
                        // objResp.Add(obj);
                        // i++;


                        //string strID = Convert.ToString(ds.Tables[0].Rows[i]["ID"]);
                        //if (strID != "")
                        //{
                        //    obj.ID = Convert.ToInt64(ds.Tables[0].Rows[i]["ID"]);
                        //}
                        obj.Code = Convert.ToString(ds.Tables[0].Rows[i]["EmpId"]);
                        obj.Name = Convert.ToString(ds.Tables[0].Rows[i]["EmpName"]);
                        if (ds.Tables[0].Rows[i]["EmpGrade"] == null)
                        {
                            obj.Grade = string.Empty;
                        }
                        else
                        {
                            obj.Grade = Convert.ToString(ds.Tables[0].Rows[i]["EmpGrade"]);
                        }

                        if (ds.Tables[0].Rows[i]["EmpShift"] == null)
                        {
                            obj.Shift = string.Empty;
                        }
                        else
                        {
                            obj.Shift = Convert.ToString(ds.Tables[0].Rows[i]["EmpShift"]);
                        }

                        if (ds.Tables[0].Rows[i]["EmpRole"] == null)
                        {
                            obj.OperatorType = string.Empty;
                        }
                        else
                        {
                            obj.OperatorType = Convert.ToString(ds.Tables[0].Rows[i]["EmpRole"]);
                        }

                        if (ds.Tables[0].Rows[i]["Section"] == null)
                        {
                            obj.Section = string.Empty;
                        }
                        else
                        {
                            obj.Section = Convert.ToString(ds.Tables[0].Rows[i]["Section"]);
                        }

                        string strPin = Convert.ToString(ds.Tables[0].Rows[i]["EmpPassword"]);
                        obj.Pin = Generic.DecryptText(strPin);

                        if (ds.Tables[0].Rows[i]["EmpMobile"] == null)
                        {
                            obj.Mobile = string.Empty;
                        }
                        else
                        {
                            obj.Mobile = Convert.ToString(ds.Tables[0].Rows[i]["EmpMobile"]);
                        }

                        if (ds.Tables[0].Rows[i]["PayRoll"] == null)
                        {
                            obj.PayRoll = string.Empty;
                        }
                        else
                        {
                            obj.PayRoll = Convert.ToString(ds.Tables[0].Rows[i]["PayRoll"]);
                        }

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
                Logger.WriteLog("Function Name : Fn_Get_Worker", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Response", "Fn_Get_Worker");
            return objResp;
        }


        public clsWorker Fn_Update_WorkerDetails(clsWorker objReq)
        {
            var objResp = new clsWorker();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Update_WorkerDetails");
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string encryptPassword = Generic.EncryptText(objReq.Pin);

                SqlCommand cmd = new SqlCommand("USP_WORKER", Con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Code", objReq.Code);
                cmd.Parameters.AddWithValue("@Name", objReq.Name);
                cmd.Parameters.AddWithValue("@EmpDesignation", objReq.EmpDesignation);
                cmd.Parameters.AddWithValue("@EmpRole", objReq.EmpUser);
                cmd.Parameters.AddWithValue("@Unit", objReq.Unit);
                cmd.Parameters.AddWithValue("@Grouping", objReq.Grouping);
                cmd.Parameters.AddWithValue("@ProductSection", objReq.ProductSection);
                cmd.Parameters.AddWithValue("@Section", objReq.Section);
                cmd.Parameters.AddWithValue("@SubSection", objReq.SubSection);
                cmd.Parameters.AddWithValue("@Mobile", objReq.Mobile);
                cmd.Parameters.AddWithValue("@Address", objReq.Address);
                cmd.Parameters.AddWithValue("@NativeAddress", objReq.NativeAddress);
                cmd.Parameters.AddWithValue("@MainOperation", objReq.MainOperation);
                cmd.Parameters.AddWithValue("@MainOpCapHr", objReq.MainOpCapHr);
                cmd.Parameters.AddWithValue("@SecondOperation", objReq.SecondOperation);
                cmd.Parameters.AddWithValue("@SecondOpCapHr", objReq.SecondOpCapHr);
                cmd.Parameters.AddWithValue("@ThirdOperation", objReq.ThirdOperation);
                cmd.Parameters.AddWithValue("@ThirdOpCapHr", objReq.ThirdOpCapHr);
                cmd.Parameters.AddWithValue("@FourthOperation", objReq.FourthOperation);
                cmd.Parameters.AddWithValue("@FourthOpCapHr", objReq.FourthOpCapHr);
                cmd.Parameters.AddWithValue("@FifthOperation", objReq.FifthOperation);
                cmd.Parameters.AddWithValue("@FifthOpCapHr", objReq.FifthOpCapHr);
                cmd.Parameters.AddWithValue("@SixthOperation", objReq.SixthOperation);
                cmd.Parameters.AddWithValue("@SixthOpCapHr", objReq.SixthOpCapHr);
                cmd.Parameters.AddWithValue("@SeventhOperation", objReq.SeventhOperation);
                cmd.Parameters.AddWithValue("@SeventhOpCapHr", objReq.SeventhOpCapHr);
                cmd.Parameters.AddWithValue("@Gender", objReq.Gender);
                cmd.Parameters.AddWithValue("@Shift", objReq.Shift);
                cmd.Parameters.AddWithValue("@Pin", encryptPassword);
                cmd.Parameters.AddWithValue("@PayRoll", objReq.PayRoll);
                cmd.Parameters.AddWithValue("@DOJ", objReq.DOJ);
                cmd.Parameters.AddWithValue("@DOB", objReq.DOB);
                cmd.Parameters.AddWithValue("@EmpImageFile", objReq.EmpImageFile);
                cmd.Parameters.AddWithValue("@Category", objReq.Category);
                cmd.Parameters.AddWithValue("@IsActive", objReq.IsActive);
                cmd.Parameters.AddWithValue("@ModifiedBy", objReq.ModifiedBy);
                cmd.Parameters.AddWithValue("@QueryType", "UpdateWorkerDetails");

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
                    objResp.vErrorMsg = "Worker Updation Failed";
                }
            }
            catch (Exception exp)
            {
                objResp.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Update_WorkerDetails", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Response", "Fn_Update_WorkerDetails");
            return objResp;
        }


        public clsWorker Fn_Fetch_WorkerDetails_ByID(clsWorker objReq)
        {
            var objResp = new clsWorker();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Fetch_WorkerDetails_ByID");
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_WORKER", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Code", objReq.Code);
                cmd.Parameters.AddWithValue("@QueryType", "FetchDetailsByID");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;

                if (ds.Tables[0].Rows.Count > 0)
                {
                    objResp.Code = Convert.ToString(ds.Tables[0].Rows[i]["EmpId"]);
                    objResp.Name = Convert.ToString(ds.Tables[0].Rows[i]["EmpName"]);
                    
                    if (ds.Tables[0].Rows[i]["EmpDesignation"] == DBNull.Value)
                    {
                        objResp.EmpDesignation = string.Empty;
                    }
                    else
                    {
                        objResp.EmpDesignation = Convert.ToString(ds.Tables[0].Rows[i]["EmpDesignation"]);
                    }

                    if (ds.Tables[0].Rows[i]["EmpRole"] == DBNull.Value)
                    {
                        objResp.EmpUser = string.Empty;
                    }
                    else
                    {
                        objResp.EmpUser = Convert.ToString(ds.Tables[0].Rows[i]["EmpRole"]);
                    }

                    if (ds.Tables[0].Rows[i]["Unit"] == DBNull.Value)
                    {
                        objResp.Unit = string.Empty;
                    }
                    else
                    {
                        objResp.Unit = Convert.ToString(ds.Tables[0].Rows[i]["Unit"]);
                    }

                    if (ds.Tables[0].Rows[i]["Grouping"] == DBNull.Value)
                    {
                        objResp.Grouping = string.Empty;
                    }
                    else
                    {
                        objResp.Grouping = Convert.ToString(ds.Tables[0].Rows[i]["Grouping"]);
                    }

                    if (ds.Tables[0].Rows[i]["ProductSection"] == DBNull.Value)
                    {
                        objResp.ProductSection = string.Empty;
                    }
                    else
                    {
                        objResp.ProductSection = Convert.ToString(ds.Tables[0].Rows[i]["ProductSection"]);
                    }

                    if (ds.Tables[0].Rows[i]["Section"] == DBNull.Value)
                    {
                        objResp.Section = string.Empty;
                    }
                    else
                    {
                        objResp.Section = Convert.ToString(ds.Tables[0].Rows[i]["Section"]);
                    }

                    if (ds.Tables[0].Rows[i]["SubSection"] == DBNull.Value)
                    {
                        objResp.SubSection = string.Empty;
                    }
                    else
                    {
                        objResp.SubSection = Convert.ToString(ds.Tables[0].Rows[i]["SubSection"]);
                    }

                    if (ds.Tables[0].Rows[i]["EmpMobile"] == DBNull.Value)
                    {
                        objResp.Mobile = string.Empty;
                    }
                    else
                    {
                        objResp.Mobile = Convert.ToString(ds.Tables[0].Rows[i]["EmpMobile"]);
                    }

                    if (ds.Tables[0].Rows[i]["Address"] == DBNull.Value)
                    {
                        objResp.Address = string.Empty;
                    }
                    else
                    {
                        objResp.Address = Convert.ToString(ds.Tables[0].Rows[i]["Address"]);
                    }

                    if (ds.Tables[0].Rows[i]["NativeAddress"] == DBNull.Value)
                    {
                        objResp.NativeAddress = string.Empty;
                    }
                    else
                    {
                        objResp.NativeAddress = Convert.ToString(ds.Tables[0].Rows[i]["NativeAddress"]);
                    }

                    if (ds.Tables[0].Rows[i]["MainOperation"] == DBNull.Value)
                    {
                        objResp.MainOperation = string.Empty;
                    }
                    else
                    {
                        objResp.MainOperation = Convert.ToString(ds.Tables[0].Rows[i]["MainOperation"]);
                    }

                    if (ds.Tables[0].Rows[i]["MainOpCapHr"] == DBNull.Value)
                    {
                        objResp.MainOpCapHr = string.Empty;
                    }
                    else
                    {
                        objResp.MainOpCapHr = Convert.ToString(ds.Tables[0].Rows[i]["MainOpCapHr"]);
                    }

                    if (ds.Tables[0].Rows[i]["SecondOperation"] == DBNull.Value)
                    {
                        objResp.SecondOperation = string.Empty;
                    }
                    else
                    {
                        objResp.SecondOperation = Convert.ToString(ds.Tables[0].Rows[i]["SecondOperation"]);
                    }

                    if (ds.Tables[0].Rows[i]["SecondOpCapHr"] == DBNull.Value)
                    {
                        objResp.SecondOpCapHr = string.Empty;
                    }
                    else
                    {
                        objResp.SecondOpCapHr = Convert.ToString(ds.Tables[0].Rows[i]["SecondOpCapHr"]);
                    }

                    if (ds.Tables[0].Rows[i]["ThirdOperation"] == DBNull.Value)
                    {
                        objResp.ThirdOperation = string.Empty;
                    }
                    else
                    {
                        objResp.ThirdOperation = Convert.ToString(ds.Tables[0].Rows[i]["ThirdOperation"]);
                    }

                    if (ds.Tables[0].Rows[i]["ThirdOpCapHr"] == DBNull.Value)
                    {
                        objResp.ThirdOpCapHr = string.Empty;
                    }
                    else
                    {
                        objResp.ThirdOpCapHr = Convert.ToString(ds.Tables[0].Rows[i]["ThirdOpCapHr"]);
                    }

                    if (ds.Tables[0].Rows[i]["FourthOperation"] == DBNull.Value)
                    {
                        objResp.FourthOperation = string.Empty;
                    }
                    else
                    {
                        objResp.FourthOperation = Convert.ToString(ds.Tables[0].Rows[i]["FourthOperation"]);
                    }

                    if (ds.Tables[0].Rows[i]["FourthOpCapHr"] == DBNull.Value)
                    {
                        objResp.FourthOpCapHr = string.Empty;
                    }
                    else
                    {
                        objResp.FourthOpCapHr = Convert.ToString(ds.Tables[0].Rows[i]["FourthOpCapHr"]);
                    }

                    if (ds.Tables[0].Rows[i]["FifthOperation"] == DBNull.Value)
                    {
                        objResp.FifthOperation = string.Empty;
                    }
                    else
                    {
                        objResp.FifthOperation = Convert.ToString(ds.Tables[0].Rows[i]["FifthOperation"]);
                    }

                    if (ds.Tables[0].Rows[i]["FifthOpCapHr"] == DBNull.Value)
                    {
                        objResp.FifthOpCapHr = string.Empty;
                    }
                    else
                    {
                        objResp.FifthOpCapHr = Convert.ToString(ds.Tables[0].Rows[i]["FifthOpCapHr"]);
                    }

                    if (ds.Tables[0].Rows[i]["SixthOperation"] == DBNull.Value)
                    {
                        objResp.SixthOperation = string.Empty;
                    }
                    else
                    {
                        objResp.SixthOperation = Convert.ToString(ds.Tables[0].Rows[i]["SixthOperation"]);
                    }

                    if (ds.Tables[0].Rows[i]["SixthOpCapHr"] == DBNull.Value)
                    {
                        objResp.SixthOpCapHr = string.Empty;
                    }
                    else
                    {
                        objResp.SixthOpCapHr = Convert.ToString(ds.Tables[0].Rows[i]["SixthOpCapHr"]);
                    }

                    if (ds.Tables[0].Rows[i]["SeventhOperation"] == DBNull.Value)
                    {
                        objResp.SeventhOperation = string.Empty;
                    }
                    else
                    {
                        objResp.SeventhOperation = Convert.ToString(ds.Tables[0].Rows[i]["SeventhOperation"]);
                    }

                    if (ds.Tables[0].Rows[i]["SeventhOpCapHr"] == DBNull.Value)
                    {
                        objResp.SeventhOpCapHr = string.Empty;
                    }
                    else
                    {
                        objResp.SeventhOpCapHr = Convert.ToString(ds.Tables[0].Rows[i]["SeventhOpCapHr"]);
                    }

                    if (ds.Tables[0].Rows[i]["EmpGender"] == DBNull.Value)
                    {
                        objResp.Gender = string.Empty;
                    }
                    else
                    {
                        objResp.Gender = Convert.ToString(ds.Tables[0].Rows[i]["EmpGender"]);
                    }

                    if (ds.Tables[0].Rows[i]["EmpShift"] == DBNull.Value)
                    {
                        objResp.Shift = string.Empty;
                    }
                    else
                    {
                        objResp.Shift = Convert.ToString(ds.Tables[0].Rows[i]["EmpShift"]);
                    }

                    string strPassword = Convert.ToString(ds.Tables[0].Rows[i]["EmpPassword"]);
                    objResp.Pin = Generic.DecryptText(strPassword);

                    if (ds.Tables[0].Rows[i]["PayRoll"] == DBNull.Value)
                    {
                        objResp.PayRoll = string.Empty;
                    }
                    else
                    {
                        objResp.PayRoll = Convert.ToString(ds.Tables[0].Rows[i]["PayRoll"]);
                    }

                    if (ds.Tables[0].Rows[i]["DOJ"] == DBNull.Value)
                    {
                        objResp.DOJ = string.Empty;                        
                    }
                    else
                    {
                        objResp.DOJ = Convert.ToDateTime(ds.Tables[0].Rows[i]["DOJ"]).ToString("dd-MMM-yyyy");
                    }

                    if (ds.Tables[0].Rows[i]["DOB"] == DBNull.Value)
                    {
                        objResp.DOB = string.Empty;
                    }
                    else
                    {
                        objResp.DOB = Convert.ToDateTime(ds.Tables[0].Rows[i]["DOB"]).ToString("dd-MMM-yyyy");
                    }

                    if (ds.Tables[0].Rows[i]["Category"] == DBNull.Value)
                    {
                        objResp.Category = string.Empty;
                    }
                    else
                    {
                        objResp.Category = Convert.ToString(ds.Tables[0].Rows[i]["Category"]);
                    }

                    if (ds.Tables[0].Rows[i]["EmpImageFile"] == DBNull.Value)
                    {
                        objResp.EmpImageFile = string.Empty;
                    }
                    else
                    {
                        objResp.EmpImageFile = Convert.ToString(ds.Tables[0].Rows[i]["EmpImageFile"]);
                    }

                    objResp.IsActive = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsActive"]);

                    objResp.vErrorMsg = "Success";
                    objResp.vErrorCode = 200;
                }
                else
                {
                    objResp.vErrorCode = 404;
                    objResp.vErrorMsg = "Worker details are not found.";
                }
            }
            catch (Exception exp)
            {
                objResp.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Fetch_WorkerDetails_ByID", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Fetch_WorkerDetails_ByID");
            return objResp;
        }

        #endregion End Worker



        public clsDesignation Fn_Insert_New_Designation(clsDesignation objReq)
        {
            var objResp = new clsDesignation();
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_MASTERENTRY", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DesignationName", objReq.DesignationName);
                cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                cmd.Parameters.AddWithValue("@QueryType", "InsertDesignation");

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
                    objResp.vErrorMsg = "Inserting Designation Failed.";
                }
            }
            catch (Exception exp)
            {
                objResp.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Insert_New_Designation", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }


        public List<clsDesignation> Fn_Get_All_Designation(clsDesignation objReq)
        {
            var objResp = new List<clsDesignation>();
            var obj = new clsDesignation();
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT DISTINCT DesignationID, DesignationName, CreatedBy,";
                strSql = strSql + " FORMAT(CreatedOn, 'dd-MMM-yyyy') AS CreatedOn FROM DesignationMaster WHERE 1=1";
                    
                if (objReq.DesignationID != 0)
                {
                    strSql = strSql + " AND DesignationID = @DesignationID";
                }
                if (objReq.DesignationName != "" && objReq.DesignationName != null)
                {
                    strSql = strSql + " AND DesignationName LIKE '%@DesignationName%'";
                }

                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;

                if (objReq.DesignationID != 0)
                {
                    cmd.Parameters.AddWithValue("@DesignationID", objReq.DesignationID);
                }
                if (objReq.DesignationName != "" && objReq.DesignationName != null)
                {
                    cmd.Parameters.AddWithValue("@DesignationName", objReq.DesignationName);
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsDesignation();
                        obj.DesignationID = Convert.ToInt64(ds.Tables[0].Rows[i]["DesignationID"]);
                        obj.DesignationName = Convert.ToString(ds.Tables[0].Rows[i]["DesignationName"]);
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
                    obj.vErrorMsg = "No Record found.";
                    objResp.Add(obj);
                }
            }
            catch (Exception exp)
            {
                obj.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Get_All_Designation", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }



        public clsDesignation Fn_Update_DesignationDetails_By_ID(clsDesignation objReq)
        {
            var objResp = new clsDesignation();
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_MASTERENTRY", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DesignationID", objReq.DesignationID);
                cmd.Parameters.AddWithValue("@DesignationName", objReq.DesignationName);
                cmd.Parameters.AddWithValue("@ModifiedBy", objReq.ModifiedBy);
                cmd.Parameters.AddWithValue("@QueryType", "UpdateDesignation");

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
                    objResp.vErrorMsg = "Updating Failed";
                }
            }
            catch (Exception exp)
            {
                objResp.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Update_DesignationDetails_By_ID", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }


        public clsColor Fn_Insert_New_Color(clsColor objReq)
        {
            var objResp = new clsColor();
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("", objReq.ColorName);
                cmd.Parameters.AddWithValue("", objReq.CreatedBy);
                cmd.Parameters.AddWithValue("@QueryType", "InsertColor");

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
                    objResp.vErrorMsg = "Inserting Color Failed.";
                }
            }
            catch (Exception exp)
            {
                objResp.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Insert_New_Color", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }


        public List<clsColor> Fn_Get_All_Color(clsColor objReq)
        {
            var objResp = new List<clsColor>();
            var obj = new clsColor();
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT Id, ColorName, CreatedBy,";
                strSql = strSql + " FORMAT(CreatedOn, 'dd-MMM-yyyy') AS CreatedOn FROM ColorMaster WHERE 1=1";

                if (objReq.ColorID != 0)
                {
                    strSql = strSql + " AND Id = @ColorID";
                }
                if (objReq.ColorName != "" && objReq.ColorName != null)
                {
                    strSql = strSql + " AND ColorName LIKE '%ColorName%'";
                }

                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;

                if (objReq.ColorID != 0)
                {
                    cmd.Parameters.AddWithValue("", objReq.ColorID);
                }
                if (objReq.ColorName != "" && objReq.ColorName != null)
                {
                    cmd.Parameters.AddWithValue("", objReq.ColorName);
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;

                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsColor();
                        obj.ColorID = Convert.ToInt64(ds.Tables[0].Rows[i]["Id"]);
                        obj.ColorName = Convert.ToString(ds.Tables[0].Rows[i]["ColorName"]);
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
                    obj.vErrorMsg = "No Record found.";
                    objResp.Add(obj);
                }
            }
            catch (Exception exp)
            {
                obj.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Get_All_Color", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }


        public clsColor Fn_Update_ColorDetails_By_ID(clsColor objReq)
        {
            var objResp = new clsColor();
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("", objReq.ColorID);
                cmd.Parameters.AddWithValue("", objReq.ColorName);
                cmd.Parameters.AddWithValue("", objReq.ModifiedBy);
                cmd.Parameters.AddWithValue("@QueryType", "UpdateColor");

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
                    objResp.vErrorMsg = "Updating Failed";
                }
            }
            catch (Exception exp)
            {
                objResp.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Update_ColorDetails_By_ID", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }

        #region Start Fn_Update_GridName 14-MAY_2026

        public clsSizeMaster Fn_Update_GridName(clsSizeMaster objReq)
        {
            var objResp = new clsSizeMaster();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Update_GridName");
            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_MASTERENTRY", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Grid", objReq.Grid); // NewGrid
                cmd.Parameters.AddWithValue("@SizeName", objReq.SizeName); // OdlGrid
                cmd.Parameters.AddWithValue("@ModifiedBy", objReq.ModifiedBy);
                cmd.Parameters.AddWithValue("@QueryType", "UpdateGridName");
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
                    objResp.vErrorMsg = "Grid Updating Failed";
                }
            }
            catch (Exception exp)
            {
                objResp.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Update_GridName", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Update_GridName");
            return objResp;
        }

        public List<clsSizeMaster> Fn_Get_All_GridName(clsSizeMaster objReq)
        {
            var objResp = new List<clsSizeMaster>();
            var obj = new clsSizeMaster();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Get_All_GridName");
            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT DISTINCT Grid FROM SizeMaster WHERE 1=1";
                if (objReq.ID != 0)
                {
                    strSql = strSql + " AND Grid = @Grid";
                }
                strSql = strSql + " ORDER BY Grid ASC ";

                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;
                if (!String.IsNullOrWhiteSpace(objReq.Grid))
                {
                    cmd.Parameters.AddWithValue("@Grid", objReq.Grid);
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsSizeMaster();
                        obj.Grid = Convert.ToString(ds.Tables[0].Rows[i]["Grid"]);

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
                Logger.WriteLog("Function Name : Fn_Get_All_GridName", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Get_All_GridName");
            return objResp;
        }

        public clsSizeMaster Fn_Delete_Grid(clsSizeMaster objReq)
        {
            var objResp = new clsSizeMaster();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Response", "Fn_Delete_Grid");
            try
            {
                if (String.IsNullOrWhiteSpace(objReq.Grid))
                {
                    objResp.vErrorCode = 400;
                    objResp.vErrorMsg = "Please send grid";
                }
                else
                {
                    if (Con.State == ConnectionState.Broken)
                    { Con.Close(); }
                    if (Con.State == ConnectionState.Closed)
                    { Con.Open(); }

                    SqlCommand cmd = new SqlCommand("USP_MASTERENTRY", Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", objReq.ID);
                    cmd.Parameters.AddWithValue("@Grid", objReq.Grid);
                    cmd.Parameters.AddWithValue("@QueryType", "DeleteGrid");
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
                        objResp.vErrorMsg = "Grid Deleting Failed";
                    }
                }
            }
            catch (Exception exp)
            {
                objResp.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Delete_Grid", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Delete_Grid");
            return objResp;
        }

        #endregion End Fn_Update_GridName 14-MAY-2026 




        public List<clsWorker> Fn_Fill_AttendanceMonthYear(clsWorker objReq)
        {
            var objResp = new List<clsWorker>();
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_WORKER", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@QueryType", "FillMonthYear");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        var objItem = new clsWorker();
                        objItem.MonthName = Convert.ToString(ds.Tables[0].Rows[i]["MonthName"]);
                        objItem.MonthNumber = Convert.ToString(ds.Tables[0].Rows[i]["MonthNumber"]);
                        objItem.YearValue = Convert.ToString(ds.Tables[0].Rows[i]["YearValue"]);

                        objItem.vErrorMsg = "Success";
                        objResp.Add(objItem);
                        i++;
                    }
                }
                else
                {
                    var objItem = new clsWorker();
                    objItem.vErrorMsg = "Month Year not found.";
                    objResp.Add(objItem);
                }
            }
            catch (Exception exp)
            {
                Logger.WriteLog("Function Name : Fn_Fill_AttendanceMonthYear", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                var objItem = new clsWorker();
                objItem.vErrorMsg = exp.Message.ToString();
                objResp.Add(objItem);
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }


        public List<clsWorker> Fn_Fetch_AttendanceDetailsByID(clsWorker objReq)
        {
            var objResp = new List<clsWorker>();
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_WORKER", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Code", objReq.Code);
                cmd.Parameters.AddWithValue("@MonthNumber", objReq.MonthNumber);
                cmd.Parameters.AddWithValue("@YearValue", objReq.YearValue);
                cmd.Parameters.AddWithValue("@QueryType", "FetchAttendance");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        var objItem = new clsWorker();
                        objItem.Code = Convert.ToString(ds.Tables[0].Rows[i]["EmpCode"]);
                        objItem.AttendenceDate = Convert.ToString(ds.Tables[0].Rows[i]["AttDate"]);

                        if (ds.Tables[0].Rows[i]["StartTime"] == DBNull.Value)
                        {
                            objItem.StartTime = string.Empty;
                        }
                        else
                        {
                            objItem.StartTime = Convert.ToString(ds.Tables[0].Rows[i]["StartTime"]);
                        }

                        if (ds.Tables[0].Rows[i]["EndTime"] == DBNull.Value)
                        {
                            objItem.EndTime = string.Empty;
                        }
                        else
                        {
                            objItem.EndTime = Convert.ToString(ds.Tables[0].Rows[i]["EndTime"]);
                        }

                        objItem.OTNoOfHrs = Convert.ToInt32(ds.Tables[0].Rows[i]["OTNoOfHrs"]);
                        objItem.DayName = Convert.ToString(ds.Tables[0].Rows[i]["DayName"]);
                        //objItem.AttendanceStatus = Convert.ToString(ds.Tables[0].Rows[i]["AttendanceStatus"]);
                        //objItem.IsOvertime = Convert.ToString(ds.Tables[0].Rows[i]["IsOvertime"]);
                        //objItem.Unit = Convert.ToString(ds.Tables[0].Rows[i]["Division"]);

                        objItem.vErrorMsg = "Success";
                        objResp.Add(objItem);
                        i++;
                    }
                }
                else
                {
                    var objItem = new clsWorker();
                    objItem.vErrorMsg = "Attendance data not found.";
                    objResp.Add(objItem);
                }
            }
            catch (Exception exp)
            {
                Logger.WriteLog("Function Name : Fn_Fetch_AttendanceDetailsByID", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                var objItem = new clsWorker();
                objItem.vErrorMsg = exp.Message.ToString();
                objResp.Add(objItem);
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }


    }
}