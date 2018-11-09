using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using DAL;

public partial class intcal : System.Web.UI.Page
{
    public string ABBR, No, asonDate, branch, strsql;
    DataTable dtint = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session.Contents.Count == 0)
        {
            Response.Redirect("ShowMessage.aspx?msg=" + GlobalConstants.msg);
        }
        else
        {
            OracleConnection con = new OracleConnection(Session["constring"].ToString());
            OracleCommand cmd = new OracleCommand("ODINT4MON1", con);
            OracleDataAdapter da = new OracleDataAdapter();
            DataSet ds = new DataSet();
            try
            {

                ABBR = Request.QueryString["abbr"].ToString();
                No = Request.QueryString["no"].ToString();
                asonDate = Request.QueryString["date"].ToString();
                branch = Request.QueryString["b"].ToString();
                //OracleConnection con = new OracleConnection(Session["constring"].ToString());
                //OracleCommand cmd = new OracleCommand("ODINT4MON1", con);
                //OracleDataAdapter da = new OracleDataAdapter();
                //DataSet ds = new DataSet();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("V_ABBR", OracleType.VarChar).Value = ABBR;
                cmd.Parameters.Add("V_ACC", OracleType.Number).Value = No;
                cmd.Parameters.Add("V_ASONDATE", OracleType.DateTime).Value = Convert.ToDateTime(asonDate);
                cmd.Parameters.Add("V_BRANCH", OracleType.Number).Value = Convert.ToInt16(branch);
                cmd.Parameters.Add("V_OUT", OracleType.Cursor).Direction = System.Data.ParameterDirection.Output;
                con.Open();
                OracleDataAdapter da1 = new OracleDataAdapter(cmd);
                da1.SelectCommand = cmd;
                da1.Fill(ds);
                da1.Fill(ds);
                dtint = ds.Tables[0];
                if (dtint.Rows.Count > 0)
                {
                    grdOdInterestDetails.DataSource = dtint;
                    grdOdInterestDetails.DataBind();
                }
            }
            catch (Exception ex)
            {
                Log log = new Log();
                log.WriteLog(ex.Message);

                ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert ('" + ex.Message + "') </script>");
            }

            finally
            {

                con.Close();
                con.Dispose();
                cmd.Dispose();
                da.Dispose();
                ds.Dispose();
                Log log = new Log();
                log.WriteLog("intcal: Finally block ends");

            }
        }
    }
}
