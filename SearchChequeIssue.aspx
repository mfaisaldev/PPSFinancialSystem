<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SearchChequeIssue.aspx.cs" Inherits="SearchChequeIssue" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Search Cheque Page</title>
    <link href="css\default.css" rel="stylesheet" type="text/css" />
</head>

<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scmgr" runat="server"></asp:ScriptManager>
    <div id="container">
        <div id="header">
            Cheque Issue Search
        </div>
        
        <div id="errormessage">
            <asp:Label ID="lblErrorMessage" runat="server" ></asp:Label>
        </div>
        <div id="search1"></div>
        <div id="search2"></div>
        <div id="searchbox">
            <table>
                <tr>
                    <td>
                        Bank:
                    </td>
                    <td>                        
                        <asp:DropDownList ID="cboBank" runat="server" OnSelectedIndexChanged="cboBank_SelectedIndexChanged" AutoPostBack="true">             
                        </asp:DropDownList>
                        <asp:Label ID="lblBank" runat="server" ForeColor="red"></asp:Label>
                    </td>
                </tr>               
                <tr>
                    <td>
                        Account :
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="cboChequeBook" runat="server" OnSelectedIndexChanged="cboChequeBook_SelectedIndexChanged" AutoPostBack="true">             
                                </asp:DropDownList>
                                <asp:Label ID="lblAccount" runat="server" ForeColor="red"></asp:Label>                                
                            </ContentTemplate>
                            <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cboBank" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                                                
                    </td>
                </tr>
                <tr id="chkNum">
                    <td>
                        Cheque Numbers :
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="cboChequeNumber" runat="server">                                 
                                </asp:DropDownList>
                                <asp:Label ID="lblChequeNumber" runat="server" ForeColor="red"></asp:Label>
                            </ContentTemplate>
                            <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cboChequeBook" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>  
                                                            
                    </td>
                </tr>
                <tr>
                    <td>
                        By Client :
                    </td>
                    <td>
                        <asp:DropDownList ID="cboClient" runat="server" >             
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        By Date :
                    </td>
                    <td>
                        <asp:TextBox ID="txtDate" runat="server" ></asp:TextBox>
                        <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server" 
                            TargetControlID="txtDate">
                        </cc1:CalendarExtender>
                        (MM/DD/YY)
                    </td>
                </tr>
                <tr>
                    <td>
                        By Month:
                    </td>
                    <td>
                        <asp:DropDownList ID="cboByMonth" runat="server" >
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
                    </td>
                </tr>
                <tr>
                    <td>
                        By Year:
                    </td>
                    <td>
                        <asp:DropDownList ID="cboByYear" runat="server" >
                            <asp:ListItem Value="0">===Select Year===</asp:ListItem>
                            <asp:ListItem Value="2013">2013</asp:ListItem>
                            <asp:ListItem Value="2012">2012</asp:ListItem>
                            <asp:ListItem Value="2011">2011</asp:ListItem>
                            <asp:ListItem Value="2010">2010</asp:ListItem>
                            <asp:ListItem Value="2009">2009</asp:ListItem>
                            <asp:ListItem Value="2008">2008</asp:ListItem>
                            <asp:ListItem Value="2007">2007</asp:ListItem>
                            <asp:ListItem Value="2006">2006</asp:ListItem>
                        </asp:DropDownList>                        
                    </td>
                </tr>
                <tr>
                    <td>
                        From Date:
                    </td>
                    <td>
                        <asp:TextBox ID="txtFromDate" runat="server" ></asp:TextBox>
                        <cc1:CalendarExtender ID="txtFromDate_CalendarExtender" runat="server" 
                            TargetControlID="txtFromDate">
                        </cc1:CalendarExtender>
                        (MM/DD/YY)
                    </td>
                </tr>
                <tr>
                    <td>
                        To Date :
                    </td>
                    <td>
                        <asp:TextBox ID="txtToDate" runat="server" ></asp:TextBox>
                        <cc1:CalendarExtender ID="txtToDate_CalendarExtender" runat="server" 
                            TargetControlID="txtToDate">
                        </cc1:CalendarExtender>
                        (MM/DD/YY)
                    </td>
                </tr>         
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />                    
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />                    
                    </td>
                </tr>           
            
            </table>
        
        <br />
        <hr />
        <center>Search Results</center>
        <br />
        <asp:DataGrid ID="dgChequeIssues" runat="server" AutoGenerateColumns="False" ShowHeader="True" Width="100%" BackColor="Silver">
                <HeaderStyle Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center" ForeColor="White" VerticalAlign="Middle"></HeaderStyle>
                <Columns>			                                                           										
                			
                    <asp:TemplateColumn HeaderText="Bank Name">
                        <HeaderStyle Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
			                ForeColor="Black"></HeaderStyle>
				            <ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Left" ForeColor="White"></ItemStyle>
				            <ItemTemplate>							
                                <asp:Panel ID="pnlBankName" runat="server">
					                <asp:Label Runat="server" ID="lblBankName" Text='<%# DataBinder.Eval(Container.DataItem, "BankName") %>'></asp:Label>
                                </asp:Panel>	
                            </ItemTemplate>                                                    
                    </asp:TemplateColumn>											
                                        
                    <asp:TemplateColumn HeaderText="Account Number">
                        <HeaderStyle Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
			                ForeColor="Black"></HeaderStyle>
				            <ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Left" ForeColor="White"></ItemStyle>
				            <ItemTemplate>							
                                <asp:Panel ID="pnlAccountNumber" runat="server">
					                <asp:Label Runat="server" ID="lblAccountNumber" Text='<%# DataBinder.Eval(Container.DataItem, "AccountNumber") %>'></asp:Label>
                                </asp:Panel>	
                            </ItemTemplate>                                                    
                    </asp:TemplateColumn> 
                       
                    <asp:TemplateColumn HeaderText="Cheque Number">
                        <HeaderStyle Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
			                ForeColor="Black"></HeaderStyle>
				            <ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Left" ForeColor="White"></ItemStyle>
				            <ItemTemplate>							
                                <asp:Panel ID="pnlChequeNumber" runat="server">
					                <asp:Label Runat="server" ID="lblChequeNumber" Text='<%# DataBinder.Eval(Container.DataItem, "ChequeNumberWithPreFix") %>'></asp:Label>
                                </asp:Panel>	
                            </ItemTemplate>                                                    
                    </asp:TemplateColumn>
                    
                    <asp:TemplateColumn HeaderText="Client">
                        <HeaderStyle Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
			                ForeColor="Black"></HeaderStyle>
				            <ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Left" ForeColor="White"></ItemStyle>
				            <ItemTemplate>							
                                <asp:Panel ID="pnlClient" runat="server">
					                <asp:Label Runat="server" ID="lblClient" Text='<%# DataBinder.Eval(Container.DataItem, "ClientName") %>'></asp:Label>
                                </asp:Panel>	
                            </ItemTemplate>                                                    
                    </asp:TemplateColumn>    
                    
                    <asp:TemplateColumn HeaderText="Issue Date">
                        <HeaderStyle Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
			                ForeColor="Black"></HeaderStyle>
				            <ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Left" ForeColor="White"></ItemStyle>
				            <ItemTemplate>							
                                <asp:Panel ID="pnlIssueDate" runat="server">
					                <asp:Label Runat="server" ID="lblIssueDate" Text='<%# DataBinder.Eval(Container.DataItem, "IssueDate") %>'></asp:Label>
                                </asp:Panel>	
                            </ItemTemplate>                                                    
                    </asp:TemplateColumn>  
                    
                    <asp:TemplateColumn HeaderText="Amount">
                        <HeaderStyle Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
			                ForeColor="Black"></HeaderStyle>
				            <ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Left" ForeColor="White"></ItemStyle>
				            <ItemTemplate>							
                                <asp:Panel ID="pnlAmount" runat="server">
					                <asp:Label Runat="server" ID="lblAmount" Text='<%# DataBinder.Eval(Container.DataItem, "Amount") %>'></asp:Label>
                                </asp:Panel>	
                            </ItemTemplate>                                                    
                    </asp:TemplateColumn>                                                            
					
					<asp:TemplateColumn HeaderText="Edit">
						<HeaderStyle Font-Size="X-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
							ForeColor="Black"></HeaderStyle>
						<ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Center" ForeColor="White"></ItemStyle>
						<ItemTemplate>
						<asp:Panel ID="pnlEdit" runat="server">
							<asp:HyperLink id="LinkButton1" runat="server" NavigateUrl='<%# EditChequeIssue(DataBinder.Eval(Container.DataItem, "ID").ToString()) %>'>Edit</asp:HyperLink>
						</asp:Panel>
						</ItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="Delete">
						<HeaderStyle Font-Size="X-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
							ForeColor="Black"></HeaderStyle>
						<ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Center" ForeColor="White"></ItemStyle>
						<ItemTemplate>
						<asp:Panel ID="pnlDelete" runat="server">
							<asp:LinkButton id="Linkbutton2" runat="server" Text='Delete' CommandName="DeleteChequeIssue" CommandArgument='<%# DeleteChequeIssue(DataBinder.Eval(Container.DataItem, "ID").ToString()) %>'>
							</asp:LinkButton>
						</asp:Panel>
						</ItemTemplate>
					</asp:TemplateColumn>                                                                      
                    
                </Columns>	
             </asp:DataGrid>
             
         </div><!-- End datagrid -->
         
    </div>
    </form>
</body>
</html>

