using System;
using System.Data.OracleClient;
using System.Data;
using System.Text.RegularExpressions;
using System.Globalization;
//using System.Collections;
//using System.Configuration;
//using System.Linq;
//using System.Web;
//using System.Web.Security;
//using System.Web.UI;
//using System.Web.UI.HtmlControls;
//using System.Web.UI.WebControls;
//using System.Web.UI.WebControls.WebParts;
//using System.Xml.Linq;
//using DAL;
//using Bal;



    public partial class bankwiseClearing : System.Web.UI.Page
    {
        global objGlobal = new global();
        Log objLog = new Log();
        XMLMetaMasters objXml = new XMLMetaMasters();
        string strsql = "", g = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            global.checkSession();
            OracleConnection oraConn = new OracleConnection(Session["constring"].ToString());
            try
            {
                if (!Page.IsPostBack)
                {
                    if (oraConn.State != ConnectionState.Open) oraConn.Open();
                    // Added by Darshan
                    objXml.FillMetaXMLDdl(ddlBranch, "BRANCH.xml", "BRAN_SNO", "BRAN_NAME", null);
                    ddlBranch.SelectedValue = Session["BranchLogin"].ToString();
                    txtfrom.Text = objGlobal.GetServerDate();
                    txtto.Text = objGlobal.GetServerDate();
                    if (Convert.ToInt16(ddlBranch.SelectedValue) == 1)
                    {
                        ddlBranch.Enabled = true;
                    }
                    else
                    {
                        ddlBranch.Enabled = false;
                    }
                    //-----------------------------
                }
            }
            catch (Exception ex)
            {
                g = ex.Message;
                g = Regex.Replace(g, "[^a-zA-Z0-9_]+", " ");
                objLog.WriteLog("bankwiseClearing.aspx: Exception in PageLoad : " + g);
                ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert('" + g + "')</script>");
                return;
            }
            finally
            {
                oraConn.Close();
                oraConn.Dispose();
                objLog.WriteLog("bankwiseClearing.aspx: Page_Load-->Page Load events Finally block Executed  ");
            }
        }
        protected void btnok_Click(object sender, EventArgs e)
        {
            OracleConnection oraConn = new OracleConnection(Session["constring"].ToString());
            DataTable dtbankwiseclr = new DataTable();
            DataTable dtbankwiseclr1 = new DataTable();
            DataTable dtclr = new DataTable();
            int n = 0, c = 0, i = 0, c1 = 0, slno = 0;
            string branch = "", b = "", strdtfrm = "", strdtTo = "", strHeading="";
            double TOT=0.00, TOT1=0.00, NOOFINST=0.00, NOOFINST1=0.00, GTOT=0.00, NGTOT=0.00;
            Double amt1 = 0.00;
            try
            {
                // GlobalConstants.StrHeading = "Bankwise Consolidated Clearing Inward From+" + strdtfrm + "+To+" + strdtTo + " ";
                strdtfrm = txtfrom.Text;
                strdtTo = txtto.Text;
                slno = 0;
                branch = ddlBranch.SelectedValue.ToString();
                b = ddlBranch.SelectedItem.ToString();
                if (oraConn.State != ConnectionState.Open) oraConn.Open();
                strsql = "SELECT COUNT(CLEA_VOUCHERNO)AS NOOFINST, SUM(CLEA_AMOUNT)AS AMOUNT,";
                strsql += " BANK_NAME FROM CLEARING, BANK WHERE CLEA_DNBANK = BANK_SNO(+)";                
                if (ckhio.Checked == true)
                {
                    strsql += "   AND CLEA_INOUT='I'";
                    strHeading = "Bankwise Consolidated Clearing Inward AS ON+" + strdtTo + " ";
                }
                else
                {
                    strsql += "   AND CLEA_INOUT='O'";
                    strHeading = "Bankwise Consolidated Clearing Outward AS ON+" + strdtTo + " ";
                }
                strsql += "  AND CLEA_RELEASEDYN IN('Y')";
                strsql += "  AND TO_DATE(TO_CHAR(CLEA_VOUCHERDATE,'DD-MON-YYYY'))  >= TO_DATE('" + strdtfrm + "','DD-MM-YYYY')";
                strsql += "  AND TO_DATE(TO_CHAR(CLEA_VOUCHERDATE,'DD-MON-YYYY'))  <= TO_DATE('" + strdtTo + "','DD-MM-YYYY')";
                strsql += "  AND CLEA_BRANCH = '" + branch + "'";
                strsql += "  AND NVL(BANK_CURRENTBANKORBRANCH,'N')<>'Y'";
                strsql += "  GROUP BY BANK_SNO,BANK_NAME ORDER BY BANK_SNO";                
                OracleDataAdapter odabankwiseclr = new OracleDataAdapter(strsql, oraConn);
                odabankwiseclr.Fill(dtbankwiseclr);

                dtclr.Columns.Add(new DataColumn("Sl No.", typeof(string)));
                dtclr.Columns.Add(new DataColumn("Bank Name", typeof(string)));
                dtclr.Columns.Add(new DataColumn("No.Inst.", typeof(decimal)));
                dtclr.Columns.Add(new DataColumn("Total Amount", typeof(decimal)));
                n = 0;
                c = dtbankwiseclr.Rows.Count;
                TOT = 0.0;
                NOOFINST = 0.0;
                for (i = 0; i < c; i++)
                {
                    DataRow clrrow = dtclr.NewRow();
                    slno += 1;
                    clrrow["Sl No."] = slno.ToString();

                    if (Dbfield_to_string(dtbankwiseclr.Rows[n]["bank_name"]) != "")
                    {
                        //if (dtbankwiseclr.Rows[n]["bank_name"].ToString().Length < 18)
                        //    nMaxLen = dtbankwiseclr.Rows[n]["bank_name"].ToString().Length;
                        clrrow["Bank Name"] = dtbankwiseclr.Rows[n]["bank_name"].ToString();//.Substring(0, nMaxLen).ToString();
                        //clrrow["Bank Name"] = (dtbankwiseclr.Rows[n]["bank_name"].ToString());
                    }
                    if (Dbfield_to_string(dtbankwiseclr.Rows[n]["NoOfInst"]) != "")
                    {
                        NOOFINST += Convert.ToDouble(dtbankwiseclr.Rows[n]["NoOfInst"].ToString());
                        clrrow["No.Inst."] = dtbankwiseclr.Rows[n]["NoOfInst"].ToString();
                    }
                    if (Dbfield_to_string(dtbankwiseclr.Rows[n]["Amount"]) != "")
                    {
                        TOT += Convert.ToDouble(dtbankwiseclr.Rows[n]["Amount"].ToString());
                        Double amt = Convert.ToDouble(dtbankwiseclr.Rows[n]["Amount"].ToString());
                        clrrow["Total Amount"] = String.Format(new CultureInfo("hi-IN", false), "{0:0,0.00}", amt);
                        clrrow["Total Amount"] = String.Format(new CultureInfo("hi-IN", false), "{0:0,0.00}", clrrow["Total Amount"]);
                    }
                    dtclr.Rows.Add(clrrow);
                    n += 1;
                }
                if (dtbankwiseclr.Rows.Count > 0)
                {
                    DataRow clrrow1 = dtclr.NewRow();
                    clrrow1["Bank Name"] = "Total";
                    clrrow1["No.Inst."] = NOOFINST;
                    clrrow1["Total Amount"] = (TOT);
                    dtclr.Rows.Add(clrrow1);
                }

                strsql = "SELECT COUNT(CLEA_VOUCHERNO)AS NOOFINST, SUM(CLEA_AMOUNT)AS AMOUNT,";
                strsql += " BANK_NAME  FROM CLEARING, BANK WHERE CLEA_DNBANK = BANK_SNO(+)";
                if (ckhio.Checked == true)
                {
                    strsql += "   AND CLEA_INOUT='I'";
                }
                else
                {
                    strsql += "   AND CLEA_INOUT='O'";
                }
                strsql += "  AND CLEA_RELEASEDYN IN ('Y')";
                strsql += "  AND TO_DATE(TO_CHAR(clea_voucherdate,'DD-MON-YYYY'))  >= TO_DATE('" + strdtfrm + "','DD-MM-YYYY')";
                strsql += "  AND TO_DATE(TO_CHAR(clea_voucherdate,'DD-MON-YYYY'))  <= TO_DATE('" + strdtTo + "','DD-MM-YYYY')";
                strsql += "  AND CLEA_BRANCH = '" + branch + "'";
                strsql += "  AND NVL(BANK_CURRENTBANKORBRANCH,'N')<>'N'";
                strsql += "  GROUP BY BANK_SNO,BANK_NAME  ORDER BY BANK_SNO";
                
                OracleDataAdapter odabankwiseclr1 = new OracleDataAdapter(strsql, oraConn);
                odabankwiseclr1.Fill(dtbankwiseclr1);
                c1 = dtbankwiseclr1.Rows.Count;
                n = 0;
                TOT1 = 0.0;
                NOOFINST1 = 0.0;
                for (i = 0; i < c1; i++)
                {

                    DataRow clrrow2 = dtclr.NewRow();

                    slno += 1;
                    clrrow2["Sl No."] = slno.ToString();

                    if (Dbfield_to_string(dtbankwiseclr1.Rows[n]["bank_name"]) != "")
                    {
                        clrrow2["Bank Name"] = (dtbankwiseclr1.Rows[n]["bank_name"].ToString());
                    }
                    if (Dbfield_to_string(dtbankwiseclr1.Rows[n]["NoOfInst"]) != "")
                    {
                        NOOFINST1 += Convert.ToDouble(dtbankwiseclr1.Rows[n]["NoOfInst"].ToString());
                        clrrow2["No.Inst."] = dtbankwiseclr1.Rows[n]["NoOfInst"].ToString();
                    }
                    if (Dbfield_to_string(dtbankwiseclr1.Rows[n]["Amount"]) != "")
                    {
                        TOT1 += Convert.ToDouble(dtbankwiseclr1.Rows[n]["Amount"].ToString());
                        amt1 = Convert.ToDouble(dtbankwiseclr1.Rows[n]["Amount"].ToString());
                        //clrrow2["Total Amount"] = String.Format(new CultureInfo("hi-IN", false), "{0:0,0.00}", amt1);
                        clrrow2["Total Amount"] = (amt1).ToString("0,0.00", CultureInfo.CreateSpecificCulture("hi-IN"));
                    }

                    dtclr.Rows.Add(clrrow2);
                    n += 1;

                }
                if (dtbankwiseclr1.Rows.Count > 0)
                {
                    DataRow clrrow3 = dtclr.NewRow();
                    clrrow3["Bank Name"] = "Total";
                    clrrow3["No.Inst."] = NOOFINST;
                    clrrow3["Total Amount"] = (TOT).ToString("0,0.00", CultureInfo.CreateSpecificCulture("hi-IN"));
                    dtclr.Rows.Add(clrrow3);
                }
                GTOT = TOT + TOT1;
                NGTOT = NOOFINST + NOOFINST1;
                DataRow clrrow4 = dtclr.NewRow();
                clrrow4["Bank Name"] = "Grand Total";
                clrrow4["No.Inst."] = NGTOT;
                clrrow4["Total Amount"] = (GTOT).ToString("0,0.00", CultureInfo.CreateSpecificCulture("hi-IN"));
                dtclr.Rows.Add(clrrow4);


                if (dtclr.Rows.Count > 0)
                {
                    //GlobalConstants.branch = ddlBranch.SelectedItem.Text;
                    //GlobalConstants.strBnkName = global.BankName();  //cal bank name
                    Session["clearingbnk"] = dtclr;
                    Response.Redirect("cdis.aspx?branch=" + ddlBranch.SelectedItem.Text + "&Comp=" + global.BankName()+ "&Head=" + strHeading);
                }
            }
            catch (Exception ex)
            {
                g = ex.Message;
                g = Regex.Replace(g, "[^a-zA-Z0-9_]+", " ");
                objLog.WriteLog("bankwiseClearing.aspx: Exception in btnOk_Click : " + g);
                ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert('" + g + "')</script>");
                return;
            }
            finally
            {
                oraConn.Close();
                oraConn.Dispose();
                dtbankwiseclr.Dispose();
                dtbankwiseclr1.Dispose();
                dtclr.Dispose();
                objLog.WriteLog("bankwiseClearing.aspx: btnOk_Click:finally block reached..");
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
        protected void ckhio_CheckedChanged(object sender, EventArgs e)
        {
            if (ckhio.Checked == true)
            {
                lblio.Text = "Inward";
            }
            else
            {
                lblio.Text = "Outward";
            }
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
           Response.Redirect(Gen.REPORT_MENU,false);
        }
}

