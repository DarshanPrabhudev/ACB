using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using System.Text;


    public partial class cdis : System.Web.UI.Page
    {
        public DataTable clrdisdt = new DataTable();
        string strComp, strBrn, strHead;
        DataTable strdt = new DataTable();
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            global.checkSession();
            if (!Page.IsPostBack)
            {
                if (Session["Title"] != null)
                {
                    Page.Title = "General Ledger";
                }
                strBrn = Request.QueryString["branch"];
                lblbranch.Text = "Branch:" + strBrn;
                strComp = Request.QueryString["Comp"];
                lblbank.Text = strComp;
                strHead = Request.QueryString["Head"];
                lblrep.Text = strHead;
                GlobalConstants.sd = Request.QueryString["gsd"];
                if (Session["Clearing"] != null)
                {
                    clrdisdt = (DataTable)Session["Clearing"];
                    gv.DataSource = clrdisdt;
                    gv.DataBind();
                    FormatRpt objgrd = new FormatRpt();
                    objgrd.FormatGridView(gv);
                    ViewState["datatable"] = clrdisdt;
                    Session["Clearing"] = null;
                }
                if (Session["clearingbnk"] != null)
                {
                    clrdisdt = (DataTable)Session["clearingbnk"];
                    gv.DataSource = clrdisdt;
                    gv.DataBind();
                    FormatRpt objgrd = new FormatRpt();
                    objgrd.FormatGridView(gv);
                    ViewState["datatable"] = clrdisdt;
                    Session["clearingbnk"] = null;
                }
                if (Session["ddissue"] != null)
                {
                    clrdisdt = (DataTable)Session["ddissue"];
                    gv.DataSource = clrdisdt;
                    gv.DataBind();
                    FormatRpt objgrd = new FormatRpt();
                    objgrd.FormatGridView(gv);
                    ViewState["datatable"] = clrdisdt;
                    Session["ddissue"] = null;
                }
                if (Session["generaldetl"] != null)
                {
                    lblgl.Visible = true;
                    lblgln.Visible = true;
                    lblgl.Text = GlobalConstants.Opening;
                    clrdisdt = (DataTable)Session["generaldetl"];
                    gv.DataSource = clrdisdt;
                    gv.DataBind();
                    FormatRpt objgrd = new FormatRpt();
                    objgrd.FormatGridView(gv);
                    ViewState["datatable"] = clrdisdt;
                    Session["generaldetl"] = null;
                }
                if (Session["generalsum"] != null)
                {
                    lblgl.Visible = true;
                    lblgln.Visible = true;
                    lblgl.Text = GlobalConstants.Opening;
                    clrdisdt = (DataTable)Session["generalsum"];
                    gv.DataSource = clrdisdt;
                    gv.DataBind();
                    FormatRpt objgrd = new FormatRpt();
                    objgrd.FormatGridView(gv);
                    ViewState["datatable"] = clrdisdt;
                    Session["generalsum"] = null;
                }
                if (Session["new"] != null)
                {
                    //lblgl.Visible = true;
                    // lblgln.Visible = true;
                    //lblgl.Text = GlobalConstants.Opening;
                    clrdisdt = (DataTable)Session["new"];
                    gv.DataSource = clrdisdt;
                    gv.ShowHeader = false;
                    gv.DataBind();
                    FormatRpt objgrd = new FormatRpt();
                    objgrd.FormatGridView(gv);
                    ViewState["datatable"] = clrdisdt;
                    Session["new"] = null;
                }
                  
            }
            
        }
        public void Printme(GridView gv, int nPageSize)
        {
            gv.UseAccessibleHeader = true;
            gv.HeaderRow.TableSection = TableRowSection.TableHeader;
            gv.FooterRow.TableSection = TableRowSection.TableFooter;
            gv.Attributes["style"] = "border-collapse:separate";
            foreach (GridViewRow row in gv.Rows)
            {
                if (row.RowIndex % nPageSize == 0 && row.RowIndex != 0)
                {
                    row.Attributes["style"] = "page-break-after:always;";
                }
            }

            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            gv.RenderControl(hw);
          
            string gridHTML = sw.ToString();
            gridHTML = gridHTML.Replace("\"", "'");
            gridHTML = gridHTML.Replace(System.Environment.NewLine, "");
            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload = new function(){");
            sb.Append("var printWin = window.open('', '', 'left=0");
            sb.Append(",top=0,width=1000,height=600,status=0,scrollbars=yes,toolbar=no,location=no,status=no,menubar=yes');");
            sb.Append("printWin.document.write(\"");
            string style = "<style type = 'text/css'>thead {display:table-header-group;} tfoot{display:table-footer-group;}</style>";
            sb.Append(style + gridHTML);
            sb.Append("\");");
            sb.Append("printWin.document.close();");
            sb.Append("printWin.focus();");
            sb.Append("printWin.print();");
            sb.Append("printWin.close();");
            sb.Append("};");
            sb.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "GridPrint", sb.ToString());
            gv.DataBind();            
        }

        public void GridPrint(GridView gvRptDisp)
        {
            if (gvRptDisp.Rows.Count > 0)
            {
                gvRptDisp.UseAccessibleHeader = true;
                gvRptDisp.HeaderRow.TableSection = TableRowSection.TableHeader;

            }
        }
        protected void btnprint_Click(object sender, EventArgs e)
        {
            if (Session["Clearing"] != null)
            {
                Printme(gv, 40);
            }
            //if (Session["clearingbnk"] != null)
            //{
            //    Printme(gvbankwiseclr, 40);
            //}
        }

        protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (Session["Clearing"] != null)
            {
                e.Row.Cells[1].Wrap = false;
                e.Row.Cells[2].Wrap = false;
                e.Row.Cells[5].Wrap = false;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[6].Wrap = false;
                GridPrint(gv);

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (e.Row.Cells[3].Text.Trim() == "Total No. of Instuments".Trim())
                    {
                        e.Row.Cells[3].ForeColor = System.Drawing.Color.Black;
                        e.Row.Cells[4].ForeColor = System.Drawing.Color.Black;
                        e.Row.Cells[3].Font.Bold = true;
                        e.Row.Cells[4].Font.Bold = true;
                    }
                    if (e.Row.Cells[3].Text.Trim() == "Total Amount".Trim())
                    {
                        e.Row.Cells[3].ForeColor = System.Drawing.Color.Black;
                        e.Row.Cells[4].ForeColor = System.Drawing.Color.Black;
                        e.Row.Cells[3].Font.Bold = true;
                        e.Row.Cells[4].Font.Bold = true;
                    }
                    if (e.Row.Cells[3].Text.Trim() == "Deposit Instruments".Trim())
                    {
                        e.Row.Cells[3].ForeColor = System.Drawing.Color.Black;
                        e.Row.Cells[4].ForeColor = System.Drawing.Color.Black;
                        e.Row.Cells[3].Font.Bold = true;
                        e.Row.Cells[4].Font.Bold = true;
                    }
                    if (e.Row.Cells[3].Text.Trim() == "OD Instruments".Trim())
                    {
                        e.Row.Cells[3].ForeColor = System.Drawing.Color.Black;
                        e.Row.Cells[4].ForeColor = System.Drawing.Color.Black;
                        e.Row.Cells[3].Font.Bold = true;
                        e.Row.Cells[4].Font.Bold = true;
                    }
                    if (e.Row.Cells[3].Text.Trim() == "Other Instruments".Trim())
                    {
                        e.Row.Cells[3].ForeColor = System.Drawing.Color.Black;
                        e.Row.Cells[4].ForeColor = System.Drawing.Color.Black;
                        e.Row.Cells[3].Font.Bold = true;
                        e.Row.Cells[4].Font.Bold = true;
                    }
                    if (e.Row.Cells[3].Text.Trim() == "Loan Instruments".Trim())
                    {
                        e.Row.Cells[3].ForeColor = System.Drawing.Color.Black;
                        e.Row.Cells[4].ForeColor = System.Drawing.Color.Black;
                        e.Row.Cells[3].Font.Bold = true;
                        e.Row.Cells[4].Font.Bold = true;
                    }
                    GridPrint(gv);
                }
            }
          
          
            if (Session["ddissue"] != null)
            {
                e.Row.Cells[1].Wrap = false;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;

                if (e.Row.Cells[3].Text == "Total")
                {
                    e.Row.Cells[3].Font.Bold = true;
                    e.Row.Cells[4].Font.Bold = true;
                    e.Row.Cells[5].Font.Bold = true;
                    e.Row.Cells[6].Font.Bold = true;
                }
                GridPrint(gv);
            }
            if (Session["clearingbnk"] != null)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Cells[1].Wrap = false;
                    e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                }
                if (e.Row.Cells[1].Text == "Total")
                {
                    e.Row.Cells[3].ForeColor = System.Drawing.Color.Black;
                    e.Row.Cells[1].ForeColor = System.Drawing.Color.Black;
                    e.Row.Cells[2].ForeColor = System.Drawing.Color.Black;
                    e.Row.Cells[3].Font.Bold = true;
                    e.Row.Cells[1].Font.Bold = true;
                    e.Row.Cells[2].Font.Bold = true;
                }
                if (e.Row.Cells[1].Text == "Grand Total")
                {
                    e.Row.Cells[3].ForeColor = System.Drawing.Color.Black;
                    e.Row.Cells[1].ForeColor = System.Drawing.Color.Black;
                    e.Row.Cells[2].ForeColor = System.Drawing.Color.Black;
                    e.Row.Cells[3].Font.Bold = true;
                    e.Row.Cells[1].Font.Bold = true;
                    e.Row.Cells[2].Font.Bold = true;
                }                
                GridPrint(gv);
            }
            if (Session["generaldetl"] != null)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
                    e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Left;
                    e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[2].Wrap = false;
                    e.Row.Cells[5].Wrap = true;

                }
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (e.Row.Cells[2].Text == "TOTAL")
                    {
                        e.Row.Cells[0].ForeColor = System.Drawing.Color.Black;
                        e.Row.Cells[1].ForeColor = System.Drawing.Color.Black;
                        e.Row.Cells[2].ForeColor = System.Drawing.Color.Black;
                        e.Row.Cells[3].ForeColor = System.Drawing.Color.Black;
                        e.Row.Cells[4].ForeColor = System.Drawing.Color.Black;
                        e.Row.Cells[5].ForeColor = System.Drawing.Color.Black;
                        e.Row.Cells[6].ForeColor = System.Drawing.Color.Black;
                        e.Row.Cells[0].Font.Bold = true;
                        e.Row.Cells[1].Font.Bold = true;
                        e.Row.Cells[2].Font.Bold = true;
                        e.Row.Cells[3].Font.Bold = true;
                        e.Row.Cells[4].Font.Bold = true;
                        e.Row.Cells[5].Font.Bold = true;
                        e.Row.Cells[6].Font.Bold = true;
                    }
                    GridPrint(gv);
                }
            }
            if (Session["generalsum"] != null)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
                    e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;

                }
                if (e.Row.Cells[0].Text == "TOTAL")
                {
                    e.Row.Cells[0].ForeColor = System.Drawing.Color.Black;
                    e.Row.Cells[1].ForeColor = System.Drawing.Color.Black;
                    e.Row.Cells[2].ForeColor = System.Drawing.Color.Black;
                    e.Row.Cells[0].Font.Bold = true;
                    e.Row.Cells[1].Font.Bold = true;
                    e.Row.Cells[2].Font.Bold = true;
                }
                GridPrint(gv);
            }
        }
        protected void gv_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void imgxl_Click(object sender, ImageClickEventArgs e)
        {
            strBrn = Request.QueryString["branch"];
            lblbranch0.Text = "Branch:" + strBrn;
            strComp = Request.QueryString["Comp"];
            lblbank0.Text = strComp;

            lblbank0.Visible = true;
            lblbranch0.Visible = true;
            lblrep0.Visible = true;
            lblrep0.Text = lblrep.Text;
            try
            {
                DataTable dtviewstate = new DataTable();
                dtviewstate = (DataTable)(ViewState["datatable"]);
                gv.DataSource = dtviewstate;
                if (gv.DataSource == null)
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('First Press SUBMIT and then press PRINT/DOWNLOAD option')</script>");
                }
                if (gv.DataSource != null)
                {

                    gv.DataBind();
                    gv.UseAccessibleHeader = true;
                    gv.HeaderRow.TableSection = TableRowSection.TableHeader;
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", "attachment; filename=Transaction.xls");
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
                string m = ex.Message;
                //   WriteLog(m);
                ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('First Press SUBMIT and then press PRINT/DOWNLOAD option')</script>");
            }
        }
        
}

