  
using System;
using System.Data;
using System.Data.OracleClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using Bal;

public partial class LoanInsrnceAutoPosting : System.Web.UI.Page
{
    string strSql, strDate = "";    
    DataTable dtPartyBal = new DataTable();
    OracleConnection oraConn = new OracleConnection();
    public DAL.DataFetch objDataFetch = new DAL.DataFetch();
    LoanInsuranceAutoPosting objLoanInsurance = new LoanInsuranceAutoPosting();
    Log objLog = new Log();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session.Contents.Count == 0)
        {
            Response.Redirect("ShowMessage.aspx?msg=" + GlobalConstants.msg);
        }
        else
        {
            if (!IsPostBack)
                FillLoad();
        }
    }
    protected void FillLoad()
    {
        DataTable dtLoanList = new DataTable();
        DataTable dtListOD = new DataTable();

        if (Session["constring"] != null)
        {
            oraConn = new OracleConnection(Session["constring"].ToString());
            if (oraConn.State != ConnectionState.Open) oraConn.Open();
        }
        try
        {
            Bal.Gen objFillFromDate = new Bal.Gen();
            objFillFromDate.Call_Date(ddFromDay, ddFromMonth, ddFromYear);

            Bal.Gen objFillDate = new Bal.Gen();
            objFillDate.Call_Date(ddlPostingDay, ddlPostingMonth, ddlPostingYear);

            strSql = objLoanInsurance.BranchFill();
            objDataFetch.FillDdl(strSql, ddlBranch, "BRAN_SNO", "BRAN_NAME");
            ddlBranch.SelectedValue = Session["BranchLogin"].ToString();

            PanelOD.Visible = false;
            strSql = objLoanInsurance.LoanFill(ddlBranch.SelectedValue);
            OracleDataAdapter oDaLoan = new OracleDataAdapter(strSql, oraConn);
            oDaLoan.Fill(dtLoanList);

            if (dtLoanList.Rows.Count <= 0)
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert ('No Loan Scheme Found...') </script>");
                return;
            }
            chkList.DataSource = dtLoanList;
            chkList.DataValueField = "LSCH_ABBREV";
            chkList.DataTextField = "LSCH_DES";
            chkList.DataBind();
            oraConn.Close();

            strSql = objLoanInsurance.ODFill(ddlBranch.SelectedValue);
            OracleDataAdapter oDaOD = new OracleDataAdapter(strSql, oraConn);
            oDaOD.Fill(dtListOD);

            if (dtListOD.Rows.Count <= 0)
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert ('No OD Scheme Found...') </script>");
                return;
            }
            chkListOd.DataSource = dtListOD;
            chkListOd.DataValueField = "ODST_ABBR";
            chkListOd.DataTextField = "ODST_NAME";
            chkListOd.DataBind();
        }
        catch (Exception ex)
        {
            objLog.WriteLog("LoanInsrnceAutoPosting Exception in FillLoad : " + ex.Message);
            ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('" + ex.Message + "')</script>");
        }
        finally
        {
            if (oraConn.State == ConnectionState.Open)
                oraConn.Close();
            oraConn.Dispose();
            dtLoanList.Dispose();
            dtListOD.Dispose();
            OracleConnection.ClearAllPools();
        }
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {       
        string abbr = "", strPostingDate = "";
        int abbrCount = 0, n = 0;
        double dblInsuranceGL = 0, dblInsuranceIntGL = 0, dblTotInterestDue = 0.00, dblTotInsurnceDue = 0.00, dblInterestDue = 0.00, dblInsuranceDue = 0.00,
            dblAmount = 0.00, dblLimit = 0.00;

        DataTable dtDetail = new DataTable();
        DataTable dtSiteParameters = new DataTable();
        DataTable dtOtherInt = new DataTable();
        DataTable dtAmount = new DataTable();
        DataTable dtLimit = new DataTable();
        DateTime temp;

        if (Session["constring"] != null)
        {
            oraConn = new OracleConnection(Session["constring"].ToString());
            if (oraConn.State != ConnectionState.Open) oraConn.Open();
        }
        try
        {
            strDate = ddFromDay.SelectedValue + "-" + ddFromMonth.SelectedValue + "-" + ddFromYear.SelectedValue;
            strPostingDate = ddlPostingDay.SelectedValue + "-" + ddlPostingMonth.SelectedValue + "-" + ddlPostingYear.SelectedValue;

            if ((!(DateTime.TryParse(strDate, out temp))) || (Convert.ToDateTime(strDate) > DateTime.Now))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert ('Enter a Valid Date') </script>");
                return;
            }
            if ((!(DateTime.TryParse(strPostingDate, out temp))) || (Convert.ToDateTime(strPostingDate) > DateTime.Now))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert ('Enter a Valid Posting Date') </script>");
                return;
            }
            if ((chkList.SelectedValue == "") && (chkListOd.SelectedValue == ""))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert ('Select atleast one Scheme') </script>");
                return;
            }
            //---------------------
            objLog.WriteLog("LoanInsrnceAutoPosting: DATE: " + strDate);
            objLog.WriteLog("LoanInsrnceAutoPosting: DATE: " + strPostingDate);

            DataTable dtParty = new DataTable();

            for (int i = 0; i < chkList.Items.Count; i++)
            {
                if (chkList.Items[i].Selected)
                {
                    abbr = chkList.Items[i].Value.Trim();
                    abbrCount = abbrCount + 1;
                    objLog.WriteLog("LoanInsrnceAutoPosting: BEFORE CALLING PROC FOR LOAN :PARTYSCHEDULE.BRANCHWISEPS");
                    dtPartyBal = PartyBalance(strDate, abbr);
                    objLog.WriteLog("LoanInsrnceAutoPosting: AFTER CALLING PROC - FOR LOAN :PARTYSCHEDULE.BRANCHWISEPS");
                    objLog.WriteLog("LoanInsrnceAutoPosting : dtPartyBal Row Count(LOAN) - " + dtPartyBal.Rows.Count.ToString());
                }
            }
            for (int i = 0; i < chkListOd.Items.Count; i++)
            {
                if (chkListOd.Items[i].Selected)
                {
                    abbr = chkListOd.Items[i].Value.Trim();
                    abbrCount = abbrCount + 1;
                    objLog.WriteLog("LoanInsrnceAutoPosting: BEFORE CALLING PROC FOR OD :PARTYSCHEDULE.BRANCHWISEPS");
                    dtPartyBal = PartyBalance(strDate, abbr);
                    objLog.WriteLog("LoanInsrnceAutoPosting: AFTER CALLING PROC - FOR OD :PARTYSCHEDULE.BRANCHWISEPS");
                    objLog.WriteLog("LoanInsrnceAutoPosting : dtPartyBal Row Count(OD) - " + dtPartyBal.Rows.Count.ToString());
                }
            }

            if (dtPartyBal.Rows.Count <= 0)
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert ('No Records...Change the Criteria...') </script>");
                return;
            }
            else
            {
                Session["PostingDate"] = strPostingDate;
                Session["selectedBranch"] = ddlBranch.SelectedValue;

                dtDetail.Columns.Add(new DataColumn("SNo", typeof(string)));
                dtDetail.Columns.Add(new DataColumn("Abbr", typeof(string)));
                dtDetail.Columns.Add(new DataColumn("A/C No", typeof(string)));
                dtDetail.Columns.Add(new DataColumn("Name", typeof(string)));
                dtDetail.Columns.Add(new DataColumn("Interest Due", typeof(string)));
                dtDetail.Columns.Add(new DataColumn("Insurance Due", typeof(string)));
                dtDetail.Columns.Add(new DataColumn("Balance", typeof(string)));
                dtDetail.Columns.Add(new DataColumn("Limit", typeof(string)));
                dtDetail.Columns.Add(new DataColumn("Amount", typeof(string)));
                dtDetail.Columns.Add(new DataColumn(" ", typeof(string)));


                n = 1;
                foreach (DataRow dr in dtPartyBal.Rows)
                {
                    if (dr["CLCRDR"].ToString().ToUpper().Trim() == "DR".Trim())
                    {
                        DataRow row = dtDetail.NewRow();
                        row["SNo"] = n.ToString();
                        if (!DBNull.Value.Equals(dr["ABBR"]))
                            row["Abbr"] = dr["ABBR"];
                        if (!DBNull.Value.Equals(dr["ACNO"]))
                            row["A/C No"] = dr["ACNO"];

                        if (!DBNull.Value.Equals(dr["CNAME"]))
                        {
                            int length = dr["CNAME"].ToString().Length;
                            if (length > 30)
                                row["Name"] = dr["CNAME"].ToString().Substring(0, 30);
                            else
                                row["Name"] = dr["CNAME"];
                        }
                        dtSiteParameters.Clear();
                        dtSiteParameters = objLoanInsurance.Insurance(oraConn);
                        objLog.WriteLog("LoanInsrnceAutoPosting (dtSiteParameters)Row Count" + dtSiteParameters.Rows.Count.ToString());
                        if (dtSiteParameters.Rows.Count > 0)
                        {
                            if (!DBNull.Value.Equals(dtSiteParameters.Rows[0]["SITP_LONGVALUE"]))
                            {
                                dblInsuranceGL = Convert.ToDouble(dtSiteParameters.Rows[0]["SITP_LONGVALUE"]);
                            }
                            if (!DBNull.Value.Equals(dtSiteParameters.Rows[0]["SITP_DOUBLEVALUE"]))
                            {
                                dblInsuranceIntGL = Convert.ToDouble(dtSiteParameters.Rows[0]["SITP_DOUBLEVALUE"]);
                            }
                        }
                        objLog.WriteLog("LoanInsrnceAutoPosting:dblInsuranceGL : " + dblInsuranceGL);
                        objLog.WriteLog("LoanInsrnceAutoPosting:dblInsuranceIntGL : " + dblInsuranceIntGL);

                        if (!DBNull.Value.Equals(dr["ACNO"]))
                        {
                            dtOtherInt.Clear();
                            dtOtherInt = objLoanInsurance.OtherInt(dr["ABBR"].ToString(), Convert.ToDouble(dr["ACNO"]), strDate, dblInsuranceGL, dblInsuranceIntGL, ddlBranch.SelectedValue, oraConn);
                        }
                        if ((!DBNull.Value.Equals(dtOtherInt.Rows[0]["OI"])) && (Convert.ToDouble(dtOtherInt.Rows[0]["OI"]) > 0))
                        {
                            row["Interest Due"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(dtOtherInt.Rows[0]["OI"])));
                            dblTotInterestDue = dblTotInterestDue + Math.Abs(Convert.ToDouble(dtOtherInt.Rows[0]["OI"]));
                            dblInterestDue = Math.Abs(Convert.ToDouble(dtOtherInt.Rows[0]["OI"]));
                        }
                        if (!DBNull.Value.Equals(dr["ACNO"]))
                        {
                            dtAmount.Clear();
                            dtAmount = objLoanInsurance.Others(dr["ABBR"].ToString(), Convert.ToDouble(dr["ACNO"]), strDate, dblInsuranceGL, ddlBranch.SelectedValue, oraConn);
                        }
                        if ((!DBNull.Value.Equals(dtAmount.Rows[0]["PI"])) && (Convert.ToDouble(dtAmount.Rows[0]["PI"]) > 0))
                        {
                            row["Insurance Due"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(dtAmount.Rows[0]["PI"])));
                            dblTotInsurnceDue = dblTotInsurnceDue + Math.Abs(Convert.ToDouble(dtAmount.Rows[0]["PI"]));
                            dblInsuranceDue = Math.Abs(Convert.ToDouble(dtAmount.Rows[0]["PI"]));
                        }
                        if (!DBNull.Value.Equals(dr["BALAMT"]))
                            row["Balance"] = String.Format("{0:0.00}", dr["BALAMT"]);

                        if (rblTypes.SelectedValue.ToUpper().Trim() == "LOAN")
                        {
                            dtLimit.Clear();
                            if (!DBNull.Value.Equals(dr["ACNO"]))
                                dtLimit = objLoanInsurance.LoanLimitSanctioned(dr["ABBR"].ToString(), Convert.ToDouble(dr["ACNO"]), ddlBranch.SelectedValue, oraConn);
                            if (dtLimit.Rows.Count > 0)
                            {
                                if (!DBNull.Value.Equals(dtLimit.Rows[0]["LOAM_SANCTIONEDAMOUNT"]))
                                {
                                    row["Limit"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(dtLimit.Rows[0]["LOAM_SANCTIONEDAMOUNT"])));
                                    dblLimit = Math.Abs(Convert.ToDouble(dtLimit.Rows[0]["LOAM_SANCTIONEDAMOUNT"]));
                                }
                            }
                        }
                        else if (rblTypes.SelectedValue.ToUpper().Trim() == "OD")
                        {
                            dtLimit.Clear();
                            if (!DBNull.Value.Equals(dr["ACNO"]))
                                dtLimit = objLoanInsurance.ODLimitSanctioned(dr["ABBR"].ToString(), Convert.ToDouble(dr["ACNO"]), ddlBranch.SelectedValue, oraConn);
                            if (dtLimit.Rows.Count > 0)
                            {
                                if (!DBNull.Value.Equals(dtLimit.Rows[0]["ODRN_LIMIT"]))
                                {
                                    row["Limit"] = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(dtLimit.Rows[0]["ODRN_LIMIT"])));
                                    dblLimit = Math.Abs(Convert.ToDouble(dtLimit.Rows[0]["ODRN_LIMIT"]));
                                }
                            }
                        }
                        if ((dblInterestDue != 0) && (dblInsuranceDue != 0) && (dblLimit != 0) && (!DBNull.Value.Equals(dr["BALAMT"])))
                        {
                            if (dblLimit < Convert.ToDouble(dr["BALAMT"]))
                            {
                                dblAmount = dblInterestDue + dblInsuranceDue;
                                row["Amount"] = String.Format("{0:0.00}", dblAmount);
                            }
                            else if (dblLimit > Convert.ToDouble(dr["BALAMT"]))
                            {
                                row[" "] = "LIMIT EXCEEDED";
                            }
                        }
                        if (((!DBNull.Value.Equals(dtOtherInt.Rows[0]["OI"])) && (Convert.ToDouble(dtOtherInt.Rows[0]["OI"]) > 0)) || ((!DBNull.Value.Equals(dtAmount.Rows[0]["PI"])) && (Convert.ToDouble(dtAmount.Rows[0]["PI"]) > 0)))
                        {
                            dtDetail.Rows.Add(row);
                            n++;
                        }
                    }
                }
                if (dtDetail.Rows.Count > 0)
                {
                    DataRow drTotal = dtDetail.NewRow();
                    drTotal["Name"] = "TOTAL";
                    drTotal["Interest Due"] = String.Format("{0:0.00}", dblTotInterestDue);
                    drTotal["Insurance Due"] = String.Format("{0:0.00}", dblTotInsurnceDue);
                    dtDetail.Rows.Add(drTotal);

                    Session["LoanAutoPost"] = dtDetail;
                    Session["BnkName"] = global.BankName();
                    if (rblTypes.SelectedValue.ToUpper().Trim() == "LOAN")
                        Session["heading"] = "Loan Insurance Auto Posting Details As On " + strDate;
                    if (rblTypes.SelectedValue.ToUpper().Trim() == "OD")
                        Session["heading"] = "OD Insurance Auto Posting Details As On " + strDate;

                    Session["strbrn"] = ddlBranch.SelectedItem.Text;
                    Response.Redirect("rptDisp_LoanInsuranceAutoPost.aspx?rb=" + rblTypes.SelectedValue);
                }
                else if (dtDetail.Rows.Count <= 0)
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert ('No Data Found...!') </script>");
                    return;
                }
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
            dtDetail.Dispose();
            dtAmount.Dispose();
            dtLimit.Dispose();
            dtOtherInt.Dispose();
            dtSiteParameters.Dispose();
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect(global.REPORT_MENU);
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        OracleConnection oraConn = new OracleConnection(Session["constring"].ToString());
        DataTable dtScheme = new DataTable();

        switch (rblTypes.SelectedValue)
        {
            case "LOAN":
                strSql = objLoanInsurance.LoanFill(ddlBranch.SelectedValue);
                OracleDataAdapter oDaType = new OracleDataAdapter(strSql, oraConn);
                oDaType.Fill(dtScheme);
                chkList.DataSource = dtScheme;
                chkList.DataValueField = "LSCH_ABBREV";
                chkList.DataTextField = "LSCH_DES";
                chkList.DataBind();
                break;

            case "OD":
                strSql = objLoanInsurance.ODFill(ddlBranch.SelectedValue);
                OracleDataAdapter oDaODType = new OracleDataAdapter(strSql, oraConn);
                oDaODType.Fill(dtScheme);
                chkListOd.DataSource = dtScheme;
                chkListOd.DataValueField = "ODST_ABBR";
                chkListOd.DataTextField = "ODST_NAME";
                chkListOd.DataBind();
                break;
        }
        if (dtScheme.Rows.Count <= 0)
        {
            ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert ('No Scheme Found...') </script>");
            return;
        }
    }
    protected DataTable PartyBalance(string sDate, string abbr)
    {
        OracleCommand cmdProcExec = new OracleCommand("PARTYSCHEDULE.BRANCHWISEPS", oraConn);
        cmdProcExec.CommandType = System.Data.CommandType.StoredProcedure;
        cmdProcExec.Parameters.Add("P_BNO", OracleType.Number).Value = ddlBranch.SelectedValue;
        cmdProcExec.Parameters.Add("P_ASONDATE", OracleType.DateTime).Value = sDate;
        cmdProcExec.Parameters.Add("P_TYPE", OracleType.VarChar).Value = rblTypes.SelectedValue;
        cmdProcExec.Parameters.Add("P_ABBR", OracleType.VarChar).Value = abbr;
        cmdProcExec.Parameters.Add("PSLREF", OracleType.Cursor).Direction = System.Data.ParameterDirection.Output;

        objLog.WriteLog("LoanInsrnceAutoPosting: AFTR CALLING PROC - PARTYSCHEDULE.BRANCHWISEPS");
        OracleDataAdapter oDaPartyBal = new OracleDataAdapter(cmdProcExec);        
        oDaPartyBal.Fill(dtPartyBal);
        return dtPartyBal;
    }
    protected void btnSelect_Click(object sender, EventArgs e)
    {
        if (rblTypes.SelectedValue.ToUpper().Trim() == "LOAN")
        {
            foreach (ListItem li in chkList.Items)
            {
                li.Selected = true;
            }
        }
        if (rblTypes.SelectedValue.ToUpper().Trim() == "OD")
        {
            foreach (ListItem li in chkListOd.Items)
            {
                li.Selected = true;
            }
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("LoanInsrnceAutoPosting.aspx");
    }
    protected void rblTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblTypes.SelectedValue.ToUpper().Trim() == "LOAN")
        {
            PanelOD.Visible = false;
            PanelLoan.Visible = true;
            chkListOd.ClearSelection();
        }
        else if (rblTypes.SelectedValue.ToUpper().Trim() == "OD")
        {
            PanelOD.Visible = true;
            PanelLoan.Visible = false;
            chkList.ClearSelection();
        }
    }
}
