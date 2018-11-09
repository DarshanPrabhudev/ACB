<%@ Page Language="C#" AutoEventWireup="true" CodeFile="debitSignature.aspx.cs" Inherits="debitSignature" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
<script language="javascript" type="text/javascript">

function goBack_onclick() {

}


</script>
    </head>
<body>
    <form id="form1" runat="server">
    <div align="left" >
    
    <table><td>
        <asp:GridView ID="gvDebitSign" runat="server" CellPadding="0" ForeColor="Black" 
            GridLines="None" AutoGenerateColumns="False" 
            BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" 
            Width="888px">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="USER_FULLNAME" HeaderText="Scanned By" >
                <HeaderStyle HorizontalAlign="Left" />
                </asp:BoundField>     
                <asp:TemplateField  HeaderText="Picture" >
                <ItemTemplate>
                    <img  align="right"  src="sigDebitHandler.ashx?id=<%# Eval("CUST_SNO").ToString()%> &oraConn=<%# Session["constring"].ToString()%>" width="600" height="400" alt="" />
               </ItemTemplate>
                    <HeaderStyle VerticalAlign="Bottom" />
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
                    <input type="button"  id="goBack"
                     onclick="JavaScript:window.close();" value="Close"
                    style="font-weight: 700;color: #003399" height="24" width="57" align="right"/>
                    </td></table>     </div>
    </form>
</body>
</html>
