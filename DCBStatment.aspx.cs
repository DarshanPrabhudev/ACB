/*-------------------Report - List of Accounts Under Execution[CBS]------------

 Version     Developed By         Date              Assigned By
 ------------------------------------------------------------------------------
  1.0      CHAITHRA.C.R          17.05.2018               VK
 
 * Stored Procedure Used - DCBSTAT.MENT
------------------------------------------------------------------------------*/
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OracleClient;
using System.Text.RegularExpressions;


public partial class DCBStatment : System.Web.UI.Page
{
    Log objLog = new Log();
    XMLMetaMasters xmlObj = new XMLMetaMasters();
    global objGlobal = new global();
    SingletonConn objDataFetch = new SingletonConn();
    OracleConnection OraConn = new OracleConnection();
    OracleCommand objCmdSave = new OracleCommand();
    DataTable dtOdList = new DataTable();
    DataTable dtLoanList = new DataTable();
    FormatRpt objFrmPt = new FormatRpt();

    protected void Page_Load(object sender, EventArgs e)
    {
        globalSession.checkSession();
        try
        {
            if (!IsPostBack)
            {
                xmlObj.FillMetaXMLDdl(ddlBranch, "Branch.xml", "BRAN_SNO", "BRAN_NAME", null);
                if (Session["BranchLogin"] != null)
                {
                    ddlBranch.SelectedValue = Session["BranchLogin"].ToString();
                }



                if (Session["gsysdate"] != null)
                {
                    txtFromDate.Text = Session["gsysdate"].ToString();
                }
                //else
                //{
                //    txtFromDate.Text = objGlobal.GetServerDate();

                //}

                if (Session["gsysdate"] != null)
                {
                    txtToDate.Text = Session["gsysdate"].ToString();
                }
                //else
                //{
                //    txtToDate.Text = objGlobal.GetServerDate();

                //}


                dtOdList = xmlObj.DataTableMetadataXML("odSchemeSetup.xml", "ODST_BRANCH=" + ddlBranch.SelectedValue + "");

                if (dtOdList.Rows.Count > 0)
                {
                    chkODScheme.DataSource = dtOdList;
                    chkODScheme.DataTextField = "ODST_NAME";
                    chkODScheme.DataValueField = "ODST_ABBR";
                    chkODScheme.DataBind();
                }
                

                dtLoanList = xmlObj.DataTableMetadataXML("LOANSCHEMESETUP.XML", "LSCH_BRANCH=" + ddlBranch.SelectedValue + " ");

                if (dtLoanList.Rows.Count > 0)
                {
                    chkLoanScheme.DataSource = dtLoanList;
                    chkLoanScheme.DataTextField = "LSCH_DES";
                    chkLoanScheme.DataValueField = "LSCH_ABBREV";
                    chkLoanScheme.DataBind();
                    rblPriOrInt.SelectedIndex = 0;
                }
                
            }
            imgxl0.Visible = false;

        }
        catch (Exception ex)
        {
            objLog.WriteLog(ex.Message);
            ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('" + ex.Message + "')</script>");
        }
        finally
        {
            objLog.WriteLog("DCBStatment.aspx-->Page_Load() Event Reached finally block without any exceptions.");
        }
    }
   
    protected void btnOK_Click(object sender, EventArgs e)
    {
        DataTable dtDCB = new DataTable();
        DataTable dtDetails = new DataTable();
        DataRow row;

        globalSession.checkSession();
        OraConn = objDataFetch.getSingletonConnectionInstance();
        objCmdSave.Connection = OraConn;

        string abbr = "", sOD = "", sLoan = "", strFrmDate = "", strToDate = "";
        int intRecount = 0;
        double dblSlNo = 0, dblTotErlierDemand = 0.00, dblTotPresentDemand = 0.00,  
            dblTotPresentCollection = 0.00, dblGTotErlierCollection = 0.00, dblTotFutureCollection = 0.00,
            dblTotErlierBalance = 0.00, dblTotPresentBalance = 0.00,
            dblgrdTotdmd = 0.00, dblgrdTotColl = 0.00, dblgrdTotBal = 0.00, dblTotErDemand = 0.00, dblTotPrDemand = 0.00,
            dblgrdTotdemand = 0.00, dblGTotErCollection = 0.00, dblTotPrCollection = 0.00, dblTotFuCollection = 0.00,
            dblgrdTotCollection = 0.00, dblTotErBalance = 0.00, dblTotPrBalance = 0.00, dblgrdTotBalance=0.00;
        DateTime temp;

        try
        {
            strFrmDate = txtFromDate.Text.ToString();
            Session["strFrmDate"] = strFrmDate;
            strToDate = txtToDate.Text;
            Session["strToDate"] = strToDate;

            if (!(DateTime.TryParse(strFrmDate, out temp)))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert ('Enter a Valid From Date') </script>");
                return;
            }
            if (!(DateTime.TryParse(strToDate, out temp)))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert ('Enter a Valid To Date') </script>");
                return;
            }

            if ((Convert.ToDateTime(strFrmDate) > DateTime.Now) || (Convert.ToDateTime(strToDate) > DateTime.Now))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert ('Enter a Valid Date') </script>");
                return;
            }
            if (Convert.ToDateTime(strFrmDate) > Convert.ToDateTime(strToDate))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert ('Check From and To Dates') </script>");
                return;
            }

            if ((chkLoanScheme.SelectedValue == "") && (chkODScheme.SelectedValue == ""))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert ('Select atleast one Scheme') </script>");
                return;
            }

            for (int c = 0; c < chkODScheme.Items.Count; c++)
            {
                if (chkODScheme.Items[c].Selected)
                {
                    abbr += chkODScheme.Items[c].Value.Trim() + ",";
                    sOD += "" + chkODScheme.Items[c].Value.Trim() + ",";
                    intRecount = intRecount + 1;
                }
            }
            objLog.WriteLog("DCBStatment sOD : " + sOD);

            for (int i = 0; i < chkLoanScheme.Items.Count; i++)
            {
                if (chkLoanScheme.Items[i].Selected)
                {
                    abbr += chkLoanScheme.Items[i].Value.Trim() + ",";
                    sLoan += "" + chkLoanScheme.Items[i].Value.Trim() + ",";
                    intRecount = intRecount + 1;
                }
            }
            objLog.WriteLog("DCBStatment sLoan : " + sLoan);


            OracleCommand cmdProcExe = new OracleCommand("DCBSTAT.MENT", OraConn);
            cmdProcExe.CommandType = System.Data.CommandType.StoredProcedure;
            cmdProcExe.Parameters.Add("V_BRANCH", OracleType.Number).Value = ddlBranch.SelectedValue;
            cmdProcExe.Parameters.Add("V_LSCHEME", OracleType.VarChar).Value = sLoan;
            cmdProcExe.Parameters.Add("V_OSCHEME", OracleType.VarChar).Value = sOD;
            cmdProcExe.Parameters.Add("V_TYPE", OracleType.VarChar).Value = rblPriOrInt.SelectedValue;
            cmdProcExe.Parameters.Add("V_FDATE", OracleType.DateTime).Value = String.Format("{0:dd-MMM-yyyy}", strFrmDate);
            cmdProcExe.Parameters.Add("V_TDATE", OracleType.DateTime).Value = String.Format("{0:dd-MMM-yyyy}", strToDate);

            cmdProcExe.Parameters.Add("V_OUT", OracleType.Cursor).Direction = System.Data.ParameterDirection.Output;

            OracleDataAdapter oDaExecution = new OracleDataAdapter(cmdProcExe);
            oDaExecution.Fill(dtDCB);

            if (dtDCB.Rows.Count <= 0)
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert ('There is No Criteria') </script>");
                return;
            }

            //objLog.WriteLog("DCBStatement - V_BRANCH INPUT to Stored Procedure " + ddlBranch.SelectedValue);
            //objLog.WriteLog("DCBStatement - V_LSCHEME INPUT to Stored Procedure " + sOD);
            //objLog.WriteLog("DCBStatement - V_OSCHEME INPUT to Stored Procedure " + sLoan);
            //objLog.WriteLog("DCBStatement - V_TYPE INPUT to Stored Procedure " + rblPriOrInt.SelectedValue);
            //objLog.WriteLog("DCBStatement - V_FDATE INPUT to Stored Procedure " + txtFromDate.Text);
            //objLog.WriteLog("DCBStatement - V_TDATE INPUT to Stored Procedure " + txtToDate.Text);

            //objLog.WriteLog("DCBStatment - dtDCB(Row Count)" + dtDCB.Rows.Count);

             objLog.WriteLog("DCBStatement - V_BRANCH:" + ddlBranch.SelectedValue+ "V_LSCHEME :"+ sOD + "V_OSCHEME:" + sLoan +
                 "V_TYPE:"+rblPriOrInt.SelectedValue+ "V_FDATE:"+txtFromDate.Text+"V_TDATE:"+txtToDate.Text);

            if (dtDCB.Rows.Count > 0)
            {
                dtDetails.Columns.Add(new DataColumn("SlNo", typeof(string)));
                dtDetails.Columns.Add(new DataColumn("LoanNo", typeof(string)));
                dtDetails.Columns.Add(new DataColumn("Name", typeof(string)));
                dtDetails.Columns.Add(new DataColumn("Arr Dmd", typeof(string)));
                dtDetails.Columns.Add(new DataColumn("Crnt Dmd", typeof(string)));
                dtDetails.Columns.Add(new DataColumn("Tot Dmd", typeof(string)));
                dtDetails.Columns.Add(new DataColumn("Arr Coln", typeof(string)));
                dtDetails.Columns.Add(new DataColumn("Crnt Coln", typeof(string)));
                dtDetails.Columns.Add(new DataColumn("Future Coln", typeof(string)));
                dtDetails.Columns.Add(new DataColumn("Tot Coln", typeof(string)));
                dtDetails.Columns.Add(new DataColumn("Arr Bal", typeof(string)));
                dtDetails.Columns.Add(new DataColumn("Cur Bal", typeof(string)));
                dtDetails.Columns.Add(new DataColumn("Tot Bal", typeof(string)));
                dtDetails.Columns.Add(new DataColumn("BrName", typeof(string)));
            }
            foreach (DataRow dr in dtDCB.Rows)
            {
                row = dtDetails.NewRow();
                dblSlNo = dblSlNo + 1;
                row["SlNo"] = dblSlNo;

                if ((!DBNull.Value.Equals(dr["C_AccNo"])) || (!DBNull.Value.Equals(dr["C_AccNo"])))
                    row["LoanNo"] = dr["C_ABBR"] + " " + dr["C_AccNo"];

                if (!DBNull.Value.Equals(dr["C_NAME"]))
                    row["Name"] = dr["C_NAME"];

                if (!DBNull.Value.Equals(dr["C_ERDEM"]))
                {
                    row["Arr Dmd"] = String.Format("{0:0.00}", dr["C_ERDEM"]);
                    dblTotErlierDemand = Convert.ToDouble(dr["C_ERDEM"]);
                    dblTotErDemand += dblTotErlierDemand;
                }

                if (!DBNull.Value.Equals(dr["C_PRDEM"]))
                {
                    row["Crnt Dmd"] = String.Format("{0:0.00}", dr["C_PRDEM"]);
                    dblTotPresentDemand = Convert.ToDouble(dr["C_PRDEM"]);
                    dblTotPrDemand += dblTotPresentDemand;
                }

                row["Tot Dmd"] = String.Format("{0:0.00}", dblTotErlierDemand + dblTotPresentDemand);
                dblgrdTotdmd = Convert.ToDouble(row["Tot Dmd"]);
                dblgrdTotdemand += dblgrdTotdmd;


                if (!DBNull.Value.Equals(dr["C_ERCOL"]))
                {
                    row["Arr Coln"] = String.Format("{0:0.00}", dr["C_ERCOL"]);
                    dblGTotErlierCollection = Convert.ToDouble(dr["C_ERCOL"]);
                    dblGTotErCollection += dblGTotErlierCollection;
                }
                if (!DBNull.Value.Equals(dr["C_PRCOL"]))
                {
                    row["Crnt Coln"] = String.Format("{0:0.00}", dr["C_PRCOL"]);
                    dblTotPresentCollection = Convert.ToDouble(dr["C_PRCOL"]);
                    dblTotPrCollection += dblTotPresentCollection;
                }
                if (!DBNull.Value.Equals(dr["C_ADVCOL"]))
                {
                    row["Future Coln"] = String.Format("{0:0.00}", dr["C_ADVCOL"]);
                    dblTotFutureCollection = Convert.ToDouble(dr["C_ADVCOL"]);
                    dblTotFuCollection += dblTotFutureCollection;
                }

                row["Tot Coln"] = String.Format("{0:0.00}", dblGTotErlierCollection + dblTotPresentCollection + dblTotFutureCollection);
                dblgrdTotColl = Convert.ToDouble(row["Tot Coln"]);
                dblgrdTotCollection += dblgrdTotColl;

                if (!DBNull.Value.Equals(dr["C_ERBAL"]))
                {
                    row["Arr Bal"] = String.Format("{0:0.00}", dr["C_ERBAL"]);
                    dblTotErlierBalance = Convert.ToDouble(dr["C_ERBAL"]);
                    dblTotErBalance += dblTotErlierBalance;
                }
                if (!DBNull.Value.Equals(dr["C_PRBAL"]))
                {
                    row["Cur Bal"] = String.Format("{0:0.00}", dr["C_PRBAL"]);
                    dblTotPresentBalance = Convert.ToDouble(dr["C_PRBAL"]);
                    dblTotPrBalance += dblTotPresentBalance;
                }

                row["Tot Bal"] = String.Format("{0:0.00}", dblTotErlierBalance + dblTotPresentBalance);
                dblgrdTotBal = Convert.ToDouble(row["Tot Bal"]);
                dblgrdTotBalance += dblgrdTotBal;

                if (!DBNull.Value.Equals(dr["C_BNO"]))
                {
                    row["BrName"] = dr["C_BNO"];

                    //DataTable dtBranch = new DataTable();
                    //string strSql = "", strbrnchname = "";
                    //strSql = "  select * from branch where bran_sno= " + dr["C_BNO"];
                    //dtBranch = objDataFetch.DatatablePassSQL(strSql);
                    //if (dtBranch.Rows.Count > 0)
                    //{
                    //    strbrnchname = dtBranch.Rows[0]["BRAN_NAME"].ToString();
                    //}
                    //row["BrName"] = strbrnchname;
                }


                dtDetails.Rows.Add(row);
            }

            row = dtDetails.NewRow();
            row["Name"] = "GRAND TOTAL";
            row["Arr Dmd"] = (String.Format("{0:0.00}", dblTotErDemand));
            row["Crnt Dmd"] = (String.Format("{0:0.00}", dblTotPrDemand));
            row["Tot Dmd"] = (String.Format("{0:0.00}", dblgrdTotdemand));
            row["Arr Coln"] = (String.Format("{0:0.00}", dblGTotErCollection));
            row["Crnt Coln"] = (String.Format("{0:0.00}", dblTotPrCollection));
            row["Future Coln"] = (String.Format("{0:0.00}", dblTotFuCollection));
            row["Tot Coln"] = (String.Format("{0:0.00}", dblgrdTotCollection));
            row["Arr Bal"] = (String.Format("{0:0.00}", dblTotErBalance));
            row["Cur Bal"] = (String.Format("{0:0.00}", dblTotPrBalance));
            row["Tot Bal"] = (String.Format("{0:0.00}", dblgrdTotBalance));

            dtDetails.Rows.Add(row);

            lblBank.Visible = true;
            lblBranch.Visible = true;
            lblHeading.Visible = true;
            lblBank.Text = global.BankName();
            lblBranch.Text = Session["LoginBranchName"].ToString().Trim();
            lblHeading.Text = "DCB STATEMENT REPORT[CBS] From " + txtFromDate.Text + " TO " + txtToDate.Text + "";

            gvDCB.DataSource = dtDetails;
            gvDCB.DataBind();
            Session["dtDetails"] = dtDetails;

            objFrmPt.FormatGridView(gvDCB);
            GridPrint(gvDCB);

            ViewState["Pagename"] = "DCB STATEMENT REPORT";

            imgxl0.Visible = true;
        }

        catch (Exception ex)
        {
            objLog.WriteLog(ex.Message);
            ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert('" + ex.Message + "')</script>");
        }
        finally
        {
            objLog.WriteLog("DCBStatment.aspx-->btnOK_Click() Event Reached finally block without any exceptions.");
            dtDCB.Dispose();
            dtDetails.Dispose();
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("rmenu.aspx");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("DCBStatment.aspx");
    }
    protected void btnSelect_Click(object sender, EventArgs e)
    {
        foreach (ListItem li in chkODScheme.Items)
        {
            li.Selected = true;
        }
        foreach (ListItem li in chkLoanScheme.Items)
        {
            li.Selected = true;
        }
    }
    protected void gvDCB_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void gvDCB_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[11].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[12].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[13].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[0].Wrap = false;
            e.Row.Cells[1].Wrap = false;
            e.Row.Cells[2].Wrap = false;
            e.Row.Cells[3].Wrap = false;
            e.Row.Cells[4].Wrap = false;
            e.Row.Cells[5].Wrap = false;
            e.Row.Cells[6].Wrap = false;
            e.Row.Cells[7].Wrap = false;
            e.Row.Cells[8].Wrap = false;
            e.Row.Cells[9].Wrap = false;
            e.Row.Cells[10].Wrap = false;
            e.Row.Cells[11].Wrap = false;
            e.Row.Cells[12].Wrap = false;
            e.Row.Cells[13].Wrap = false;
            //e.Row.Cells[13].Font.Bold = true ;
        }
        if (e.Row.Cells[2].Text == "GRAND TOTAL")
        {
            e.Row.Cells[2].Font.Bold = true;
            e.Row.Cells[2].ForeColor = System.Drawing.Color.Black;
            e.Row.Cells[3].Font.Bold = true;
            e.Row.Cells[3].ForeColor = System.Drawing.Color.Black;
            e.Row.Cells[4].Font.Bold = true;
            e.Row.Cells[4].ForeColor = System.Drawing.Color.Black;
            e.Row.Cells[5].Font.Bold = true;
            e.Row.Cells[5].ForeColor = System.Drawing.Color.Black;
            e.Row.Cells[6].Font.Bold = true;
            e.Row.Cells[6].ForeColor = System.Drawing.Color.Black;
            e.Row.Cells[7].Font.Bold = true;
            e.Row.Cells[7].ForeColor = System.Drawing.Color.Black;
            e.Row.Cells[8].Font.Bold = true;
            e.Row.Cells[8].ForeColor = System.Drawing.Color.Black;
            e.Row.Cells[9].Font.Bold = true;
            e.Row.Cells[9].ForeColor = System.Drawing.Color.Black;
            e.Row.Cells[10].Font.Bold = true;
            e.Row.Cells[10].ForeColor = System.Drawing.Color.Black;
            e.Row.Cells[11].Font.Bold = true;
            e.Row.Cells[11].ForeColor = System.Drawing.Color.Black;
            e.Row.Cells[12].Font.Bold = true;
            e.Row.Cells[12].ForeColor = System.Drawing.Color.Black;
        }
           }

    protected void imgxl_Click(object sender, ImageClickEventArgs e)
    {
        string dTimeFDate = "", dTimeTDate = "";
        try
        {
            if (Session["dtDetails"] != null)
            {
                lblBank.Text = global.BankName();
                lblBranch.Text = Session["LoginBranchName"].ToString().Trim();
                dTimeFDate = Convert.ToString(Session["strFrmDate"]);
                dTimeTDate = Convert.ToString(Session["strToDate"]);
                lblHeading.Text = "DCB STATEMENT REPORT[CBS] From  " + String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(dTimeFDate)) + " to " + String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(dTimeTDate));
                gvDCB.DataSource = (DataTable)Session["dtDetails"];
            }

            if (gvDCB.DataSource != null)
            {
                gvDCB.UseAccessibleHeader = true;
                gvDCB.HeaderRow.TableSection = TableRowSection.TableHeader;
                Response.ClearContent();
                Response.AddHeader("content-disposition", "attachment; filename='" + ViewState["Pagename"].ToString() + "'.xls");
                Response.ContentType = "application/excel";
                System.IO.StringWriter sw = new System.IO.StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                pnlGrid.RenderControl(htw);
                Response.Write("<style> .cost{mso-number-format:\"\\#\\#0\\.00\";} </style>");
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('There is no data to Export to Excel.')</script>");
            }
        }
        catch (Exception ex)
        {
            string g = ex.Message;
            g = Regex.Replace(g, "[^a-zA-Z0-9_]+", " ");
            objLog.WriteLog("DCBStatment.aspx--->Exception in imgxl_Click()  Event-->" + g);
            ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('" + g + "')</script>");
            return;
        }
        finally
        {
            objLog.WriteLog("DCBStatment.aspx--->imgxl_Click() Event completed Execution without any exception or errors");
        }
    }

    public void GridPrint(GridView grd)
    {
        if (grd.Rows.Count > 0)
        {
            grd.UseAccessibleHeader = true;
            grd.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        //return;
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtList = new DataTable();
        dtList.Clear();
        dtList = xmlObj.DataTableMetadataXML("ODSCHEMESETUP.xml", "ODST_BRANCH=" + ddlBranch.SelectedValue);
        if (dtList.Rows.Count <= 0)
        {
            ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert ('No OD Scheme Found...') </script>");
            return;
        }
        chkODScheme.DataSource = dtList;
        chkODScheme.DataTextField = "ODST_NAME";
        chkODScheme.DataValueField = "ODST_ABBR";
        chkODScheme.DataBind();

        dtList.Clear();
        dtList = xmlObj.DataTableMetadataXML("LOANSCHEMESETUP.xml", "LSCH_BRANCH=" + ddlBranch.SelectedValue);
        if (dtList.Rows.Count <= 0)
        {
            ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert ('No Loan Scheme Found...') </script>");
            return;
        }
        chkLoanScheme.DataSource = dtList;
        chkLoanScheme.DataTextField = "LSCH_DES";
        chkLoanScheme.DataValueField = "LSCH_ABBREV";
        chkLoanScheme.DataBind();
    }
}