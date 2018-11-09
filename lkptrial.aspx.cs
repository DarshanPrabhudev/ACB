using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class lkptrial : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["buttonId"] != null)
        {
            Label1.Text = "Residential";
        }
        if (Session["ButtonOff"] != null)
        {
            Label1.Text = "office";
        }
        
    }
}