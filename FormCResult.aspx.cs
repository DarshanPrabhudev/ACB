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



public partial class FormCResult : System.Web.UI.Page
{
    public FormatRpt objfrmpt = new FormatRpt();

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session.Contents.Count == 0)
        {
            Response.Redirect("ShowMessage.aspx?msg=" + GlobalConstants.msg);
        }
        else
        {
            if (!IsPostBack)
            {
                if (Session["table"] != null)
                {
                    DataTable dtnew = new DataTable();
                    dtnew = (DataTable)Session["table"];
                    int sno = 1;
                    for (int j = 0; j < dtnew.Rows.Count; j++)
                    {
                        dtnew.Rows[j]["Sno"] = sno++;
                        double amt = Convert.ToDouble(dtnew.Rows[j]["TRND_AMOUNT"]);
                        dtnew.Rows[j]["TRND_AMOUNT"] = String.Format("{0:0.00}", Convert.ToDouble(dtnew.Rows[j]["TRND_AMOUNT"]));
                    }
                    DataRow row = dtnew.NewRow();
                    string stramt = String.Format("{0:0.00}", Convert.ToDouble(dtnew.Compute("Sum(TRND_AMOUNT)", "")));
                    row["TRND_AMOUNT"] = String.Format("{0:0.00}", Convert.ToDouble(dtnew.Compute("Sum(TRND_AMOUNT)", "")));
                    row["TRND_INSTRUMENTNUMBER"] = "T O T A L";
                    dtnew.Rows.Add(row);
                    //*******total*******

                    //********amount conversion***********

                    MultiCurrency currenc = new MultiCurrency(Criteria.Indian);
                    string number = stramt;
                    string[] ss = number.Split('.');
                    if (ss.Length == 1)
                    {
                        number = number + ".00";
                    }
                    string[] amount = number.Split('.');
                    string AmountInWords = currenc.ConvertToWord(amount[0]);
                    string paises;
                    AmountInWords = AmountInWords + " Rupees";
                    if (int.Parse(amount[1]) == 0)
                    {
                        amount[1] = "00";
                        paises = "";
                    }
                    else
                    {
                        paises = currenc.ConvertToWord(amount[1]);
                        paises = " And " + paises + " Paises";
                    }
                    AmountInWords = AmountInWords + paises;
                    //**************** 


                    lblManager.Text = Session["Manager"].ToString();
                    Label1.Text = global.BankName();
                    Label6.Text = Label1.Text;
                    Label5.Text = DateTime.Now.ToString();
                    lblAmount.Text = AmountInWords.ToString();
                    Label2.Text = Session["branch"].ToString();
                    Label3.Text = Session["frmdate"].ToString();
                    Label4.Text = Session["todate"].ToString();
                    lblFrmDate.Text = Session["frmdate"].ToString();
                    lblToDate.Text = Session["todate"].ToString();
                    GridView1.DataSource = dtnew;
                    GridView1.DataBind();
                    objfrmpt.FormatGridView(GridView1);
                }
            }
        }
    }
    protected void imgxl_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            DataTable dtviewstate = new DataTable();
            dtviewstate = (DataTable)(Session["table"]);
            GridView1.DataSource = dtviewstate;

            if (GridView1.DataSource != null)
            {
                GridView1.DataBind();
                GridView1.UseAccessibleHeader = true;
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                Response.ClearContent();
                Response.AddHeader("content-disposition", "attachment; filename=FormCReport.xls");
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
    protected void goBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("FormC.aspx", false);
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[3].Text.Trim() == ("T O T A L").Trim())
            {
                e.Row.Cells[3].ForeColor = System.Drawing.Color.Black;
                e.Row.Cells[4].ForeColor = System.Drawing.Color.Black;
                e.Row.Cells[3].Font.Bold = true;
                e.Row.Cells[4].Font.Bold = true;
            }
        }
    }
}

