<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form15HLogAccountWise.aspx.cs" Inherits="Form15HLogAccountWise" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title></title>
    <style type="text/css">
        .style1
        {
            width: 98%;
            height: 321px;
        }
        .style3
        {
        	text-align:center;
        }
        .style4
        {
            font-weight: bold;
            text-align: left;
        }
        .style5
        {
        	width:100%;
        	font-weight:bold;
        	text-align:center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
    
        <br />
        <table class="style5">
            <tr>
                <td>
                    FORM NO.15H AND G REPORT</td>
            </tr>
        </table>
        <br />
        <br />
        <br />
        <br />
        <br />
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
        <asp:Panel ID="Panel2" runat="server" Height="335px"  
            Width="336px" BorderColor="#666699" BorderStyle="Solid">
            <table class="style1">
                <tr>
                    <td class="style3" colspan="2" valign="middle">
                        <asp:RadioButtonList ID="rdbtnForm15" runat="server" 
                            RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True">Form15H</asp:ListItem>
                            <asp:ListItem>Form15G</asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:RadioButtonList ID="rdbtnAccWise" runat="server" 
                            RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True">ClientWise</asp:ListItem>
                            <asp:ListItem>AccountWise</asp:ListItem>
                        </asp:RadioButtonList>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td class="style4">
                        From date</td>
                    <td class="style4">
                        <asp:TextBox ID="txtFromdate" runat="server" Width="100px"></asp:TextBox>
                        <asp:CalendarExtender ID="txtFromDt_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFromdate"></asp:CalendarExtender>
                        <asp:MaskedEditExtender ID="txtFromDt_MaskedEditExtender" runat="server" 
                            AcceptNegative="Left" DisplayMoney="Left" Enabled="True" 
                            ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" 
                            TargetControlID="txtFromdate" UserDateFormat="DayMonthYear"></asp:MaskedEditExtender>
                    </td>
                </tr>
                <tr>
                    <td class="style4">
                        To Date</td>
                    <td class="style4">
                        <asp:TextBox ID="txtTodate" runat="server" Width="100px"></asp:TextBox>
                        <asp:CalendarExtender ID="txtTodate_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtTodate"></asp:CalendarExtender>
                        <asp:MaskedEditExtender ID="txtTodate_MaskedEditExtender" runat="server" 
                            AcceptNegative="Left" DisplayMoney="Left" Enabled="True" 
                            ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" 
                            TargetControlID="txtTodate" UserDateFormat="DayMonthYear"></asp:MaskedEditExtender>
                    </td>
                </tr>
                <tr>
                    <td class="style4">
                        Amount</td>
                    <td class="style4">
                        <asp:TextBox ID="txtAmount" runat="server" Width="100px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style4">
                        Client Code</td>
                    <td class="style4">
                        <asp:TextBox ID="txtCCode" runat="server" AutoPostBack="True" 
                            ontextchanged="txtCCode_TextChanged" Width="100px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style4" colspan="2">
                        <asp:Label ID="lblName" runat="server" Font-Size="Smaller"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style4">
                        <asp:Label ID="lblAccNo" runat="server" Text="Account No"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtAbbr" runat="server" Width="30px"></asp:TextBox>
                        <asp:TextBox ID="txtAccNo" runat="server" 
                            Width="100px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style3">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center">
                        <asp:Button ID="btnOk" runat="server" Text="OK" Width="43px" 
                            onclick="btnOk_Click" />
                        <asp:Button ID="btnClear" runat="server" Text="Clear" 
                            onclick="btnClear_Click" />
                        <asp:Button ID="btnClear0" runat="server" onclick="btnBack_Click" Text="Back" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
    
    </div>
    </form>
</body>
</html>
