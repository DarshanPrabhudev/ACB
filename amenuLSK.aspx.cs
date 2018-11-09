using System;
using DAL;
using System.Data;
using System.Data.OracleClient;



public partial class amenuLSK : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["gblnSupervisor"] = "N";
        global.checkSession();
        if (Session["gblnSupervisor"].ToString() == "Y")
        {
            //hlEUNmsg.Visible = true;
            //hlEUNmsg.NavigateUrl = "SetupMsg.aspx";
        }
        if (Request.QueryString["LoginMsg"] != null)
        {
            string msg = Request.QueryString["LoginMsg"];
            ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert('" + msg + "')</script>");
            return;
        }
    }
}