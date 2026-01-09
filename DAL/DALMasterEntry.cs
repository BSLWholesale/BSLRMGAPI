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
    public class DALMasterEntry
    {
        SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["BSL"].ConnectionString);

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

        public clsDivision Fn_Update_Division(clsDivision objReq)
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
                Logger.WriteLog("Function Name : Fn_Add_New_Division", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }
        public clsDivision Fn_Delete_Division(clsDivision objReq)
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
                cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
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
                Logger.WriteLog("Function Name : Fn_Delete_Division", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
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

                string strSql = "SELECT ID, Division, FORMAT(CreatedOn, 'dd-MMM-yyyy') AS CreatedOn FROM DivisionMaster WHERE 1=1";
                if (objReq.ID != 0)
                {
                    strSql = strSql + " AND ID = @ID ";
                }
                if (objReq.Division != "")
                {
                    strSql = strSql + " AND Division LIKE '%@Division%' ";
                }

                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;
                if (objReq.ID != 0)
                {
                    cmd.Parameters.AddWithValue("@ID", objReq.ID);
                }
                if (objReq.Division != "")
                {
                    cmd.Parameters.AddWithValue("@Division", objReq.Division);
                }
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
                        obj.Division = Convert.ToString(ds.Tables[0].Rows[i]["vDivision"]);
                        obj.CreatedOn = Convert.ToString(ds.Tables[0].Rows[i]["CreatedOn"]);
                        obj.CreatedBy = Convert.ToInt16(ds.Tables[0].Rows[i]["CreatedBy"]);

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

        public clsSection Fn_Update_Section(clsSection objReq)
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
                cmd.Parameters.AddWithValue("@vSectionName", objReq.SectionName);
                cmd.Parameters.AddWithValue("@vSectionHead", objReq.SectionHead);
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
                Logger.WriteLog("Function Name : Fn_Update_Section", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }
        public clsSection Fn_Delete_Section(clsSection objReq)
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
                cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
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
                Logger.WriteLog("Function Name : Fn_Delete_Section", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
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

                string strSql = "SELECT SectionID, SectionName, SectionHead, CreatedBy,";
                strSql = strSql + " FORMAT(CreatedOn, 'dd-MMM-yyyy') AS CreatedOn FROM SectionMaster WHERE 1=1";
                if (objReq.SectionID != 0)
                {
                    strSql = strSql + " AND SectionID = @SectionID ";
                }
                if (objReq.SectionName != "")
                {
                    strSql = strSql + " AND SectionName LIKE '%@SectionName%; ";
                }
                if (objReq.SectionHead != "")
                {
                    strSql = strSql + " AND SectionHead LIKE '%@SectionHead%; ";
                }

                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;
                if (objReq.SectionID != 0)
                {
                    cmd.Parameters.AddWithValue("@SectionID", objReq.SectionID);
                }
                if (objReq.SectionName != "")
                {
                    cmd.Parameters.AddWithValue("@SectionName", objReq.SectionName);
                }
                if (objReq.SectionHead != "")
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
                        obj.SectionName = Convert.ToString(ds.Tables[0].Rows[i]["vSectionName"]);
                        obj.SectionHead = Convert.ToString(ds.Tables[0].Rows[i]["vSectionHead"]);
                        obj.CreatedOn = Convert.ToString(ds.Tables[0].Rows[i]["CreatedOn"]);
                        obj.CreatedBy = Convert.ToInt16(ds.Tables[0].Rows[i]["CreatedBy"]);

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

        public clsLine Fn_Update_Line(clsLine objReq)
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
                Logger.WriteLog("Function Name : Fn_Update_Line", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }
        public clsLine Fn_Delete_Line(clsLine objReq)
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
                cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
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
                Logger.WriteLog("Function Name : Fn_Delete_Line", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
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

                string strSql = "SELECT DivisionID, SeqNo, LineCode, LineName, SuperVisor, SectionName,";
                strSql = strSql + " SuperMarketCode, IsQuality, IsFinishing, CreatedBy, ";
                strSql = strSql + " FORMAT(CreatedOn, 'dd-MMM-yyyy') AS CreatedOn FROM LineMaster WHERE 1=1";

                if (objReq.LineCode != "")
                {
                    strSql = strSql + " AND LineCode = @LineCode ";
                }
                if (objReq.LineName != "")
                {
                    strSql = strSql + " AND LineName LIKE '%@LineName%; ";
                }


                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;
                if (objReq.LineCode != "")
                {
                    cmd.Parameters.AddWithValue("@LineCode", objReq.LineCode);
                }
                if (objReq.LineName != "")
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
                        obj.Section = Convert.ToString(ds.Tables[0].Rows[i]["Section"]);
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

            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_MASTERENTRY", Con);
                cmd.CommandType = CommandType.StoredProcedure;
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

        public clsCustomer Fn_Update_Customer(clsCustomer objReq)
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
                Logger.WriteLog("Function Name : Fn_Update_Shift", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }
        public clsCustomer Fn_Delete_Customer(clsCustomer objReq)
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
                cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
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
                Logger.WriteLog("Function Name : Fn_Delete_Customer", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
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
                strSql = strSql + " FORMAT(CreatedOn, 'dd-MMM-yyyy') AS CreatedOn FROM CustomerMaster WHERE 1=1";
                if (objReq.vName != "")
                {
                    strSql = strSql + " AND vName = @vName ";
                }
                if (objReq.CodeNo != "")
                {
                    strSql = strSql + " AND CodeNo = @CodeNo ";
                }
                if (objReq.vContact != "")
                {
                    strSql = strSql + " AND vContact = @vContact ";
                }

                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;
                if (objReq.vName != "")
                {
                    cmd.Parameters.AddWithValue("@vName", objReq.vName);
                }
                if (objReq.CodeNo != "")
                {
                    cmd.Parameters.AddWithValue("@CodeNo", objReq.CodeNo);
                }
                if (objReq.vContact != "")
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

            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_MASTERENTRY", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@vName", objReq.SizeName);
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
                cmd.Parameters.AddWithValue("@vName", objReq.SizeName);
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
        public clsSizeMaster Fn_Delete_Customer(clsSizeMaster objReq)
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

                string strSql = "SELECT ID, SizeName, CreatedBy, FORMAT(CreatedOn, 'dd-MMM-yyyy') AS CreatedOn FROM SizeMaster WHERE 1=1";
                if (objReq.ID != 0)
                {
                    strSql = strSql + " AND ID = @ID";
                }
                if (objReq.SizeName != "")
                {
                    strSql = strSql + " AND SizeName LIKE '%@ID%' ";
                }

                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;
                if (objReq.ID != 0)
                {
                    cmd.Parameters.AddWithValue("@ID", objReq.ID);
                }
                if (objReq.SizeName != "")
                {
                    cmd.Parameters.AddWithValue("@SizeName", objReq.SizeName);
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
                        obj.ID = Convert.ToInt16(ds.Tables[0].Rows[i]["ID"]);
                        obj.SizeName = Convert.ToString(ds.Tables[0].Rows[i]["SizeName"]);
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
    }
}