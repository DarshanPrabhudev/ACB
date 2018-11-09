<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FormC.aspx.cs" Inherits="FormC" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title> 
    <link href="Styles/cwebsite.css" rel="stylesheet" type="text/css" />  
</head>
<body>
    <form id="form1" runat="server">    
    <div>
        <table class="clstable">
            <tr>
                <td colspan="2" class="clsrpthdr" align="center">Form-C Details</td>
            </tr>
            <tr>
                <td class="clstd"></td>
                <td class="clstd"></td>
            </tr>
            <tr>
                <td class="clstd">Branch</td>
                <td class="clstd">
                    <asp:DropDownList ID="ddlBranch" runat="server" AutoPostBack="True" Width="136px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="clstd">From-Date</td>
                <td class="clstd">
                    <asp:TextBox ID="txtfrom" runat="server" Width="70px"></asp:TextBox>
                    <asp:CalendarExtender ID="txtfrom_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtfrom" Format="dd/MM/yyyy">
                    </asp:CalendarExtender>
                    <asp:MaskedEditExtender ID="txtfrom_MaskedEditExtender" runat="server" Mask="99/99/9999" MaskType="Date" Enabled="True" TargetControlID="txtfrom" UserDateFormat="DayMonthYear">
                    </asp:MaskedEditExtender>
                </td>
            </tr>
            <tr>
                <td class="clstd">To-Date </td>
                <td class="clstd">
                    <asp:TextBox ID="txtto" runat="server" Width="70px"></asp:TextBox>
                    <asp:CalendarExtender ID="txtto_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtto" Format="dd/MM/yyyy">
                    </asp:CalendarExtender>
                    <asp:MaskedEditExtender ID="txtto_MaskedEditExtender" runat="server" Mask="99/99/9999" MaskType="Date" Enabled="True" TargetControlID="txtto" UserDateFormat="DayMonthYear">
                    </asp:MaskedEditExtender>
                </td>
            </tr>
            <tr>
                <td class="clstd" colspan="2"></td>
            </tr>
            <tr>
                <td class="clstd" colspan="2" align="center">
                    <asp:Button ID="btnOK" runat="server" OnClick="btnOK_Click" Text="Submit" Width="70px" />
                    <asp:Button ID="btnClear" runat="server" Text="Clear" onclick="btnClear_Click" Width="70px" />
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
