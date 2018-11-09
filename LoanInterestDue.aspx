<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LoanInterestDue.aspx.cs" Inherits="LoanInterestDue" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <title> Loan Interestdue</title>
    <link href="Styles/cwebsite.css" rel="stylesheet" type="text/css" />
    
    </head>
<body>
    <form id="form1" runat="server">
    <div >
     
        <table class="clstable">
         <tr align="center">
            <td colspan="3" style="text-align: center" class="clsrpthdr">
                Loan Interestdue</td>
        </tr>
         <tr>
            <td class="style3" colspan="2">
                    &nbsp;</td>
        </tr>
           <tr>
            <td class="clstd">
                    Branch</td>
            <td class="style3">
                <asp:DropDownList ID="ddlBranch" runat="server" 
                    onselectedindexchanged="ddlBranch_SelectedIndexChanged1">
                </asp:DropDownList>
            
               </td>
            <td class="clstd">               
                &nbsp;</td>
        </tr>
         
            <tr>
                <td class="clstd" >
                    Principal Due=
                </td>
              
                <td class="clstd">
                    <asp:TextBox ID="txtPrincipalDue" runat="server" runat="server" BackColor="#CAEBEE"></asp:TextBox>
                  
                </td>
                <td class="clstd">
                    &nbsp;
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                        ControlToValidate="txtPrincipalDue" ErrorMessage="Pricipal due must be Numeric" 
                        ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="clstd">
                    Interest Due=
                </td>
                <td class="clstd">
                    <asp:TextBox ID="txtInterestDue" runat="server" runat="server" BackColor="#CAEBEE"></asp:TextBox>
                   
                </td>
                <td class="clstd">
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                        ControlToValidate="txtInterestDue" ErrorMessage="Interest due must be Numeric" 
                        ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
                   
                </td>
                <td class="clstd">
                    &nbsp;
                    </td>
            </tr>
            <tr>
                <td class="clstd">
                    Instalment Due=
                </td>
                <td class="clstd">
                    <asp:TextBox ID="txtInstalmentDue" runat="server" Width="60px" BackColor="#CAEBEE"></asp:TextBox>
                   
                    &lt;=<asp:TextBox ID="txtInstalmentDueTo" runat="server" Width="40px" BackColor="#CAEBEE">9999</asp:TextBox>
                  
                </td>
                <td>                 
                  
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                        ControlToValidate="txtInstalmentDue" 
                        ErrorMessage="Instalment due must be Numeric" ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
                    <br />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" 
                        ControlToValidate="txtInstalmentDueTo" 
                        ErrorMessage="Instalment due must be Numeric" ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
                  
                </td>
                <td class="clstd">
                    &nbsp;&nbsp;
                  
                    &nbsp;
                                      
                </td>
            </tr>
            <tr>
                <td class="clstd">
                    Net Due=
                </td>
                <td class="clstd">
                    <asp:TextBox ID="txtNetLoanDue" runat="server" BackColor="#CAEBEE"></asp:TextBox>
                  
                </td>
                <td class="clstd" >
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" 
                        ControlToValidate="txtNetLoanDue" ErrorMessage="Net Loan due must be Numeric" 
                        ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
                </td>
                <td class="clstd" >
                    &nbsp;
                    </td>
            </tr>
            <tr >
                <td class="clstd" colspan="3">
                    Ason Date&nbsp;&nbsp;  <asp:DropDownList ID="ddfrmday" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddfrmMonth" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddfrmYear" runat="server" AutoPostBack="True">
                    </asp:DropDownList></td>
               
            </tr>
            <tr>
                <td class="clstd" colspan="2">
               <div style="border-style: solid; border-width: 1px; height:190px; overflow:auto; width: 261px; margin-top: 0px; background-color: #CAEBEE;">
                    <asp:CheckBoxList ID="chkList" runat="server" BackColor="White"
                            ForeColor="Black">
                    </asp:CheckBoxList>
                    </div>              
                </td>
            </tr>
           
            <tr>
                <td class="clstd" colspan="2">
                    <asp:RadioButtonList ID="rdbSecured" runat="server" 
                        RepeatDirection="Horizontal" Width="238px">
                        <asp:ListItem>Secured</asp:ListItem>
                        <asp:ListItem>Un Secured</asp:ListItem>
                        <asp:ListItem Selected="True">All</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td class="clstd" colspan="2" >
                    <asp:RadioButtonList ID="rdbMember" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem>Regular</asp:ListItem>
                        <asp:ListItem>Nominal</asp:ListItem>
                        <asp:ListItem>Associate</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
           
            <tr>
                <td class="clstd" colspan="2">
                    <asp:CheckBox ID="chkAddress" runat="server" Text="With Loanee Address" />
               
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
               
                    <asp:CheckBox ID="chkArbt" runat="server" Text="Include Arbritation" />
                </td>
              
            </tr>
            <tr>
                <td class="clstd" colspan="2">
                    <asp:Button ID="btnSelect" runat="server" Text="Select All" OnClick="btnSelect_Click" Width="70px" />
                    <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" Width="70px" />
                    <asp:Button ID="btnOk" runat="server" OnClick="btnOk_Click" Text="OK" Width="70px" />
                    <asp:Button ID="btnBack" runat="server" onclick="btnBack_Click" Text="Back" Width="70px" />
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
     <br />
     <br />
     <br />
     <br />
     <br />
     <br />
     <br />
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

