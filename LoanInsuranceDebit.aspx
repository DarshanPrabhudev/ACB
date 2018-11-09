<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LoanInsuranceDebit.aspx.cs" Inherits="LoanInsuranceDebit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Report</title>
    <link href="cwebsite.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style4
        {
            font-family: arial, Helvetica, sans-serif;
            font-size: 11px;
            font-weight: normal;
            font-style: normal;
            text-align : left;
            width: 104px;
        }
    </style>
</head>
<body class="clsbody">
    <form id="form1" runat="server">
    <div>
    
        <table class="clstable">
            <tr>
                <td  class="clspagehdr" colspan="2" align="center">
                    &nbsp;&nbsp;&nbsp;Loan Insurance Debit</td>
            </tr>
            <tr>
                <td class="style4" >
                    Branch</td>
                <td class="clstd">
                    <asp:DropDownList ID="ddlBranch" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlBranch_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style4" >
                    &nbsp;</td>
                <td class="clstd">
                    <asp:RadioButtonList ID="rblTypes" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="rblTypes_SelectedIndexChanged" 
                        RepeatDirection="Horizontal">
                        <asp:ListItem Value="LOAN" Selected="True">Loan</asp:ListItem>
                        <asp:ListItem Value="OD">OD</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td class="style4" >
                    Scheme</td>
                <td class="clstd">
                    <asp:DropDownList ID="ddlScheme" runat="server" 
                        onselectedindexchanged="ddlScheme_SelectedIndexChanged" Width="180px" 
                        AutoPostBack="True">
                    </asp:DropDownList>
                </td>
            </tr>
           <tr>
                <td class="style4" >
                    As on Date:</td>
                <td class="clstd" class="style18">
                    <asp:DropDownList ID="ddFromDay" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
              
                
                    <asp:DropDownList ID="ddFromMonth" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
               
                
                    <asp:DropDownList ID="ddFromYear" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                   
                   </td>
            </tr>
           <tr>
                <td class="style4" >
                    Voucher Date:</td>
                <td class="clstd" class="style18">
                    <asp:DropDownList ID="ddlVchrDay" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlVchrMonth" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlVchrYear" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                   
                   </td>
            </tr>
            <tr>
                <td class="style4" >
                    Insurance GL</td>
                <td class="clstd">
                    <asp:TextBox ID="txtInsurance" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style4" >
                    Amount</td>
                <td class="clstd">
                    <asp:TextBox ID="txtAmount" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="clstd" colspan="2">
                    <asp:Button ID="btnOk" runat="server" onclick="btnOk_Click" Text="Ok" 
                        Width="50px" ForeColor="#3333CC" />
                    &nbsp;
                    <asp:Button ID="btnBack" runat="server" onclick="btnBack_Click" Text="Back" 
                        ForeColor="#3333CC" />
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
