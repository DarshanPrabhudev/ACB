/*>>>>>>>>>>>>>>>>>>>>>>>>>>Report - Consolidated Balance Sheet of Selected Branches<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

 Version            Developed By            Date                  Assigned By
 -----------------------------------------------------------------------------------------------------------------
  1.0                 Meghana              14.04.2014                   VK
  2.0               Dhanalakshmi            06/05/2017                   LS Mam (code optimization, XMl, Ajax Implementation)
------------------------------------------------------------------------------------------------------------------
-------------------------------------------------Pseudo Code------------------------------------------------------
This report is used to display CRR/SLR Report between Two Dates of Bank
INPUTS - From Date,To Date
OUTPUT - Amount at different Dates
Stored Procedure Used - CRRSLR15
-----------------------------------------------------------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.OracleClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Globalization;

public partial class CRRSLRbtnTwoGivenDates : System.Web.UI.Page
{
    DataTable dtBranch = new DataTable();
    Log objLog = new Log();    
    OracleConnection oraConn = new OracleConnection();
    OracleDataAdapter oDaBranch = new OracleDataAdapter();
    global objGlob = new global();
    XMLMetaMasters objXml = new XMLMetaMasters();
    protected void Page_Load(object sender, EventArgs e)
    {

        globalSession.checkSession();
            try
            {
                OracleConnection oraConn = new OracleConnection(Session["constring"].ToString());
                if (!Page.IsPostBack)
                {
                    if (oraConn.State != ConnectionState.Open) oraConn.Open();
                    string strSql = "";
                    //Bal.Gen objFillDate = new Bal.Gen();
                    //objFillDate.Call_Date(ddlFromDay, ddlFromMonth, ddlFromYear);

                    //Bal.Gen objFillToDate = new Bal.Gen();
                    //objFillToDate.Call_Date(ddlToDay, ddlToMonth, ddlToYear);


                    OracleConnection con = new OracleConnection(Session["constring"].ToString());
                    //strSql = "select bran_sno,bran_name from MASTER.branch";
                    //oDaBranch = new OracleDataAdapter(strSql, con);
                    //oDaBranch.Fill(dtBranch);
                    //ddlBranch.DataSource = dtBranch;
                    //ddlBranch.DataTextField = "bran_name";
                    //ddlBranch.DataValueField = "bran_sno";
                    //ddlBranch.DataBind();
                    txtFrom.Text = objGlob.GetServerDate();
                    txtTo.Text = objGlob.GetServerDate();
                    objXml.FillMetaXMLDdl(ddlBranch, "BRANCH.xml", "BRAN_SNO", "BRAN_NAME", null); //Added by Dhanalakshmi
                    ddlBranch.SelectedValue = Session["BranchLogin"].ToString();
                    ddlBranch.SelectedValue = 1.ToString();//GlobalConstants.branchname.ToString();
                    ddlBranch.Enabled = false;
                    //if (!global.Valid_Activity(30005))
                    //{
                    //    ddlBranch.Enabled = false;
                    //}
                    //else
                    //{
                    //    ddlBranch.Enabled = true;
                    //}
                    //ddlBranch.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                string g = ex.Message;
                g = Regex.Replace(g, "[^a-zA-Z0-9_]+", " ");
                objLog.WriteLog("CRRSLRbtnTwoGivenDates.aspx: Exception in Pageload" + g);
                ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('" + g + "')</script>");
                return;
            }
            finally
            {
                if (oraConn.State == ConnectionState.Open)
                    oraConn.Close();
                oraConn.Dispose();               
                objLog.WriteLog("CRRSLRbtnTwoGivenDates.aspx: Page_Load-->Page Load events Finally block Executed  ");
            }      
    }                          
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string strFromDate = "", strToDate = "", dayWeek = "", sDay1 = "", sDay2 = "", sDay3 = "", sDay4 = "",
            sDay5 = "", sDay6 = "", sDay7 = " ", monthName = "";
        int m;
        double y;

        DateTime temp, dTo, dFrom;
        DataTable dtDetails = new DataTable();
        DataTable dtMain = new DataTable();       
        DataRow row;


        strFromDate = Convert.ToDateTime(txtFrom.Text).ToString("dd-MMM-yyyy"); 

        objLog.WriteLog("CRRSLR BETWEEN TWO GIVEN DATES - strFromDate : " + strFromDate);

        strToDate = Convert.ToDateTime(txtTo.Text).ToString("dd-MMM-yyyy"); 

        objLog.WriteLog("CRRSLR BETWEEN TWO GIVEN DATES - strToDate : " + strToDate);
              

        dFrom = Convert.ToDateTime(strFromDate);
        objLog.WriteLog("CRRSLR BETWEEN TWO GIVEN DATES - dFrom : " + dFrom);
        dTo = Convert.ToDateTime(strToDate);
        objLog.WriteLog("CRRSLR BETWEEN TWO GIVEN DATES - dTo : " + dTo);

        OracleConnection oraConn = new OracleConnection();
        if (Session["constring"] != null)
        {
            oraConn = new OracleConnection(Session["constring"].ToString());
        }

        try
        {
            if (!(DateTime.TryParse(strFromDate, out temp)) || (!(DateTime.TryParse(strToDate, out temp))))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert ('Enter a Valid Date') </script>");
                return;
            }
            if ((Convert.ToDateTime(strFromDate) > DateTime.Now) || (Convert.ToDateTime(strToDate) > DateTime.Now))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert ('Enter a Valid Date') </script>");
                return;
            }
            if (Convert.ToDateTime(strFromDate) > Convert.ToDateTime(strToDate))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert ('Check From Date') </script>");
                return;
            }

            y = (Convert.ToDateTime(dTo) - dFrom).TotalDays;

            objLog.WriteLog("CRRSLR BETWEEN TWO GIVEN DATES - y : " + y);

            if (y >= 15)
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert ('The difference between two dates must not be greater than 7 days') </script>");
                return;
            }

            //month = ddlFromMonth.SelectedItem.Text;
            //m = ddlFromMonth.SelectedIndex + 1;
            //monthName = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(m);
            m = dFrom.Month;
            monthName = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(m);

            objLog.WriteLog("CRRSLR BETWEEN TWO GIVEN DATES - CALLING PROC : CRRSLR15");

            OracleCommand cmdProcExec = new OracleCommand("CRRSLR15", oraConn);
            cmdProcExec.CommandType = System.Data.CommandType.StoredProcedure;
            cmdProcExec.Parameters.Add("VDATE", OracleType.DateTime).Value = strFromDate;
            cmdProcExec.Parameters.Add("ASONDATE", OracleType.DateTime).Value = strToDate;
            cmdProcExec.Parameters.Add("DOP_S", OracleType.Cursor).Direction = System.Data.ParameterDirection.Output;

            objLog.WriteLog("CRRSLR BETWEEN TWO GIVEN DATES - AFTER CALLING PROC : CRRSLR15");
            objLog.WriteLog("CRRSLR BETWEEN TWO GIVEN DATES - strFromDate : " + strFromDate);
            objLog.WriteLog("CRRSLR BETWEEN TWO GIVEN DATES - strToDate : " + strToDate);

            OracleDataAdapter oDaCRRSLR = new OracleDataAdapter(cmdProcExec);

            oDaCRRSLR.Fill(dtDetails);

            objLog.WriteLog("CRRSLR BETWEEN TWO GIVEN DATES - Row Count(dtDetails) " + dtDetails.Rows.Count.ToString());

            if (dtDetails.Rows.Count <= 0)
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert ('No Records...Change the Criteria...') </script>");
                return;
            }
            else
            {
                dayWeek = System.DateTime.Now.DayOfWeek.ToString();

                if (dFrom < dTo)
                {
                    dayWeek = dFrom.DayOfWeek.ToString();
                    sDay1 = String.Format("{0:dd/MM/yyyy}", dFrom);
                    sDay1 = dayWeek + " " + sDay1;
                    dFrom = dFrom.AddDays(1);
                }
                else
                {
                    sDay1 = " ";
                }
                if (dFrom <= dTo)
                {
                    dayWeek = dFrom.DayOfWeek.ToString();
                    sDay2 = String.Format("{0:dd/MM/yyyy}", dFrom);
                    sDay2 = dayWeek + " " + sDay2;
                    dFrom = dFrom.AddDays(1);
                }
                else
                {
                    sDay2 = "  ";
                }
                if (dFrom <= dTo)
                {
                    dayWeek = dFrom.DayOfWeek.ToString();
                    sDay3 = String.Format("{0:dd/MM/yyyy}", dFrom);
                    sDay3 = dayWeek + " " + sDay3;
                    dFrom = dFrom.AddDays(1);
                }
                else
                {
                    sDay3 = "   ";
                }
                if (dFrom <= dTo)
                {
                    dayWeek = dFrom.DayOfWeek.ToString();
                    sDay4 = String.Format("{0:dd/MM/yyyy}", dFrom);
                    sDay4 = dayWeek + " " + sDay4;
                    dFrom = dFrom.AddDays(1);
                }
                else
                {
                    sDay4 = "    ";
                }
                if (dFrom <= dTo)
                {
                    dayWeek = dFrom.DayOfWeek.ToString();
                    sDay5 = String.Format("{0:dd/MM/yyyy}", dFrom);
                    sDay5 = dayWeek + " " + sDay5;
                    dFrom = dFrom.AddDays(1);
                }
                else
                {
                    sDay5 = "     ";
                }
                if (dFrom <= dTo)
                {
                    dayWeek = dFrom.DayOfWeek.ToString();
                    sDay6 = String.Format("{0:dd/MM/yyyy}", dFrom);
                    sDay6 = dayWeek + " " + sDay6;
                    dFrom = dFrom.AddDays(1);
                }
                else
                {
                    sDay6 = "      ";
                }
                if (dFrom <= dTo)
                {
                    dayWeek = dFrom.DayOfWeek.ToString();
                    sDay7 = String.Format("{0:dd/MM/yyyy}", dFrom);
                    sDay7 = dayWeek + " " + sDay7;
                    dFrom = dFrom.AddDays(1);
                }
                else
                {
                    sDay7 = "       ";
                }

                dtMain.Columns.Add("CRRSLR15", typeof(string));
                dtMain.Columns.Add(sDay1, typeof(string));
                dtMain.Columns.Add(sDay2, typeof(string));
                dtMain.Columns.Add(sDay3, typeof(string));
                dtMain.Columns.Add(sDay4, typeof(string));
                dtMain.Columns.Add(sDay5, typeof(string));
                dtMain.Columns.Add(sDay6, typeof(string));
                dtMain.Columns.Add(sDay7, typeof(string));


                foreach (DataRow dr in dtDetails.Rows)
                {
                    row = dtMain.NewRow();

                    if (!DBNull.Value.Equals(dr["DESCRIPTION"]))
                        row["CRRSLR15"] = dr["DESCRIPTION"].ToString();

                    if (!DBNull.Value.Equals(dr["TOTAL"]))
                        row[sDay1] =  Convert.ToDouble(dr["TOTAL"]).ToString("0,0.00", CultureInfo.CreateSpecificCulture("hi-IN"));

                    if (dr["TOTAL"].ToString() == "0")
                        row[sDay1] = " ";

                    if (!DBNull.Value.Equals(dr["TOTAL2"]))
                        row[sDay2] = Convert.ToDouble(dr["TOTAL2"]).ToString("0,0.00", CultureInfo.CreateSpecificCulture("hi-IN"));

                    if (dr["TOTAL2"].ToString() == "0")
                        row[sDay2] = " ";

                    if (!DBNull.Value.Equals(dr["TOTAL3"]))
                        row[sDay3] = Convert.ToDouble(dr["TOTAL3"]).ToString("0,0.00", CultureInfo.CreateSpecificCulture("hi-IN"));

                    if (dr["TOTAL3"].ToString() == "0")
                        row[sDay3] = " ";

                    if (!DBNull.Value.Equals(dr["TOTAL4"]))
                        row[sDay4] = Convert.ToDouble(dr["TOTAL4"]).ToString("0,0.00", CultureInfo.CreateSpecificCulture("hi-IN"));

                    if (dr["TOTAL4"].ToString() == "0")
                        row[sDay4] = " ";

                    if (!DBNull.Value.Equals(dr["TOTAL5"]))
                        row[sDay5] = Convert.ToDouble(dr["TOTAL5"]).ToString("0,0.00", CultureInfo.CreateSpecificCulture("hi-IN"));

                    if (dr["TOTAL5"].ToString() == "0")
                        row[sDay5] = " ";

                    if (!DBNull.Value.Equals(dr["TOTAL6"]))
                        row[sDay6] = Convert.ToDouble(dr["TOTAL6"]).ToString("0,0.00", CultureInfo.CreateSpecificCulture("hi-IN"));

                    if (dr["TOTAL6"].ToString() == "0")
                        row[sDay6] = " ";

                    if (!DBNull.Value.Equals(dr["TOTAL7"]))
                        row[sDay7] = Convert.ToDouble(dr["TOTAL7"]).ToString("0,0.00", CultureInfo.CreateSpecificCulture("hi-IN"));

                    if (dr["TOTAL7"].ToString() == "0")
                        row[sDay7] = " ";

                    dtMain.Rows.Add(row);
                }
            }
            Session["rptcrrslr15"] = dtMain;
            Session["BnkName"] = global.BankName();
            Session["month"] = monthName;
            Response.Redirect("rptDisp_CRRSLRTwoDates.aspx");
        }       
        catch (Exception ex)
        {
            string g = ex.Message;
            g = Regex.Replace(g, "[^a-zA-Z0-9_]+", " ");
            objLog.WriteLog("CRRSLRbtnTwoGivenDates.aspx: Exception in btnSubmit_Click" + g);
            ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('" + g + "')</script>");
            return;
        }
        finally
        {
            if (oraConn.State == ConnectionState.Open)
                oraConn.Close();
            oraConn.Dispose();
            dtDetails.Dispose();
            dtMain.Dispose();
            objLog.WriteLog("CRRSLRbtnTwoGivenDates.aspx: btnSubmit_Click-->btnSubmit_Click events Finally block Executed  ");
        }      
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect(Gen.REPORT_MENU);
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("CRRSLRbtnTwoGivenDates.aspx");
    }
}