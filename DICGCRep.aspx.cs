/*-------------------------------DICGC REPORT----------------------------------
//* Version                   Created By                      Date                   INTIAL DRAFT
//* 1.0                         Roopa KV                   28-May-2018               Inayath Sir

//NAME OF THE CLIENT:  BGK
//DATABASE USED FOR TESTING : BASAVADB

//* -----------------------------PSEUDOCODE--------------------------------
//* ***********************************TABLES USED*************************************
//*1.BRANCH
//*2.DEPOSITSCHEMESETUP
//************************************PROCEDURES*************************************
 *1. HEAD.DICGC_REPORT
 */

using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OracleClient;
using System.Text.RegularExpressions;

public partial class DICGCRep : System.Web.UI.Page
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
                    txtDate.Text = Session["sysdate"].ToString();
                }
                else
                {
                    txtDate.Text = objGlobal.GetServerDate();
                }
                if (Session["BranchLogin"] != null)
                {
                    ddlBranch.SelectedValue = Session["BranchLogin"].ToString();
                }

                dtScheme = objXml.DataTableMetadataXML("DEPOSITSCHEMESETUP.XML", "");
                if (dtScheme.Rows.Count > 0)
                {
                    chkScheme.DataSource = dtScheme;
                    chkScheme.DataTextField = "DSCH_NAME";
                    chkScheme.DataValueField = "DSCH_ABBR";
                    chkScheme.DataBind();
                }
                else
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert ('No Deposit Scheme Found...') </script>");
                    return;
                }
                imgxl0.Visible = false;
            }

            catch (Exception ex)
            {
                string g = ex.Message + ex;
                g = Regex.Replace(g, "[^a-zA-Z0-9_]+", " ");
                objLog.WriteLog("DICGCRep.aspx-->Exception in Page_Load() Event" + g);
                ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert('" + g + "')</script>");
                return;
            }
            finally
            {
                objLog.WriteLog("DICGCRep.aspx-->Page_Load() Event Reached finally block without any exceptions.");
                dtScheme.Dispose();
            }
        }
    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        DataTable dtDetails = new DataTable();
        DateTime temp;
        string sDeposit = "";
        OracleConnection oraConn;
        oraConn = objDataFetch.getSingletonConnectionInstance();
        OracleCommand cmd = new OracleCommand();
        try
        {
            if (!(DateTime.TryParse(txtDate.Text, out temp)))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert ('Enter a Valid Date.') </script>");
                return;
            }

            if (txtBalance.Text == string.Empty)
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert ('Please enter the Balance and Proceed.') </script>");
                return;
            }

            if (Convert.ToDouble(txtBalance.Text) == 0)
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert ('Balance Amount should be greater than Zero.') </script>");
                return;
            }

            if (chkScheme.SelectedValue == "")
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert ('Select atleast one Scheme and Proceed.') </script>");
                return;
            }

            for (int i = 0; i < chkScheme.Items.Count; i++)
            {
                if (chkScheme.Items[i].Selected)
                {
                    sDeposit += chkScheme.Items[i].Value.Trim() + ",";
                }
            }
            sDeposit = sDeposit.Remove(sDeposit.Length - 1, 1) + "";

            cmd.Connection = oraConn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "HEAD.DICGC_REPORT";
          //  cmd.Parameters.Add(new OracleParameter("branch", OracleType.Number)).Value =Convert.ToInt16( ddlBranch.SelectedValue);
            if (rblBrnchConsol.SelectedValue.ToUpper().Trim() == "C")
            {
                cmd.Parameters.Add("branch", OracleType.Number).Value = DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("branch", OracleType.Number).Value = ddlBranch.SelectedValue;
            }
            cmd.Parameters.Add(new OracleParameter("schemes", OracleType.VarChar)).Value = sDeposit.Trim();
            cmd.Parameters.Add(new OracleParameter("asdate", OracleType.DateTime)).Value = String.Format("{0:dd/MM/yyyy}", txtDate.Text);
            cmd.Parameters.Add(new OracleParameter("balance", OracleType.Number)).Value =Convert.ToDouble( txtBalance.Text);
            cmd.Parameters.Add(new OracleParameter("dicgccur", OracleType.Cursor)).Direction = ParameterDirection.Output;
            OracleDataAdapter adp = new OracleDataAdapter(cmd);
            adp.Fill(dtDetails);

            if (dtDetails.Rows.Count > 0)
            {
                pnlGrid.Visible = true;
                lblBank.Visible = true;
                lblBranch.Visible = true;
                lblHeading.Visible = true;
                lblBank.Text = global.BankName();
                lblBranch.Text = Session["LoginBranchName"].ToString().Trim();
                lblHeading.Text = "DICGC REPORT AS ON " + txtDate.Text + "";
                gvDetails.DataSource = dtDetails;
                gvDetails.DataBind();
                Session["dtDetails"] = dtDetails;

                objFrmPt.FormatGridView(gvDetails);
                GridPrint(gvDetails);

                ViewState["Pagename"] = "DICGCREPORT";
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
            objLog.WriteLog("DICGCRep.aspx-->Exception in btnOK_Click() Event" + g);
            ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert('" + g + "')</script>");
            return;
        }
        finally
        {
            dtDetails.Dispose();
            objLog.WriteLog("DICGCRep.aspx-->btnOK_Click() Event Reached finally block without any exceptions.");
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
        Response.Redirect("DICGCRep.aspx");
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("rmenu.aspx");
    }
    protected void imgxl_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Session["dtDetails"] != null)
            {
                lblBank.Text = global.BankName();
                lblBranch.Text = Session["LoginBranchName"].ToString().Trim();
                lblHeading.Text = "DICGC REPORT AS ON" + txtDate.Text + "";
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
            objLog.WriteLog("DICGCRep.aspx-->Exception in btnOK_Click() Event" + g);
            ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert('" + g + "')</script>");
            return;
        }
        finally
        {
            objLog.WriteLog("DICGCRep.aspx-->btnOK_Click() Event Reached finally block without any exceptions.");
        }
    }
    protected void rblBrnchConsol_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblBrnchConsol.SelectedValue == "B")
        {
            ddlBranch.Enabled = true;
        }
        else
        {
            ddlBranch.Enabled = false;
        }
    }
    protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[30].HorizontalAlign = HorizontalAlign.Center;
    }
}