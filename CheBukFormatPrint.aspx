<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheBukFormatPrint.aspx.cs" Inherits="CheBukFormatPrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cheque Book Print Format</title>
      <link href="Styles/WebAppl.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            font-family: Calibri;
            font-size: small;
            color: Black;
            font-size: x-large;
            text-decoration: underline;
        }
    </style>
    </head>
<body class=clsbody >
    <form id="form1" runat="server">
     
    <div align="center"  > 
        <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server">
        </asp:ToolkitScriptManager>

     <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
        <table align="center" style="border: medium solid #808080">
            <tr>
                <td colspan="4" class="clsHeadingTd" align="center">
                    <strong>CHEQUE BOOK PRINT FORMAT</strong></td>
            </tr>
            <tr>
                <td align="left" class="clstd">
                    &nbsp;</td>
                <td align="left" class="clstd">
                    &nbsp;</td>
                <td align="left" class="clstd">
                    &nbsp;</td>
                <td align="left" class="clstd">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="left" class="clstd">
                    Branch</td>
                <td align="left" class="clstd">
                    <asp:DropDownList ID="ddlBranch" runat="server" CssClass="clsDDL" 
                        onselectedindexchanged="rblDeposit_SelectedIndexChanged" Width="150px">
                    </asp:DropDownList>
                </td>
                <td align="left" class="clstd">
                    &nbsp;</td>
                <td align="left" class="clstd">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="left" class="clstd">
                    Account Type</td>
                <td align="left" class="clstd">
                            <asp:RadioButtonList ID="rblDeposit" runat="server" AutoPostBack="True" 
                                onselectedindexchanged="rblDeposit_SelectedIndexChanged" 
                                RepeatDirection="Horizontal" TabIndex="1" 
                                style="font-family: Arial, Helvetica, sans-serif; font-size: small" 
                                CssClass="clsRBL">
                                <asp:ListItem Selected="True" Value="Deposit">Deposit</asp:ListItem>
                                <asp:ListItem Value="OverDraft">OverDraft</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                <td align="left" class="clstd">
                    &nbsp;</td>
                <td align="left" class="clstd">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="left" class="clstd">
                    Scheme</td>
                <td align="left" class="clstd">
                            <asp:DropDownList ID="ddlScheme" runat="server" AutoPostBack="True" 
                                onselectedindexchanged="ddlScheme_SelectedIndexChanged" CssClass="clsDDL" 
                                Width="200px" TabIndex="2">
                            </asp:DropDownList>
                        </td>
                <td align="center" colspan="2" class="clstd">
                    <asp:CheckBox ID="cbAtPar" runat="server" AutoPostBack="True" 
                        CssClass="clsCheckBox" Font-Bold="True" Text="At ParY/N" TextAlign="Left" />
                </td>
            </tr>
            <tr>
                <td align="left" class="clstd">
                    Account Number</td>
                <td align="left" class="clstd">
                            <asp:TextBox ID="lblAccType" runat="server" CssClass="clsTextBoxAbbr" 
                                ReadOnly="True" Width="40px"></asp:TextBox>
                        <asp:TextBox ID="txtAccNo" runat="server" CssClass="clstxtDate" 
                        MaxLength="10" Width="150px" ontextchanged="txtAccNo_TextChanged" 
                                AutoPostBack="True" CausesValidation="True" TabIndex="3"></asp:TextBox>
                </td>
                <td align="left" class="clstd" colspan="2">
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                        ControlToValidate="txtAccNo" CssClass="clsRegExp" Display="Dynamic" 
                        ErrorMessage="Enter Valid A/C Number" SetFocusOnError="True" 
                        ValidationExpression="^[0-9]+$"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td align="left" class="clstd">
                    Name</td>
                <td align="left" class="clstd">
                    <asp:TextBox ID="lblName" runat="server" CssClass="clstxtDate" ReadOnly="True" 
                        Width="200px"></asp:TextBox>
                </td>
                <td align="left" class="clstd">
                    &nbsp;</td>
                <td align="left" class="clstd">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="left" class="clstd">
                    Cheque Book Number</td>
                <td align="left" class="clstd">
                            <asp:TextBox ID="txtChqBukNum" runat="server" CssClass="clstxtDate" 
                                ReadOnly="True" Width="200px"></asp:TextBox>
                </td>
                <td align="left" class="clstd">
                    No of Leaves</td>
                <td align="left" class="clstd">
                    <asp:TextBox ID="lblNoOfLeaves" runat="server" CssClass="clstxtDate" 
                        ReadOnly="True" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left" class="clstd">
                    Leaf&nbsp; No. From</td>
                <td align="left" class="clstd">
                    <asp:TextBox ID="txtLeafFrom" runat="server" CssClass="clstxtDate" 
                        ReadOnly="True" Width="200px"></asp:TextBox>
                </td>
                <td align="left" class="clstd">
                    Leaf&nbsp; No. To</td>
                <td align="left" class="clstd">
                    <asp:TextBox ID="txtLeafTo" runat="server" CssClass="clstxtDate" ReadOnly="True" 
                        Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="4">
                    <asp:Button ID="btnPrint" runat="server" CssClass="clsCornerBtn" Text="PRINT" 
                        Width="100px" onclick="btnPrint_Click" TabIndex="4" />
                    <asp:Button ID="txtClear" runat="server" CssClass="clsCornerBtn" Text="CLEAR" 
                        Width="100px" TabIndex="5" onclick="txtClear_Click" />
                    <asp:Button ID="btnBack" runat="server" CssClass="clsCornerBtn" Text="BACK" 
                        Width="100px" onclick="btnBack_Click" TabIndex="6" />
                </td>
            </tr>
        </table>
         </ContentTemplate>
        </asp:UpdatePanel>
    
    </div>
    </form>
</body>
</html>
