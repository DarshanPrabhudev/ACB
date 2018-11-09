<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Err.aspx.cs" Inherits="Appl_Transaction_Err" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            text-align: left;
            font-size: medium;
        }
    </style>
</head>
<body>
   &nbsp;<form id="form1" runat="server">
    <div align="center">
        <asp:Label ID="Label1" runat="server" BackColor="#3366FF" CssClass="style1" 
            Font-Bold="True" Font-Size="X-Large" ForeColor="White" Text="Time Out..."></asp:Label>
        <br />
        <br />
        <br />
        <br />
        <br />
        <img alt="" src="img/gworldclock.png" style="width: 298px; height: 253px; margin-left: 0px; margin-top: 10px;" 
            align="center" />
        <br />
        <br />
<br />
       
        <br />
        <asp:LinkButton ID="lnkRedirect" runat="server" onclick="lnkRedirect_Click">Re Login</asp:LinkButton>
       
        <br />
    </div>
    </form>
</body>
</html>
