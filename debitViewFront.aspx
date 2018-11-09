<%@ Page Language="C#" AutoEventWireup="true" CodeFile="debitViewFront.aspx.cs" Inherits="debitViewFront" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script language="javascript" type="text/javascript">
// <![CDATA[

        function goback_onclick() {

        }

// ]]>
    </script>
    </head>
<body>
    <form id="form1" runat="server">
    <div>
    <table>
     <td>
        <asp:GridView ID="gvViewDetails" runat="server" AutoGenerateColumns="False" 
            BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" 
            CellPadding="0" ForeColor="Black" GridLines="None" Height="213px" 
             Width="470px">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField HeaderText="Picture">

                <ItemTemplate>
                <img align="right" src="debitViewFrontH.ashx?id=<%# Eval("DBMT_SNO").ToString() %>&oraConn=<%# Session["constring"].ToString()%>" width="600" height="400" alt="" />
                </ItemTemplate>
                </asp:TemplateField>             
            </Columns>
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
         </asp:GridView>
    
        <input id="goback" align="right" type="button" value="Close" 
             onclick="JavaScript:window.close();" style="border: thin groove #FF0000" />
       </td>
       </table>
       </div>
    </form>
</body>
</html>
