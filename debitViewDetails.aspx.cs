//**---------------------------Debit View Details Front---------------------------
/*
 *   Version    Created By          Date           Assigned By
 *    1.0       Laxmikantha         09/08/2016     Venkatesh + LS 
 
 * -----------------------------------------------------------------------
 * -----------------------------PSEUDOCODE--------------------------------
 * */
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;

public partial class debitViewDetails : System.Web.UI.Page
{
    public int value;
    Log objLog = new Log();
    OracleConnection oraConn = new OracleConnection();
    protected void Page_Load(object sender, EventArgs e)
    {
        globalSession.checkSession();    
        try
        {
            if (!Page.IsPostBack)
            {  
                string sSql = "";
                if (Request.QueryString["id"] != null)
                {
                    value = Convert.ToInt32(Request.QueryString["id"]);
                }
                oraConn = new OracleConnection(Session["constring"].ToString());
                if (oraConn.State != ConnectionState.Open)
                {
                    oraConn.Open();
                }
                sSql = "SELECT DBMT_SNO,DBMT_FRONTDETAIL FROM DEBITMANDATE WHERE DBMT_SNO='" + value + "' ";
                OracleDataAdapter oda = new OracleDataAdapter(sSql, oraConn);
                DataTable dt = new DataTable();
                oda.Fill(dt);
                gvViewDetails.DataSource = dt;
                gvViewDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            objLog.WriteLog("debitViewDetails Exception in PageLoad : " + ex.Message);
            ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('" + ex.Message + "')</script>");
        }
        finally
        {
            if (oraConn.State == ConnectionState.Open)
                oraConn.Close();
            oraConn.Dispose();
            OracleConnection.ClearAllPools();
        }
    }
}