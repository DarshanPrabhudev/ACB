<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form1.aspx.cs" Inherits="Form1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Form1</title>   
    <link href="Styles/cwebsite.css" rel="stylesheet" type="text/css" />
    </head>
<body class="clsbody">
    <form id="form1" runat="server">
        <table class="clstable">
            <tr>
                <td class="clsrpthdr" colspan="2">Form 1 Report</td>
            </tr>
            <tr>
                <td class="clstd">Branch</td>
                <td class="clstd">
                    <asp:DropDownList ID="ddlBranch" runat="server" Width="136px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="clstd">As On Date</td>
                <td class="clstd">
<%--                    <asp:DropDownList ID="ddlDay" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlMonth" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="True">
                    </asp:DropDownList>--%>
                    <asp:TextBox ID="txtAsOnDate" runat="server" Width="70px"></asp:TextBox>
                    <asp:CalendarExtender ID="txtAsOnDate_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtAsOnDate" Format="dd/MM/yyyy">
                    </asp:CalendarExtender>
                    <asp:MaskedEditExtender ID="txtAsOnDate_MaskedEditExtender" runat="server" Mask="99/99/9999" MaskType="Date" Enabled="True" TargetControlID="txtAsOnDate" UserDateFormat="DayMonthYear">
                    </asp:MaskedEditExtender>
                </td>
            </tr>
            <tr>
                <td class="clstd">Officer</td>
                <td class="clstd">
                    <asp:TextBox ID="txtOfficer" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="clstd">Designation</td>
                <td class="clstd">
                    <asp:TextBox ID="txtDesignation" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="clstd" colspan="2" align="center">
                    <asp:Button ID="btnOk" runat="server" onclick="btnOk_Click" Text="Submit" Width="70px" />
                    <asp:Button ID="btnBack" runat="server" onclick="btnBack_Click" Text="Back" Width="70px" />
                    <asp:Button ID="btnClear" runat="server" Text="Clear" onclick="btnClear_Click" Width="70px" />    
                </td>
            </tr>
        </table>
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    </form>
</body>
</html>
