<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Bank Page</title>
    <link href="css\formstyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server"> 
        
    <div id="container" align="center">
        <div id="header">
            Login Screen 
        </div><!-- End Header --> 
        <div id="errormessage">
            <asp:Label ID="lblErrorMessage" runat="server" ></asp:Label>
        </div>
        <div id="form">                       
            
            <table id="tblForm">
                <tr>
                    <td>
                        User ID:
                    </td>
                    <td>
                        <asp:TextBox ID="txtUserID" runat="server" MaxLength="50"></asp:TextBox>
                    </td>
                </tr>                
                <tr>
                    <td>
                        Password:
                    </td>
                    <td>
                        <asp:TextBox ID="txtPassword" runat="server" MaxLength="14"></asp:TextBox>
                    </td>
                </tr>
                <tr>                    
                    <td colspan="2" align="center">
                        <asp:Button ID="btnSave" runat="server" Text="Login Me" Width="65px" OnClick="btnSave_Click"/>
                        <asp:Button ID="btnClose" runat="server" Text="Cancel" Width="65px" OnClick="btnClose_Click"/>&nbsp;
                    </td>
                    
                </tr>
            </table>  
            
             
         </div><!-- End datagrid -->
            
        </div>
    
    </form>
</body>
</html>



