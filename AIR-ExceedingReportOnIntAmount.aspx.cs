//**---------------------------AIR EXCEEDING REPORT ON INTEREST AMOUNT-----------
/*
 * Version     Created By        Date           Assigned By
 * 1.0         SHEETHAL       28/12/2015          Ayaz 
 * ------------------------------------------------------------------------------
 * -----------------------------PSEUDOCODE---------------------------------------
 * */

using System;
using DAL;
using System.Data.OracleClient;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_AIR_ExceedingReportOnIntAmount : System.Web.UI.Page
{
    DAL.DataFetch objDataFetch = new DataFetch();
    FillScheme objFillScheme = new FillScheme();
    OracleConnection oraConn = new OracleConnection();
    DataTable dtListDeposit = new DataTable();
    DataTable dtAllDetails = new DataTable();
    global objGlb = new global();
    Log objLog = new Log();
    string Ssql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            globalSession.checkSession();
            if (!Page.IsPostBack)
            {
                txtFrmDt.Text = "01/04/2014";
                if (Session["constring"] == null)
                {
                    Response.Redirect("ShowMessage.aspx?msg=" + GlobalConstants.msg);
                }
                if (Session["constring"] != null)
                {
                    oraConn = new OracleConnection(Session["constring"].ToString());
                    if (oraConn.State != ConnectionState.Open) oraConn.Open();
                }
                Ssql = objFillScheme.BranchFill();
                objDataFetch.FillDdl(Ssql, ddlBranch, "BRAN_SNO", "BRAN_NAME");
                if (Session["BranchLogin"] != null)
                {
                    ddlBranch.SelectedValue = Session["BranchLogin"].ToString();
                }
                Ssql = objFillScheme.DepositFill(ddlBranch.SelectedValue);
                OracleDataAdapter oDaDeposit = new OracleDataAdapter(Ssql, oraConn);
                oDaDeposit.Fill(dtListDeposit);
                if (dtListDeposit.Rows.Count <= 0)
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert ('No Deposit Scheme Found...') </script>");
                    return;
                }
                chkListDeposit.DataSource = dtListDeposit;
                chkListDeposit.DataTextField = "DSCH_NAME";
                chkListDeposit.DataValueField = "DSCH_SNO";
                chkListDeposit.DataBind();
                txtToDt.Text = objGlb.GetServerDate();
                Visible();

            }
        }
        catch (Exception ex)
        {
            objLog.WriteLog("AIR-ExceedingReportOnIntAmount: Error in Page Loading " + ex.Message);
            ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert('Error in Page Loading: " + ex.Message + "')</script>");
        }
        objLog.WriteLog("ExceedingReportOnIntAmount:Page Loaded Successfully");
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        try
        {
            string sDeposit = "", dtToDate = "", dtFromDt = "", strDtFrmt = "";
            DateTime temp;
            Session["strBranch"] = ddlBranch.SelectedItem.Text;
            dtToDate = txtToDt.Text.ToString();
            dtFromDt = txtFrmDt.Text.ToString();
            dtFromDt = dtFromDt.Substring(6, 4);
            dtToDate = dtToDate.Substring(6, 4);
            strDtFrmt = dtFromDt + "-" + dtToDate;
            if (Session["constring"] != null)
            {
                oraConn = new OracleConnection(Session["constring"].ToString());
            }
            if (oraConn.State != ConnectionState.Open)
            {
                oraConn.Open();
            }
            if ((!(DateTime.TryParse(txtFrmDt.Text, out temp))) || (!(DateTime.TryParse(txtToDt.Text, out temp))))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert ('Enter a Valid Date') </script>");
                return;
            }
            if ((Convert.ToDateTime(txtFrmDt.Text) > DateTime.Now) || (Convert.ToDateTime(txtToDt.Text) > DateTime.Now))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert ('Enter a Valid Date') </script>");
                return;
            }
            if (Convert.ToDateTime(txtFrmDt.Text) > Convert.ToDateTime(txtToDt.Text))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert ('Check From and To Dates') </script>");
                return;
            }
            if ((txtAmnt.Text == "") || (txtAmnt.Text == "0"))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert ('Enter the Amount Range...!!') </script>");
                return;
            }
            for (int i = 0; i < chkListDeposit.Items.Count; i++)
            {
                if (chkListDeposit.Items[i].Selected)
                {
                    sDeposit += chkListDeposit.Items[i].Value.Trim() + ",";
                }
            }
            if (rblSum.Checked==true)
            {
                OracleCommand cmdProcExe = new OracleCommand("DEPOSIT_TDSNEW", oraConn);
                cmdProcExe.CommandType = System.Data.CommandType.StoredProcedure;
                if (rblBranch.Items[0].Selected == true)
                {
                    cmdProcExe.Parameters.Add("I_BRANCH", OracleType.Number).Value = DBNull.Value;
                }
                else
                {
                    cmdProcExe.Parameters.Add("I_BRANCH", OracleType.Number).Value = ddlBranch.SelectedValue;
                }
                cmdProcExe.Parameters.Add("I_SCHEME", OracleType.VarChar).Value = "0,";
                cmdProcExe.Parameters.Add("I_DATE1", OracleType.DateTime).Value = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(txtFrmDt.Text));
                cmdProcExe.Parameters.Add("I_DATE2", OracleType.DateTime).Value = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(txtToDt.Text));
                if (rblList.Items[0].Selected == true)
                {
                    cmdProcExe.Parameters.Add("I_MEMYN", OracleType.VarChar).Value = "Y";
                }
                else
                {
                    cmdProcExe.Parameters.Add("I_MEMYN", OracleType.VarChar).Value = DBNull.Value;
                }
                cmdProcExe.Parameters.Add("I_AMT", OracleType.Number).Value = txtAmnt.Text;
                cmdProcExe.Parameters.Add("FORMTYPE", OracleType.VarChar).Value = "ALL";
                cmdProcExe.Parameters.Add("I_OUT", OracleType.Cursor).Direction = System.Data.ParameterDirection.Output;
                OracleDataAdapter oDaReport = new OracleDataAdapter(cmdProcExe);
                oDaReport.Fill(dtAllDetails);
            }
            else if (rblDetails.Checked==true)
            {
                OracleCommand cmdProcExe = new OracleCommand("DEPOSIT_TDSDET", oraConn);
                cmdProcExe.CommandType = System.Data.CommandType.StoredProcedure;
                if (rblBranch.Items[0].Selected == true)
                {
                    cmdProcExe.Parameters.Add("I_BRANCH", OracleType.Number).Value = DBNull.Value;
                }
                else
                {
                    cmdProcExe.Parameters.Add("I_BRANCH", OracleType.Number).Value = ddlBranch.SelectedValue;
                }
                objLog.WriteLog("Reports_AIR_ExceedingReportOnIntAmount sDeposit " + sDeposit);
                cmdProcExe.Parameters.Add("I_SCHEME", OracleType.VarChar).Value = sDeposit;
                cmdProcExe.Parameters.Add("I_DATE1", OracleType.DateTime).Value = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(txtFrmDt.Text));
                cmdProcExe.Parameters.Add("I_DATE2", OracleType.DateTime).Value = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(txtToDt.Text));
                if (rblList.Items[0].Selected == true)
                {
                    cmdProcExe.Parameters.Add("I_MEMYN", OracleType.VarChar).Value = "Y".Trim();
                }
                else
                {
                    cmdProcExe.Parameters.Add("I_MEMYN", OracleType.VarChar).Value = DBNull.Value;
                }
                cmdProcExe.Parameters.Add("I_AMT", OracleType.Number).Value = txtAmnt.Text;
                cmdProcExe.Parameters.Add("FORMTYPE", OracleType.VarChar).Value = "ALL";
                cmdProcExe.Parameters.Add("I_OUT", OracleType.Cursor).Direction = System.Data.ParameterDirection.Output;
                OracleDataAdapter oDaReport = new OracleDataAdapter(cmdProcExe);
                oDaReport.Fill(dtAllDetails);
                objLog.WriteLog("Reports_AIR_ExceedingReportOnIntAmount dtAllDetails Row Count" + dtAllDetails.Rows.Count.ToString());
            }
            dtAllDetails.DefaultView.Sort = "O_BRANCH";
            dtAllDetails = dtAllDetails.DefaultView.ToTable();

            Session["dtAllDetails"] = dtAllDetails;
            if (dtAllDetails.Rows.Count > 0)
            {
                Response.Redirect("rptAirIntAmntDisp.aspx?ch=" + ddlBranch.SelectedValue +  "&dt=" + strDtFrmt);
            }
            else
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert ('No Records Found...!!') </script>");
                return;
            }
        }
        catch (Exception ex)
        {
            objLog.WriteLog("AIR-ExceedingReportOnIntAmount: Error in btnOk_Click " + ex.Message);
            ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert('" + ex.Message + "')</script>");
        }
    }
    //protected void rblSumDep_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (rblSumDep.Items[0].Selected == true)
    //    {
    //        pnlSchemeList.Visible = false;
    //    }
    //    else
    //    {
    //        pnlSchemeList.Visible = true;
    //    }

    //}
    //protected void rblBranch_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (rblBranch.Items[0].Selected == true)
    //    {
    //        ddlBranch.Enabled = false;
    //        rblSumDep.Items[1].Enabled = false;
    //        rblSumDep.Items[0].Enabled = true;
    //    }
    //    else
    //    {
    //        rblSumDep.Items[1].Enabled = true;
    //        ddlBranch.Enabled = true;
    //        rblSumDep.Items[0].Enabled = false;
         
    //    }
    //}
    protected void btnBck_Click(object sender, EventArgs e)
    {
        Response.Redirect(Gen.REPORT_MENU, false);
    }
    //protected void btnBck0_Click(object sender, EventArgs e)
    //{

    //}
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (Session["constring"] != null)
            {
                oraConn = new OracleConnection(Session["constring"].ToString());
            }
            if (oraConn.State != ConnectionState.Open)
            {
                oraConn.Open();
            }
            Ssql = objFillScheme.DepositFill(ddlBranch.SelectedValue);
            OracleDataAdapter oDaDeposit = new OracleDataAdapter(Ssql, oraConn);
            dtListDeposit.Clear();
            oDaDeposit.Fill(dtListDeposit);
           
            if (dtListDeposit.Rows.Count <= 0)
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert ('No Deposit Scheme Found...') </script>");
                return;
            }
          
            chkListDeposit.DataSource = dtListDeposit;
            chkListDeposit.DataTextField = "DSCH_NAME";
            chkListDeposit.DataValueField = "DSCH_SNO";
            chkListDeposit.DataBind();
        }
        catch (Exception ex)
        {
            objLog.WriteLog("AIR-ExceedingReportOnIntAmount: Error in ddlBranch_SelectedIndexChanged " + ex.Message);
            ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert('" + ex.Message + "')</script>");
        }
    }
    protected void rblBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        Visible();

    }
    protected void Visible()
    {
        if (rblBranch.Items[0].Selected == true)
        {
            pnlSchemeList.Visible = false;
            rblDetails.Visible = false;
            rblSum.Visible = true;
        }
        else
        {
            pnlSchemeList.Visible = true;
            rblDetails.Visible = true;
            rblSum.Visible = false;
        }
    }
}