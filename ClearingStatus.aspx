<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ClearingStatus.aspx.cs" Inherits="ClearingStatus" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Clearing status</title>
    <link href="Styles/cwebsite.css" rel="stylesheet" />
</head>
<body class="clsbody">
    <form id="form1" runat="server">
    <div>
        <table class="clstable">
            <tr>
                <td colspan="2" class="clsrpthdr">Clearing Status</td>
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
                <td class="clstd">From Date</td>
                <td class="clstd">
                    <asp:TextBox ID="txtFrom" runat="server" Width="70px"></asp:TextBox>
                         <asp:CalendarExtender ID="txtFrom_CalendarExtender" runat="server" 
                            Enabled="True" TargetControlID="txtFrom" Format="dd/MM/yyyy">
                        </asp:CalendarExtender>
                        <asp:MaskedEditExtender ID="txtFrom_MaskedEditExtender" runat="server" 
                        Mask="99/99/9999" MaskType="Date"
                             Enabled="True" 
                            TargetControlID="txtFrom" UserDateFormat="DayMonthYear"></asp:MaskedEditExtender>
                </td>
            </tr>
            <tr>
                <td class="clstd">To Date</td>
                <td class="clstd">
                    <asp:TextBox ID="txtTo" runat="server" Width="70px"></asp:TextBox>
                        <asp:CalendarExtender ID="txtTo_CalendarExtender" runat="server" Enabled="True" 
                            TargetControlID="txtTo" Format="dd/MM/yyyy">
                        </asp:CalendarExtender>
                        <asp:MaskedEditExtender ID="txtTo_MaskedEditExtender" runat="server" 
                            Enabled="True" 
                             Mask="99/99/9999" MaskType="Date"
                            TargetControlID="txtTo" UserDateFormat="DayMonthYear">
                        </asp:MaskedEditExtender>
                </td>
            </tr>
            <tr>
                <td class="clstd">&nbsp;</td>
                <td class="clstd">&nbsp;</td>
            </tr>
            <tr>
                <td class="clstd" colspan="2" align="center">
                    <asp:Button ID="btnOK" runat="server" OnClick="btnOK_Click" Text="Submit" Width="70px" />                    
                    <asp:Button ID="btnClear" runat="server" Text="Clear" Width="70px" OnClick="btnClear_Click" />
                    <asp:Button ID="btnBack" runat="server" onclick="btnBack_Click" Text="Back" Width="70px" />
                </td>
            </tr>
        </table>

        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
        <%--<table class="style1">
            <tr>
                <td bgcolor="#66CCFF" style="color: #000000; font-weight: bold;" class="style2">
                    Clearing Status
                </td>
            </tr>
            <tr>
                <td>
                    
                </td>
            </tr>
            <tr>
                <td>
                    Branch:-</td>
            </tr>
            <tr>
                <td>
                    </td>
            </tr>
            <tr>
                <td>
                    From-Date:-<asp:DropDownList ID="ddfrmday" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
              
                
                    <asp:DropDownList ID="ddfrmMonth" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
               
                
                    <asp:DropDownList ID="ddfrmYear" runat="server" AutoPostBack="True">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td>
                    </td>
            </tr>
            <tr>
                <td>
                    To-Date:- 
<asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
              
                
                    <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
               
                
                    <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnOK" runat="server" OnClick="btnOK_Click" Text="Submit" Width="70px" />                    
                    <asp:Button ID="btnBack" runat="server" onclick="btnBack_Click" Text="Back" Width="70px" />
                </td>
            </tr>
        </table>--%>
    </div>
    </form>
</body>
</html>
