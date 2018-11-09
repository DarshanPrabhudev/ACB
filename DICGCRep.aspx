<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DICGCRep.aspx.cs" Inherits="DICGCRep" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>DICGC REPORT</title>
    <link href="../Styles/cwebsite.css" rel="stylesheet" type="text/css" />
    </head>
<body class="clsbody">
    <form id="form1" runat="server">
     <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server">
              </asp:ToolkitScriptManager>
    <div align="center">
    
        <table class="clstable">
            <tr>
                <td colspan="2" class="clsrpthdr">
                    DICGC REPORT</td>
            </tr>
            <tr>
                <td align="left" colspan="2">
                    <asp:RadioButtonList ID="rblBrnchConsol" runat="server" 
                        RepeatDirection="Horizontal" AutoPostBack="True" 
                        onselectedindexchanged="rblBrnchConsol_SelectedIndexChanged">
                        <asp:ListItem Value="B" Selected="True">Branch</asp:ListItem>
                        <asp:ListItem Value="C">Consolidated</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td class="clstd">
                    Branch</td>
                <td class="clstd">
                    <asp:DropDownList ID="ddlBranch" runat="server" 
                        style="font-family: Verdana; font-size: small" Width="100px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="clstd">
                    Date</td>
                <td class="clstd">
                    <asp:TextBox ID="txtDate" runat="server" Width="100px" 
                        style="font-family: Verdana; font-size: small"></asp:TextBox>
                         <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" 
                                            Format="dd/MM/yyyy" PopupPosition="BottomRight" 
                                            TargetControlID="txtDate" TodaysDateFormat="DD/MM/YYYY"></asp:CalendarExtender>
                                        <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Enabled="True" 
                                            Mask="99/99/9999" MaskType="Date" TargetControlID="txtDate"></asp:MaskedEditExtender>

                </td>
            </tr>
            <tr>
                <td class="clstd">
                    Balance &gt;=</td>
                <td class="clstd">
                    <asp:TextBox ID="txtBalance" runat="server" CssClass="clstd" Width="100px" style="text-align:right; "></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                        ControlToValidate="txtBalance" Display="Dynamic" ErrorMessage="Enter Amount" 
                        Font-Bold="True"  ForeColor="#FF0066" SetFocusOnError="True" 
                        ValidationExpression="^[0-9]\d*(\.\d+)?$" style="color: #FF0000"></asp:RegularExpressionValidator>

                </td>
            </tr>
            <tr>
                <td class="clstd">
                    Schemes</td>
                <td align="left" >
                  <div align="left" 
                            
                            
                        
                        style="border-style: solid; border-width: 1px; height:248px; overflow:auto; width: 306px; margin-top: 0px; margin-bottom: 0px; background-color: #CAEBEE;">
                    <asp:CheckBoxList ID="chkScheme" runat="server" Font-Bold="False" 
                        style="font-family: Verdana; font-size: small">
                    </asp:CheckBoxList>
                     </div>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:Button ID="btnOK" runat="server" Text="OK" OnClick="btnOK_Click" Width="100px" style="font-weight: 700" TabIndex="5" />

                    &nbsp;<asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" Width="100px" style="font-weight: 700" TabIndex="5" />

                    &nbsp;<asp:Button ID="btnBack" runat="server" Text="Back" Width="100px" OnClick="btnBack_Click" style="font-weight: 700" />
                    
      
                    
       <asp:ImageButton ID="imgxl0" runat="server" Height="24px" 
           ImageUrl="~/img/images.jpg" onclick="imgxl_Click" Width="57px" ImageAlign="AbsMiddle" />
                    
                </td>
            </tr>
        </table>


        <asp:Panel ID="pnlGrid" runat="server">
        <table align="center">
            <tr>
                <td align="center">
                    <asp:Label ID="lblBank" runat="server" Font-Bold="True" Visible="False"></asp:Label>
                    <br />
                    <asp:Label ID="lblBranch" runat="server" Font-Bold="True" Visible="False"></asp:Label>
                    <br />
                    <%-- <asp:GridView ID="gvRptDisp" runat="server" 
                            Caption="Share Partial Transaction Report" CaptionAlign="Top" 
                            Font-Names="Cambria" ForeColor="Black" onrowdatabound="gvRptDisp_RowDataBound">
                        </asp:GridView>--%>
                    <asp:Label ID="lblHeading" runat="server" Font-Bold="True" Visible="False"></asp:Label>
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
