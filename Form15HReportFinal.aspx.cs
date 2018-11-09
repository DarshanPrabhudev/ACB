//**---------------------------FORM15H REPORT PRINTING---------------------------
/*
 * Version    Created By         Date       Modified date  Assigned By
 * 1.1        Harshith        30.04.2015      13.05.2015    Venkatesh 
 * -----------------------------------------------------------------------
 * -----------------------------PSEUDOCODE--------------------------------
 
 * * Purpose of this webform is to print the Form15H report for selected client .
 * 
 * ----------Conditions to Apply Form15H--------------
 * Client is having Age>=60,he must having the PAN No and it is marked as Form15H.
 * 
**-----------TABLES USED----------------
 *BANKINFO
 *CLIENTMASTER
 * 
 * */
using System;
using DAL;
using System.Data;
using System.Data.OracleClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class Form15HReportFinal : System.Web.UI.Page
{
    Log objLog = new Log();
    DAL.DataFetch objDataFetch = new DataFetch();
    global objGlb = new global();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            global.checkSession();
            if (!IsPostBack)
            {
                if (Session["constring"] == null)
                {
                    Response.Redirect("ShowMessage.aspx?msg=" + GlobalConstants.msg);
                }
                if (Session["dtForm15Det"] != null)
                {
                    ViewState["Pagename"] = "Form15HReport";
                    fillValue();
                }
            }
        }
        catch (Exception ex)
        {
            objLog.WriteLog("Form15ReportFinal ->Page_Load(): Unable to load...." + ex.Message);
            ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert('Error in Page Loading: " + ex.Message + "')</script>");
        }
        objLog.WriteLog("Form15ReportFinal:Page Loaded Successfully");
    }
    protected void fillValue()
    {
        DateTime dTimeFromDate, dTimeToDate, dtTimeFdate, dTimeTDate;
        DataTable dtForm15Det = new DataTable();        
        DataTable dtgrdsmmry = new DataTable();
        DataTable dtBankDet = new DataTable();
        double dblPayable = 0.00, dblBal = 0.00, dblfyr = 0, dbltyr = 0, dblFinanyr = 0;
        try
        {
            if (Session["dtForm15Det"] != null)
            {
                dtForm15Det = (DataTable)Session["dtForm15Det"];
            }
            if (dtForm15Det.Rows.Count > 0)
            {
                getBankDet();
                dtBankDet=(DataTable) Session["dtBankDet"];
                dtgrdsmmry.Columns.Add("Name and address of the person to whom the are given on interest", typeof(string));
                dtgrdsmmry.Columns.Add("Account No", typeof(string));
                dtgrdsmmry.Columns.Add("Amount of sums given on interest", typeof(string));
                dtgrdsmmry.Columns.Add("Date on which sums given on Interest (dd/mm/yyyy)", typeof(string));
                dtgrdsmmry.Columns.Add("Period for which sums were given on interest", typeof(string));
                dtgrdsmmry.Columns.Add("Rate of interest", typeof(string));
                DataRow grdmn1 = dtgrdsmmry.NewRow();
                grdmn1["Name and address of the person to whom the are given on interest"] = dtBankDet.Rows[0]["BAN_NAME"].ToString();
                grdmn1["Account No"] = "";
                grdmn1["Amount of sums given on interest"] = "";
                grdmn1["Date on which sums given on Interest (dd/mm/yyyy)"] = "";
                grdmn1["Period for which sums were given on interest"] = "";
                grdmn1["Rate of interest"] = "";
                dtgrdsmmry.Rows.Add(grdmn1);
                foreach (DataRow rgrd1 in dtForm15Det.Rows)
                {
                    if (Convert.ToDouble(rgrd1["N_BAL"]) > 0 && Convert.ToDouble(rgrd1["N_PAYABLE"]) > 0)
                    {
                        if (rgrd1["FORM_TYPE"].ToString().Trim() == "0" || rgrd1["FORM_TYPE"].ToString().Trim() == "F15H")
                        {
                            grdmn1 = dtgrdsmmry.NewRow();
                            grdmn1["Name and address of the person to whom the are given on interest"] = "";
                            grdmn1["Account No"] = rgrd1["N_ABBR"] + "-" + rgrd1["N_ACNO"] + "(" + rgrd1["N_BRANCH"] + ")";
                            grdmn1["Amount of sums given on interest"] = String.Format("{0:0.00}", rgrd1["N_BAL"]);
                            grdmn1["Date on which sums given on Interest (dd/mm/yyyy)"] = String.Format("{0:dd/MM/yyyy}", rgrd1["N_TOD"]).ToString();
                            grdmn1["Period for which sums were given on interest"] = String.Format("{0:dd/MM/yyyy}", rgrd1["N_FROMD"]).ToString() + " - " + String.Format("{0:dd/MM/yyyy}", rgrd1["N_TOD"]).ToString();
                            grdmn1["Rate of interest"] = rgrd1["N_ROI"] + " %";
                            dblBal = dblBal + Convert.ToDouble(rgrd1["N_BAL"]);
                            dblPayable = dblPayable + Convert.ToDouble(rgrd1["N_PAYABLE"]);
                            dtgrdsmmry.Rows.Add(grdmn1);
                        }

                    }
                }
                grdmn1 = dtgrdsmmry.NewRow();
                grdmn1["Name and address of the person to whom the are given on interest"] = "T O T A L";
                grdmn1["Account No"] = "";
                grdmn1["Amount of sums given on interest"] = String.Format("{0:0.00}", Math.Round(dblBal)).ToString();
                grdmn1["Date on which sums given on Interest (dd/mm/yyyy)"] = "";
                grdmn1["Period for which sums were given on interest"] = "";
                grdmn1["Rate of interest"] = "";
                dtgrdsmmry.Rows.Add(grdmn1);
                Session["dtgrdsmmry"] = dtgrdsmmry;
                gvFormPrinting.DataSource = dtgrdsmmry;
                gvFormPrinting.DataBind();
            }
            else
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Data Not Found')</script>");
                return;
            }

            if (Session["strDate"] != null && Session["dDate"] != null)
            {
                dTimeFromDate = (DateTime)Session["strDate"];
                dTimeToDate = (DateTime)Session["dDate"];
                dblFinanyr = dTimeFromDate.Year;
                dtTimeFdate = dTimeFromDate.AddYears(1);
                dTimeTDate = dTimeToDate.AddYears(1);
                dblfyr = dtTimeFdate.Year;
                dbltyr = dTimeTDate.Year;
                lblAssesmentyr.Text = dblfyr.ToString() + "-" + dbltyr.ToString();
                lblPeriod.Text = dblFinanyr + "-" + dblfyr.ToString();
                lblAYear.Text = dblfyr.ToString() + "-" + dbltyr.ToString();
                lblPYear.Text = dtTimeFdate.Year.ToString();
            }         
            lblBAmount.Text = String.Format("{0:0.00}", Math.Round(dblPayable)).ToString(); 
            getFormDet();
            getClientDet();
        }
        catch (Exception ex)
        {
            objLog.WriteLog("Inside Form15HReportFinal->fillvalue()" + ex.Message);
            ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('" + ex.Message + "')</script>");
            return;
        }        
        finally
        {            
            dtForm15Det.Dispose();
            dtBankDet.Dispose();
        }
        objLog.WriteLog("Form15HReportFinal->fillvalue() Executed successfully");
    }
    protected void getFormDet()
    {
        DataTable dtForm15Det = new DataTable();
        if (Session["dtForm15Det"] != null)
        {
            dtForm15Det = (DataTable)Session["dtForm15Det"];
        }
        if (dtForm15Det.Rows.Count > 0)
        {
            if (!DBNull.Value.Equals(dtForm15Det.Rows[0]["N_NAME"].ToString()))
            {
                lblName.Text = dtForm15Det.Rows[0]["N_NAME"].ToString();
                lblName2.Text = dtForm15Det.Rows[0]["N_NAME"].ToString();
            }
            else
            {
                lblName.Text = "";
                lblName2.Text = "";
            }
            if (!DBNull.Value.Equals(dtForm15Det.Rows[0]["N_CLIENT"].ToString()))
                lblClientCode.Text = dtForm15Det.Rows[0]["N_CLIENT"].ToString();
            else
                lblClientCode.Text = "";
            if (Session["Age"]!=null)
                lblAge.Text = Session["Age"].ToString();
            else
                lblAge.Text = "";
            if (!DBNull.Value.Equals(dtForm15Det.Rows[0]["N_PAN"].ToString()))
            {
                lblPan.Text = dtForm15Det.Rows[0]["N_PAN"].ToString();
            }
            else
            {
                lblPan.Text = "";
            }
            if (!DBNull.Value.Equals(dtForm15Det.Rows[0]["N_ADDRESS1"].ToString()))
                lblAddress1.Text = dtForm15Det.Rows[0]["N_ADDRESS1"].ToString();
            else
                lblAddress1.Text = "";
            if (!DBNull.Value.Equals(dtForm15Det.Rows[0]["N_ADDRESS2"].ToString()))
                lblAddress2.Text = dtForm15Det.Rows[0]["N_ADDRESS2"].ToString();
            else
                lblAddress2.Text = "";
            if (!DBNull.Value.Equals(dtForm15Det.Rows[0]["N_ADDRESS3"].ToString()))
                lblAddress3.Text = dtForm15Det.Rows[0]["N_ADDRESS3"].ToString();
            else
                lblAddress3.Text = "";            
            if (!DBNull.Value.Equals(dtForm15Det.Rows[0]["N_AREA"].ToString()))
                lblArea.Text = dtForm15Det.Rows[0]["N_AREA"].ToString();
            else
                lblArea.Text = "";
            if (!DBNull.Value.Equals(dtForm15Det.Rows[0]["N_AREA"].ToString()))
                lblArea.Text = dtForm15Det.Rows[0]["N_AREA"].ToString();
            else
                lblArea.Text = "";
            if (!DBNull.Value.Equals(dtForm15Det.Rows[0]["N_PIN"].ToString()))
                lblPin.Text = dtForm15Det.Rows[0]["N_PIN"].ToString();
            else
                lblPin.Text = "";
        }
        else
        {
            ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Data Not Found on getFormDet()')</script>");
            return;
        }
    }
    protected void getBankDet()
    {
        string strDate;
        string strqry = "";
        DataTable dtBankDet = new DataTable();
        strqry = "select * from HEAD.BANKINFO";
        dtBankDet = objDataFetch.DatatablePassSQL(strqry);
        objLog.WriteLog("Inside Form15ReportFinal->getBankDet()->BankInfo query" + dtBankDet.Rows.Count);
        if (dtBankDet.Rows.Count > 0)
        {            
            Session["dtBankDet"] = dtBankDet;
            if ((!DBNull.Value.Equals(dtBankDet.Rows[0]["BAN_NAME"].ToString())))
            {
                objLog.WriteLog("Form15ReportFinal dtBankDet(BAN_NAME)" + dtBankDet.Rows[0]["BAN_NAME"].ToString());
                lblBankName.Text = dtBankDet.Rows[0]["BAN_NAME"].ToString();
            }
            else
            {
                lblBankName.Text = "";
            }
            if ((!DBNull.Value.Equals(dtBankDet.Rows[0]["BAN_ADDRESS1"].ToString())))
            {
                objLog.WriteLog("Form15ReportFinal dtBankDet(BAN_ADDRESS1)" + dtBankDet.Rows[0]["BAN_ADDRESS1"].ToString());
                lblBAddress1.Text = dtBankDet.Rows[0]["BAN_ADDRESS1"].ToString();
            }
            else
                lblBAddress1.Text = "";
            if ((!DBNull.Value.Equals(dtBankDet.Rows[0]["BAN_ADDRESS2"].ToString())))
                lblBAddress2.Text = dtBankDet.Rows[0]["BAN_ADDRESS2"].ToString();
            else
                lblBAddress2.Text = "";
            if ((!DBNull.Value.Equals(dtBankDet.Rows[0]["BAN_ADDRESS3"].ToString())))
            {
                lblBAddress3.Text = dtBankDet.Rows[0]["BAN_ADDRESS3"].ToString();
                lblBAddress33.Text = dtBankDet.Rows[0]["BAN_ADDRESS3"].ToString();
                lblPlace1.Text = dtBankDet.Rows[0]["BAN_ADDRESS3"].ToString();
            }
            else
            {
                lblBAddress3.Text = "";
                lblBAddress33.Text = "";
                lblPlace1.Text = "";
            }
            if ((!DBNull.Value.Equals(dtBankDet.Rows[0]["BAN_STATUS"].ToString())))
                lblStatus.Text = dtBankDet.Rows[0]["BAN_STATUS"].ToString();
            else
                lblStatus.Text = "";
            if ((!DBNull.Value.Equals(dtBankDet.Rows[0]["BAN_TAN"].ToString())))
                lblTan.Text = dtBankDet.Rows[0]["BAN_TAN"].ToString();
            else
                lblTan.Text = "";
            if ((!DBNull.Value.Equals(dtBankDet.Rows[0]["BAN_PAN"].ToString())))
                lblBPan.Text = dtBankDet.Rows[0]["BAN_PAN"].ToString();
            else
                lblBPan.Text = "";
            if ((!DBNull.Value.Equals(dtBankDet.Rows[0]["BAN_EMAIL"].ToString())))
                lblBEmail.Text = dtBankDet.Rows[0]["BAN_EMAIL"].ToString();
            else
                lblBEmail.Text = "";
            if ((!DBNull.Value.Equals(dtBankDet.Rows[0]["BAN_PHONENO"].ToString())))
                lblBPhoneNo.Text = dtBankDet.Rows[0]["BAN_PHONENO"].ToString();
            else
                lblBPhoneNo.Text = "";
            strDate = objGlb.GetServerDate();
            lblBDate.Text = strDate.ToString();
            lblBdate3.Text = strDate.ToString();
            lblBDate4.Text = strDate.ToString();
            lblCdate.Text = strDate.ToString();
            objLog.WriteLog("Form15HReportFinal->getBankDet()->Bankinfo executed successfully");
        }
        else
        {
            ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Data Not Found on getBankDet()')</script>");
            return;
        }
    }
    protected void getClientDet()
    {
        DataTable dtForm = new DataTable();
        DataTable dtClient = new DataTable();
        dtForm=(DataTable) Session["dtForm15Det"];
        string strQRY = "";
        if (dtForm.Rows.Count > 0)
        {
            if (!DBNull.Value.Equals(dtForm.Rows[0]["N_CLIENT"].ToString()))
            {
                strQRY = " select CLMS_PHONENO,OCCU_DES,ADDR_PINCODE,ADDR_AREACODE,ADDR_EMAILID,ADDR_MOBILENO,ADDR_TELPHNO from head.clientmaster,head.occupation,head.address";
                strQRY += " where OCCU_SNO=CLMS_OCCUPATION";
                strQRY += " and ADDR_SNO=CLMS_ADDRESS";
                strQRY += " and CLMS_NO=" + dtForm.Rows[0]["N_CLIENT"].ToString();

                dtClient = objDataFetch.DatatablePassSQL(strQRY);
                if (dtClient.Rows.Count > 0)
                {
                    if (!DBNull.Value.Equals(dtClient.Rows[0]["ADDR_MOBILENO"].ToString()) || (!DBNull.Value.Equals(dtClient.Rows[0]["ADDR_TELPHNO"].ToString())))
                        lblPhoneNo.Text = dtClient.Rows[0]["ADDR_MOBILENO"].ToString() + "-" + dtClient.Rows[0]["ADDR_TELPHNO"].ToString();
                    else
                        lblPhoneNo.Text = "";
                    if (!DBNull.Value.Equals(dtClient.Rows[0]["OCCU_DES"].ToString()))
                        lblOccupation.Text = dtClient.Rows[0]["OCCU_DES"].ToString();
                    else
                        lblOccupation.Text = "";
                    if (!DBNull.Value.Equals(dtClient.Rows[0]["ADDR_EMAILID"].ToString()))
                        lblEmail.Text = dtClient.Rows[0]["ADDR_EMAILID"].ToString();
                    else
                        lblEmail.Text = "";
                    if (!DBNull.Value.Equals(dtClient.Rows[0]["ADDR_PINCODE"].ToString()))
                        lblPin.Text = dtClient.Rows[0]["ADDR_PINCODE"].ToString();
                    else
                        lblPin.Text = "";
                }
            }
        }
        else
        {
            ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Data Not Found on getClientDet()')</script>");
            return;
        }
        objLog.WriteLog("Form15HReportFinal->getClientDet()->executed successfully");
    }
    protected void img_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Session["dtForm15Det"] != null)
            {
                gvFormPrinting.DataSource = (DataTable)(Session["dtForm15Det"]);
            }
            if (gvFormPrinting.DataSource != null)
            {
                gvFormPrinting.DataBind();
                gvFormPrinting.UseAccessibleHeader = true;
                gvFormPrinting.HeaderRow.TableSection = TableRowSection.TableHeader;
                Response.ClearContent();
                Response.AddHeader("content-disposition", "attachment; filename='" + ViewState["Pagename"].ToString() + "'.xls");
                Response.ContentType = "application/excel";
                System.IO.StringWriter sw = new System.IO.StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Panel6.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
        }
        catch (Exception ex)
        {
            string m = ex.Message;
            ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('First Press SUBMIT and then press PRINT/DOWNLOAD option')</script>");
        }
    }
    protected void gvFormPrinting_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[0].Width = 100;
            e.Row.Cells[0].Font.Size = 8;
            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[1].Width = 100;
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[2].Width = 100;
            e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[3].Width = 100;
            e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[4].Width = 200;
            e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;

        }
        if (e.Row.Cells[0].Text == "T O T A L")
        {
            e.Row.Cells[0].Font.Bold = true;
            e.Row.Cells[0].ForeColor = System.Drawing.Color.Black;
            e.Row.Cells[2].Font.Bold = true;
            e.Row.Cells[2].ForeColor = System.Drawing.Color.Black;

        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Form15HLog.aspx");
    }

}