/*----------------------------Clearing Status Report------------------------------------
 *   Version        Modified by         Date of Modified        Remarks
 *   ----------------------------------------------------------------------------
 *   2.0            Darshan P           23.01.2017              Taken to Implement Ajax, XML, and BPR
 * ------------------------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.OracleClient;
using System.Text.RegularExpressions;

public partial class ClearingStatus : System.Web.UI.Page
{
    string strSQL = "", g = "";    
    DataTable dtResult = new DataTable();
    XMLMetaMasters objXml = new XMLMetaMasters();
    global objGlobal = new global();
    Log objLog = new Log();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        global.checkSession();
        OracleConnection oraConn = new OracleConnection(Session["constring"].ToString());
        try
        {
            if (!IsPostBack)
            {
                objXml.FillMetaXMLDdl(ddlBranch, "BRANCH.xml", "BRAN_SNO", "BRAN_NAME", null);
                ddlBranch.SelectedValue = Session["BranchLogin"].ToString();
                txtFrom.Text = objGlobal.GetServerDate();
                txtTo.Text = objGlobal.GetServerDate();
                if (Convert.ToInt16(ddlBranch.SelectedValue) == 1)
                {
                    ddlBranch.Enabled = true;
                }
                else
                {
                    ddlBranch.Enabled = false;
                }
            }
        }
        catch (Exception ex)
        {
            g = ex.Message;
            g = Regex.Replace(g, "[^a-zA-Z0-9_]+", " ");
            objLog.WriteLog("ClearingStatus.aspx: Exception in PageLoad : " + g);
            ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert('" + g + "')</script>");
            return;
        }
        finally
        {
            oraConn.Close();
            oraConn.Dispose();
            objLog.WriteLog("ClearingStatus.aspx: Page_Load-->Page Load events Finally block Executed  ");
        }
    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        string strdtfrm = "", To_Date = "";
        OracleConnection oraConn = new OracleConnection(Session["constring"].ToString());
        try
        {
            //strdtfrm = ddfrmday.SelectedItem.Value + "-" + ddfrmMonth.SelectedItem.Text + "-" + ddfrmYear.SelectedItem.Value;
            //To_Date = DropDownList1.SelectedItem.Value + "-" + DropDownList2.SelectedItem.Text + "-" + DropDownList3.SelectedItem.Value;            
            strdtfrm =Convert.ToDateTime(txtFrom.Text).ToString("dd/MMM/yyyy");
            To_Date = Convert.ToDateTime(txtTo.Text).ToString("dd/MMM/yyyy");

            strSQL = " SELECT CLEA_VOUCHERDATE,COUNT(DECODE(CLEA_INOUT,'I',CLEA_VOUCHERNO)) AS IN_NO, ";
            strSQL += " SUM(DECODE(CLEA_INOUT,'I',CLEA_AMOUNT)) AS IN_AMT, ";
            strSQL += " COUNT(DECODE(CLEA_INOUT,'I',DECODE(CLEA_RELEASEDYN,'B',CLEA_VOUCHERNO))) AS IN_NO_RET, ";
            strSQL += " SUM(DECODE(CLEA_INOUT,'I',DECODE(CLEA_RELEASEDYN,'B',CLEA_AMOUNT))) AS IN_AMT_RET, ";
            strSQL += " COUNT(DECODE(CLEA_INOUT,'O',CLEA_VOUCHERNO)) AS OUT_NO, ";
            strSQL += " SUM(DECODE(CLEA_INOUT,'O',CLEA_AMOUNT)) AS OUT_AMT, ";
            strSQL += " COUNT(DECODE(CLEA_INOUT,'O',DECODE(CLEA_RELEASEDYN,'B',CLEA_VOUCHERNO))) AS OUT_NO_RET, ";
            strSQL += " SUM(DECODE(CLEA_INOUT,'O',DECODE(CLEA_RELEASEDYN,'B',CLEA_AMOUNT))) AS OUT_AMT_RET ";
            strSQL += " FROM CLEARING ";
            strSQL += " WHERE CLEA_VOUCHERDATE BETWEEN '" + strdtfrm + "' AND '" + To_Date + "' ";
            strSQL += " and clea_branch = " + Convert.ToInt32(ddlBranch.SelectedValue) + " ";
            strSQL += " GROUP BY CLEA_VOUCHERDATE ";
            strSQL += " ORDER BY CLEA_VOUCHERDATE ";
            objLog.WriteLog("ClearingStatus.aspx: Query:"+strSQL);
            OracleDataAdapter oda = new OracleDataAdapter(strSQL, oraConn);
            oda.Fill(dtResult);
            //GlobalConstants.strBnkName = global.BankName();
            //GlobalConstants.strVocDate = strdtfrm;
            //GlobalConstants.branch = ddlBranch.SelectedItem.Text;
            //GlobalConstants.StrHeading = "Clearing Status As on-" + GlobalConstants.strVocDate;
            string strHeading = "Clearing Status As on-" + strdtfrm;
            Session["ClearingStatus"] = dtResult;
            Response.Redirect("rptDisp.aspx?branch=" + ddlBranch.SelectedItem.Text + "&Brn=" + global.BankName() + "&Head=" + strHeading + "&FDate=" + strdtfrm, false);
        }
        catch (Exception ex)
        {
            g = ex.Message;
            g = Regex.Replace(g, "[^a-zA-Z0-9_]+", " ");
            objLog.WriteLog("ClearingStatus.aspx: Exception in btnOk_Click : " + g);
            ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert('" + g + "')</script>");
            return;
        }
        finally
        {
            oraConn.Close();
            oraConn.Dispose();
            dtResult.Dispose();
            objLog.WriteLog("ClearingStatus.aspx: btnOk_Click:finally block reached..");
        }
    }   
    protected void btnBack_Click(object sender, EventArgs e)
    {
       Response.Redirect(Gen.REPORT_MENU,false);
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("ClearingStatus.aspx");
    }
}
