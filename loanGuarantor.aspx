<%@ Page Language="C#" AutoEventWireup="true" Inherits="loanGuarantor" CodeFile="loanGuarantor.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <style type="text/css">
        .style10
        {
            color: #000000;
            font-size: x-large;
                font-weight: normal;
        }
        .style11
        {
            color: #0099CC;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="background-color: #FFFFFF; font-weight: 700;">
    
    &nbsp;<span class="style10">Guarantor</span><br class="style11" />
        <br />
        <asp:GridView ID="GvGuara" runat="server" CellPadding="4" onselectedindexchanged="GvGuara_SelectedIndexChanged1" 
            Width="566px" onrowcommand="GvGuara_RowCommand" ForeColor="#333333" 
            GridLines="None">
            <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
            <RowStyle BackColor="#EFF3FB" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
        <br />
        <INPUT type="button" value="Back" 
            onclick="window.history.back(); return true;" 
            style="font-weight: 700; color: #003399">
        <br />
        <br />
        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    
    </div>
    </form>
</body>
</html>
