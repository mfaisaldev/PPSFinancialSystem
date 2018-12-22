<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CashRegister.aspx.cs" Inherits="CashRegister" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Cash Page</title>
    <link href="css\formstyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server"> 
    <asp:ScriptManager ID="scmgr" runat="server"></asp:ScriptManager>    
    <div id="container">
        <div id="header">
            Cash Control's Page 
        </div><!-- End Header --> 
        <div id="errormessage">
            <asp:Label ID="lblErrorMessage" runat="server" ></asp:Label>
        </div>
        <div id="form">
            
            <asp:TextBox ID="txtCashInHandID" runat="server" Visible="false"></asp:TextBox>
            
            <table id="tblForm">                
                <tr>
                    <td>
                        Cheque Number:
                    </td>
                    <td>                        
                        <asp:DropDownList ID="cboChequeIssue" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cboChequeIssue_SelectedIndexChanged" Width="155px">             
                        </asp:DropDownList>
                    </td>
                </tr>
                                                               
                <tr>
                    <td>
                        Cash In
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate> 
                            <asp:TextBox ID="txtCashIN" runat="server" Enabled="false"></asp:TextBox>
                            </ContentTemplate>
                            <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cboChequeIssue" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td>
                        Cash Brought Forward
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txtCashBF" runat="server" Enabled="false"></asp:TextBox>
                            </ContentTemplate>
                            <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cboChequeIssue" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>                
                <tr>
                    <td>
                        Cash Available:
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                            <asp:TextBox ID="txtCashAvailable" runat="server" Enabled="false"></asp:TextBox>
                            </ContentTemplate>
                            <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cboChequeIssue" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                
                <tr>
                    <td>
                        Date
                    </td>
                    <td>
                        <asp:TextBox ID="txtDateIN" runat="server" Enabled="false"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtDateIN_CalendarExtender" runat="server" 
                            TargetControlID="txtDateIN">
                        </cc1:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                        Description:
                    </td>
                    <td>
                        <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Rows="5" Columns="25"></asp:TextBox>
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
            <asp:DataGrid ID="dgCashInHands" runat="server" AutoGenerateColumns="False" ShowHeader="True" Width="100%" BackColor="Silver" AllowPaging="true" PageSize="15">
                <HeaderStyle Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center" ForeColor="White" VerticalAlign="Middle"></HeaderStyle>
                <Columns>		                                                      										
                			
                    <asp:TemplateColumn HeaderText="Cheque Number">
                        <HeaderStyle Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
			                ForeColor="Black"></HeaderStyle>
				            <ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Left" ForeColor="White"></ItemStyle>
				            <ItemTemplate>							
                                <asp:Panel ID="pnlChequeNumber" runat="server">
					                <asp:Label Runat="server" ID="lblChequeNumber" Text='<%# DataBinder.Eval(Container.DataItem, "ChequeNumber") %>'></asp:Label>
                                </asp:Panel>	
                            </ItemTemplate>                                                    
                    </asp:TemplateColumn>											
                                        
                    <asp:TemplateColumn HeaderText="Cash IN">
                        <HeaderStyle Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
			                ForeColor="Black"></HeaderStyle>
				            <ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Left" ForeColor="White"></ItemStyle>
				            <ItemTemplate>							
                                <asp:Panel ID="pnlCashIN" runat="server">
					                <asp:Label Runat="server" ID="lblCashIN" Text='<%# DataBinder.Eval(Container.DataItem, "CashIN") %>'></asp:Label>
                                </asp:Panel>	
                            </ItemTemplate>                                                    
                    </asp:TemplateColumn> 
                    <asp:TemplateColumn HeaderText="Total Expense">
                        <HeaderStyle Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
			                ForeColor="Black"></HeaderStyle>
				            <ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Left" ForeColor="White"></ItemStyle>
				            <ItemTemplate>							
                                <asp:Panel ID="pnlTExp" runat="server">
					                <asp:Label Runat="server" ID="lblTExp" Text='<%# DataBinder.Eval(Container.DataItem, "TotalAmount") %>'></asp:Label>
                                </asp:Panel>	
                            </ItemTemplate>                                                    
                    </asp:TemplateColumn>    
                    
                    <asp:TemplateColumn HeaderText="Cash Brought Forward">
                        <HeaderStyle Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
			                ForeColor="Black"></HeaderStyle>
				            <ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Left" ForeColor="White"></ItemStyle>
				            <ItemTemplate>							
                                <asp:Panel ID="pnlCashOUT" runat="server">
					                <asp:Label Runat="server" ID="lblCashOUT" Text='<%# DataBinder.Eval(Container.DataItem, "CashBF") %>'></asp:Label>
                                </asp:Panel>	
                            </ItemTemplate>                                                    
                    </asp:TemplateColumn> 
                    <asp:TemplateColumn HeaderText="Cash Available">
                        <HeaderStyle Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
			                ForeColor="Black"></HeaderStyle>
				            <ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Left" ForeColor="White"></ItemStyle>
				            <ItemTemplate>							
                                <asp:Panel ID="pnlCashAvailable" runat="server">
					                <asp:Label Runat="server" ID="lblCashAvailable" Text='<%# DataBinder.Eval(Container.DataItem, "CashAvailable") %>'></asp:Label>
                                </asp:Panel>	
                            </ItemTemplate>                                                    
                    </asp:TemplateColumn> 
                                      
                    <asp:TemplateColumn HeaderText="Date">
                        <HeaderStyle Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
			                ForeColor="Black"></HeaderStyle>
				            <ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Left" ForeColor="White"></ItemStyle>
				            <ItemTemplate>							
                                <asp:Panel ID="pnlDate" runat="server">
					                <asp:Label Runat="server" ID="lblDate" Text='<%# DataBinder.Eval(Container.DataItem, "DateIN") %>'></asp:Label>
                                </asp:Panel>	
                            </ItemTemplate>                                                    
                    </asp:TemplateColumn>                                                            
					
					<asp:TemplateColumn HeaderText="Edit">
						<HeaderStyle Font-Size="X-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
							ForeColor="Black"></HeaderStyle>
						<ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Center" ForeColor="White"></ItemStyle>
						<ItemTemplate>
						<asp:Panel ID="pnlEdit" runat="server">
							<asp:HyperLink id="LinkButton1" runat="server" NavigateUrl='<%# EditItem(DataBinder.Eval(Container.DataItem, "ID").ToString(),DataBinder.Eval(Container.DataItem, "ChequeIssueID").ToString()) %>'>Edit</asp:HyperLink>
						</asp:Panel>
						</ItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="Delete">
						<HeaderStyle Font-Size="X-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
							ForeColor="Black"></HeaderStyle>
						<ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Center" ForeColor="White"></ItemStyle>
						<ItemTemplate>
						<asp:Panel ID="pnlDelete" runat="server">
							<asp:LinkButton id="Linkbutton2" runat="server" Text='Delete' CommandName="DeleteCashInHand" CommandArgument='<%# DeleteItem(DataBinder.Eval(Container.DataItem, "ID").ToString()) %>'>
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



