using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class hortrsinglexml : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        visibleToggle(tvMaster);
    }
    protected void visibleToggle(TreeView tv)
    {
        tvMaster.Visible = false;
        tvApps.Visible = false;
        tvEnquiry.Visible = false;
        tvReports.Visible = false;
        tv.Visible = true;

    }
    protected void menuTop_MenuItemClick(object sender, MenuEventArgs e)
    {
         
        //menuTop.SelectedItem.ToString().ToUpper().Trim();

    }
    protected void xdsMaster_Transforming(object sender, EventArgs e)
    {

    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {

        string MenuChoice = "";
        // RadioButtonList1.SelectedValue.ToString().Trim();
        switch (MenuChoice)
        {
            case "Masters":
                visibleToggle(tvMaster);
                break;
            case "Apps":
                visibleToggle(tvApps);
                break;
            case "Enquiry":
                visibleToggle(tvEnquiry);
                break;
            case "Reports":
                visibleToggle(tvReports);
                break;
            default:
                
                break;
        }
    }
    protected void optMaster_CheckedChanged(object sender, EventArgs e)
    {
        visibleToggle(tvMaster);
    }
    protected void optApps_CheckedChanged(object sender, EventArgs e)
    {
        visibleToggle(tvApps);
    }
    protected void optEnquiry_CheckedChanged(object sender, EventArgs e)
    {
        visibleToggle(tvEnquiry);
    }
    protected void optReports_CheckedChanged(object sender, EventArgs e)
    {
        visibleToggle(tvReports);
    }
}
