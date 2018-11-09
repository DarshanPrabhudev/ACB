//AK+LS 02/04/2016 Remove unwanted libraries
//like using System.Xml.Linq;
//AK+LS 02/04/2016 mod history missing
////*-------------------------------CHEQUE BOOK DETAILS DISPLAY------------------------------------
//*
//* Version     Modified By         Date           Remarks
//* 1.0         ROOPA       21-MAR-2016    Intial Draft-Assisted by Venkatesh 
//*------------------------------------------------------------------------------
//* -----------------------------PSEUDOCODE--------------------------------------
//*This Form is is used to see the details of the cheque book issued to customer and convert the details to text format.
//****** Tables Used ******//
//*.CHEQUEH
//*SITEPARAMETERS
using System;
using System.Data;
using System.Data.OracleClient;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

public partial class ChkBukDisp : System.Web.UI.Page
{
    OracleConnection oraConn = new OracleConnection();
    DAL.conn objcon = new DAL.conn();
    Log objLog = new Log();
    global objGlb = new global();
    DAL.DataFetch objDataFetch = new DAL.DataFetch();
    DataTable dtChkBukPrint = new DataTable();
    DataTable dtcom = new DataTable();
    DataRow row;
    Int32 Slno = 1,count;
    string sSql = "", strBnkName="";
    double dblAccNo = 0.00;
   
    double  dblTranCode = 0.00;
    string strJointHolderName = "", strAccno = "", sysdate="";
   

    protected void Page_Load(object sender, EventArgs e)
    {
        objLog.WriteLog("CheBukFormatPrint.aspx: Entered Page_Load");

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

                    ViewState["Pagename"] = "Cheque Book Print Format";
                    //AK+LS 02/04/2016 serach comp name in login.Might have fetched somewhere in session
                    //Company Name is Not taken in Session in Login page.
                    sSql = "Select SITP_TEXTVALUE From Siteparameters Where SITP_NAME = 'COMPANY NAME'";
                    OracleDataAdapter odaC = new OracleDataAdapter(sSql, oraConn);
                    odaC.Fill(dtcom);

                    global gblSysDt = new global();
                    sysdate = gblSysDt.GetServerDate();

                    if (dtcom.Rows.Count != 0)
                    {
                        strBnkName = dtcom.Rows[0]["SITP_TEXTVALUE"].ToString();
                    }

                    if (Session["BRanchName"] != null)
                    {
                        lblBranch.Text = Session["BRanchName"].ToString();
                    }
                    DataTable dtPrintDetails = new DataTable();
                    dtPrintDetails = (DataTable)Session["PrintDetails"];
                    if (dtPrintDetails.Rows.Count > 0)
                    {
                        dtChkBukPrint.Columns.Add(new DataColumn("Cheque Sno", typeof(string)));
                        dtChkBukPrint.Columns.Add(new DataColumn("Serial No", typeof(string)));
                        dtChkBukPrint.Columns.Add(new DataColumn("City Code", typeof(string)));
                        dtChkBukPrint.Columns.Add(new DataColumn("Bank code", typeof(string)));
                        dtChkBukPrint.Columns.Add(new DataColumn("Branch Code", typeof(string)));
                   
                        dtChkBukPrint.Columns.Add(new DataColumn("Reference Code", typeof(string)));
                        dtChkBukPrint.Columns.Add(new DataColumn("MICR Code", typeof(string)));
                        dtChkBukPrint.Columns.Add(new DataColumn("Account No.", typeof(string)));
                        dtChkBukPrint.Columns.Add(new DataColumn("Transaction Code", typeof(string)));
                        dtChkBukPrint.Columns.Add(new DataColumn("Customer Name", typeof(string)));
                        dtChkBukPrint.Columns.Add(new DataColumn("Jointholder Name", typeof(string)));
                        dtChkBukPrint.Columns.Add(new DataColumn("Jointholder Name2", typeof(string)));
                        dtChkBukPrint.Columns.Add(new DataColumn("Sign Authority", typeof(string)));
                        dtChkBukPrint.Columns.Add(new DataColumn("SA2", typeof(string)));
                        dtChkBukPrint.Columns.Add(new DataColumn("SA3", typeof(string)));
                        dtChkBukPrint.Columns.Add(new DataColumn("Address1", typeof(string)));
                        dtChkBukPrint.Columns.Add(new DataColumn("Address2", typeof(string)));
                        dtChkBukPrint.Columns.Add(new DataColumn("Address3", typeof(string)));
                        dtChkBukPrint.Columns.Add(new DataColumn("Address4", typeof(string)));
                        dtChkBukPrint.Columns.Add(new DataColumn("Address5", typeof(string)));
                        dtChkBukPrint.Columns.Add(new DataColumn("City Name", typeof(string)));
                        dtChkBukPrint.Columns.Add(new DataColumn("PINCODE", typeof(string)));
                        dtChkBukPrint.Columns.Add(new DataColumn("Res Tel No.", typeof(string)));
                        dtChkBukPrint.Columns.Add(new DataColumn("Off Tel No", typeof(string)));
                        dtChkBukPrint.Columns.Add(new DataColumn("Mob No", typeof(string)));
                        dtChkBukPrint.Columns.Add(new DataColumn("No. of Leaves", typeof(string)));

                        dtChkBukPrint.Columns.Add(new DataColumn("Book Size", typeof(string)));
                        dtChkBukPrint.Columns.Add(new DataColumn("Bearer/Order", typeof(string)));
                        dtChkBukPrint.Columns.Add(new DataColumn("At Par Y/N", typeof(string)));
                        dtChkBukPrint.Columns.Add(new DataColumn("Product Code", typeof(string)));
                        dtChkBukPrint.Columns.Add(new DataColumn("From Leaf Num", typeof(string)));
                        dtChkBukPrint.Columns.Add(new DataColumn("To Leaf Num", typeof(string)));

                        dtChkBukPrint.Columns.Add(new DataColumn("AddOpenField1", typeof(string)));
                        dtChkBukPrint.Columns.Add(new DataColumn("AddOpenField2", typeof(string)));
                        dtChkBukPrint.Columns.Add(new DataColumn("AddOpenField3", typeof(string)));
                        dtChkBukPrint.Columns.Add(new DataColumn("AddOpenField4", typeof(string)));
                        dtChkBukPrint.Columns.Add(new DataColumn("AddOpenField5", typeof(string)));
                        dtChkBukPrint.Columns.Add(new DataColumn("AddOpenField6", typeof(string)));
                        dtChkBukPrint.Columns.Add(new DataColumn("AddOpenField7", typeof(string)));
                        dtChkBukPrint.Columns.Add(new DataColumn("AddOpenField8", typeof(string)));
                        dtChkBukPrint.Columns.Add(new DataColumn("AddOpenField9", typeof(string)));
                   

                        //dtChkBukPrint.Columns.Add(new DataColumn("IFSC Code", typeof(string)));
                        //dtChkBukPrint.Columns.Add(new DataColumn("PRINTEDYN", typeof(string)));

                        for (int i = 0; i < dtPrintDetails.Rows.Count; i++)
                        {
                                row = dtChkBukPrint.NewRow();
                            
                                if (Session["TranCode"] != null)
                                {
                                    dblTranCode = Convert.ToDouble(Session["TranCode"]);
                                }
                                if (Session["cnt"] != null)
                                {
                                    count =Convert.ToInt16(Session["cnt"]);
                                }
                                //if (Session["JOINTHOLDERNAME"] != null)
                                //{
                                    //strJointHolderName = (Session["JOINTHOLDERNAME"].ToString());
                               // }





                                row["Cheque Sno"] = dtPrintDetails.Rows[i]["CHQH_SNO"].ToString();
                                //Slno = Slno.ToString().PadLeft(9, '0');
                            //dtDepositList.Rows[i]["clms_des"].ToString().Substring(0, 25)

                                row["Serial No"] = Slno.ToString().PadLeft(8, '0');

                                //AK+LS 02/04/2016 not required brackets here (dtPrintDetails.Rows[i]["CITY"].ToString()) where not doing any casting here
                                row["City Code"] = dtPrintDetails.Rows[i]["CTY"].ToString();
                                row["Bank code"] = dtPrintDetails.Rows[i]["BKC"].ToString();
                                row["Branch Code"] = dtPrintDetails.Rows[i]["BRC"].ToString();
                                row["Reference Code"] = dtPrintDetails.Rows[i]["BRSID"].ToString();
                                row["MICR Code"] = dtPrintDetails.Rows[i]["MCRCOD"].ToString();
                                strAccno = dtPrintDetails.Rows[i]["COD"].ToString();

                                row["Account No."] = strAccno.PadLeft(16, '0');

                               // dblAccNo = Convert.ToDouble(strAccno.PadLeft(16, '0'));
                                //dblAccNo = Convert.ToDouble(dtPrintDetails.Rows[i]["COD"].ToString());
                                //Session["ACCNO"] = dblAccNo;
                                Session["ACCNO"] = strAccno.PadLeft(16, '0');
                                row["Transaction Code"] = dblTranCode;

                                row["Customer Name"] = dtPrintDetails.Rows[i]["NAM"].ToString();
                                row["Jointholder Name"] = dtPrintDetails.Rows[i]["NAM"].ToString();
                                row["Jointholder Name2"] = "";
                                row["Sign Authority"] = "Authorised Signatory";
                                row["SA2"] = "";
                                row["SA3"] = "";
                                row["Address1"] = dtPrintDetails.Rows[i]["AD1"].ToString();
                                row["Address2"] = dtPrintDetails.Rows[i]["AD2"].ToString();
                                row["Address3"] = dtPrintDetails.Rows[i]["AD3"].ToString();
                                row["Address4"] = "";
                                row["Address5"] = "";
                                row["City Name"] = dtPrintDetails.Rows[i]["CSTCTY"].ToString();
                                row["PINCODE"] = dtPrintDetails.Rows[i]["CSTPIN"].ToString();
                                row["Res Tel No."] = dtPrintDetails.Rows[i]["TELR"].ToString();
                                row["Off Tel No"] = dtPrintDetails.Rows[i]["TELO"].ToString();
                                row["Mob No"] = dtPrintDetails.Rows[i]["MOB"].ToString();
                                row["Book Size"] = count;
                                row["No. of Leaves"] = dtPrintDetails.Rows[i]["QPB"].ToString();

                                row["Bearer/Order"] = "Bearer";
                            //AK+LS 02/04/2016 write the logic behind this .comment and clarify.
                                //AK+LS 02/04/2016 if one var is comapring with three different val any other reliable method is there??
                                if ((dblTranCode.ToString().Trim() == Convert.ToDouble("29").ToString().Trim()) || (dblTranCode.ToString().Trim() == Convert.ToDouble("30").ToString().Trim()) || (dblTranCode.ToString().Trim() == Convert.ToDouble("31").ToString().Trim()))
                                {
                                    row["At Par Y/N"] = "Y";
                                }
                                else
                                {
                                    row["At Par Y/N"] = "N";
                                }
                                row["Product Code"] = "";
                                row["From Leaf Num"] = dtPrintDetails.Rows[i]["CHQFROM"].ToString();
                                row["To Leaf Num"] = dtPrintDetails.Rows[i]["CHQTO"].ToString();


                                row["AddOpenField1"] = sysdate;
                                row["AddOpenField2"] = "";
                                row["AddOpenField3"] = "";
                                row["AddOpenField4"] = "";
                                row["AddOpenField5"] = "";
                                row["AddOpenField6"] = "";
                                row["AddOpenField7"] = "";
                                row["AddOpenField8"] = "";
                                row["AddOpenField9"] = "";

                                //row["IFSC Code"] = dtPrintDetails.Rows[i]["SITP_TEXTVALUE"].ToString();
                                //row["PRINTEDYN"] = dtPrintDetails.Rows[i]["PRINTEDYN"].ToString();

                                dtChkBukPrint.Rows.Add(row);
                                Slno = Slno + 1;
                        }

                        gvChkBukPrint.DataSource = dtChkBukPrint;
                        gvChkBukPrint.DataBind();
                        ViewState["PrintDetails"] = dtChkBukPrint;
                    }
                }
            }
            catch (Exception ex)
            {
                objLog.WriteLog("ChkBukDisp Exception in PageLoad : " + ex.Message);
                ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert('" + ex.Message + "')</script>");
            }
            finally
            {
                if (oraConn.State == ConnectionState.Open)
                    oraConn.Close();
                oraConn.Dispose();
                OracleConnection.ClearAllPools();
                objLog.WriteLog("CheBukDisp.aspx---> Page_Load---> Page_Load event reached finally block");
                //AK+LS 02/04/2016 log missing

            }
        }
    }
    protected void btnText_Click(object sender, EventArgs e)
    {
        string  sSql = "", StrAppend="";
        DataTable dtNewDetails = new DataTable();
       DataTable dtCheck = new DataTable();
        //DataTable dtSmry = new DataTable();
        System.IO.StringWriter sWriter;
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sWriter = new System.IO.StringWriter(sb);


        if (ViewState["PrintDetails"] != null)
        {
            dtNewDetails = (DataTable)ViewState["PrintDetails"];
        }
    
       try
        {
 //objLog.WriteLog("CheBukFormatPrint.aspx: btnText_Click");
            if (dtNewDetails.Rows.Count > 0)
            {
 //objLog.WriteLog("CheBukFormatPrint.aspx: btnText_Click:Record Count" +dtNewDetails.Rows.Count);
                foreach (DataRow drPrint in dtNewDetails.Rows)
                {
                    //objLog.WriteLog("CheBukFormatPrint.aspx: btnText_Click:Processing First Record");
                    if (StrAppend == "")
                    {
                        StrAppend = "~";
                    }

                    if (StrAppend == "0")
                    {
                        StrAppend = "~";
                    }

                    StrAppend = drPrint["Serial No"].ToString() + "~";
                    StrAppend += drPrint["City Code"].ToString() + "~";
                    StrAppend += drPrint["Bank code"].ToString() + "~";
                    StrAppend += drPrint["Branch Code"].ToString() + "~";
                    StrAppend += drPrint["Reference Code"].ToString() + "~";
                    StrAppend += drPrint["MICR Code"].ToString() + "~";
                    StrAppend += drPrint["Account No."].ToString() + "~";
                    StrAppend += drPrint["Transaction Code"].ToString() + "~";
                    StrAppend += drPrint["Customer Name"].ToString() + "~";
                    StrAppend += drPrint["Jointholder Name"].ToString() + "~";
                    StrAppend += drPrint["Jointholder Name2"].ToString() + "~";
                    StrAppend += drPrint["Sign Authority"].ToString() + "~";
                    StrAppend += drPrint["SA2"].ToString() + "~";
                    StrAppend += drPrint["SA2"].ToString() + "~";
                    StrAppend += drPrint["Address1"].ToString() + "~";
                    StrAppend += drPrint["Address2"].ToString() + "~";
                    StrAppend += drPrint["Address3"].ToString() + "~";
                    StrAppend += drPrint["Address4"].ToString() + "~";
                    StrAppend += drPrint["Address5"].ToString() + "~";
                    StrAppend += drPrint["City Name"].ToString() + "~";
                    StrAppend += drPrint["PINCODE"].ToString() + "~";
                    StrAppend += drPrint["Res Tel No."].ToString() + "~";
                    StrAppend += drPrint["Off Tel No"].ToString() + "~";
                    StrAppend += drPrint["Mob No"].ToString() + "~";
                    StrAppend += drPrint["Book Size"].ToString() + "~";
                    StrAppend += drPrint["No. of Leaves"].ToString() + "~";
                  
                    StrAppend += drPrint["Bearer/Order"].ToString() + "~";
                    StrAppend += drPrint["At Par Y/N"].ToString() + "~";
                    StrAppend += drPrint["Product Code"].ToString() + "~";
                    StrAppend += drPrint["From Leaf Num"].ToString() + "~";
                    StrAppend += drPrint["To Leaf Num"].ToString() + "~";

                    StrAppend += drPrint["AddOpenField1"].ToString() + "~";
                    StrAppend += drPrint["AddOpenField2"].ToString() + "~";
                    StrAppend += drPrint["AddOpenField3"].ToString() + "~";
                    StrAppend += drPrint["AddOpenField4"].ToString() + "~";
                    StrAppend += drPrint["AddOpenField5"].ToString() + "~";
                    StrAppend += drPrint["AddOpenField6"].ToString() + "~";
                    StrAppend += drPrint["AddOpenField7"].ToString() + "~";
                    StrAppend += drPrint["AddOpenField8"].ToString() + "~";
                    StrAppend += drPrint["AddOpenField9"].ToString() ;

                   // StrAppend += drPrint["IFSC Code"].ToString();

                    sWriter.Write(StrAppend);
                    sWriter.WriteLine();
                    StrAppend = "";


                    sSql = " UPDATE CHEQUEH SET  CHQH_PRINTEDYN = 'Y' ";
                    sSql += " WHERE CHQH_SNO= '" + drPrint["Cheque Sno"].ToString() + "'";
                    objDataFetch.loginconnection = Session["constring"].ToString();
                    objDataFetch.ExecuteQuery(sSql);
                }
                     sWriter.Close();
                    string sLogPath = HttpContext.Current.Server.MapPath("PRINT/" + getLogFileName());
                    System.IO.File.WriteAllText(sLogPath, sWriter.ToString());
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Text File Successfully written to " + sLogPath + " ')</script>");
                    return;
            }
//objLog.WriteLog("CheBukFormatPrint.aspx: btnText_Click:successfully Completed");
            else
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('No data found')</script>");
                return;
            }
        }
       catch (Exception ex)
        {
            objLog.WriteLog("ChkBukDisp Exception in btnText_Click Event : " + ex.Message);
            string m = ex.Message;
            ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Could Not Convert to Text File')</script>");
     //       //AK+LS 02/04/2016 missing log
        }
        finally
        {
            if (oraConn.State == ConnectionState.Open)
                oraConn.Close();
            oraConn.Dispose();
            OracleConnection.ClearAllPools();
           objLog.WriteLog("CheBukDisp.aspx---> btnText_Click---> btnText_Click event reached finally block");
           // //AK+LS 02/04/2016 log missing

        }
        //AK+LS 02/04/2016 finally block missing
    }
    //AK+LS 02/04/2016 access specifier is missing
    protected string getLogFileName()
    {
        string sRetFileName = "";
        sRetFileName = " " + Session["ACCNO"] + ".txt";
        return sRetFileName;
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("CheBukFormatPrint.aspx");
    }
    protected void gvChkBukPrint_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[11].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[12].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[13].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[14].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[15].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[16].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[17].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[18].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[19].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[20].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[21].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[22].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[23].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[24].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[25].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[26].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[27].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[28].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[29].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[30].HorizontalAlign = HorizontalAlign.Left;

            e.Row.Cells[31].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[32].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[33].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[34].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[35].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[36].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[37].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[38].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[39].HorizontalAlign = HorizontalAlign.Left;


           
            e.Row.Cells[1].Wrap = false;
            e.Row.Cells[2].Wrap = false;
            e.Row.Cells[3].Wrap = false;
            e.Row.Cells[4].Wrap = false;
            e.Row.Cells[5].Wrap = false;
            e.Row.Cells[6].Wrap = false;
            e.Row.Cells[7].Wrap = false;
            e.Row.Cells[8].Wrap = false;
            e.Row.Cells[9].Wrap = false;
            e.Row.Cells[10].Wrap = false;
            e.Row.Cells[11].Wrap = false;
            e.Row.Cells[12].Wrap = false;
            e.Row.Cells[13].Wrap = false;
            e.Row.Cells[14].Wrap = false;
            e.Row.Cells[15].Wrap = false;
            e.Row.Cells[16].Wrap = false;
            e.Row.Cells[17].Wrap = false;
            e.Row.Cells[18].Wrap = false;
            e.Row.Cells[19].Wrap = false;
            e.Row.Cells[20].Wrap = false;
            e.Row.Cells[21].Wrap = false;
            e.Row.Cells[22].Wrap = false;
            e.Row.Cells[23].Wrap = false;
            e.Row.Cells[24].Wrap = false;
           // e.Row.Cells[25].Wrap = false;
        }
    }
}