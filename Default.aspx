<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Invoice System</title>
    <link href="css\default.css" rel="stylesheet" type="text/css" /> 
    <link href="css\leftmenu.css" rel="stylesheet" type="text/css" /> 
</head>
<body>
    <form id="form1" runat="server">
        <div id="container">
            <div id="header">
                Information System of Power Protection Services
            </div>
            <div id="errormessage">
                <asp:Label ID="lblErrorMessage" runat="server" ></asp:Label>
            </div>
            <div id="leftmenu">
                <ul id="listmenu">
                    <li id="Li1">Order System</li>
                    <li><a href="manageorder.aspx">Manage Order</a></li>
                    <li><a href="#">Order Tracking System</a></li>                                      
                    <li><a href="client.aspx">Manage Clients</a></li>
                    <li><a href="OrderSearch.aspx">Search Orders</a></li>                                      
                    <li id="endmenu"><a href="reportingbySearch.aspx">Advance Search</a></li>                    
                </ul>
            </div>
            <div id="leftmenubanking">
                <ul id="listmenu1"> 
                    <li id="Li2">Banking System</li>                   
                    <li><a href="BankAccountInfo.aspx">Bank Info</a></li>
                    <li><a href="ChequeBooksInfo.aspx">Cheque Books Info</a></li>
                    <li><a href="ChequeIssue.aspx">Cheque Issued</a></li>
                    <li><a href="ChequeReceived.aspx">Cheque Recieved</a></li>
                    <li><a href="ManageCashDeposit.aspx">Cash Deposit</a></li>
                    <li><a href="ChkRecAddToBalance.aspx">Add CHK Received to an Account</a></li>                    
                    <li><a href="Reports.aspx">Bank Statement & Balances</a></li> 
                    <li><a href="Closing.aspx">Perform Closing</a></li>  
                    <li><a href="SearchChequeIssue.aspx">Cheque Issue Search</a></li>                                     
                    <li><a href="SearchChequeReceived.aspx">Cheque Received Search</a></li>                                                         
                    <li id="endmenu1"><a href="#">Advance Search</a></li>                    
                </ul>
            </div>
            <div id="leftmenuexpense">
                <ul id="listmenu2"> 
                    <li id="Li3">Expense System</li>                   
                    <li><a href="DailyExp.aspx">Daily Expense</a></li>
                    <li><a href="CashRegister.aspx">Cash in Hand</a></li>                    
                    <li><a href="SearchDailyExpense.aspx">Expense Search</a></li>
                    <li id="endmenu2"><a href="#">Advance Search</a></li>                    
                </ul>
            </div>
            
            <div id="leftmenuemployee">
                <ul id="listmenu3"> 
                    <li id="Li4">Employee Management</li>     
                    <li><a href="addusers.aspx">Manage Users</a></li>              
                    <li><a href="ManageEmployee.aspx">Manage Employee</a></li>
                    <li><a href="#">Attendance</a></li>                    
                    <li><a href="OrderSearch.aspx">Salary Generator</a></li>
                    <li id="endmenu3"><a href="#">Advance Search</a></li>                    
                </ul>
            </div>
            
            <div id="leftmenuloan">
                <ul id="listmenu4">   
                    <li id="Li5">Loan System</li>   
                    <li><a href="loanaccount.aspx">Manage Loan Account</a></li>              
                    <li><a href="manageLoan.aspx">Manage Loan</a></li>                                        
                    <li><a href="SearchLoan.aspx">Search Loan</a></li>
                    <li id="endmenu4"><a href="#">Advance Search</a></li>                    
                </ul>
            </div>
            
            <div id="leftmenuATM">
                <ul id="listmenu5">   
                    <li id="Li6">ATM System</li>   
                    <li><a href="ManageATMCard.aspx">Manage ATM Card</a></li>              
                    <li><a href="ATMTrans.aspx">ATM Transaction</a></li>                                       
                    <li><a href="SearchATMTrans.aspx">Search ATM Transaction</a></li>
                    <li id="endmenu5"><a href="#">Advance Search</a></li>                    
                </ul>
            </div>
        
        </div>
    </form>
</body>
</html>
