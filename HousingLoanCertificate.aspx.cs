//*-------------------------------ACCOUNT CHEQUE STOP PAYMENT------------------------------------
//*
//* Version     Modified By         Date           Remarks
//* 1.0         ROOPA            22-FEB-2016   Intial Draft-Assisted by LS

//*------------------------------------------------------------------------------
//*STARTED ON 20-FEB-2016
//*COMPLETED ON 22-FEB-2016
//* -----------------------------PSEUDOCODE--------------------------------------
//*  THIS REPORT IS USED TO SEE WHO HAS  PAID TOTAL AMOUNT AND INTEREST FOR THE HOUSING LOAN CUSTOMER HAS TAKEN FROM THE BANK.
//*
//****** PROCEDIRES Used ******//
//1.N_CLIENTNOANDNAMEADD2
//***************************************
//************TABLES USED**********************
//1.TRANSACTIONH
//2.TRANSACTIOND
//3.LOANSCHEMESETUP
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using System.Data;
using System.Data.OracleClient;
using System.Drawing;

public partial class Appl_HousingLoanCertificate : System.Web.UI.Page
{
    OracleConnection oraConn = new OracleConnection();
    DAL.conn objcon = new DAL.conn();
    Log objLog = new Log();
    global objGlb = new global();
    DAL.DataFetch objDataFetch = new DAL.DataFetch();
    DataTable dtTempFinYr = new DataTable();
    string sSQL = "", strToDt = "", strDate = "", strSysDate = "", strYrDate = "", strBnkName = "";
    int intDt = 0, intgSysDate = 0;
    DataTable dtLoanDisp = new DataTable();
    DataRow row;
    double dblCurFinYear = 0.00, dblPrincipal = 0.00, dblInterest = 0.00;
    DataTable dtcom = new DataTable();
    public override void VerifyRenderingInServerForm(Control control)
    {
        //return;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        objLog.WriteLog("HousingLoan.aspx: Entered Page_Load");

        if (Session.Contents.Count == 0)
        {
            Response.Redirect("ShowMessage.aspx?msg=" + GlobalConstants.msg);
        }
        else
        {
            try
            {
                if (!IsPostBack)
                {
                    OracleConnection oraConn = new OracleConnection(Session["constring"].ToString());
                    if (oraConn.State != ConnectionState.Open) oraConn.Open();


                    sSQL = "Select SITP_TEXTVALUE From Siteparameters Where SITP_NAME = 'COMPANY NAME'";
                    OracleDataAdapter odaC = new OracleDataAdapter(sSQL, oraConn);
                    odaC.Fill(dtcom);

                    if (dtcom.Rows.Count != 0)
                    {
                        strBnkName = dtcom.Rows[0]["SITP_TEXTVALUE"].ToString();
                    }
                    lblForm1.Text = strBnkName;
                    //lblPlc.Text = strBnkName.Substring(32, 10);
                    lblDate.Text = objGlb.GetServerDate();
                    lblSysDt.Text = objGlb.GetServerDate();
                    strSysDate = lblSysDt.Text;
                    if (Request.QueryString["name"] != null)
                    {
                        lblName.Text = Request.QueryString["name"].ToString();
                    }
                    if (Session["LoanName"] != null)
                    {
                        lblLoanName.Text = Session["LoanName"].ToString();
                    }
                    //if (Request.QueryString["Interest"] != null)
                    //{
                    //    dblInterest = Convert.ToDouble(Request.QueryString["Interest"]);
                    //}

                    if (Session["PlaceName"] != null)
                    {
                        lblPlc.Text = Session["PlaceName"].ToString();
                    }

                    if (Session["PlaceName"] != null)
                    {
                        lblPlc.Text = Session["PlaceName"].ToString();
                    }

                    if (Session["BRanchName"] != null)
                    {
                        lblBranch.Text= Session["BRanchName"].ToString();
                    }


                    sSQL = "Select SITP_LONGVALUE,SITP_TEXTVALUE,SITP_DOUBLEVALUE";
                    sSQL += " From SiteParameters";
                    sSQL += " Where SITP_NAME = 'FORM FIN YEAR'";
                    dtTempFinYr = objDataFetch.DatatablePassSQL(sSQL);
                    lblFrmDt.Text = "01/" + "0" + dtTempFinYr.Rows[0]["SITP_LONGVALUE"].ToString() + "/" + objGlb.GetFinancialYear(Convert.ToDateTime(lblSysDt.Text), oraConn);

                    strDate = strSysDate.Substring(3, 2);
                    intDt = Convert.ToInt16(strDate);

                    if (intDt < 4)
                    {
                        strYrDate = strSysDate.Substring(6, 4);
                        intgSysDate = Convert.ToInt16(strYrDate);
                        dblCurFinYear = intgSysDate;
                    }
                    else
                    {
                        strYrDate = strSysDate.Substring(6, 4);
                        intgSysDate = Convert.ToInt16(strYrDate);
                        dblCurFinYear = intgSysDate;
                    }
                    lblToDt.Text = string.Format("{0:dd/MMM/yyyy}", ("31/03/" + (dblCurFinYear)));

                    dtLoanDisp.Columns.Add(new DataColumn("PRINCIPAL", typeof(string)));
                    dtLoanDisp.Columns.Add(new DataColumn("INTEREST", typeof(string)));
                    dtLoanDisp.Columns.Add(new DataColumn("TOTAL", typeof(string)));

                    row = dtLoanDisp.NewRow();
                    if (Session["dblPrinciAmnt"] != null)
                    {
                        dblPrincipal = Convert.ToDouble(Session["dblPrinciAmnt"]);
                    }
                    if (Session["dblIntAmt"] != null)
                    {
                        dblInterest = Convert.ToDouble(Session["dblIntAmt"]);
                    }
                    row["PRINCIPAL"] = String.Format("{0:0.00}", dblPrincipal);
                    row["INTEREST"] = String.Format("{0:0.00}", dblInterest);
                    row["TOTAL"] = String.Format("{0:0.00}", dblPrincipal + dblInterest);
                    dtLoanDisp.Rows.Add(row);
                    gvLoanDetails.DataSource = dtLoanDisp;
                    gvLoanDetails.DataBind();
                }
            }
            catch (Exception ex)
            {
                objLog.WriteLog("HousingLoanCertificate Exception in PageLoad : " + ex.Message);
                ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert('" + ex.Message + "')</script>");
            }
            finally
            {
                if (oraConn.State == ConnectionState.Open)
                    oraConn.Close();
                oraConn.Dispose();
                Session["dblPrinciAmnt"] = null;
                Session["dblIntDue"] = null;
                OracleConnection.ClearAllPools();

            }
        }


        //objLog.WriteLog("rptDisp_LoanIntimationLetters: display function: ");
        //global.checkSession();
        //try
        //{
        //    if (!IsPostBack)
        //    {
        //        ViewState["Pagename"] = null;
        //        if (Session["HousingLoan"] != null)
        //        {
        //            objLog.WriteLog("HousingLoanCertificate :-Session HousingLoan");
        //            ViewState["Pagename"] = "HousingLoan";
        //            dtDisplay = (DataTable)Session["HousingLoan"];

        //        }
        //    }
        //    if (dtDisplay.Rows.Count > 0)
        //    {
        //        gvRptDisp.AllowPaging = true;
        //        gvRptDisp.PagerSettings.Mode = PagerButtons.NextPreviousFirstLast;
        //        gvRptDisp.PageSize = 40;
        //        gvRptDisp.PageIndexChanging += new GridViewPageEventHandler(gvRptDisp_PageIndexChanging);

        //        if (dtLoanDisp.Rows.Count > 0)
        //        {
        //            dtLoanDisp.Columns.Add(new DataColumn("PRINCIPAL", typeof(string)));
        //            dtLoanDisp.Columns.Add(new DataColumn("INTEREST", typeof(string)));
        //            dtLoanDisp.Columns.Add(new DataColumn("TOTAL", typeof(string)));

        //            row = dtLoanDisp.NewRow();
        //            if (Session["dblPrinciAmnt"] != null)
        //            {
        //                dblPrincipal = Convert.ToDouble(Session["dblPrinciAmnt"]);
        //            }
        //            if (Session["dblIntAmt"] != null)
        //            {
        //                dblInterest = Convert.ToDouble(Session["dblIntAmt"]);
        //            }
        //            row["PRINCIPAL"] = String.Format("{0:0.00}", dblPrincipal);
        //            row["INTEREST"] = String.Format("{0:0.00}", dblInterest);
        //            row["TOTAL"] = String.Format("{0:0.00}", dblPrincipal + dblInterest);
        //            dtLoanDisp.Rows.Add(row);
        //            gvRptDisp.DataSource = dtLoanDisp;
        //            gvRptDisp.DataBind();
        //            this.gvRptDisp.GridLines = GridLines.Both;
        //        }
        //        gvRptDisp.DataSource = dtDisplay;
        //        gvRptDisp.DataBind();

        //        Session["datatable"] = dtDisplay;
        //        Session["LoanIntimationLetters"] = null;

        //        objFrmPt.FormatGridView(gvRptDisp);
        //        GridPrint(gvRptDisp);
        //    }
        //    this.gvRptDisp.GridLines = GridLines.None;
        //}
        //catch (Exception ex)
        //{
        //    objLog.WriteLog("rptDisp_LoanIntimationLetters Exception in PageLoad : " + ex.Message);
        //    ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert('" + ex.Message + "')</script>");
        //}
        //finally
        //{
        //    dtDisplay.Dispose();
        //}

    }

    protected void gvLoanDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
        }
      
    }
}