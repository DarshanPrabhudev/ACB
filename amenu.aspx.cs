using System;
using DAL;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OracleClient;


public partial class amenu : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        global.checkSession();
        //if (Session["gblnSupervisor"].ToString() == "Y")
        //{
        //    //hlEUNmsg.Visible = true;
        //    //hlEUNmsg.NavigateUrl = "SetupMsg.aspx";
        //}
        if (!IsPostBack)
        {
            if (Request.QueryString["LoginMsg"] != null)
            {
                string msg = Request.QueryString["LoginMsg"];
                ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert('" + msg + "')</script>");
                return;
            }
        }
    }
    protected void lnkSignOut_Click(object sender, EventArgs e)
    {
        // Added by Darshan 09/03/2017
        string strSQL = "";
        OracleConnection oraConn = new OracleConnection(Session["constring"].ToString());
        if (oraConn.State != ConnectionState.Open)
        {
            oraConn.Open();
        }
        strSQL = "UPDATE MASTER.ACTIVITYUSER SET ACUS_STATUS = 'N',";
        strSQL += " ACUS_LOGOUT =(SELECT TO_DATE(TO_CHAR(SYSDATE,'DD-MON-YYYY HH:MI:SS AM'),'DD-MON-YYYY HH:MI:SS AM') FROM DUAL)";
        strSQL += " WHERE ACUS_USERID = " + Session["gUserId"] + " AND ACUS_STATUS = 'Y'";
        OracleCommand cmd = new OracleCommand(strSQL, oraConn);
        cmd.ExecuteNonQuery();
        //-----------------

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();


        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        HttpContext.Current.Response.Cache.SetNoServerCaching();
        HttpContext.Current.Response.Cache.SetNoStore();
        Session.Abandon();
        Session.RemoveAll();
        Session.Clear();
        //if (HttpContext.Current.Session.Contents.Count == 0)
        //{
        //    HttpContext.Current.Response.Redirect("~/ShowMessage.aspx");//" + HttpContext.Current.Request.ApplicationPath + "/" + Gen.GEN_ERROR);
        //}
        Response.Redirect("LOGIN.ASPX?Msg=Y");
        //Session.Abandon();
        //LogOut();
    }

    //protected void LogOut()
    //{
    //    Session.Abandon();
    //    Response.Redirect("LOGIN.ASPX");
    //}
}