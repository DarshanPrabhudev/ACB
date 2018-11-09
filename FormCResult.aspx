<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FormCResult.aspx.cs" Inherits="FormCResult" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
       <link href="cwebsite.css" rel="stylesheet" type="text/css"  media="print"/>

</head>
<body>
    <form id="form1" runat="server">
    <div>    
 <asp:Panel ID="Panel1" runat="server">
        <table class="style1">
            <tr>
                <td align="center">
                    <asp:Label ID="Label1" runat="server" Text="BankName"></asp:Label>
                    <br />
&nbsp;Branch :
                    <asp:Label ID="Label2" runat="server" Text="BankName"></asp:Label>
                    <br />
                    Formc-C Details FROM                     
                    <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
                &nbsp;To
                    <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Date :<asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;To, &nbsp;&nbsp;<br />
                    GENERAL MANAGER<br />
                    &nbsp;<asp:Label ID="lblManager" runat="server" Text="Manager"></asp:Label>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    Sir,<br />
                                        &nbsp; We have to advice that from 
                    <asp:Label ID="lblFrmDate" runat="server"></asp:Label>
                    &nbsp;&nbsp; to&nbsp;&nbsp;
                    <asp:Label ID="lblToDate" runat="server"></asp:Label>
                    &nbsp;we have paid the marginally noted DDs drawn on us by the various member Banks 
                    under mutual arrangement scheme. </td>
            </tr>
            <tr>
                <td>
                   
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                        onrowdatabound="GridView1_RowDataBound">
            <Columns>             
        <asp:BoundField HeaderText="Sno" DataField="Sno"></asp:BoundField>  
        
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
                
                                    
        </Columns>    
        </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    Kindly credit a sum of&nbsp; Rupees. 
                    <asp:Label ID="lblAmount" runat="server" ForeColor="Red"></asp:Label>
&nbsp;Only. by debiting the account of the drawer Banks with you. </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; For&nbsp;
                    <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label>
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                    Authorised Signatory</td>
            </tr>
        </table>
     
  </asp:Panel>
    </div>
    <p>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:ImageButton ID="imgxl" runat="server" Height="24px" 
           ImageUrl="~/img/images.jpg" onclick="imgxl_Click" Width="57px" />
            <asp:Button ID="goBack" runat="server" onclick="goBack_Click" Text="Back" />
    </p>
    </form>
</body>
</html>
