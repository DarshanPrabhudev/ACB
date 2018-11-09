<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form15HReportAccountWise.aspx.cs" Inherits="Form15HReportAccountWise" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>FORM15H REPORT</title>
   
    <link href="cwebsite.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
        	  	
        	text-align:left;
        }
        .style3
        {
            height: 25px;
            font-weight: bold;
            text-align: center;
        }
        .style4
        {
        	text-align:right;
        }
        .style5
        {
        	text-align:center;
        	background-color:AntiqueWhite;
        }
        .style6
        {
        	text-align:right;
        	font-weight:bold;
        	font-style:italic;
        }
        .style7
        {
        	text-align:center;
        }
        
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <asp:Panel ID="Panel1" runat="server">
            <asp:Panel ID="Panel2" runat="server">

                <table class="style1">
                    <tr>
                     <td class="style3">
                         FORM NO. 15H</td>
                 </tr>
                 <tr>
                        <td class="style7">
                            [See section 197A(1C),197A(1A) and rule 29C(1A)]</td>
                    </tr>
                 <tr>
                     <td class="style7">
                         Declaration under section 197A(1C) of the Income-tax Act, 1961 to be made by an 
                          individual who is of the age of sixty years or more</td>
                 </tr>
                 <tr>
                     <td class="style7">
                         claiming certain receipts without deduction of tax.</td>
                 </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style3">
                            PART - I</td>
                    </tr>
                </table>

            </asp:Panel>

            <asp:Panel ID="Panel3" runat="server">
            <table class="style1">
                <tr>
                    <td rowspan="3" style="border-style: dotted; border-width: thin" class="style2" 
                        valign="top" colspan="2">
                        1] Name of Assessee (Declareant) : 
                        <br />
                        &nbsp;<asp:Label ID="lblName" runat="server" 
                            Font-Bold="True"></asp:Label>
                        <br />
                        &nbsp;</td>
                    <td style="border-style: dotted; border-width: thin" class="style2" 
                        valign="top" colspan="4">
                        2] PAN : 
                        <asp:Label ID="lblPan" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="border-style: dotted; border-width: thin" class="style2" 
                        valign="top" colspan="4">
                        3] Age :
                        <asp:Label ID="lblAge" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="border-style: dotted; border-width: thin" class="style2" 
                        valign="top" colspan="4">
                        4]Assessment Year :
                        <asp:Label ID="lblAssesmentyr" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="border-style: dotted; border-width: thin" class="style2" 
                        valign="top">
                        5] Flat / Door / Block No. :<br /> &nbsp;<asp:Label ID="lblAddress1" runat="server" 
                            Font-Bold="True"></asp:Label>
                        <br />
                    </td>
                    <td class="style2" style="border-style: dotted; border-width: thin" 
                        valign="top">
                        6] Name of Premises :</td>
                    <td class="style2" colspan="2" style="border-style: dotted; border-width: thin" 
                        valign="top">
                        7]Assessed in which
                        <br />
                        Ward / Circle</td>
                    <td class="style2" colspan="2" style="border-style: dotted; border-width: thin" 
                        valign="top">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
                </tr>
                <tr>
                    <td style="border-style: dotted; border-width: thin" class="style2" 
                        valign="top" rowspan="3">
                        8] Road / Street / Lane :
                        <br />
                        &nbsp;<asp:Label ID="lblAddress2" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                    <td class="style2" rowspan="3" style="border-style: dotted; border-width: thin" 
                        valign="top">
                        9] Area / Locality :
                        <br />
                        &nbsp;<asp:Label ID="lblArea" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                    <td class="style2" colspan="4" style="border-style: dotted; border-width: thin" 
                        valign="top">
                        10] AO Code (whom assessed last time) :</td>
                </tr>
                <tr>
                    <td style="border-style: dotted; border-width: thin" 
                        valign="top">
                        Area<br /> Code</td>
                    <td style="border-style: dotted; border-width: thin" 
                        valign="top">
                        AO Type</td>
                    <td style="border-style: dotted; border-width: thin" 
                        valign="top">
                        Range<br /> Code</td>
                    <td style="border-style: dotted; border-width: thin" 
                        valign="top">
                        AO NO.</td>
                </tr>
                <tr>
                    <td class="style2" style="border-style: dotted; border-width: thin" 
                        valign="top">
                        &nbsp;</td>
                    <td class="style2" style="border-style: dotted; border-width: thin" 
                        valign="top">
                        &nbsp;</td>
                    <td class="style2" style="border-style: dotted; border-width: thin" 
                        valign="top">
                        &nbsp;</td>
                    <td class="style2" style="border-style: dotted; border-width: thin" 
                        valign="top">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="border-style: dotted; border-width: thin" class="style2" 
                        valign="top" rowspan="2">
                        11] Town / City / District :
                        <br />
                        &nbsp;<asp:Label ID="lblAddress3" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                    <td class="style2" style="border-style: dotted; border-width: thin" 
                        valign="top">
                        12] State :
                        <asp:Label ID="lblState" runat="server" Font-Bold="True" Text="KARNATAKA"></asp:Label>
                    </td>
                    <td class="style2" colspan="2" rowspan="2" 
                        style="border-style: dotted; border-width: thin" valign="top">
                        14]Last Assessment Year in<br /> which assessed :</td>
                    <td class="style2" colspan="2" rowspan="2" 
                        style="border-style: dotted; border-width: thin" valign="top">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style2" style="border-style: dotted; border-width: thin" 
                        valign="top">
                        13] PIN :
                        <asp:Label ID="lblPin" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="border-style: dotted; border-width: thin" class="style2" 
                        valign="top">
                        15] Email :
                        <br />
                        <asp:Label ID="lblEmail" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                    <td class="style2" style="border-style: dotted; border-width: thin" 
                        valign="top">
                        16] Telephone / Mobile No :
                        <br />
                        <asp:Label ID="lblPhoneNo" runat="server" Font-Bold="True"></asp:Label>
                        <br />
                    </td>
                    <td class="style2" colspan="2" style="border-style: dotted; border-width: thin" 
                        valign="top">
                        17] Present Ward / Circle :</td>
                    <td class="style2" colspan="2" style="border-style: dotted; border-width: thin" 
                        valign="top">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2" style="border-style: dotted; border-width: thin" class="style2" 
                        valign="top">
                        18] Name of Business / Occupation :
                        <asp:Label ID="lblOccupation" runat="server"></asp:Label>
                    </td>
                    <td class="style2" colspan="4" style="border-style: dotted; border-width: thin" 
                        valign="top">
                        19] Present AO Code (if not same as above):</td>
                </tr>
                <tr>
                    <td colspan="2" style="border-style: dotted; border-width: thin" class="style2" 
                        valign="top" rowspan="2">
                        20] Jurisdictional Chief Comm. of Income Tax or Comm. of Income Tax<br /> (if 
                        not assessed to income tax earlier) :</td>
                    <td style="border-style: dotted; border-width: thin" 
                        valign="top">
                        Area<br /> Code</td>
                    <td style="border-style: dotted; border-width: thin" 
                        valign="top">
                        AO Type</td>
                    <td style="border-style: dotted; border-width: thin" 
                        valign="top">
                        Range<br /> Code</td>
                    <td style="border-style: dotted; border-width: thin" 
                        valign="top">
                        AO No.</td>
                </tr>
                <tr>
                    <td class="style2" style="border-style: dotted; border-width: thin" 
                        valign="top">
                        &nbsp;</td>
                    <td class="style2" style="border-style: dotted; border-width: thin" 
                        valign="top">
                        &nbsp;</td>
                    <td class="style2" style="border-style: dotted; border-width: thin" 
                        valign="top">
                        &nbsp;</td>
                    <td class="style2" style="border-style: dotted; border-width: thin" 
                        valign="top">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3" style="border-style: dotted; border-width: thin" class="style2" 
                        valign="top">
                        21] Estimated total income from the sources mentioned below:<br /> </td>
                    <td class="style2" colspan="3" style="border-style: dotted; border-width: thin" 
                        valign="top">
                        (Please tick the relevant box)</td>
                </tr>
                <tr>
                    <td colspan="3" style="border-style: dotted; border-width: thin" class="style4"
                        valign="top">
                        Interest on sums referred to in Schedule - III</td>
                    <td class="style2" colspan="3" style="border-style: dotted; border-width: thin" 
                        valign="top">
                        &nbsp;<asp:CheckBox ID="CheckBox3" runat="server" Checked="True" />
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3" style="border-style: dotted; border-width: thin" class="style2" 
                        valign="top">
                        22]Estimated total income of the previous year in income mentioned in col - 21 
                        to be included :</td>
                    <td class="style2" colspan="3" style="border-style: dotted; border-width: thin" 
                        valign="top">
                        <asp:TextBox ID="txtIncome" runat="server" BackColor="#66CCFF" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" style="border-style: dotted; border-width: thin" class="style2" 
                        valign="top">
                        23] Details of investments in respect of which the declaration is being made :</td>
                </tr>
                <tr>
                    <td class="style2" colspan="6" style="border-style: dotted; border-width: thin" 
                        valign="top">
                        Client Code :
                        <asp:Label ID="lblClientCode" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
            </table>

            </asp:Panel>
        <asp:Panel ID="Panel6" runat="server">

            <table class="style1">
                <tr>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style3">
                        SCHEDULE - III</td>
                </tr>
                <tr>
                    <td class="style7">
                        (Details of sums given by the declarant on interest)</td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:GridView ID="gvFormPrinting" runat="server" 
                            onrowdatabound="gvFormPrinting_RowDataBound">
                            <HeaderStyle BackColor="#FFCCCC" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>

            </asp:Panel>

            
            

            <asp:Panel ID="Panel9" runat="server">
            <table class="style1">
                <tr>
                    <td class="style4">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style4">
                        *_____________________________________</td>
                </tr>
                <tr>
                    <td class="style6">
                        Signature of the Declarant&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
                </tr>
                <tr>
                    <td class="style3">
                        Declaration / Verification</td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style2">
                        *I/We&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lblName2" runat="server" Font-Bold="True"></asp:Label>
                        &nbsp;&nbsp; do hereby declare that I am resident in India</td>
                </tr>
                <tr>
                    <td class="style2">
                        within the meaning of section 6 of the Income-tax Act, 1961. I also, hereby 
                        declare that to the best of my knowledge and belief </td>
                </tr>
                <tr>
                    <td class="style2">
                        what is stated above is correct, complete and is truly stated and that the 
                        incomes referred to in this form are not includible in the</td>
                </tr>
                <tr>
                    <td class="style2">
                        total income of any other person u/s 60 to 64 of the Income-tax Act, 1961. I 
                        further , declare that the tax my estimated total</td>
                </tr>
                <tr>
                    <td class="style2">
                        income, including *income /&nbsp; incomes referred to in column 21 computed in 
                        accordance with the provisions of the Income-tax Act,</td>
                </tr>
                <tr>
                    <td class="style2">
                        1961, for the previous year ending on 31.03.<asp:Label ID="lblPYear" 
                            runat="server"></asp:Label>
                        &nbsp; relevant to the relevant to the assessment year
                        <asp:Label ID="lblAYear" runat="server"></asp:Label>
                        &nbsp;will be nil.</td>
                </tr>
                <tr>
                    <td class="style6">
                        *____________________________________</td>
                </tr>
                <tr>
                    <td class="style6">
                        Signature of the Declarant&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
                </tr>
                <tr>
                    <td class="style2">
                        Place :
                        <asp:Label ID="lblPlace1" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        Date :
                        <asp:Label ID="lblCdate" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>

            </asp:Panel>
            <asp:Panel ID="Panel10" runat="server">
            <table class="style1">
                <tr>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style3">
                        PART - II</td>
                </tr>
                <tr>
                    <td class="style7">
                        [For use by the person to whom thw declaration is furnished]</td>
                </tr>
            </table>

            </asp:Panel>
            <asp:Panel ID="Panel11" runat="server">

                       <table class="style1">
                <tr>
                    <td style="border-style: dotted; border-width: thin" class="style2" colspan="2">
                        &nbsp; 1]Name of the person responsible for paying the income referred to in column 
                        21 of&nbsp; Part I :
                        <br />
                        <asp:Label ID="lblBankName" runat="server" Font-Bold="True"></asp:Label>
                        <br />
                    </td>
                    <td class="style2" style="border-style: dotted; border-width: thin" 
                        valign="top" colspan="2">
                        &nbsp; 2]PAN of the person indicated in Column 1 of Part II :<br />
                        <asp:Label ID="lblBPan" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="border-style: dotted; border-width: thin" class="style2" colspan="2" valign="top">
                        &nbsp; 3]Complete Address :<br /> &nbsp;<asp:Label ID="lblBAddress1" runat="server" 
                            Font-Bold="True"></asp:Label>
                        <br />
                        &nbsp;<asp:Label ID="lblBAddress2" runat="server" Font-Bold="True"></asp:Label>
                        <br />
                        &nbsp;<asp:Label ID="lblBAddress3" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                    <td class="style2" colspan="2" style="border-style: dotted; border-width: thin" 
                        valign="top">
                        &nbsp;4]&nbsp;TAN of the person indicated in Column 1 of Part II :<br />
                        <asp:Label ID="lblTan" runat="server" Font-Bold="True"></asp:Label>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="border-style: dotted; border-width: thin" class="style2" 
                        valign="top">
                        &nbsp;5] Email :
                        <br />
                        <asp:Label ID="lblBEmail" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                    <td class="style2" style="border-style: dotted; border-width: thin" 
                        valign="top">
                        &nbsp; 6] Telephone / Mobile No :
                        <asp:Label ID="lblBPhoneNo" runat="server" Font-Bold="True"></asp:Label>
                        <br />
                    </td>
                    <td class="style2" colspan="2" style="border-style: dotted; border-width: thin" 
                        valign="top">
                        &nbsp; 7] Status :
                        <br />
                        <asp:Label ID="lblStatus" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="border-style: dotted; border-width: thin" class="style2" 
                        valign="top">
                        &nbsp; 8] Date on which Declaration is<br /> Furnished (dd/mm/yyyy) :</td>
                    <td class="style2" style="border-style: dotted; border-width: thin" 
                        valign="top">
                        &nbsp;9] Period in respect of which the<br /> dividend has been declared or the<br /> 
                        income has been paid / credited :</td>
                    <td class="style2" style="border-style: dotted; border-width: thin" 
                        valign="top">
                        &nbsp;10] Amount of income paid :&nbsp;</td>
                    <td class="style2" style="border-style: dotted; border-width: thin" 
                        valign="top">
                        11] Date on which the income<br /> has been paid / credited<br /> (dd/mm/yyyy) :</td>
                </tr>
                <tr>
                    <td style="border-style: dotted; border-width: thin" class="style2" 
                        valign="top">
                        <asp:Label ID="lblBDate" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                    <td class="style2" style="border-style: dotted; border-width: thin" 
                        valign="top">
                        <asp:Label ID="lblPeriod" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                    <td class="style2" style="border-style: dotted; border-width: thin" 
                        valign="top">
                        <asp:Label ID="lblBAmount" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                    <td class="style2" style="border-style: dotted; border-width: thin" 
                        valign="top">
                        <asp:Label ID="lblBDate2" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="border-style: dotted; border-width: thin" class="style2" colspan="2" 
                        valign="top">
                        &nbsp; 12] Date of declaration, ditribution or payment of dividend/<br /> &nbsp;withdrawal 
                        under the National Savings Scheme(dd/mm//yyyy) :<br /> 
                        <asp:Label ID="lblBdate3" runat="server" Font-Bold="True"></asp:Label>
                        <br /> </td>
                    <td class="style2" colspan="2" style="border-style: dotted; border-width: thin" 
                        valign="top">
                        &nbsp; 13] Account Number of National Saving Scheme from which withdrawal has been 
                        made:</td>
                </tr>
            </table>

            
            </asp:Panel>
            <asp:Panel ID="Panel12" runat="server">

            
            <table class="style1">
                <tr>
                    <td class="style2">
                        Forwarded to the chief Commisioner or Commisioner of Income-tax&nbsp;
                        <asp:Label ID="lblPlace" runat="server" Font-Bold="True">TUMAKURU</asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style6">
                        ____________________________________________________________</td>
                </tr>
                <tr>
                    <td class="style6">
                        Signature of the person responsible for paying the&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <br />
                        income referred to in Column 21 of Part I&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
                </tr>
                <tr>
                    <td class="style2">
                        Place :
                        <asp:Label ID="lblBAddress33" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        Date :&nbsp;
                        <asp:Label ID="lblBDate4" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        &nbsp;</td>
                </tr>
                
            </table>
            </asp:Panel>
        </asp:Panel>
    

        <table>
            <tr>
                <td class="clstd">
                    <asp:ImageButton ID="img" runat="server" Height="24px" 
                        ImageUrl="~/img/images.jpg" Width="57px" onclick="img_Click" />
                </td>
                <td class="clstd">
                    <asp:Button ID="btnBack" runat="server" Text="Back" Width="57px" Height="24px" 
                        onclick="btnBack_Click"/>
                </td>
                
            </tr>
           
        </table>
    

    </div>
    </form>
</body>
</html>
