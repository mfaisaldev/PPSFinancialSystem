<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Closing.aspx.cs" Inherits="Closing" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Closing Page</title>
    <link href="css\formstyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server"> 
        <asp:ScriptManager ID="scmgr" runat="server"></asp:ScriptManager>
    <div id="container" align="center">
        <div id="header">
            Closing Page 
        </div><!-- End Header --> 
        <div id="errormessage">
            <asp:Label ID="lblErrorMessage" runat="server" ></asp:Label>
        </div>
        <div id="form">                        
            <br />
            <hr width="100%"/>
            <br />
            <table id="tblForm">                                
                <tr>
                    <td>
                        <asp:DropDownList ID="cboByMonth" runat="server" Width="156px">
                            <asp:ListItem Value="0">===Select Month===</asp:ListItem>
                            <asp:ListItem Value="1">January</asp:ListItem>
                            <asp:ListItem Value="2">Feburary</asp:ListItem>
                            <asp:ListItem Value="3">March</asp:ListItem>
                            <asp:ListItem Value="4">April</asp:ListItem>
                            <asp:ListItem Value="5">May</asp:ListItem>
                            <asp:ListItem Value="6">June</asp:ListItem>
                            <asp:ListItem Value="7">July</asp:ListItem>
                            <asp:ListItem Value="8">August</asp:ListItem>
                            <asp:ListItem Value="9">September</asp:ListItem>
                            <asp:ListItem Value="10">October</asp:ListItem>
                            <asp:ListItem Value="11">November</asp:ListItem>
                            <asp:ListItem Value="12">December</asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList ID="cboByMonthYear" runat="server" Width="156px">
                            <asp:ListItem Value="0">===Select Year===</asp:ListItem>
                            <asp:ListItem Value="2009">2009</asp:ListItem>
                            <asp:ListItem Value="2010">2010</asp:ListItem>
                            <asp:ListItem Value="2011">2011</asp:ListItem>
                            <asp:ListItem Value="2012">2012</asp:ListItem>
                            <asp:ListItem Value="2013">2013</asp:ListItem>
                        </asp:DropDownList>   
                    </td>  
                    <td>
                        <asp:DropDownList ID="cboSelectSysMonthly" runat="server" Width="156px" OnSelectedIndexChanged="cboSelectSysMonthly_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Value="0">===Select System===</asp:ListItem>
                            <asp:ListItem Value="1">Bank Transactions</asp:ListItem>
                            <asp:ListItem Value="2">Cheque Issue</asp:ListItem>
                            <asp:ListItem Value="3">Cheque Received</asp:ListItem>
                            <asp:ListItem Value="4">Expenses</asp:ListItem>
                            <asp:ListItem Value="5">Loan Account Receivable</asp:ListItem>
                            <asp:ListItem Value="6">Loan Account Payable</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate> 
                                <asp:DropDownList ID="cboIDMonthly" runat="server" Width="156px">                            
                                </asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cboSelectSysMonthly" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                        
                    </td>                          
                    <td align="center">
                        <asp:Button ID="btnClosingMonth" runat="server" Text="Monthly Closing" Width="100px" OnClick="btnClosingMonth_Click"/>
                    </td>                    
                </tr>                
                <tr>    
                    <td align="right">
                        <asp:DropDownList ID="cboByYear" runat="server" Width="156px">
                            <asp:ListItem Value="0">===Select Year===</asp:ListItem>
                            <asp:ListItem Value="2009">2009</asp:ListItem>
                            <asp:ListItem Value="2010">2010</asp:ListItem>
                            <asp:ListItem Value="2011">2011</asp:ListItem>
                            <asp:ListItem Value="2012">2012</asp:ListItem>
                            <asp:ListItem Value="2013">2013</asp:ListItem>
                        </asp:DropDownList>                        
                    </td>   
                    <td>
                        <asp:DropDownList ID="cboSelectSysYearly" runat="server" Width="156px" AutoPostBack="true" OnSelectedIndexChanged="cboSelectSysYearly_SelectedIndexChanged">
                            <asp:ListItem Value="0">===Select System===</asp:ListItem>
                            <asp:ListItem Value="1">Bank Transactions</asp:ListItem>
                            <asp:ListItem Value="2">Cheque Issue</asp:ListItem>
                            <asp:ListItem Value="3">Cheque Received</asp:ListItem>
                            <asp:ListItem Value="4">Expenses</asp:ListItem>
                            <asp:ListItem Value="5">Loan Account Receivable</asp:ListItem>
                            <asp:ListItem Value="6">Loan Account Payable</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate> 
                                <asp:DropDownList ID="cboIDYearly" runat="server" Width="156px">                            
                                </asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cboSelectSysYearly" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel> 
                        
                    </td>                   
                    <td align="center">
                        <asp:Button ID="btnClosingYear" runat="server" Text="Yearly Closing" Width="100px" OnClick="btnClosingYear_Click"/>
                    </td>                    
                </tr>
                
                        
                        
            </table>
            <br />
            <hr width="100%"/>
            <br />
            
            
        </div>
    </div>
    </form>
</body>
</html>


