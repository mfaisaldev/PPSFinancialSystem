<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportBankTrans.aspx.cs" Inherits="ReportBankTrans" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Bank Page</title>
    <link href="css\Bank.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="container">
        <div id="header">
            <h4><asp:Label ID="lblMainTitle" runat="server"></asp:Label></h4>
        </div><!-- Ending Header -->
        
        <div id="bankaccount">
            <u>Bank Account's Detail :</u>
            <br />
            <br />
            <table width="100%">
                    <tr>
                        <td>Bank Name : </td>
                        <td><asp:Label ID="lblBankName" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>Account Holder Name : </td>
                        <td><asp:Label ID="lblAccHolderName" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>Account Number : </td>
                        <td><asp:Label ID="lblAccountNumber" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>Balance at the start of Month: </td>
                        <td align="right"><asp:Label ID="lblBalanceStartOfMonth" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>Available Balance Now: </td>
                        <td align="right"><asp:Label ID="lblAvailBalance" runat="server"></asp:Label></td>
                    </tr>
            </table>           
            
        </div><!-- Ending loacaccount -->
            
        <div id="headtable">
            <div id="banknumberanddate">        
                <table>
                    <tr>
                        <td>Date :</td>
                        <td><asp:Label ID="lblDateNow" runat="server"></asp:Label></td>
                    </tr>                    
                </table>
            </div><!-- Ending Banknumberanddate -->            
        </div>
         
               
        <div id="bankDetail">           
            <br />
            <br /> 
            <br />
            <br />  
            <br />
            <br />
            <hr color="black" />            
            <h4><asp:Label ID="lblAccTitle" runat="server" Text="Cheque Issue Details"></asp:Label></h4>
            <br />
            <table id="detailtable">
                <tr id="headertable">                    
                    <td>Date</td>
                    <td>Cheque Number</td>
                    <td>Client Name</td>
                    <td>Amount</td>                                                                                        
                </tr>                
                <%=GenerateHTMLAccPay%>                            
                <tr style="font-weight:bold;">
                    <td colspan="2" bordercolor="white" style="border:none;">                    
                    </td>
                    <td align="right" style="border:dashed 1px black;">Total Transaction Amount :</td>
                    <td align="right" style="border:dashed 1px black;"><asp:Label ID="lblTotalAmountCheIssue" runat="server"></asp:Label></td>
                </tr>
                        
                
            </table>
            
            <br />             
            <br />
            <hr color="black" />            
            <h4><asp:Label ID="lblAccTitle2" runat="server" Text="Cheque Received Details"></asp:Label></h4>
            <br />
            <table id="detailtable2">
                <tr id="headertable2">                    
                    <td>Receiving Date</td>
                    <td>Cheque Number</td>
                    <td>Client Name</td>
                    <td>Clear And Added Date</td>                                             
                    <td>Amount</td>                    
                </tr>
                <%=GenerateHTMLAccRec%>
                <tr style="font-weight:bold;">
                    <td colspan="3" bordercolor="white" style="border:none;">                    
                    </td>
                    <td align="right" style="border:dashed 1px black;">Total Transaction Amount :</td>
                    <td align="right" style="border:dashed 1px black;"><asp:Label ID="lblTotalAmountCheRec" runat="server"></asp:Label></td>
                </tr>                            
                        
                
            </table>
            
            
            <br />
            <br />
            <hr color="black" />            
            <h4><asp:Label ID="lblATMTrans" runat="server" Text="ATM Transactions"></asp:Label></h4>
            <br />
            <table id="detailtable3">
                <tr id="headertable3">     
                    <td>Card Number</td>               
                    <td>Transaction Date</td>
                    <td>Transaction ID</td>                    
                    <td>Amount</td>                    
                                                                                     
                </tr>
                <%=GenerateHTMLATMTrans%>                            
                <tr style="font-weight:bold;">
                    <td colspan="2" bordercolor="white" style="border:none;">                    
                    </td>
                    <td align="right" style="border:dashed 1px black;">Total Transaction Amount :</td>        
                    <td align="right" style="border:dashed 1px black;"><asp:Label ID="lblTotalAmountATMTrans" runat="server"></asp:Label></td>
                </tr>
            </table>
            
            <br />
            <br />
            <hr color="black" />            
            <h4><asp:Label ID="lblCashDep" runat="server" Text="Cash Deposit Transactions"></asp:Label></h4>
            <br />
            <table id="detailtable4">
                <tr id="headertable4">     
                    <td>Date</td>                                   
                    <td>Deposit By</td>                                                                               
                    <td>Amount</td>                                                         
                </tr>
                <%=GenerateHTMLCashDep%>                            
                <tr style="font-weight:bold;">
                    <td bordercolor="white" style="border:none;">                    
                    </td>
                    <td align="right" style="border:dashed 1px black;">Total Transaction Amount :</td>        
                    <td align="right" style="border:dashed 1px black;"><asp:Label ID="lblTotalAmountCashDep" runat="server"></asp:Label></td>
                </tr>
            </table>
                       
            <br /> 
            <br /> 
            <br /> 
            <div id="lowerbanner">
                <h5>XYZ Company</h5>
            </div>
            <br /> 
            <br /> 
            <br /> 
                        
        </div>
                        
    </div><!-- Ending container -->
    </form>
</body>
</html>


