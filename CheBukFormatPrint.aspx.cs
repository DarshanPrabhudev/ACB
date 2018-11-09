////*-------------------------------CHEQUE BOOK PRINTING FORMAT------------------------------------
//*
//* Version     Modified By         Date           Remarks
//* 1.0         ROOPA       21-MAR-2016    Intial Draft-Assisted by Venkatesh 
//*------------------------------------------------------------------------------
//* -----------------------------PSEUDOCODE--------------------------------------
//*  This Form is is used to see the details of the cheque book issued to customer and convert the details to text format.
//*  This Page is Redirected to ChkBukDisp.aspx,which gives the details of cheque book in a grid.
//****** Tables Used ******//
//*.CHEQUEH
//*.DEPOSITMASTER
//*.ODMASTER
//*.LINKMAIN
//*.CLIENTMASTER
//*BRANCH
//*BANK
//*CHEQUEBOOKSTOCK
//*ADDRESS
//*SITEPARAMETERS
//ADDED ON 27-12-2016 BY ROOPA 
//*Columns added for respective tables:-
//1.Siteparameters table, added one more row for IFSC CODE-KARB0000768.
//*2.Added column CHQH_PRINTEDYN to CHEQUEH table.
//*Data Type:-Varchar2 (1byte)
//*3.Added column ADDR_OFFPHNO to ADDRESS table.
//*Data Type:-Varchar2 (20byte)

using System;
using System.Data;
using System.Web.UI;
using System.Data.OracleClient;
using DAL;

public partial class CheBukFormatPrint : System.Web.UI.Page
{
    //AK+LS 02/04/2016 General Inst-->>Declare the var and obj with necessary and sufficient scopes only
    //like  FillScheme objFillScheme = new FillScheme(); 
    //AK+LS 02/04/2016 remove unused VAr and Obj like below
    // DataTable dtScheme = new DataTable();
    DAL.DataFetch objDataFetch = new DAL.DataFetch();
    DAL.Validation objvalid = new Validation();
    //XMLMetaMasters objXml = new XMLMetaMasters();
    global objGlobal = new global();
    Log objLog = new Log();
    OracleConnection oraConn = new OracleConnection();
    string sSql = "", strBranchName = "";
  FillScheme objFillScheme = new FillScheme(); 
    DataTable dtScheme = new DataTable();
 
    DataTable dtSno = new DataTable();
 
    protected void Page_Load(object sender, EventArgs e)
    {
        globalSession.checkSession();
        if (!IsPostBack)
        {
            sSql = objFillScheme.BranchFill();//AK+LS 02/04/2016 not required this func
            objDataFetch.FillDdl(sSql, ddlBranch, "BRAN_SNO", "BRAN_NAME");//AK+LS 02/04/2016 convert to XML

            //objXml.FillMetaXMLDdl(ddlBranch, "BRANCH.xml", "bran_sno", "bran_name", "");
            ddlBranch.SelectedValue = Session["BranchLogin"].ToString();
            if (Session["BranchLogin"] != null)
            {
                ddlBranch.SelectedValue = Session["BranchLogin"].ToString();
            }
            ddlBranch.Enabled = false;
            sSql = " select link_sno,link_abbrev,dsch_abbr as abbr, dsch_name as scheme ";
            sSql += " From  depositschemesetup,linkmain";
            sSql += " where  ltrim(rtrim(upper(link_abbrev))) = ltrim(rtrim(upper(dsch_abbr)))";
            sSql += " and DSCH_CHQBOOKYN = 'Y'";
            sSql += " AND DSCH_BRANCH = " + ddlBranch.SelectedValue;
            objDataFetch.FillDdl(sSql, ddlScheme, "link_abbrev", "scheme");
            ddlScheme.Items.Insert(0, "----Select----");
        }

    }
    protected void rblDeposit_SelectedIndexChanged(object sender, EventArgs e)
    {
        ChequeBook objCB = new ChequeBook();
        //AK+LS 02/04/2016 keep before the if block
        //lblAccType.Text = "";            
                
        if (rblDeposit.SelectedValue.ToUpper().Trim() == "DEPOSIT")
        {
            //AK+LS 02/04/2016 Keep after the if cond since general in different scenarios
         
            lblAccType.Text = "";            
            sSql = objCB.DepositScheme(Convert.ToInt32(ddlBranch.SelectedValue));
            objDataFetch.FillDdl(sSql, ddlScheme, "abbr", "scheme");
            ddlScheme.Items.Insert(0, "----Select----");

        }
        else if (rblDeposit.SelectedValue.ToUpper().Trim() == "OVERDRAFT")
        {
            lblAccType.Text = "";
            sSql = objCB.ODscheme(Convert.ToInt32(ddlBranch.SelectedValue));
            objDataFetch.FillDdl(sSql, ddlScheme, "abbr", "scheme");
            ddlScheme.Items.Insert(0, "----Select----");
        }
    }

    protected void txtAccNo_TextChanged(object sender, EventArgs e)
    {
        OracleConnection oraConn = new OracleConnection(Session["constring"].ToString());
        if (oraConn.State != ConnectionState.Open)
            oraConn.Open();
        int branch = 0;
        //DataTable dtGLdetails = new DataTable();
        DataTable dtDetail = new DataTable();
        DataTable dtChequeClosed = new DataTable();
        DataTable dtPrintCheBuk = new DataTable();
        string sSQL = "";
        double dblCheqSno = 0.00;

        try //AK+LS 02/04/2016 can be opened before conn string
        {
            //AK+LS 02/04/2016 should keep outside the try since no chance of getting exceptions in these declarations.
           
            //AK+LS 02/04/2016 end----

            branch = (int)Session["BranchLogin"];
            if (ddlScheme.SelectedValue.ToUpper().Trim() == "----SELECT----")
            {
                ScriptManager.RegisterStartupScript(this.UpdatePanel4, typeof(string), "Alert", "alert('Select the Scheme and proceed ');", true);
                lblAccType.Text = "";
                txtAccNo.Text = "";
                lblName.Text = "";
                return;
            }
            else
            {
                if (txtAccNo.Text.Trim() != string.Empty)
                {
                    ChequeBook objCB = new ChequeBook();
                    ddlBranch.Enabled = false;
                    rblDeposit.Enabled = false;
                    ddlScheme.Enabled = false;
                    dtDetail = objGlobal.GetClientNoAndName(branch, txtAccNo.Text, lblAccType.Text.ToUpper(), oraConn);
                    if (dtDetail.Rows.Count > 0)
                    {
                        lblName.Text = dtDetail.Rows[0]["ACCHOLDERNAME"].ToString();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.UpdatePanel4, typeof(string), "Alert", "alert('Invalid Account Number ');", true);
                        return;
                    }
                    if (rblDeposit.SelectedValue.ToUpper().Trim() == "DEPOSIT")
                    {
                        //oraConn.Dispose();//AK+LS 02/04/2016 why it is  disposed each time????
                        sSql = objCB.DepClose(lblAccType.Text.ToUpper().Trim(), txtAccNo.Text.Trim(), Convert.ToInt32(ddlBranch.SelectedValue));
                    }
                    else if (rblDeposit.SelectedValue.ToUpper().Trim() == "OVERDRAFT")
                    {
                        //oraConn.Dispose();//AK+LS 02/04/2016 why it is  disposed each time????
                        sSql = objCB.ODClose(lblAccType.Text.ToUpper().Trim(), txtAccNo.Text.Trim(), Convert.ToInt32(ddlBranch.SelectedValue));

                    }
                    
                    objDataFetch.loginconnection = oraConn.ToString();
                    dtChequeClosed = objDataFetch.DatatablePassSQL(sSql);
                    objLog.WriteLog("CheckBookIssue App:: Inserting Values to the application " + ddlBranch.SelectedValue);
                    //AK+LS 02/04/2016 null checking is not necessary???
                    if (dtChequeClosed.Rows.Count > 0) 
                    {
                        if (dtChequeClosed.Rows[0]["DEPM_CLOSEDYN"].ToString().ToUpper().Trim() == "Y")
                        {
                            //AK+LS 02/04/2016 Make the msg proper with C as small case A/c already closed
                            ScriptManager.RegisterStartupScript(this.UpdatePanel4, typeof(string), "Alert", "alert('A/c already closed');", true);
                            rblDeposit.Enabled = true;
                            txtAccNo.Text = "";
                            lblName.Text = "";
                            return;
                        }

                    }
                    else
                    {
                        rblDeposit.Enabled = true;
                        ddlScheme.Enabled = true;
                        ddlScheme.SelectedIndex = 0;
                        txtAccNo.Text = "";
                        //AK+LS 02/04/2016 Make the msg proper with C as small case A/c does not exist
                        ScriptManager.RegisterStartupScript(this.UpdatePanel4, typeof(string), "Alert", "alert('A/c does not exist');", true);
                        return;
                    }
                    sSQL = " SELECT CHQH_SNO, CHQH_ACABBR,CHQH_ACNO,CHQH_BOOKNO,CHQH_NOOFLEAFS,CHQH_FROMNUMBER,CHQH_TO,CHQH_BRANCH ,CHQH_PRINTEDYN";
                    sSQL += " FROM CHEQUEH ";
                    sSQL += " WHERE NVL(CHQH_PRINTEDYN,'N') <> 'Y' ";
                    sSQL += " AND CHQH_ACABBR= '" + lblAccType.Text.ToUpper().Trim() + "'";
                    sSQL += " AND CHQH_ACNO= " + txtAccNo.Text;
                    sSQL += " AND CHQH_BRANCH=" + ddlBranch.SelectedValue;
                    dtPrintCheBuk = objDataFetch.DatatablePassSQL(sSQL);
                    //Session["dtPrintCheBuk"] = dtPrintCheBuk;

                    if (dtPrintCheBuk.Rows.Count > 0)
                    {
                        dblCheqSno = Convert.ToDouble(dtPrintCheBuk.Rows[0]["CHQH_SNO"].ToString());
                        Session["CHEQUESNO"] = dblCheqSno;
                        txtChqBukNum.Text = dtPrintCheBuk.Rows[0]["CHQH_BOOKNO"].ToString();
                        txtLeafFrom.Text = dtPrintCheBuk.Rows[0]["CHQH_FROMNUMBER"].ToString();
                        txtLeafTo.Text = dtPrintCheBuk.Rows[0]["CHQH_TO"].ToString();
                        lblNoOfLeaves.Text = dtPrintCheBuk.Rows[0]["CHQH_NOOFLEAFS"].ToString();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.UpdatePanel4, typeof(string), "Alert", "alert('No cheque is issued for this Account Number');", true);
                        lblName.Text = "";
                        txtAccNo.Text = "";
                        txtAccNo.Focus();
                        return;
                    }
                }
            }

            btnPrint.Focus();

        }
        catch (Exception ex)
        {
            objLog.WriteLog("CheBukFormatPrint:txtAccNo_Text  event error : " + ex.Message);
            ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert('" + ex.Message + "')</script>");
        }
           finally
        {
            objLog.WriteLog("CheBukFormatPrint.aspx---> txtAccNo_TextChanged---> txtAccNo_TextChanged event reached finally block");
            if (oraConn.State == ConnectionState.Open)
                oraConn.Close();
            oraConn.Dispose();
            OracleConnection.ClearAllPools();
            //AK+LS 02/04/2016 missing writelog here
        }
        
    }
    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        Chkbklist();
    }

    protected void Chkbklist()
    {
        OracleConnection oraConn = new OracleConnection(Session["constring"].ToString());
        if (oraConn.State != ConnectionState.Open)
            oraConn.Open();
        DataTable dtChbookNo = new DataTable();
        ChequeBook objCB = new ChequeBook();

        if (ddlScheme.SelectedValue.Trim() != "")
        {
            lblAccType.Text = ddlScheme.SelectedValue;
            sSql = objCB.chequebookstock(lblAccType.Text, Convert.ToInt32(ddlBranch.SelectedValue));
            dtChbookNo = objDataFetch.DatatablePassSQL(sSql);
        }

        txtAccNo.Focus();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect(Gen.REPORT_MENU, false);
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (Session["constring"] != null)
        {
            oraConn = new OracleConnection(Session["constring"].ToString());
            if (oraConn.State != ConnectionState.Open)
                oraConn.Open();
        }
        double dblTranCode = 0.00, dblSno = 0.00;
        DataTable dtPrint = new DataTable();
        DataTable dtDetails = new DataTable();
        string strJointHolderName = "", strAbbr = "", strcount="";
        try
        {

            dtDetails = (DataTable)(Session["dtPrintCheBuk"]);
            strBranchName = ddlBranch.SelectedItem.ToString();
            Session["BRanchName"] = strBranchName;
            //AK+LS 02/04/2016 move embedded query to a oracle procedure or func(Need to discuss)

            sSql = "  SELECT SUBSTR(BRAN_MICRBRANCHCODE,1,3) AS CTY,";
            sSql += " SUBSTR(BRAN_MICRBRANCHCODE,4,3) AS BKC, ";
            sSql += "  SUBSTR(BRAN_MICRBRANCHCODE,7,3) AS BRC,";
            sSql += "  LPAD(BRAN_SNO,6,0) AS BRSID,";
            sSql += " SUBSTR(BRAN_MICRBRANCHCODE,4,6) AS MCRCOD,";
            sSql += " LPAD(CBMS_BRANCH,3,0)||LPAD(LINK_SNO,3,0)||LPAD(CBMS_ACNO,6,0) AS COD,";
           // sSql += " LPAD(CBMS_ACNO,16,0) AS COD,";
            sSql += " LPAD(LINK_SNO,2,0) AS TRC,";
            sSql += " SUBSTR(CLMS_DES,1,40) as NAM,";
            sSql += "CHQH_SNO,SUBSTR(ADDR_ADDRESS1,1,40) AS AD1,SUBSTR(ADDR_ADDRESS2,1,40) AS  AD2,SUBSTR(ADDR_ADDRESS3,1,40) AS AD3,  ";
            sSql += " SUBSTR(ADDR_ADDRESS3,1,20) AS CSTCTY,SUBSTR(ADDR_PINCODE,1,20) CSTPIN,SUBSTR(ADDR_TELPHNO,1,8) AS TELR,SUBSTR(ADDR_OFFPHNO,1,8) AS TELO,SUBSTR(ADDR_MOBILENO,1,10) AS MOB,";
            sSql += " SUBSTR(CHQH_NOOFLEAFS,1,3) AS QPB,";
            sSql += "SUBSTR(CBMS_FROMLEAFNO,1,6) AS CHQFROM,SUBSTR(CBMS_TOLEAFNO,1,6) AS CHQTO,";
            sSql += " DEPM_SNO AS SNO,SITP_TEXTVALUE,CHQH_PRINTEDYN AS PRINTEDYN  ";
            sSql += " FROM CHEQUEBOOKSTOCK,BRANCH,LINKMAIN,CLIENTMASTER,ADDRESS,DEPOSITMASTER,CHEQUEH,SITEPARAMETERS  ";
            sSql += "  WHERE BRAN_SNO =CBMS_BRANCH ";
            sSql += " AND NVL(CHQH_PRINTEDYN,'N') <> 'Y' ";
            sSql += " AND TRIM(SITP_NAME)=TRIM('IFSC CODE')  ";
            sSql += " AND CHQH_BRANCH = LINK_BRANCH  ";
            sSql += " AND CHQH_ACABBR = LINK_ABBREV ";
            sSql += " AND CLMS_BRANCHNO=BRAN_SNO ";
            sSql += "  AND ADDR_SNO = CLMS_ADDRESS ";
            sSql += " AND CLMS_SNO= DEPM_CLIENT ";
            sSql += " AND CHQH_ACABBR =DEPM_ABBR ";
            sSql += " AND CHQH_ACNO = DEPM_NO ";
            sSql += "AND CHQH_BOOKNO=CBMS_BOOKNO ";
            sSql += " AND CHQH_BRANCH=DEPM_BRANCH ";
            sSql += "  AND CBMS_ACABBR =DEPM_ABBR ";
            sSql += " AND CBMS_ACNO =DEPM_NO ";
            sSql += "  AND LINK_ABBREV ='" + lblAccType.Text.ToUpper().Trim() + "'";
            sSql += " AND CHQH_BRANCH=  " + ddlBranch.SelectedValue;
            sSql += "  AND CHQH_ACABBR ='" + lblAccType.Text.ToUpper().Trim() + "'";
            sSql += " AND  CHQH_ACNO = " + txtAccNo.Text;
            sSql += " UNION";
            sSql += "  SELECT SUBSTR(BRAN_MICRBRANCHCODE,1,3) AS CTY,SUBSTR(BRAN_MICRBRANCHCODE,4,3) AS BKC, ";
            sSql += " SUBSTR(BRAN_MICRBRANCHCODE,7,3) AS BRC, ";
            sSql += " LPAD(BRAN_SNO,6,0) AS BRSID,";
            sSql += " SUBSTR(BRAN_MICRBRANCHCODE,4,6) AS MCRCOD,";
            sSql += " LPAD(CBMS_BRANCH,3,0)||LPAD(LINK_SNO,3,0)||LPAD(CBMS_ACNO,6,0) AS COD,";
            //sSql += " LPAD(CBMS_ACNO,16,0) AS COD,";
            sSql += " LPAD(LINK_SNO,2,0) as TRC,";
            sSql += " SUBSTR(CLMS_DES,1,40) as NAM,";
            sSql += "CHQH_SNO, SUBSTR(ADDR_ADDRESS1,1,40) AS AD1,SUBSTR(ADDR_ADDRESS2,1,40) AS  AD2,SUBSTR(ADDR_ADDRESS3,1,40) AS AD3, ";
            sSql += " SUBSTR(ADDR_ADDRESS3,1,20) AS CSTCTY,SUBSTR(ADDR_PINCODE,1,20) CSTPIN,SUBSTR(ADDR_TELPHNO,1,8) AS TELR,SUBSTR(ADDR_OFFPHNO,1,8) AS TELO,SUBSTR(ADDR_MOBILENO,1,10) AS MOB,";
            //sSql += " ADDR_ADDRESS3 CSTCTY,ADDR_PINCODE CSTPIN,ADDR_TELPHNO TELR,ADDR_OFFPHNO TELO,ADDR_MOBILENO MOB,";
            sSql += "  SUBSTR(CHQH_NOOFLEAFS,1,3) AS QPB,";
            sSql += " SUBSTR(CBMS_FROMLEAFNO,1,6) AS CHQFROM,SUBSTR(CBMS_TOLEAFNO,1,6) AS CHQTO,";         
            sSql += " ODMT_SNO AS SNO,SITP_TEXTVALUE,CHQH_PRINTEDYN AS PRINTEDYN";
            sSql += " FROM CHEQUEBOOKSTOCK,BRANCH,LINKMAIN,CLIENTMASTER,ADDRESS,ODMASTER,CHEQUEH,SITEPARAMETERS ";
            sSql += " WHERE BRAN_SNO =CBMS_BRANCH ";
            sSql += " AND NVL(CHQH_PRINTEDYN,'N') <> 'Y' ";
            sSql += " AND TRIM(SITP_NAME)=TRIM('IFSC CODE')  ";
            sSql += " AND CHQH_BRANCH = LINK_BRANCH ";
            sSql += " AND CHQH_ACABBR = LINK_ABBREV";
            sSql += " AND CLMS_BRANCHNO=BRAN_SNO";
            sSql += " AND ADDR_SNO = CLMS_ADDRESS";
            sSql += " AND CLMS_SNO= ODMT_CLIENT";
            sSql += "  AND CHQH_ACABBR =ODMT_ABBR";
            sSql += " AND CHQH_ACNO = ODMT_NO";
            sSql += " AND CHQH_BOOKNO=CBMS_BOOKNO";
            sSql += "  AND CHQH_BRANCH=ODMT_BRANCH";
            sSql += "  AND CBMS_ACABBR =ODMT_ABBR";
            sSql += " AND CBMS_ACNO =ODMT_NO";
            sSql += " AND LINK_ABBREV ='" + lblAccType.Text.ToUpper().Trim() + "'";
            sSql += " AND CHQH_BRANCH= " + ddlBranch.SelectedValue;
            sSql += " AND CHQH_ACABBR ='" + lblAccType.Text.ToUpper().Trim() + "'";
            sSql += " AND  CHQH_ACNO = " + txtAccNo.Text;

            dtPrint = objDataFetch.DatatablePassSQL(sSql);

            Session["PrintDetails"] = dtPrint;

            if (dtPrint.Rows.Count > 0)
            {

                // if (dtPrint.Rows[0]["SNO"].ToString() != "")
                // {
                //dblSno = Convert.ToDouble(dtPrint.Rows[0]["SNO"].ToString());
                // Session["ODDEPSNO"] = dblSno;
                //sSql = "  SELECT ODMT_SNO,JHOL_ODLINK,CLMS_SNO,CLMS_DES AS JointHolderName ";
                //sSql += " FROM JOINTHOLDER,ODMASTER,CLIENTMASTER ";
                //sSql += " WHERE ODMT_SNO=JHOL_ODLINK ";
                //sSql += " AND JHOL_CLIENT=CLMS_SNO ";
                //sSql += " AND JHOL_ODLINK= " + Session["ODDEPSNO"] + "";
                //sSql += " UNION";
                //sSql = "  SELECT DEPM_SNO,JHOL_DEPLINK,CLMS_SNO,CLMS_DES AS JointHolderName ";
                //sSql += " FROM JOINTHOLDER,DEPOSITMASTER,CLIENTMASTER ";
                //sSql += " WHERE DEPM_SNO=JHOL_DEPLINK ";
                //sSql += " AND JHOL_CLIENT=CLMS_SNO ";
                // sSql += " AND JHOL_DEPLINK= " + Session["ODDEPSNO"] + "";
                //dtSno = objDataFetch.DatatablePassSQL(sSql);
                //  if (dtSno.Rows.Count > 0)
                // {
                // if (dtSno.Rows[0]["JointHolderName"].ToString() != "")
                //{
                //strJointHolderName = dtSno.Rows[0]["JointHolderName"].ToString();
                //  }
                // }
                // else if (dtSno.Rows.Count <= 0)
                // {
                //strJointHolderName = "0";
                // }
                // Session["JOINTHOLDERNAME"] = strJointHolderName;
                //}
             
                int cnt = dtPrint.Rows.Count;
                Session["cnt"] = cnt;
                strAbbr = lblAccType.Text.ToUpper().Trim();

                Session["Abbrevation"] = strAbbr;


                if (dtPrint.Rows[0]["TRC"].ToString() != "")
                {
                    dblTranCode = Convert.ToDouble(dtPrint.Rows[0]["TRC"].ToString());
                }

                //TRANSACTION CODE 

                if (cbAtPar.Checked == true)
                {
                    if (Session["Abbrevation"].ToString().Trim() == "SB".ToString().Trim())
                    {
                        //AK+LS 02/04/2016 needs clarifications with comment
                        Session["TranCode"] = "31";
                    }
                    else if (Session["Abbrevation"].ToString().Trim() == "CA".ToString().Trim())
                    {
                        //AK+LS 02/04/2016 needs clarifications with comment
                        Session["TranCode"] = "29";
                    }
                    else if ((Session["Abbrevation"].ToString().Trim() == "OD".ToString().Trim()) || (Session["Abbrevation"].ToString().Trim() == "CL".ToString().Trim()))
                    {
                        //AK+LS 02/04/2016 needs clarifications with comment
                        Session["TranCode"] = "30";
                    }
                    else
                    {
                        Session["TranCode"] = dblTranCode;
                    }
                }
                else
                {
                    if (Session["Abbrevation"].ToString().Trim() == "SB".ToString().Trim())
                    {
                        //AK+LS 02/04/2016 needs clarifications with comment
                        //Hardcoded because of Client Requirements
                        Session["TranCode"] = "10";
                    }
                    else if (Session["Abbrevation"].ToString().Trim() == "CA".ToString().Trim())
                    {
                        //AK+LS 02/04/2016 needs clarifications with comment
                        //Hardcoded because of Client Requirements
                        Session["TranCode"] = "11";
                    }
                    else if ((Session["Abbrevation"].ToString().Trim() == "OD".ToString().Trim()) || (Session["Abbrevation"].ToString().Trim() == "CL".ToString().Trim()))
                    {
                        //AK+LS 02/04/2016 needs clarifications with comment
                        //Hardcoded because of Client Requirements
                        Session["TranCode"] = "13";
                    }
                    else
                    {
                        Session["TranCode"] = dblTranCode;
                    }
                }

                //TRANSACTION CODE


                //sSql = " UPDATE CHEQUEH SET  CHQH_PRINTEDYN='Y' ";
                //sSql += " WHERE CHQH_SNO= '" + Session["CHEQUESNO"] + "'";

                //objDataFetch.loginconnection = Session["constring"].ToString();
                //objDataFetch.ExecuteQuery(sSql);
            }
            if (dtPrint.Rows.Count <= 0)
            {
                ScriptManager.RegisterStartupScript(this.UpdatePanel4, typeof(string), "Alert", "alert('No Records exists');", true);
                lblName.Text = "";
                txtAccNo.Text = "";
                txtLeafFrom.Text = "";
                txtLeafTo.Text = "";
                lblNoOfLeaves.Text = "";
                txtChqBukNum.Text = "";
                txtAccNo.Focus();
                return;
            }
            objLog.WriteLog("CheBukFormatPrint.aspx:redirecting to ChkBukDisp");
            rblDeposit.SelectedValue = "Deposit";
            Response.Redirect("ChkBukDisp.aspx");
        }
        catch (Exception ex)
        {
            objLog.WriteLog("CheBukFormatPrint:btnPrintClick event error : " + ex.Message);
            ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert('" + ex.Message + "')</script>");
        }
        finally
        {
            objLog.WriteLog("CheBukFormatPrint.aspx---> txtAccNo_TextChanged---> txtAccNo_TextChanged event reached finally block");
            if (oraConn.State == ConnectionState.Open)
                //ddlScheme.SelectedValue = "----Select----";
            oraConn.Close();
            oraConn.Dispose();
            OracleConnection.ClearAllPools();
            //AK+LS 02/04/2016 Log missing
        }
    }
    protected void txtClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("CheBukFormatPrint.aspx");
        //txtAccNo.Text = "";
        //txtChqBukNum.Text = "";
        //txtLeafFrom.Text = "";
        //txtLeafTo.Text = "";
        //lblNoOfLeaves.Text = "";
        //lblName.Text = "";
    }
}
