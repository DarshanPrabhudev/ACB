<%@ Page Language="C#" AutoEventWireup="true" Inherits="intcal" CodeFile ="intcal.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <style type="text/css">

        .style10
        {
            color: #0099CC;
            font-size: xx-large;
            text-decoration: underline;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    &nbsp;<span class="style10">INT.CALCULATION<br />
        <br />
        </span>
        <asp:GridView ID="grdOdInterestDetails" runat="server" BackColor="White" 
            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <RowStyle ForeColor="#000066" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
        <br />
        <br />
        <INPUT type="button" value="Back" 
            onclick="window.history.back(); return true;" 
            style="font-weight: 700; color: #003399"></div>
    </form>
</body>
</html>
