<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AIR-ExceedingReportForDeposit.aspx.cs" Inherits="Reports_AIR_ExceedingReportForDeposit" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            text-align: left;
        }
                
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
    
        <table class="style1" >
            <tr>
                <td style="font-weight: 700" align="center">
                    AIR-INTEREST EXCEEDING REPORT FOR DEPOSIT</td>
            </tr>
        </table>
    
        <br />
          <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
        <asp:Panel ID="Panel1" runat="server" BorderColor="Silver" BorderStyle="Solid" 
            BorderWidth="5px" Width="466px">
            <table class="style1" align="center">
                <tr>
                    <td class="style2" colspan="2">
                        <asp:RadioButtonList ID="rblBranch" runat="server" AutoPostBack="True" 
                            Height="30px"
                            RepeatDirection="Horizontal" Width="226px">
                            <asp:ListItem Selected="True">Consolidated</asp:ListItem>
                            <asp:ListItem>Branch</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        Branch</td>
                    <td class="style2" >
                        <asp:DropDownList ID="ddlBranch" runat="server" Width="188px" 
                            onselectedindexchanged="ddlBranch_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        From Date</td>
                    <td class="style2">
                        <asp:TextBox ID="txtFrmDt" runat="server" Width="130px"></asp:TextBox>
                        <%-- <asp:CalendarExtender ID="txtFrmDt_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFrmDt">
                        </asp:CalendarExtender>
                        <asp:MaskedEditExtender ID="txtFrmDt_MaskedEditExtender" runat="server" 
                            AcceptNegative="Left" DisplayMoney="Left" Enabled="True" 
                            ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" 
                            TargetControlID="txtFrmDt" UserDateFormat="DayMonthYear">
                        </asp:MaskedEditExtender>--%>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        To Date</td>
                    <td class="style2">
                        <asp:TextBox ID="txtToDt" runat="server" Width="130px"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" 
                            Format="dd/MM/yyyy" TargetControlID="txtToDt">
                        </asp:CalendarExtender>
                        <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" 
                            AcceptNegative="Left" DisplayMoney="Left" Enabled="True" 
                            ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" 
                            TargetControlID="txtToDt" UserDateFormat="DayMonthYear">
                        </asp:MaskedEditExtender>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                    
                        Amount &gt;&gt;</td>
                    <td class="style2">
                        <asp:TextBox ID="txtAmnt" runat="server" Width="130px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="2">
                        <asp:RadioButtonList ID="rblList" runat="server" Height="20px" 
                            RepeatDirection="Horizontal" Width="232px">
                            <asp:ListItem Selected="True">Yearly</asp:ListItem>
                            <asp:ListItem>Dailywise</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <asp:Panel ID="pnlSchemeList" runat="server">
                            <div style="border-style: solid; border-width: 1px; height:190px; overflow:auto; width: 340px; margin-top: 0px; margin-bottom: 0px; background-color: #CAEBEE;">
                                <asp:CheckBoxList ID="chkListDeposit" runat="server" ForeColor="Black">
                                </asp:CheckBoxList>
                            </div>
                        </asp:Panel>
                        <br />
                        <asp:Button ID="btnOk" runat="server" Font-Bold="True" onclick="btnOk_Click" 
                            style="height: 26px" Text="OK" Width="100px" />
                        <asp:Button ID="btnBck" runat="server" Font-Bold="True" Height="26px" 
                            Text="BACK" Width="100px" onclick="btnBck0_Click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    
        <br />
        <br />
        <br />
    
    </div>
    </form>
</body>
</html>
