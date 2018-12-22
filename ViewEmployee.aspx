<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewEmployee.aspx.cs" Inherits="ViewEmployee" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Employee Page</title>
    <link href="css\employee.css" rel="stylesheet" type="text/css" />
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
                    <td><asp:Label ID="lblCompanyAddress" runat="server"></asp:Label></td>
                </tr>                    
            </table>                               
        </div><!-- Ending ourcompany --> 
               
        <div id="headtable">
        <div id="Employeenumberanddate">        
            <table>
                <tr>
                    <td>Date :</td>
                    <td><asp:Label ID="lblDate" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>Employee ID :</td>
                    <td><asp:Label ID="lblEmployeeID" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>Date of Join:</td>
                    <td><asp:Label ID="lblDateOfJoin" runat="server"></asp:Label></td>
                </tr>            
            </table>
        </div><!-- Ending Employeenumberanddate -->
        </div>
        
               
        <div id="EmployeeDetail">
            
            <br />  
            <br />
            <br />
            <hr color="black" />            
            <h4><asp:Label ID="lblAccTitle" runat="server" Text="Employee Detail"></asp:Label></h4>
            <br />
            <table id="masterdetail">
                <tr align="center">
                    <td colspan="2">Employee Detail</td>                    
                </tr>
                <tr>
                    <td>Full Name :</td>
                    <td><asp:Label ID="lblName" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>Father Name :</td>
                    <td><asp:Label ID="lblFName" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>Designation :</td>
                    <td><asp:Label ID="lblDesignation" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>NIC:</td>
                    <td><asp:Label ID="lblNIC" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>Phone:</td>
                    <td><asp:Label ID="lblPhone" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>Cell Phone:</td>
                    <td><asp:Label ID="LblMobile" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>Email:</td>
                    <td><asp:Label ID="lblEmail" runat="server"></asp:Label></td>
                </tr>    
                <tr>
                    <td>Address:</td>
                    <td><asp:Label ID="lblAddress" runat="server"></asp:Label></td>
                </tr>  
                <tr>
                    <td>City:</td>
                    <td><asp:Label ID="lblCity" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>Country:</td>
                    <td><asp:Label ID="lblCountry" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>Reference :</td>
                    <td><asp:Label ID="lblReference" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>Any Description :</td>
                    <td><asp:Label ID="lblDescription" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>Salary At the time of Job Started :</td>
                    <td><asp:Label ID="lblStartingSalary" runat="server"></asp:Label></td>
                </tr>                               
            </table>
            <br />
            <h4>Salary Details</h4>
            <br />
            <table id="detailtable">
                <tr id="headertable">                    
                    <td>Date of Increament</td>
                    <td>Increamented Amount</td>                    
                    <td>Description</td>                                                
                </tr>
                <%=GenerateHTML%>                            
                        
                
            </table>
            <br />            
            <table id="salarytotal">
                <tr>
                    <td colspan="2" align="right">Current Salary :</td>
                    <td align="right"><asp:Label ID="lblCurrentSalary" runat="server"></asp:Label></td>
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

