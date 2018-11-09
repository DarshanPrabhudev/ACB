/*-------------------------------DAILY EXCEPTIONAL REPORT----------------------------------
//* Version                   Created By                      Date                   INTIAL DRAFT
//* 1.0                         Roopa KV                   04-JUN-2018               LS Madam

//NAME OF THE CLIENT:  General Client
//DATABASE USED FOR TESTING : ACBCBS

//* -----------------------------PSEUDOCODE--------------------------------
 * This report is used to check
*1. Any Account opened in Amount range ( >= 100000).
*2. Cash Transactions with out PAN Numbers ( >= 50000).
*3. Higher Range of Amount disbursed as Loan/OD.
*4. Transactions done for Inoperative accounts ( 2 years).
*5. Debit Balance with Deposit Accounts - Credit Balance with Loans/OD.
//* ***********************************TABLES USED*************************************
//*1.BRANCH
//* ***********************************PROCEDURES USED*************************************
 * HEAD.DAILY_EXCEPTION_CONSOLIDATED
 */

using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OracleClient;
using System.Text.RegularExpressions;

public partial class DailyExcepRep : System.Web.UI.Page
{
    XMLMetaMasters objXml = new XMLMetaMasters();
    global objGlobal = new global();
    Log objLog = new Log();
    FormatRpt objFrmPt = new FormatRpt();
    SingletonConn objDataFetch = new SingletonConn();

    public override void VerifyRenderingInServerForm(Control control)
    {
        //return;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        globalSession.checkSession();
        if (!IsPostBack)
        {
            DataTable dtScheme = new DataTable();
            try
            {
                objXml.FillMetaXMLDdl(ddlBranch, "BRANCH.xml", "bran_sno", "bran_name", "");
                if (Session["sysdate"] != null)
                {
                    txtAsonDate.Text = Session["sysdate"].ToString();
                }
                else
                {
                    txtAsonDate.Text = objGlobal.GetServerDate();
                }
                if (Session["BranchLogin"] != null)
                {
                    ddlBranch.SelectedValue = Session["BranchLogin"].ToString();
                }

                imgxl0.Visible = false;
            }

            catch (Exception ex)
            {
                string g = ex.Message + ex;
                g = Regex.Replace(g, "[^a-zA-Z0-9_]+", " ");
                objLog.WriteLog("DailyExcepRep.aspx-->Exception in Page_Load() Event" + g);
                ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert('" + g + "')</script>");
                return;
            }
            finally
            {
                objLog.WriteLog("DailyExcepRep.aspx-->Page_Load() Event Reached finally block without any exceptions.");
                dtScheme.Dispose();
            }
        }
    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        int Slno = 1, namecnt = 1, nCnt = 1, Count = 0;
        double dblTotAmt = 0.00, dblAmt = 0.00, dblcdAmt = 0.00,dblTotcdAmt=0.00 ;
        string strDesc = "";
        DataTable dtDetails = new DataTable();
        DataTable dtDailyExp = new DataTable();
        DateTime temp;
        DataRow grdmn1;
        OracleConnection oraConn;
        oraConn = objDataFetch.getSingletonConnectionInstance();
        OracleCommand cmd = new OracleCommand();
        try
        {
            if (!(DateTime.TryParse(txtAsonDate.Text, out temp)))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert ('Enter a Valid Date.') </script>");
                return;
            }

            //if (txtNoOfYears.Text == string.Empty)
            //{
            //    txtNoOfYears.Text = "0";
            //}

            //cmd.Parameters.Clear();
            //cmd.Connection = oraConn;
            //cmd.CommandType = System.Data.CommandType.StoredProcedure;
            //cmd.CommandText = "HEAD.DAILY_EXCEPTION";
            //cmd.Parameters.Add(new OracleParameter("d_asondate", OracleType.DateTime)).Value = String.Format("{0:dd/MM/yyyy}", txtAsonDate.Text);
            //if (rblBrnConsol.SelectedValue.ToUpper().Trim() == "C")
            //{
            //    cmd.Parameters.Add(new OracleParameter("d_branch", OracleType.Number)).Value = DBNull.Value;
            //}
            //else
            //{
            //    cmd.Parameters.Add(new OracleParameter("d_branch", OracleType.Number)).Value =Convert.ToInt16(ddlBranch.SelectedValue);
            //}
            //cmd.Parameters.Add(new OracleParameter("d_detorsum", OracleType.VarChar)).Value ="'"+rblSumDet.SelectedItem.Text.Trim()+"'";
            //cmd.Parameters.Add(new OracleParameter("c_out", OracleType.Cursor)).Direction = ParameterDirection.Output;
            //OracleDataAdapter adp = new OracleDataAdapter(cmd);
            //adp.Fill(dtDetails);



            cmd.Parameters.Clear();
            cmd.Connection = oraConn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "HEAD.DAILY_EXCEPTION_CONSOLIDATED";
            cmd.Parameters.Add("d_asondate", OracleType.DateTime).Value = String.Format("{0:dd-MMM-yyyy}", txtAsonDate.Text);
            //if (rblBrnConsol.SelectedValue.ToUpper().Trim() == "C")
            //{
            //    cmd.Parameters.Add("d_branch", OracleType.Number).Value = DBNull.Value;
            //}
            //else
            //{
                cmd.Parameters.Add("d_branch", OracleType.Number).Value = Convert.ToInt16(ddlBranch.SelectedValue);
           // }

            cmd.Parameters.Add("d_detorsum", OracleType.VarChar).Value = rblSumDet.SelectedItem.Text.Trim().ToUpper();

            cmd.Parameters.Add("c_out", OracleType.Cursor).Direction = System.Data.ParameterDirection.Output;
            OracleDataAdapter adp = new OracleDataAdapter(cmd);
            adp.Fill(dtDetails);


            if (dtDetails.Rows.Count > 0)
            {
                if (rblSumDet.SelectedValue == "S")
                {
                    dtDailyExp.Columns.Add(new DataColumn("SlNo", typeof(string)));
                    dtDailyExp.Columns.Add(new DataColumn("No of Accounts", typeof(string)));
                    dtDailyExp.Columns.Add(new DataColumn("Description", typeof(string)));

                    foreach (DataRow rgrd1 in dtDetails.Rows)
                    {
                        grdmn1 = dtDailyExp.NewRow();
                        grdmn1["SlNo"] = Slno;

                        if (!DBNull.Value.Equals(rgrd1["NOACCOUNTS"]))
                        {
                            grdmn1["No of Accounts"] = rgrd1["NOACCOUNTS"].ToString();
                        }

                        if (!DBNull.Value.Equals(rgrd1["D_DESCRIPTION"]))
                        {
                            grdmn1["Description"] = rgrd1["D_DESCRIPTION"].ToString();
                        }
                        dtDailyExp.Rows.Add(grdmn1);
                        Slno++;
                    }

                }
                else
                {
                    dtDailyExp.Columns.Add(new DataColumn("SlNo", typeof(string)));
                    dtDailyExp.Columns.Add(new DataColumn("Name", typeof(string)));
                    dtDailyExp.Columns.Add(new DataColumn("PAN No", typeof(string)));
                    dtDailyExp.Columns.Add(new DataColumn("A/c No", typeof(string)));
                    dtDailyExp.Columns.Add(new DataColumn("Branch", typeof(string)));
                    dtDailyExp.Columns.Add(new DataColumn("Amount", typeof(string)));
                    dtDailyExp.Columns.Add(new DataColumn("Debit/CreditAmt", typeof(string)));
                    dtDailyExp.Columns.Add(new DataColumn("Debit/Credit", typeof(string)));

                   // dtDailyExp.Columns.Add(new DataColumn("Description", typeof(string)));

                    foreach (DataRow rgrd1 in dtDetails.Rows)
                    {
                        if ((strDesc != string.Empty) && (strDesc != rgrd1["D_DESCRIPTION"].ToString()))
                        {
                            grdmn1 = dtDailyExp.NewRow();
                            grdmn1["PAN No"] = "Total no. of Accounts";
                            grdmn1["A/c No"] = Count;
                            grdmn1["Branch"] = "TOTAL";
                            grdmn1["Amount"] = String.Format("{0:0.00}", dblAmt);
                            grdmn1["Debit/CreditAmt"] = String.Format("{0:0.00}", dblcdAmt);
                            dblTotAmt += dblAmt;
                            dblTotcdAmt += dblcdAmt;
                            dtDailyExp.Rows.Add(grdmn1);
                            dblAmt = 0.00;
                            dblcdAmt = 0.00;
                            Count = 0;
                            //Slno++;
                            namecnt = 1;

                            grdmn1 = dtDailyExp.NewRow();
                            dtDailyExp.Rows.Add(grdmn1);

                        }

                        if (namecnt == 1)
                        {
                            grdmn1 = dtDailyExp.NewRow();
                            if (!DBNull.Value.Equals(rgrd1["D_DESCRIPTION"]))
                            {
                                grdmn1["Name"] = rgrd1["D_DESCRIPTION"].ToString();
                            }
                            dtDailyExp.Rows.Add(grdmn1);

                            grdmn1 = dtDailyExp.NewRow();

                            grdmn1["SlNo"] = Slno;
                            Count++;
                            if (!DBNull.Value.Equals(rgrd1["D_CNAME"]))
                            {
                                grdmn1["Name"] = rgrd1["D_CNAME"].ToString();
                            }

                            if (!DBNull.Value.Equals(rgrd1["D_PAMNO"]))
                            {
                                grdmn1["PAN No"] = rgrd1["D_PAMNO"].ToString();
                            }

                            if (!DBNull.Value.Equals(rgrd1["D_ABBR"]))
                            {
                                grdmn1["A/c No"] = rgrd1["D_ABBR"].ToString() + "-" + rgrd1["D_ACNO"];
                            }

                            if (!DBNull.Value.Equals(rgrd1["D_BRANCH"]))
                            {
                                grdmn1["Branch"] = rgrd1["D_BRANCH"].ToString();
                            }

                            if (!DBNull.Value.Equals(rgrd1["D_AMOUNT"]))
                            {
                                grdmn1["Amount"] = String.Format("{0:0.00}", rgrd1["D_AMOUNT"]);
                                dblAmt = dblAmt + Convert.ToDouble(rgrd1["D_AMOUNT"]);

                                //dblTotAmt = dblTotAmt + Convert.ToDouble(rgrd1["AMOUNT"]);
                            }

                            if (!DBNull.Value.Equals(rgrd1["D_DEBITORCREDITAMOUNT"]))
                            {
                                grdmn1["Debit/CreditAmt"] = String.Format("{0:0.00}", rgrd1["D_DEBITORCREDITAMOUNT"]);
                                dblcdAmt = dblcdAmt + Convert.ToDouble(rgrd1["D_DEBITORCREDITAMOUNT"]);
                            }

                            if (!DBNull.Value.Equals(rgrd1["D_DRCR"]))
                            {
                                grdmn1["Debit/Credit"] = String.Format("{0:0.00}", rgrd1["D_DRCR"]);
                            }


                            //if (!DBNull.Value.Equals(rgrd1["D_DESCRIPTION"]))
                            //{
                            //    grdmn1["Description"] = rgrd1["D_DESCRIPTION"].ToString();
                            //}

                            dtDailyExp.Rows.Add(grdmn1);
                        }
                        else if ((strDesc != string.Empty) && (strDesc == rgrd1["D_DESCRIPTION"].ToString()))
                        {
                            grdmn1 = dtDailyExp.NewRow();

                            grdmn1["SlNo"] = Slno;
                            Count++;
                            if (!DBNull.Value.Equals(rgrd1["D_CNAME"]))
                            {
                                grdmn1["Name"] = rgrd1["D_CNAME"].ToString();
                            }

                            if (!DBNull.Value.Equals(rgrd1["D_PAMNO"]))
                            {
                                grdmn1["PAN No"] = rgrd1["D_PAMNO"].ToString();
                            }

                            if (!DBNull.Value.Equals(rgrd1["D_ABBR"]))
                            {
                                grdmn1["A/c No"] = rgrd1["D_ABBR"].ToString() + "-" + rgrd1["D_ACNO"];
                            }

                            if (!DBNull.Value.Equals(rgrd1["D_BRANCH"]))
                            {
                                grdmn1["Branch"] = rgrd1["D_BRANCH"].ToString();
                            }

                            if (!DBNull.Value.Equals(rgrd1["D_AMOUNT"]))
                            {
                                grdmn1["Amount"] = String.Format("{0:0.00}", rgrd1["D_AMOUNT"]);
                               // dblTotAmt = dblTotAmt + Convert.ToDouble(rgrd1["AMOUNT"]);
                                dblAmt = dblAmt + Convert.ToDouble(rgrd1["D_AMOUNT"]);
                            }

                            if (!DBNull.Value.Equals(rgrd1["D_DEBITORCREDITAMOUNT"]))
                            {
                                grdmn1["Debit/CreditAmt"] = String.Format("{0:0.00}", rgrd1["D_DEBITORCREDITAMOUNT"]);
                                dblcdAmt = dblcdAmt + Convert.ToDouble(rgrd1["D_DEBITORCREDITAMOUNT"]);
                            }

                            if (!DBNull.Value.Equals(rgrd1["D_DRCR"]))
                            {
                                grdmn1["Debit/Credit"] = String.Format("{0:0.00}", rgrd1["D_DRCR"]);
                            }

                            //if (!DBNull.Value.Equals(rgrd1["D_DESCRIPTION"]))
                            //{
                            //    grdmn1["Description"] = rgrd1["D_DESCRIPTION"].ToString();
                            //}

                            dtDailyExp.Rows.Add(grdmn1);
                        }

                        if (!DBNull.Value.Equals(rgrd1["D_DESCRIPTION"]))
                        {
                            strDesc = rgrd1["D_DESCRIPTION"].ToString();
                        }
                        Slno++;
                        namecnt++;
                      //  nCnt++;
                        //if (!DBNull.Value.Equals(dtPosting.Rows[i]["ClientNo"]))
                        //{
                        //    strClient = dtPosting.Rows[i]["ClientNo"].ToString();
                        //}

                    }

                    grdmn1 = dtDailyExp.NewRow();
                    grdmn1["PAN No"] = "Total no. of Accounts";
                    grdmn1["A/c No"] = Count;
                    grdmn1["Branch"] = "TOTAL";
                    grdmn1["Amount"] = String.Format("{0:0.00}", dblAmt);
                    grdmn1["Debit/CreditAmt"] = String.Format("{0:0.00}", dblcdAmt);
                    dtDailyExp.Rows.Add(grdmn1);
                    dblTotAmt += dblAmt;
                    dblTotcdAmt += dblcdAmt;
                    Count = 0;
                    dblcdAmt = 0.00;

                    grdmn1 = dtDailyExp.NewRow();
                    dtDailyExp.Rows.Add(grdmn1);

                    grdmn1 = dtDailyExp.NewRow();
                    grdmn1["Branch"] = "GRAND TOTAL";
                    grdmn1["Amount"] = String.Format("{0:0.00}", dblTotAmt);
                    grdmn1["Debit/CreditAmt"] = String.Format("{0:0.00}", dblTotcdAmt);
                    dtDailyExp.Rows.Add(grdmn1);
                }
                
                pnlGrid.Visible = true;
                lblBank.Visible = true;
                lblBranch.Visible = true;
                lblHeading.Visible = true;
                lblBank.Text = global.BankName();
                lblBranch.Text = Session["LoginBranchName"].ToString().Trim();
                lblHeading.Text = "DAILY EXCEPTIONAL REPORT AS ON " + txtAsonDate.Text + "";
                gvDetails.DataSource = dtDailyExp;
                gvDetails.DataBind();
                Session["dtDetails"] = dtDailyExp;

                //objFrmPt.FormatGridView(gvDetails);
                //GridPrint(gvDetails);

                ViewState["Pagename"] = "DAILYEXCEPTIONALREPORT";
                imgxl0.Visible = true;
            }
            else
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert('Data Not Found.')</script>");
                imgxl0.Visible = false;
                pnlGrid.Visible = false;
                return;
            }
        }
        catch (Exception ex)
        {
            string g = ex.Message + ex;
            g = Regex.Replace(g, "[^a-zA-Z0-9_]+", " ");
            objLog.WriteLog("DailyExcepRep.aspx-->Exception in btnOK_Click() Event" + g);
            ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert('" + g + "')</script>");
            return;
        }
        finally
        {
            objLog.WriteLog("DailyExcepRep.aspx-->btnOK_Click() Event Reached finally block without any exceptions.");
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
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("DailyExcepRep.aspx");
    }
    protected void imgxl_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Session["dtDetails"] != null)
            {
                lblBank.Text = global.BankName();
                lblBranch.Text = Session["LoginBranchName"].ToString().Trim();
                lblHeading.Text = "DAILY EXCEPTIONAL REPORT AS ON " + txtAsonDate.Text + "";
                gvDetails.DataSource = (DataTable)Session["dtDetails"];
                if (gvDetails.DataSource != null)
                {
                    gvDetails.UseAccessibleHeader = true;
                    gvDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
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
            }
        }
        catch (Exception ex)
        {
            string g = ex.Message + ex;
            g = Regex.Replace(g, "[^a-zA-Z0-9_]+", " ");
            objLog.WriteLog("DailyExcepRep.aspx-->Exception in btnOK_Click() Event" + g);
            ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert('" + g + "')</script>");
            return;
        }
        finally
        {
            objLog.WriteLog("DailyExcepRep.aspx-->btnOK_Click() Event Reached finally block without any exceptions.");
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("rmenu.aspx");
    }
  
    //protected void rblBrnConsol_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (rblBrnConsol.SelectedValue == "B")
    //    {
    //        ddlBranch.Enabled = true;
    //    }
    //    else
    //    {
    //        ddlBranch.Enabled = false;
    //    }
    //}
    protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (rblSumDet.SelectedValue == "D")
            {
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;

                if (e.Row.Cells[4].Text == "TOTAL") 
                {
                    e.Row.Cells[4].Font.Bold = true;
                    e.Row.Cells[4].ForeColor = System.Drawing.Color.Black;
                    e.Row.Cells[5].Font.Bold = true;
                    e.Row.Cells[5].ForeColor = System.Drawing.Color.Black;
                    e.Row.Cells[6].Font.Bold = true;
                    e.Row.Cells[6].ForeColor = System.Drawing.Color.Black;
                }

                if (e.Row.Cells[4].Text == "GRAND TOTAL")
                {
                    e.Row.Cells[4].Font.Bold = true;
                    e.Row.Cells[4].ForeColor = System.Drawing.Color.Black;
                    e.Row.Cells[5].Font.Bold = true;
                    e.Row.Cells[5].ForeColor = System.Drawing.Color.Black;
                    e.Row.Cells[6].Font.Bold = true;
                    e.Row.Cells[6].ForeColor = System.Drawing.Color.Black;
                }


                if (e.Row.Cells[2].Text == "Total no. of Accounts") 
                {
                    e.Row.Cells[2].Font.Bold = true;
                    e.Row.Cells[2].ForeColor = System.Drawing.Color.Black;
                    e.Row.Cells[3].Font.Bold = true;
                    e.Row.Cells[3].ForeColor = System.Drawing.Color.Black;
                }
            }
        }
    }
}