<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HousingLoan.aspx.cs" Inherits="Appl_HousingLoan" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Loan Interest Received Certificate</title> 
  
    <style type="text/css">
        #btnReturn
        {
            font-weight: 700;
        }
    </style>
  
</head>
<body>
    <form id="form1" runat="server">
    <table style="border: medium solid #808080" align="center">
            <tr>
                <td align="center" bgcolor="Black" class="style1" 
                    colspan="4" 
                    
                    style="color: #000000; font-size: medium; background-color: #66CCFF; font-weight: bold;">
                    &nbsp;Loan Interest Received Certificate</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td class="style3">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td class="style2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    Branch</td>
                <td class="style3">
                    <asp:DropDownList ID="ddlBranch" runat="server" 
                        Height="20px" Width="120px">
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;</td>
                <td class="style2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td class="style3">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td class="style2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    Ref&nbsp; No.</td>
                <td class="style3">
                    <asp:TextBox ID="txtAbbr" runat="server" Height="20px"  style=" text-transform:uppercase; font-weight: 700;" 
                        MaxLength="5"  AutoPostBack="True" 
                        ontextchanged="txtAbbr_TextChanged" TabIndex="1" CausesValidation="True" 
                        Width="50px"></asp:TextBox>
                    &nbsp;<asp:TextBox ID="txtAcno" runat="server" 
                        AutoPostBack="True" Height="20px" MaxLength="15" 
                        ontextchanged="txtAcno_TextChanged" Width="70px" TabIndex="2" 
                        CausesValidation="True"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
                <td class="style2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td class="style3">
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                        ControlToValidate="txtAbbr" Display="Dynamic" 
                        ErrorMessage="Alphabets Only" ForeColor="#EC0000" style="font-weight: 700" 
                        ValidationExpression="^[A-Za-z]+$" SetFocusOnError="True"></asp:RegularExpressionValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                        ControlToValidate="txtAcno" Display="Dynamic" 
                        ErrorMessage="Enter  valid A/C Number" ForeColor="#EC0000" 
                        SetFocusOnError="True" style="font-weight: 700" ValidationExpression="^[0-9]+$"></asp:RegularExpressionValidator>
                </td>
                <td>
                    &nbsp;</td>
                <td class="style2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;Name</td>
                <td class="style3">
                    <asp:TextBox ID="txtName" runat="server" Height="20px" 
                        ReadOnly="True" style="font-weight: 700" 
                        Width="331px" Enabled="False"></asp:TextBox>
                </td>
                <td>
                    Client Code</td>
                <td class="style2">
                    <asp:TextBox ID="txtClientCode" runat="server" 
                        Height="20px" ReadOnly="True" Enabled="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td class="style3">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td class="style2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    From Date</td>
                <td class="style3">
                    <asp:DropDownList ID="ddfrmday" runat="server">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddfrmMonth" runat="server">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddfrmYear" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    To Date</td>
                <td class="style2">
                    <asp:DropDownList ID="ddtoday" runat="server">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddtoMonth" runat="server">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddtoYear" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td class="style3">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td class="style2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    Place</td>
                <td class="style3">
                    <asp:TextBox ID="txtPlaceName" runat="server" Font-Bold="True" Width="200px">JAMKHANDI</asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
                <td class="style2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td class="style3">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td class="style2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="4" align="center">
                    &nbsp;<asp:Button ID="btnCertificate" runat="server" 
                        ForeColor="Black" Height="26px" onclick="btnCertificate_Click" 
                        style="font-weight: 700" Text="Certificate" Width="88px" />
                    <asp:Button ID="btnClear" runat="server" 
                        Font-Bold="True" ForeColor="Black" Height="26px" onclick="btnClear_Click" 
                        Text="Clear All" Width="88px" />
                    <asp:Button ID="btnBack" runat="server" 
                        ForeColor="Black" Height="26px" onclick="btnBack_Click" 
                        style="font-weight: 700" Text="Back" Width="88px" />
                </td>
            </tr>
        </table>
    <div>
    
    </div>
    </form>
</body>
</html>
