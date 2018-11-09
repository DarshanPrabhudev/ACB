using System;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data.OracleClient;

public partial class LoanInterestdueDisplay : System.Web.UI.Page
{
    public FormatRpt objfrmpt = new FormatRpt();
    public DataTable dt = new DataTable();
    public object sumObject;
    public DataTable dtAll = new DataTable();
    public DataTable dtcopy = new DataTable();
    public double db1, db2, db3, db4, db5, db6, db7, db8, db9, db10, db11, db12, db13, db14;
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session.Contents.Count == 0)
        {
            Response.Redirect("ShowMessage.aspx?msg=" + GlobalConstants.msg);
        }
        else
        {
            try
            {
                if (Session["res"] != null)
                {
                    dt = (DataTable)Session["res"];
                    DataTable dtdublicate = new DataTable();
                    dtdublicate = dt.Clone();

                    if (Session["interestdue"] != null)
                    {
                        DataRow[] rowintst = dt.Select("INTERESTDUE < " + Convert.ToInt32(Session["interestdue"]));
                        if (rowintst.Count() > 0)
                        {
                            foreach (DataRow rinst in rowintst)
                            {
                                dt.Rows.Remove(rinst);
                            }
                        }
                    }



                    foreach (DataRow dr in dt.Rows)
                    {
                        DataRow row = dtdublicate.NewRow();
                        if (dr["TEL_NO"] != DBNull.Value)
                        {
                            row["TEL_NO"] = dr["TEL_NO"];
                        }
                        row["SNO"] = dr["SNO"];
                        row["LOANABBR"] = dr["LOANABBR"];
                        row["LNO"] = dr["LNO"];
                        row["CNAME"] = dr["CNAME"];


                        if (dr["INTERESTDUE"] != DBNull.Value)
                        {
                            db4 = Convert.ToDouble(dr["INTERESTDUE"]);
                            db10 = db10 + db4;
                            row["INTERESTDUE"] = String.Format("{0:0.00}", db4);
                        }


                        if (dr["MEMABBR"] != DBNull.Value)
                            row["MEMABBR"] = dr["MEMABBR"];
                        if (dr["MEMNO"] != DBNull.Value)
                            row["MEMNO"] = dr["MEMNO"];
                        dtdublicate.Rows.Add(row);

                    }

                    string sumry = "", abbr = "";
                    int selectedCount;
                    //sumry = Session["sumdetl"].ToString();
                    abbr = Session["chklist"].ToString();
                    selectedCount = Convert.ToInt32(Session["chklistcount"]);
                    GlobalConstants.strBnkName = global.BankName();


                    //**********Loanname******

                    for (int i = 0; i < selectedCount; i++)
                    {
                        int str = Convert.ToInt32(abbr.IndexOf(','));
                        string str1 = (abbr.Substring(0, str)).ToString();
                        DataView dv = dtdublicate.DefaultView;
                        dv.RowFilter = "LOANABBR='" + str1 + "'";
                        DataTable dtNewabr = dv.ToTable();
                        int rowcount = Convert.ToInt32(dtNewabr.Rows.Count);
                        string strsql = "select LSCH_DES,LSCH_ABBREV from loanschemesetup where LSCH_ABBREV='" + str1 + "'";
                        DataTable dtquery = new DataTable();
                        OracleConnection oraConn = new OracleConnection(Session["constring"].ToString());
                        OracleDataAdapter dataAdapter = new OracleDataAdapter(strsql, oraConn);
                        DataTable dscon = new DataTable();
                        dataAdapter.Fill(dtquery);
                        dtcopy = dtNewabr.Clone();

                        DataRow dr1 = dtcopy.NewRow();
                        dr1["CNAME"] = dtquery.Rows[0]["LSCH_DES"].ToString().ToUpper();
                        dtcopy.Rows.Add(dr1);
                        foreach (DataRow ro in dtNewabr.Rows)
                        {
                            DataRow dr = dtcopy.NewRow();

                            dr["CNAME"] = ro["CNAME"];
                            dr["LNO"] = ro["LNO"];

                            dr["LOANABBR"] = ro["LOANABBR"];

                            dr["INTERESTDUE"] = ro["INTERESTDUE"];

                            dtcopy.Rows.Add(dr);

                        }

                        //**********display total*******
                        double LAMT = 0, LBAL = 0, ODAMT = 0, INTERESTDUE = 0, PENAL = 0, OTHER = 0, NETDUE = 0;
                        for (int s = 0; s < dtcopy.Rows.Count; s++)
                        {

                            if (dtcopy.Rows[s]["INTERESTDUE"] != DBNull.Value)
                            {
                                INTERESTDUE += Convert.ToDouble(dtcopy.Rows[s]["INTERESTDUE"]);
                            }

                        }

                        DataRow dr3 = dtcopy.NewRow();
                        dr3["CNAME"] = "T O T A L";


                        dr3["INTERESTDUE"] = String.Format("{0:0.00}", INTERESTDUE);

                        dtcopy.Rows.Add(dr3);
                        //**********display total*******

                        dtAll.Merge(dtcopy);
                        for (int k = dtNewabr.Rows.Count - 1; k >= 0; k--)
                            dtNewabr.Rows[k].Delete();
                        for (int k = dtcopy.Rows.Count - 1; k >= 0; k--)
                            dtcopy.Rows[k].Delete();

                        int tempval1 = 0;

                        tempval1 = str + 1;

                        abbr = abbr.Remove(0, tempval1);

                    }// for selectedCount

                    DataRow rr1 = dtAll.NewRow();
                    rr1["CNAME"] = "--------------";
                    dtAll.Rows.Add(rr1);
                    double ot = 0;
                    for (int i = 0; i < dtAll.Rows.Count; i++)
                    {
                        if (dtAll.Rows[i]["OTHER"] != DBNull.Value)
                        {
                            ot += Convert.ToInt32(dtAll.Rows[i]["OTHER"]);
                        }
                    }

                    DataRow row1 = dtAll.NewRow();
                    row1["CNAME"] = "T O T A L";

                    row1["INTERESTDUE"] = String.Format("{0:0.00}", db10);

                    dtAll.Rows.Add(row1);

                    //************Loanname*******


                    DataTable dtgridmain = new DataTable();
                    dtgridmain.Columns.Add("Sno", typeof(int));
                    dtgridmain.Columns.Add("Loan-Type", typeof(string));
                    dtgridmain.Columns.Add("Loan-No", typeof(string));
                    dtgridmain.Columns.Add("Name", typeof(string));
                    dtgridmain.Columns.Add("Interestdue", typeof(string));

                    if (Session["rdbmember"].ToString() == "Selected")
                    {
                        dtgridmain.Columns.Add("MemType", typeof(string));
                        dtgridmain.Columns.Add("MemNo", typeof(string));
                    }
                    int sno = 1, ck = 0;
                    foreach (DataRow rgrd in dtAll.Rows)
                    {
                        DataRow grdmn = dtgridmain.NewRow();
                        if (dtAll.Rows[ck]["CNAME"].ToString() != "T O T A L" && dtAll.Rows[ck]["LNO"] != DBNull.Value)
                        {
                            grdmn["Sno"] = sno++;
                        }
                        ck++;
                        grdmn["Loan-Type"] = rgrd["LOANABBR"];
                        grdmn["Loan-No"] = rgrd["LNO"];
                        grdmn["Name"] = rgrd["CNAME"];
                        grdmn["Interestdue"] = String.Format("{0:0.00}", rgrd["INTERESTDUE"]);

                        if (Session["rdbmember"].ToString() == "Selected")
                        {
                            grdmn["MemType"] = rgrd["MEMABBR"];
                            grdmn["MemNo"] = String.Format("{0:0.00}", rgrd["MEMNO"]);
                        }
                        dtgridmain.Rows.Add(grdmn);

                    }

                    GlobalConstants.strVocDate = Session["asondate"].ToString();
                    GlobalConstants.StrHeading = "Loan Overdue As On f1 " + (Convert.ToInt32(Session["instalmentdue"]) + "-9999") + " " + GlobalConstants.strVocDate;
                    Session["LoanOverdue"] = dtgridmain;
                    Response.Redirect("rptDisp_LoanInterestDue.aspx?branch=" + Session["LoginBranchName"].ToString() + "&Brn=" + GlobalConstants.strBnkName + "&Head=" + GlobalConstants.StrHeading, false);

                }

            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
                ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('" + ex.Message + "')</script>");
            }
            finally
            {
                OracleConnection.ClearAllPools();
            }
        }
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

        Response.Redirect("LoanInterestDue.aspx", false);
    }

}

