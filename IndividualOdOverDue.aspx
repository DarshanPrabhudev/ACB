<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IndividualOdOverDue.aspx.cs" Inherits="IndividualOdOverDue" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Individual OD Overdue</title>
    <link href="Styles/cwebsite.css" rel="stylesheet" type="text/css" />
    
</head>
<body class="clsbody">
    <form id="form1" runat="server">
    <div>
        <table class="clstable">
            <tr>
                <td class="clsrpthdr" colspan="2">Individual OdOverdue</td>
            </tr>
            <tr>
                <td class="clstd" colspan="2" >
                    </td>
            </tr>
            <tr>
                <td class="clstd">Branch</td>
                <td class="clstd">
                    <asp:DropDownList ID="ddlBranch" runat="server" AutoPostBack="True" 
                        Width="136px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="clstd">As on Date</td>
                <td class="clstd">
                    <asp:TextBox ID="txtAsOnDate" runat="server" Width="70px"></asp:TextBox>
                    <asp:CalendarExtender ID="txtAsOnDate_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtAsOnDate" Format="dd/MM/yyyy">
                    </asp:CalendarExtender>
                    <asp:MaskedEditExtender ID="txtAsOnDate_MaskedEditExtender" runat="server" Mask="99/99/9999" MaskType="Date" Enabled="True" TargetControlID="txtAsOnDate" UserDateFormat="DayMonthYear">
                    </asp:MaskedEditExtender>
                </td>
            </tr>
            <tr>
                <td class="clstd" colspan="2">
                    <asp:RadioButtonList ID="rdbType" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="Format1">Format1</asp:ListItem>
                        <asp:ListItem Selected="True" Value="Format2">Format2</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td class="clstd" colspan="2" >
                    <asp:RadioButtonList ID="rdbSummary" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem>Summary</asp:ListItem>
                        <asp:ListItem Selected="True">Detail</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td class="clstd"  colspan="2">
                    <div style="border:1px groove #333; width:235px" >
                        <asp:CheckBoxList ID="chkList" runat="server" ForeColor="Black">
                        </asp:CheckBoxList>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="clstd" colspan="2" >
                    </td>
            </tr>
            <tr>
                <td class="clstd" colspan="2" align="center">
                    <asp:Button ID="btnOk" runat="server" Text="Submit" onclick="btnOk_Click" Width="70px" />
                    <asp:Button ID="btnClear" runat="server" Text="Clear" Width="70px" />
                    <asp:Button ID="btnBack" runat="server" onclick="btnBack_Click" Text="Back" Width="70px" />
                </td>
            </tr>
        </table>
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>    
    </div>
    </form>
</body>
</html>
