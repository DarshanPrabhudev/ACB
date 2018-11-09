using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using DAL;

public partial class loanGuarantor : System.Web.UI.Page
{
    string sSql, clsno, branch, LO, odSno;
    DAL.DataFetch objDataFetch = new DAL.DataFetch();
    Log objLog = new Log();
    DataTable dtGuarantor = new DataTable();
    DataTable dtDetails = new DataTable();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session.Contents.Count == 0)
        {
            Response.Redirect("ShowMessage.aspx?msg=" + GlobalConstants.msg);
        }
        else
        {
            OracleConnection oraConn = new OracleConnection(Session["constring"].ToString());
            int nCount, n = 0;

            try
            {
                clsno = Request.QueryString["sno"].ToString();
                branch = Request.QueryString["b"].ToString();
                LO = Request.QueryString["LO"].ToString();
                //odSno = Request.QueryString["ODENQSNO"].ToString();

                sSql = "Select CLMS_SNO,CLMS_DES,";
                sSql = sSql + "BRAN_NAME,";
                sSql = sSql + "MEMM_NO,MEMM_ABBR,MEMM_SNO,";
                sSql = sSql + " ADDR_ADDRESS1 || ADDR_ADDRESS2";
                sSql = sSql + " || ADDR_ADDRESS3  || ADDR_AREACODE ||";
                sSql = sSql + " ARMS_DES || ADDR_PINCODE Addr,";
                sSql = sSql + " ADDR_REMARKS,ADDR_MOBILENO || ADDR_TELPHNO Phn,";
                sSql = sSql + "DEPM_ABBR || DEPM_NO AS DEPM_NO";
                sSql = sSql + " FROM LOANGUARANTORS,";
                sSql = sSql + " CLIENTMASTER,";
                sSql = sSql + " MASTER.BRANCH,";
                sSql = sSql + " ADDRESS,";
                sSql = sSql + " MEMBERMASTER,";
                sSql = sSql + " AREAMASTER,";
                sSql = sSql + " DEPOSITMASTER";
                sSql = sSql + " WHERE CLMS_SNO = LGUA_CLIENT";
                sSql = sSql + " AND MEMM_CLIENT(+) = LGUA_CLIENT";
                sSql = sSql + " AND DEPM_CLIENT(+) = CLMS_SNO  AND DEPM_ABBR(+) = 'SB' AND CLMS_ADDRESS = ADDR_SNO";
                sSql = sSql + " AND ADDR_AREACODE = ARMS_SNO(+)";
                sSql = sSql + " AND LGUA_CANCELYN = 'N'";
                if (LO == "L")
                {
                    sSql = sSql + " AND LGUA_LOANLINK = " + clsno + "";
                }
                else
                {
                    sSql = sSql + " AND LGUA_ODLINK = " + clsno + "";
                    //sSql = sSql + " AND LGUA_ODRNLINK = " + odSno + "";
                }

                sSql = sSql + " and ARMS_branch(+)= " + branch + "";
                sSql = sSql + " and DEPM_branch(+)= " + branch + "";
                sSql = sSql + " and bran_sno= " + branch + "";
                sSql = sSql + " ORDER BY LGUA_ORDER";

                OracleDataAdapter oDaGuar = new OracleDataAdapter(sSql, oraConn);
                oDaGuar.Fill(dtGuarantor);

                if (dtGuarantor.Rows.Count > 0)
                {
                    dtDetails.Columns.Add(new DataColumn("Code", typeof(string)));
                    dtDetails.Columns.Add(new DataColumn("MemNo", typeof(string)));
                    dtDetails.Columns.Add(new DataColumn("Guarantor", typeof(string)));
                    dtDetails.Columns.Add(new DataColumn("Address", typeof(string)));
                    dtDetails.Columns.Add(new DataColumn("PhnNo", typeof(string)));

                    nCount = dtGuarantor.Rows.Count;
                    for (int i = 0; i < nCount; i++)
                    {
                        DataRow dr = dtDetails.NewRow();
                        dr["Code"] = dtGuarantor.Rows[n]["CLMS_SNO"].ToString();
                        dr["MemNo"] = dtGuarantor.Rows[n]["MEMM_NO"].ToString();
                        dr["Guarantor"] = dtGuarantor.Rows[n]["CLMS_DES"].ToString();
                        dr["Address"] = dtGuarantor.Rows[n]["Addr"].ToString();
                        dr["PhnNo"] = dtGuarantor.Rows[n]["Phn"].ToString();

                        dtDetails.Rows.Add(dr);
                        n = n + 1;
                    }
                    this.GvGuara.Visible = true;
                    GvGuara.DataSource = dtDetails;
                    GvGuara.DataBind();
                }
                else
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert ('No Guarantor Found') </script>");
                    return;
                }
            }

            catch (Exception ex)
            {
                objLog.WriteLog(ex.Message);
                ClientScript.RegisterStartupScript(Page.GetType(), "VALIDATION", "<script language='javascript'>alert ('" + ex.Message + "') </script>");
                return;
            }
            finally
            {
                oraConn.Close();
                oraConn.Dispose();
                objLog.WriteLog("Guarantor: Finally block ends");
            }
        }
    }
    protected void GvGuara_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void GvGuara_SelectedIndexChanged1(object sender, EventArgs e)
    {

    }
}
