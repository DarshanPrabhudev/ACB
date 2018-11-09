using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Net;
using System.Text;  
using System.Data.OracleClient;
using System.ComponentModel;
using DAL;

//Created By Husna  on Sep-2013

    [ToolboxData("<{0}:MyBackButton runat=server></{0}:MyBackButton>")]
    public partial class BankLogin : System.Web.UI.Page
    {

        public DAL.conn objcon = new DAL.conn();
        public DAL.DataFetch objdal = new DAL.DataFetch();
        public DataTable dtnew = new DataTable();
        public DataTable dtnew1 = new DataTable();
        public DataTable dt = new DataTable();
        public DataTable dt1 = new DataTable();
        public DataTable dt2 = new DataTable();
        public DataTable dt3 = new DataTable();
        public DataTable dt4 = new DataTable();
        public DataTable dt5 = new DataTable();
        public DataTable dt6 = new DataTable();
        public DataTable dt7 = new DataTable();
        public DataTable dtSite = new DataTable(); 
        public string strsql, strTime, pasdcr, strasciiD, strtempascii = " ", gComputerName = "";//strComputerName
        public string strascii = "", gblnSupervisor, gblLoginname, gSysDate, strConnectionString = "";
        public double dblSno, DblretivedGenRandNo;
        public int intRow = 0, temprand;
        public int intCol = 0;
        public int intLen, intCounter, intPassDays, intPassExpDays, gUserId, dblcrypt;
        public int b = 1;
        public string[,] strRtrRand = new string[12, 100];
        public string[,] strstoreRand = new string[20, 100];
        public DataTable dtBranch = new DataTable();
        
        protected void Page_Load(object sender, EventArgs e)
         {   
                if (!Page.IsPostBack)
                {
                    txtUserName.Focus();
                    Session.Abandon();
                    Session.Clear();
                }
        }       
      

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ddlBranch.Items.Clear();
            txtUserName.Text = "";
            txtPassword.Text = "";
            ddlBranch.Enabled = true;
        }
        public void WriteLog(string logMessage)
        {
            string sLogPath = HttpContext.Current.Server.MapPath("cbslog.txt");
            System.IO.StreamWriter file = new System.IO.StreamWriter(sLogPath, true);
            file.WriteLine(DateTime.Now.ToString() + ":" + logMessage);
            file.Close();
            return;
        }

        protected void txtUserName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtUserName.Text = txtUserName.Text.ToUpper();
                ddlBranch.Enabled = true;
                strsql = "SELECT USER_BRANCH,USER_ADMINYN,USER_ID FROM MASTER.genusers WHERE USER_NAME='" + txtUserName.Text.ToUpper().Trim() + "'";               
                dtBranch = objdal.LoginExecute(strsql);
                DataTable dtconst = new DataTable();
                DataTable dtconst1 = new DataTable();
                if (dtBranch.Rows.Count > 0)
                {
                    ////strsql = "select GNBR_USERID,GNBR_BRANID from master.genuserbranch where GNBR_USERID=" + Convert.ToInt32(dtBranch.Rows[0]["USER_ID"]) + "";
                    ////DataTable dtconst4 = new DataTable();
                    ////dtconst4 = objdal.LoginExecute(strsql);
                    ////if (dtconst4.Rows.Count > 1)
                    ////{
                    ////    DataTable dtcurs = new DataTable();
                    ////    dtcurs.Columns.Add("Branch-Name", typeof(string));
                    ////    dtcurs.Columns.Add("Branch-Sno", typeof(string));
                    ////    for (int i = 0; i < dtconst4.Rows.Count; i++)
                    ////    {
                    ////        strsql = "SELECT BRAN_SNO,BRAN_NAME FROM MASTER.branch where BRAN_SNO= " + Convert.ToInt32(dtconst4.Rows[i]["GNBR_BRANID"]) + " ";
                    ////        dtconst1 = objdal.LoginExecute(strsql);
                    ////        DataRow drcur = dtcurs.NewRow();
                    ////        drcur["Branch-Name"] = dtconst1.Rows[0]["BRAN_NAME"].ToString();
                    ////        drcur["Branch-Sno"] = dtconst1.Rows[0]["BRAN_SNO"].ToString();
                    ////        dtcurs.Rows.Add(drcur);
                    ////        dtconst1.Clear();
                    ////    }
                    ////    dtcurs.DefaultView.Sort = "Branch-Name asc";
                    ////    ddlBranch.DataSource = dtcurs;
                    ////    ddlBranch.DataTextField = "Branch-Name";
                    ////    ddlBranch.DataValueField = "Branch-Sno";
                    ////    ddlBranch.DataBind();
                    ////    ddlBranch.SelectedValue = "1";
                    ////    ddlBranch.Enabled = false;
                    ////    txtPassword.Focus();
                    ////}// if rows count >0
                    ////else if (dtconst4.Rows.Count <= 1)
                    ////{
                        strsql = "SELECT BRAN_SNO,BRAN_NAME FROM MASTER.branch WHERE BRAN_SNO=" + Convert.ToInt32(dtBranch.Rows[0]["USER_BRANCH"]) + "";
                        DataTable dtconst2 = new DataTable();
                        dtconst2 = objdal.LoginExecute(strsql);
                        if (dtconst2.Rows.Count > 0)
                        {
                            ddlBranch.DataSource = dtconst2;
                            ddlBranch.DataTextField = "BRAN_NAME";
                            ddlBranch.DataValueField = "BRAN_SNO";
                            ddlBranch.DataBind();
                            if (!DBNull.Value.Equals(dtconst2.Rows[0]["BRAN_SNO"]))
                            {
                                ddlBranch.SelectedValue = dtconst2.Rows[0]["BRAN_SNO"].ToString();
                            }
                            ddlBranch.Enabled = false;
                            txtPassword.Focus();
                        }
                   // }
                }
                else
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Invalid Userid')</script>");
                    return;
                }
            }
            catch (Exception ex)
            {
                string g = ex.Message;
                WriteLog(g);
               // btnCancel_Click(null, null);
                 txtUserName.Focus();
                ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Invalid Userid')</script>");
                return;
            }
            finally
            {
                OracleConnection.ClearAllPools();
            }
        }
      

        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void imgbtnLogin_Click(object sender, ImageClickEventArgs e)
        {
            Session["userName"] = "";
            Session["UserRole"] = "";
            Session["LoginDttm"] = "";
            try
            {

                Session["SenderID"] = "Bank";

                strsql = " SELECT SITP_TEXTVALUE FROM SITEPARAMETERS WHERE SITP_NAME='SMS SENDER ID'";
                dtSite  = objdal.LoginExecute(strsql);

                if (dtSite.Rows.Count > 0)
                {
                    Session["SenderID"] = dtSite.Rows[0]["SITP_TEXTVALUE"].ToString(); 
                }

                strTime = System.DateTime.Now.ToLongTimeString();
                strsql = " select user_id ,user_id sno, user_name,user_fullname, User_Password,USER_BRANCH, User_AdminYN,";
                strsql = strsql + " TO_CHAR(Sysdate,'DD-MM-YYYY') as LoginDttm,USER_PASSDATE,user_reset_pass,USER_DATEOFBIRTH";
                strsql = strsql + " from MASTER.GENUSERS where Upper(user_name)= '" + txtUserName.Text.ToUpper() + "'";
                strsql = strsql + " and USER_ACTIVEYN  ='Y'";
                dt = objdal.LoginExecute(strsql);
                WriteLog(strsql + "MASTER.GENUSERS");
                
                if (dt.Rows.Count <= 0)
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Invalid UserName and Password')</script>");
                    return;
                }
                else if (dt.Rows.Count > 1)
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Conflict in User Name and Password')</script>");
                    return;
                }
                strascii = dt.Rows[0]["User_Password"].ToString();
                dblSno = double.Parse(dt.Rows[0]["sno"].ToString());
                Session["gblnSupervisor"] = dt.Rows[0]["User_AdminYN"].ToString();
                Session["userName"] = dt.Rows[0]["user_name"].ToString();
                Session["LoginDttm"] = dt.Rows[0]["LoginDttm"].ToString();

                //--hb
                DataTable dtRole=new DataTable();
                strsql ="";
                strsql =" SELECT role_des FROM master.genuserrole,master.genrole";
                strsql = strsql + " WHERE   master.genrole.ROLE_NO=USRO_ROLEID and USRO_USERID=" + dt.Rows[0]["user_id"].ToString();
                dtRole = objdal.LoginExecute(strsql);
                WriteLog(strsql + "MASTER.USERS  role");
                string[] rl = new string[dtRole.Rows.Count];
                DataRow dr = dtRole.Rows[0];
                string sRoles="";
                if (dtRole.Rows.Count > 0)
                {
                    for (int i = 0; i < dtRole.Rows.Count; i++)
                    {
                        rl[i] = dtRole.Rows[i]["role_des"].ToString() + ",";
                        sRoles = sRoles + rl[i];
                    }
                    Session["UserRole"] = sRoles;
                    WriteLog("Bank Login.aspx---sroles =="  + sRoles);
                }
                //--hb

                strsql = " select PSWL_RNO,PSWL_ROWID from MASTER.passwordlink where PSWL_SNO ='" + dblSno + "'";
                dt1 = objdal.LoginExecute(strsql);
                int N = 0;
                WriteLog(strsql + "MASTER.passwordlink");
                if (dt1.Rows.Count > 0)
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        strstoreRand[Convert.ToInt32(dt1.Rows[N]["PSWL_ROWID"].ToString()), intCol] = dt1.Rows[N]["PSWL_RNO"].ToString();
                        N = N + 1;
                    }
                }
                string s4 = txtPassword.Text.ToUpper();
                string s = txtPassword.Text.ToUpper();
                char[] a = s.ToCharArray();
                Array.Reverse(a);
                s = new string(a);
                intLen = s.Length;
                DblretivedGenRandNo = double.Parse(strstoreRand[intRow, intCol].ToString());
                intRow = intRow + 1;
                for (int i = 0; i < intLen; i++)
                {
                    if (strstoreRand[intRow, intCol] !=null)
                    {
                           temprand = Convert.ToInt32(Math.Sqrt(Convert.ToInt32(strstoreRand[intRow, intCol].ToString()) - Convert.ToInt32(strstoreRand[0, 0].ToString())));                 
                    }
                    intRow = intRow + 1;
                    int intLen1 = s.Length;
                    string str = s.Substring(intLen1 - 1);
                    intLen1 = intLen1 - 1;
                    char c = s4[i];
                    strsql = " select ascii('" + c + "') key from dual";
                    dt2 = objdal.LoginExecute(strsql);
                    WriteLog(strsql + "dual");
                    if (dt2.Rows.Count > 0)
                    {
                        strtempascii = dt2.Rows[0]["key"].ToString();
                        int s1 = 0, s2 = 0, s3 = 0;
                        s1 = ((temprand) + Convert.ToInt32(strstoreRand[0, 0]));
                        s2 = Convert.ToInt32(strtempascii);
                        s3 = ((s2) ^ (s1));
                        strasciiD = strasciiD + " " + (s3).ToString();
                        for (int j = dt2.Rows.Count - 1; j >= 0; j--)
                        dt2.Rows[j].Delete(); 
                    }     
            
                }
                //21977 14824 27539 21605 15869 24510 30971 20217
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    strstoreRand[intRow, intCol] = "";
                }
                string strHostName = "";
                strHostName = System.Net.Dns.GetHostName();
                IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(strHostName);
                IPAddress[] addr = ipEntry.AddressList;
                Session["strComputerName"] = addr[addr.Length - 1].ToString();
                WriteLog(Session["strComputerName"].ToString());
                if (strascii.Trim() == strasciiD.Trim())
                {

                    //strsql = " SELECT BRAN_IPADDRESS,BRAN_NAME, BRAN_SERVICENAME, BRAN_DBUSER, BRAN_DBPASSWORD,BRAN_HOBRANCH From MASTER.Branch";
                    //strsql = strsql + " WHERE BRAN_SNO =" + dt.Rows[0]["USER_BRANCH"].ToString();// Convert.ToInt32(ddlBranch.SelectedValue) + " ";
                    //dt7 = objdal.LoginExecute(strsql);
                    //Session["branchname"] = dt7.Rows[0]["BRAN_NAME"].ToString();
                    //Session["gUserId"] = Convert.ToInt32(dt.Rows[0]["user_id"].ToString());
                    //Session["BranchLogin"] = dt.Rows[0]["USER_BRANCH"].ToString();
                    //ddlBranch.SelectedItem.Text = Session["branchname"].ToString();
                   
                    //if (dt7.Rows.Count > 0)
                    //{
                    //    //BY HUSNA ASST BY INAYATH SIR AND IMTIYAZ SIR
                    //    Session["role"] = dt7.Rows[0]["BRAN_HOBRANCH"].ToString(); 

                    //    string StrServiceName = dt7.Rows[0]["BRAN_SERVICENAME"].ToString();
                    //    string StrDBUSer = dt7.Rows[0]["BRAN_DBUSER"].ToString();
                    //    string StrDBPass = StrDBUSer;
                    //    strConnectionString = "Password=";
                    //    strConnectionString = strConnectionString + StrDBPass + ";";
                    //    strConnectionString = strConnectionString + " User ID=" + StrDBUSer + ";";
                    //    strConnectionString = strConnectionString + " Data Source=" + StrServiceName;
                    //    string strdName = dt7.Rows[0]["BRAN_SERVICENAME"].ToString();

                    //}
                    //Session["constring"] = strConnectionString;
                    //global.Loginconn = strConnectionString;
                    //Session["name"] = txtUserName.Text.ToUpper();
                  //  Session["BranchLogin"] = Session["BranchLogin"];// Convert.ToInt32(ddlBranch.SelectedValue);
                    //Session["BranchLogin"] = (ddlBranch.SelectedValue);
                    //Session["branchname"] = (ddlBranch.SelectedItem.Text);


                   // Response.Redirect("frmsaved.aspx", false);

                  //  Response.Redirect("rtgsMenu.aspx",  false);
                    
                }
                else if (strascii != strasciiD && intCounter <= 2)
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Invalid User name or Password')</script>");
                    return;
                }
                else
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Branch Database Not Found')</script>");
                    return;
                }
                //Moved by LS and commented above on 28-dec-2016
                strsql = " SELECT BRAN_IPADDRESS,BRAN_NAME, BRAN_SERVICENAME, BRAN_DBUSER, BRAN_DBPASSWORD,BRAN_HOBRANCH From MASTER.Branch";
                strsql = strsql + " WHERE BRAN_SNO =" + dt.Rows[0]["USER_BRANCH"].ToString();// Convert.ToInt32(ddlBranch.SelectedValue) + " ";
                dt7 = objdal.LoginExecute(strsql);
                Session["branchname"] = dt7.Rows[0]["BRAN_NAME"].ToString();
                Session["gUserId"] = Convert.ToInt32(dt.Rows[0]["user_id"].ToString());
                Session["BranchLogin"] = dt.Rows[0]["USER_BRANCH"].ToString();
                ddlBranch.SelectedItem.Text = Session["branchname"].ToString();

                if (dt7.Rows.Count > 0)
                {
                    //BY HUSNA ASST BY INAYATH SIR AND IMTIYAZ SIR
                    Session["role"] = dt7.Rows[0]["BRAN_HOBRANCH"].ToString();

                    string StrServiceName = dt7.Rows[0]["BRAN_SERVICENAME"].ToString();
                    string StrDBUSer = dt7.Rows[0]["BRAN_DBUSER"].ToString();
                    string StrDBPass = StrDBUSer;
                    strConnectionString = "Password=";
                    strConnectionString = strConnectionString + StrDBPass + ";";
                    strConnectionString = strConnectionString + " User ID=" + StrDBUSer + ";";
                    strConnectionString = strConnectionString + " Data Source=" + StrServiceName;
                    string strdName = dt7.Rows[0]["BRAN_SERVICENAME"].ToString();

                }
                Session["constring"] = strConnectionString;
                global.Loginconn = strConnectionString;
                Session["name"] = txtUserName.Text.ToUpper();
                //  Session["BranchLogin"] = Session["BranchLogin"];// Convert.ToInt32(ddlBranch.SelectedValue);
                //Session["BranchLogin"] = (ddlBranch.SelectedValue);
                //Session["branchname"] = (ddlBranch.SelectedItem.Text);


                // Response.Redirect("frmsaved.aspx", false);

                Response.Redirect("rtgsMenu.aspx", false);

                Session["RTGSLogPath"] = @"D:\IPS\tempInput\";
                Session["RTGSLogName"] = "SBSBN_SBSBNUPLD_";
                Session["CorpPushUtility"] = @"D:\IPS\";

        }
        catch (Exception ex)
        {
            string g = ex.Message;
            WriteLog(ex.Source + " " + ex.Message + ex.InnerException);
            btnCancel_Click(null, null);
            ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('" + g + "')</script>");
            return;
        }
        finally
        {
            OracleConnection.ClearAllPools();
        }
    }








     
}
