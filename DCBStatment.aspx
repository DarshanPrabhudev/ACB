<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DCBStatment.aspx.cs" Inherits="DCBStatment" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="ascx/iisplHdr.ascx" tagname="iisplHdr" tagprefix="uc1" %>
<%@ Register src="ascx/iisplFooter.ascx" tagname="iisplFooter" tagprefix="uc2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>DCB STATEMENT[CBS]</title>

    <link href="Styles/cwebsite.css" rel="stylesheet" type="text/css" />
     <script type="text/javascript">


         function PrintDiv() {
             var panel = document.getElementById("pnlGrid");
             var printLoanRepay = window.open('', '', 'height=1076,width=1076');

             printLoanRepay.document.write('<html><head>');
             printLoanRepay.document.write('</head><body>');
             printLoanRepay.document.write(panel.innerHTML);
             printLoanRepay.document.write('</body><body>');
             printLoanRepay.document.close();
             setTimeout(function () {
                 printLoanRepay.print();
             }, 500);
             return false;
         }

</script>
 </head>
<body class="clsbody">
    <form id="form1" runat="server">
         <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</asp:ToolkitScriptManager>
    
        <%--<uc1:iisplHdr ID="iisplHdr" runat="server" />--%>
        <div class="auto-style1">
        <table class="clstable" align="left">
            <tr>
                <td class="clsrpthdr" bgcolor="#99CCFF" colspan="2"
                    title="DCB STATEMENT[CBS]">
                   DCB STATEMENT[CBS] </td>
            </tr>
            <tr>
                <td class="clstd">Branch:</td>
                <td class="auto-style2">
                    <asp:DropDownList ID="ddlBranch" runat="server" Width="136px" AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" >
                    </asp:DropDownList>
                </td>
            </tr>
            
            <tr>
                <td class="clstd">From Date:</td>
                <td class="clstd">
                    <asp:TextBox ID="txtFromDate" runat="server" Width="80px" 
                                    style="text-align: left" CssClass="clstxtDate" AutoPostBack="True" 
                                    ></asp:TextBox>
                                <asp:CalendarExtender ID="txtFromDate_CalendarExtender" runat="server" 
                                    Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFromDate">
                                    </asp:CalendarExtender>
                                <asp:MaskedEditExtender ID="txtFromDate_MaskedEditExtender" runat="server" 
                                    Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtFromDate">
                                    </asp:MaskedEditExtender>
                </td>
            </tr>
            <tr>
                <td class="clstd">To Date:</td>
                <td class="clstd">

                    <asp:TextBox ID="txtToDate" runat="server" Width="80px" 
                                    style="text-align: left" CssClass="clstxtDate" AutoPostBack="True" 
                                    ></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" 
                                    Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtToDate">
                                    </asp:CalendarExtender>
                                <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" 
                                    Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtToDate">
                                    </asp:MaskedEditExtender>
                </td>
            </tr>
            <tr> 
              <td class="clstd">

                </td>
                 
                <td colspan="2" class="clstd">
                    <div style="border-style: solid; border-width: 1px;  height:70px; overflow:auto; width: 277px; margin-top: 0px;">
                    <asp:CheckBoxList ID="chkODScheme" runat="server">
                    </asp:CheckBoxList>
                    </div>
                </td>
              </tr>
            <tr>
                <td class="clstd">Scheme:</td>
                <td aria-multiline="True" aria-orientation="vertical" class="auto-style2">
                    <div style="border-style: solid; border-width: 1px; height:70px; overflow:auto; width: 277px; margin-top: 0px;"> 
                    <asp:CheckBoxList ID="chkLoanScheme" runat="server" Font-Bold="False">
                    </asp:CheckBoxList>
                        </div>
                    <asp:RadioButtonList ID="rblPriOrInt" runat="server" RepeatDirection="Horizontal" Font-Bold="False">
                        <asp:ListItem Value="P">Profit</asp:ListItem>
                        <asp:ListItem Value="I">Interest</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td class="clstd">&nbsp;</td>
                <td colspan="2" class="clstd">
                     <asp:Button ID="btnSelect" runat="server" Text="SelectAll" Width="70px" OnClick="btnSelect_Click" />
                    <asp:Button ID="btnOK" runat="server" Text="OK" Width="70px" OnClick="btnOK_Click" />
                    <asp:Button ID="btnClear" runat="server" Text="Clear" Width="70px" OnClick="btnClear_Click" />
                    <asp:Button ID="btnBack" runat="server" Text="Back" Width="70px" OnClick="btnBack_Click" />
                   <asp:ImageButton ID="imgxl0" runat="server" Height="24px" 
           ImageUrl="~/img/images.jpg" onclick="imgxl_Click" Width="57px" ImageAlign="AbsMiddle" />
                    <input onclick="PrintDiv();" type="button" value="Print" width="90" />
                </td>
                </tr>
    </table>
    </div>
        
        <asp:Panel ID="pnlGrid" runat="server">
        <table align="center">
            <tr>
                <td align="center">
                    <asp:Label ID="lblBank" runat="server" Font-Bold="True" Visible="False"></asp:Label>
                    <br />
                    <asp:Label ID="lblBranch" runat="server" Font-Bold="True" Visible="False"></asp:Label>
                    <br />
                    <asp:Label ID="lblHeading" runat="server" Font-Bold="True" Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:GridView ID="gvDCB" runat="server" OnRowDataBound="gvDCB_RowDataBound" OnSelectedIndexChanged="gvDCB_SelectedIndexChanged" Font-Bold="False">
                        <RowStyle Font-Size="Smaller" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
             </asp:Panel>
        <uc2:iisplFooter ID="iisplFooter1" runat="server" />
    </form>
</body>
</html>
