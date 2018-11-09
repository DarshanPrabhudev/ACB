<%@ Page Language="C#" AutoEventWireup="true" CodeFile="branchIntrstCalcn.aspx.cs" Inherits="branchIntrstCalcn" %>

<%@ Register src="ascx/iisplHdr.ascx" tagname="iisplHdr" tagprefix="uc3" %>
<%@ Register src="ascx/iisplFooter.ascx" tagname="iisplFooter" tagprefix="uc4" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Branch Interest Calculation</title>   
    <link href="Styles/cwebsite.css" rel="stylesheet" type="text/css" />
</head>
<body class="clsbody">
    <form id="form1" runat="server">
                    <uc3:iisplHdr ID="iisplHdr1" runat="server" />
    
        <table class="clstable">
            <tr>
                <td class="clsrpthdr" colspan="2">
    
                    Branch Interest Calculation</td>
            </tr>
            <tr>
                <td class="clstd">
                    &nbsp;</td>
                <td class="clstd">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="clstd">
                    Branch</td>
                <td class="clstd">
                    <asp:DropDownList ID="ddlBranch" runat="server" style="margin-left: 0px">
</asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="clstd">
                    From Date</td>
                <td class="clstd">
                    <asp:DropDownList ID="ddlFromDay" runat="server" AutoPostBack="True">
</asp:DropDownList>


<asp:DropDownList ID="ddlFromMonth" runat="server" AutoPostBack="True">
</asp:DropDownList>


<asp:DropDownList ID="ddlFromYear" runat="server" AutoPostBack="True">
</asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="clstd">
                    To Date</td>
                <td class="clstd">
                    <asp:DropDownList ID="ddlToDay" runat="server">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlToMonth" runat="server">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlToYear" runat="server">
                    </asp:DropDownList>
                                </td>
            </tr>
            <tr>
                <td class="clstd">
                    Credit Int. Rate</td>
                <td class="clstd">
                    <asp:TextBox ID="txtCredit" runat="server"></asp:TextBox>
                </td>
            </tr>
                            <tr>
                <td class="clstd">
                    Debit Int. Rate</td>
                <td class="clstd">
                    <asp:TextBox ID="txtDebit" runat="server"></asp:TextBox>
                </td>
                            </tr>
                            <tr>
                <td class="clstd">
                    &nbsp;</td>
                <td class="clstd">
                    <asp:Button 
                        ID="btnOk" runat="server" 
                        onclick="btnOk_Click" Text="Ok" Width="50px" />
                    <asp:Button ID="btnBack" runat="server" onclick="btnBack_Click" Text="Back" 
                        Width="50px" />
                </td>
                            </tr>
            </table>
    <br />
                        <uc4:iisplFooter ID="iisplFooter1" runat="server" />
    </form>
    <p>
        &nbsp;</p>
</body>
</html>
