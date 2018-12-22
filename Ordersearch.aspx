<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Ordersearch.aspx.cs" Inherits="Ordersearch" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="css\default.css" rel="stylesheet" type="text/css" />
</head>

<body>
    <form id="form1" runat="server">
    <asp:ScriptManager runat="server" ID="ScrptManager"></asp:ScriptManager>
    <div>
        <div id="header">
        Order Lists
        </div>
        
        <div id="errormessage">
            <asp:Label ID="lblErrorMessage" runat="server" ></asp:Label>
        </div>
        
        <div id="searchbox">
            <table>
                <tr>
                    <td>
                        Order Number :
                    </td>
                    <td>
                        <asp:TextBox ID="txtOrderNumber" runat="server" ></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Date :
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
                    <td colspan="2" align="center">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />                    
                    </td>
                </tr>           
            
            </table>
        </div>
        
        <div id="datagrid">
            Search By:<asp:DropDownList ID="cboOrderSearch" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cboOrderSearch_SelectedIndexChanged1">
                <asp:ListItem>Show All</asp:ListItem>
                <asp:ListItem Value="0">Pending</asp:ListItem>
                <asp:ListItem Value="1">Issues</asp:ListItem>
            </asp:DropDownList>
            <br />
            <br />
            <asp:DataGrid ID="dgOrders" runat="server" AutoGenerateColumns="False" ShowHeader="True" Width="100%" BackColor="Silver" AllowPaging="true" PageSize="15">
                <HeaderStyle Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center" ForeColor="White" VerticalAlign="Middle"></HeaderStyle>
                <Columns>			
                
                    <asp:TemplateColumn HeaderText="Order Number">
                        <HeaderStyle Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
			                ForeColor="Black"></HeaderStyle>
				            <ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Center" ForeColor="White"></ItemStyle>
				            <ItemTemplate>							
                                <asp:Panel ID="pnlOrderNumber" runat="server">
					                <asp:Label Runat="server" ID="lblOrderNumber" Text='<%# DataBinder.Eval(Container.DataItem, "OrderNumber") %>'></asp:Label>
                                </asp:Panel>	
                            </ItemTemplate>                                                    
                    </asp:TemplateColumn>											
                			
                    <asp:TemplateColumn HeaderText="Order Date">
                        <HeaderStyle Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
			                ForeColor="Black"></HeaderStyle>
				            <ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Left" ForeColor="White"></ItemStyle>
				            <ItemTemplate>							
                                <asp:Panel ID="pnlOrderDate" runat="server">
					                <asp:Label Runat="server" ID="lblOrderDate" Text='<%# DataBinder.Eval(Container.DataItem, "OrderDate") %>'></asp:Label>
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
                    
                    <asp:TemplateColumn HeaderText="ClientName">
                        <HeaderStyle Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
			                ForeColor="Black"></HeaderStyle>
				            <ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Left" ForeColor="White"></ItemStyle>
				            <ItemTemplate>							
                                <asp:Panel ID="pnlClientName" runat="server">
					                <asp:Label Runat="server" ID="lblClientName" Text='<%# DataBinder.Eval(Container.DataItem, "ClientName") %>'></asp:Label>
                                </asp:Panel>	
                            </ItemTemplate>                                                    
                    </asp:TemplateColumn>
                   <asp:TemplateColumn HeaderText="Pending">
					    <HeaderStyle Font-Size="X-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
						    ForeColor="Black"></HeaderStyle>
					    <ItemStyle Font-Size="X-Small" Font-Names="Verdana" HorizontalAlign="Center" ForeColor="Black"></ItemStyle>					    
					    <ItemTemplate>
						    <asp:CheckBox id="chkSelected" runat="server"></asp:CheckBox>
						    <asp:Label Runat=server ID="lblHidden" Visible=False Text='<%# DataBinder.Eval(Container.DataItem, "ID") %>'>
						    </asp:Label>
					    </ItemTemplate>
				    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="View Order">
						<HeaderStyle Font-Size="X-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
							ForeColor="Black"></HeaderStyle>
						<ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Center" ForeColor="White"></ItemStyle>
						<ItemTemplate>
						<asp:Panel ID="pnlShowDetail" runat="server">
							<asp:HyperLink id="Hyperlink1" runat="server" Target=_blank NavigateUrl='<%# ShowFullOrder(DataBinder.Eval(Container.DataItem, "ID").ToString()) %>'>View Slip</asp:HyperLink>
						</asp:Panel>
						</ItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="Challan">
						<HeaderStyle Font-Size="X-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
							ForeColor="Black"></HeaderStyle>
						<ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Center" ForeColor="White"></ItemStyle>
						<ItemTemplate>
						<asp:Panel ID="pnlChallan" runat="server">
							<asp:HyperLink id="Hyperlink12" runat="server" Target=_blank NavigateUrl='<%# GenerateChallan(DataBinder.Eval(Container.DataItem, "ID").ToString()) %>'>Generate Challan</asp:HyperLink>
						</asp:Panel>
						</ItemTemplate>
					</asp:TemplateColumn>
														
					<asp:TemplateColumn HeaderText="Edit">
						<HeaderStyle Font-Size="X-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
							ForeColor="Black"></HeaderStyle>
						<ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Center" ForeColor="White"></ItemStyle>
						<ItemTemplate>
						<asp:Panel ID="pnlEdit" runat="server">
							<asp:HyperLink id="LinkButton1" runat="server" NavigateUrl='<%# EditItem(DataBinder.Eval(Container.DataItem, "ID").ToString()) %>'>Edit Order</asp:HyperLink>
						</asp:Panel>
						</ItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="Delete">
						<HeaderStyle Font-Size="X-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
							ForeColor="Black"></HeaderStyle>
						<ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Center" ForeColor="White"></ItemStyle>
						<ItemTemplate>
						<asp:Panel ID="pnlDelete" runat="server">
							<asp:LinkButton id="Linkbutton2" runat="server" Text='Delete' CommandName="DeleteOrder" CommandArgument='<%# DeleteItem(DataBinder.Eval(Container.DataItem, "ID").ToString()) %>'>
							</asp:LinkButton>
						</asp:Panel>
						</ItemTemplate>
					</asp:TemplateColumn>                                                                      
                    
                </Columns>	
             </asp:DataGrid>
             <div id="lowerbuttons">
                <asp:Button ID="btnprintOrder" runat="server" Text="Mark as Pending" Width="150px" OnClick="btnprintOrder_Click" />
                <asp:Button ID="btncancelOrder" runat="server" Text="Cancel" Width="80px" OnClick="btncancelOrder_Click" />
            </div> 
         </div><!-- End datagrid -->
         
    </div>
    </form>
</body>
</html>
