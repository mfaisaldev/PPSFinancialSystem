<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewLoan.aspx.cs" Inherits="ViewLoan" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Loan Page</title>
    <link href="css\Loan.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="container">
        <div id="header">
            <h4><asp:Label ID="lblMainTitle" runat="server"></asp:Label></h4>
        </div><!-- Ending Header -->
        
        <div id="loanaccount">
            <u>Loan Account's Detail :</u>
            <br />
            <br />
            <table width="100%">
                    <tr>
                        <td>Person's Name : </td>
                        <td><asp:Label ID="lblLoanAccount" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>Company Name : </td>
                        <td><asp:Label ID="lblCompanyName" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>Address : </td>
                        <td><asp:Label ID="lblAddress" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>Phone : </td>
                        <td><asp:Label ID="lblPhone" runat="server"></asp:Label></td>
                    </tr>
            </table>           
            
        </div><!-- Ending loacaccount -->
            
        <div id="headtable">
            <div id="Loannumberanddate">        
                <table>
                    <tr>
                        <td>Loan Date :</td>
                        <td><asp:Label ID="lblLoanDate" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>Loan ID :</td>
                        <td><asp:Label ID="lblLoanID" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>Due Date :</td>
                        <td><asp:Label ID="lblDueDate" runat="server"></asp:Label></td>
                    </tr>       
                    <tr>
                        <td>Status :</td>
                        <td><asp:Label ID="lblIsCleared" runat="server"></asp:Label></td>
                    </tr>  
                    <tr>
                        <td>Amount Due :</td>
                        <td><asp:Label ID="lblAmountDue" runat="server"></asp:Label></td>
                    </tr>     
                </table>
            </div><!-- Ending Loannumberanddate -->            
        </div>
         
               
        <div id="LoanDetail">           
            <br />
            <br /> 
            <br />
            <br />  
            <br />
            <br />
            <hr color="black" />            
            <h4><asp:Label ID="lblAccTitle" runat="server"></asp:Label></h4>
            <br />
            <table id="detailtable">
                <tr id="headertable">                    
                    <td>Transaction Date</td>
                    <td>Amount</td>                    
                    <td>Description</td>                                                
                </tr>
                <%=GenerateHTML%>                            
                        
                
            </table>
            
            <br /> 
            <br />            
            <table id="transactiontotal">
                <tr>
                    <td colspan="2" align="right">Total Transaction Amount :</td>
                    <td align="right"><asp:Label ID="lblTotalAmount" runat="server"></asp:Label></td>
                </tr>
            </table>
                       
            <br /> 
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


