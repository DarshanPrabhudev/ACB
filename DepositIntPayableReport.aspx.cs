//version             Created By              Date              Assigned By
//-------------------------------------------------------------------------
// 1.0                   DL                15/12/2017              LS MAM 
// 2.0                  ROOPA           14-MAR-2018           LS MAM 
//ADDED 4 COLUMNS -DEPOSIT DATE, MATURITY DATE, RATE OF INTEREST AND NO. OF INTEREST IS CALCULATED.
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using System.Text.RegularExpressions;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;

public partial class DepositIntPayableReport : System.Web.UI.Page
{
    XMLMetaMasters xmlObj = new XMLMetaMasters();
    global objGlob = new global();  
    DataTable dtList = new DataTable();
    DAL.DataFetch objFetch = new DAL.DataFetch();
    Log objlog = new Log();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {                
                xmlObj.FillMetaXMLDdl(ddlBranch, "Branch.xml", "BRAN_SNO", "BRAN_NAME", null);
                if (Session["BranchLogin"] != null)
                {
                    ddlBranch.SelectedValue = Session["BranchLogin"].ToString();
                }
                txtAsDate.Text = objGlob.GetServerDate();                
                dtList = xmlObj.FillMetaXMLDdl(null, "DEPOSITSCHEMESETUP.xml", "", "", "DSCH_ACTIVEYN = 'Y' AND DSCH_BRANCH = '" + ddlBranch.SelectedValue + "' AND DSCH_TYPE IN (3,4)");
                if (dtList.Rows.Count <= 0)
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert ('No Deposit Scheme Found...') </script>");
                    return;
                }
                chkList.DataSource = dtList;
                chkList.DataTextField = "DSCH_NAME";
                chkList.DataValueField = "DSCH_ABBR";
                chkList.DataBind();
            }
        }
        catch (Exception ex)
        {
            string g = ex.Message;
            g = Regex.Replace(g, "[^a-zA-Z0-9_]+", " ");
            objlog.WriteLog("DepositIntPayableReport .aspx: Exception in PageLoad():" + g);
            ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('" + g + "')</script>");
            return;
        }
        finally
        {            
            objlog.WriteLog("DepositIntPayableReport .aspx: Page_Load-->Page Load events Finally block Executed  ");
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string strFromDate = "",strAbbr = "", abbrCount = "", strBranch = "", abbrSno = "" ,sDate = string.Empty;           
        double dblBal = 0.0, dblDiffAmt = 0.0;
        DataTable dtResult = new DataTable();
        DataTable dtAllDetails = new DataTable();
        DataRow row; 
        int iLength = 0;
        try
        {
            SingletonConn objDataFetch = new SingletonConn();
            OracleConnection oraConn = new OracleConnection();
            oraConn = objDataFetch.getSingletonConnectionInstance();


            lblBank.Visible = true;
            lblBranch.Visible = true;
            lblBank.Text = global.BankName();
            lblBranch.Text = ddlBranch.SelectedItem.Text;

            strBranch = ddlBranch.SelectedValue;
            strFromDate = txtAsDate.Text;

            for (int i = 0; i < chkList.Items.Count; i++)
            {
                if (chkList.Items[i].Selected)
                {
                    strAbbr +=  chkList.Items[i].Value.Trim() +  ",";
                    abbrCount = abbrCount + 1;
                }
            }
            if (strAbbr != "")
            {
                iLength = strAbbr.Length - 1;
                abbrSno = strAbbr.Substring(0, iLength);
            }
            else
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert ('Select a Scheme and then Proceed...!') </script>");
                return;
            }

            strFromDate = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(txtAsDate.Text));
            objlog.WriteLog("DepositIntPayableReport .aspx: Before calling the procedur");
            dtAllDetails = DepositIntPayable(Convert.ToInt32(ddlBranch.SelectedValue), strAbbr, strFromDate);
            if(dtAllDetails.Rows.Count>0)
            {
                dtResult.Columns.Add(new DataColumn("SL No", typeof(string)));
                dtResult.Columns.Add(new DataColumn("Client No", typeof(string)));
                dtResult.Columns.Add(new DataColumn("Abbr", typeof(string)));
                dtResult.Columns.Add(new DataColumn("Acno", typeof(string)));                
                dtResult.Columns.Add(new DataColumn("Name", typeof(string)));
                dtResult.Columns.Add(new DataColumn("Balance", typeof(string)));
                dtResult.Columns.Add(new DataColumn("Int Payable", typeof(string)));
                //ADDED BY ROOPA ON 14032018
                dtResult.Columns.Add(new DataColumn("Deposit Date", typeof(string)));
                dtResult.Columns.Add(new DataColumn("Maturity Date", typeof(string)));
                dtResult.Columns.Add(new DataColumn("Rate of Int(%)", typeof(string)));
                dtResult.Columns.Add(new DataColumn("No. of Days Int Calu", typeof(string)));
                //END
                int i = 1;
                
                foreach (DataRow dr in dtAllDetails.Rows)
                {
                    row = dtResult.NewRow();
                    row["SL No"] = i;
                    row["Abbr"] = dr["ABBR"];
                    row["Acno"] = dr["ACC_NO"];
                    row["Client No"] = dr["CLIENT"];
                    row["Name"] = dr["CLIENT_NAME"];
                    row["Balance"] =  string.Format("{0:0.00}",dr["ACC_BAL"]);
                    if (!DBNull.Value.Equals(dr["ACC_BAL"]))
                    {
                        dblBal = dblBal + Convert.ToDouble(dr["ACC_BAL"]);
                    }
                    row["Int Payable"] =  string.Format("{0:0.00}",dr["DIFFAMT"]);
                    if (!DBNull.Value.Equals(dr["DIFFAMT"]))
                    {
                        dblDiffAmt = dblDiffAmt + Convert.ToDouble(dr["DIFFAMT"]);
                    }

                    //ADDED BY ROOPA ON 14032018
                    if (!DBNull.Value.Equals(dr["DEPDATE"]))
                    {
                        row["Deposit Date"]=String.Format("{0:dd/MM/yyyy}",dr["DEPDATE"]);
                    }
                    else
                    {
                        row["Deposit Date"]="";
                    }

                    if (!DBNull.Value.Equals(dr["MATDATE"]))
                    {
                        row["Maturity Date"]=String.Format("{0:dd/MM/yyyy}",dr["MATDATE"]);
                    }
                    else
                    {
                        row["Maturity Date"]="";
                    }

                    if (!DBNull.Value.Equals(dr["INTRATE"]))
                    {
                        row["Rate of Int(%)"] = dr["INTRATE"];
                    }
               
                    if (!DBNull.Value.Equals(dr["NOOFDAYS"]))
                    {
                        row["No. of Days Int Calu"] = dr["NOOFDAYS"]; ;
                    }
                    else
                    {
                        row["No. of Days Int Calu"] = 0;
                    }
                    //END
                    
                    dtResult.Rows.Add(row);
                    i++;
                }                
            }
            else
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert ('Data not found') </script>");
                return;
            }
            row = dtResult.NewRow();
            row["Name"] = "Total";
            row["Balance"]= string.Format("{0:0.00}",dblBal);
            row["Int Payable"] =  string.Format("{0:0.00}",dblDiffAmt);
            dtResult.Rows.Add(row);
            gvRptDisp.DataSource = dtResult;
            gvRptDisp.DataBind();
            (ViewState["Details"]) = dtResult;
        }
        catch (Exception ex)
        {
            string g = ex.Message;
            g = Regex.Replace(g, "[^a-zA-Z0-9_]+", " ");
            objlog.WriteLog("DepositIntPayableReport .aspx: Exception in btnSubmit_Click():" + g);
            ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('" + g + "')</script>");
            return;
        }
        finally
        {          
            
            objlog.WriteLog("DepositIntPayableReport .aspx: btnSubmit_Click-->btnSubmit_Click events Finally block Executed  ");
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("rmenu.aspx");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("DepositIntPayableReport.aspx");
    }
    public DataTable DepositIntPayable(double Branch, string strAbbr, string strdate)
    {
        
        DataTable dtData = new DataTable();

        try
        {

            SingletonConn objDataFetch = new SingletonConn();
            OracleConnection oraConn = new OracleConnection();
            OracleCommand cmd = new OracleCommand();
            oraConn = objDataFetch.getSingletonConnectionInstance();
            cmd.Parameters.Clear();
            cmd.Connection = oraConn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "INTPAYABLE";
            cmd.Parameters.Add("ABBR", OracleType.VarChar).Value = strAbbr;
            cmd.Parameters.Add("ONDATE", OracleType.DateTime).Value = String.Format("{0:dd-MMM-yyyy}", strdate);
            cmd.Parameters.Add("BRANCH", OracleType.Number).Value = Branch;
            cmd.Parameters.Add("ITCUR", OracleType.Cursor).Direction = System.Data.ParameterDirection.Output;
            OracleDataAdapter odaLOrpt = new OracleDataAdapter(cmd);
            odaLOrpt.Fill(dtData); 
            objlog.WriteLog("INTPAYABLE" + "" + "Executed Sucessfully");
            //objLog.WriteLog("CBSILODNEWFORMAT.CBS_ILODREPORT : ds2.Rows.Count:" + dtLOrpt.Rows.Count);
        }
        catch (Exception ex)
        {
            string g;
            g = ex.Message;
            g = Regex.Replace(g, "[^a-zA-Z0-9_]+", " ");
            objlog.WriteLog("DepositIntPayableReport.aspx: Exception in DepositIntPayable() : " + g);
            ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert('" + g + "')</script>");
        }
        finally
        {
            objlog.WriteLog("DepositIntPayableReport.aspx: DepositIntPayable():finally block reached..");
        }
        return dtData;
    }
    protected void gvRptDisp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
            //e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;   
        }

        if (e.Row.Cells[4].Text.ToString().Trim() == "Total".Trim())
        {
            e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[4].Font.Bold = true;
            e.Row.Cells[5].Font.Bold = true;
            e.Row.Cells[6].Font.Bold = true;
        }
    }
    protected void imgxl_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dtData = new DataTable();
        try
        {
            dtData = (DataTable)(ViewState["Details"]);
            gvRptDisp.DataSource = dtData;

            if (gvRptDisp.DataSource == null)
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('First Press SUBMIT and then press PRINT/DOWNLOAD option')</script>");
            }
            if (gvRptDisp.DataSource != null)
            {

                objlog.WriteLog("DepositIntPayableReport.aspx:imgxl_Click(): binding gvPackage grid");
                gvRptDisp.DataBind();
                gvRptDisp.UseAccessibleHeader = true;
                gvRptDisp.HeaderRow.TableSection = TableRowSection.TableHeader;
                Response.ClearContent();
                Response.AddHeader("content-disposition", "attachment; filename=Report.xls");
                Response.ContentType = "application/excel";
                System.IO.StringWriter sw = new System.IO.StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);

                Panel1.RenderControl(htw);

                Response.Write(sw.ToString());
                Response.End();
            }
        }
        catch (Exception ex)
        {
            string g = ex.Message;
            g = Regex.Replace(g, "[^a-zA-Z0-9_]+", " ");
            objlog.WriteLog("DepositIntPayableReport.aspx--->imgxl_Click() Event-->: " + ex.Message + ex);
            ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('" + g + "')</script>");
        }
        finally
        {
            objlog.WriteLog("DepositIntPayableReport.aspx--->imgxl_Click() Event-->:Finally block");

        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }
}