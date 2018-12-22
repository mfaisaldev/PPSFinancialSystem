<%@ Page Language="C#" AutoEventWireup="true" CodeFile="printOrder.aspx.cs" Inherits="printOrder" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Order Page</title>
    <link href="css\printOrder.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    
    <div id="container">
        <div id="header">
            <h4><asp:Label ID="lblMainTitle" runat="server"></asp:Label></h4>
        </div><!-- Ending Header -->
        
        <div id="ourcompany">            
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
        
        <div id="client">
            <u>Client's Detail :</u>
            <br />
            <br />
            <table width="100%">                    
                <tr>
                    <td>Client Name : </td>
                    <td><asp:Label ID="lblClientName" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>Company Name : </td>
                    <td><asp:Label ID="lblClientCompanyName" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>Address : </td>
                    <td><asp:Label ID="lblClientAddress" runat="server"></asp:Label></td>
                </tr>                    
            </table>                   
        </div>
        
        <div id="ordernumberanddate">
            <table>
                <tr>
                    <td>Date :</td>
                    <td><asp:Label ID="lblOrderDate" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>Order Number :</td>
                    <td><asp:Label ID="lblOrderNumber" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>Due Date :</td>
                    <td><asp:Label ID="lblDueDate" runat="server"></asp:Label></td>
                </tr>            
            </table>
        </div><!-- Ending Ordernumberanddate -->
        
                
        <div id="orderDetail">           
            
            <hr color="black" />            
            <h4><asp:Label ID="lblDetailTitle" runat="server"></asp:Label></h4>
            <br />
            <table id="detailtable">
                <tr id="headertable">                    
                    <td>Date</td>
                    <td>Item</td>                    
                    <td>Description</td>                            
                    <td>Quantity</td>
                    <td>Rate</td>
                    <td>Amount</td>
                </tr>
                <%=GenerateHTML%> 
                <tr>
                    <td colspan="5" align="right">Total Amount :</td>
                    <td align="right"><asp:Label ID="lblGrandTotdal" runat="server"></asp:Label></td>
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
                        
        </div>
                        
    </div><!-- Ending container -->
    </form>
</body>
</html>
