<%@ Page Language="C#" AutoEventWireup="true" CodeFile="cdis.aspx.cs" Inherits="cdis"  UICulture="hi-IN" Culture="hi-IN"  %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
       <link href="cwebsite.css" rel="stylesheet" type="text/css"  media="print"/>
       <script type="text/javascript" src="Scripts/Esc.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="style1" align="center">
        <table class="style2">
            <tr>
                <td class="style3" align="center">
                    <asp:Label ID="lblbank" runat="server"></asp:Label>
                            <br />
                    <asp:Label ID="lblbranch" runat="server"></asp:Label>
                            <br />
                    <asp:Label ID="lblrep" runat="server"></asp:Label>
                            <br />
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <asp:Panel ID="Panel1" runat="server">
                    <table style="width: 176px" align="center">
                    <tr>
                    <td align="center">
                    
                        <b>
                        <asp:Label ID="lblbank0" runat="server"></asp:Label>
                        </b>
                    
                    </td>
                    </tr>
                        <tr>
                            <td align="center">
                                <b>
                                <asp:Label ID="lblbranch0" runat="server"></asp:Label>
                                </b>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <b>
                                <asp:Label ID="lblrep0" runat="server"></asp:Label>
                                </b>
                            </td>
                        </tr>
                    </table>
                   
                        <b>
                        <asp:Label ID="lblgln" runat="server" style="font-weight: 700" Text="GL Name:" 
                            Visible="False"></asp:Label>
                        <b>
                        <asp:Label ID="lblgl" runat="server" style="text-align: left; font-weight: 700" 
                            Text="Label" Visible="False"></asp:Label>
                        </b></b>
                        <br />
                        <asp:GridView ID="gv" runat="server" onrowdatabound="gv_RowDataBound">
                            <RowStyle Wrap="False" />
                            <HeaderStyle Wrap="True" />
                        </asp:GridView>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td style="text-align:center">
       <asp:ImageButton ID="imgxl" runat="server" Height="24px" 
           ImageUrl="~/img/images.jpg" onclick="imgxl_Click" Width="57px" />
                    <input type="button" value ="Back" id="goBack"
                    onclick="window.history.back();return true;"
                    style="font-weight: 700;color: #003399">
                    
                    </td>
            </tr>
        </table>
        <br />
        <br />
    </div>
    

    </form>
</body>
</html>
