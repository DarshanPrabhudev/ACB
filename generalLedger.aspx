<%@ Page Language="C#" AutoEventWireup="true" CodeFile="generalLedger.aspx.cs" Inherits="generalLedger" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div align="center" 
            
            
            style="color: #000000; font-size: medium; background-color: #66CCFF; height: 27px; width: 338px;">
     General Ledger
    </div>
       
        
        <table>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    Branch</td>
                <td>
                    <asp:DropDownList ID="ddBranch" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style3">
                    &nbsp;</td>
                <td class="style4">
                    <asp:RadioButtonList ID="rbtnSD" runat="server" RepeatDirection="Horizontal" 
                        style="font-weight: 700">
                        <asp:ListItem Value="S" Selected="True">Summary</asp:ListItem>
                        <asp:ListItem Value="D">Detail</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    From</td>
                <td>
                    <asp:DropDownList ID="ddfrmday" runat="server">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddfrmMonth" runat="server">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddfrmYear" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style5">
                    To</td>
                <td>
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
                    Scheme Name:</td>
                <td>
                    <asp:DropDownList ID="ddschemeName" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style5">
                    G L No</td>
                <td>
                    <asp:TextBox ID="txtFromNo" runat="server" Height="22px" Width="63px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:RadioButtonList ID="rdbtGl" runat="server" RepeatDirection="Horizontal" 
                        style="font-weight: 700">
                        <asp:ListItem Value="optAllDrCr" Selected="True">All</asp:ListItem>
                        <asp:ListItem Value="optDebitTran">Debit</asp:ListItem>
                        <asp:ListItem Value="optCreditTran">Credit</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td class="style5">
                    <asp:Button ID="btnok" runat="server" onclick="btnok_Click" 
                        style="font-weight: 700; height: 26px;" Text="OK" Width="56px" />
                </td>
                <td>
                <asp:Button ID="btnBack" runat="server" onclick="btnBack_Click" Text="Back" 
                        Width="80px" Font-Bold="True" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
        <br />
        <br />
        <br />
    
    </div>
    </form>
</body>
</html>
