<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CRRSLRbtnTwoGivenDates.aspx.cs" Inherits="CRRSLRbtnTwoGivenDates" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
    <title>CRR/SLR Between Two Given Dates</title>
    <link href="Styles/cwebsite.css" rel="stylesheet" />    
</head>
<body class="clsbody">
    <form id="form1" runat="server">
    <table class="clstable">
        <tr>
            <td colspan="2" style="text-align: center" class="clsrpthdr">
                CRR/SLR Between Two Given Dates</td>
        </tr>
        <tr>
            <td colspan="2" class="clstd">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="clstd">
                    Branch</td>
            <td class="clstd">
                    <asp:DropDownList ID="ddlBranch" runat="server" 
                       >
</asp:DropDownList>
                </td>
        </tr>
        <tr>
            <td class="clstd">
                    From Date</td>
            <td class="clstd">
                    <asp:TextBox ID="txtFrom" runat="server" Width="70px"></asp:TextBox>
                    <asp:CalendarExtender ID="txtFrom_CalendarExtender" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFrom">
                    </asp:CalendarExtender>
                    <asp:MaskedEditExtender ID="txtFrom_MaskedEditExtender" runat="server" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtFrom" UserDateFormat="DayMonthYear">
                    </asp:MaskedEditExtender>
                </td>
        </tr>
        <tr>
            <td class="clstd">
                    To Date</td>
            <td class="clstd">
                    <asp:TextBox ID="txtTo" runat="server" Width="70px"></asp:TextBox>
                    <asp:CalendarExtender ID="txtTo_CalendarExtender" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtTo"></asp:CalendarExtender>
                    <asp:MaskedEditExtender ID="txtTo_MaskedEditExtender" runat="server" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtTo" UserDateFormat="DayMonthYear"></asp:MaskedEditExtender>
                </td>
        </tr>
        <tr>
            <td class="clstd">
                    &nbsp;</td>
            <td class="clstd">
<asp:Button ID="btnSubmit" runat="server" Text="OK" onclick="btnSubmit_Click" style="height: 26px" 
                        Width="70px" />
<asp:Button ID="btnBack" runat="server" onclick="btnBack_Click" Text="Back" Width="70px" />
<asp:Button ID="btnClear" runat="server" onclick="btnClear_Click" Text="Clear" Width="70px" />
                </td>
        </tr>
        </table>
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
    </form>
</body>
</html>
