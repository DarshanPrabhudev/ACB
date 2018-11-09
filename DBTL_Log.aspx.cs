//*-------------------------------ACCOUNT CHEQUE STOP PAYMENT------------------------------------
//*
//* Version     Modified By         Date           Remarks
//* 1.0         ROOPA       26-FEB-2016    Intial Draft-Assisted by Venkatesh 
//*------------------------------------------------------------------------------
//*STARTED ON 25-FEB-2016
//*COMPLETED ON 26-FEB-2016
//* -----------------------------PSEUDOCODE--------------------------------------
//*  THIS REPORT IS USED TO SEE THE DBTL TRANSACTIONS.
//*
//****** Tables Used ******//
//*1.DBTLLOG

using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using Bal;
using DAL;

public partial class DBTL_Log : System.Web.UI.Page
{

    global objGlobal = new global();
    Log objLog = new Log();
    string sSql = "", stFromDate = "", strToDate = "", StrHeading="";
    DateTime temp;
    DataTable dtDBTL = new DataTable();
    DAL.DataFetch objDataFetch = new DAL.DataFetch();
    DataRow drNew;
    


    protected void Page_Load(object sender, EventArgs e)
    {
        globalSession.checkSession();
        if (!IsPostBack)
        {
            try
            {
                if (Session["constring"] != null)
                {
                    OracleConnection oraConn = new OracleConnection(Session["constring"].ToString());
                    if (oraConn.State != ConnectionState.Open)
                        oraConn.Open();

                }


                txtFromDate.Text = objGlobal.GetServerDate();
                txtToDate.Text = objGlobal.GetServerDate();
                txtFromDate.Focus();
            }

            catch (Exception ex)
            {
                objLog.WriteLog("DBTL_Log :" + ex.Message);
            }
            finally
            {
                objLog.WriteLog("DBTL_Log.aspx--->Page_Load--->Page Load events Finally block Executed  ");
            }
        }
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {

        DataTable dtDTBLDisp = new DataTable();
        stFromDate = txtFromDate.Text.ToString();
        strToDate = txtToDate.Text.ToString();
        double dblAmount = 0.00;
        int Slno = 1;
        OracleConnection oraConn = new OracleConnection(Session["constring"].ToString());
        if (oraConn.State != ConnectionState.Open)
            oraConn.Open();

        try
        {

            if (!(DateTime.TryParse(stFromDate, out temp)))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert ('Enter a Valid From Date') </script>");
                txtFromDate.Focus();
                return;
            }

            if (!(DateTime.TryParse(strToDate, out temp)))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert ('Enter a Valid To Date') </script>");
                txtToDate.Focus();
                return;
            }

            if ((Convert.ToDateTime(stFromDate) > DateTime.Now) || (Convert.ToDateTime(strToDate) > DateTime.Now))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert ('Enter a Valid Date') </script>");
                return;
            }
            if (Convert.ToDateTime(stFromDate) > Convert.ToDateTime(strToDate))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert ('Check From Date') </script>");
                return;
            }

            StrHeading = "List of DBTL TRANSACTIONS FROM " + stFromDate + " To " + strToDate;
            Session["Heading"] = StrHeading;
            Session["FromDate"] = stFromDate;
            Session["ToDate"] = strToDate;


            sSql = "SELECT * FROM DBTLLOG ";
            sSql += " WHERE To_date(To_char(LOG_DATE,'dd-mon-yyyy')) >= To_date('" + stFromDate + "','dd/mm/yyyy')";
            sSql += " AND  To_date(To_char(LOG_DATE,'dd-mon-yyyy')) <= To_date('" + strToDate + "','dd/mm/yyyy')";
            if (rblSelection.SelectedValue.ToString().ToUpper().Trim() == "ACH".ToUpper().Trim())
            {
                sSql += " AND LOG_TYPE ='ACH'";
            }
            else if (rblSelection.SelectedValue.ToString().ToUpper().Trim() == "ACHDR".ToUpper().Trim())
            {
                sSql += " AND LOG_TYPE ='ACHDR'";
            }
            else if (rblSelection.SelectedValue.ToString().ToUpper().Trim() == "APB".ToUpper().Trim())
            {
                sSql += " AND LOG_TYPE ='APB'";
            }
            else if (rblSelection.SelectedValue.ToString().ToUpper().Trim() == "ECS-DR".ToUpper().Trim())
            {
                sSql += " AND LOG_TYPE ='ECS-DR'";
            }
            else if (rblSelection.SelectedValue.ToString().ToUpper().Trim() == "ECS-CR".ToUpper().Trim())
            {
                sSql += " AND LOG_TYPE ='ECS-CR'";
            }
            else if (rblSelection.SelectedValue.ToString().ToUpper().Trim() == "AV".ToUpper().Trim())
            {
                sSql += " AND LOG_TYPE ='AV'";
            }
            else if (rblSelection.SelectedValue.ToString().ToUpper().Trim() == "ALL".ToUpper().Trim())
            {
                sSql += " AND LOG_TYPE IN ('ACH','ACHDR','APB','ECS-DR','ECS-CR','AV') ";
            }
            sSql += " ORDER BY LOG_DATE DESC ";
            dtDBTL = objDataFetch.DatatablePassSQL(sSql);
            Session["DBTL"] = dtDBTL;

            if (dtDBTL.Rows.Count > 0)
            {
                dtDTBLDisp.Columns.Add("Sl_No", typeof(string));
                dtDTBLDisp.Columns.Add("File_Type", typeof(string));
                dtDTBLDisp.Columns.Add("A/c_No", typeof(string));
                dtDTBLDisp.Columns.Add("Name", typeof(string));
                dtDTBLDisp.Columns.Add("Amount", typeof(string));
                dtDTBLDisp.Columns.Add("Status", typeof(string));
                dtDTBLDisp.Columns.Add("Log_Date", typeof(string));
                dtDTBLDisp.Columns.Add("Tran_Date", typeof(string));
                dtDTBLDisp.Columns.Add("Req_File_Name", typeof(string));
                dtDTBLDisp.Columns.Add("Res_File_Name", typeof(string));
                dtDTBLDisp.Columns.Add("LOG_ACK", typeof(string));
                dtDTBLDisp.Columns.Add("LOG_ACKREASON", typeof(string));

                foreach (DataRow rowV in dtDBTL.Rows)
                {
                    drNew = dtDTBLDisp.NewRow();
                    drNew["Sl_No"] = Slno;
                    if (!DBNull.Value.Equals(rowV["LOG_TYPE"]))
                    {
                        drNew["File_Type"] = rowV["LOG_TYPE"].ToString();
                    }

                    if (!DBNull.Value.Equals(rowV["LOG_ACNO"]))
                    {
                        drNew["A/c_No"] = rowV["LOG_ACNO"].ToString();
                    }

                    if (!DBNull.Value.Equals(rowV["LOG_ACNAME"]))
                    {
                        drNew["Name"] = rowV["LOG_ACNAME"].ToString();
                    }

                    if (!DBNull.Value.Equals(rowV["LOG_AMOUNT"]))
                    {
                        drNew["Amount"] = String.Format("{0:0.00}", rowV["LOG_AMOUNT"]);
                        dblAmount = Convert.ToDouble(rowV["LOG_AMOUNT"]);
                    }

                    if (!DBNull.Value.Equals(rowV["LOG_STATUS"]))
                    {
                        drNew["Status"] = rowV["LOG_STATUS"].ToString();
                    }

                    if (!DBNull.Value.Equals(rowV["LOG_DATE"]))
                    {
                        drNew["Log_Date"] = String.Format("{0:dd/MM/yyyy}", rowV["LOG_DATE"]);
                    }

                    if (!DBNull.Value.Equals(rowV["LOG_TRANDATE"]))
                    {
                        drNew["Tran_Date"] = String.Format("{0:dd/MM/yyyy}", rowV["LOG_TRANDATE"]);
                    }

                    if (!DBNull.Value.Equals(rowV["LOG_REQFILENAME"]))
                    {
                        drNew["Req_File_Name"] = rowV["LOG_REQFILENAME"].ToString();
                    }

                    if (!DBNull.Value.Equals(rowV["LOG_RESFILENAME"]))
                    {
                        drNew["Res_File_Name"] = rowV["LOG_RESFILENAME"].ToString();
                    }
                    if (!DBNull.Value.Equals(rowV["LOG_ACK"]))
                    {
                        drNew["LOG_ACK"] = rowV["LOG_ACK"].ToString();
                    }
                    if (!DBNull.Value.Equals(rowV["LOG_ACKREASON"]))
                    {
                        drNew["LOG_ACKREASON"] = rowV["LOG_ACKREASON"].ToString();
                    }
                    dtDTBLDisp.Rows.Add(drNew);
                    Slno = Slno + 1;

                }
            }
            else if (dtDBTL.Rows.Count == 0)
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert('No Records Found')</script>");
                return;
            }


            DataTable dtNonTot = new DataTable();
            double dblNonCred = 0.00;
            sSql = " SELECT nvl(SUM(LOG_AMOUNT),0) AS AMOUNT FROM DBTLLOG ";
            sSql += " WHERE LOG_STATUS='Not Credited' ";
            sSql += " AND To_date(To_char(LOG_DATE,'dd-mon-yyyy')) >= To_date('" + stFromDate + "','dd/mm/yyyy')";
            sSql += " AND  To_date(To_char(LOG_DATE,'dd-mon-yyyy')) <= To_date('" + strToDate + "','dd/mm/yyyy')";
            dtNonTot = objDataFetch.DatatablePassSQL(sSql);
            //if (!DBNull.Value.Equals(dtNonTot.Rows))
            //{
            if (dtNonTot.Rows.Count >= 0)
            {
                //if (!DBNull.Value.Equals(dtNonTot.Rows[0]["AMOUNT"]))
                //{
                dblNonCred = Convert.ToDouble(dtNonTot.Rows[0]["AMOUNT"]);
                //}
            }
            //}
            else if (dtNonTot.Rows.Count == 0)
            {
                dblNonCred = Convert.ToDouble(dtNonTot.Rows[0]["AMOUNT"]);
            }
            Session["NonCredit"] = dblNonCred;

            DataTable dtCredTot = new DataTable();
            double dblCredit = 0.00;
            sSql = " SELECT nvl(SUM(LOG_AMOUNT),0) AS AMOUNT FROM DBTLLOG ";
            sSql += " WHERE LOG_STATUS='Credited' ";
            sSql += " AND To_date(To_char(LOG_DATE,'dd-mon-yyyy')) >= To_date('" + stFromDate + "','dd/mm/yyyy')";
            sSql += " AND  To_date(To_char(LOG_DATE,'dd-mon-yyyy')) <= To_date('" + strToDate + "','dd/mm/yyyy')";
            dtCredTot = objDataFetch.DatatablePassSQL(sSql);
            if (dtCredTot.Rows.Count >= 0)
            {
                //if (!DBNull.Value.Equals(dtCredTot.Rows[0]["AMOUNT"]))
                //{
                dblCredit = Convert.ToDouble(dtCredTot.Rows[0]["AMOUNT"]);
                //}
            }
            else if (dtCredTot.Rows.Count == 0)
            {
                dblCredit = Convert.ToDouble(dtCredTot.Rows[0]["AMOUNT"]);
            }
            Session["Credit"] = dblCredit;

            Session["Display"] = dtDTBLDisp;
            Response.Redirect("DBTL_LogDisp.aspx");
        }
        catch (Exception ex)
        {
            string g = ex.Message;
            Log objlog = new Log();
            objlog.WriteLog(ex.Source + " " + ex.Message + ex.InnerException);
            ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert('" + g + "')</script>");

        }
        finally
        {
            oraConn.Close();
            oraConn.Dispose();
            OracleConnection.ClearAllPools();
            dtDBTL.Dispose();
        }

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect(global.REPORT_MENU, false);
        //Response.Redirect("../../amenu.aspx");
    }
}