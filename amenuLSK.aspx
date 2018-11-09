 <%@ Page Language="C#" AutoEventWireup="true" CodeFile="amenuLSK.aspx.cs" Inherits="amenuLSK" %>
<%@ Register src="ascx/BrName.ascx" tagname="BrName" tagprefix="uc1" %>
<%@ Register src="ascx/iisplFooter.ascx" tagname="iisplFooter" tagprefix="uc3" %>
<%@ Register src="ascx/menuFooter.ascx" tagname="menuFooter" tagprefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"> 
    <title>MENU</title>
     <script src="Scripts/Cwebsite.js" type="text/javascript"> </script>
        <script>    history.go(1)</script>
    <link href="Styles/menuUL.css" rel="stylesheet" type="text/css" />      
   
    <style type="text/css">
        .style3
        {
            height: 527px;
        }
        #form1
        {
            height: 560px;
        }
    </style>
   
    </head>
<body class="clsbody">
<%--<%  Session["rolePO"] = null;
    Session["rolePO"] = globalSession.checkRolesPO(Session["constring"].ToString(), Session["BranchLogin"].ToString(), "Transaction Passing Officer", Session["gblnSupervisor"].ToString(), Convert.ToInt32(Session["gUserId"]));               %>
--%>
<form id="form1" runat="server" > 
<div class="style3">
  <uc1:BrName ID="BrName1" runat="server" EnableTheming="True" 
        EnableViewState="True" />

    <%--<asp:HyperLink ID="hlEUNmsg" runat="server" ForeColor="#3333FF" Visible="False" >End user Notifications</asp:HyperLink>--%>


<ul class="clsUL">

<li class="clsCornerBtn"><a  href="#" class="clsAnchrtags"><span class="clsMenuHdrTxt">Masters</span><br />
    <%--<img align="middle" alt="" src="images/SN_placebo.gif" align="middle" 
        class="clsImg"  />--%></a>
        <ul >                                                         
            <li ><a  class="clsCornerdown2"  href="Appl/Master/ClientMaster.aspx">Client Master</a> </li>
            <li><a  class="clsCornerdown2" href="Appl/Master/ClientAddrModify.aspx">Client Addr Modify</a></li>  
          <%--  <li><a  class="lipadding" href="Appl/Share/memberAllot.aspx">Membership Allotment</a></li>                             
            <li><a  class="lipadding" href="Appl/Deposit/ChangeOfJointHolder.aspx">Change Of JointHolder </a></li>            
            <li><a  class="lipadding" href="Appl/Pigmy/PigmyAgentMaster.aspx">Pigmy Agent Master</a></li>                                
            <li><a  class="lipadding" href="Appl/Master/CertificateIssue.aspx">Certificate Issue</a></li>  --%>                                                                                      
            <li><a  class="clsCornerdown2" href="Appl/Master/smsBankingRegister.aspx">SmsBanking Register</a></li>  
          <li><a  class="clsCornerdown2" href="Appl/Master/SIGroupMaster.aspx">SI Group Master</a></li>
            <li><a  class="clsCornerdown2" href="Appl/Master/GroupEmployee.aspx">Group Employee</a></li>
            <li><a  class="clsCornerdown2" href="Appl/Master/frmAddifsccode.aspx">Add Ifsc</a></li>               
            <li><a  class="clsCornerdown2" href="Appl/Share/ClientMaster.aspx">CKYC</a></li>               
            <li><a  class="clsCornerdown2" href="millath/PassOfficerAuthority.aspx">Delegation of passing officer Role</a></li>               
           <%-- <li><a  class="lipadding"  href="Appl/Master/DayEndOpen.aspx">DayEnd Open</a></li> 
            <li><a  class="lipadding" href="Appl/DDPO/DDPOSchemeSetup.aspx">DDPO SchemeSetup</a></li> 
            <li><a  class="lipadding" href="Appl/DDPO/ComMaster.aspx">Comn Master</a></li> --%>                                                 

            <li><a  class="clsCornerdown2" href="#">Standing Instructions<img alt="Posting" src="images/play-button.png" style="height: 12px; width: 13px"/></a> 
            <ul>
            <li><a  class="clsCornerdown2" href="Appl/Master/StandingInstructions.aspx">Standing Instructions Creation</a></li>    
            <li><a  class="clsCornerdown2" href="Appl/Master/ExecuteSI.aspx"">Standing Instructions Execution</a></li>            
            </ul>
            </li> 
        </ul>
</li>
 <li class="clsCornerBtn"><a  href="#" class="clsAnchrtags"><span class="clsMenuHdrTxt">Applns<br />
     <%--<img  
         alt=""  src="images/Applimg.jpeg" class="clsImg" 
           />--%></span></a>
<ul class="clsUL">
<li><a  class="clsCornerdown" href="Appl/Deposit/DepScheme.aspx">Deposit Application</a></li> 
<li><a  class="clsCornerdown" href="Appl/Loan/loanApplication.aspx">Loan Application</a></li> 	
<%--<li><a  class="lipadding" href="Appl/Loan/ODApplication.aspx">OD Application</a></li>
<li><a  class="lipadding" href="Appl/Loan/DepLoanApplication.aspx">Deposit/Pigmy Loan Application</a></li> 
<li><a  class="lipadding" href="Appl/Pigmy/PigmyAccountOpening.aspx">Pigmy Account Opening</a></li>  	--%>
</ul>
</li>

<li  class="clsCornerBtn"><a  href="#" class="clsAnchrtags"><span class="clsMenuHdrTxt">Enquiry<br/><%--<img alt=""  
        src="images/enqimg.jpeg" class="clsImg"/>--%></span></a>
        <ul>
 <li><a  class="clsCornerdown" href="Appl/Master/GeneralEnq.aspx">General Enquiry</a></li>
 <%--<li><a  class="lipadding" href="Appl/Deposit/DepositEnq.aspx">Deposit Enquiry </a></li>
 <li><a  class="lipadding" href="Appl/Share/ShareEnquiry.aspx">Share Enquiry</a></li>
 <li><a  class="lipadding" href="Appl/Loan/loanEnq.aspx">Loan Enquiry </a></li>
 <li><a  class="lipadding" href="Appl/Loan/odEnq.aspx">OD Enquiry </a></li> 
<li><a  class="lipadding" href="Appl/DDPO/ddpoEnq.aspx">DDPO Enquiry</a></li>   --%>
</ul>
</li>

 <%--<li><a  class="lipadding" href="#"><span class="clsMenuHdrTxt">Deposit/Post</span><br /><img  align="middle" alt="" width="50px"  src="images/migrateUpdate.gif" width="40px" height="40px" align="middle" /></a>
<ul> 
 </ul>
 </li>
 </li>--%>
<li class="clsCornerBtn"><a  href="#" class="clsAnchrtags"><span class="clsMenuHdrTxt">Trans<br />&nbsp&nbsp<%--<img alt="" 
        src="images/Transimg.jpeg" class="clsImg"  />--%></span></a>
<ul>
<li><a  class="clsCornerdown2"  href="Appl/Transaction/selRnP.aspx">Receipts and Payments Transaction</a></li>  
<%--<li><a  class="lipadding"  href="Appl/DDPO/DDPOissue.aspx">DDPO Issue</a></li>--%>
<li ><a  class="clsCornerdown2"  href="Appl/Transaction/ChkBookReceipt.aspx">Cheque Book Receipt</a></li>     
<li><a  class="clsCornerdown2"  href="Appl/Transaction/ChequeIssue.aspx">Cheque Book Issue</a></li>
 <li><a  class="clsCornerdown2"  href="Appl/Transaction/OtherCreditTrans.aspx">Other Linked Credit Transaction</a></li>
 <li ><a  class="clsCornerdown2" href="Appl/Transaction/CashChest.aspx">Cash Chest</a></li>
 <li ><a  class="clsCornerdown2" href="Appl/Deposit/TermDepositTran.aspx">Term  Deposit Enquiry with Transaction</a></li>  
<%--<li><a  class="lipadding" href="Appl/Share/shareTransaction.aspx">Share Transaction</a></li>  
<li><a  class="lipadding"  href="Appl/Share/dividendTransaction.aspx">Dividend Transaction</a></li> 
<li><a  class="lipadding"  href="Appl/Pigmy/PigmyTransactiion2.aspx">Pigmy Transaction</a></li>  
<li><a  class="lipadding"  href="Appl/Transaction/LimitChangeFDTransfer.aspx">Limit Change FD Transfer</a></li>   	
<li><a  class="lipadding"  href="Appl/DDPO/ddpoCancel.aspx">DDPO Cancel</a></li>--%>

<%--<li><a  href="#"class="button">Postings<img alt="Posting" src="images/play-button.png" style="height: 12px; width: 13px"/></a>
    <ul >
        <li><a  class="lipadding"  href="Appl/Deposit/CapitalizeOfInterest.aspx">SB CompoundInterest posting</a></li>
        <li ><a  class="lipadding"  href="Appl/Deposit/CashCertificateInterestPosting.aspx">CashCertificateInterestPosting </a></li>
        <li ><a  class="lipadding"  href="Appl/Share/DividendCalculation.aspx">Dividend Calculation and Posting</a></li>
        <li ><a  class="lipadding"  href="Appl/Deposit/MaturedDepositPosting.aspx">MaturedDepositPosting </a></li>
        <li ><a  class="lipadding"  href="Appl/Deposit/RDinterestpostin.aspx">RDinterestposting</a></li> 
        <li ><a  class="lipadding"  href="Appl/Deposit/FDInterestPostingForIndivdlClient.aspx">FDIntPostingForIndivdlClient</a></li>                           
                             
    </ul>
</li> --%>
                 
                 
<%--<li><a  class="lipadding button" href="#">Closures<img alt="Posting" src="images/play-button.png" style="height: 12px; width: 13px"/></a>
    <ul> 
        <li><a  class="lipadding" href="Appl/Deposit/DepAccClosure.aspx">Dep Closure </a> </li> 
        <li><a  class="lipadding" href="Appl/Transaction/AccClosure.aspx">Acc Closure</a></li> 
        <li><a  class="lipadding" href="Appl/Loan/LoanAndOdAccClosure.aspx">Loan/Od AccClosure </a></li>
    </ul>
</li>--%>

<li><a  class="clsCornerdown2" href="#">Stop PayMents<img alt="Posting" src="images/play-button.png" style="height: 12px; width: 13px"/></a> 
<ul>
<li><a  class="clsCornerdown2" href="Appl/Transaction/AccChqStopPayment.aspx">AccChqStopPayment</a></li>
<li><a  class="clsCornerdown2" href="Appl/DDPO/DdpoStopPayment.aspx">DDPO StopPayment</a></li>    
</ul>
</li> 
</ul>  
</li>
 <%------------Passing Officer-----------%>   
      <%--
      if (Session["rolePO"] == "PO")
    {--%>
  <li class="clsCornerBtn"><a  href="#" class="clsAnchrtags"><span class="clsMenuHdrTxt">Authrsns<br />&nbsp&nbsp<%--<img alt="" 
        src="images/Transimg.jpeg" class="clsImg"  />--%></span></a> 
 <ul>
 <li><a  class="clsCornerdown2" href="Appl/Transaction/PassingOfficerCust.aspx">Passing Officer</a></li> 
<li><a  class="clsCornerdown2" href="Appl/Master/ClientAuthorize.aspx">Client Authorize</a></li>  
<li><a  class="clsCornerdown2" href="Appl/Transaction/Chequeauthorize.aspx">Cheque Book Authorize</a></li>
<%--<li><a  class="lipadding" href="Appl/Transaction/SignatureAuth.aspx">Signature Authorize</a></li>--%>
<li><a  class="clsCornerdown2" href="Appl/Transaction/StopPaymentAuthView.aspx">Stop Payment Authorize</a></li>	   
 </ul>
 </li>   
 <%--}--%>
 <%-------------------------------%>

<li class="clsCornerBtn"><a  href="#" class="clsAnchrtags"><span class="clsMenuHdrTxt">Loan/OD</span><br /><%--<img   alt=""  
        src="images/Loans.jpeg"  align="middle" class="clsImg" />--%></a>
<ul>
<li><a  class="clsCornerdown2" href="Appl/Loan/LoanNoticeDetails.aspx">LoanNoticeDetails </a></li> 
<li><a  class="clsCornerdown2" href="Appl/Loan/DocumentReceivedList.aspx">DocumentReceivedList </a></li> 
<li><a  class="clsCornerdown2" href="Appl/Loan/InsuranceDetails.aspx">InsuranceDetails </a></li>  
<li><a  class="clsCornerdown2" href="Appl/Loan/LoanAndODParkedExpenses.aspx">LoanAndODParkedExpenses </a></li>
<li><a  class="clsCornerdown2" href="Appl/Loan/DepLoanApplication.aspx">Deposit Loan Appl</a></li> 
<%--<li><a  class="lipadding" href="Appl/Loan/LoanODParkModifications.aspx">LoanODParkModifications </a></li>    
<li><a  class="lipadding" href="Appl/Loan/MarkAccountABNorEP.aspx">MarkAccountABNorEP </a></li>  
<li><a  class="lipadding" href="Appl/Loan/ODGuarantorUpadate.aspx">ODGuarantorUpadate </a></li>
<li><a  class="lipadding" href="Appl/Loan/OdRenewal.aspx">OdRenewal </a></li>
<li><a  class="lipadding" href="Appl/Loan/OdSecurity.aspx">OdSecurity </a></li> --%>
 </ul>
 </li>

<li class="clsCornerBtn"><a  href="#" class="clsAnchrtags"><span class="clsMenuHdrTxt">Postings</span><br /><%--<img   alt=""  
        src="images/Loans.jpeg"  align="middle" class="clsImg" />--%></a>
<ul>
        <li><a  class="clsCornerdown2"  href="Appl/Deposit/CapitalizeOfInterest.aspx">SB CompoundInterest posting</a></li>
        <li ><a  class="clsCornerdown2"  href="Appl/Deposit/DepositAutoRenewalWithoutLien.aspx">DepositAutoRenewal </a></li>	
      <%--  <li ><a  class="lipadding"  href="Appl/Deposit/CashCertificateInterestPosting.aspx">CashCertificateInterestPosting </a></li>--%>
        <%--<li ><a  class="lipadding"  href="Appl/Share/DividendCalculation.aspx">Dividend Calculation and Posting</a></li>--%>
       <%-- <li ><a  class="lipadding"  href="Appl/Deposit/MaturedDepositPosting.aspx">MaturedDepositPosting </a></li>--%>
        <%--<li ><a  class="lipadding"  href="Appl/Deposit/RDinterestpostin.aspx">RDinterestposting</a></li> --%>
        <li ><a  class="clsCornerdown2"  href="Appl/Deposit/FDInterestPostingForIndivdlClient.aspx">FDIntPostingForIndivdlClient</a></li>                           
                             
    </ul>
</li> 
 <%-----------------------------Deposit End----------------%>     

<%--<li class="clsCornerBtn"><a  href="#" class="clsAnchrtags"><span class="clsMenuHdrTxt">6 Clearing/BDBC </span><br />&nbsp&nbsp&nbsp--%><%--<img  
         alt=""  src="images/clearing.jpeg" class="clsImg"/>--%></a>
   <%--<ul>
   <li><a  class="clsCornerdown2" href="debitReqPendingappr.aspx">6.1 DebitManDate Approval</a></li>
 </ul>
 </li> --%> 
<%-- ------------------------------------------------%> 
 
 <li class="clsCornerBtn"><a  href="#" class="clsAnchrtags"><span class="clsMenuHdrTxt">Pigmy</span><br/>
     <%--<img alt=""  src="images/pigmi.jpeg" class="clsImg"/>--%></a>
 <ul>
 <li><a  class="clsCornerdown2" href="Appl/Pigmy/PIGMYAGENT.aspx">Pigmy Account Opening</a></li>
<li><a  class="clsCornerdown2" href="Appl/Pigmy/AccountSearch.aspx">AccountSearch</a></li>
<li><a  class="clsCornerdown2" href="Appl/Pigmy/DepositClosedOpen.aspx">PigmyDepositClosedOpen</a></li> 
<li><a  class="clsCornerdown2" href="Appl/Pigmy/PigmyAgentLoanMarking.aspx">PigmyAgentLoanMarking</a></li>  
<li><a  class="clsCornerdown2" href="Appl/Pigmy/PigmyLienandRemove.aspx">PigmyLienandRemove</a></li>
<li><a  class="clsCornerdown2" href="Appl/Pigmy/PIGMYTRANSELECTCRE.aspx">Pigmy Trasnactions</a></li>  
<li><a  class="clsCornerdown2" href="Appl/Pigmy/PigmyRemittanceManual.aspx">PigmyRemittanceManual</a></li>      
 </ul>
 </li>
 
 <%--------------------------Pigmy End-------------------%>
 <li class="clsCornerBtn"><a  href="#" class="clsAnchrtags"><span class="clsMenuHdrTxt">DDPO</span><br/>
     <%--<img alt=""  src="images/pigmi.jpeg" class="clsImg"/>--%></a>
 <ul>    
   <li><a  class="clsCornerdown" href="Appl/DDPO/ComMaster.aspx">Comm.setup</a></li>    
   <li><a  class="clsCornerdown" href="Appl/DDPO/DDPORecipt.aspx">DDPO Receipt</a></li>       
  <li><a  class="clsCornerdown" href="Appl/DDPO/DDPOissue.aspx">DDPO Issue</a></li>    
  <li><a  class="clsCornerdown" href="Appl/DDPO/ddpoEnq.aspx">DDPO Enq</a></li>    
  <li><a  class="clsCornerdown" href="Appl/DDPO/ddpoCancel.aspx">DDPO Cancellation</a></li>    
  <li><a  class="clsCornerdown" href="Appl/DDPO/DdpoStopPayment.aspx">DDPO StopPayment</a></li>     
 </ul>
 </li>
 <%-----------------------DDPO End-----------------
 
<li class="clsCornerBtn"><a  href="#" class="clsAnchrtags" onclick='window.location.href="rmenu.aspx";return false;' ><span class="clsMenuHdrTxt">10 Reports</span><br/>
<img alt=""  src="images/SN_placebo.gif" class="clsImg"/></a>   
 </li>  
--%>    
    <li class="clsCornerBtn"><a  href="#" class="clsAnchrtags" onclick='window.location.href="rmenu.aspx";return false;'><span class="clsMenuHdrTxt">Reports</span><br/>
     <%--<img alt=""  src="images/pigmi.jpeg" class="clsImg"/>--%></a>
 </ul>   
    
 </div> 
</form>
<uc2:menuFooter ID="menuFooter6" runat="server" />
</body>
</html>
