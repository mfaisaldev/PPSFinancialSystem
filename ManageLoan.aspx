<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManageLoan.aspx.cs" Inherits="ManageLoan" %>


<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Loan Page</title>
    <link href="css\formstyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server"> 
    <asp:ScriptManager runat="server" ID="ScrptManager"></asp:ScriptManager>    
    <div id="container" align="center">
        <div id="header">
            Loan Page 
        </div><!-- End Header --> 
        <div id="errormessage">
            <asp:Label ID="lblErrorMessage" runat="server" ></asp:Label>
        </div>
        <div id="form" align="left">
            
            <asp:TextBox ID="txtLoanID" runat="server" Visible="false"></asp:TextBox>
            
            <table id="tblForm">
                <tr>
                    <td>
                        Account Name:
                    </td>
                    <td>
                        <asp:DropDownList ID="cboLoanAcc" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        Loan Date:
                    </td>
                    <td>
                        <asp:TextBox ID="txtLoanDate" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtLoanDate_CalendarExtender" runat="server" 
                            TargetControlID="txtLoanDate">
                        </cc1:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                        Due Date:
                    </td>
                    <td style="color:Black;">
                        <asp:TextBox ID="txtDueDate" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtDueDate_CalendarExtender" runat="server" 
                            TargetControlID="txtDueDate">
                        </cc1:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                        Amount:
                    </td>
                    <td>
                        <asp:TextBox ID="txtAmount" runat="server"></asp:TextBox>
                    </td>
                </tr>                
                <tr>
                    <td>
                        Description:
                    </td>
                    <td>
                        <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Rows="5" Columns="30"></asp:TextBox>                        
                    </td>
                </tr>
                <tr>
                    <td>
                        Account Receivable/Payable:
                    </td>
                    <td>
                        <asp:RadioButton ID="rdoAcREC" Text="Accounts Receivable" runat="server" GroupName="Acc"/><br />
                        <asp:RadioButton ID="rdoAcPAY" Text="Accounts Payable" runat="server" GroupName="Acc"/>
                    </td>
                </tr>
                <tr>                    
                    <td colspan="2" align="center">
                        <asp:Button ID="btnClose" runat="server" Text="Cancel" OnClick="btnClose_Click" Width="60px"/>                    
                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" Width="60px"/>
                        <asp:Button ID="btnReset" runat="server" Text="Reset" Width="60px" OnClick="btnReset_Click" />
                    </td>
                    
                </tr>
            </table>
            <br />
            <hr width="100%"/>
            <table width="100%">
                <tr>
                    <td align="center">
                        Grid For Loan
                    </td>
                </tr>
            </table>
            <br />
            <font color="white">Search By:</font><asp:DropDownList ID="cboLoanSearch" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cboLoanSearch_SelectedIndexChanged1">
                <asp:ListItem>Show All</asp:ListItem>
                <asp:ListItem Value="1">Cleared</asp:ListItem>
                <asp:ListItem Value="0">UnCleared</asp:ListItem>
            </asp:DropDownList>
            <br />
            <br />
            <asp:DataGrid ID="dgLoans" runat="server" AutoGenerateColumns="False" ShowHeader="True" Width="100%" BackColor="Silver" AllowPaging="true" PageSize="15">
                <HeaderStyle Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center" ForeColor="White" VerticalAlign="Middle"></HeaderStyle>
                <Columns>			                                                           										
                			
                    <asp:TemplateColumn HeaderText="Account Name">
                        <HeaderStyle Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
			                ForeColor="Black"></HeaderStyle>
				            <ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Left" ForeColor="White"></ItemStyle>
				            <ItemTemplate>							
                                <asp:Panel ID="pnlLoanName" runat="server">
					                <asp:Label Runat="server" ID="lblLoanName" Text='<%# DataBinder.Eval(Container.DataItem, "Name") %>'></asp:Label>
                                </asp:Panel>	
                            </ItemTemplate>                                                    
                    </asp:TemplateColumn>											
                    
                    <asp:TemplateColumn HeaderText="Loan Date">
                        <HeaderStyle Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
			                ForeColor="Black"></HeaderStyle>
				            <ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Left" ForeColor="White"></ItemStyle>
				            <ItemTemplate>							
                                <asp:Panel ID="pnlLoanDate" runat="server">
					                <asp:Label Runat="server" ID="lblLoanDate" Text='<%# DataBinder.Eval(Container.DataItem, "LoanDate") %>'></asp:Label>
                                </asp:Panel>	
                            </ItemTemplate>                                                    
                    </asp:TemplateColumn>   
                    
                    <asp:TemplateColumn HeaderText="Due Date">
                        <HeaderStyle Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
			                ForeColor="Black"></HeaderStyle>
				            <ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Left" ForeColor="White"></ItemStyle>
				            <ItemTemplate>							
                                <asp:Panel ID="pnlDueDate" runat="server">
					                <asp:Label Runat="server" ID="lblDueDate" Text='<%# DataBinder.Eval(Container.DataItem, "DueDate") %>'></asp:Label>
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
                    
                    <asp:TemplateColumn HeaderText="Description">
                        <HeaderStyle Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
			                ForeColor="Black"></HeaderStyle>
				            <ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Left" ForeColor="White"></ItemStyle>
				            <ItemTemplate>							
                                <asp:Panel ID="pnlDescription" runat="server">
					                <asp:Label Runat="server" ID="lblDescription" Text='<%# DataBinder.Eval(Container.DataItem, "Description") %>'></asp:Label>
                                </asp:Panel>	
                            </ItemTemplate>                                                    
                    </asp:TemplateColumn>                                                               
                    
                    <asp:TemplateColumn HeaderText="Transaction IN/OUT">
						<HeaderStyle Font-Size="X-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
							ForeColor="Black"></HeaderStyle>
						<ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Center" ForeColor="Red"></ItemStyle>
						<ItemTemplate>
						<asp:Panel ID="pnlTINOUT" runat="server">
							<asp:HyperLink id="LinkButton3" runat="server" NavigateUrl='<%# TransINOUT(DataBinder.Eval(Container.DataItem, "ID").ToString()) %>'>Perform Transaction</asp:HyperLink>
						</asp:Panel>
						</ItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="View">
						<HeaderStyle Font-Size="X-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
							ForeColor="Black"></HeaderStyle>
						<ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Center" ForeColor="White"></ItemStyle>
						<ItemTemplate>
						<asp:Panel ID="pnlView" runat="server">
							<asp:HyperLink id="LinkButton4" runat="server" NavigateUrl='<%# ViewLoan(DataBinder.Eval(Container.DataItem, "ID").ToString()) %>'>View</asp:HyperLink>
						</asp:Panel>
						</ItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="Edit">
						<HeaderStyle Font-Size="X-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
							ForeColor="Black"></HeaderStyle>
						<ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Center" ForeColor="White"></ItemStyle>
						<ItemTemplate>
						<asp:Panel ID="pnlEdit" runat="server">
							<asp:HyperLink id="LinkButton1" runat="server" NavigateUrl='<%# EditLoan(DataBinder.Eval(Container.DataItem, "ID").ToString()) %>'>Edit Item</asp:HyperLink>
						</asp:Panel>
						</ItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="Delete">
						<HeaderStyle Font-Size="X-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
							ForeColor="Black"></HeaderStyle>
						<ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Center" ForeColor="White"></ItemStyle>
						<ItemTemplate>
						<asp:Panel ID="pnlDelete" runat="server">
							<asp:LinkButton id="Linkbutton2" runat="server" Text='Delete' CommandName="DeleteLoan" CommandArgument='<%# DeleteLoan(DataBinder.Eval(Container.DataItem, "ID").ToString()) %>'>
							</asp:LinkButton>
						</asp:Panel>
						</ItemTemplate>
					</asp:TemplateColumn>                                                                      
                    
                </Columns>	
             </asp:DataGrid>
             
            <br />
            <hr width="100%"/>
            <table width="100%">
                <tr>
                    <td align="center">
                        Grid For Accounts Receivable
                    </td>
                </tr>
            </table>
            <br />
            
            <asp:DataGrid ID="dgAccReceivable" runat="server" AutoGenerateColumns="False" ShowHeader="True" Width="100%" BackColor="Silver" AllowPaging="true" PageSize="15">
                <HeaderStyle Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center" ForeColor="White" VerticalAlign="Middle"></HeaderStyle>
                <Columns>			                                                           										
                			
                    <asp:TemplateColumn HeaderText="Account Name">
                        <HeaderStyle Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
			                ForeColor="Black"></HeaderStyle>
				            <ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Left" ForeColor="White"></ItemStyle>
				            <ItemTemplate>							
                                <asp:Panel ID="pnlRLoanName" runat="server">
					                <asp:Label Runat="server" ID="lblRLoanName" Text='<%# DataBinder.Eval(Container.DataItem, "Name") %>'></asp:Label>
                                </asp:Panel>	
                            </ItemTemplate>                                                    
                    </asp:TemplateColumn>											
                    
                    <asp:TemplateColumn HeaderText="Receivable Date">
                        <HeaderStyle Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
			                ForeColor="Black"></HeaderStyle>
				            <ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Left" ForeColor="White"></ItemStyle>
				            <ItemTemplate>							
                                <asp:Panel ID="pnlRRecDate" runat="server">
					                <asp:Label Runat="server" ID="lblRRecDate" Text='<%# DataBinder.Eval(Container.DataItem, "AccRecDate") %>'></asp:Label>
                                </asp:Panel>	
                            </ItemTemplate>                                                    
                    </asp:TemplateColumn>   
                    
                    <asp:TemplateColumn HeaderText="Due Date">
                        <HeaderStyle Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
			                ForeColor="Black"></HeaderStyle>
				            <ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Left" ForeColor="White"></ItemStyle>
				            <ItemTemplate>							
                                <asp:Panel ID="pnlRDueDate" runat="server">
					                <asp:Label Runat="server" ID="lblRDueDate" Text='<%# DataBinder.Eval(Container.DataItem, "DueDate") %>'></asp:Label>
                                </asp:Panel>	
                            </ItemTemplate>                                                    
                    </asp:TemplateColumn>   
                    
                    <asp:TemplateColumn HeaderText="Amount Receivable">
                        <HeaderStyle Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
			                ForeColor="Black"></HeaderStyle>
				            <ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Left" ForeColor="White"></ItemStyle>
				            <ItemTemplate>							
                                <asp:Panel ID="pnlRAmountR" runat="server">
					                <asp:Label Runat="server" ID="lblRAmountR" Text='<%# DataBinder.Eval(Container.DataItem, "AmountRec") %>'></asp:Label>
                                </asp:Panel>	
                            </ItemTemplate>                                                    
                    </asp:TemplateColumn>
                    
                    <asp:TemplateColumn HeaderText="Amount Received">
                        <HeaderStyle Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
			                ForeColor="Black"></HeaderStyle>
				            <ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Left" ForeColor="White"></ItemStyle>
				            <ItemTemplate>							
                                <asp:Panel ID="pnlRAmountRd" runat="server">
					                <asp:Label Runat="server" ID="lblRAmountRd" Text='<%# DataBinder.Eval(Container.DataItem, "Amount") %>'></asp:Label>
                                </asp:Panel>	
                            </ItemTemplate>                                                    
                    </asp:TemplateColumn>    
                    
                    <asp:TemplateColumn HeaderText="Description">
                        <HeaderStyle Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
			                ForeColor="Black"></HeaderStyle>
				            <ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Left" ForeColor="White"></ItemStyle>
				            <ItemTemplate>							
                                <asp:Panel ID="pnlRDescription" runat="server">
					                <asp:Label Runat="server" ID="lblRDescription" Text='<%# DataBinder.Eval(Container.DataItem, "Description") %>'></asp:Label>
                                </asp:Panel>	
                            </ItemTemplate>                                                    
                    </asp:TemplateColumn>                                                               
					
					<asp:TemplateColumn HeaderText="Edit">
						<HeaderStyle Font-Size="X-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
							ForeColor="Black"></HeaderStyle>
						<ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Center" ForeColor="White"></ItemStyle>
						<ItemTemplate>
						<asp:Panel ID="pnlREdit" runat="server">
							<asp:HyperLink id="RLinkButton1" runat="server" NavigateUrl='<%# EditAccRec(DataBinder.Eval(Container.DataItem, "LoanID").ToString(),DataBinder.Eval(Container.DataItem, "ID").ToString()) %>'>Edit </asp:HyperLink>
						</asp:Panel>
						</ItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="Delete">
						<HeaderStyle Font-Size="X-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
							ForeColor="Black"></HeaderStyle>
						<ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Center" ForeColor="White"></ItemStyle>
						<ItemTemplate>
						<asp:Panel ID="pnlRDelete" runat="server">
							<asp:LinkButton id="RLinkbutton2" runat="server" Text='Delete' CommandName="DeleteLoan" CommandArgument='<%# DeleteAccRec(DataBinder.Eval(Container.DataItem, "ID").ToString()) %>'>
							</asp:LinkButton>
						</asp:Panel>
						</ItemTemplate>
					</asp:TemplateColumn>                                                                      
                    
                </Columns>	
             </asp:DataGrid>
             
             <br />
            <hr width="100%"/>
            <table width="100%">
                <tr>
                    <td align="center">
                        Grid For Accounts Payable
                    </td>
                </tr>
            </table>
            <br />
            
            <asp:DataGrid ID="dgAccPayable" runat="server" AutoGenerateColumns="False" ShowHeader="True" Width="100%" BackColor="Silver" AllowPaging="true" PageSize="15">
                <HeaderStyle Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center" ForeColor="White" VerticalAlign="Middle"></HeaderStyle>
                <Columns>			                                                           										
                			
                    <asp:TemplateColumn HeaderText="Account Name">
                        <HeaderStyle Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
			                ForeColor="Black"></HeaderStyle>
				            <ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Left" ForeColor="White"></ItemStyle>
				            <ItemTemplate>							
                                <asp:Panel ID="pnlPLoanName" runat="server">
					                <asp:Label Runat="server" ID="lblPLoanName" Text='<%# DataBinder.Eval(Container.DataItem, "Name") %>'></asp:Label>
                                </asp:Panel>	
                            </ItemTemplate>                                                    
                    </asp:TemplateColumn>											
                    
                    <asp:TemplateColumn HeaderText="Payable Date">
                        <HeaderStyle Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
			                ForeColor="Black"></HeaderStyle>
				            <ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Left" ForeColor="White"></ItemStyle>
				            <ItemTemplate>							
                                <asp:Panel ID="pnlPPayDate" runat="server">
					                <asp:Label Runat="server" ID="lblPPayDate" Text='<%# DataBinder.Eval(Container.DataItem, "AccPayDate") %>'></asp:Label>
                                </asp:Panel>	
                            </ItemTemplate>                                                    
                    </asp:TemplateColumn>   
                    
                    <asp:TemplateColumn HeaderText="Due Date">
                        <HeaderStyle Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
			                ForeColor="Black"></HeaderStyle>
				            <ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Left" ForeColor="White"></ItemStyle>
				            <ItemTemplate>							
                                <asp:Panel ID="pnlPDueDate" runat="server">
					                <asp:Label Runat="server" ID="lblPDueDate" Text='<%# DataBinder.Eval(Container.DataItem, "DueDate") %>'></asp:Label>
                                </asp:Panel>	
                            </ItemTemplate>                                                    
                    </asp:TemplateColumn>   
                    
                    <asp:TemplateColumn HeaderText="Amount Receivable">
                        <HeaderStyle Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
			                ForeColor="Black"></HeaderStyle>
				            <ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Left" ForeColor="White"></ItemStyle>
				            <ItemTemplate>							
                                <asp:Panel ID="pnlPAmountP" runat="server">
					                <asp:Label Runat="server" ID="lblPAmountP" Text='<%# DataBinder.Eval(Container.DataItem, "AmountPayable") %>'></asp:Label>
                                </asp:Panel>	
                            </ItemTemplate>                                                    
                    </asp:TemplateColumn>
                    
                    <asp:TemplateColumn HeaderText="Amount Paid">
                        <HeaderStyle Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
			                ForeColor="Black"></HeaderStyle>
				            <ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Left" ForeColor="White"></ItemStyle>
				            <ItemTemplate>							
                                <asp:Panel ID="pnlPAmountPd" runat="server">
					                <asp:Label Runat="server" ID="lblPAmountPd" Text='<%# DataBinder.Eval(Container.DataItem, "Amount") %>'></asp:Label>
                                </asp:Panel>	
                            </ItemTemplate>                                                    
                    </asp:TemplateColumn>    
                    
                    <asp:TemplateColumn HeaderText="Description">
                        <HeaderStyle Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
			                ForeColor="Black"></HeaderStyle>
				            <ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Left" ForeColor="White"></ItemStyle>
				            <ItemTemplate>							
                                <asp:Panel ID="pnlPDescription" runat="server">
					                <asp:Label Runat="server" ID="lblPDescription" Text='<%# DataBinder.Eval(Container.DataItem, "Description") %>'></asp:Label>
                                </asp:Panel>	
                            </ItemTemplate>                                                    
                    </asp:TemplateColumn>                                                               
					
					<asp:TemplateColumn HeaderText="Edit">
						<HeaderStyle Font-Size="X-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
							ForeColor="Black"></HeaderStyle>
						<ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Center" ForeColor="White"></ItemStyle>
						<ItemTemplate>
						<asp:Panel ID="pnlPEdit" runat="server">
							<asp:HyperLink id="PLinkButton1" runat="server" NavigateUrl='<%# EditAccPay(DataBinder.Eval(Container.DataItem, "LoanID").ToString(),DataBinder.Eval(Container.DataItem, "ID").ToString()) %>'>Edit Item</asp:HyperLink>
						</asp:Panel>
						</ItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="Delete">
						<HeaderStyle Font-Size="X-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
							ForeColor="Black"></HeaderStyle>
						<ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Center" ForeColor="White"></ItemStyle>
						<ItemTemplate>
						<asp:Panel ID="pnlPDelete" runat="server">
							<asp:LinkButton id="PLinkbutton2" runat="server" Text='Delete' CommandName="DeleteLoan" CommandArgument='<%# DeleteAccPay(DataBinder.Eval(Container.DataItem, "ID").ToString()) %>'>
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


