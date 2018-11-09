<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LoanOverDue.aspx.cs" Inherits="LoanOverDue" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Loan Over Due</title>    
    <link href="Styles/cwebsite.css" rel="stylesheet" type="text/css" />
    </head>
<body class="clsbody">
    <form id="form1" runat="server">
    <div>
        <table class="clstable">
            <tr> 
                <td class="clsrpthdr" colspan="3">Individual Loan Overdue</td>
            </tr>
            <tr> 
                <td class="clstd"></td>
                <td class="clstd"></td>
                <td class="clstd"></td>
            </tr>
            <tr> 
                <td class="clstd">Branch</td>
                <td class="clstd">
                    <asp:DropDownList ID="ddlBranch" runat="server" AutoPostBack="True" 
                        Width="136px">
                    </asp:DropDownList>
                </td>
                <td class="clstd">&nbsp;</td>
            </tr>
            <tr> 
                <td class="clstd">Principal Due&nbsp;&gt;=</td>
                <td class="clstd">
                    <asp:TextBox ID="txtPrincipalDue" runat="server" Width="100px"></asp:TextBox>                  
                </td>
                <td class="clstd">
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                        ControlToValidate="txtPrincipalDue" ErrorMessage="Numbers Only" 
                        ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr> 
                <td class="clstd">Interest Due &gt;=</td>
                <td class="clstd"><asp:TextBox ID="txtInterestDue" runat="server" Width="100px"></asp:TextBox></td>
                <td class="clstd">
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                        ControlToValidate="txtInterestDue" ErrorMessage="Numbers Only" 
                        ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr> 
                <td class="clstd">Instalment Due&nbsp;&gt;=
                </td>
                <td class="clstd" valign="middle">                 
                    <asp:TextBox ID="txtInstalmentDue" runat="server" Width="68px"></asp:TextBox>
                   
                    &nbsp;&lt;=
                    <asp:TextBox ID="txtInstalmentDueTo" runat="server" Width="40px">9999</asp:TextBox>
                  
                </td>
                <td class="clstd">
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtInstalmentDue" 
                        ErrorMessage="Numbers Only" ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
                    &nbsp;
                    </td>
            </tr>
            <tr> 
                <td class="clstd">Net Due&gt;=
                </td>
                <td class="clstd">
                    <asp:TextBox ID="txtNetLoanDue" runat="server" Width="100px"></asp:TextBox>
                  
                </td>
                <td class="clstd">
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtNetLoanDue" 
                        ErrorMessage="Numbers Only" ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr> 
                <td class="clstd">As On Date:</td>
                <td class="clstd">
                    <asp:TextBox ID="txtAsOnDate" runat="server" Width="70px"></asp:TextBox>
                    <asp:CalendarExtender ID="txtAsOnDate_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtAsOnDate" Format="dd/MM/yyyy">
                    </asp:CalendarExtender>
                    <asp:MaskedEditExtender ID="txtAsOnDate_MaskedEditExtender" runat="server" Mask="99/99/9999" MaskType="Date" Enabled="True" TargetControlID="txtAsOnDate" UserDateFormat="DayMonthYear">
                    </asp:MaskedEditExtender>
                </td>
                <td class="clstd">
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtInstalmentDueTo" 
                        ErrorMessage="Numbers Only" ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr> 
                <td class="clstd" colspan="2">
                    <div style="border-style: groove; border-width: 2px; height:200px; overflow:auto;">
                        <asp:CheckBoxList ID="chkList" runat="server" ForeColor="Black">
                        </asp:CheckBoxList>
                    </div>
                </td>
                <td class="clstd">&nbsp;</td>
            </tr>
            <tr> 
                <td class="clstd" colspan="3">
                    <asp:RadioButtonList ID="rdbFormat" runat="server" AutoPostBack="True" 
                        Height="27px" onselectedindexchanged="rdbFormat_SelectedIndexChanged" 
                        RepeatDirection="Horizontal" Width="221px">
                        <asp:ListItem>Format1</asp:ListItem>
                        <asp:ListItem Selected="True">Format2</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr> 
                <td class="clstd" colspan="3">
                    <asp:RadioButtonList ID="rdbSecured" runat="server" 
                        RepeatDirection="Horizontal" Width="238px">
                        <asp:ListItem>Secured</asp:ListItem>
                        <asp:ListItem>Un Secured</asp:ListItem>
                        <asp:ListItem Selected="True">All</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr> 
                <td class="clstd" colspan="3">
                    <asp:RadioButtonList ID="rdbMember" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True">Regular</asp:ListItem>
                        <asp:ListItem>Nominal</asp:ListItem>
                        <asp:ListItem>Associate</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr> 
                <td class="clstd" colspan="3">
                    <asp:RadioButtonList ID="rdbSummary" runat="server" 
                        RepeatDirection="Horizontal">
                        <asp:ListItem>Summary</asp:ListItem>
                        <asp:ListItem Selected="True">Detail</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr> 
                <td class="clstd" colspan="3">
                    <asp:RadioButtonList ID="rblOrder" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem>Member Wise</asp:ListItem>
                        <asp:ListItem>Scheme Wise</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr> 
                <td class="clstd" colspan="2">
                    <asp:CheckBox ID="chkAddress" runat="server" Text="With Loanee Address" />
                </td>
                <td class="clstd">&nbsp;</td>
            </tr>
            <tr> 
                <td class="clstd" colspan="2">
                    &nbsp;</td>
                <td class="clstd">&nbsp;</td>
            </tr>
            <tr> 
                <td class="clstd" colspan="3" align="center">
                    <asp:Button ID="btnSelect" runat="server" Text="Select All" OnClick="btnSelect_Click"  Width="70px" />
                    <asp:Button ID="btnOk" runat="server" OnClick="btnOk_Click" Text="Submit" 
                        Width="70px" />
                    <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click"  Width="70px" />
                    <asp:Button ID="btnBack" runat="server" onclick="btnBack_Click" Text="Back"  Width="70px" />
                </td>
            </tr>
            </table>
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>        
    </div>
  
    <%--<div>
        <table>
            <tr>
                <td class="clstd">
                    &nbsp;</td>
                <td class="clstd">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="clstd">
                    Branch
                </td>
                <td class="clstd">
                    &nbsp;</td>
            </tr>
        </table>
    </div>
    <div>
        <table>
            <tr bgcolor="#F7F7DE">
                <td class="clstd" bgcolor="White" class="style8">
                    
                </td>
                <td class="clstd" bgcolor="White" class="style7">
                    &nbsp;</td>
                <td class="clstd" bgcolor="White" class="style9">
                    &nbsp;
                    
                </td>
            </tr>
            <tr>
                <td class="clstd" class="style8">
                    
                </td>
                <td class="clstd" class="style7">
                    
                   
                </td>
                <td class="clstd" class="style9">
                    &nbsp;
                    
                </td>
            </tr>
            <tr>
                <td class="clstd" class="style8">
                    &nbsp;</td>
                <td class="clstd">                 
                   
                    &nbsp;&nbsp;
                  
                </td>
                <td class="clstd" class="style9">
                    &nbsp;&nbsp;
                  
                    
                  
                </td>
            </tr>
            <tr>
                <td class="clstd" class="style8">
                    &nbsp;</td>
                <td class="clstd" class="style7">
                    &nbsp;</td>
                <td class="clstd" class="style9">
                    &nbsp;
                    
                </td>
            </tr>
            <tr bgcolor="#F7F7DE">
                <td class="clstd" bgcolor="White" class="style8">
                    
                </td>
                <td class="clstd" colspan="2" bgcolor="White">
                    <asp:DropDownList ID="ddfrmday" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddfrmMonth" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                    &nbsp;
                    <asp:DropDownList ID="ddfrmYear" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="clstd" class="style3" colspan="3">
                              
                </td>
            </tr>
            <tr>
                <td class="clstd" class="style3" colspan="3">
                    
                </td>
            </tr>
            <tr>
                <td class="clstd" class="style3" colspan="3">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="clstd" class="style5" colspan="3" bgcolor="White">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="clstd" class="style8">
                    &nbsp;</td>
                <td class="clstd" class="style7">
                    &nbsp;
                </td>
                <td class="clstd" class="style9">
                    &nbsp;
                </td>
            </tr>
            <tr bgcolor="#F7F7DE">
                <td class="clstd" bgcolor="White" class="style8">
                    &nbsp;</td>
                <td class="clstd" bgcolor="White" class="style7">
                    &nbsp;</td>
                <td class="clstd" bgcolor="White" class="style9">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="clstd" class="style6" colspan="3">
                    &nbsp;</td>
            </tr>
        </table>
    </div>    --%>
    </form>
</body>
</html>
