//**---------------------------FORM15H REPORT---------------------------
/*
 * Version    Created By         Date         Assigned By
 * 1.1        Harshith        30.04.2015      Venkatesh 
 * -----------------------------------------------------------------------
 * -----------------------------PSEUDOCODE--------------------------------
 
 * * Purpose of this webform is to print the Form15H report for selected client.
 *-------Tables used------------
 *Form15H
 *Depositmaster
 * * *-------Stored Procedures------------
 * FORM15HANDG.SCHEDULEIII
 * 
 * */
using System;
using DAL;
using System.Data.OracleClient;
using System.Data;
using System.Web.UI;

public partial class Form15HLog : System.Web.UI.Page
{    
    DAL.DataFetch objDataFetch = new DataFetch();
    Log objLog = new Log();
    global objGlb = new global();
    
    OracleConnection oraConn = new OracleConnection();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            global.checkSession();
            if (!Page.IsPostBack)
            {
                if (Session["constring"] == null)
                {
                    Response.Redirect("ShowMessage.aspx?msg=" + GlobalConstants.msg);
                }
            }
        }
        catch (Exception ex)
        {
            objLog.WriteLog("Form15HLOG: Error in Page Loading " + ex.Message);
            ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert('Error in Page Loading: " + ex.Message + "')</script>");
        }

        objLog.WriteLog("Form15HLOG:Page Loaded Successfully");

    }
    protected void txtCCode_TextChanged(object sender, EventArgs e)
    {

        string strSql = "",strDays;
        int Age = 0;
        double dblAcNo, dblAmount = 0.00;        
        DataTable dtFormdet = new DataTable();
        DataTable dtForm15Det = new DataTable();
        DateTime dTimeDate, dTimeTempDate, dTimeFromDate, dTimeSDate, dTimeToDate, dTimeDOB, dTimeServerDate;

        if (txtCCode.Text == string.Empty || txtAmount.Text == string.Empty || txtFromdate.Text == string.Empty || txtTodate.Text == string.Empty)
        {
            ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert ('Please fill the Details...!!') </script>");
            return;
        }
        try
        {

            if (Session["constring"] != null)
            {
                oraConn = new OracleConnection(Session["constring"].ToString());
                if (oraConn.State != ConnectionState.Open)
                    oraConn.Open();
            }

            dblAmount = Convert.ToDouble(txtAmount.Text);
            dblAcNo = Convert.ToDouble(txtCCode.Text);
            dTimeFromDate = Convert.ToDateTime(txtFromdate.Text);
            dTimeToDate = Convert.ToDateTime(txtTodate.Text);
            dTimeTempDate = Convert.ToDateTime(txtFromdate.Text).AddYears(1);
            dTimeSDate = Convert.ToDateTime("01/04/" + dTimeFromDate.Year);
            dTimeDate = Convert.ToDateTime("31/03/" + dTimeTempDate.Year);
            Session["strDate"] = dTimeFromDate;
            Session["dDate"] = dTimeDate;
            //get the client details
            dtForm15Det = getForm15HDet(dTimeFromDate, dTimeToDate, dblAmount, dblAcNo, oraConn);


            if (dtForm15Det.Rows.Count == 0)
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Data Not Found')</script>");
                txtCCode.Text = "";
                lblName.Text = "";
                return;
            }
            if (dtForm15Det.Rows.Count > 0)
            {
                if ((dtForm15Det.Rows[0]["N_PAN"].ToString().Trim() == string.Empty) || (dtForm15Det.Rows[0]["N_DOB"].ToString().Trim() == string.Empty))
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Not eligible')</script>");
                    txtCCode.Text = "";
                    lblName.Text = "";
                    return;
                }
                if (!DBNull.Value.Equals(dtForm15Det.Rows[0]["N_DOB"].ToString()))
                {
                    dTimeDOB = Convert.ToDateTime(dtForm15Det.Rows[0]["N_DOB"]);
                    objLog.WriteLog("Inside Form15HLog->txtCCode_TextChanged()->before getserverdate");
                    dTimeServerDate = Convert.ToDateTime(objGlb.GetServerDate());
                    objLog.WriteLog("Inside Form15HLog->txtCCode_TextChanged()->after getserverdate");
                    strDays = (dTimeServerDate - dTimeDOB).TotalDays.ToString();
                    Age = Convert.ToInt32(strDays) / 365;
                    Session["Age"] = Age;
                }
                strSql = "SELECT FORM_TYPE FROM HEAD.FORM15H WHERE FORM_CLIENT='" + txtCCode.Text + "'";
                strSql += " AND FORM_FORMDATE <= To_date('" + txtFromdate.Text + "','dd-mm-yyyy')";
                strSql += " AND FORM_TODATE >= To_date('" + txtTodate.Text + "','dd-mm-yyyy')";
                dtFormdet = objDataFetch.DatatablePassSQL(strSql);

                if (rdbtnForm15.Items[0].Selected == true)
                {

                    if (dtFormdet.Rows.Count > 0)
                    {
                        if ((!DBNull.Value.Equals(dtFormdet.Rows[0]["FORM_TYPE"].ToString())))
                        {
                            if (dtFormdet.Rows[0]["FORM_TYPE"].ToString().Trim() == "F15H")
                            {
                                
                                if ((!DBNull.Value.Equals(dtForm15Det.Rows[0]["N_DOB"].ToString())) && (!DBNull.Value.Equals(dtForm15Det.Rows[0]["N_PAN"].ToString())) && (!DBNull.Value.Equals(dtForm15Det.Rows[0]["N_NAME"].ToString())))
                                {
                                    
                                    if (Age >= 60)
                                    {
                                        lblName.Text = dtForm15Det.Rows[0]["N_NAME"].ToString();
                                        Session["dtForm15Det"] = dtForm15Det;
                                    }
                                    else
                                    {
                                        ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Not eligible')</script>");
                                        txtCCode.Text = "";
                                        lblName.Text = "";
                                        return;
                                    }
                                }
                                else
                                {
                                    ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Enter the Valid Client Number')</script>");
                                    txtCCode.Text = "";
                                    lblName.Text = "";
                                    return;
                                }
                            }
                            else
                            {
                                ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('This Client is not marked In Form15H Link Set Up')</script>");
                                txtCCode.Text = "";
                                lblName.Text = "";
                                return;
                            }
                        }

                        else
                        {
                            ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('This Client is not marked In Form15H Link Set Up')</script>");
                            txtCCode.Text = "";
                            lblName.Text = "";
                            return;
                        }

                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('This Client is not eligible for Form15H')</script>");
                        txtCCode.Text = "";
                        lblName.Text = "";
                        return;
                    }
                }
                else
                {
                    if ((!DBNull.Value.Equals(dtFormdet.Rows[0]["FORM_TYPE"].ToString())))
                    {
                        if (dtFormdet.Rows[0]["FORM_TYPE"].ToString().Trim() == "F15G")
                        {
                            if ((dtForm15Det.Rows[0]["N_PAN"].ToString().Trim() == string.Empty) || (dtForm15Det.Rows[0]["N_DOB"].ToString().Trim() == string.Empty))
                            {
                                ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Not eligible')</script>");
                                txtCCode.Text = "";
                                lblName.Text = "";
                                return;
                            }

                            if ((!DBNull.Value.Equals(dtForm15Det.Rows[0]["N_PAN"].ToString())) && (!DBNull.Value.Equals(dtForm15Det.Rows[0]["N_NAME"].ToString())))
                            {
                                if ((Age < 60) && (Age > 0))
                                {
                                    lblName.Text = dtForm15Det.Rows[0]["N_NAME"].ToString();
                                    Session["dtForm15Det"] = dtForm15Det;
                                }
                                else
                                {
                                    ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Not eligible')</script>");
                                    txtCCode.Text = "";
                                    lblName.Text = "";
                                    return;
                                }
                            }
                            else
                            {
                                ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Enter The Valid Client Number')</script>");
                                txtCCode.Text = "";
                                lblName.Text = "";
                                return;
                            }
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('This Client is not marked In Form15G Link Set Up')</script>");
                            txtCCode.Text = "";
                            lblName.Text = "";
                            return;
                        }
                    }

                    else
                    {
                        ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('This Client is not marked In Form15H Link Set Up')</script>");
                        txtCCode.Text = "";
                        lblName.Text = "";
                        return;
                    }
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Data Does Not Exists!!! Please check the client Code ')</script>");
                txtCCode.Text = "";
                lblName.Text = "";
                return;
            }

        }
        catch (Exception ex)
        {
            objLog.WriteLog("Inside Form15HLog->txtCCode_TextChanged()" + ex.Message);
            ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('" + ex.Message + "')</script>");
            return;
        }
        finally
        {
            if (oraConn.State == ConnectionState.Open)
            oraConn.Close();
            oraConn.Dispose();
            OracleConnection.ClearAllPools();
            dtForm15Det.Dispose();
            dtFormdet.Dispose();
           
        }
    }
    protected DataTable getForm15HDet(DateTime dTimeFromDate, DateTime dTimeToDate, double dblAmount, double dblAcNo, OracleConnection oraConn)
    {
        DataTable dtForm15Details = new DataTable();
        OracleCommand oraCmd = new OracleCommand("HEAD.DEPOSIT_PAYABALENEW.ESTIMATION", oraConn);
        oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
        oraCmd.Parameters.Add("V_BRANCH", OracleType.Number).Value = DBNull.Value;
        oraCmd.Parameters.Add("V_ABBR", OracleType.VarChar).Value = DBNull.Value;
        oraCmd.Parameters.Add("V_FROMD", OracleType.DateTime).Value = String.Format("{0:dd-MMM-yyyy}", dTimeFromDate);
        oraCmd.Parameters.Add("V_TOD", OracleType.DateTime).Value = String.Format("{0:dd-MMM-yyyy}", dTimeToDate);
        oraCmd.Parameters.Add("V_AMOUNT", OracleType.Number).Value = dblAmount;
        oraCmd.Parameters.Add("V_MEMYN", OracleType.VarChar).Value = DBNull.Value;
        oraCmd.Parameters.Add("V_FROM", OracleType.Number).Value = dblAcNo;
        oraCmd.Parameters.Add("V_TO", OracleType.Number).Value = dblAcNo;
        oraCmd.Parameters.Add("V_OUTTYPE", OracleType.VarChar).Value = "D";
        oraCmd.Parameters.Add("V_CUR", OracleType.Cursor).Direction = System.Data.ParameterDirection.Output;
        OracleDataAdapter old = new OracleDataAdapter(oraCmd);
        old.Fill(dtForm15Details);
        return dtForm15Details;
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        DataTable dtForm15DetFin = new DataTable();
        try
        {
            if (txtCCode.Text == string.Empty || txtAmount.Text == string.Empty || txtFromdate.Text == string.Empty || txtTodate.Text == string.Empty)
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert ('Please fill all the Details...!!') </script>");
                return;
            }
            if (Session["dtForm15Det"] != null)
            {
                dtForm15DetFin = (DataTable)Session["dtForm15Det"];
            }
            if (dtForm15DetFin.Rows.Count == 0)
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Data Not Found')</script>");
                txtCCode.Text = "";
                lblName.Text = "";
                return;
            }
            if (dtForm15DetFin.Rows.Count > 0)
            {

                if (rdbtnForm15.Items[0].Selected == true)
                {
                    Response.Redirect("Form15HReportFinal.aspx");
                }
                else
                {
                    Response.Redirect("Form15GReport.aspx");
                }

            }
        }
        catch (Exception ex)
        {
            objLog.WriteLog("Inside Form15HLog->btnok_click()" + ex.Message);
            ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('" + ex.Message + "')</script>");
            return;
        }
        finally
        {
            dtForm15DetFin.Dispose();
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearAllCtrls();
    }
    private void ClearAllCtrls()
    {
        txtFromdate.Text = "";
        txtCCode.Text = "";
        lblName.Text = "";
        txtAmount.Text = "";
        txtTodate.Text = "";
        Session["dtForm15Det"] = null;
        Session["strDate"] = null;
        Session["dDate"] = null;
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("rmenu.aspx");
    }
}