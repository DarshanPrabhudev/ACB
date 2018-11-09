<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DBTL_LogDisp.aspx.cs" Inherits="DBTL_LogDisp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 30%;
        }
        .style2
        {
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
   <div align="center" style="height: 409px">  
    <asp:Panel ID="Panel1" runat="server" Height="311px" >  

    <table>
               <tr>
                   <td align="center">
                       <asp:Label ID="lblBank" runat="server" Font-Bold="True"></asp:Label>
                   </td>
               </tr>
               <tr>
                   <td align="center">
                       <strong>Branch:</strong><asp:Label ID="lblBranch" runat="server" Font-Bold="True"></asp:Label>
                   </td>
               </tr>
               <tr>
                   <td align="center">
                       <asp:Label ID="lblRptHead" runat="server" Font-Bold="True" 
                           Font-Underline="True"></asp:Label>
                   </td>
               </tr>
           </table>
           <br>
           <br>
          <div align="center" style="overflow: auto; height: 133px; width: 908px;">
        <asp:GridView ID="gvDBTL" runat="server" onrowdatabound="gvDBTL_RowDataBound" 
            Height="16px" Width="406px">
        </asp:GridView>
         </div>
        <table style="width: 359px">
            <tr>
                <td class="style2">
                    Total Non-Credited:
                    <asp:Label ID="lblNonCre" runat="server" style="font-weight: 700"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    Total Credited:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblCredit" runat="server" style="font-weight: 700"></asp:Label>
                </td>
            </tr>
        </table>
     
               <br />
        <br />
        <table class="style1">
            <tr>
                <td>
                    <asp:ImageButton ID="imgxl" runat="server" Height="24px" 
                        ImageUrl="~/img/images.jpg" onclick="imgxl_Click" Width="57px" />
                    <input type="button" value ="Back" id="goBack"
                    onclick="window.history.back();return true;"
                    style="font-weight: 700;color: #003399"/>
                </td>
            </tr>
        </table>
     
               </asp:Panel>  
               
             
    
    </div>
    </form>
</body>
</html>
