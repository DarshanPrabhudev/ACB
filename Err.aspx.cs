using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Appl_Transaction_Err : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void lnkRedirect_Click(object sender, EventArgs e)
    {
        Response.Redirect("Login.aspx");
    }
}