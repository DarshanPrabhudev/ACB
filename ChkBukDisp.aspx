<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChkBukDisp.aspx.cs" Inherits="ChkBukDisp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CHEQUE BOOK DISPLAY</title>
      <link href="Styles/WebAppl.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="Scripts/Esc.js"></script>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        </style>
</head>
<body class=clsbody >
    <form id="form1" runat="server">
     
    <div align="center"  > 
        <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server">
        </asp:ToolkitScriptManager>
    
        <table align="center" style="border: medium solid #808080">
            <tr>
                <td align="center">
                    Branch:<asp:Label ID="lblBranch" runat="server" style="font-weight: 700"></asp:Label>
                       </td>
            </tr>
            <tr>
                <td align="center" class="clsHeadingTd">
                    <strong>CHEQUE BOOK DETAILS</strong></td>
            </tr>
            <tr>
                <td>
                <div style="overflow:auto;height:156px; width: 894px;">
                           <asp:GridView ID="gvChkBukPrint" runat="server" align="center" 
                                Width="882px" CellPadding="4" 
                               ForeColor="#333333" GridLines="None" CssClass="clsGrid" 
                               Font-Underline="False" onrowdatabound="gvChkBukPrint_RowDataBound" >
                               <AlternatingRowStyle BackColor="White" />
                               <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                 <HeaderStyle BackColor="#FFAE88" Font-Bold="True" ForeColor="#993333" />
                               <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                               <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                               <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                               <SortedAscendingCellStyle BackColor="#FDF5AC" />
                               <SortedAscendingHeaderStyle BackColor="#4D0000" />
                               <SortedDescendingCellStyle BackColor="#FCF6C0" />
                               <SortedDescendingHeaderStyle BackColor="#820000" />
                           </asp:GridView>
                           </div>
                       </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btnBack" runat="server" ForeColor="Black" 
                        onclick="btnBack_Click" Text="Back" 
                        CssClass="clsCornerBtn" Width="100px" />
                    <asp:Button ID="btnText" runat="server" Text="Generate Text File" onclick="btnText_Click" 
                         CssClass="clsCornerBtn" Width="200px" />
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
