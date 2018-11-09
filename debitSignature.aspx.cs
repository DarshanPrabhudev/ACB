//**---------------------------Signature---------------------------
/*
 *   Version  Created By          Date           Assigned By
 *    1.0     Laxmikantha         08/08/2016     Venkatesh + LS 
 
 * -----------------------------------------------------------------------
 * -----------------------------PSEUDOCODE--------------------------------
 * */
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;

public partial class debitSignature : System.Web.UI.Page
{
    public int value;
    Log objLog = new Log();
    OracleConnection oraConn = new OracleConnection();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    value = Convert.ToInt32(Request.QueryString["id"]);
                }
                string sSql = "";
                OracleConnection oraConn = new OracleConnection(Session["constring"].ToString());
                if (oraConn.State != ConnectionState.Open)
                {
                    oraConn.Open();
                }
                sSql = "SELECT CUST_SNO,USER_FULLNAME,CUST_SIGN FROM CUSTOMERSIGN,MASTER.GENUSERS,DEBITMANDATE WHERE CUST_SNO= DBMT_SNO AND CUST_CREATEUSERID = USER_ID AND CUST_SNO=" + value + " ";
                OracleDataAdapter oda = new OracleDataAdapter(sSql, oraConn);
                DataTable dt = new DataTable();
                oda.Fill(dt);
                gvDebitSign.DataSource = dt;    
                gvDebitSign.DataBind();
            }
        }
         catch (Exception ex)
        {
            objLog.WriteLog("debitViewFront Exception in PageLoad : " + ex.Message);
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