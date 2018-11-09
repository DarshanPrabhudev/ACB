//*-------------------------------ACCOUNT CHEQUE STOP PAYMENT------------------------------------
//*
//* Version     Modified By         Date           Remarks
//* 1.0         ROOPA       26-FEB-2016    Intial Draft-Assisted by Venkatesh 
//*------------------------------------------------------------------------------
//*STARTED ON 25-FEB-2016
//*COMPLETED ON 26-FEB-2016
//* -----------------------------PSEUDOCODE--------------------------------------
//*  THIS REPORT IS USED TO SEE THE DBTL TRANSACTIONS.
//*
//****** Tables Used ******//
//*1.DBTLLOG
//*2.Branch
//*3.Siteparameters

using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using System.Data;
using System.Data.OracleClient;


public partial class DBTL_LogDisp : System.Web.UI.Page
{
    public Log objLog = new Log();
    public FormatRpt objFrmPt = new FormatRpt();
    DataTable dtDisplay = new DataTable();
    DataTable dtcom = new DataTable();
    string sSql = "", strBnkName="";
    OracleConnection oraConn = new OracleConnection();
   
    protected void Page_Load(object sender, EventArgs e)
    {
        objLog.WriteLog("DBTL_LogDisp : display function : ");

        if (Session.Contents.Count == 0)
        {
            Response.Redirect("ShowMessage.aspx?msg=" + GlobalConstants.msg);
        }
        else
        {
            try
            {
                if (!IsPostBack)
                {
                    OracleConnection oraConn = new OracleConnection(Session["constring"].ToString());
                    if (oraConn.State != ConnectionState.Open) oraConn.Open();
                    ViewState["Pagename"] = null;

                    if (Session["Display"] != null)
                    {
                        objLog.WriteLog("DBTL_LogDisp :-Session Display");

                        ViewState["Pagename"] = "DBTL_Log";

                        dtDisplay = (DataTable)Session["Display"];
                    }


                    sSql = "Select SITP_TEXTVALUE From Siteparameters Where SITP_NAME = 'COMPANY NAME'";
                    OracleDataAdapter odaC = new OracleDataAdapter(sSql, oraConn);
                    odaC.Fill(dtcom);

                    if (dtcom.Rows.Count != 0)
                    {
                        strBnkName = dtcom.Rows[0]["SITP_TEXTVALUE"].ToString();
                    }
                    lblBank.Text = strBnkName;

                    if (Session["LoginBranchName"] != null)
                    {
                        lblBranch.Text = Session["LoginBranchName"].ToString();
                    }
                    if ( Session["Heading"] != null)
                    {
                        lblRptHead.Text= Session["Heading"].ToString();
                    }

                    if (Session["NonCredit"] != null)
                    {
                        lblNonCre.Text = Session["NonCredit"].ToString();
                    }

                    if (Session["Credit"] != null)
                    {
                        lblCredit.Text = Session["Credit"].ToString();
                    }

                    if (dtDisplay.Rows.Count > 0)
                    {
                        gvDBTL.DataSource = dtDisplay;
                        gvDBTL.DataBind();

                        Session["datatable"] = dtDisplay;
                        Session["Display"] = null;

                        objFrmPt.FormatGridView(gvDBTL);
                        GridPrint(gvDBTL);
                    }

                }
            }

            catch (Exception ex)
            {
                objLog.WriteLog("DBTL_LogDisp Exception in PageLoad : " + ex.Message);
                ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert('" + ex.Message + "')</script>");
            }
            finally
            {
                if (oraConn.State == ConnectionState.Open)
                    oraConn.Close();
                oraConn.Dispose();
                OracleConnection.ClearAllPools();

            }
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
    protected void gvDBTL_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[11].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[12].HorizontalAlign = HorizontalAlign.Center;
           
        }
        if ((e.Row.Cells[0].Text == "&nbsp;") || (e.Row.Cells[0].Text.Contains(":")))
        {
            e.Row.Cells[0].Font.Bold = true;
            e.Row.Cells[0].ForeColor = System.Drawing.Color.Black;
            e.Row.Cells[2].Font.Bold = true;
            e.Row.Cells[2].ForeColor = System.Drawing.Color.Black;
            e.Row.Cells[3].Font.Bold = true;
            e.Row.Cells[3].ForeColor = System.Drawing.Color.Black;
            e.Row.Cells[5].Font.Bold = true;
            e.Row.Cells[5].ForeColor = System.Drawing.Color.Black;
        }
        e.Row.Cells[2].Wrap = false;
        //e.Row.Cells[4].Wrap = false;
        e.Row.Cells[5].Wrap = false;
        e.Row.Cells[8].Wrap = false;
     
    }
    protected void imgxl_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Session["datatable"] != null)
            {
                gvDBTL.DataSource = (DataTable)(Session["datatable"]);
            }
            if (gvDBTL.DataSource != null)
            {
                gvDBTL.DataBind();
                gvDBTL.UseAccessibleHeader = true;
                gvDBTL.HeaderRow.TableSection = TableRowSection.TableHeader;

                Response.ClearContent();
                Response.AddHeader("content-disposition", "attachment; filename='" + ViewState["Pagename"].ToString() + "'.xls");
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
            ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('First Press SUBMIT and then press PRINT/DOWNLOAD option')</script>");
        }
    }

}