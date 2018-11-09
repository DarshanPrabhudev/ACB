//*-------------------------------Form C Details------------------------------------
//*
//* Version       Modified By     Date                  Remarks
//* -------------------------------------------------------------------------------
//*  1.0            ---             -----               -----
//*  2.0          Darshan         17-12-2016          Taken to Ajax and XML Implementation.
//*                                                             correction in original code.
//*--------------------------------------------------------------------------------


using System;
using System.Data;
using System.Data.OracleClient;
using System.Text.RegularExpressions;

    public partial class FormC : System.Web.UI.Page
    {
        // DAL.conn objcon = new DAL.conn();
        // DAL.DataFetch objdal = new DAL.DataFetch();
        string strsql = "", strdtfrm = "", To_Date = "", g = "";
        XMLMetaMasters objXml = new XMLMetaMasters();
        global objGlobal = new global();
        Log objLog = new Log();
        RptQueries objRpt = new RptQueries();

        protected void Page_Load(object sender, EventArgs e)
        {
            global.checkSession();
            OracleConnection oraConn = new OracleConnection(Session["constring"].ToString());
            try
            {
                if (!IsPostBack)
                {
                    if (oraConn.State != ConnectionState.Open) oraConn.Open();
                    // ADDED BY DARSHAN
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
                    //---------------------
                }
            }
            catch (Exception ex)
            {
                g = ex.Message;
                g = Regex.Replace(g, "[^a-zA-Z0-9_]+", " ");
                objLog.WriteLog("form c Details Exception in PageLoad() : " + g);
                ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert('" + g + "')</script>");
                return;
            }
            finally
            {
                oraConn.Close();
                oraConn.Dispose();
                objLog.WriteLog("FormC.aspx: Page_Load-->Page Load events Finally block Executed  ");             
            }
        }
        protected int FormGlink(int brnfrmc, String fmmdat, string todat)
        {
            OracleConnection oraConn = new OracleConnection(Session["constring"].ToString());            
            int glval = 0;
            DataTable adofrmc = new DataTable();
            try
            {
                if (oraConn.State != ConnectionState.Open) oraConn.Open();
                strsql = objRpt.GLlink(fmmdat, fmmdat, brnfrmc);
                OracleDataAdapter oda = new OracleDataAdapter(strsql, oraConn);
                oda.Fill(adofrmc);
                objLog.WriteLog("FormGlink Query" + strsql);
                if (adofrmc.Rows.Count > 0)
                {
                    glval = Convert.ToInt32(adofrmc.Rows[0]["TRND_GLLINK"]);
                }
            }
            catch (Exception ex)
            {
                g = ex.Message;
                g = Regex.Replace(g, "[^a-zA-Z0-9_]+", " ");
                objLog.WriteLog("Form C Details Exception in FormGlink() : " + g);
                ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert('" + g + "')</script>");                
            }
            finally
            {
                oraConn.Close();
                oraConn.Dispose();
                objLog.WriteLog("Formc.aspx: FormGlink() reached the finally block");
            }
            return glval;
        }
        protected DataTable FormDetailsC(int brnch, string frmdat, string todat, int gllink)
        {
            DataTable adonpa = new DataTable();
            OracleConnection oraConn = new OracleConnection(Session["constring"].ToString());
            try
            {
                if (oraConn.State != ConnectionState.Open) oraConn.Open();
                strsql = objRpt.formcDetails(frmdat,todat,gllink,brnch);
                OracleDataAdapter odaNPA = new OracleDataAdapter(strsql, oraConn);
                odaNPA.Fill(adonpa);                
                objLog.WriteLog("FormDetailsC Query:"+strsql);
            }
            catch (Exception ex)
            {
                g = ex.Message;
                g = Regex.Replace(g, "[^a-zA-Z0-9_]+", " ");
                objLog.WriteLog("Form C Details Exception in FormDetailsC() : " + g);
                ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert('" + g + "')</script>");                
            }
            finally
            {
                oraConn.Close();
                oraConn.Dispose();
                objLog.WriteLog("Formc.aspx: FormDetailsC() reached the finally block");
            }
            return adonpa;
        }
        protected DataTable FormCAddress(int sno)
        {
            DataTable dtFromAddress = new DataTable();
            OracleConnection oraConn = new OracleConnection(Session["constring"].ToString());
            try
            {
                if (oraConn.State != ConnectionState.Open) oraConn.Open();
                dtFromAddress = objXml.DataTableMetadataXML("FORMCBANK.xml", "FORMC_SNO=" + sno);
                objLog.WriteLog("form c Details Query:" + strsql);
            }
            catch (Exception ex)
            {
                g = ex.Message;
                g = Regex.Replace(g, "[^a-zA-Z0-9_]+", " ");
                objLog.WriteLog("Form C Details Exception in FormCAddress() : " + g);
                ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert('" + g + "')</script>");
            }
            finally
            {
                oraConn.Close();
                oraConn.Dispose();
                objLog.WriteLog("Formc.aspx: FormCAddress event reached the finally block");
            }
            return dtFromAddress;
        }
        protected void btnOK_Click(object sender, EventArgs e)
        {
            int glink = 0, i = 0, dryr = 0, drawe=0;
            string mangr = "";
            DataTable dtdetails = new DataTable();
            DataTable dtdrawer = new DataTable();
            DataTable dtdrawee = new DataTable();
            DataTable dtglno = new DataTable();
            DataTable dtglnoDetails = new DataTable();
            strdtfrm = Convert.ToDateTime(txtfrom.Text).ToString("dd/MMM/yyyy");
            To_Date = Convert.ToDateTime(txtto.Text).ToString("dd/MMM/yyyy");
            OracleConnection oraConn = new OracleConnection(Session["constring"].ToString());

            //strdtfrm = ddfrmday.SelectedItem.Value + "-" + ddfrmMonth.SelectedItem.Text + "-" + ddfrmYear.SelectedItem.Value;
            //To_Date = DropDownList1.SelectedItem.Value + "-" + DropDownList2.SelectedItem.Text + "-" + DropDownList3.SelectedItem.Value;

            try
            {
                if (oraConn.State != ConnectionState.Open) oraConn.Open();
                glink = FormGlink(Convert.ToInt32(ddlBranch.SelectedValue), strdtfrm, To_Date);
                dtdetails = FormDetailsC(Convert.ToInt32(ddlBranch.SelectedValue), strdtfrm, To_Date, glink);
                dtdetails.Columns.Add("Sno", typeof(int));
                dtdetails.Columns.Add("Issuing-Bank-Name(Drawer)", typeof(string));
                dtdetails.Columns.Add("Payee-Bank-Name(Drawee)", typeof(string));
                dtdetails.Columns.Add("Drawer Bank Code", typeof(int));
                dtdetails.Columns.Add("Drawee Bank Code", typeof(int));                
                if (dtdetails.Rows.Count > 0)
                {
                    for (i = 0; i < dtdetails.Rows.Count; i++)
                    {
                        dryr = Convert.ToInt32(dtdetails.Rows[i]["TRNH_DRAWER"]);
                        dtdrawer = FormCAddress(dryr);
                        dtdetails.Rows[i]["Issuing-Bank-Name(Drawer)"] = dtdrawer.Rows[0]["FORMC_BANK"].ToString() + "-" + dtdrawer.Rows[0]["FORMC_ADDRESS1"].ToString();
                        dtdetails.Rows[i]["Drawer Bank Code"] = Convert.ToInt32(dtdrawer.Rows[0]["FORMC_CODE"]);

                        drawe = Convert.ToInt32(dtdetails.Rows[i]["TRNH_DRAWEE"]);
                        //WriteLog(
                        dtdrawee = FormCAddress(drawe);
                        dtdetails.Rows[i]["Payee-Bank-Name(Drawee)"] = dtdrawee.Rows[0]["FORMC_BANK"].ToString();
                        dtdetails.Rows[i]["Drawee Bank Code"] = Convert.ToInt32(dtdrawee.Rows[0]["FORMC_CODE"]);
                    }
                }
                strsql = objRpt.getGLno(Convert.ToInt16(ddlBranch.SelectedValue), strdtfrm, To_Date);
                OracleDataAdapter odagl = new OracleDataAdapter(strsql, oraConn);                
                odagl.Fill(dtglno);                
                if (dtglno.Rows.Count > 0)
                {
                    strsql = " SELECT FORMC_BANK,FORMC_OFFICER, FORMC_ADDRESS1, FORMC_ADDRESS2,FORMC_ADDRESS3 FROM  FORMCBANK WHERE FORMC_GLCODE = " + Convert.ToInt32(dtglno.Rows[0][0]) + " ";
                    OracleDataAdapter odbank = new OracleDataAdapter(strsql, oraConn);
                    odbank.Fill(dtglnoDetails);
                    //dtglnoDetails = objXml.DataTableMetadataXML("FORMCBANK.xml", "FORMC_SNO=" + Convert.ToInt32(dtglno.Rows[0][0]));
                }

                if (dtdetails.Rows.Count > 0)
                {
                    for (i = 0; i < dtglnoDetails.Rows.Count; i++)
                    {
                        if (dtglnoDetails.Rows[i]["FORMC_OFFICER"].ToString() == "GENERAL MANAGER")
                        {
                            mangr = dtglnoDetails.Rows[i]["FORMC_BANK"].ToString() + "-" + dtglnoDetails.Rows[i]["FORMC_ADDRESS1"].ToString() + "-" + dtglnoDetails.Rows[i]["FORMC_ADDRESS2"].ToString();
                            break;
                        }
                    }
                    Session["Manager"] = mangr;
                    Session["table"] = dtdetails;
                    Session["frmdate"] = strdtfrm;
                    Session["todate"] = To_Date;
                    Session["branch"] = ddlBranch.SelectedItem.Text.ToString();
                    Response.Redirect("FormCSelect.aspx", false);
                }
                else
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('No Records to Display')</script>");
                }
            }
            catch (Exception ex)
            {
                g = ex.Message;
                g = Regex.Replace(g, "[^a-zA-Z0-9_]+", " ");
                objLog.WriteLog("Form C Details Exception in btnOk_Click : " + g);
                ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert('" + g + "')</script>");
                return;
            }
            finally
            {
                oraConn.Close();
                oraConn.Dispose();
                dtdetails.Dispose();                
                dtdrawer.Dispose();
                dtdrawee.Dispose();
                dtglno.Dispose();
                dtglnoDetails.Dispose();
                objLog.WriteLog("Formc.aspx: btnOk_Click event reached the finally block");
            }
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
           Response.Redirect(Gen.REPORT_MENU,false);
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            Response.Redirect("rptdayBook.aspx", false);
        }
    }
