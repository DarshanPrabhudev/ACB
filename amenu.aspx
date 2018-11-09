<%@ Page Language="C#" AutoEventWireup="true" CodeFile="amenu.aspx.cs" Inherits="amenu" %>

<%@ Register Src="ascx/BrName.ascx" TagName="BrName" TagPrefix="uc1" %>
<%@ Register Src="ascx/menuFooter.ascx" TagName="menuFooter" TagPrefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Navigation Menu</title>

    <script src="Scripts/Cwebsite.js" type="text/javascript">   </script>
    <script>history.go(1)</script>
    <link href="~/Styles/styles.css" rel="Stylesheet" type="text/css" />
    <script src="Scripts/jquery.min.js"></script>
    <script src="Scripts/bootstrap-submenu.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
    <script>
        (function ($) {
            $(document).ready(function () {
                $('ul.dropdown-menu [data-toggle=dropdown]').on('click', function (event) {
                    event.preventDefault();
                    event.stopPropagation();
                    $(this).parent().siblings().removeClass('open');
                    $(this).parent().toggleClass('open');
                });
            });
        })(jQuery);
    </script>

    <style type="text/css">
        .style4 {
            text-align: center;
            color: #0066FF;
            font-family: Nyala;
            font-size: large;
        }
    </style>


</head>
<body class="bg-warning">
    <form id="form1" runat="server">
        <uc1:BrName ID="BrName1" runat="server" EnableTheming="True"
            EnableViewState="True" />
        <div class="container text-primary">
            <h4 class="style4"><strong>Application Menu</strong></h4>
        </div>
        <nav class="navbar navbar-inverse">
            <ul class="nav navbar-nav">
					<li class="dropdown"><a class="dropdown-toggle" data-toggle="dropdown" href="#"><font color="white"><strong>Admin</strong><span class="caret"></span></font></a>
                        <ul class="dropdown-menu">
                            <li><a href="Appl/Admin/GLMaster.aspx">General Ledger</a></li>
							<li><a href="Appl/metagentool.aspx">XML Gen Tool</a></li>
                        </ul>
                    </li>
                    <li class="dropdown"><a class="dropdown-toggle" data-toggle="dropdown" href="#"><font color="white"><strong>Masters</strong><span class="caret"></span></font></a>
                        <ul class="dropdown-menu">                                                                                 
                            <li><a tabindex="-1" href="Appl/Master/ClientMaster.aspx">Client Master</a> </li>
                            <li><a tabindex="-1" href="Appl/Master/ClientAddrModify.aspx">Client Addr Modify</a></li>  
							<li><a tabindex="-1" href="Appl/Master/ChangePassword.aspx">Change of Password</a></li>  
                           <li><a tabindex="-1" href="Appl/Master/smsBankingRegister.aspx">SMS Banking Register</a></li>
                            <%--<li><a tabindex="-1" href="Appl/Master/SIGroupMaster.aspx">SI Group Master</a></li>
                            <li><a tabindex="-1" href="Appl/Master/GroupEmployee.aspx">Group Employee</a></li>							
                            <li><a tabindex="-1" href="Appl/Master/frmAddifsccode.aspx">ADD IFSC</a></li>  --%>							
                            <li><a tabindex="-1" href="Appl/Master/DayEndOpen.aspx">Day End Open</a></li>
							<li><a href="Appl/Master/ClntDtlsDisp.aspx">Signature &amp; Photo Scan</a></li>     
                            <li class="dropdown dropdown-submenu"><a href="#" class="dropdown-toggle" data-toggle="dropdown">Standing Instructions</a>
                            <ul class="dropdown-menu">
                                <li><a tabindex="-1" href="Appl/Master/StandingInstructions.aspx">Standing Instructions Creation</a></li>    
                                <li><a tabindex="-1" href="Appl/Master/ExecuteSI.aspx"">Standing Instructions Execution</a></li>            
                            </ul>
                            </li>                               
                        </ul>
                    </li>

                    <li class="dropdown"><a class="dropdown-toggle" data-toggle="dropdown" href="#"><font color="white"><strong>Appl</strong><span class="caret"></span></font></a>
                        <ul class="dropdown-menu">
                            <li><a href="Appl/Deposit/DepScheme.aspx">Deposit Application</a></li>
							<li><a href="Appl/Deposit/ChangeOfJointHolder.aspx">ChangeOfJointHolder</a></li>
                            <li><a href="Appl/Loan/loanApplication.aspx">Loan Application</a></li>
                            <li><a href="Appl/Deposit/DepAccRenewal.aspx">Deposit Account Renewal</a></li>
                            <li><a href="Appl/Deposit/DepositAutoRenewalWithoutLien.aspx">Deposit Auto Renewal</a></li>
							<li><a href="Appl/Deposit/JointHolderMarkingforTDS.aspx">Deposit Tax Payer Marking</a></li>
                        </ul>
                    </li>

                    <li class="dropdown"><a class="dropdown-toggle" data-toggle="dropdown" href="#"><font color="white"><strong>Enquiry</strong><span class="caret"></span></font></a>
                        <ul class="dropdown-menu">
                            <li><a href="Appl/Master/GeneralEnq.aspx">General Enquiry</a></li>
							 <li><a href="BASAVA/f7.aspx">Personal Ledger</a></li>
							 <li><a href="Appl/Loan/loanEnq.aspx">Loan Enquiry </a></li>
							 <li><a href="Appl/Loan/loanGeneralEnq.aspx">Loan General Enquiry </a></li>
							 <li><a href="Appl/Deposit/GeneralDepositEnquiry.aspx">Deposit General Enquiry</a></li>
							 <li><a href="Appl/Deposit/TermDepositTran.aspx">Deposit Enquiry / Closing</a></li>
                        </ul>
                    </li>
                    <li class="dropdown"><a class="dropdown-toggle" data-toggle="dropdown" href="#"><font color="white"><strong>Trans</strong><span class="caret"></span></font></a>
                        <ul class="dropdown-menu">
                             <li><a href="Appl/Transaction/selRnP.aspx">Receipts and Payments Transaction</a></li>  
                             
							 <li><a href="Appl/Transaction/PassOffCust.aspx">Passing Officer New</a></li>
							 <li><a href="Appl/Transaction/CashPaymentApp.aspx">Pending Payment List</a></li>
							 <li><a href="Appl/Transaction/TrnsfrTransaction.aspx">Transfer Transaction</a></li>
                             <li><a href="Appl/Transaction/ChkBookReceipt.aspx">Cheque Book Receipt</a></li>     
                             <li><a href="Appl/Transaction/ChequeIssue.aspx">Cheque Book Issue</a></li>
                             <li><a href="Appl/Transaction/OtherCreditTrans.aspx">Other Linked Credit Transaction</a></li>
                             <li><a href="Appl/Transaction/CashChest.aspx">Cash Chest</a></li>
                             <li><a href="Appl/Transaction/DeleteTran.aspx">Delete Released Transactions</a></li>  
                             <li class=" dropdown dropdown-submenu"><a href="#" class="dropdown-toggle" data-toggle="dropdown">Stop Payments</a> 
                             <ul class="dropdown-menu">
                                 <li><a href="Appl/Transaction/AccChqStopPayment.aspx">Acc Cheque Stop Payment</a></li>
                                 <li><a href="Appl/DDPO/DdpoStopPayment.aspx">DDPO Stop Payment</a></li>    
                             </ul>
                             </li>                              
                             <li class=" dropdown dropdown-submenu"><a href="#" class="dropdown-toggle" data-toggle="dropdown">Authorization</a> 
                             <ul class="dropdown-menu">                                
                                 <%--<li><a  href="Appl/Master/ClientAuthorize.aspx">Client Authorize</a></li> --%> 
                                <li><a  href="Appl/Transaction/Chequeauthorize.aspx">Cheque Book Authorize</a></li>
                                <li><a  href="Appl/Transaction/StopPaymentAuthView.aspx">Stop Payment Authorize</a></li>   
                             </ul>
                             </li> 

                              <li class=" dropdown dropdown-submenu"><a href="#" class="dropdown-toggle" data-toggle="dropdown">Cheque Book Request</a> 
                             <ul class="dropdown-menu">
                                 <li><a href="Appl/Transaction/ChequeBookRequest.aspx">Cheque Book Request</a></li>
                                 <li><a href="Appl/Transaction/ChequeBookAuthorization.aspx">Cheque Book Authorize</a></li>
                                 <li><a href="Appl/Transaction/ChqBookHOauthorize.aspx">Cheque Book Authorize in HO</a></li>
				                 <li><a href="Appl/Transaction/CheBukFormatPrint.aspx">Cheque Print</a></li>
                             </ul>
                             </li> 

						  <li class="dropdown dropdown-submenu"><a href="#" class="dropdown-toggle" data-toggle="dropdown">Postings</a> 
                            <ul class="dropdown-menu">
                                <li><a href="Appl/Deposit/CapitalizeOfInterest.aspx">SB Interest posting</a></li>
                                <li><a href="Appl/Deposit/DepositAutoRenewalWithoutLien.aspx">Deposit Auto Renewal </a></li> 
                                <%--<li><a href="Appl/Deposit/FDInterestPostingForIndivdlClient.aspx">FD Int Posting(Daily)</a></li> --%>      
                                <li><a href="Appl/Deposit/DailyPostingsOnDays.aspx">Term Deposit Posting(Daily)</a></li>       
                            </ul>
                            </li>   
                         </ul>
                    </li> 
                    <li class="dropdown"><a class="dropdown-toggle" data-toggle="dropdown" href="#"><font color="white"><strong>Shares</strong><span class="caret"></span></font></a>
                        <ul class="dropdown-menu">
                        <li><a  href="Appl/SHARE/memberAllot.aspx">Membership Allotment</a></li> 
                            <li><a  href="Appl/SHARE/ShareEnquiry.aspx">Share Enquiry</a></li> 
                            <li><a  href="Appl/SHARE/dividendTransaction.aspx">Dividend Transaction</a></li>                            
                            <li><a  href="Appl/SHARE/shareTransaction.aspx">Share Transaction</a></li>
							<li><a href="Appl/Share/DividendPosting.aspx">Dividend Posting</a></li>	
                        </ul>
                    </li>                      
                  <li class="dropdown"><a class="dropdown-toggle" data-toggle="dropdown" href="#"><font color="white"><strong>Loan</strong><span class="caret"></span></font></a>
                        <ul class="dropdown-menu">
                            <li><a href="Appl/LOAN/DepLoanApplication.aspx">Loan on Deposits Application</a></li>    
                            <li><a  href="Appl/Loan/LoanNoticeDetails.aspx">Loan Notice Details </a></li> 
                            <li><a  href="Appl/Loan/DocumentReceivedList.aspx">Document Received List </a></li> 
                            <li><a  href="Appl/Loan/InsuranceDetails.aspx">Insurance Details </a></li>  
                            <li><a  href="Appl/Loan/LoanAndODParkedExpenses.aspx">Loan Parked Expenses </a></li>
                            <li><a  href="Appl/Loan/LoanAndOdAccClosure.aspx">Loan Acc Closure</a></li>       
                            <li><a  href="Appl/Loan/MarkAccountABNorEP.aspx">Mark Account ABN or EP</a></li>
                            <li><a href="Appl/Loan/LoanRepaymentReschedule.aspx">Loan Repaymment Reschedule</a></li>
                            <li><a href="Appl/Loan/LoanGurantUpdation.aspx">Loan Guarantor Update</a></li>                                                                            
                        </ul>
                    </li>
                    <li class="dropdown"><a class="dropdown-toggle" data-toggle="dropdown" href="#"><font color="white"><strong>Clearing</strong><span class="caret"></span></font></a>
                        <ul class="dropdown-menu">
                        <li><a href="Appl/Clearing/selBrAbbrAccnoClg.aspx">Clearing Inward Outward</a></li>
                          <%--<li><a href="Appl/Clearing/SelectingInwardWithCTS.aspx">Selecting Inward With CTS</a></li>--%>
                          <%--<li><a href="Appl/Clearing/ClearingInwardReturnFile.aspx">Clearing Inward Return</a></li>--%>
                          <li><a href="Appl/Clearing/ClearingPassing.aspx">Clearing Passing officer</a></li>
                          <%--<li><a href="Appl/Clearing/selBrAbbrAccnoClg.aspx">Clearing Outward</a></li>  --%>                       
                        </ul>
                    </li>
                   
                    <li class="dropdown"><a class="dropdown-toggle" data-toggle="dropdown" href="#"><font color="white"><strong>DDPO</strong><span class="caret"></span></font></a>
                        <ul class="dropdown-menu">                                  
                            <li><a  href="Appl/DDPO/DDPOissue.aspx">DDPO Issue</a></li>    
                            <li><a  href="Appl/DDPO/ddpoEnq.aspx">DDPO Enq</a></li>    
                            <li><a  href="Appl/DDPO/ddpoCancel.aspx">DDPO Cancellation</a></li>    
                            <li><a  href="Appl/DDPO/DdpoStopPayment.aspx">DDPO StopPayment</a></li>
							<li><a  href="Appl/DDPO/DDPOReceipt.aspx">DDPO Receipt</a></li>
                        </ul>
                    </li>
                   <%-- <li class="dropdown"><a class="dropdown-toggle" data-toggle="dropdown" href="#"><font color="white"><strong>NACH</strong><span class="caret"></span></font></a>
                        <ul class="dropdown-menu">                                
                            <li><a  href="APPL/Nach/DbtlAndApbsSystemEnrollment.aspx">DBTL & APBS System Enrolment</a></li>
                            <li><a  href="APPL/Nach/DBTL_LOG.aspx">DBTL LOG</a></li>
			                <li><a  href="APPL/Nach/debitReqPendingappr.aspx">Debit Mandate Approval</a></li>
                        </ul>
                    </li>--%>
                    <li class="dropdown"><a class="dropdown-toggle" data-toggle="dropdown" onclick='window.location.href="rmenu.aspx";return false;' href="#"><font color="white"><strong>Reports</strong></font></a></li>
	        <asp:Button ID="lnkSignOut" runat="server" Text="Sign Out" CssClass="btn btn-info navbar-btn" onclick="lnkSignOut_Click" />
            &nbsp;
            </ul>
        </nav>
        <div style="height: 500px;"></div>
    </form>
    <uc2:menuFooter ID="menuFooter6" runat="server" />
</body>
</html>
