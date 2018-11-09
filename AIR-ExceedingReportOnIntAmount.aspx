<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AIR-ExceedingReportOnIntAmount.aspx.cs" Inherits="Reports_AIR_ExceedingReportOnIntAmount" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 95%;
        }
        .style2
        {
            text-align: left;
        }
        .style5
        {
        	width:100%;
        	font-weight:bold;
        	text-align:center;
        }
        
        .style6
        {
            height: 31px;
        }
        
        .style7
        {
            text-align: left;
            height: 26px;
        }
        
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
    
        <table class="style5">
            <tr>
                <td>
                    AIR-EXCEEDING INTEREST AMOUNT REPORT FOR DEPOSIT</td>
            </tr>
        </table>
        <br />
        <br />
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
        <asp:Panel ID="Panel1" runat="server" BorderColor="Silver" BorderStyle="Solid" 
            BorderWidth="5px" Width="466px">
            <table class="style1">
                <tr>
                    <td colspan="2" align="center">
                        <asp:RadioButtonList ID="rblBranch" runat="server" Height="30px" 
                            RepeatDirection="Horizontal" Width="226px" AutoPostBack="True" 
                            onselectedindexchanged="rblBranch_SelectedIndexChanged">
                            <asp:ListItem Selected="True">Consolidated</asp:ListItem>
                            <asp:ListItem>Branch</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        Branch</td>
                    <td class="style2">
                        <asp:DropDownList ID="ddlBranch" runat="server" Width="188px" 
                            AutoPostBack="True" onselectedindexchanged="ddlBranch_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style7">
                        From Date</td>
                    <td class="style7" >
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
                       <asp:CalendarExtender ID="CalendarExtender1" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtToDt">
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
                    <td colspan="2" align="center" class="style6">
                        <asp:RadioButtonList ID="rblList" runat="server" Height="20px" 
                            RepeatDirection="Horizontal" Width="232px">
                            <asp:ListItem Selected="True">Members</asp:ListItem>
                            <asp:ListItem>All</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:RadioButton ID="rblSum" runat="server" Text="Summary" />
                        <asp:RadioButton ID="rblDetails" runat="server" Text="Details" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                    
                        <asp:Panel ID="pnlSchemeList" runat="server">
                            <div style="border-style: solid; border-width: 1px; height:190px; overflow:auto; width: 340px; margin-top: 0px; margin-bottom: 0px; background-color: #CAEBEE;">
                                <asp:CheckBoxList ID="chkListDeposit" runat="server" ForeColor="Black">
                                </asp:CheckBoxList>
                            </div>
                        </asp:Panel>
                        <br />
                        <asp:Button ID="btnOk" runat="server" Font-Bold="True" Text="OK" 
                            Width="100px" onclick="btnOk_Click" style="height: 26px" />
                        <asp:Button ID="btnBck" runat="server" Font-Bold="True" Text="BACK" 
                            Width="100px" Height="26px" onclick="btnBck_Click" />
                            
                   </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        &nbsp;</td>
                </tr>
            </table>
        </asp:Panel>
    
    </div>
    </form>
</body>
</html>
