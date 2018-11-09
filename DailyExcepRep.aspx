<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DailyExcepRep.aspx.cs" Inherits="DailyExcepRep" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Daily Exceptional Report</title>
     <link href="../Styles/cwebsite.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .newStyle1 {
            font-family: Verdana;
            font-size: medium;
            font-weight: bold;
        }
        .newStyle2 {
            font-family: Verdana;
            font-size: medium;
            font-weight: bold;
        }
    </style>
</head>
<body class="clsbody">
    <form id="form1" runat="server">
     <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server">
              </asp:ToolkitScriptManager>
       <div align="center" style="height: 441px">
    
           <table class="clstable">
               <tr>
                   <td class="clsrpthdr" colspan="2">DAILY EXCEPTIONAL REPORT</td>
               </tr>
               <tr>
                   <td>
                       <asp:RadioButtonList ID="rblSumDet" runat="server" RepeatDirection="Horizontal">
                           <asp:ListItem Value="S">Summary</asp:ListItem>
                           <asp:ListItem Selected="True" Value="D">Detail</asp:ListItem>
                       </asp:RadioButtonList>
                   </td>
                   <td>
                       &nbsp;</td>
               </tr>
               <tr>
                   <td class="clstd">Branch</td>
                   <td class="clstd">
                    <asp:DropDownList ID="ddlBranch" runat="server" 
                        style="font-family: Verdana; font-size: small" Width="150px">
                    </asp:DropDownList>
                   </td>
               </tr>
               <tr>
                   <td class="clstd">As on Date</td>
                   <td class="clstd">
                       <asp:TextBox ID="txtAsonDate" runat="server" Width="100px"></asp:TextBox>
                       <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" 
                                            Format="dd/MM/yyyy" PopupPosition="BottomRight" 
                                            TargetControlID="txtAsonDate" TodaysDateFormat="DD/MM/YYYY"></asp:CalendarExtender>
                                        <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Enabled="True" 
                                            Mask="99/99/9999" MaskType="Date" TargetControlID="txtAsonDate"></asp:MaskedEditExtender>
                   </td>
               </tr>
               <tr>
                   <td align="center" colspan="2">
                    <asp:Button ID="btnOK" runat="server" Text="OK" OnClick="btnOK_Click" Width="100px" style="font-weight: 700" TabIndex="5" />

                    &nbsp;<asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" Width="100px" style="font-weight: 700" TabIndex="5" />

                    &nbsp;<asp:Button ID="btnBack" runat="server" Text="Back" Width="100px" OnClick="btnBack_Click" style="font-weight: 700" />
                    
      
                    
       <asp:ImageButton ID="imgxl0" runat="server" Height="20px" 
           ImageUrl="~/img/images.jpg" onclick="imgxl_Click" Width="57px" ImageAlign="AbsMiddle" />
                    
                   </td>
               </tr>
           </table>
    
           <br />


        <asp:Panel ID="pnlGrid" runat="server">
        <table align="center">
            <tr>
                <td align="center">
                    <asp:Label ID="lblBank" runat="server" Font-Bold="True" Visible="False" CssClass="newStyle1"></asp:Label>
                    <br />
                    <asp:Label ID="lblBranch" runat="server" Font-Bold="True" Visible="False" CssClass="newStyle2"></asp:Label>
                    <br />
                    <%-- <asp:GridView ID="gvRptDisp" runat="server" 
                            Caption="Share Partial Transaction Report" CaptionAlign="Top" 
                            Font-Names="Cambria" ForeColor="Black" onrowdatabound="gvRptDisp_RowDataBound">
                        </asp:GridView>--%>
                    <asp:Label ID="lblHeading" runat="server" Font-Bold="True" Visible="False" CssClass="newStyle2"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:GridView ID="gvDetails" runat="server" HeaderStyle-Font-Size="Small" 
                        Font-Bold="false" Font-Names="VERDANA" Font-Size="Small"  
                        OnRowDataBound="gvDetails_RowDataBound">
                        <HeaderStyle Font-Size="Small" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
              </asp:Panel>


    
    </div>
    </form>
</body>
</html>
