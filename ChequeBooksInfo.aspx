<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChequeBooksInfo.aspx.cs" Inherits="ChequeBooksInfo" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Bank Page</title>
    <link href="css\formstyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server"> 
    <asp:ScriptManager ID="scmgr" runat="server"></asp:ScriptManager>    
    <div id="container">
        <div id="header">
            Cheque Book Page 
        </div><!-- End Header --> 
        <div id="errormessage">
            <asp:Label ID="lblErrorMessage" runat="server" ></asp:Label>
        </div>
        <div id="form">
            
            <asp:TextBox ID="txtChequeBookID" runat="server" Visible="false"></asp:TextBox>
            
            <table id="tblForm">                
                <tr>
                    <td>
                        Bank:
                    </td>
                    <td>                        
                        <asp:DropDownList ID="cboBank" runat="server" OnSelectedIndexChanged="cboBank_SelectedIndexChanged" AutoPostBack="true">             
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        Cheque Book Number:
                    </td>
                    <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txtChequeBookNumber" runat="server" Enabled="false"></asp:TextBox>
                        </ContentTemplate>
                        <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cboBank" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                    </td>
                    
                </tr>
                
                <tr>
                    <td colspan="2">
                        Cheque Number may vary bank to bank, If the cheque numbers in a cheque book contains any Pre Alphabatic Characters before Cheque Number (like "CHK10000" so "CHK" will be your prefix). Please write them in e Prefix Box.<br />
                        Other wise leave the Prefix box Empty and Write the first cheque number and last cheque number in the text boxes given below. Thanks.
                    </td>                    
                </tr>
                <tr>
                    <td>
                        Cheque Number Prefix: (Prefix Character only)
                    </td>
                    <td>
                        <asp:TextBox ID="txtChequeNumberPrefix" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        First Cheque Number: (Only Numeric Value Here.)
                    </td>
                    <td>
                        <asp:TextBox ID="txtFirstChequeNumber" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Last Cheque Number: (Only Numeric Value Here.)
                    </td>
                    <td>
                        <asp:TextBox ID="txtLastChequeNumber" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Issue Date:
                    </td>
                    <td>
                        <asp:TextBox ID="txtIssueDate" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtIssueDate_CalendarExtender" runat="server" 
                            TargetControlID="txtIssueDate">
                        </cc1:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                        End Date:
                    </td>
                    <td>
                        <asp:TextBox ID="txtEndDate" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtEndDate_CalendarExtender" runat="server" 
                            TargetControlID="txtEndDate">
                        </cc1:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                        Is Finished:
                    </td>
                    <td>
                        <asp:CheckBox ID="chkIsFinished" runat="server" ></asp:CheckBox>
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
            <br />
            <asp:DataGrid ID="dgChequeBooks" runat="server" AutoGenerateColumns="False" ShowHeader="True" Width="100%" BackColor="Silver" AllowPaging="true" PageSize="15">
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
                    <asp:TemplateColumn HeaderText="Cheque Book Number">
                        <HeaderStyle Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
			                ForeColor="Black"></HeaderStyle>
				            <ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Left" ForeColor="White"></ItemStyle>
				            <ItemTemplate>							
                                <asp:Panel ID="pnlChequeBookNumber" runat="server">
					                <asp:Label Runat="server" ID="lblChequeBookNumber" Text='<%# DataBinder.Eval(Container.DataItem, "ChequeBookNumber") %>'></asp:Label>
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
					
					<asp:TemplateColumn HeaderText="Edit">
						<HeaderStyle Font-Size="X-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
							ForeColor="Black"></HeaderStyle>
						<ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Center" ForeColor="White"></ItemStyle>
						<ItemTemplate>
						<asp:Panel ID="pnlEdit" runat="server">
							<asp:HyperLink id="LinkButton1" runat="server" NavigateUrl='<%# EditItem(DataBinder.Eval(Container.DataItem, "ID").ToString()) %>'>Edit</asp:HyperLink>
						</asp:Panel>
						</ItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="Delete">
						<HeaderStyle Font-Size="X-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
							ForeColor="Black"></HeaderStyle>
						<ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Center" ForeColor="White"></ItemStyle>
						<ItemTemplate>
						<asp:Panel ID="pnlDelete" runat="server">
							<asp:LinkButton id="Linkbutton2" runat="server" Text='Delete' CommandName="DeleteChequeBook" CommandArgument='<%# DeleteItem(DataBinder.Eval(Container.DataItem, "ID").ToString()) %>'>
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


