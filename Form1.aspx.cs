/*>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>Report - Form1<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

 Version            Developed By            Date                  Assigned By
 -----------------------------------------------------------------------------------------------------------------
  1.0                 Meghana             04.04.2014                  VK
 * 2.0              Darshan               23.05.2017                  LS
------------------------------------------------------------------------------------------------------------------
-------------------------------------------------Pseudo Code------------------------------------------------------
This report is used to display Form1 Report of Bank
INPUTS: As On Date, branch,Officer,Designation
Stored Procedure used:"MAS_FORM1"
OUTPUTS: Description,Amount
 * 
 *2.0
 *Ajax,xml Implementation and BPR
-----------------------------------------------------------------------------------------------------------------*/

using System;
using System.Data;
//using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;
using System.Data.OracleClient;
using System.Text.RegularExpressions;
using System.Globalization;
//using System.Collections.Generic;
//using System.Linq;
//using DAL;
//using Bal;

public partial class Form1 : System.Web.UI.Page
{
    DAL.DataFetch objDataFetch = new DAL.DataFetch();
    XMLMetaMasters objXML = new XMLMetaMasters();
    global objGlobal = new global();
    Log objLog = new Log();
    string g = "";
    //DataTable dtBranch = new DataTable();    
    //OracleConnection oraConn = new OracleConnection();
    //OracleDataAdapter oDaBranch = new OracleDataAdapter();

    protected void Page_Load(object sender, EventArgs e)
    {
        global.checkSession();
        try
        {
            //OracleConnection oraConn = new OracleConnection(Session["constring"].ToString());
            if (!Page.IsPostBack)
            {
                //if (oraConn.State != ConnectionState.Open) oraConn.Open();
                //string strSql = "";
                //Bal.Gen objFillDate = new Bal.Gen();
                //objFillDate.Call_Date(ddlDay, ddlMonth, ddlYear);

                OracleConnection con = new OracleConnection(Session["constring"].ToString());

                txtAsOnDate.Text = objGlobal.GetServerDate();
                objXML.FillMetaXMLDdl(ddlBranch, "BRANCH.xml", "BRAN_SNO", "BRAN_NAME", null);
                ddlBranch.SelectedValue = 1.ToString();

                //strSql = "select bran_sno,bran_name from MASTER.branch";
                //oDaBranch = new OracleDataAdapter(strSql, con);
                //oDaBranch.Fill(dtBranch);
                //ddlBranch.DataSource = dtBranch;
                //ddlBranch.DataTextField = "bran_name";
                //ddlBranch.DataValueField = "bran_sno";
                //ddlBranch.DataBind();
                //ddlBranch.SelectedValue = 1.ToString();//GlobalConstants.branchname.ToString();
                //ddlBranch.Enabled = false;
                //if (!objValid.Valid_Activity(30005))
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
            g = ex.Message;
            g = Regex.Replace(g, "[^a-zA-Z0-9_]+", " ");
            objLog.WriteLog("Form1.aspx: Exception in PageLoad():" + g);
            ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('" + g + "')</script>");
            return;
        }
        finally
        {
            objLog.WriteLog("Form1.aspx: Page_Load-->Page Load events Finally block Executed  ");
        }

    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        string strDate, month, monthName = "", monthYear = "", strOfficer = "", strDesignation = "", strTotalX = "", sFridays = "",
            friday1 = "", friday2 = "", friday3 = "", firstFriday = "", secondFriday = "", thirdFriday = "", strSql = "", sDateFriday = "",
            sEnd = "";
        string[] sAltFridays = new string[30];
        int year, m, nNoOfFriday = 0, NoOfFiday = 0;
        double dblDateFst, dblDateLst;

        DateTime temp, dDate, dRBIAltFriday, dFirst;
        DataTable dtAllDetails = new DataTable();
        DataTable dtDetails = new DataTable();
        //DataTable dtFriday = new DataTable();
        DataTable dtDate = new DataTable();
        DataRow row;
        OracleConnection oraConn = new OracleConnection(Session["constring"].ToString());

        try
        {
            //strDate = ddlDay.SelectedValue + "-" + ddlMonth.SelectedValue + "-" + ddlYear.SelectedValue;
            strDate = Convert.ToDateTime(txtAsOnDate.Text).ToString("dd/MMM/yyyy");
            objLog.WriteLog("Form1.aspx strDate: " + strDate);

            if ((!(DateTime.TryParse(strDate, out temp))) || (Convert.ToDateTime(strDate) > DateTime.Now))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert ('Enter a Valid Date') </script>");
                return;
            }
            Session["ason"] = strDate;
            //month = ddlMonth.SelectedItem.Text;
            //month = Convert.ToDateTime(txtAsOnDate.Text).ToString("MMMM", CultureInfo.InvariantCulture);
            m = (DateTime.Parse(strDate).Month) + 1;
            //year = Convert.ToInt32(ddlYear.SelectedItem.Text);

            year = DateTime.Parse(strDate).Year;
            monthName = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(m);
            monthYear = monthName + "-" + year;
            Session["year"] = monthYear;

            strOfficer = txtOfficer.Text;
            strDesignation = txtDesignation.Text;

            Session["officer"] = strOfficer;
            Session["designation"] = strDesignation;

            objLog.WriteLog("Form1.aspx: Calling Procedure : MAS_FORM1 Inputs: ASONDATE = " + strDate);
            OracleCommand cmdProcExec = new OracleCommand("MAS_FORM1", oraConn);
            cmdProcExec.CommandType = System.Data.CommandType.StoredProcedure;
            cmdProcExec.Parameters.Add("ASONDATE", OracleType.DateTime).Value = strDate;
            cmdProcExec.Parameters.Add("DOP_S", OracleType.Cursor).Direction = System.Data.ParameterDirection.Output;
            OracleDataAdapter oDaForm = new OracleDataAdapter(cmdProcExec);
            oDaForm.Fill(dtAllDetails);
            objLog.WriteLog("Form1.aspx: After Calling Procedure: MAS_FORM1 and Row Count Details:" + dtAllDetails.Rows.Count.ToString());

            if (dtAllDetails.Rows.Count <= 0)
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert ('No Records for this Criteria...') </script>");
                return;
            }
            else
            {
                //-----------------------------------------------------------------------------------------------
                dDate = Convert.ToDateTime("17/01/1997");
                dRBIAltFriday = dDate;

                sDateFriday = "01/" + DateTime.Parse(strDate).Month + "/" + DateTime.Parse(strDate).Year;
                dFirst = Convert.ToDateTime(sDateFriday);

                strSql = "SELECT ADD_MONTHS(TO_DATE('" + String.Format("{0:dd/MM/yyyy}", dFirst) + "','DD/MM/YYYY'),1) - 1 AS ENDDATE FROM DUAL";
                OracleDataAdapter oDaDate = new OracleDataAdapter(strSql, oraConn);
                oDaDate.Fill(dtDate);

                if (dtDate.Rows.Count > 0)
                {
                    if ((!DBNull.Value.Equals(dtDate.Rows[0]["EndDate"])) && (dtDate.Rows[0]["EndDate"].ToString() != string.Empty))
                        sEnd = String.Format("{0:dd/MM/yyyy}", dtDate.Rows[0]["EndDate"]);
                }
                while (dFirst <= Convert.ToDateTime(strDate))
                {
                    dblDateFst = global.DateDiffFind(dDate, dFirst, "DD");
                    dblDateLst = global.DateDiffFind(dDate, dFirst, "DD");

                    if (!DBNull.Value.Equals(dblDateLst))
                    {
                        if ((dblDateFst) / 14.0 == Convert.ToInt32(dblDateLst / 14.0))
                        {
                            nNoOfFriday++;
                            sAltFridays[nNoOfFriday] = dFirst.ToString("dd/MM/yyyy");
                        }
                    }
                    dFirst = dFirst.AddDays(1);
                }
                if (nNoOfFriday == 1)
                {
                    friday1 = sAltFridays[NoOfFiday];
                    friday2 = "";
                    friday3 = "";
                }
                if (nNoOfFriday == 2)
                {
                    friday1 = sAltFridays[1];
                    friday2 = sAltFridays[2];
                    friday3 = "";
                }
                if (nNoOfFriday == 3)
                {
                    friday1 = sAltFridays[1];
                    friday2 = sAltFridays[2];
                    friday3 = sAltFridays[3];
                }
                if (nNoOfFriday == 4)
                {
                    friday1 = sAltFridays[1];
                    friday2 = sAltFridays[2];
                    friday3 = sAltFridays[3];
                }

                objLog.WriteLog("Form1.aspx : friday1 : " + friday1 + " friday2 : " + friday2 + " friday3 : " + friday3);

                firstFriday = "1st alternate Friday@" + friday1;
                secondFriday = "2nd alternate Friday@" + friday2;
                thirdFriday = "3rd alternate Friday@" + friday3;
                //-------------------------------------

                dtDetails.Columns.Add("Description", typeof(string));
                dtDetails.Columns.Add(firstFriday, typeof(string));
                dtDetails.Columns.Add(secondFriday, typeof(string));
                dtDetails.Columns.Add(thirdFriday, typeof(string));


                foreach (DataRow dr in dtAllDetails.Rows)
                {
                    row = dtDetails.NewRow();
                    if (!DBNull.Value.Equals(dr["SPA_CE"]))
                    {
                        switch (Convert.ToInt32(dr["SPA_CE"]))
                        {
                            case 1: if (dr["DESCRIPTION"].ToString().Trim() != ".")
                                {
                                    row["Description"] = dr["DESCRIPTION"].ToString();
                                }
                                break;
                            case 2: row["Description"] = "      " + dr["DESCRIPTION"].ToString();
                                break;

                            case 3: row["Description"] = "      " + dr["DESCRIPTION"].ToString();
                                break;

                            case 4: row["Description"] = "" + dr["DESCRIPTION"].ToString();
                                break;

                            case 5: row["Description"] = "            " + dr["DESCRIPTION"].ToString();
                                break;

                            case 6: row["Description"] = "     " + dr["DESCRIPTION"].ToString();
                                break;

                            case 7: row["Description"] = "      " + dr["DESCRIPTION"].ToString();
                                break;

                            default:
                                break;
                        }
                    }
                    if (!DBNull.Value.Equals(dr["FIRST_FRI"]))
                    {
                        if (Convert.ToDouble(dr["FIRST_FRI"]) > 0.00)
                        {
                            if (dr["DESCRIPTION"].ToString().Contains("IX."))
                            {
                                strTotalX = (dr["FIRST_FRI"]).ToString();
                                row[firstFriday] = "(" + String.Format(new CultureInfo("hi-IN", false), "{0:0,0.00}", Convert.ToDouble(strTotalX)) + ")";

                                strTotalX = (dr["SECOND_FRI"]).ToString();
                                row[secondFriday] = "(" + String.Format(new CultureInfo("hi-IN", false), "{0:0,0.00}", Convert.ToDouble(strTotalX)) + ")";

                                strTotalX = (dr["THIRD_FRI"]).ToString();
                                row[thirdFriday] = "(" + String.Format(new CultureInfo("hi-IN", false), "{0:0,0.00}", Convert.ToDouble(strTotalX)) + ")";
                            }
                            else if (dr["DESCRIPTION"].ToString().Contains("XI."))
                            {
                                strTotalX = (dr["FIRST_FRI"]).ToString();
                                row[firstFriday] = "(" + String.Format(new CultureInfo("hi-IN", false), "{0:0,0.00}", Convert.ToDouble(strTotalX)) + ")";

                                strTotalX = (dr["SECOND_FRI"]).ToString();
                                row[secondFriday] = "(" + String.Format(new CultureInfo("hi-IN", false), "{0:0,0.00}", Convert.ToDouble(strTotalX)) + ")";

                                strTotalX = (dr["THIRD_FRI"]).ToString();
                                row[thirdFriday] = "(" + String.Format(new CultureInfo("hi-IN", false), "{0:0,0.00}", Convert.ToDouble(strTotalX)) + ")";
                            }
                            else if ((!DBNull.Value.Equals(dr["FIRST_FRI"])) || (!DBNull.Value.Equals(dr["SECOND_FRI"])) || (!DBNull.Value.Equals(dr["THIRD_FRI"])))
                            {
                                row[firstFriday] = String.Format(new CultureInfo("hi-IN", false), "{0:0,0.00}", dr["FIRST_FRI"]);
                                row[secondFriday] = String.Format(new CultureInfo("hi-IN", false), "{0:0,0.00}", dr["SECOND_FRI"]);
                                row[thirdFriday] = String.Format(new CultureInfo("hi-IN", false), "{0:0,0.00}", dr["THIRD_FRI"]);
                            }
                        }
                    }
                    dtDetails.Rows.Add(row);
                }
            }
            Session["BnkName"] = global.BankName();
            Session["form1"] = dtDetails;
            Response.Redirect("rptDisp_Form1.aspx?branch=" + ddlBranch.SelectedItem.Text + "&brn=" + global.BankName() + "&dt=" + strDate, false);
        }
        catch (Exception ex)
        {
            g = ex.Message;
            g = Regex.Replace(g, "[^a-zA-Z0-9_]+", " ");
            objLog.WriteLog("Form1.aspx: Exception in btnOk_Click():" + g);
            ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('" + g + "')</script>");
            return;
        }
        finally
        {
            if (oraConn.State == ConnectionState.Open)
                oraConn.Close();
            oraConn.Dispose();            
            dtDetails.Dispose();
            dtAllDetails.Dispose();
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect(Gen.REPORT_MENU);
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("Form1.aspx");
    }
}
