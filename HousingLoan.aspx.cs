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
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using DAL;
using Bal;

public partial class Appl_HousingLoan : System.Web.UI.Page
{
    DAL.conn objcon = new DAL.conn();
    global objGlb = new global();
    DAL.DataFetch objDataFetch = new DAL.DataFetch();
    Log objLog = new Log();
    OracleConnection oraConn = new OracleConnection();
    Transaction objTrans = new Transaction();
    DataTable dtLoanDetail = new DataTable();
    string sSQL = "", strdtfrm = "", strdtTo = "", strBankName = "",strBranchName="";
    //string strToDt = "", strDate = "", strSysDate = "", strYrDate = "";
    //string FromDate = "", ToDate = "";
    string sSql = "", strLoanDes = "";
    FillScheme objFill = new FillScheme();
    //int intDt = 0, intgSysDate = 0;
    DataTable dtOthers = new DataTable();
    DataTable dtcom = new DataTable();
    double dblPrnAmt = 0.00,dblIntAmt=0.00;
    //DataRow row,row1;
    DataTable dtLoanDisp = new DataTable();
    //double dblCurFinYear = 0.00, dblPrincipal = 0.00, dblInterest = 0.00;
    DataTable dtTempFinYr = new DataTable();

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
                    sSQL = objFill.BranchFill();
                    objDataFetch.FillDdl(sSQL, ddlBranch, "BRAN_SNO", "BRAN_NAME");
                    ddlBranch.SelectedValue = Session["BranchLogin"].ToString();
                    ddlBranch.Enabled = false;
                    txtAbbr.Focus();
                    Bal.Gen objfilldate = new Bal.Gen();
                    objfilldate.Call_Date(ddfrmday, ddfrmMonth, ddfrmYear);
                    Bal.Gen objfilldate1 = new Bal.Gen();
                    objfilldate1.Call_Date(ddtoday, ddtoMonth, ddtoYear);
                }
                strdtfrm = ddfrmday.SelectedItem.Value + "-" + ddfrmMonth.SelectedItem.Text + "-" + ddfrmYear.SelectedItem.Value;
                strdtTo = ddtoday.SelectedItem.Value + "-" + ddtoMonth.SelectedItem.Text + "-" + ddtoYear.SelectedItem.Value;
                objLog.WriteLog("HousingLoan.aspx: strdtfrm=" + strdtfrm);
                objLog.WriteLog("HousingLoan.aspx: strdtTo=" + strdtTo);
              
            }
            catch (Exception ex)
            {
                objLog.WriteLog("HousingLoan Exception in PageLoad : " + ex.Message);
                ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert('" + ex.Message + "')</script>");
            }
            finally
            {
                if (oraConn.State == ConnectionState.Open)
                    oraConn.Close();
                oraConn.Dispose();
                OracleConnection.ClearAllPools();
               
            }
        }
    }
   
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("HousingLoan.aspx");
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect(Gen.REPORT_MENU, false);
    }
    protected void txtAcno_TextChanged(object sender, EventArgs e)
    {
        OracleConnection oraConn = new OracleConnection(Session["constring"].ToString());
        if (oraConn.State != ConnectionState.Open) oraConn.Open();
        try
        {
            string sCondition = txtAcno.Text.Trim();
         
            double AccountNmbr;
            bool isNum = double.TryParse(sCondition, out AccountNmbr);
            if (isNum)
            {
                txtAbbr.Text = txtAbbr.Text.ToUpper().Trim();
                GetClientNoAndName((txtAbbr.Text).ToString(), Convert.ToDouble(txtAcno.Text), Convert.ToDouble(ddlBranch.SelectedValue), true);
                txtClientCode.Focus();
            }

          
        }
        catch (Exception ex)
        {
            objLog.WriteLog(ex.Message);
            ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('" + ex.Message + "')</script>");
        }
        finally
        {
            if (oraConn.State == ConnectionState.Open)
                oraConn.Close();
            oraConn.Dispose();
            OracleConnection.ClearAllPools();

        }
    }
    public string GetClientNoAndName(string abbr, double clientno, double ClientBranch, bool bnomsg)
    {
        OracleConnection oraConn = new OracleConnection(Session["constring"].ToString());
        string ClientNo = "";
        try
        {
            DAL.conn objcn = new DAL.conn();
            //string cnstr = objcn.GetConnectionString();
            DataTable dt = new DataTable();

            objLog.WriteLog("HousingLoan.aspx: calling proc:'N_CLIENTNOANDNAMEADD2'");

            if (oraConn.State != ConnectionState.Open) oraConn.Open();
            OracleCommand cmd = new OracleCommand("N_CLIENTNOANDNAMEADD2", oraConn);
            OracleDataAdapter da = new OracleDataAdapter();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("P_ABBR", OracleType.VarChar).Value = abbr.ToUpper().Trim();
            cmd.Parameters.Add("P_ACCNO", OracleType.Number).Value = clientno;
            cmd.Parameters.Add("P_BRANCH", OracleType.Number).Value = ClientBranch;
            cmd.Parameters.Add("P_OUT", OracleType.Cursor).Direction = System.Data.ParameterDirection.Output;
            OracleDataAdapter dataAdapter = new OracleDataAdapter(cmd);
            dataAdapter.Fill(dt);

            objLog.WriteLog("HousingLoan.aspx: after calling proc:'N_CLIENTNOANDNAMEADD2'");

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["ACCHOLDERNAME"] != DBNull.Value && dt.Rows[0]["CSNO"] != DBNull.Value)
                {
                    txtName.Text = dt.Rows[0]["ACCHOLDERNAME"].ToString();
                    txtClientCode.Text = dt.Rows[0]["CSNO"].ToString();
                }
                else
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('A/C does not Exist')</script>");
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('A/C does not Exist')</script>");
            }
            return ClientNo;
        }
        catch (Exception ex)
        {
            objLog.WriteLog(ex.Message);
            ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('" + ex.Message + "')</script>");
        }
        finally
        {
            if (oraConn.State == ConnectionState.Open)
                oraConn.Close();
            oraConn.Dispose();
            OracleConnection.ClearAllPools();
        }
        return ClientNo;
    }
    protected void btnCertificate_Click(object sender, EventArgs e)
    {
        DateTime temp;
        DataTable dtPrinAmt = new DataTable();
        DataTable dtIntAmt = new DataTable();
        if (Session["constring"] != null)
        {
            oraConn = new OracleConnection(Session["constring"].ToString());
            if (oraConn.State != ConnectionState.Open)
                oraConn.Open();
        }
        if (txtAbbr.Text != "" && txtAcno.Text != "")
        {
           
            DataTable dt1 = new DataTable();

            if ((!(DateTime.TryParse(strdtfrm, out temp))) || (!(DateTime.TryParse(strdtTo, out temp))))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert ('Enter the Valid Date') </script>");
                return;
            }
            if ((Convert.ToDateTime(strdtfrm) > DateTime.Now) || (Convert.ToDateTime(strdtTo) > DateTime.Now))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert ('Enter a Valid Date') </script>");
                return;
            }
            if (Convert.ToDateTime(strdtfrm) > Convert.ToDateTime(strdtTo))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert ('Check From Date') </script>");
                return;
            }
        }
        if ((txtAbbr.Text == "") && (txtAcno.Text == ""))
        {
            ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert ('Enter Abbr/Account Number and Proceed') </script>");
            return;
        }
        sSQL = "  SELECT SUM(TRND_AMOUNT) AS PRINCIPAL ";
        sSQL += " FROM TRANSACTIONH,TRANSACTIOND,LOANSCHEMESETUP ";
        sSQL += " WHERE TRNH_VOUCHERNO=TRND_VOUCHERLINK";
        sSQL += " AND TRND_PLABBR = '" + txtAbbr.Text.ToUpper().Trim() + "'";
        sSQL += " AND TRND_PLACCOUNT = " + txtAcno.Text;
        sSQL += " AND TRND_DEBITORCREDIT='C'";
        sSQL += " AND TRNH_VOUCHERDATE >= ' " + strdtfrm + " '";
        sSQL += " AND TRNH_VOUCHERDATE <= ' " + strdtTo + " '";
        sSQL += " AND LSCH_ABBREV=TRND_PLABBR";
        sSQL += " AND TRND_GLLINK=LSCH_GLLINK";
        sSQL += " AND TRNH_BRANCH=" + ddlBranch.SelectedValue;
        sSQL += " AND LSCH_BRANCH=TRNH_BRANCH";
        sSQL += " AND TRNH_STATUS='R'";

        dtPrinAmt = objDataFetch.DatatablePassSQL(sSQL);
        if (dtPrinAmt.Rows[0]["PRINCIPAL"].ToString() != "")
        {
            dblPrnAmt = Convert.ToDouble(dtPrinAmt.Rows[0]["PRINCIPAL"].ToString());
        }
        Session["dblPrinciAmnt"] = dblPrnAmt;


        sSQL = "   SELECT SUM(TRND_AMOUNT) AS INTEREST ";
        sSQL += " FROM TRANSACTIONH,TRANSACTIOND,LOANSCHEMESETUP ";
        sSQL += " WHERE TRNH_VOUCHERNO=TRND_VOUCHERLINK ";
        sSQL += " AND TRND_PLABBR =  '" + txtAbbr.Text.ToUpper().Trim() + "'";
        sSQL += " AND TRND_PLACCOUNT =  " + txtAcno.Text;
        sSQL += " AND TRND_DEBITORCREDIT='C' ";
        sSQL += " AND TRNH_VOUCHERDATE >= ' " + strdtfrm + " '";
        sSQL += " AND TRNH_VOUCHERDATE <= ' " + strdtTo + " '";
        sSQL += " AND LSCH_ABBREV=TRND_PLABBR ";
        sSQL += " AND TRND_GLLINK=LSCH_INTGLLINK ";
        sSQL += " AND TRNH_BRANCH=" + ddlBranch.SelectedValue;
        sSQL += " AND LSCH_BRANCH=TRNH_BRANCH ";
        sSQL += " AND TRNH_STATUS='R' ";

        dtIntAmt = objDataFetch.DatatablePassSQL(sSQL);
        if (dtIntAmt.Rows[0]["INTEREST"].ToString() != "")
        {
            dblIntAmt = Convert.ToDouble(dtIntAmt.Rows[0]["INTEREST"].ToString());
        }
        Session["dblIntAmt"] = dblIntAmt;

        string strPlace = txtPlaceName.Text;
        Session["PlaceName"] = strPlace;

        strBranchName = ddlBranch.SelectedItem.ToString();
        Session["BRanchName"] = strBranchName;



        //added to grid for certificate display
        //sSql = "Select SITP_TEXTVALUE From Siteparameters Where SITP_NAME = 'COMPANY NAME'";
        //OracleDataAdapter odaC = new OracleDataAdapter(sSql, oraConn);
        //odaC.Fill(dtcom);

        //if (dtcom.Rows.Count != 0)
        //{
        //    strBankName = dtcom.Rows[0]["SITP_TEXTVALUE"].ToString();

        //}
        //strAsonDate = objGlb.GetServerDate();
        //strSysDate = strAsonDate;

        //sSQL = "Select SITP_LONGVALUE,SITP_TEXTVALUE,SITP_DOUBLEVALUE";
        //sSQL += " From SiteParameters";
        //sSQL += " Where SITP_NAME = 'FORM FIN YEAR'";
        //dtTempFinYr = objDataFetch.DatatablePassSQL(sSQL);
        //FromDate = "01/" + "0" + dtTempFinYr.Rows[0]["SITP_LONGVALUE"].ToString() + "/" + objGlb.GetFinancialYear(Convert.ToDateTime(strAsonDate), oraConn);

        //strDate = strSysDate.Substring(3, 2);
        //intDt = Convert.ToInt16(strDate);

        //if (intDt < 4)
        //{
        //    strYrDate = strSysDate.Substring(6, 4);
        //    intgSysDate = Convert.ToInt16(strYrDate);
        //    dblCurFinYear = intgSysDate;
        //}
        //else
        //{
        //    strYrDate = strSysDate.Substring(6, 4);
        //    intgSysDate = Convert.ToInt16(strYrDate);
        //    dblCurFinYear = intgSysDate;
        //}
        //ToDate = string.Format("{0:dd/MMM/yyyy}", ("31/03/" + (dblCurFinYear)));

       




        //DataTable dtDetails = new DataTable();
        //dtDetails.Columns.Add(new DataColumn(" ", typeof(string)));
        //dtDetails.Columns.Add(new DataColumn("  ", typeof(string)));


        //row = dtDetails.NewRow();
        //row[" "] = strBankName;
        //dtDetails.Rows.Add(row);

        //row = dtDetails.NewRow();
        //row[" "] = ddlBranch.SelectedItem;
        //dtDetails.Rows.Add(row);


        //row = dtDetails.NewRow();
        //dtDetails.Rows.Add(row);


        //row = dtDetails.NewRow();
        //row[" "] = "Ref:";
        //row["  "] = "Date:" + strAsonDate + "";
        //dtDetails.Rows.Add(row);

        //row = dtDetails.NewRow();
        //dtDetails.Rows.Add(row);

        //row = dtDetails.NewRow();
        //row[" "] = "C E R T I F I C A T E";
        //dtDetails.Rows.Add(row);

        //row = dtDetails.NewRow();
        //dtDetails.Rows.Add(row);


        //row = dtDetails.NewRow();
        //row[" "] = "This is to Certify that Shri. " + txtName.Text + " customer  ";
        //dtDetails.Rows.Add(row);


        //string LnName = "";
        //LnName = Session["LoanName"].ToString();

        //  //Session["LoanName"] = strLoanDes;
        //row = dtDetails.NewRow();
        //row[" "] = "has availed " + LnName + " from our Bank. He/She has paid towards";
        //dtDetails.Rows.Add(row);

        //row = dtDetails.NewRow();
        //row[" "] = " Prinicipal and Interest during the period from " + FromDate + " to "+ToDate+" is as following.";
        //dtDetails.Rows.Add(row);

        //row = dtDetails.NewRow();
        //dtDetails.Rows.Add(row);


        //dtLoanDisp.Columns.Add(new DataColumn("PRINCIPAL", typeof(string)));
        //dtLoanDisp.Columns.Add(new DataColumn("INTEREST", typeof(string)));
        //dtLoanDisp.Columns.Add(new DataColumn("TOTAL", typeof(string)));

        //row1 = dtLoanDisp.NewRow();
        //if (Session["dblPrinciAmnt"] != null)
        //{
        //    dblPrincipal = Convert.ToDouble(Session["dblPrinciAmnt"]);
        //}
        //if (Session["dblIntAmt"] != null)
        //{
        //    dblInterest = Convert.ToDouble(Session["dblIntAmt"]);
        //}
        //row1["PRINCIPAL"] = String.Format("{0:0.00}", dblPrincipal);
        //row1["INTEREST"] = String.Format("{0:0.00}", dblInterest);
        //row1["TOTAL"] = String.Format("{0:0.00}", dblPrincipal + dblInterest);
        //dtLoanDisp.Rows.Add(row1);
        


       // row = dtDetails.NewRow();
       // dtDetails.Rows.Add(row);


       // row = dtDetails.NewRow();
       // row[" "] = "PLACE:  " + txtPlaceName .Text+ "";
       //// row[" "] = "'" + txtPlaceName + "'";
       // dtDetails.Rows.Add(row);

       // row = dtDetails.NewRow();
       // row[" "] = "DATE:" + strAsonDate + " ";
       // row["  "] = "Asst.Manager ";
       // dtDetails.Rows.Add(row);



        //row = dtDetails.NewRow();
        //row[" "] = "Asst.Manager";
        //dtDetails.Rows.Add(row);

        //Session["HousingLoan"] = dtDetails;
        //Response.Redirect("HousingLoanCertificate.aspx");

        objLog.WriteLog("HousingLoan.aspx:redirecting to HousingLoanCertificate");
        Response.Redirect("HousingLoanCertificate.aspx?name=" + txtName.Text.ToString() + "&tod=" + strdtTo  );

    }
    protected void txtAbbr_TextChanged(object sender, EventArgs e)
    {
        OracleConnection oraConn = new OracleConnection(Session["constring"].ToString());
        if (oraConn.State != ConnectionState.Open) oraConn.Open();
        try
        {
            DataTable dtLoanAbbr = new DataTable();
            sSql = " SELECT LSCH_DES,LSCH_ABBREV ";
            sSql += " FROM LOANSCHEMESETUP";
            sSql += "  WHERE LSCH_BRANCH =  " + ddlBranch.SelectedValue;
            sSql += " AND LSCH_ABBREV = '" + txtAbbr.Text.Trim().ToUpper() + "'";
            dtLoanAbbr = objDataFetch.DatatablePassSQL(sSql);

            if (dtLoanAbbr.Rows.Count <= 0)
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Invalid Loan Abbr')</script>");
                txtAbbr.Text = "";
                txtAbbr.Focus();
                return;
            }
            //else
            //{
            //    if (!DBNull.Value.Equals(dtLoanAbbr.Rows[0]["LSCH_ABBREV"]) && (dtLoanAbbr.Rows[0]["LSCH_ABBREV"].ToString() != string.Empty))
            //    {

            //    }
            //}
            if (dtLoanAbbr.Rows[0]["LSCH_DES"].ToString() != "")
            {
                strLoanDes = dtLoanAbbr.Rows[0]["LSCH_DES"].ToString();
            }
            Session["LoanName"] = strLoanDes;
           
            txtAcno.Focus();
        }
        catch (Exception ex)
        {
            objLog.WriteLog(ex.Message);
            ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('" + ex.Message + "')</script>");
        }
        finally
        {
            if (oraConn.State == ConnectionState.Open)
                oraConn.Close();
            oraConn.Dispose();
            OracleConnection.ClearAllPools();
        }
    }
}