<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FormCSelect.aspx.cs" Inherits="FormCSelect" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" src="Scripts/Esc.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" 
            DataKeyNames="TRNH_VOUCHERNO" EnableViewState="False" 
            onrowdatabound="gv_RowDataBound">
            <Columns>             
       <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="ProductSelector" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
        
         <asp:BoundField HeaderText="DD Date" DataField="TRNH_DD" 
        SortExpression="TRNH_DD" DataFormatString="{0:d}"></asp:BoundField>  
                 
        <asp:BoundField HeaderText="Drawer Bank Code" DataField="Drawer Bank Code" 
        SortExpression="Drawer Bank Code" ItemStyle-HorizontalAlign="Center">
<ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                       
        <asp:BoundField HeaderText="Drawee Bank Code" DataField="Drawee Bank Code" 
        SortExpression="Drawee Bank Code" ItemStyle-HorizontalAlign="Center">
<ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>     
       
         <asp:BoundField HeaderText="DemandDraft-No" DataField="TRND_INSTRUMENTNUMBER" 
        SortExpression="TRND_INSTRUMENTNUMBER"  ItemStyle-HorizontalAlign="Center">
<ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>  
        <asp:BoundField HeaderText="Amount" DataField="TRND_AMOUNT" 
        SortExpression="TRND_AMOUNT"  ItemStyle-HorizontalAlign="Right">
<ItemStyle HorizontalAlign="Right"></ItemStyle>
                </asp:BoundField>                         
          
         <asp:BoundField HeaderText="Issuing-Bank-Name(Drawer)" DataField="Issuing-Bank-Name(Drawer)" 
        SortExpression="Issuing-Bank-Name(Drawer)">
                </asp:BoundField>    
                
                  <asp:BoundField HeaderText="Payee-Bank-Name(Drawee)" DataField="Payee-Bank-Name(Drawee)" 
        SortExpression="Payee-Bank-Name(Drawee)">
                </asp:BoundField>                    
        </Columns>    
        </asp:GridView>
        <br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnPrint" runat="server" Text="Print Selected Transactions" 
            onclick="btnPrint_Click" />

            <asp:Button ID="goBack" runat="server" onclick="goBack_Click" Text="Back" />

    </div>    
    </form>
</body>
</html>
