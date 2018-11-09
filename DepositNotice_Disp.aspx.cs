using System;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using System.Web;

public partial class Notice_DepositNotice_Disp : System.Web.UI.Page
{
    DataTable strdt = new DataTable();
    public Log objLog = new Log();
    DataTable dtDisplay = new DataTable();
    public FormatRpt objFrmPt = new FormatRpt();
    DataTable dtcom = new DataTable();
    string sSql = "", strBankAddr = "", strBankName = "";
    public override void VerifyRenderingInServerForm(Control control)
    {
        //return;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        objLog.WriteLog("DepositNotice_Disp: display function: ");
        global.checkSession();
        try
        {
            if (!IsPostBack)
            {
                ViewState["Pagename"] = null;
                if (Session["DepositNotice"] != null)
                {
                    objLog.WriteLog("DepositNotice_Disp :-Session DepositNotice");
                    ViewState["Pagename"] = "DepositNotice";
                    dtDisplay = (DataTable)Session["DepositNotice"];
                }
            }
            if (dtDisplay.Rows.Count > 0)
            {
                gvRptDisp.DataSource = dtDisplay;
                gvRptDisp.DataBind();

                //Session["datatable"] = dtDisplay;
                Session["DepositNotice"] = null;

            }
            this.gvRptDisp.GridLines = GridLines.None;
        }
        catch (Exception ex)
        {
            objLog.WriteLog("DepositNotice_Disp Exception in PageLoad : " + ex.Message);
            ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert('" + ex.Message + "')</script>");
        }
        finally
        {
            dtDisplay.Dispose();
        }
    }   
    protected void gvRptDisp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;        
        //  e.Row.Cells[0].Font.Size = 15;//12;
        //  e.Row.Cells[2].Font.Size = 15;//12;
        //////if (!e.Row.Cells[1].Text.Trim().Contains(":-"))
        //////{
        //////    e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
        //////   // e.Row.Cells[1].Font.Size = 17;//12;
        //////}
        //////if (e.Row.Cells[1].Text.Trim().Contains("-"))
        //////{
        //////    e.Row.Cells[1].Font.Bold = true;
        //////  //  e.Row.Cells[1].Font.Size = 17;//12;
        //////}

        //e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
        //e.Row.Cells[0].Width = 855;

        //e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
        //e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
        //e.Row.Cells[2].Wrap = false;
        if (!e.Row.Cells[0].Text.Trim().Contains(":-"))
        {
            e.Row.Cells[0].Font.Size = 16;//12;
            e.Row.Cells[0].Font.Size = 16;//12;
            e.Row.Cells[0].Font.Size = 16;//12;                      
        }
        if (e.Row.Cells[0].Text.Trim().Contains("ಸಂಖ್ಯೆ:"))
        {
            e.Row.Cells[0].Font.Bold = true;            
        }
        if ((e.Row.Cells[0].Text.Trim().Contains(":-")) || (e.Row.Cells[0].Text.Trim().Contains("ಬಸವೇಶ್ವರ")) || (e.Row.Cells[0].Text.Trim().Contains("ಸೆಕ್ಟರ")))
        {
            e.Row.Cells[0].Font.Bold = true;
            e.Row.Cells[0].Font.Size = 20;//16;
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
        }
        //UNCOMMENTED ON 01/02/17
        if (e.Row.Cells[0].Text.Trim().Contains("ದಿನಾಂಕ."))
        {
            e.Row.Cells[0].Font.Size = 16;
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Right;
        }
        if (e.Row.Cells[0].Text.Trim() == ("ನಂ."))
        {
            e.Row.Cells[0].Font.Size = 16;
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
        }        

        if (e.Row.Cells[0].Text.Trim().Contains("ಸೂಚನೆ"))
        {
            e.Row.Cells[0].CssClass = "grRowStyle";
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[0].Font.Bold = true;
            e.Row.Cells[0].Font.Underline = true;
        }
        if (e.Row.Cells[0].Text.Trim().Contains("ಕೆ.ವೈ.ಸಿ"))
        {
            e.Row.Cells[0].CssClass = "grTextStyle";
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
        }
        if (e.Row.Cells[0].Text.Trim().Contains("ಆಧಾರ್"))
        {
            e.Row.Cells[0].CssClass = "grTextStyle";
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
        }
        if (e.Row.Cells[0].Text.Trim().Contains("ಬ್ಯಾಂಕಿಗೆ"))
        {
            e.Row.Cells[0].CssClass = "grColmStyle";
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
        }
        //e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
        string decodedText = HttpUtility.HtmlDecode(e.Row.Cells[0].Text);
        e.Row.Cells[0].Text = decodedText;
        e.Row.Cells[0].Wrap = false;
    }
}