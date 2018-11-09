<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DepositIntPayableReport.aspx.cs" Inherits="DepositIntPayableReport" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Deposit Int Payable Report</title>
    <link href="Styles/cwebsite.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">


        function PrintDiv() {
            var panel = document.getElementById('Panel1');
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
    <div align="center">
    
        <table class="clstable">
            <tr>
                <td class="clsrpthdr" colspan="2">Deposit Int Payable Report</td>
            </tr>
            <tr>
                <td class="clsrpthdr" colspan="2">(Broken Period Interest Payable )</td>
            </tr>
            <tr>
                <td class="clstd">Branch</td>
                <td class="clstd">
                    <asp:DropDownList ID="ddlBranch" runat="server" Width="110px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="clstd">As on date</td>
                <td class="clstd">
                    <asp:TextBox ID="txtAsDate" runat="server" Width="100px"></asp:TextBox>
                    <asp:CalendarExtender ID="txtAsDate_CalendarExtender" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtAsDate"></asp:CalendarExtender>
                    <asp:MaskedEditExtender ID="txtAsDate_MaskedEditExtender" runat="server" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtAsDate" UserDateFormat="DayMonthYear"></asp:MaskedEditExtender>
                </td>
            </tr>
            <tr>
                <td class="clstd">Scheme</td>
                <td class="clstd" align="left">
                    <div style="border-style: solid; border-width: 1px; height:150px; overflow:auto; width: 278px; margin-top: 0px; background-color: #CAEBEE;" align="left">
                        <asp:CheckBoxList ID="chkList" runat="server" ForeColor="Black">
                        </asp:CheckBoxList>
                    </div>                
                </td>
            </tr>
            <tr>
                <td class="clstd">&nbsp;</td>
                <td class="clstd">&nbsp;</td>
            </tr>
            <tr>
                <td class="clstd">&nbsp;</td>
                <td class="clstd">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" Width="70px" 
                        onclick="btnSubmit_Click" />
                    <asp:Button ID="btnBack" runat="server" Text="Back" Width="70px" OnClick="btnBack_Click" />
                    <asp:Button ID="btnClear" runat="server" Text="Clear" Width="70px" OnClick="btnClear_Click" />
                    <input onclick="PrintDiv();" type="button" value="Print" width="90" /><asp:ImageButton ID="imgxl0" runat="server" Height="24px" 
                                        ImageUrl="~/img/images.jpg" onclick="imgxl_Click" Width="57px" />
                </td>
            </tr>
        </table>
    
    </div>
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
        <asp:Panel ID="Panel1" runat="server">
            <table align="center" class="style1">
                <tr>
                    <td align="center"> 
                        <%--<div align="center" style="height:178px; overflow:auto; width: 704px;">--%>
                            <br />
                        <asp:Label ID="lblBank" runat="server" Font-Bold="True" Visible="False"></asp:Label>
                        <br />
                        <asp:Label ID="lblBranch" runat="server" Font-Bold="True" Visible="False"></asp:Label>
                        <br />
                        <br />                        
                        <asp:GridView ID="gvRptDisp" runat="server" Caption="Deposit Int Payable Report(Broken Period Interest Payable )" CaptionAlign="Top" Font-Names="Cambria" ForeColor="Black" onrowdatabound="gvRptDisp_RowDataBound">
                        </asp:GridView>
                            <br />
                            <br />
                            <%--</div>--%>
                        </td>
                </tr>
            </table>
        </asp:Panel>
    </form>
</body>
</html>
