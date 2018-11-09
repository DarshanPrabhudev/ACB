/*----------------InterestDue Report----------------------------

 Version     Developed By         Date              Assigned By
 -------------------------------------------------------------------------------------
  1.0         swetha m           09.05.2015         Venkatesh
 
 * 
 * Stored Procedure Used - CBSILODNEWFORMAT.CBS_ILODREPORT
---------------------------------------------------------------------------------------*/
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.OracleClient;

public partial class LoanInterestDue : System.Web.UI.Page
{
    Log objLog = new Log();
    DAL.conn objcon = new DAL.conn();
    DAL.DataFetch objdal = new DAL.DataFetch();
    string strDBQuery = "WHERE", strFilter = "", MEMYN = "N";
    DataTable dtBranch = new DataTable();
    DataTable dtList = new DataTable();
    DataTable dtdetails = new DataTable();
    string strsql = "",loanful = "";
    OracleConnection oraConn = new OracleConnection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            OracleConnection con = new OracleConnection(Session["constring"].ToString());
            if (con.State != ConnectionState.Open) con.Open();

            strsql = "select bran_sno,bran_name from MASTER.branch";
            OracleDataAdapter oda = new OracleDataAdapter(strsql, con);
            oda.Fill(dtBranch);
            ddlBranch.DataSource = dtBranch;
            ddlBranch.DataTextField = "bran_name";
            ddlBranch.DataValueField = "bran_sno";
            ddlBranch.DataBind();
            ddlBranch.SelectedValue = (GlobalConstants.branchname.ToString());
            Bal.Gen objfilldate = new Bal.Gen();
            objfilldate.Call_Date(ddfrmday, ddfrmMonth, ddfrmYear);
            dtList = LoanReportsList(Convert.ToInt32(ddlBranch.SelectedValue));
            chkList.DataSource = dtList;
            chkList.DataTextField = "LSCH_DES";
            chkList.DataValueField = "LSCH_ABBREV";
            chkList.DataBind();
        }
    }
    public DataTable LoanReportsList(int brnch)
    {
        OracleConnection con = new OracleConnection(Session["constring"].ToString());
        strsql = "Select LSCH_SNO,LSCH_DES,LSCH_ABBREV from loanschemesetup Where LSCH_BRANCH=" + brnch + "";
        DataTable adorepts = new DataTable();
        OracleDataAdapter oda = new OracleDataAdapter(strsql, con);
        oda.Fill(adorepts);
        objLog.WriteLog(strsql);
        return adorepts;
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        string loanful = "",strdtfrm = "";
        int selectedCount = 0;
        try
        {
            if ((txtPrincipalDue.Text != "") && (Convert.ToInt32(txtPrincipalDue.Text) > 0))
                strDBQuery = strDBQuery + " ODAMT >=" + Convert.ToInt32(txtPrincipalDue.Text);
            if (txtInterestDue.Text != "" && Convert.ToInt32(txtInterestDue.Text) > 0)
            {
                if (strDBQuery == "WHERE")
                    strDBQuery = strDBQuery + " INTERESTDUE >=" + Convert.ToInt32(txtInterestDue.Text);
                else
                    strDBQuery = strDBQuery + " AND INTERESTDUE >=" + Convert.ToInt32(txtInterestDue.Text);
            }
            if (txtInstalmentDue.Text != "" && Convert.ToInt32(txtInstalmentDue.Text) > 0)
            {
                if (strFilter == "")
                    strFilter = strFilter + " INSTALLDUE >=" + Convert.ToInt32(txtInstalmentDue.Text);
                else
                    strFilter = strFilter + " AND INSTALLDUE >=" + Convert.ToInt32(txtInstalmentDue.Text);
            }
            if (txtInstalmentDueTo.Text != "" && Convert.ToInt32(txtInstalmentDueTo.Text) > 0)
            {
                if (strDBQuery == "WHERE")
                    strDBQuery = strDBQuery + " INSTALLDUE <=" + Convert.ToInt32(txtInstalmentDueTo.Text);
                else
                    strDBQuery = strDBQuery + " AND INSTALLDUE <=" + Convert.ToInt32(txtInstalmentDueTo.Text);
            }
            if (txtNetLoanDue.Text != "" && Convert.ToInt32(txtNetLoanDue.Text) > 0)
            {
                if (strDBQuery == "WHERE")
                    strDBQuery = strDBQuery + " NETDUE >=" + Convert.ToInt32(txtNetLoanDue.Text);
                else
                    strDBQuery = strDBQuery + " AND NETDUE >=" + Convert.ToInt32(txtNetLoanDue.Text);
            }
            if (rdbSecured.SelectedValue == "Secured")
            {
                if (strDBQuery == "WHERE")
                    strDBQuery = strDBQuery + " (SECURED = 'Y' OR SECURED IS NULL)";
                else
                    strDBQuery = strDBQuery + " AND (SECURED = 'Y' OR SECURED IS NULL)";
            }
            else if (rdbSecured.SelectedValue == "Un Secured")
            {
                if (strDBQuery == "WHERE")
                    strDBQuery = strDBQuery + " SECURED = 'N' ";
                else
                    strDBQuery = strDBQuery + " AND SECURED = 'N' ";
            }
            if (rdbMember.SelectedValue == "Regular")
            {
                MEMYN = "Y";
                if (strDBQuery == "WHERE")
                    strDBQuery = strDBQuery + " (MEMABBR = 'REG' OR MEMABBR is NULL)";
                else
                    strDBQuery = strDBQuery + " AND (MEMABBR = 'REG' OR MEMABBR IS NULL)";
            }
            else if (rdbMember.SelectedValue == "Nominal")
            {
                MEMYN = "Y";
                if (strDBQuery == "WHERE")
                    strDBQuery = strDBQuery + " (MEMABBR = 'NOM' OR MEMABBR IS NULL)";
                else
                    strDBQuery = strDBQuery + " AND (MEMABBR = 'NOM' OR MEMABBR IS NULL)";
            }
            else if (rdbMember.SelectedValue == "Associate")
            {
                MEMYN = "Y";
                if (strDBQuery == "WHERE")
                    strDBQuery = strDBQuery + " (MEMABBR = 'ASS' OR MEMABBR IS NULL) ";
                else
                    strDBQuery = strDBQuery + " AND(MEMABBR = 'ASS' OR MEMABBR IS NULL)";
            }

            string abbr = "", abbritm = "";
            for (int i = 0; i < chkList.Items.Count; i++)
            {
                if (chkList.Items[i].Selected)
                {
                    abbr += chkList.Items[i].Value.Trim().ToUpper() + ",";
                    abbritm += chkList.Items[i].Text.Trim() + ",";
                }
            }
            int lstcont = Convert.ToInt32(chkList.Items.Count);

            string branval = "";
            string branname = "";

            for (int i = 0; i < ddlBranch.Items.Count; i++)
            {
                branval += (ddlBranch.Items[i].Value + ",");
                branname += ddlBranch.Items[i].Text + ",";
            }

            selectedCount = chkList.Items.Cast<ListItem>().Count(li => li.Selected);           
            strdtfrm = ddfrmday.SelectedItem.Value + "-" + ddfrmMonth.SelectedItem.Text + "-" + ddfrmYear.SelectedItem.Value;
            
            if (abbr != "")
            {
                dtdetails = LoanInterestdueReport(Convert.ToInt32(ddlBranch.SelectedValue), abbr, selectedCount, MEMYN, strDBQuery, strdtfrm);

                if (selectedCount == lstcont)
                {
                    loanful = "ALL";
                }
                if (selectedCount != lstcont)
                {
                    loanful = "NO";
                }

                if (dtdetails.Rows.Count > 0)
                {
                    int value = 0;
                    if (txtPrincipalDue.Text == "")
                    {
                        txtPrincipalDue.Text = value.ToString();
                    }
                    if (txtInterestDue.Text == "")
                    {
                        txtInterestDue.Text = value.ToString();
                    }
                    if (txtInstalmentDue.Text == "")
                    {
                        txtInstalmentDue.Text = value.ToString();
                    }
                    if (txtNetLoanDue.Text == "")
                    {
                        txtNetLoanDue.Text = value.ToString();
                    }
                    Session["principaldue"] = txtPrincipalDue.Text;
                    Session["interestdue"] = txtInterestDue.Text;
                    Session["instalmentdue"] = txtInstalmentDue.Text;
                    Session["netdue"] = txtNetLoanDue.Text;


                    Session["branchcount"] = Convert.ToInt32(ddlBranch.Items.Count);
                    Session["branval"] = branval;
                    Session["branname"] = branname;

                    Session["asondate"] = strdtfrm;
                    Session["chklist"] = abbr;
                    Session["chklistcount"] = selectedCount;
                    Session["res"] = dtdetails;


                    string radiomember = "";
                    if (rdbMember.SelectedValue.ToString() == "Regular" || rdbMember.SelectedValue.ToString() == "Nominal" || rdbMember.SelectedValue.ToString() == "Associate")
                    {
                        radiomember = "Selected";
                    }
                    else
                    {
                        radiomember = "Notselected";
                    }
                    Session["rdbmember"] = radiomember;
                    global.Loginconn = Session["constring"].ToString();
                    GlobalConstants.pagename = HttpContext.Current.Request.Url.AbsoluteUri;
                    //Session["LoginBranchName"].ToString() ddlBranch.SelectedItem.Text;
                    Response.Redirect("LoanInterestdueDisplay.aspx?br=" + ddlBranch.SelectedValue, false);

                }
                else if (dtdetails.Rows.Count <= 0)
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('No Records for this Criteria')</script>");
                }
            }
            else
            {
                string path = "Please Select atleast one Scheme";
                ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('" + path + "')</script>");
            }
        }
        catch (Exception ex)
        {
            objLog.WriteLog(ex.Message);
            ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('" + ex.Message + "')</script>");
        }
    }
    protected void btnSelect_Click(object sender, EventArgs e)
    {
        foreach (ListItem li in chkList.Items)
        {
            li.Selected = true;
        }
    }
    public DataTable LoanInterestdueReport(int brnch, string abbr, int abbrno, string member, string qry, string date)
    {
        OracleConnection oraConn = new OracleConnection(Session["constring"].ToString());
        OracleCommand cmdProcExec = new OracleCommand();
        cmdProcExec.Connection = oraConn;
        oraConn.Open();

        Log objLog = new Log();
        objLog.WriteLog("CBSILODNEWFORMAT.CBS_ILODREPORT : abbr :" + abbr);
        objLog.WriteLog("CBSILODNEWFORMAT.CBS_ILODREPORT : abbrno :" + abbrno);
        objLog.WriteLog("CBSILODNEWFORMAT.CBS_ILODREPORT : date :" + String.Format("{0:dd-MMM-yyyy}", date));
        objLog.WriteLog("CBSILODNEWFORMAT.CBS_ILODREPORT : brnch:" + brnch);
        objLog.WriteLog("CBSILODNEWFORMAT.CBS_ILODREPORT : member:" + member);
        objLog.WriteLog("CBSILODNEWFORMAT.CBS_ILODREPORT : qry:" + qry);

        cmdProcExec.CommandType = System.Data.CommandType.StoredProcedure;
        cmdProcExec.CommandText = "CBSILODNEWFORMAT.CBS_ILODREPORT";
        cmdProcExec.Parameters.Add("P_STRABBR", OracleType.VarChar).Value = abbr;
        cmdProcExec.Parameters.Add("P_NO", OracleType.Number).Value = abbrno;
        cmdProcExec.Parameters.Add("P_ASONDATE", OracleType.DateTime).Value = String.Format("{0:dd-MMM-yyyy}", date);
        cmdProcExec.Parameters.Add("P_BNO", OracleType.Number).Value = brnch;
        cmdProcExec.Parameters.Add("MEM", OracleType.VarChar).Value = member;
        cmdProcExec.Parameters.Add("QRY", OracleType.VarChar).Value = "";
        cmdProcExec.Parameters.Add("REC", OracleType.Cursor).Direction = System.Data.ParameterDirection.Output;

        OracleDataAdapter dataAdapter = new OracleDataAdapter(cmdProcExec);
        DataTable ds2 = new DataTable();
        dataAdapter.Fill(ds2);
        objLog.WriteLog("CBSILODNEWFORMAT.CBS_ILODREPORT" + "" + "Executed Sucessfully");
        objLog.WriteLog("CBSILODNEWFORMAT.CBS_ILODREPORT : ds2.Rows.Count:" + ds2.Rows.Count);
        cmdProcExec.Dispose();
        oraConn.Close();
        oraConn.Dispose();
        return ds2;
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtInstalmentDue.Text = "";
        txtInstalmentDueTo.Text = "9999";
        txtInterestDue.Text = "";
        txtNetLoanDue.Text = "";
        txtPrincipalDue.Text = "";
        chkAddress.Checked = false;
        chkArbt.Checked = false;
        foreach (ListItem li in chkList.Items)
        {
            li.Selected = false;
        }
        
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {

        Response.Redirect(global.REPORT_MENU, false);
    }   
    protected void ddlBranch_SelectedIndexChanged1(object sender, EventArgs e)
    {
        //{
        //    if (!IsPostBack)
        //    {
        //        OracleConnection con = new OracleConnection(Session["constring"].ToString());
        //        if (con.State != ConnectionState.Open) con.Open();

        //        strsql = "select bran_sno,bran_name from MASTER.branch";
        //        OracleDataAdapter oda = new OracleDataAdapter(strsql, con);
        //        oda.Fill(dtBranch);
        //        ddlBranch.DataSource = dtBranch;
        //        ddlBranch.DataTextField = "bran_name";
        //        ddlBranch.DataValueField = "bran_sno";
        //        ddlBranch.DataBind();
        //        ddlBranch.SelectedValue = (GlobalConstants.branchname.ToString());
        //        Bal.Gen objfilldate = new Bal.Gen();
        //        objfilldate.Call_Date(ddfrmday, ddfrmMonth, ddfrmYear);
        //        dtList = LoanReportsList(Convert.ToInt32(ddlBranch.SelectedValue));
        //        chkList.DataSource = dtList;
        //        chkList.DataTextField = "LSCH_DES";
        //        chkList.DataValueField = "LSCH_ABBREV";
        //        chkList.DataBind();
        //    }
        //}
        {
            try
            {
                if (Session["constring"] != null)
                {
                    oraConn = new OracleConnection(Session["constring"].ToString());
                    if (oraConn.State != ConnectionState.Open)
                        oraConn.Open();
                }
                strsql = "select bran_sno,bran_name from MASTER.branch";
                OracleDataAdapter oda = new OracleDataAdapter(strsql, oraConn);
                oda.Fill(dtBranch);

                ddlBranch.DataSource = dtBranch;
                ddlBranch.DataTextField = "bran_name";
                ddlBranch.DataValueField = "bran_sno";
                ddlBranch.DataBind();
                ddlBranch.SelectedValue = (GlobalConstants.branchname.ToString());

                if (dtBranch.Rows.Count <= 0)
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert ('No Deposit Scheme Found...') </script>");
                    return;
                }
                dtList = LoanReportsList(Convert.ToInt32(ddlBranch.SelectedValue));
                chkList.DataSource = dtList;
                chkList.DataTextField = "LSCH_DES";
                chkList.DataValueField = "LSCH_ABBREV";
                chkList.DataBind();
            }
            catch (Exception ex)
            {
                objLog.WriteLog(ex.Message);
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
}

