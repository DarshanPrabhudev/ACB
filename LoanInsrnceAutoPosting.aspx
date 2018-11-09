<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LoanInsrnceAutoPosting.aspx.cs" Inherits="LoanInsrnceAutoPosting" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Report</title>     
    
    <link href="Styles/cwebsite.css" rel="stylesheet" type="text/css" />
    
</head>
<body class="clsbody">
    <form id="form1" runat="server">
    <div style="height: 646px">
    
        <table class="clstable">
            <tr>
                <td  class="clsrpthdr" colspan="2" align="center">
                    &nbsp;&nbsp;
                    OD Insurance Auto Posting</td>
            </tr>
            <tr>
                <td class="clstd" colspan="2" >
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="clstd" >
                    Branch</td>
                <td class="clstd">
                    <asp:DropDownList ID="ddlBranch" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlBranch_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
           <tr>
                <td class="clstd" >
                    As on Date:</td>
                <td class="clstd">
                    <asp:DropDownList ID="ddFromDay" runat="server">
                    </asp:DropDownList>
              
                
                    <asp:DropDownList ID="ddFromMonth" runat="server">
                    </asp:DropDownList>
               
                
                    <asp:DropDownList ID="ddFromYear" runat="server">
                    </asp:DropDownList>
                   
                   </td>
            </tr>
           <tr>
                <td class="clstd" >
                    Posting Date:</td>
                <td class="clstd">
                    <asp:DropDownList ID="ddlPostingDay" runat="server">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlPostingMonth" runat="server">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlPostingYear" runat="server">
                    </asp:DropDownList>
                   
                   </td>
            </tr>
           <tr>
                <td class="clstd" >
                    &nbsp;</td>
                <td class="clstd">
                    <asp:RadioButtonList ID="rblTypes" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="rblTypes_SelectedIndexChanged" 
                        RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="LOAN">Loan</asp:ListItem>
                        <asp:ListItem>OD</asp:ListItem>
                    </asp:RadioButtonList>
                   
                   </td>
            </tr>
           <tr>
                <td class="clstd" colspan="2" >
                    <asp:Panel ID="PanelOD" runat="server">
                        <div style="border-style: solid; border-width: 1px; width: 259px; height: 52px;">
                            <asp:CheckBoxList ID="chkListOd" runat="server" ForeColor="Black">
                            </asp:CheckBoxList>
                        </div>
                    </asp:Panel>
                </td>
            </tr>
           <tr>
                <td class="clstd" colspan="2" >
                    <asp:Panel ID="PanelLoan" runat="server">
                        <div style="border-style: solid; border-width: 1px; height:229px; overflow:auto; width: 259px;">
                            <asp:CheckBoxList ID="chkList" runat="server" ForeColor="Black">
                            </asp:CheckBoxList>
                        </div>
                    </asp:Panel>
                </td>
            </tr>
           <tr>
                <td class="clstd" colspan="2" >
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="clstd" colspan="2" >
                    <asp:Button ID="btnSelect" runat="server" onclick="btnSelect_Click" 
                        Text="Select All" Width="70px" />
                    <asp:Button ID="btnClear" runat="server" onclick="btnClear_Click" Text="Clear" 
                        Width="70px" />
                    <asp:Button ID="btnOk" runat="server" Text="Ok" Width="70px" 
                        onclick="btnOk_Click" />
                    <asp:Button ID="btnBack" runat="server" Text="Back" Width="70px" 
                        onclick="btnBack_Click" />
                </td>
            </tr>
            <tr>
                <td class="clstd" colspan="2">
                    &nbsp;
                    </td>
            </tr>
        </table>
    
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
