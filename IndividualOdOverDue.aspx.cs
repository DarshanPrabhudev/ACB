/*>>>>>>>>>>>>>>>>>>>>>Report - Individual Od OverDue<<<<<<<<<<<<<<<<<<<<<<<
 
    Version    Modified By      Date             Remarks
-------------------------------------------------------------------------------------
    2.0          Darshan        06/12/2016      Taken to Implement Ajax and XML, And BPR
----------------------------------------------------------------------------------*/

using System;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using System.Globalization;
using System.Text.RegularExpressions;

    public partial class IndividualOdOverDue : System.Web.UI.Page
    {
        Log objLog = new Log();
        RptQueries objRpt = new RptQueries();
        string strsql = "", g = "";
        DataTable dtodReport = new DataTable();
        XMLMetaMasters objXml = new XMLMetaMasters();
        global objGlobal = new global();

        protected void Page_Load(object sender, EventArgs e)
        {
            global.checkSession();
            OracleConnection oraConn = new OracleConnection(Session["constring"].ToString());
            DataTable dtList = new DataTable();
            try
            {
                if (!IsPostBack)
                {
                    if (oraConn.State != ConnectionState.Open) oraConn.Open();
                    // ADDED BY DARSHAN
                    objXml.FillMetaXMLDdl(ddlBranch, "BRANCH.xml", "BRAN_SNO", "BRAN_NAME", null);
                    ddlBranch.SelectedValue = Session["BranchLogin"].ToString();
                    txtAsOnDate.Text = objGlobal.GetServerDate();
                    if (Convert.ToInt16(ddlBranch.SelectedValue) == 1)
                    {
                        ddlBranch.Enabled = true;
                    }
                    else
                    {
                        ddlBranch.Enabled = false;
                    }
                    dtList = objXml.DataTableMetadataXML("ODSCHEMESETUP.xml", "ODST_BRANCH=" + ddlBranch.SelectedValue);
                    //--------------------------------------------------
                    chkList.DataSource = dtList;
                    chkList.DataTextField = "ODST_NAME";
                    chkList.DataValueField = "ODST_ABBR";
                    chkList.DataBind();
                }
            }
            catch (Exception ex)
            {
                g = ex.Message;
                g = Regex.Replace(g, "[^a-zA-Z0-9_]+", " ");
                objLog.WriteLog("IndividualOdOverDue.aspx: Exception in PageLoad : " + g);
                ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert('" + g + "')</script>");
                return;
            }
            finally
            {
                dtList.Dispose();
                oraConn.Close();
                oraConn.Dispose();
                objLog.WriteLog("IndividualOdOverDue.aspx: Page_Load-->Page Load events Finally block Executed  ");
            }

        }
        protected void btnOk_Click(object sender, EventArgs e)
        {
            DataTable dtdetails = new DataTable();
            string abbr = "", abbritm = "", strdtfrm = "", branval = "", branname = "",records = "";
            int lstcont = 0, i = 0, selectedCount = 0;
            try
            {
                // ADDED BY DARSHAN
                strdtfrm = Convert.ToDateTime(txtAsOnDate.Text).ToString("dd/MMM/yyyy");
                //--------------------

                for (i = 0; i < chkList.Items.Count; i++)
                {
                    if (chkList.Items[i].Selected)
                    {
                        abbr += chkList.Items[i].Value.Trim() + ",";
                        abbritm += chkList.Items[i].Text.Trim() + ",";
                    }
                }

                lstcont = Convert.ToInt32(chkList.Items.Count);

                for (i = 0; i < ddlBranch.Items.Count; i++)
                {
                    branval += (ddlBranch.Items[i].Value + ",");
                    branname += ddlBranch.Items[i].Text + ",";
                }

                selectedCount = chkList.Items.Cast<ListItem>().Count(li => li.Selected);

                dtdetails = OdOverdueReport(Convert.ToInt32(ddlBranch.SelectedValue), abbr, strdtfrm);

                records = dtdetails.Rows.Count.ToString();

                if (dtdetails.Rows.Count > 0)
                {
                    Session["asondate"] = strdtfrm;
                    Session["odres"] = dtdetails;
                    Session["chklist"] = abbr + ",";
                    Session["sumdetl"] = rdbSummary.SelectedValue.ToString();
                    Session["chklistcount"] = selectedCount;
                    Session["Format"] = rdbType.SelectedValue.ToString();
                    //global.Loginconn = Session["constring"].ToString();
                    //GlobalConstants.pagename = HttpContext.Current.Request.Url.AbsoluteUri;
                    //GlobalConstants.branch = ddlBranch.SelectedItem.Text;

                    Response.Redirect("OdResult.aspx?branch=" + ddlBranch.SelectedItem.Text, false);
                }
                else
                {
                    objLog.WriteLog(records);
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('No Records for this Criteria')</script>");
                }
            }
            catch (Exception ex)
            {
                g = ex.Message;
                g = Regex.Replace(g, "[^a-zA-Z0-9_]+", " ");
                objLog.WriteLog("IndividualOdOverDue.aspx: Exception in btnOk_Click : " + g);
                ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert('" + g + "')</script>");
                return;
            }
            finally
            {
                dtdetails.Dispose();
                dtodReport.Dispose();
                objLog.WriteLog("IndividualOdOverDue.aspx: btnOk_Click:finally block reached..");
            }
        }
        public DataTable OdOverdueReport(int brnch, string abbr, string date)
        {
            OracleConnection oraConn = new OracleConnection(Session["constring"].ToString());
            OracleCommand cmdProcExec = new OracleCommand();
            try
            {
                objLog.WriteLog("IndividualOdOverDue.aspx:CBSOD.CBS_IODREPORT: abbr :" + abbr + " date : " + date + " brnch :" + brnch + " MEM :" + "Y" + " QRY :" + "NO");
                if (oraConn.State != ConnectionState.Open) oraConn.Open();
                cmdProcExec.Connection = oraConn;
                cmdProcExec.CommandType = System.Data.CommandType.StoredProcedure;
                cmdProcExec.CommandText = "CBSOD.CBS_IODREPORT";
                cmdProcExec.Parameters.Add("P_STRABBR", OracleType.VarChar).Value = abbr;
                cmdProcExec.Parameters.Add("P_ASONDATE", OracleType.DateTime).Value = date;
                cmdProcExec.Parameters.Add("P_BNO", OracleType.Number).Value = brnch;
                cmdProcExec.Parameters.Add("MEM", OracleType.VarChar).Value = "Y";
                cmdProcExec.Parameters.Add("QRY", OracleType.VarChar).Value = "NO";
                cmdProcExec.Parameters.Add("REC", OracleType.Cursor).Direction = System.Data.ParameterDirection.Output;
                OracleDataAdapter dataAdapter = new OracleDataAdapter(cmdProcExec);

                dataAdapter.Fill(dtodReport);
                objLog.WriteLog("IndividualOdOverDue.aspx:CBSOD.CBS_IODREPORT" + "" + "Executed Sucessfully");
            }
            catch (Exception ex)
            {
                g = ex.Message;
                g = Regex.Replace(g, "[^a-zA-Z0-9_]+", " ");
                objLog.WriteLog("IndividualOdOverDue.aspx: Exception in OdOverdueReport() : " + g);
                ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert('" + g + "')</script>");
            }
            finally
            {
                cmdProcExec.Dispose();
                oraConn.Close();
                oraConn.Dispose();
                objLog.WriteLog("IndividualOdOverDue.aspx: OdOverdueReport():finally block reached..");
            }
            return dtodReport;
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
           Response.Redirect(Gen.REPORT_MENU,false);
        }
    }

