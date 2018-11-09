/**---------------------------AIR EXCEEDING REPORT ON DEPOSIT AMOUNT-----------
/*
 * Version     Created By        Date           Assigned By
 * 1.0         SHEETHAL       11/01/2016          Ayaz 
 * ------------------------------------------------------------------------------
 * -----------------------------PSEUDOCODE---------------------------------------
 * */
using System;
using DAL;
using System.Data.OracleClient;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_AIR_ExceedingReportForDeposit : System.Web.UI.Page
{
    DAL.DataFetch objDataFetch = new DataFetch();
    FillScheme objFillScheme = new FillScheme();
    OracleConnection oraConn = new OracleConnection();
    DataTable dtListDeposit = new DataTable();
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
                txtFrmDt.Text = "01/01/1985";
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
            }
        }
        catch (Exception ex)
        {
            objLog.WriteLog("AIR-ExceedingReportForDeposit.aspx: Error in Page Loading " + ex.Message);
            ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert('Error in Page Loading: " + ex.Message + "')</script>");
        }
        finally
        {
            objLog.WriteLog("AIR-ExceedingReportForDeposit.aspx:Page Loaded Successfully");
        }

    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        DataTable dtReport = new DataTable();
        DateTime temp;
        string sDeposit = "", dtToDate = "", dtFromDt = "", strDtFrmt = "";
        dtToDate = txtToDt.Text.ToString();
        dtFromDt = txtFrmDt.Text.ToString();
        dtFromDt = dtFromDt.Substring(6, 4);
        dtToDate = dtToDate.Substring(6, 4);
        strDtFrmt = dtFromDt + "-" + dtToDate;
        if (Session["constring"] != null)
        {
            oraConn = new OracleConnection(Session["constring"].ToString());
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
        if ((rblBranch.Items[1].Selected == true) && (chkListDeposit.SelectedValue == ""))
        {
            ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert ('Select atleast one Scheme') </script>");
            return;
        }
        try
        {
            for (int i = 0; i < chkListDeposit.Items.Count; i++)
            {
                if (chkListDeposit.Items[i].Selected)
                {
                    sDeposit += chkListDeposit.Items[i].Value.Trim() + ",";
                }
            }
            if (rblBranch.Items[1].Selected == true)
            {
                objLog.WriteLog("AGG_AMT_INYR procedure is calling for Branchwise");
                OracleCommand cmdProcExe = new OracleCommand("AGG_AMT_INYR", oraConn);
                cmdProcExe.CommandType = System.Data.CommandType.StoredProcedure;
                cmdProcExe.Parameters.Add("V_BRANCH", OracleType.Number).Value = ddlBranch.SelectedValue;
                cmdProcExe.Parameters.Add("V_DEPOSIT", OracleType.VarChar).Value = sDeposit;
                cmdProcExe.Parameters.Add("V_LOAN", OracleType.VarChar).Value = DBNull.Value;
                cmdProcExe.Parameters.Add("V_OD", OracleType.VarChar).Value = DBNull.Value;
                cmdProcExe.Parameters.Add("V_FROMDATE", OracleType.DateTime).Value = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(txtFrmDt.Text));
                cmdProcExe.Parameters.Add("V_TODATE", OracleType.DateTime).Value = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(txtToDt.Text));
                if (rblList.Items[0].Selected == true)
                    cmdProcExe.Parameters.Add("V_TYPE", OracleType.VarChar).Value = "YEARLY".Trim();
                else
                    cmdProcExe.Parameters.Add("V_TYPE", OracleType.VarChar).Value = "DAYWISE".Trim();
                cmdProcExe.Parameters.Add("V_SUMORDET", OracleType.VarChar).Value = "S".Trim();
                cmdProcExe.Parameters.Add("V_AMT", OracleType.Number).Value = txtAmnt.Text;
                cmdProcExe.Parameters.Add("V_OUT", OracleType.Cursor).Direction = System.Data.ParameterDirection.Output;

                OracleDataAdapter oDaReport = new OracleDataAdapter(cmdProcExe);
                oDaReport.Fill(dtReport);
                objLog.WriteLog("AGG_AMT_INYR procedure is executed successfully for Branchwise");
            }
            if (rblBranch.Items[0].Selected == true)
            {
                objLog.WriteLog("AGG_AMT_INYR procedure is calling for Conslidated option");
                OracleCommand cmdProcExe = new OracleCommand("AGG_AMT_INYR", oraConn);
                cmdProcExe.CommandType = System.Data.CommandType.StoredProcedure;
                cmdProcExe.Parameters.Add("V_BRANCH", OracleType.Number).Value = DBNull.Value;
                cmdProcExe.Parameters.Add("V_DEPOSIT", OracleType.VarChar).Value = sDeposit;
                cmdProcExe.Parameters.Add("V_LOAN", OracleType.VarChar).Value = DBNull.Value;
                cmdProcExe.Parameters.Add("V_OD", OracleType.VarChar).Value = DBNull.Value;
                cmdProcExe.Parameters.Add("V_FROMDATE", OracleType.DateTime).Value = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(txtFrmDt.Text));
                cmdProcExe.Parameters.Add("V_TODATE", OracleType.DateTime).Value = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(txtToDt.Text));
                if (rblList.Items[0].Selected == true)
                    cmdProcExe.Parameters.Add("V_TYPE", OracleType.VarChar).Value = "YEARLY".Trim();
                else
                    cmdProcExe.Parameters.Add("V_TYPE", OracleType.VarChar).Value = "DAYWISE".Trim();
                cmdProcExe.Parameters.Add("V_SUMORDET", OracleType.VarChar).Value = "S".Trim();
                cmdProcExe.Parameters.Add("V_AMT", OracleType.Number).Value = txtAmnt.Text;
                cmdProcExe.Parameters.Add("V_OUT", OracleType.Cursor).Direction = System.Data.ParameterDirection.Output;

                OracleDataAdapter oDaReport = new OracleDataAdapter(cmdProcExe);
                oDaReport.Fill(dtReport);
                objLog.WriteLog("AGG_AMT_INYR procedure is executed successfully for consolidated option");
            }
            Session["dtReport"] = dtReport;
            string Option = "";
            if (rblList.SelectedValue == "Dailywise")
            {
                Option = rblList.SelectedValue;
            }
            if (dtReport.Rows.Count > 0)
            {
                Response.Redirect("rptDispAIRDeposit.aspx?ch=" + ddlBranch.SelectedValue + "&dt=" + strDtFrmt + "&Opt=" + Option);
            }
            else
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert ('No Records Found...!!') </script>");
                return;
            }
        }
        catch (Exception exp)
        {
            objLog.WriteLog("AIR-ExceedingReportForDeposit.aspx: Error in ok button event" + exp.Message);
            ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert(' Error in ok button event: " + exp.Message + "')</script>");
        }
        finally
        {
            objLog.WriteLog("AIR-ExceedingReportForDeposit.aspx: Successfully Reached the Finally block of the ok click event");
        }
    }
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
            objLog.WriteLog("Schemes Binded to the checkbox list ");
        }
        catch (Exception ex)
        {
            objLog.WriteLog("AIR-ExceedingReportForDeposit.aspx: Error in ddlBranch_SelectedIndexChanged " + ex.Message);
            ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert('" + ex.Message + "')</script>");
        }
        finally
        {
            objLog.WriteLog("AIR-ExceedingReportForDeposit.aspx: Successfully reached the finally block of ddlBranch_SelectedIndexChanged Event");
        }
    }
    protected void btnBck_Click(object sender, EventArgs e)
    {
        Response.Redirect(Gen.REPORT_MENU, false);
    }
    protected void btnBck0_Click(object sender, EventArgs e)
    {

    }
}