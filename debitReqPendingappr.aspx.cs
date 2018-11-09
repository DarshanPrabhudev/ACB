//**---------------------------ManDate Request Pending For Approval---------------------------
/*
 *   Version    Created By          Date           Assigned By
 *    1.0       Laxmikantha         10/08/2016     Venkatesh+LS 
 * -----------------------------------------------------------------------
 * -----------------------------PSEUDOCODE--------------------------------
 * */
using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OracleClient;

public partial class debitReqPendingappr : System.Web.UI.Page
{
    Log objLog = new Log();
    OracleConnection oraConn = new OracleConnection();
    protected void Page_Load(object sender, EventArgs e)
    {
        globalSession.checkSession();
        OracleConnection oraConn = new OracleConnection(Session["constring"].ToString());
        if (oraConn.State != ConnectionState.Open)
        {
            oraConn.Open();
        }
        try
        {
            if (!Page.IsPostBack)
            {            
                string sSql = "";
                string sSqll = "";   
                sSql = "select DBMT_SNO,DBMT_DMSGID,DBMT_DRACNO,DBMT_DRNAME,DBMT_AMOUNT from HEAD.DEBITMANDATE ORDER BY DBMT_SNO";
                DataTable dt = new DataTable();
                OracleDataAdapter oda = new OracleDataAdapter(sSql, oraConn);
                oda.Fill(dt);
                gvDebit.DataSource = null;
                gvDebit.DataSource = dt;
                gvDebit.DataBind();
                sSqll = "select * from HEAD.DEBITMANDATE ORDER BY DBMT_SNO";
                OracleDataAdapter odaa = new OracleDataAdapter(sSqll, oraConn);
                DataTable dta = new DataTable();
                odaa.Fill(dta);
                Session["dta"] = dta;
            }
        }
        catch (Exception ex)
        {
            objLog.WriteLog("debitReqPendingappr exception in Page Load: " + ex.Message);
            ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language = 'javascript'>alert('" + ex.Message + "')</script>");      
        }
        finally
        {
            if (oraConn.State == ConnectionState.Open)
                oraConn.Close();
            oraConn.Dispose();
            OracleConnection.ClearAllPools();
        }
    }    
    protected void gvDebit_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowid = 0;
        DataTable dtNew = new DataTable();
        if (e.CommandName == "Select")
        {
            if (Session["dta"] != null)
            {
                dtNew = (DataTable)Session["dta"];
            }
            rowid = int.Parse(e.CommandArgument.ToString());
            Session["row"] = rowid;
            Response.Redirect("deBitmandate.aspx");
        }
    }
    protected void btnBackMenu_Click(object sender, EventArgs e)
    {
        Response.Redirect("amenu.aspx");
    }
}