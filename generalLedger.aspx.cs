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
using DAL;
using Bal;
using System.Data.OracleClient;


    public partial class generalLedger : System.Web.UI.Page
    {
        string strFromDate, strdtfrm, strdtTo, strSQL, GenSno, branch, strdate2, Accdes, strDRCR, sd, intGllink;
        Double DTOT, Credit, Debit, CTOT, BAL, strOpBalance, GlClosingBal, strDebit, strCredit, CATOT, TRTOT, CLTOT;
        int GetFinancialYear, n;
        DateTime d,dtdate;
        DataRow genrow;
         DataRow genrowd;
         DataTable dtGL = new DataTable();
         DataTable dtscheme = new DataTable();
         DataTable dtBranch = new DataTable();
         DataTable dtGenD = new DataTable();
         DataTable dtGenSD = new DataTable();
         DataTable dtGenSumC = new DataTable();
         DataTable dttemp = new DataTable();
         DataTable dtName = new DataTable();
         DataTable dtTransaction = new DataTable();
         DataTable dtadded = new DataTable();
         DataTable dtOpeningBalance = new DataTable();
        DAL.conn objconn = new DAL.conn();
        OracleConnection oraConn = new OracleConnection();
        DAL.Validation objValid = new DAL.Validation();

        protected void Page_Load(object sender, EventArgs e)
        {
            // WriteLog("general Ledger.aspx:Page_Load");
            if (Session.Contents.Count == 0)
            {
                Response.Redirect("ShowMessage.aspx?msg=" + GlobalConstants.msg);
            }
            else
            {
                if (!Page.IsPostBack)
                {
                    OracleConnection oraConn = new OracleConnection(Session["constring"].ToString());

                    // WriteLog("Session-constring" + Session["constring"].ToString());

                    if (oraConn.State != ConnectionState.Open) oraConn.Open();

                    strSQL = "select bran_sno,bran_name from MASTER.branch ORDER BY bran_sno";
                    OracleDataAdapter oda = new OracleDataAdapter(strSQL, oraConn);
                    WriteLog("strSQL" + strSQL);

                    oda.Fill(dtBranch);
                    ddBranch.DataSource = dtBranch;
                    ddBranch.DataTextField = "bran_name";
                    ddBranch.DataValueField = "bran_sno";
                    ddBranch.DataBind();
                    ddBranch.SelectedValue = (GlobalConstants.branchname.ToString());



                    Bal.Gen objfilldate = new Bal.Gen();
                    objfilldate.Call_Date(ddfrmday, ddfrmMonth, ddfrmYear);

                    Bal.Gen objfilldate1 = new Bal.Gen();
                    objfilldate1.Call_Date(ddtoday, ddtoMonth, ddtoYear);


                    strSQL = "select ACCM_DESCRIPTION , ACCM_SNO ";
                    strSQL = strSQL + " From accountmaster";
                    strSQL = strSQL + " order by ACCM_DESCRIPTION";
                    WriteLog("strSQL" + strSQL);

                    OracleDataAdapter odascheme = new OracleDataAdapter(strSQL, oraConn);
                    odascheme.Fill(dtscheme);
                    ddschemeName.DataSource = dtscheme;
                    ddschemeName.DataTextField = "ACCM_DESCRIPTION";
                    ddschemeName.DataValueField = "ACCM_SNO";
                    ddschemeName.DataBind();
                    //=Added on 9/04/2014 by husna 
                    if (!objValid.Valid_Activity(30005, Session["gblnSupervisor"].ToString(), oraConn, Convert.ToInt32(Session["gUserId"])))
                    {
                        ddBranch.Enabled = false;
                    }
                    else
                    {
                        ddBranch.Enabled = true;
                    }
                }

                strdtfrm = ddfrmday.SelectedItem.Value + "-" + ddfrmMonth.SelectedItem.Text + "-" + ddfrmYear.SelectedItem.Value;
                strdtTo = ddtoday.SelectedItem.Value + "-" + ddtoMonth.SelectedItem.Text + "-" + ddtoYear.SelectedItem.Value;
                WriteLog("strdtfrm" + strdtfrm);
                WriteLog("strdtTo" + strdtfrm);
            }
        }

        protected void btnok_Click(object sender, EventArgs e)
        {
            try
            {
                OracleConnection oraConn = new OracleConnection(Session["constring"].ToString());
                strOpBalance = 0.0;
                GlClosingBal = 0.0;
                // strOpBalance =0.0;
                strDebit = 0.0;
                strCredit = 0.0;
                GenSno = ddschemeName.SelectedValue.ToString();
                Accdes = ddschemeName.SelectedItem.Text;
                branch = ddBranch.SelectedValue.ToString();

                //strdate2 = Convert.ToString(Convert.ToDateTime(strdtfrm).AddDays(-1).ToString("dd/MM/yyyy"));
                //d = Convert.ToDateTime(strdtfrm).AddDays(-1).ToString("dd/MM/yyyy");
                d = Convert.ToDateTime(strdtfrm).AddDays(-1);

                //dtdate = Convert.ToDateTime(strdate2);


                //d = Convert.ToDateTime(strdate2);

                int m = d.Month;
                int y = d.Year;

                if (txtFromNo.Text == "")
                {
                    intGllink = GenSno;
                }
                else
                {
                    intGllink = txtFromNo.Text;
                }

                strSQL = "Select SITP_LONGVALUE,SITP_TEXTVALUE";
                strSQL = strSQL + " From SiteParameters";
                strSQL = strSQL + " Where SITP_NAME = 'FINANCIAL YEAR'";


                OracleDataAdapter odatemp = new OracleDataAdapter(strSQL, oraConn);
                odatemp.Fill(dttemp);


                strSQL = "select accm_description as NAME,ACCM_TYPE from accountmaster";
                strSQL = strSQL + " Where ACCM_SNO = '" + intGllink + "'";
                //WriteLog("btnOk strSQL" + strSQL);

                OracleDataAdapter odaName = new OracleDataAdapter(strSQL, oraConn);
                odaName.Fill(dtName);


                if (m < Convert.ToInt16(dttemp.Rows[0]["SITP_LONGVALUE"]))
                {
                    string year = (y - 1).ToString();
                    strFromDate = "01/" + dttemp.Rows[0]["SITP_LONGVALUE"] + "/" + year;
                }
                else
                {
                    strFromDate = "01/" + dttemp.Rows[0]["SITP_LONGVALUE"] + "/" + y;

                }

                DateTime strDate = Convert.ToDateTime(strFromDate);
                int intMonth = strDate.Month;
                int GetFinancialYear = strDate.Year;

                if (intMonth < Convert.ToInt16(dttemp.Rows[0]["SITP_LONGVALUE"]))
                {
                    GetFinancialYear = GetFinancialYear - 1;
                }


                strSQL = "Select GSOB_DRCR, GSOB_AMOUNT as Amount From GLSLOpeningBalance";
                strSQL = strSQL + " Where GSOB_GLLINK = ";
                strSQL = strSQL + "" + intGllink + " And ";
                strSQL = strSQL + " GSOB_BRANCH = ";
                strSQL = strSQL + "" + branch + "";
                strSQL = strSQL + " AND  GSOB_MIGDATE <=  To_date('" + strFromDate + "','dd-mm-yyyy')";
                strSQL = strSQL + " And GSOB_FINANYEAR = ";
                strSQL = strSQL + "'" + strDate.Year + "'"; //GetFinancialYear + "'";
                strSQL = strSQL + "Order by GSOB_DRCR ";

                WriteLog("gl sl opening balance" + strSQL);

                OracleDataAdapter odaOpeningBalance = new OracleDataAdapter(strSQL, oraConn);
                odaOpeningBalance.Fill(dtOpeningBalance);




                if (dtOpeningBalance.Rows.Count > 0)
                {
                    strOpBalance = Convert.ToDouble(dtOpeningBalance.Rows[0]["Amount"].ToString());

                    if (dtOpeningBalance.Rows[0]["GSOB_DRCR"].ToString() == "C")
                    {
                        strOpBalance = Convert.ToDouble(dtOpeningBalance.Rows[0]["Amount"].ToString()) * -1;
                    }
                }
                //    //'***  transaction
                strSQL = "select sum(decode(TRND_DEBITORCREDIT,'D',TRND_AMOUNT)) AS trndamountdr, sum(decode(TRND_DEBITORCREDIT,'C',TRND_AMOUNT)) AS trndamountcr from transactionh,transactiond";
                strSQL = strSQL + " Where trnh_voucherno = trnd_voucherlink";
                strSQL = strSQL + " and trnd_gllink = " + intGllink + "";
                strSQL = strSQL + " AND TRNH_VOUCHERDATE >= TO_DATE('" + strFromDate + "','DD-MM-YYYY')";
                //strSQL = strSQL + " AND TRNH_VOUCHERDATE <= TO_DATE('" + dtdate.ToString("dd-M-yyyy") + "','DD-MM-YYYY')";
                strSQL = strSQL + " AND TRNH_VOUCHERDATE <= TO_DATE('" + d.ToString("dd-M-yyyy") + "','DD-MM-YYYY')";
                strSQL = strSQL + " And Trnh_Branch = " + branch + "";
                strSQL = strSQL + " and trnh_status = 'R'";
                OracleDataAdapter odaTransaction = new OracleDataAdapter(strSQL, oraConn);


                odaTransaction.Fill(dtTransaction);


                if (Dbfield_to_string(dtTransaction.Rows[0]["trndamountdr"]) != "")
                {
                    strDebit = Convert.ToDouble(dtTransaction.Rows[0]["trndamountdr"].ToString());
                }
                if (Dbfield_to_string(dtTransaction.Rows[0]["trndamountcr"]) != "")
                {
                    strCredit = Convert.ToDouble(dtTransaction.Rows[0]["trndamountcr"].ToString());
                }


                string strTmp = "31/MAR/" + y;
                if (dtName.Rows[0]["ACCM_TYPE"].ToString() == "I" || dtName.Rows[0]["ACCM_TYPE"].ToString() == "E")
                {
                    if (strFromDate == strTmp)
                    {
                        strOpBalance = 0;
                    }
                    else
                    {
                        //cOMM & ADDED BY HUSNA ON 5/4/2014
                        GlClosingBal = strOpBalance + strDebit - strCredit;
                        strOpBalance = strOpBalance + strDebit - strCredit;

                    }
                }
                else
                {
                    //cOMM & ADDED BY HUSNA ON 5/4/2014                    
                    GlClosingBal = strOpBalance + strDebit - strCredit;
                    strOpBalance = strOpBalance + strDebit - strCredit;

                }



                DTOT = 0.0;
                CTOT = 0.0;

                strSQL = "select accm_code,accm_description as NAME,ACCM_TYPE from accountmaster";
                if (txtFromNo.Text == "")
                {
                    strSQL = strSQL + " Where accm_description = '" + Accdes + "'";
                }
                else
                {
                    strSQL = strSQL + " Where accm_sno  = " + txtFromNo.Text + "";
                }
                //WriteLog("btnOk accm_code,accm_description  strSQL" + strSQL);

                OracleDataAdapter odaGL = new OracleDataAdapter(strSQL, oraConn);
                odaGL.Fill(dtGL);




                if ((rbtnSD.Text).Trim() == ("S").Trim())
                {
                    CATOT = 0.0;
                    TRTOT = 0.0;
                    CLTOT = 0.0;
                    sd = "S";
                    GlobalConstants.StrHeading = "General Ledger (Summary) " + "+From+" + strdtfrm + "+To+" + strdtTo + " ";

                    dtGenSumC.Columns.Add(new DataColumn("Date", typeof(string)));
                    dtGenSumC.Columns.Add(new DataColumn("Debit", typeof(string)));
                    dtGenSumC.Columns.Add(new DataColumn("Credit", typeof(string)));
                    dtGenSumC.Columns.Add(new DataColumn("Balance", typeof(string)));

                    strSQL = "select TRNH_TYPE,TRNH_VOUCHERDATE,TRND_DEBITORCREDIT, sum(TRND_AMOUNT) as Amount";
                    strSQL = strSQL + " From transactionh, transactiond";
                    strSQL = strSQL + " Where trnh_voucherno = trnd_voucherlink";
                    strSQL = strSQL + " and trnh_status = 'R'";


                    if (txtFromNo.Text == "")
                    {
                        strSQL = strSQL + " and trnd_gllink = " + GenSno + "";
                    }
                    else
                    {
                        strSQL = strSQL + " and trnd_gllink = " + txtFromNo.Text + "";
                    }
                    strSQL = strSQL + " and trnh_branch = " + branch + " ";
                    strSQL = strSQL + " AND  TO_DATE(TO_CHAR(TRNH_VOUCHERDATE,'DD-MON-YYYY'))>=  TO_DATE('" + strdtfrm + "','DD-MM-YYYY')";
                    strSQL = strSQL + " AND  TO_DATE(TO_CHAR(TRNH_VOUCHERDATE,'DD-MON-YYYY')) <= TO_DATE('" + strdtTo + "','DD-MM-YYYY')";
                    strSQL = strSQL + " group by TRNH_VOUCHERDATE,TRND_DEBITORCREDIT,TRNH_TYPE";
                    strSQL = strSQL + " order by TRNH_VOUCHERDATE,TRND_DEBITORCREDIT";

                    // WriteLog("btnOk - transactionh, transactiond:  strSQL: " + strSQL);

                    OracleDataAdapter odaGenSD = new OracleDataAdapter(strSQL, oraConn);
                    odaGenSD.Fill(dtGenSD);





                    int c = dtGenSD.Rows.Count;
                    n = 0;
                    for (int i = 0; i < c; i++)
                    {

                        strDRCR = "";
                        if ((GlClosingBal) < 0)
                        {
                            strDRCR = "C";
                        }
                        else
                        {
                            strDRCR = "D";
                        }

                        genrow = dtGenSumC.NewRow();
                        if (n.ToString() == "0")
                        {
                            GlobalConstants.Opening = "[" + dtGL.Rows[0]["accm_code"].ToString() + "]" + dtGL.Rows[0]["NAME"].ToString();
                            genrow["Date"] = "Opn Bal:";
                            //cOMM & ADDED BY HUSNA ON 5/4/2014
                            // genrow["Balance"] = String.Format("{0:0.00}", Math.Abs(GlClosingBal)) + "" + strDRCR;
                            genrow["Balance"] = String.Format("{0:0.00}", Math.Abs(strOpBalance)) + "" + strDRCR;
                            //-----------------
                            dtGenSumC.Rows.Add(genrow);
                            genrow = dtGenSumC.NewRow();
                        }

                        if (dtGenSD.Rows[n]["TRNH_TYPE"].ToString() == "C")
                        {

                            CATOT = CATOT + Convert.ToDouble(dtGenSD.Rows[n]["Amount"].ToString());

                        }
                        if (dtGenSD.Rows[n]["TRNH_TYPE"].ToString() == "T")
                        {
                            TRTOT = TRTOT + Convert.ToDouble(dtGenSD.Rows[n]["Amount"].ToString());

                        }
                        if (dtGenSD.Rows[n]["TRNH_TYPE"].ToString() == "L")
                        {

                            CLTOT = CLTOT + Convert.ToDouble(dtGenSD.Rows[n]["Amount"].ToString());
                        }
                        if (Dbfield_to_string(dtGenSD.Rows[n]["TRNH_VOUCHERDATE"]) != "")
                        {
                            genrow["Date"] = String.Format("{0:dd/MM/yyyy}", dtGenSD.Rows[n]["TRNH_VOUCHERDATE"]);
                        }
                        if (Dbfield_to_string(dtGenSD.Rows[n]["TRND_DEBITORCREDIT"]) != "")
                        {
                            if ((rdbtGl.Text).Trim() == ("optCreditTran").Trim())
                            {
                                if (dtGenSD.Rows[n]["TRND_DEBITORCREDIT"].ToString().Trim() == ("C").Trim())
                                {
                                    Credit = 0.0;
                                    Credit = Convert.ToDouble(dtGenSD.Rows[n]["Amount"].ToString());
                                    genrow["Credit"] = String.Format("{0:0.00}", (Credit));
                                    CTOT = CTOT + Convert.ToDouble(dtGenSD.Rows[n]["Amount"].ToString());
                                    //BY HUSNA ON 20/02/2014
                                    //dtGenSD.Rows.Add(genrow);
                                    dtGenSumC.Rows.Add(genrow);
                                    //BY HUSNA ON 20/02/2014
                                }
                                else
                                {
                                    genrow["Credit"] = 0.0;
                                }
                            }

                            if ((rdbtGl.Text).Trim() == ("optDebitTran").Trim())
                            {
                                if (dtGenSD.Rows[n]["TRND_DEBITORCREDIT"].ToString().Trim() == ("D").Trim())
                                {
                                    Debit = 0.0;
                                    Debit = Convert.ToDouble(dtGenSD.Rows[n]["Amount"].ToString());
                                    genrow["Debit"] = String.Format("{0:0.00}", (Debit));
                                    DTOT = DTOT + Convert.ToDouble(dtGenSD.Rows[n]["Amount"].ToString());
                                    dtGenSumC.Rows.Add(genrow);
                                }
                                else
                                {
                                    genrow["Debit"] = 0.0;
                                }
                            }
                            if ((rdbtGl.Text).Trim() == ("optAllDrCr").Trim())
                            {
                                if (dtGenSD.Rows[n]["TRND_DEBITORCREDIT"].ToString().Trim() == ("D").Trim())
                                {
                                    Debit = 0.0;
                                    Debit = Convert.ToDouble(dtGenSD.Rows[n]["Amount"].ToString());
                                    genrow["Debit"] = String.Format("{0:0.00}", (Debit));
                                    DTOT = DTOT + Convert.ToDouble(dtGenSD.Rows[n]["Amount"].ToString());
                                    BAL = BAL + Convert.ToDouble(dtGenSD.Rows[n]["Amount"].ToString());
                                    GlClosingBal = GlClosingBal + Convert.ToDouble(dtGenSD.Rows[n]["Amount"].ToString());
                                    genrow["Balance"] = String.Format("{0:0.00}", Math.Abs(GlClosingBal)) + "" + strDRCR;
                                }
                                else
                                {
                                    Credit = 0.0;
                                    Credit = Convert.ToDouble(dtGenSD.Rows[n]["Amount"].ToString());
                                    genrow["Credit"] = String.Format("{0:0.00}", (Credit));
                                    CTOT = CTOT + Convert.ToDouble(dtGenSD.Rows[n]["Amount"].ToString());
                                    BAL = BAL - Convert.ToDouble(dtGenSD.Rows[n]["Amount"].ToString());
                                    GlClosingBal = GlClosingBal - Convert.ToDouble(dtGenSD.Rows[n]["Amount"].ToString());
                                    genrow["Balance"] = String.Format("{0:0.00}", Math.Abs(GlClosingBal)) + "" + strDRCR;
                                }
                                dtGenSumC.Rows.Add(genrow);
                            }


                        }


                        n = n + 1;
                    }
                }
                else
                {
                    sd = "D";
                    GlobalConstants.StrHeading = "General Ledger (Detail) " + "+From+" + strdtfrm + "+To+" + strdtTo + " ";

                    dtGenSumC.Columns.Add(new DataColumn("Date", typeof(string)));
                    dtGenSumC.Columns.Add(new DataColumn("Instr. No", typeof(string)));
                    dtGenSumC.Columns.Add(new DataColumn("Narration", typeof(string)));
                    dtGenSumC.Columns.Add(new DataColumn("Debit", typeof(string)));
                    dtGenSumC.Columns.Add(new DataColumn("Credit", typeof(string)));
                    dtGenSumC.Columns.Add(new DataColumn("Balance", typeof(string)));
                    strSQL = "SELECT TRNH_VOUCHERNO,TRND_INSTRUMENTNUMBER,TRNH_VOUCHERDATE,TRND_DEBITORCREDIT,TRNH_NARRATION1,";
                    strSQL = strSQL + "TRNH_NARRATION2,TRNH_DRAWER, TRNH_DRAWEE, TRND_AMOUNT AS AMOUNT,TRNH_MODE";
                    strSQL = strSQL + " FROM TRANSACTIONH, TRANSACTIOND";
                    strSQL = strSQL + " WHERE TRNH_VOUCHERNO = TRND_VOUCHERLINK";
                    strSQL = strSQL + " AND TRNH_STATUS = 'R'";
                    if (txtFromNo.Text == "")
                    {
                        strSQL = strSQL + " AND TRND_GLLINK = " + GenSno + "";
                    }
                    else
                    {
                        strSQL = strSQL + " AND TRND_GLLINK = " + txtFromNo.Text + "";
                    }
                    strSQL = strSQL + " AND TRNH_BRANCH = " + branch + " ";
                    strSQL = strSQL + " AND  TO_DATE(TO_CHAR(TRNH_VOUCHERDATE,'DD-MON-YYYY')) >= TO_DATE('" + strdtfrm + "','DD-MM-YYYY')";
                    strSQL = strSQL + " AND  TO_DATE(TO_CHAR(TRNH_VOUCHERDATE,'DD-MON-YYYY')) <= TO_DATE('" + strdtTo + "','DD-MM-YYYY')";
                    strSQL = strSQL + " ORDER BY TRNH_VOUCHERDATE,TRNH_VOUCHERNO";
                    WriteLog("btnOk - transactionh, transactiond: After dtGenSumC. strSQL: " + strSQL);
                    OracleDataAdapter odaGenSD = new OracleDataAdapter(strSQL, oraConn);
                    odaGenSD.Fill(dtGenSD);

                    n = 0;
                    int c1 = dtGenSD.Rows.Count;
                    if (c1 > 0)
                    {
                        for (int i = 0; i < c1; i++)
                        {



                            strDRCR = "";
                            if ((GlClosingBal) < 0)
                            {
                                strDRCR = " C";
                            }
                            else
                            {
                                strDRCR = " D";
                            }

                            genrowd = dtGenSumC.NewRow();
                            if (n.ToString() == "0")
                            {
                                GlobalConstants.Opening = "[" + dtGL.Rows[0]["accm_code"].ToString() + "]" + dtGL.Rows[0]["NAME"].ToString();
                                genrowd["Date"] = "Opn Bal:";
                                //COMM & ADDED BY NAYANA
                                //genrowd["Balance"] = String.Format("{0:0.00}", Math.Abs(GlClosingBal)) + "" + strDRCR;
                                genrowd["Balance"] = String.Format("{0:0.00}", Math.Abs(strOpBalance)) + "" + strDRCR;
                                dtGenSumC.Rows.Add(genrowd);
                                genrowd = dtGenSumC.NewRow();
                            }


                            if (Dbfield_to_string(dtGenSD.Rows[n]["TRNH_VOUCHERDATE"]) != "")
                            {
                                genrowd["Date"] = String.Format("{0:dd/MM/yyyy}", dtGenSD.Rows[n]["TRNH_VOUCHERDATE"]);
                            }
                            if (Dbfield_to_string(dtGenSD.Rows[n]["TRND_INSTRUMENTNUMBER"]) != "")
                            {
                                genrowd["Instr. No"] = (dtGenSD.Rows[n]["TRND_INSTRUMENTNUMBER"].ToString());
                            }
                            if ((Dbfield_to_string(dtGenSD.Rows[n]["TRNH_NARRATION1"]) != "") || (Dbfield_to_string(dtGenSD.Rows[n]["TRNH_NARRATION2"]) != ""))
                            {
                                genrowd["Narration"] = (dtGenSD.Rows[n]["TRNH_NARRATION1"].ToString()) + (dtGenSD.Rows[n]["TRNH_NARRATION2"].ToString());
                            }
                            if (Dbfield_to_string(dtGenSD.Rows[n]["TRND_DEBITORCREDIT"]) != "")
                            {
                                if ((rdbtGl.Text).Trim() == ("optCreditTran").Trim())
                                {
                                    if (dtGenSD.Rows[n]["TRND_DEBITORCREDIT"].ToString().Trim() == ("C").Trim())
                                    {
                                        Credit = 0.0;
                                        Credit = Convert.ToDouble(dtGenSD.Rows[n]["Amount"].ToString());
                                        genrowd["Credit"] = String.Format("{0:0.00}", (Credit));
                                        CTOT = CTOT + Convert.ToDouble(dtGenSD.Rows[n]["Amount"].ToString());
                                        dtGenSumC.Rows.Add(genrowd);
                                    }
                                }

                                if ((rdbtGl.Text).Trim() == ("optDebitTran").Trim())
                                {
                                    if (dtGenSD.Rows[n]["TRND_DEBITORCREDIT"].ToString().Trim() == ("D").Trim())
                                    {
                                        Debit = 0.0;
                                        Debit = Convert.ToDouble(dtGenSD.Rows[n]["Amount"].ToString());
                                        genrowd["Debit"] = String.Format("{0:0.00}", (Debit));
                                        DTOT = DTOT + Convert.ToDouble(dtGenSD.Rows[n]["Amount"].ToString());
                                        dtGenSumC.Rows.Add(genrowd);
                                    }
                                }
                                if ((rdbtGl.Text).Trim() == ("optAllDrCr").Trim())
                                {

                                    if (dtGenSD.Rows[n]["TRND_DEBITORCREDIT"].ToString().Trim() == ("D").Trim())
                                    {

                                        Debit = 0.0;
                                        Debit = Convert.ToDouble(dtGenSD.Rows[n]["Amount"].ToString());
                                        genrowd["Debit"] = String.Format("{0:0.00}", (Debit));
                                        DTOT = DTOT + Convert.ToDouble(dtGenSD.Rows[n]["Amount"].ToString());
                                        BAL = BAL + Convert.ToDouble(dtGenSD.Rows[n]["Amount"].ToString());
                                        GlClosingBal = GlClosingBal + Convert.ToDouble(dtGenSD.Rows[n]["Amount"].ToString());
                                        genrowd["Balance"] = String.Format("{0:0.00}", Math.Abs(GlClosingBal)) + "" + strDRCR;
                                    }
                                    else
                                    {
                                        Credit = 0.0;
                                        Credit = Convert.ToDouble(dtGenSD.Rows[n]["Amount"].ToString());
                                        genrowd["Credit"] = String.Format("{0:0.00}", (Credit));
                                        CTOT = CTOT + Convert.ToDouble(dtGenSD.Rows[n]["Amount"].ToString());
                                        BAL = BAL - Convert.ToDouble(dtGenSD.Rows[n]["Amount"].ToString());
                                        GlClosingBal = GlClosingBal - Convert.ToDouble(dtGenSD.Rows[n]["Amount"].ToString());
                                        genrowd["Balance"] = String.Format("{0:0.00}", Math.Abs(GlClosingBal)) + "" + strDRCR;
                                    }
                                    dtGenSumC.Rows.Add(genrowd);
                                }
                            }

                            n = n + 1;

                        }
                    }


                }


                if (dtGenSD.Rows.Count > 0)
                {
                    DataRow genrowtot = dtGenSumC.NewRow();
                    genrowtot["Date"] = "TOTAL";
                    genrowtot["Debit"] = String.Format("{0:0.00}", DTOT);
                    genrowtot["Credit"] = String.Format("{0:0.00}", CTOT);
                    dtGenSumC.Rows.Add(genrowtot);
                    genrowtot = dtGenSumC.NewRow();
                    if ((rbtnSD.Text).Trim() == ("S").Trim())
                    {
                        genrowtot["Date"] = "Cash";
                        genrowtot["Debit"] = String.Format("{0:0.00}", CATOT);
                        dtGenSumC.Rows.Add(genrowtot);
                        genrowtot = dtGenSumC.NewRow();

                        genrowtot["Date"] = "Transfer";
                        genrowtot["Debit"] = String.Format("{0:0.00}", TRTOT);
                        dtGenSumC.Rows.Add(genrowtot);
                        genrowtot = dtGenSumC.NewRow();

                        genrowtot["Date"] = "Clearing";
                        genrowtot["Debit"] = String.Format("{0:0.00}", CLTOT);
                        dtGenSumC.Rows.Add(genrowtot);
                        //   genrowtot = dtGenSumC.NewRow();

                    }



                }
                if (dtGenSD.Rows.Count <= 0)
                {

                    dtadded.Columns.Add("Gl-Name", typeof(string));
                    //dtadded.Columns.Add("Opening Balance", typeof(double));
                    string valgl = "";
                    valgl = "GL-Name  :" + dtGL.Rows[0]["ACCM_CODE"].ToString() + "-" + dtGL.Rows[0]["NAME"].ToString();

                    DataRow rd = dtadded.NewRow();
                    rd["Gl-Name"] = valgl;
                    dtadded.Rows.Add(rd);

                    if (dtOpeningBalance.Rows.Count > 0)
                    {
                        DataRow rd1 = dtadded.NewRow();
                        //COMM & ADDED BY HUSNA ON 5/04/2014
                        //rd1["Gl-Name"] = Convert.ToDouble(dtOpeningBalance.Rows[0][0]);
                        rd1["Gl-Name"] = dtOpeningBalance.Rows[0]["AMOUNT"].ToString() + "   " + dtOpeningBalance.Rows[0]["GSOB_DRCR"].ToString();
                        dtadded.Rows.Add(rd1);
                    }
                    else if (dtOpeningBalance.Rows.Count <= 0)
                    {
                        DataRow rd1 = dtadded.NewRow();
                        rd1["Gl-Name"] = "Opening Balance  :" + strOpBalance;
                        dtadded.Rows.Add(rd1);
                    }




                }

                if (dtGenSumC.Rows.Count > 0 && rbtnSD.SelectedValue == "S")
                {
                    global.Loginconn = Session["constring"].ToString();
                    GlobalConstants.pagename = HttpContext.Current.Request.Url.AbsoluteUri;

                    //Session["LoginBranchName"].ToString() ddBranch.SelectedItem.Text;
                    GlobalConstants.strBnkName = global.BankName();  //cal bank name
                    Session["generalsum"] = dtGenSumC;
                    Session["Title"] = "GENERAL LEDGER";
                    Response.Redirect("cdis.aspx?branch=" + Session["LoginBranchName"].ToString() + "&Comp=" + GlobalConstants.strBnkName + "&Head=" + GlobalConstants.StrHeading + "&gsd=" + sd);
                }
                else if (dtGenSumC.Rows.Count > 0 && rbtnSD.SelectedValue == "D")
                {
                    global.Loginconn = Session["constring"].ToString();
                    GlobalConstants.pagename = HttpContext.Current.Request.Url.AbsoluteUri;

                    //Session["LoginBranchName"].ToString() ddBranch.SelectedItem.Text;
                    GlobalConstants.strBnkName = global.BankName();  //cal bank name
                    Session["generaldetl"] = dtGenSumC;
                    Session["Title"] = "GENERAL LEDGER";
                    Response.Redirect("cdis.aspx?branch=" + Session["LoginBranchName"].ToString() + "&Comp=" + GlobalConstants.strBnkName + "&Head=" + GlobalConstants.StrHeading + "&gsd=" + sd);
                }
                else if (dtGenSumC.Rows.Count <= 0)
                {
                    global.Loginconn = Session["constring"].ToString();
                    GlobalConstants.pagename = HttpContext.Current.Request.Url.AbsoluteUri;

                    //Session["LoginBranchName"].ToString() ddBranch.SelectedItem.Text;
                    GlobalConstants.strBnkName = global.BankName();  //cal bank name
                    Session["Title"] = "GENERAL LEDGER";
                    Session["new"] = dtadded;
                    Response.Redirect("cdis.aspx?branch=" + Session["LoginBranchName"].ToString() + "&Comp=" + GlobalConstants.strBnkName + "&Head=" + GlobalConstants.StrHeading + "&gsd=" + sd);
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('" + ex.Message + "')</script>");
            }
            finally
            {
                OracleConnection.ClearAllPools();
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
           Response.Redirect(global.REPORT_MENU,false);

        }
        public void WriteLog(string logMessage)
        {
            string sLogPath = HttpContext.Current.Server.MapPath("cbslog.txt");
            System.IO.StreamWriter file = new System.IO.StreamWriter(sLogPath, true);
            file.WriteLine(DateTime.Now.ToString() + ":" + logMessage);
            file.Close();
            return;
        }





        protected void btnBack_Click(object sender, EventArgs e)
        {
           Response.Redirect(global.REPORT_MENU,false);

        }
}

