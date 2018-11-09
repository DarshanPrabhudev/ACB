//*-------------------------------DD issue Register------------------------------------
//*
//* Version       Modified By     Date                  Remarks
//* -------------------------------------------------------------------------------
//*  1.0            ---             -----               -----
//*  2.0          Darshan         17-12-2016            Taken to Ajax and XML Implementation.
//*                                                     correction in original code.
//*--------------------------------------------------------------------------------


using System;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using System.Globalization;
using System.Text.RegularExpressions;

    public partial class Issue : System.Web.UI.Page
    {        
        Log objLog = new Log();
        global objGlobal = new global();
        string g = "";
        XMLMetaMasters objXml = new XMLMetaMasters();

        protected void Page_Load(object sender, EventArgs e)    
        {
            global.checkSession();
            DataTable dtList = new DataTable();
            int c = 0;
            OracleConnection oraConn = new OracleConnection(Session["constring"].ToString());            
            try
            {
                if (!IsPostBack)
                {                    
                    if (oraConn.State != ConnectionState.Open) oraConn.Open();
                    // Added by Darshan
                    objXml.FillMetaXMLDdl(cmbBranch, "BRANCH.xml", "BRAN_SNO", "BRAN_NAME", null);
                    cmbBranch.SelectedValue = Session["BranchLogin"].ToString();
                    txtfrom.Text = objGlobal.GetServerDate();
                    txtto.Text = objGlobal.GetServerDate();
                    if (Convert.ToInt16(cmbBranch.SelectedValue) == 1)
                    {
                        cmbBranch.Enabled = true;
                    }
                    else
                    {
                        cmbBranch.Enabled = false;
                    }
                    dtList.Clear();
                    dtList = objXml.DataTableMetadataXML("DDPOSCHEMESETUP.xml", "DDPS_ACTIVEYN='Y'");
                    //----------------------------
                    c = dtList.Rows.Count;
                    ckhlist.Items.Clear();
                    ckhlist.DataSource = dtList;
                    ckhlist.DataTextField = "DDPS_DES";
                    ckhlist.DataValueField = "DDPS_SNO";
                    ckhlist.DataBind();
                }
            }
            catch (Exception ex)
            {
                g = ex.Message;
                g = Regex.Replace(g, "[^a-zA-Z0-9_]+", " ");
                objLog.WriteLog("issue.aspx: Exception in PageLoad : " + g);
                ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert('" + g + "')</script>");
                return;
            }
            finally
            {
                oraConn.Close();
                oraConn.Dispose();
                dtList.Dispose();
                objLog.WriteLog("issue.aspx: Page_Load-->Page Load events Finally block Executed  ");
            }
        }
        protected void btnok_Click(object sender, EventArgs e)
        {
            string strsql, strdtfrm, strdtTo, blnValid, blndate,abbr = "", abbritm = "", b = "", branch = "", scheme = "",strHeading="";
            int slno = 0, nMaxLen = 18, tn = 0, fn = 0, i = 0, l = 0,n=0,c=0;
            Double ATOT, CTOT, STOT, Service, amt, Charges;
            DataTable dtissue = new DataTable();
            DataTable dtcharges = new DataTable();
            DataTable dtScharges = new DataTable();
            DataTable dtdd = new DataTable();            
            OracleConnection oraConn = new OracleConnection(Session["constring"].ToString()); 
            blnValid = "false";
            blndate = "false";
            strdtfrm = Convert.ToDateTime(txtfrom.Text).ToString("dd/MMM/yyyy");
            strdtTo = Convert.ToDateTime(txtto.Text).ToString("dd/MMM/yyyy");
            try
            {
                if (strdtfrm == "")
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert ('From Date Cannot Be Blank') </script>");
                    blndate = "false";
                }
                else
                {
                    blndate = "true";
                }
                if (blndate == "true" & strdtTo == "")
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert ('From Date Cannot Be Blank') </script>");
                    blndate = "false";
                }
                else
                {
                    blndate = "true";
                }
                if (blndate == "true" & txtFAbbr.Text != "" & txtFNo.Text != "")
                {
                    blnValid = "false";
                    if (txtFAbbr.Text != "" & txtFNo.Text != "")
                    {
                        if (txtFAbbr.Text != txtTAbbr.Text)
                        {
                            ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert ('Abbrevation Should Be Same') </script>");
                            blnValid = "false";
                        }
                        fn = Convert.ToInt32(txtFNo.Text);
                        tn = Convert.ToInt32(txtTNo.Text);
                        if ((fn) > (tn))
                        {
                            ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert ('To Number should be Greater or Equal to From Number') </script>");
                            blnValid = "false";
                        }
                        else
                        {
                            blnValid = "true";
                        }
                    }
                    else
                    {
                        blnValid = "false";
                    }
                    if (blnValid == "false")
                    {
                        return;
                    }
                }                
                for (i = 0; i < ckhlist.Items.Count; i++)
                {
                    if (ckhlist.Items[i].Selected)
                    {
                        abbr += ckhlist.Items[i].Value + ",";
                        abbritm += ckhlist.Items[i].Text + ",";
                    }
                }

                strHeading = "DD Issue Register " + strdtfrm + "+To+" + strdtTo + " ";
                slno = 0;
                b = cmbBranch.SelectedItem.ToString();
                branch = cmbBranch.SelectedValue.ToString();
                l = Convert.ToInt16(abbr.Length);
                scheme = abbr.Substring(0, l - 1);
                strsql = "SELECT DDPD_TRANSLINK,DDPD_SERTRNO,DDPD_ISSUEDATE,DDPD_ABBR || '' || DDPD_LEAFNO AS InstNo,";
                strsql += " DDPD_PARTYNAME,DDPD_INSTAMT,DDPD_INSTCOM,TRNH_TYPE,DDPD_TRANSLINK,";
                strsql += " DDPD_REALISEDDATE,DDPD_REALISEDBANK,TRNH_NARRATION1,";
                strsql += " DDPD_ISSUEDYN,DDPD_STOPPAYYN,DDPD_CANCELYN,STOP_WEF,";
                strsql += " TRNH_VOUCHERDATE , DDPS_GL, DDPS_SL, DDPS_COMMGL, DDPS_COMMSL, DDPS_SERTAXGL, DDPS_SERTAXSL";
                strsql += "  From DDPOSCHEMESETUP,DDPAYORDERH,DDPAYORDERD,STOPPAYMENT,TRANSACTIONH";
                strsql += "  Where DDPD_ISSUEDYN='Y' ";
                strsql += "  And DDPD_LINK=DDPO_SNO";
                strsql += "  And DDPO_INSTLINK = DDPS_SNO";
                strsql += "  And DDPS_SNO IN (" + scheme + ") ";
                if (blnValid == "true")
                {
                    strsql += "  And DDPD_ABBR = '" + txtFAbbr.Text + "' ";
                    strsql += "  And DDPD_LEAFNO BETWEEN " + txtFNo.Text + " AND " + txtTNo.Text + "";
                }
                if (blndate == "true")
                {
                    strsql += "  And DDPD_ISSUEDATE BETWEEN to_date('" + strdtfrm + "','dd-MM-yyyy') AND to_date('" + strdtTo + "','dd-MM-yyyy')";
                }
                strsql += "  And DDPD_SNO = STOP_CHQDLINK(+)";
                strsql += "  And STOP_WET IS NULL";
                strsql += "  And TRNH_VOUCHERNO = DDPD_TRANSLINK";
                strsql += "  And TRNH_STATUS IN('R','U')";
                strsql += "  And TRNH_BRANCH= '" + branch + "'";
                strsql += " Order by DDPD_ABBR,DDPD_LEAFNO,TRNH_VOUCHERNO";
                OracleDataAdapter odaissue = new OracleDataAdapter(strsql, oraConn);
				objLog.WriteLog("Issue.aspx"+strsql);
                odaissue.Fill(dtissue);
                if (dtissue.Rows.Count > 0)
                {
                    dtdd.Columns.Add(new DataColumn("Sl No.", typeof(string)));
                    dtdd.Columns.Add(new DataColumn("Issued Date", typeof(string)));
                    dtdd.Columns.Add(new DataColumn("Instrument", typeof(string)));
                    dtdd.Columns.Add(new DataColumn("Type", typeof(string)));
                    dtdd.Columns.Add(new DataColumn("Amount", typeof(string)));
                    dtdd.Columns.Add(new DataColumn("Charges", typeof(string)));
                    dtdd.Columns.Add(new DataColumn("Service Charges", typeof(string)));
                    dtdd.Columns.Add(new DataColumn("Narration", typeof(string)));
                    n = 0;
                    c = dtissue.Rows.Count;
                    //TOT = 0.0;
                    ATOT = 0.0;
                    CTOT = 0.0;
                    STOT = 0.0;

                    for (i = 0; i < c; i++)
                    {

                        DataRow clrrow = dtdd.NewRow();

                        slno += 1;
                        clrrow["Sl No."] = slno.ToString();
                        if (Dbfield_to_string(dtissue.Rows[n]["DDPD_ISSUEDATE"]) != "")
                        {
                            clrrow["Issued Date"] = (dtissue.Rows[n]["DDPD_ISSUEDATE"].ToString().Substring(0, 10));
                        }
                        if (Dbfield_to_string(dtissue.Rows[n]["InstNo"]) != "")
                        {

                            clrrow["Instrument"] = dtissue.Rows[n]["InstNo"].ToString();
                        }
                        if (Dbfield_to_string(dtissue.Rows[n]["TRNH_TYPE"]) != "")
                        {
                            if (dtissue.Rows[n]["TRNH_TYPE"].ToString() == "C")
                            {
                                clrrow["Type"] = "CASH";
                            }
                            else
                            {
                                clrrow["Type"] = "TRANSFER";
                            }
                        }
                        if (Dbfield_to_string(dtissue.Rows[n]["DDPD_INSTAMT"]) != "")
                        {
                            ATOT = ATOT + Convert.ToDouble(dtissue.Rows[n]["DDPD_INSTAMT"].ToString());
                            amt = Convert.ToDouble(dtissue.Rows[n]["DDPD_INSTAMT"].ToString());
                            clrrow["Amount"] = String.Format(new CultureInfo("hi-IN", false), "{0:0,0.00}", amt);
                        }
                        strsql = "SELECT TRND_AMOUNT FROM TRANSACTIOND WHERE TRND_GLLINK =  '" + dtissue.Rows[n]["DDPS_COMMGL"].ToString() + "'";
                        strsql += "  AND TRND_VOUCHERLINK =  '" + dtissue.Rows[n]["DDPD_TRANSLINK"].ToString() + "'";
                        OracleDataAdapter odacharges = new OracleDataAdapter(strsql, oraConn);
                        dtcharges.Clear();
                        odacharges.Fill(dtcharges);

                        if (dtcharges.Rows.Count > 0)
                        {
                            if (Dbfield_to_string(dtcharges.Rows[0]["TRND_AMOUNT"]) != "")
                            {
                                CTOT = CTOT + Convert.ToDouble(dtcharges.Rows[0]["TRND_AMOUNT"].ToString());
                                Charges = Convert.ToDouble(dtcharges.Rows[0]["TRND_AMOUNT"].ToString());
                                clrrow["Charges"] = String.Format(new CultureInfo("hi-IN", false), "{0:0,0.00}", Charges);
                            }                            
                        }
                        else
                        {
                            clrrow["Charges"] = String.Format(new CultureInfo("hi-IN", false), "{0:0,0.00}", 0.00);
                        }
                        strsql = "select TRND_VOUCHERLINK, TRND_AMOUNT";
                        strsql += "  FROM TRANSACTIOND";
                        strsql += "  Where TRND_VOUCHERLINK = '" + dtissue.Rows[n]["DDPD_TRANSLINK"].ToString() + "'";
                        strsql += "  AND TRND_GLLINK ='" + dtissue.Rows[n]["DDPS_SERTAXGL"].ToString() + "'";
                        OracleDataAdapter odaScharges = new OracleDataAdapter(strsql, oraConn);
                        dtScharges.Clear();
                        odaScharges.Fill(dtScharges);

                        if (dtScharges.Rows.Count > 0)
                        {
                            if (Dbfield_to_string(dtScharges.Rows[0]["TRND_AMOUNT"]) != "")
                            {
                                STOT = STOT + Convert.ToDouble(dtScharges.Rows[0]["TRND_AMOUNT"].ToString());
                                Service = Convert.ToDouble(dtScharges.Rows[0]["TRND_AMOUNT"].ToString());
                                clrrow["Service Charges"] = String.Format(new CultureInfo("hi-IN", false), "{0:0,0.00}", Service);
                            }
                        }
                        else
                        {
                            clrrow["Service Charges"] = String.Format(new CultureInfo("hi-IN", false), "{0:0,0.00}", 0.00);
                        }

                        if (Dbfield_to_string(dtissue.Rows[n]["TRNH_NARRATION1"]) != "")
                        {
                            if (dtissue.Rows[n]["TRNH_NARRATION1"].ToString().Length < 18)
                                nMaxLen = dtissue.Rows[n]["TRNH_NARRATION1"].ToString().Length;
                            clrrow["Narration"] = dtissue.Rows[n]["TRNH_NARRATION1"].ToString().Substring(0, nMaxLen).ToString();
                        }
                        dtdd.Rows.Add(clrrow);
                        n += 1;
                    }
                    if (dtissue.Rows.Count > 0)
                    {
                        DataRow clrrow1 = dtdd.NewRow();
                        clrrow1["Type"] = "Total";
                        clrrow1["Amount"] = String.Format(new CultureInfo("hi-IN", false), "{0:0,0.00}", ATOT);
                        clrrow1["Charges"] = String.Format(new CultureInfo("hi-IN", false), "{0:0,0.00}", CTOT);
                        clrrow1["Service Charges"] = String.Format(new CultureInfo("hi-IN", false), "{0:0,0.00}", STOT);
                        dtdd.Rows.Add(clrrow1);
                    }
                    if (dtdd.Rows.Count > 0)
                    {
                        //global.Loginconn = Session["constring"].ToString();
                        //GlobalConstants.pagename = HttpContext.Current.Request.Url.AbsoluteUri;
                        //GlobalConstants.branch = cmbBranch.SelectedItem.Text;
                        //GlobalConstants.strBnkName = global.BankName();  //cal bank name
                        Session["ddissue"] = dtdd;
                        Response.Redirect("cdis.aspx?branch=" + cmbBranch.SelectedItem.Text + "&Comp=" + global.BankName() + "&Head=" + strHeading);
                    }
                }
            }
            catch (Exception ex)
            {
                g = ex.Message;
                g = Regex.Replace(g, "[^a-zA-Z0-9_]+", " ");
                objLog.WriteLog("issue.aspx: Exception in PageLoad : " + g);
                ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert('" + g + "')</script>");
                return;
            }
            finally
            {
                oraConn.Close();
                oraConn.Dispose();
                dtissue.Dispose();
                dtcharges.Dispose();
                dtScharges.Dispose();
                dtdd.Dispose();
                objLog.WriteLog("issue.aspx: btnOk_Click:finally block reached..");
            }             
        }
        private string Dbfield_to_string(object DBField)
        {
            string Dbfield_to_string = "";
            // conversion to a string
            if (DBField == null)
                Dbfield_to_string = "";
            else
                Dbfield_to_string = DBField.ToString();

            return Dbfield_to_string;
        }

        protected void btnHome_Click(object sender, EventArgs e)
        {
           Response.Redirect(Gen.REPORT_MENU,false);
        }
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            foreach (ListItem li in ckhlist.Items)
            {
                li.Selected = true;
            }
        }
        protected void btnclr_Click(object sender, EventArgs e)
        {
            foreach (ListItem li in ckhlist.Items)
            {
                li.Selected = false;
            }
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
           Response.Redirect(Gen.REPORT_MENU,false);
        }
}

