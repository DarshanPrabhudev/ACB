<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HousingLoanCertificate.aspx.cs" Inherits="Appl_HousingLoanCertificate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Loan Interest Received Certificate Display</title>
    <style type="text/css">

        .style2
        {
            text-decoration: underline;
            height: 18px;
            font-size: x-large;
        }
        .style4
        {
            width: 139px;
            text-align: left;
        }
        .style6
        {
            height: 119px;
        }
        </style>
    </head>
<body>
    <form id="form1" runat="server">
    <div align="center" style="width: 859px">    
       <asp:Panel ID="Panel1" runat="server" Width="641px">     
    
         <%--  <div align="center" style="width: 907px">--%>
               <table align="center" style="width: 591px; height: 468px" >
                   <tr>
                       <td colspan="2" style="text-align: center">
                           <asp:Label ID="lblForm1" runat="server" Font-Bold="True" Width="457px"></asp:Label>
                       </td>
                   </tr>
                   <tr>
                       <td colspan="2" align="center">
                           Branch:<asp:Label ID="lblBranch" runat="server" style="font-weight: 700"></asp:Label>
                       </td>
                   </tr>
                   <tr>
                       <td colspan="2">
                           &nbsp;</td>
                   </tr>
                   <tr>
                       <td align="left">
                           Ref:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                       </td>
                       <td style="text-align: right">
                           &nbsp;Date:&nbsp;
                           <asp:Label ID="lblSysDt" runat="server"></asp:Label>
                       </td>
                   </tr>
                   <tr>
                       <td align="center" class="style2" colspan="2">
                           <strong>C E R T I F I C A T E</strong></td>
                   </tr>
                   <tr>
                       <td colspan="2">
                           &nbsp;</td>
                   </tr>
                   <tr>
                       <td colspan="2" style="text-align: left">
                           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; This&nbsp; is&nbsp; to&nbsp; certify&nbsp; that&nbsp; Shri.&nbsp;<asp:Label 
                               ID="lblName" runat="server" 
                               style="font-weight: 700; text-decoration: underline"></asp:Label>
                           &nbsp; customer of
                           <br />
                           our&nbsp; Bank has&nbsp; availed&nbsp;
                           <asp:Label ID="lblLoanName" runat="server" style="font-weight: 700"></asp:Label>
                           &nbsp;&nbsp; from our Bank.&nbsp; He/She has paid towards<br /> Principal and Interest during 
                           the period from<asp:Label ID="lblFrmDt" runat="server" 
                               style="font-weight: 700; text-decoration: underline"></asp:Label>
                           &nbsp;to
                           <asp:Label ID="lblToDt" runat="server" 
                               style="font-weight: 700; text-decoration: underline"></asp:Label>
                           &nbsp; &nbsp; is as&nbsp; following.
                       </td>
                   </tr>
                   <tr>
                       <td align="center" colspan="2" style="text-align: center" class="style6">
                           <asp:GridView ID="gvLoanDetails" runat="server" align="center" 
                                Width="563px" onrowdatabound="gvLoanDetails_RowDataBound">
                           </asp:GridView>
                       </td>
                   </tr>
                   <tr>
                       <td align="center"  colspan="2" style="text-align: center">
                           &nbsp;</td>
                   </tr>
                   <tr>
                       <td class="style4" style="text-align: left">
                           <strongplace :</strong="">
                               <strong>PLACE&nbsp; : </strong>
                               <asp:Label ID="lblPlc" runat="server" style="font-weight: 700"></asp:Label>
                           </strongplace>
                       </td>
                       <td>
                           &nbsp;</td>
                   </tr>
                   <tr>
                       <td class="style4">
                           <strong>DATE&nbsp; :&nbsp;&nbsp; </strong>
                           <asp:Label ID="lblDate" runat="server" style="font-weight: 700"></asp:Label>
                       </td>
                       <td align="right">
                           Asst.Manager</td>
                   </tr>
               </table>
        <%--   </div>--%>
          </asp:Panel>

    </div>
    </form>
</body>
</html>
