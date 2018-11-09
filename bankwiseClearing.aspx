<%@ Page Language="C#" AutoEventWireup="true" CodeFile="bankwiseClearing.aspx.cs" Inherits="bankwiseClearing" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Bankwise Clearing Report</title>
    <link href="Styles/cwebsite.css" rel="stylesheet" type="text/css" />   
</head>
<body class="clsbody">
    <form id="form1" runat="server">
    <div>    
        <table class="clstable">
            <tr>
                <td class="clsrpthdr" colspan="2">BankWise Clearing</td>
            </tr>
            <tr>
                <td class="clstd"></td>
                <td class="clstd"></td>
            </tr>
            <tr>
                <td class="clstd">Branch</td>
                <td class="clstd">
                    <asp:DropDownList ID="ddlBranch" runat="server" Width="136px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="clstd">From Date</td>
                <td class="clstd">
                    <asp:TextBox ID="txtfrom" runat="server" Width="70px"></asp:TextBox>
                    <asp:CalendarExtender ID="txtfrom_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtfrom" Format="dd/MM/yyyy">
                    </asp:CalendarExtender>
                    <asp:MaskedEditExtender ID="txtfrom_MaskedEditExtender" runat="server" Mask="99/99/9999" MaskType="Date" Enabled="True" TargetControlID="txtfrom" UserDateFormat="DayMonthYear">
                    </asp:MaskedEditExtender>
                    <%--<asp:DropDownList ID="ddfrmday" runat="server">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddfrmMonth" runat="server">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddfrmYear" runat="server">
                    </asp:DropDownList>--%>
                </td>
            </tr>
            <tr>
                <td class="clstd">To Date</td>
                <td class="clstd">
                    <asp:TextBox ID="txtto" runat="server" Width="70px"></asp:TextBox>
                    <asp:CalendarExtender ID="txtto_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtto" Format="dd/MM/yyyy">
                    </asp:CalendarExtender>
                    <asp:MaskedEditExtender ID="txtto_MaskedEditExtender" runat="server" Mask="99/99/9999" MaskType="Date" Enabled="True" TargetControlID="txtto" UserDateFormat="DayMonthYear">
                    </asp:MaskedEditExtender>
                    <%--<asp:DropDownList ID="ddtoday" runat="server">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddtoMonth" runat="server">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddtoYear" runat="server">
                    </asp:DropDownList>--%>
                </td>
            </tr>
            <tr>
                <td class="clstd">Select I/O</td>
                <td class="clstd">
                    <asp:CheckBox ID="ckhio" runat="server" AutoPostBack="True" 
                        oncheckedchanged="ckhio_CheckedChanged" />
                    <asp:Label ID="lblio" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="clstd"></td>
                <td class="clstd"></td>
            </tr>
            <tr>
                <td class="clstd" colspan="2" align="center">
                    <asp:Button ID="btnok" runat="server" onclick="btnok_Click" Text="Submit" Width="70px" />
                    <asp:Button ID="btnClear" runat="server" Text="Clear" Width="70px" />
                    <asp:Button ID="btnBack" runat="server" onclick="btnBack_Click" Text="Back" Width="70px"/>
                </td>
            </tr>
        </table>
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    </div>
    </form>
</body>
</html>
