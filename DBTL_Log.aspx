<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DBTL_Log.aspx.cs" Inherits="DBTL_Log" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>DBTL & APBS & ECS Transaction Report</title>
      <link href="Styles/WebAppl.css" rel="stylesheet" type="text/css" />
   
</head>
<body class=clsbody >
  <form id="form2" runat="server">
   <div align="center" style="height: 408px">
<asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server">
        </asp:ToolkitScriptManager>
    <table align="center" style="border: medium solid #808080; width: 585px;">
        <tr>
            <td class="clsHeadingTd" colspan="2" align="center">
                <strong>DBTL &amp; APBS &amp; ECS Transaction Report</strong></td>
        </tr>
        <tr>
            <td align="center" class="clstd">
                &nbsp;</td>
            <td align="center" class="clstd">
                &nbsp;</td>
        </tr>
        <tr>
            <td align="center" class="clstdCenter">
                From Date</td>
            <td align="center" class="clstd">
                <asp:TextBox ID="txtFromDate" runat="server" TabIndex="1"></asp:TextBox>
                  <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" 
                                            Format="dd/MM/yyyy" PopupPosition="BottomRight" 
                                            TargetControlID="txtFromDate" TodaysDateFormat="DD/MM/YYYY">
                                        </asp:CalendarExtender>
                                        <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Enabled="True" 
                                            Mask="99/99/9999" MaskType="Date" TargetControlID="txtFromDate">
                                        </asp:MaskedEditExtender>
            </td>
        </tr>
        <tr>
            <td align="center" class="clstdCenter">
                To Date</td>
            <td align="center" class="clstd">
                <asp:TextBox ID="txtToDate" runat="server" TabIndex="2"></asp:TextBox>
                  <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" 
                                            Format="dd/MM/yyyy" PopupPosition="BottomRight" 
                                            TargetControlID="txtToDate" TodaysDateFormat="DD/MM/YYYY">
                                        </asp:CalendarExtender>
                                        <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Enabled="True" 
                                            Mask="99/99/9999" MaskType="Date" TargetControlID="txtToDate">
                                        </asp:MaskedEditExtender>
            </td>
        </tr>
        <tr>
            <td align="center" class="clstdCenter">
                Log Type</td>
            <td align="left">
                <asp:RadioButtonList ID="rblSelection" runat="server" Height="146px">
                    <asp:ListItem Value="ACH">ACH(ACH Credit)</asp:ListItem>
                    <asp:ListItem Value="ACHDR">ACHDR(ACH Debit)</asp:ListItem>
                    <asp:ListItem Value="APB">APB(APB Credit)</asp:ListItem>
                    <asp:ListItem Value="ECS-DR">ECS-DR(ECS Debit)</asp:ListItem>
                    <asp:ListItem Value="ECS-CR">ECS-CR(ECS Credit)</asp:ListItem>
                    <asp:ListItem Value="AV">AV(Account Validation)</asp:ListItem>
                    <asp:ListItem Selected="True">All</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td align="center" class="clstdCenter">
                &nbsp;</td>
            <td align="center" class="clstd">
                &nbsp;</td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Button ID="btnOk" runat="server" CssClass="clsCornerBtn" Text="OK" 
                    Width="100px" onclick="btnOk_Click" TabIndex="3" />
                <asp:Button ID="btnBack" runat="server" CssClass="clsCornerBtn" Text="Back" 
                    Width="100px" onclick="btnBack_Click" TabIndex="4" />
            </td>
        </tr>
    </table>

   </div>
       
    </form>
</body>
</html>
