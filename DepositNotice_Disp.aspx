



<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DepositNotice_Disp.aspx.cs" Inherits="Notice_DepositNotice_Disp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Deposit Notice</title>   
    <style type="text/css">
        .style1
        {
            width: 100%;
        }      
        .grRowStyle
        {            
            border-top:solid 3px #000000;       
           border-left:solid 3px #000000;
           border-right:solid 3px #000000;        
        } 
         .grColmStyle
        {               
            border-bottom:solid 3px #000000;       
           border-left:solid 3px #000000;
           border-right:solid 3px #000000;
        } 
         .grTextStyle
         {
              border-left:solid 3px #000000;
           border-right:solid 3px #000000;
         }
    </style>
    <script src="Scripts/Esc.js" type="text/javascript"></script>        

</head>
<body>
    <form id="form2" runat="server">
    <div id="dvContents" align="center" class="Top10ArticleHeading">    
        <%--/////////LS--%>
     <asp:Panel ID="Panel1" runat="server" Width="100%">     
      
        <table align="center">
            <tr>
                <td  align="left">
                        <asp:GridView ID="gvRptDisp" runat="server" GridLines="None" 
                            onrowdatabound="gvRptDisp_RowDataBound" Width="1000px">
                        </asp:GridView>
                                  
                                  </td>
            </tr>
  <%--  ///END--%>

        <%-- <asp:Panel ID="Panel1" runat="server" Width="702px" Font-Names="BRH Kannada">     --%>
      <%-- <table align="center" style="width: 659px">--%>
           
        </table>
    
      <%--  <table align="center">--%>
            <%--<tr>
                <td  align="left">
                        <asp:GridView ID="gvRptDisp" runat="server" 
               onrowdatabound="gvRptDisp_RowDataBound" PageSize="9" GridLines="None" 
                            HorizontalAlign="Left" Font-Size="10pt" 
                            onrowcreated="gvRptDisp_RowCreated">
                            <EmptyDataRowStyle Wrap="False" />              
           </asp:GridView>   
                                  </td>
            </tr>--%>
          <%--  <tr>
                <td  align="left">
                
                </td>
            </tr>--%>
<%--        </table>--%>
          </asp:Panel>                                              
        </div>
       
    <table class="style1">
        <tr>
            <td align="center">
                                <input type="button" value ="Back" id="goBack"
                    onclick="window.history.back();return true;"
                    style="font-weight: 700;color: #003399" height="24" width="57"/></td>
        </tr>
    </table>
    </form>
</body>
</html>
