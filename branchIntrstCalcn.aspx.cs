/*>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>Branch Interest Calculation<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

 Version            Developed By            Date                  Assigned By
 -----------------------------------------------------------------------------------------------------------------
  1.0                 Meghana             11.04.2014                  VK
------------------------------------------------------------------------------------------------------------------
-------------------------------------------------Pseudo Code------------------------------------------------------
 This Report is used to calculate the Interest Rates.
 INPUTS - Branch,From Date,To Date and Credit Int. Rate,Debit Int. Rate.
 OUTPUT - DEP NO,A/C Holder, Dr.Prod,Int%,Dr Int.,Cr.Prod,Int%,Cr Int. 
 No Stored Procedure used  
-----------------------------------------------------------------------------------------------------------------*/

using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using DAL;
using Bal;


public partial class branchIntrstCalcn : System.Web.UI.Page
{
    DAL.DataFetch objDataFetch = new DAL.DataFetch();
    DataTable dtBranch = new DataTable();
    Log objLog = new Log();
    OracleConnection oraConn = new OracleConnection();
    OracleDataAdapter oDaBranch = new OracleDataAdapter(); 

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session.Contents.Count == 0)
        {
            Response.Redirect("ShowMessage.aspx?msg=" + GlobalConstants.msg);
        }
        else
        {
            try
            {
                OracleConnection oraConn = new OracleConnection(Session["constring"].ToString());
                if (!Page.IsPostBack)
                {
                    if (oraConn.State != ConnectionState.Open) oraConn.Open();
                    string strSql = "";

                    Bal.Gen objFillDate = new Bal.Gen();
                    objFillDate.Call_Date(ddlFromDay, ddlFromMonth, ddlFromYear);

                    Bal.Gen objFillToDate = new Bal.Gen();
                    objFillToDate.Call_Date(ddlToDay, ddlToMonth, ddlToYear);

                    OracleConnection con = new OracleConnection(Session["constring"].ToString());
                    strSql = "select bran_sno,bran_name from MASTER.branch";
                    oDaBranch = new OracleDataAdapter(strSql, con);
                    oDaBranch.Fill(dtBranch);
                    ddlBranch.DataSource = dtBranch;
                    ddlBranch.DataTextField = "bran_name";
                    ddlBranch.DataValueField = "bran_sno";
                    ddlBranch.DataBind();
                    ddlBranch.SelectedValue = 1.ToString();//BY NAYANA
                    // ddlBranch.Enabled = true;

                    if (!global.Valid_Activity(30005))
                    {
                        ddlBranch.Enabled = false;
                    }
                    else
                    {
                        ddlBranch.Enabled = true;
                    }
                    ddlBranch.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                objLog.WriteLog(ex.Message);
                ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert('" + ex.Message + "')</script>");
            }
        }
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        string strFromDate = "", strToDate = "", strSql = "", strHeading = "";
        DateTime temp;
        DataTable dtAcBal = new DataTable();
        DataTable dtBranches = new DataTable();
        DataTable dtDetails = new DataTable();
        DataRow row;     
        DateTime dFrom;
        DateTime dTo;

        double CrIntRate = 0.00, DrIntRate = 0.00, dblCrInt, dblDrInt, dblDays,
        dblBal = 0.00, dblOldBal = 0.00,drTotal = 0.00, crTotal = 0.00, dblCrProd, dblDrprod;

        dblDays = 1;       
  
        try
        {
            strFromDate = ddlFromDay.SelectedValue + "-" + ddlFromMonth.SelectedItem.Text + "-" + ddlFromYear.SelectedValue;

            objLog.WriteLog("BRANCH INTEREST CALCN - strFromDate : " + strFromDate);

            strToDate = ddlToDay.SelectedValue + "-" + ddlToMonth.SelectedItem.Text + "-" + ddlToYear.SelectedValue;

            objLog.WriteLog("BRANCH INTEREST CALCN - strToDate : " +  strToDate);

            if (!(DateTime.TryParse(strFromDate, out temp))||(!(DateTime.TryParse(strToDate, out temp))))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert ('Enter a Valid Date') </script>");
                return;
            }
            if ((Convert.ToDateTime(strFromDate) > DateTime.Now) || (Convert.ToDateTime(strToDate) > DateTime.Now))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert ('Enter a Valid Date') </script>");
                return;
            }
            if (Convert.ToDateTime(strFromDate) > Convert.ToDateTime(strToDate))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert ('Check From Date') </script>");
                return;
            }
            dtDetails.Columns.Add("DEP NO",typeof(string));
            dtDetails.Columns.Add("A/C Holder",typeof(string));
            dtDetails.Columns.Add("Dr.Prod",typeof(string));
            dtDetails.Columns.Add("Int%",typeof(string));
            dtDetails.Columns.Add("Dr Int.",typeof(string));
            dtDetails.Columns.Add("Cr.Prod",typeof(string));
            dtDetails.Columns.Add(" Int%",typeof(string));
            dtDetails.Columns.Add("Cr Int.",typeof(string));

            if (txtCredit.Text != "")
            {
                CrIntRate = Convert.ToDouble(txtCredit.Text);
            }
            if (txtDebit.Text != "")
            {
                DrIntRate = Convert.ToDouble(txtDebit.Text);
            }

            strSql = " SELECT DEPM_ABBR,DEPM_NO,DEPM_BRANCH,DSCH_SNO,DSCH_GLLINK,DSCH_SLLINK,CLMS_DES";
            strSql = strSql + " From DEPOSITMASTER,DEPOSITSCHEMESETUP,CLIENTMASTER";
            strSql = strSql + " Where DEPM_ABBR = DSCH_ABBR";
            strSql = strSql + " AND DEPM_CLIENT = CLMS_SNO";
            strSql = strSql + " AND DEPM_ABBR = 'BR'";
            strSql = strSql + " AND DEPM_BRANCH = DSCH_BRANCH";
            strSql = strSql + " AND DEPM_BRANCH = " + ddlBranch.SelectedValue;
            strSql = strSql + " ORDER BY DEPM_NO";

            objDataFetch.loginconnection = Session["constring"].ToString();
            dtBranches = objDataFetch.DatatablePassSQL(strSql);

            if (dtBranches.Rows.Count > 0)
            {
                foreach (DataRow dr in dtBranches.Rows)
                {
                    dFrom = Convert.ToDateTime(strFromDate);
                    dTo = Convert.ToDateTime(strToDate);                                            
                    dblCrInt = 0.00;
                    dblDrInt = 0.00;
                    dblCrProd = 0.00;
                    dblDrprod = 0.00;
                    dblOldBal = 0.00;

                    //while (Convert.ToDateTime(strToDate) >= Convert.ToDateTime(strFromDate))


                    while (dTo >= dFrom)
                    {
                        dblDays = 1;

                        strSql = "SELECT ACCOUNTBALANCE('";
                        strSql = strSql + dr["DEPM_ABBR"].ToString().Trim() + "',";
                        strSql = strSql + dr["DEPM_NO"] + ",";
                        strSql = strSql + "'" + String.Format("{0:dd/MMM/yyyy}", dFrom) + "','R',";
                        strSql = strSql + dr["DSCH_GLLINK"] + ",";
                        strSql = strSql + dr["DSCH_SLLINK"] + ",";
                        strSql = strSql + (ddlBranch.SelectedValue) + ") AS AMT FROM DUAL ";
                        objDataFetch.loginconnection = Session["constring"].ToString();

                        dtAcBal = objDataFetch.DatatablePassSQL(strSql);


                        if (dtAcBal.Rows.Count != 0)
                        {
                            if (!DBNull.Value.Equals(dtAcBal.Rows[0]["AMT"]))
                                dblBal = Convert.ToDouble(dtAcBal.Rows[0]["AMT"]);
                        }
                        if (dblBal >= 0)
                        {
                            dblCrProd = dblCrProd + (dblBal);
                            dblDays = 0;
                        }
                        else
                        {
                            dblDrprod = dblDrprod + (dblBal);
                            dblDays = 0;
                        }
                        dblOldBal = dblBal;
                        dFrom = dFrom.AddDays(1);                    
                    }

                    row = dtDetails.NewRow();
                    row["DEP NO"] = dr["DEPM_ABBR"] + "  " + dr["DEPM_NO"];
                    row["A/C Holder"] = dr["CLMS_DES"];

                    dblDrInt = ((Math.Abs(dblDrprod) * DrIntRate) / 36500);
                    row["Dr.Prod"] = String.Format("{0:0.00}", Math.Abs(dblDrprod));                   
                    row["Int%"] = String.Format("{0:0.00}", DrIntRate);


                    drTotal = drTotal + dblDrInt;
                    row["Dr Int."] = String.Format("{0:0.00}", Math.Abs(dblDrInt));
                    dblCrInt = ((Math.Abs(dblCrProd) * CrIntRate) / 36500);

                    dblCrProd = Math.Abs(dblCrProd);
                    row["Cr.Prod"]=String.Format("{0:0.00}",Math.Round(dblCrProd));
                    row[" Int%"] = String.Format("{0:0.00}", CrIntRate);

                    crTotal = crTotal + dblCrInt;
                    row["Cr Int."] = String.Format("{0:0.00}", Math.Round(dblCrInt));

                    dtDetails.Rows.Add(row);
                }

                row = dtDetails.NewRow();

                drTotal = Math.Round(drTotal);
                crTotal = Math.Round(crTotal);

                row["DEP NO"] = "TOTAL";
                row["Dr Int."] = String.Format("{0:0.00}", Math.Abs(drTotal));
                row["Cr Int."] = String.Format("{0:0.00}", crTotal);
                dtDetails.Rows.Add(row);

            }
            else
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert ('No Records...Change the Criteria...') </script>");
                return;
            }

            strHeading = "Branch Interest Calculation From " + strFromDate + " To " + strToDate;          
            Session["BnkName"] = global.BankName();
            Session["strbrn"] = ddlBranch.SelectedItem.Text;
            Session["heading"] = strHeading;
            Session["brnchintrstcalcn"] = dtDetails;
            Response.Redirect("rptDisp_BranchIntrstCalcn.aspx");    
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
            dtAcBal.Dispose();
            dtBranches.Dispose();
            dtDetails.Dispose();
            OracleConnection.ClearAllPools();
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect(Gen.REPORT_MENU);
    }   
}
