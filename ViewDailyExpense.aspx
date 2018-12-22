<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewDailyExpense.aspx.cs" Inherits="ViewDailyExpense" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Daily Expense Page</title>
    <link href="css\printOrder.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="container">    
        <div id="header">
            <h4><asp:Label ID="lblMainTitle" runat="server"></asp:Label></h4>
        </div><!-- Ending Header -->
        
        <div id="ourcompanyForDE">            
            <u>Our Company's Detail :</u>
            <br />
            <br />
            <table width="100%">                    
                <tr>
                    <td>Company Name : </td>
                    <td><asp:Label ID="lblCompanyName" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>Address : </td>
                    <td><asp:Label ID="lblAddress" runat="server"></asp:Label></td>
                </tr>                    
            </table>                               
        </div><!-- Ending ourcompany -->         
        
        <div id="expinitialsanddate">
            <table>
                <tr>
                    <td>Date :</td>
                    <td><asp:Label ID="lblExpDate" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>Cash in Hand :</td>
                    <td><asp:Label ID="lblCashWasInHand" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>Total Expense of the Day :</td>
                    <td><asp:Label ID="lblExpOfTheDay" runat="server"></asp:Label></td>
                </tr> 
                <tr>
                    <td>Remaining Cash in Hand :</td>
                    <td><asp:Label ID="lblRemainingCash" runat="server"></asp:Label></td>
                </tr>            
            </table>
        </div><!-- Ending Ordernumberanddate -->
        
                
        <div id="orderDetail">
            <hr color="black" />            
            <h4><asp:Label ID="lblDailyExpTitle" runat="server"></asp:Label></h4>
            <br />
            <table id="detailtable">
                <tr id="headertable">                                        
                    <td>Item Description</td>                                                                    
                    <td>Quantity</td>
                    <td>Rate</td>
                    <td>Amount</td>
                </tr>
                <%=GenerateHTML%> 
                <tr>
                    <td colspan="3" align="right">Total Amount :</td>
                    <td align="right"><asp:Label ID="lblGrandTotal" runat="server"></asp:Label></td>
                </tr>
                
                        
                
            </table>
                   
            <br /> 
            <br /> 
            <br /> 
            <div id="lowerbanner">
                <h5>Power Protection Services</h5>
            </div>
            <br /> 
            <br /> 
            <br /> 
                        
        </div><!-- Ending orderDetail -->
                        
    </div><!-- Ending container -->
    </form>
</body>
</html>
