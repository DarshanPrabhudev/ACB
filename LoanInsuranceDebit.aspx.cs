  
using System;
using System.Data;
using System.Data.OracleClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using Bal;

public partial class LoanInsuranceDebit : System.Web.UI.Page
{
    string strSql, strDate = "", strVchrDate = "";
    DataTable dtBranch=new DataTable() ;
    public DAL.DataFetch objDataFetch = new DAL.DataFetch();
    Log objlog = new Log();
    public DataTable dtScheme = new DataTable();

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
        OracleConnection oraConn = new OracleConnection(Session["constring"].ToString());
        if (oraConn.State != ConnectionState.Open)
            oraConn.Open();

        Bal.Gen objFillFromDate = new Bal.Gen();
        objFillFromDate.Call_Date(ddFromDay, ddFromMonth, ddFromYear);

        Bal.Gen objFillDate = new Bal.Gen();
        objFillDate.Call_Date(ddlVchrDay, ddlVchrMonth, ddlVchrYear);

        strSql = "select bran_sno,bran_name from MASTER.branch";
        objDataFetch.loginconnection = Session["constring"].ToString();
        objDataFetch.FillDdl(strSql, ddlBranch, "BRAN_SNO", "BRAN_NAME");
        ddlBranch.SelectedValue = Session["BranchLogin"].ToString();

        //rblTypes.SelectedValue = "LOAN";
                      
        strSql = "SELECT LSCH_ABBREV,LSCH_DES FROM LOANSCHEMESETUP WHERE LSCH_BRANCH=" + ddlBranch.SelectedValue;
        OracleDataAdapter odaScheme = new OracleDataAdapter(strSql, oraConn);
        odaScheme.Fill(dtScheme);
        
        if (dtScheme.Rows.Count <= 0)
        {
            ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert ('No Scheme found...') </script>");
            return;
        }

        ddlScheme.DataSource = dtScheme;
        ddlScheme.DataValueField = "LSCH_ABBREV";
        ddlScheme.DataTextField = "LSCH_DES";
        ddlScheme.DataBind();
        ddlScheme.Items.Insert(0, new ListItem("---select the scheme---"));
        oraConn.Close();
        objlog.WriteLog("LoanInsuranceDebit: strSql: " + strSql);              
    }    
    protected void btnOk_Click(object sender, EventArgs e)
    {
        DataTable dtDetail = new DataTable();
        DataTable dtSummary = new DataTable();
        DataTable dtTotDetail = new DataTable();
        DataTable dtLogin = new DataTable();
        double Amt = 0.0, dblInsrcGL = 0.0;
        DateTime temp;              
       
        dtLogin = (DataTable)Session["userid"];            
        int userId = 0;

        if (!DBNull.Value.Equals(dtLogin.Rows[0]["USER_ID"]))
            userId = Convert.ToInt32(dtLogin.Rows[0]["USER_ID"]);
        //-------------------        
        OracleConnection oraConn = new OracleConnection(Session["constring"].ToString());

        try
        {
            if (ddlScheme.SelectedValue == "---select the scheme---")
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert ('Select the Scheme type..!') </script>");
                return;
            }
            strDate = ddFromDay.SelectedValue + "-" + ddFromMonth.SelectedValue + "-" + ddFromYear.SelectedValue;
            strVchrDate = ddlVchrDay.SelectedValue + "-" + ddlVchrMonth.SelectedValue + "-" + ddlVchrYear.SelectedValue;

            if ((!(DateTime.TryParse(strDate, out temp))) || (Convert.ToDateTime(strDate) > DateTime.Now))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert ('Enter a Valid Date') </script>");
                return;
            }
            if ((!(DateTime.TryParse(strVchrDate, out temp))) || (Convert.ToDateTime(strDate) > DateTime.Now))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert ('Enter the Valid Voucher Date') </script>");
                return;
            }
            //---------------------
            objlog.WriteLog("LoanInsuranceDebit: DATE: " + strDate);
            objlog.WriteLog("LoanInsuranceDebit: DATE: " + strVchrDate);

            objlog.WriteLog("LoanInsuranceDebit: BEFORE CALLING PROC:PARTYSCHEDULE.BRANCHWISEPS");

            DAL.conn objcon = new DAL.conn();

            OracleCommand cmdProcExec = new OracleCommand("PARTYSCHEDULE.BRANCHWISEPS", oraConn);
            cmdProcExec.CommandType = System.Data.CommandType.StoredProcedure;
            cmdProcExec.Parameters.Add("P_BNO", OracleType.Number).Value = ddlBranch.SelectedValue;
            cmdProcExec.Parameters.Add("P_ASONDATE", OracleType.DateTime).Value = Convert.ToDateTime(strDate);//GlobalConstants.strVocDate ;
            cmdProcExec.Parameters.Add("P_TYPE", OracleType.VarChar).Value = rblTypes.SelectedValue;
            cmdProcExec.Parameters.Add("P_ABBR", OracleType.VarChar).Value = ddlScheme.SelectedValue.Trim();
            cmdProcExec.Parameters.Add("PSLREF", OracleType.Cursor).Direction = System.Data.ParameterDirection.Output;

            objlog.WriteLog("LoanInsuranceDebit: AFTR CALLING PROC - PARTYSCHEDULE.BRANCHWISEPS");

            OracleDataAdapter odaPartyBal = new OracleDataAdapter(cmdProcExec);
            DataTable dtPartyBal = new DataTable();
            odaPartyBal.Fill(dtPartyBal);

            dtDetail.Columns.Add(new DataColumn("SNo", typeof(string)));
            dtDetail.Columns.Add(new DataColumn("Abbr", typeof(string)));
            dtDetail.Columns.Add(new DataColumn("A/C No", typeof(string)));
            dtDetail.Columns.Add(new DataColumn("Name", typeof(string)));
            dtDetail.Columns.Add(new DataColumn("Amount", typeof(string)));

            //dtDetail.Columns.Add(new DataColumn("Balance", typeof(string)));

            if (dtPartyBal.Rows.Count <= 0)
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert ('No Records..... Change the Criteria..') </script>");
                return;
            }
            else
            {
                Session["date"] = strDate;
                Session["VchrDate"] = strVchrDate;
                Session["branch"] = ddlBranch.SelectedValue;

                int i = 1;
                foreach (DataRow dr in dtPartyBal.Rows)
                {
                    if (dr["CLCRDR"].ToString().ToUpper().Trim() == "DR".Trim())
                    {
                        DataRow row = dtDetail.NewRow();
                        row["SNo"] = i.ToString();
                        row["Abbr"] = dr["ABBR"];
                        row["A/C No"] = dr["ACNO"];

                        int length = dr["CNAME"].ToString().Length;
                        if (length > 30)
                            row["Name"] = dr["CNAME"].ToString().Substring(0, 30);
                        else
                            row["Name"] = dr["CNAME"];

                        if (txtAmount.Text == "")
                        {
                            ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert ('Enter the Amount') </script>");
                            return;
                        }
                        else
                        {
                            row["Amount"] = txtAmount.Text;
                            Amt = Amt + Convert.ToDouble(txtAmount.Text);
                        }

                        if (txtInsurance.Text == "")
                        {
                            ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert ('Enter the GL Number') </script>");
                            return;
                        }
                        else
                        {
                            dblInsrcGL = Convert.ToDouble(txtInsurance.Text);
                            Session["insrcgl"] = null;
                            Session["insrcgl"] = dblInsrcGL;
                        }

                        //row["Amount"] = txtAmount.Text;                        
                        //Amt=Amt + Convert.ToDouble(txtAmount.Text);
                        //row["Balance"] = String.Format("{0:0.00}", dr["BALAMT"]);

                        dtDetail.Rows.Add(row);
                        i++;
                    }
                }
                DataTable dtLoan = new DataTable();
                dtLoan = dtDetail;
                Session["loan"] = dtLoan;

                DataRow row1 = dtDetail.NewRow();
                row1["Name"] = "TOTAL";
                row1["Amount"] = String.Format("{0:0.00}", Amt);
                dtDetail.Rows.Add(row1);
                GlobalConstants.strVocDate = strDate;

                //Added on 10.3.2014
                Session["BnkName"] = global.BankName();
                Session["heading"] = "Loan Insurance Debit Details " + GlobalConstants.strVocDate;
                Session["strbrn"] = ddlBranch.SelectedItem.Text;
                //-------------------

                Session["PartyBalance"] = dtDetail;
                //Session["schedule"] = dtPartyBal;
                Response.Redirect("rptDisp_LoanInsuranceDebit.aspx?branch=" + ddlBranch.SelectedItem.Text + "&Brn=" + GlobalConstants.strBnkName + "&AccName=" + ddlScheme.SelectedItem + "&Head=" + Session["heading"], false);
                //  }               
            }
        }
        catch (Exception ex)
        {
            objlog.WriteLog(ex.Message);
            ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('" + ex.Message + "')</script>");
        }
        finally
        {
            if (oraConn.State == ConnectionState.Open)
                oraConn.Close();
            oraConn.Dispose();
            OracleConnection.ClearAllPools();
            dtDetail.Dispose();
            dtSummary.Dispose();
            dtTotDetail.Dispose();
            dtLogin.Dispose();
        }
    }
    protected void rblTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        OracleConnection oraConn = new OracleConnection(Session["constring"].ToString());

        txtInsurance.Text = "";
        txtAmount.Text = "";

        switch (rblTypes.SelectedValue)
        {
            case "LOAN":
                strSql = "SELECT LSCH_DES as Name,LSCH_ABBREV as Abbr FROM LOANSCHEMESETUP WHERE LSCH_BRANCH=" + ddlBranch.SelectedValue;
                break;        

            case "OD":
                strSql = "SELECT ODST_NAME as Name,ODST_ABBR as Abbr FROM ODSCHEMESETUP WHERE ODST_BRANCH=" + ddlBranch.SelectedValue;
                break;
        }
        OracleDataAdapter odaScheme = new OracleDataAdapter(strSql, oraConn);
        odaScheme.Fill(dtScheme);
      
        ddlScheme.DataSource = dtScheme;
        ddlScheme.DataTextField = "Name";
        ddlScheme.DataValueField = "Abbr";
        ddlScheme.DataBind();
        ddlScheme.Items.Insert(0, new ListItem("---select the scheme---"));
        oraConn.Close();
        objlog.WriteLog("LoanInsuranceDebit: strSql: " + strSql);
    }
    
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect(global.REPORT_MENU);
    }    
    protected void rblSumDetail_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }    
    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
 
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        OracleConnection oraConn = new OracleConnection(Session["constring"].ToString());
        txtInsurance.Text = "";
        txtAmount.Text = "";
        rblTypes.SelectedValue = "LOAN";

        strSql = "SELECT LSCH_DES as Name,LSCH_ABBREV as Abbr FROM LOANSCHEMESETUP WHERE LSCH_BRANCH=" + ddlBranch.SelectedValue;
        
        OracleDataAdapter odaScheme = new OracleDataAdapter(strSql, oraConn);
        odaScheme.Fill(dtScheme);

        ddlScheme.DataSource = dtScheme;
        ddlScheme.DataTextField = "Name";
        ddlScheme.DataValueField = "Abbr";
        ddlScheme.DataBind();
        ddlScheme.Items.Insert(0, new ListItem("---select the scheme---"));
        oraConn.Close();
        objlog.WriteLog("LoanInsuranceDebit: strSql: " + strSql);
    }
}
