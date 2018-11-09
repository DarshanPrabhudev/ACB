//**----------------------FormC Select-------------------------------------
//**DATE          MODIFIED BY         Remarks
//**30/12/2013    Nayana              Implemented Log
//**08/12/2016    Darshan             Taken to Ajax and XML Implementation.
//*                                   correction in original code.
//-------------------------------------------------------------------------

using System;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Globalization;

public partial class FormCSelect : System.Web.UI.Page
{
    Log objLog = new Log();
    public DataTable dtnew = new DataTable();
    public DataTable dtselection = new DataTable();
    public DataTable dtmerge = new DataTable();
    public FormatRpt objfrmpt = new FormatRpt();

    protected void Page_Load(object sender, EventArgs e)
    {
        global.checkSession();
        objLog.WriteLog("FormCSelect : page_load");
        try
        {
            if (Session["table"] != null)
            {

                dtnew = (DataTable)Session["table"];

                for (int j = 0; j < dtnew.Rows.Count; j++)
                {

                    double amt = Convert.ToDouble(dtnew.Rows[j]["TRND_AMOUNT"]);
                    dtnew.Rows[j]["TRND_AMOUNT"] = String.Format(new CultureInfo("hi-IN", false), "{0:0,0.00}", Convert.ToDouble(dtnew.Rows[j]["TRND_AMOUNT"]));
                }
                DataRow row = dtnew.NewRow();
                string stramt = String.Format(new CultureInfo("hi-IN", false), "{0:0,0.00}", Convert.ToDouble(dtnew.Compute("Sum(TRND_AMOUNT)", "")));
                row["TRND_AMOUNT"] = String.Format(new CultureInfo("hi-IN", false), "{0:0,0.00}", Convert.ToDouble(dtnew.Compute("Sum(TRND_AMOUNT)", "")));
                row["TRND_INSTRUMENTNUMBER"] = "Grand Total";

                dtnew.Rows.Add(row);
                gv.DataSource = dtnew;
                //gv.DataSource = (DataTable)Session["table"];
                gv.DataBind();
                objfrmpt.FormatGridView(gv);
            }
        }
        catch (Exception ex)
        {
            objLog.WriteLog("form c Select Exception in PageLoad() : " + ex.Message);
            ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert('" + ex.Message + "')</script>");
        }
        finally
        {
            dtnew.Dispose();
        }
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        objLog.WriteLog("FormCSelect : btnPrint_Click");
        try
        {
            bool atLeastOneRowselected = false;
            int productID = 0;
            foreach (GridViewRow row in gv.Rows)
            {

                CheckBox cb = (CheckBox)row.FindControl("ProductSelector");
                if (cb != null && cb.Checked)
                {
                    atLeastOneRowselected = true;
                    productID = Convert.ToInt32(gv.DataKeys[row.RowIndex].Value);
                    dtselection = dtnew.Clone();
                    DataRow[] row1 = dtnew.Select("TRNH_VOUCHERNO = " + productID);
                    if (row1.Count() > 0)
                    {
                        foreach (DataRow rpri in row1)
                        {
                            DataRow r1 = dtselection.NewRow();
                            r1["TRNH_DD"] = rpri["TRNH_DD"];
                            r1["Drawer Bank Code"] = rpri["Drawer Bank Code"];
                            r1["Drawee Bank Code"] = rpri["Drawee Bank Code"];
                            r1["TRND_INSTRUMENTNUMBER"] = rpri["TRND_INSTRUMENTNUMBER"];
                            r1["TRND_AMOUNT"] = Convert.ToDouble(rpri["TRND_AMOUNT"]).ToString("0,0.00", CultureInfo.CreateSpecificCulture("hi-IN"));
                            r1["Issuing-Bank-Name(Drawer)"] = rpri["Issuing-Bank-Name(Drawer)"];
                            r1["Payee-Bank-Name(Drawee)"] = rpri["Payee-Bank-Name(Drawee)"];
                            dtselection.Rows.Add(r1);
                        }
                        dtmerge.Merge(dtselection);
                        productID = 0;
                    }
                }
                else if (cb.Checked == false)
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Select atleast one Recoed and proceed')</script>");

                }

            }//foreach
            if (dtmerge.Rows.Count > 0)
            {
                Session["table"] = null;
                Session["table"] = dtmerge;
                Response.Redirect("FormCResult.aspx", false);
            }
            objLog.WriteLog("FormCSelect : btnPrint_Click completed");

        }
        catch (Exception ex)
        {
            objLog.WriteLog("form c Select Exception in btnPrint_Click() : " + ex.Message);
            ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('" + ex.Message + "')</script>");
        }
        finally
        {
            dtselection.Dispose();
        }
    }
    protected void goBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("FormC.aspx", false);
    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[0].Text.Trim().Contains("Grand Total"))
            {
                e.Row.Cells[4].ForeColor = System.Drawing.Color.Black;
                e.Row.Cells[5].ForeColor = System.Drawing.Color.Black;                
                e.Row.Cells[4].Font.Bold = true;
                e.Row.Cells[5].Font.Bold = true;
            }
        }
    }
}
