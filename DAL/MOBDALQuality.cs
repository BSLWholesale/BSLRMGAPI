using BSLDaman.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;

namespace BSLDaman.DAL
{
    public class MOBDALQuality
    {
        
        SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["BSL"].ConnectionString);
        DALOrder _DALOrder = new DALOrder(); 

        public List<clsQAOrderList> Fn_Fetch_AllOrderNumbers(clsQAOrderList objReq)
        {
            var objResp = new List<clsQAOrderList>();
            var obj = new clsQAOrderList();
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                SqlCommand cmd = new SqlCommand("USP_MobileQualityAnalysis", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@QueryType", "GetAllOrderNumbers");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;

                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsQAOrderList();
                        obj.OrderNo = Convert.ToString(ds.Tables[0].Rows[i]["OrderNo"]);
                        obj.Product = Convert.ToString(ds.Tables[0].Rows[i]["Product"]);
                        obj.Qty = Convert.ToInt64(ds.Tables[0].Rows[i]["Qty"]);
                        obj.vErrorCode = 200;
                        obj.vErrorMsg = "Success";
                        objResp.Add(obj);
                        i++;
                    }
                }
                else
                {
                    obj.vErrorCode = 404;
                    obj.vErrorMsg = "Order Numbers records are not found.";
                    objResp.Add(obj);
                }
            }
            catch (Exception exp)
            {
                obj.vErrorCode = 500;
                Logger.WriteLog("Function Name : ", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            return objResp;
        }

        #region Start Fn_Get_QA_checkPoint_SubSection 28-APR-2026

        public List<clsQACheckPoint> Fn_Get_QA_checkPoint(clsQACheckPoint objReq)
        {
            var objResp = new List<clsQACheckPoint>();
            var obj = new clsQACheckPoint();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Get_QA_checkPoint");
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT  DISTINCT Products, SubSection from  QACheckPointMaster WHERE 1=1";
                if (!String.IsNullOrWhiteSpace(objReq.Products))
                {
                    strSql = strSql + " AND Products = @Products";
                }
                if (!String.IsNullOrWhiteSpace(objReq.SubSection))
                {
                    strSql = strSql + " AND SubSection = @SubSection";
                }
                strSql = strSql + " ORDER BY SubSection ";


                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;
                if (!String.IsNullOrWhiteSpace(objReq.Products))
                {
                    cmd.Parameters.AddWithValue("@Products", objReq.Products);
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
                        obj = new clsQACheckPoint();
                        obj.Products = Convert.ToString(ds.Tables[0].Rows[i]["Products"]);
                        obj.SubSection = Convert.ToString(ds.Tables[0].Rows[i]["SubSection"]);
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
                Logger.WriteLog("Function Name : Fn_Get_QA_checkPoint", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Get_QA_checkPoint");
            return objResp;
        }

        public List<clsQADefects> Fn_Get_QA_Defects(clsQADefects objReq)
        {
            var objResp = new List<clsQADefects>();
            var obj = new clsQADefects();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Get_QA_Defects");
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT DISTINCT DefectsID, Products, SubSection, Defects, ImageName from  QACheckPointMaster WHERE 1=1";
                if (!String.IsNullOrWhiteSpace(objReq.Products))
                {
                    strSql = strSql + " AND Products = @Products";
                }
                if (!String.IsNullOrWhiteSpace(objReq.SubSection))
                {
                    strSql = strSql + " AND SubSection = @SubSection";
                }
                strSql = strSql + " ORDER BY SubSection, Defects ";


                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;
                if (!String.IsNullOrWhiteSpace(objReq.Products))
                {
                    cmd.Parameters.AddWithValue("@Products", objReq.Products);
                }
                if (!String.IsNullOrWhiteSpace(objReq.SubSection))
                {
                    cmd.Parameters.AddWithValue("@SubSection", objReq.SubSection);
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;
                string strImgPath = ConfigurationManager.AppSettings["GetDefectImgPath"];
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsQADefects();
                        obj.ID = Convert.ToInt64(ds.Tables[0].Rows[i]["DefectsID"]);
                        obj.Products = Convert.ToString(ds.Tables[0].Rows[i]["Products"]);
                        obj.SubSection = Convert.ToString(ds.Tables[0].Rows[i]["SubSection"]);
                        obj.Defects = Convert.ToString(ds.Tables[0].Rows[i]["Defects"]);
                        string strImageName = Convert.ToString(ds.Tables[0].Rows[i]["ImageName"]);
                        if (strImageName != "")
                        {
                            obj.ImageName = strImgPath + Convert.ToString(ds.Tables[0].Rows[i]["ImageName"]);
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
                Logger.WriteLog("Function Name : Fn_Get_QA_Defects", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Get_QA_Defects");
            return objResp;
        }

        #endregion End Fn_Get_QA_checkPoint_SubSection 28-APR-2026

        #region Strt Fn_Get_QA_Order_Color 29-APR-2026

        public List<clsQAColors> Fn_Get_QA_Order_Color(clsQAColors objReq)
        {
            var objResp = new List<clsQAColors>();
            var obj = new clsQAColors();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Get_QA_Order_Color");
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT DISTINCT ColorName, OrderNo FROM BundleCompile WHERE 1=1";
                if (!String.IsNullOrWhiteSpace(objReq.ColorName))
                {
                    strSql = strSql + " AND ColorName = @ColorName";
                }
                if (!String.IsNullOrWhiteSpace(objReq.OrderNo))
                {
                    strSql = strSql + " AND OrderNo = @OrderNo ";
                }
                strSql = strSql + " ORDER BY ColorName ASC ";

                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;
                if (!String.IsNullOrWhiteSpace(objReq.ColorName))
                {
                    cmd.Parameters.AddWithValue("@ColorName", objReq.ColorName);
                }
                if (!String.IsNullOrWhiteSpace(objReq.OrderNo))
                {
                    cmd.Parameters.AddWithValue("@OrderNo", objReq.OrderNo);
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsQAColors();
                        obj.OrderNo = Convert.ToString(ds.Tables[0].Rows[i]["OrderNo"]);
                        obj.ColorName = Convert.ToString(ds.Tables[0].Rows[i]["ColorName"]);
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
                Logger.WriteLog("Function Name : Fn_Get_QA_Order_Color", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Get_QA_Order_Color");
            return objResp;
        }

        public List<clsQASize> Fn_Get_QA_Order_Size(clsQASize objReq)
        {
            var objResp = new List<clsQASize>();
            var obj = new clsQASize();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Get_QA_Order_Size");
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT DISTINCT SizeName, ColorName, OrderNo FROM BundleCompile WHERE 1=1";
                if (!String.IsNullOrWhiteSpace(objReq.SizeName))
                {
                    strSql = strSql + " AND SizeName = @SizeName";
                }
                if (!String.IsNullOrWhiteSpace(objReq.ColorName))
                {
                    strSql = strSql + " AND ColorName = @ColorName";
                }
                if (!String.IsNullOrWhiteSpace(objReq.OrderNo))
                {
                    strSql = strSql + " AND OrderNo = @OrderNo ";
                }
                strSql = strSql + " ORDER BY SizeName ASC ";

                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;
                if (!String.IsNullOrWhiteSpace(objReq.SizeName))
                {
                    cmd.Parameters.AddWithValue("@SizeName", objReq.SizeName);
                }
                if (!String.IsNullOrWhiteSpace(objReq.ColorName))
                {
                    cmd.Parameters.AddWithValue("@ColorName", objReq.ColorName);
                }
                if (!String.IsNullOrWhiteSpace(objReq.OrderNo))
                {
                    cmd.Parameters.AddWithValue("@OrderNo", objReq.OrderNo);
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsQASize();
                        obj.OrderNo = Convert.ToString(ds.Tables[0].Rows[i]["OrderNo"]);
                        obj.ColorName = Convert.ToString(ds.Tables[0].Rows[i]["ColorName"]);
                        obj.SizeName = Convert.ToString(ds.Tables[0].Rows[i]["SizeName"]);
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
                Logger.WriteLog("Function Name : Fn_Get_QA_Order_Size", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Get_QA_Order_Size");
            return objResp;
        }

        public List<clsQASubSection> Fn_Get_QA_Order_SubSection(clsQASubSection objReq)
        {
            var objResp = new List<clsQASubSection>();
            var obj = new clsQASubSection();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Get_QA_Order_SubSection");
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT DISTINCT SubSection, OrderNo FROM BundleCompile WHERE 1=1";
                if (!String.IsNullOrWhiteSpace(objReq.SubSection))
                {
                    strSql = strSql + " AND SubSection = @SubSection";
                }
                if (!String.IsNullOrWhiteSpace(objReq.OrderNo))
                {
                    strSql = strSql + " AND OrderNo = @OrderNo ";
                }
                strSql = strSql + " ORDER BY SubSection ASC ";

                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;
                if (!String.IsNullOrWhiteSpace(objReq.SubSection))
                {
                    cmd.Parameters.AddWithValue("@SubSection", objReq.SubSection);
                }
                if (!String.IsNullOrWhiteSpace(objReq.OrderNo))
                {
                    cmd.Parameters.AddWithValue("@OrderNo", objReq.OrderNo);
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsQASubSection();
                        obj.OrderNo = Convert.ToString(ds.Tables[0].Rows[i]["OrderNo"]);
                        obj.SubSection = Convert.ToString(ds.Tables[0].Rows[i]["SubSection"]);
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
                Logger.WriteLog("Function Name : Fn_Get_QA_Order_SubSection", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Get_QA_Order_SubSection");
            return objResp;
        }


        public clsQAOrder Fn_Insert_QA_Order_Defect(clsQAOrder objReq)
        {
            var objResp = new clsQAOrder();
            var obj = new clsQAOrderDefectList();
            var objList = new List<clsQAOrderDefectList>();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Insert_QA_Order_Defect");
            try
            {

                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                if (objReq.QAID == 0 || objReq.QAID == null)
                {
                  Int64 mxID =  _DALOrder.Fn_Get_MXID("QA_Order_CheckPoint", "QAID");
                    objReq.QAID = mxID;
                }

                if (String.IsNullOrWhiteSpace(objReq.OrderNo))
                {
                    objResp.vErrorCode = 400;
                    objResp.vErrorMsg = "OrderNo is empty";
                }
                if (String.IsNullOrWhiteSpace(objReq.SizeName))
                {
                    objResp.vErrorCode = 400;
                    objResp.vErrorMsg = "SizeName is empty";
                }
                if (String.IsNullOrWhiteSpace(objReq.SubSection))
                {
                    objResp.vErrorCode = 400;
                    objResp.vErrorMsg = "SubSection is empty";
                }
                if (String.IsNullOrWhiteSpace(objReq.QAStatus))
                {
                    objResp.vErrorCode = 400;
                    objResp.vErrorMsg = "QAStatus is empty";
                }
                if(objReq.Qty == 0)
                {
                    objResp.vErrorCode = 400;
                    objResp.vErrorMsg = "Qty is zero";
                }
                if (objReq.CreatedBy == 0)
                {
                    objResp.vErrorCode = 400;
                    objResp.vErrorMsg = "CreatedBy is empty";
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("USP_MobileQualityAnalysis", Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@QAID", objReq.QAID);
                    cmd.Parameters.AddWithValue("@OrderNo", objReq.OrderNo);
                    cmd.Parameters.AddWithValue("@SizeName", objReq.SizeName);
                    cmd.Parameters.AddWithValue("@SubSection", objReq.SubSection);
                    cmd.Parameters.AddWithValue("@Qty", objReq.Qty);
                    cmd.Parameters.AddWithValue("@QAStatus", objReq.QAStatus);
                    cmd.Parameters.AddWithValue("@PlyFrom", objReq.PlyFrom);
                    cmd.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                    cmd.Parameters.AddWithValue("@QueryType", "InsertQAOrder");
                    int i = 0;
                    i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        foreach(var oList in objReq._oList)
                        {
                          
                            #region upload Image

                            if (!String.IsNullOrWhiteSpace(oList.ImageName))
                            {
                                string fileName = objReq.SubSection + "-" + Convert.ToString(objReq.QAID + "-" + oList.DefectID);
                                string imgFile = oList.ImageName;

                                byte[] imageBytes = Convert.FromBase64String(imgFile.Split(',')[1]);

                                string filePath = HttpContext.Current.Server.MapPath(string.Format("~/Image/DefectImage/{0}.jpg", fileName));
                                System.IO.File.WriteAllBytes(filePath, imageBytes);
                                oList.ImageName = fileName + ".jpg";
                                
                            }
                            #endregion upload Image

                            SqlCommand cmd1 = new SqlCommand("USP_MobileQualityAnalysis", Con);
                            cmd1.CommandType = CommandType.StoredProcedure;

                            cmd1.Parameters.AddWithValue("@QAID", objReq.QAID);
                            cmd1.Parameters.AddWithValue("@DefectID", oList.DefectID);
                            cmd1.Parameters.AddWithValue("@ImageName", oList.ImageName);
                            cmd1.Parameters.AddWithValue("@CreatedBy", objReq.CreatedBy);
                            cmd1.Parameters.AddWithValue("@QueryType", "InsertQADefectList");
                            int j = 0;
                            j = cmd1.ExecuteNonQuery();
                            if (j > 0)
                            {
                                obj = new clsQAOrderDefectList();
                                obj.vErrorCode = 200;
                                obj.vErrorMsg = "Success";
                                objList.Add(obj);
                            }
                            else
                            {
                                obj.vErrorCode = 400;
                                obj.vErrorMsg = "Error in defect inerting";
                                objList.Add(obj);
                            }
                        }
                        objResp.QAID = objReq.QAID;
                        objResp.OrderNo = objReq.OrderNo;
                        objResp.SubSection = objReq.SubSection;
                        objResp.SizeName = objReq.SizeName;
                        objResp.Qty = objReq.Qty;
                        objResp.QAStatus = objReq.QAStatus;
                        objResp.vErrorCode = 200;
                        objResp.vErrorMsg = "Success";
                        objResp._oList = objList;
                    }
                    else
                    {
                        objResp.vErrorCode = 400;
                        objResp.vErrorMsg = "QA inserting failed";
                    }
                }
            }
            catch (Exception exp)
            {
                objResp.vErrorCode = 500;
                Logger.WriteLog("Function Name : Fn_Insert_QA_Order_Defect", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                objResp.vErrorMsg = exp.Message.ToString();
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Insert_QA_Order_Defect");
            return objResp;
        }

        #endregion End Fn_Get_QA_Order_Color 29-APR-2026

        #region Start Fn_Get_QA_Order_Defect 30-APR-2026

        public List<clsQAOrder> Fn_Get_QA_Order_Defect(clsQAOrder objReq)
        {
            var objResp = new List<clsQAOrder>();
            var obj = new clsQAOrder();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Get_QA_Order_Defect");
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "SELECT QAID, OrderNo, SizeName, SubSection, Qty, QAStatus, PlyFrom, CreatedBy,";
                strSql = strSql + " FORMAT(CreatedOn, 'dd-MMM-yyyy') AS CreatedOn FROM QA_Order_CheckPoint WHERE 1=1";
                if (!String.IsNullOrWhiteSpace(objReq.SubSection))
                {
                    strSql = strSql + " AND SubSection = @SubSection";
                }
                if (!String.IsNullOrWhiteSpace(objReq.SizeName))
                {
                    strSql = strSql + " AND SizeName = @SizeName";
                }
                if (!String.IsNullOrWhiteSpace(objReq.QAStatus))
                {
                    strSql = strSql + " AND QAStatus = @QAStatus";
                }
                if (!String.IsNullOrWhiteSpace(objReq.OrderNo))
                {
                    strSql = strSql + " AND OrderNo = @OrderNo ";
                }
                strSql = strSql + " ORDER BY QAID DESC ";

                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;
                if (!String.IsNullOrWhiteSpace(objReq.SubSection))
                {
                    cmd.Parameters.AddWithValue("@SubSection", objReq.SubSection);
                }
                if (!String.IsNullOrWhiteSpace(objReq.SizeName))
                {
                    cmd.Parameters.AddWithValue("@SizeName", objReq.SizeName);
                }
                if (!String.IsNullOrWhiteSpace(objReq.QAStatus))
                {
                    cmd.Parameters.AddWithValue("@QAStatus", objReq.QAStatus);
                }
                if (!String.IsNullOrWhiteSpace(objReq.OrderNo))
                {
                    cmd.Parameters.AddWithValue("@OrderNo", objReq.OrderNo);
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsQAOrder();
                        obj.QAID = Convert.ToInt64(ds.Tables[0].Rows[i]["QAID"]);
                        obj.OrderNo = Convert.ToString(ds.Tables[0].Rows[i]["OrderNo"]);
                        obj.SizeName = Convert.ToString(ds.Tables[0].Rows[i]["SizeName"]);
                        obj.SubSection = Convert.ToString(ds.Tables[0].Rows[i]["SubSection"]);
                        obj.Qty = Convert.ToInt64(ds.Tables[0].Rows[i]["Qty"]);
                        obj.QAStatus = Convert.ToString(ds.Tables[0].Rows[i]["QAStatus"]);
                        string PlyFrom = Convert.ToString(ds.Tables[0].Rows[i]["PlyFrom"]);
                        if (PlyFrom != "")
                        {
                            obj.PlyFrom = Convert.ToInt64(ds.Tables[0].Rows[i]["PlyFrom"]);
                        }
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
                Logger.WriteLog("Function Name : Fn_Get_QA_Order_Defect", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Get_QA_Order_Defect");
            return objResp;
        }

        #endregion End Fn_Get_QA_Order_Defect 30-APR-2026

        #region Start Fn_Get_QA_Order_DefectList 30-APR-2026

        public List<clsQAOrderDefectList> Fn_Get_QA_Order_DefectList(clsQAOrderDefectList objReq)
        {
            var objResp = new List<clsQAOrderDefectList>();
            var obj = new clsQAOrderDefectList();
            Logger.ErrorLog(JsonConvert.SerializeObject(objReq), "Request", "Fn_Get_QA_Order_DefectList");
            try
            {
                if (Con.State == ConnectionState.Broken)
                { Con.Close(); }
                if (Con.State == ConnectionState.Closed)
                { Con.Open(); }

                string strSql = "Select DL.QADetailID, DL.QAID, DL.DefectID, CM.Defects,";
                strSql = strSql + " DL.ImageName, DL.CreatedBy, FORMAT(DL.CreatedOn, 'dd-MMM-yyyy') AS CreatedOn from QA_Order_DefectList DL";
                strSql = strSql + " INNER JOIN QACheckPointMaster CM ON DL.DefectID = CM.DefectsID WHERE 1=1 ";
                
                if (objReq.QAID != 0)
                {
                    strSql = strSql + " AND DL.QAID = @QAID";
                }
                
                strSql = strSql + " ORDER BY DL.QAID, CreatedOn ";

                SqlCommand cmd = new SqlCommand(strSql, Con);
                cmd.CommandType = CommandType.Text;
                if (objReq.QAID != 0)
                {
                    cmd.Parameters.AddWithValue("@QAID", objReq.QAID);
                }
                
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int i = 0;
                string strImgPath = ConfigurationManager.AppSettings["UploadDefectImgPath"];
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        obj = new clsQAOrderDefectList();
                        obj.QADetailID = Convert.ToInt64(ds.Tables[0].Rows[i]["QADetailID"]);
                        obj.QAID = Convert.ToInt64(ds.Tables[0].Rows[i]["QAID"]);
                        obj.DefectID = Convert.ToInt64(ds.Tables[0].Rows[i]["DefectID"]);
                        obj.Defect = Convert.ToString(ds.Tables[0].Rows[i]["Defects"]);
                        string strImageName = Convert.ToString(ds.Tables[0].Rows[i]["ImageName"]);
                        if(strImageName != "")
                        {
                            obj.ImageName = strImgPath + Convert.ToString(ds.Tables[0].Rows[i]["ImageName"]);
                        }
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
                Logger.WriteLog("Function Name : Fn_Get_QA_Order_DefectList", " " + "Error Msg : " + exp.Message.ToString(), new StackTrace(exp, true));
                obj.vErrorMsg = exp.Message.ToString();
                objResp.Add(obj);
            }
            finally
            {
                Con.Close();
            }
            Logger.ErrorLog(JsonConvert.SerializeObject(objResp), "Response", "Fn_Get_QA_Order_DefectList");
            return objResp;
        }

        #endregion End Fn_Get_QA_Order_DefectList 30-APR-2026
    }
}