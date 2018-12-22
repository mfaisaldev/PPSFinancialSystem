<%@ Page Language="C#" AutoEventWireup="true" CodeFile="reportingbySearch.aspx.cs" Inherits="reportingbySearch" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Reporting Page</title>
    <link href="css\default.css" rel="stylesheet" type="text/css" />
</head>

<body>
    <form id="form1" runat="server">
    <asp:ScriptManager runat="server" ID="ScrptManager"></asp:ScriptManager>
    <div>
        <div id="header">
            Reporting by Search
        </div>
        
        <div id="errormessage">
            <asp:Label ID="lblErrorMessage" runat="server" ></asp:Label>
        </div>
        
        <div id="searchbox">
            <table>
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
                    </td>
                </tr>           
            
            </table>
        </div>
        
        <div id="datagrid">            
            <br />
            <br />
            <asp:DataGrid ID="dgOrders" runat="server" AutoGenerateColumns="False" ShowHeader="True" Width="100%" BackColor="Silver" >
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
                    
                   <asp:TemplateColumn HeaderText="TotalAmount">
					    <HeaderStyle Font-Size="X-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
						    ForeColor="Black"></HeaderStyle>
					    <ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Right" ForeColor="White"></ItemStyle>					    
					    <ItemTemplate>						    
						    <asp:Label Runat=server ID="lblTotalAmount" Text='<%# CalculateSUM(DataBinder.Eval(Container.DataItem, "TotalAmount").ToString()) %>'>
						    </asp:Label>
					    </ItemTemplate>
				    </asp:TemplateColumn>
				    
                    <asp:TemplateColumn HeaderText="Full Order">
						<HeaderStyle Font-Size="X-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
							ForeColor="Black"></HeaderStyle>
						<ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Center" ForeColor="White"></ItemStyle>
						<ItemTemplate>
						<asp:Panel ID="pnlShowDetail" runat="server">
							<asp:HyperLink id="Hyperlink1" runat="server" Target=_blank NavigateUrl='<%# ShowFullOrder(DataBinder.Eval(Container.DataItem, "ID").ToString()) %>'>View</asp:HyperLink>
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
             <div id="grandtotal">
                <asp:Label ID="Label1" runat="server" >Grand Total = </asp:Label><asp:Label ID="lblGranTotal" runat="server" ></asp:Label>
            </div>
             <div id="lowerbuttons">                
                <asp:Button ID="btncancelOrder" runat="server" Text="Cancel" Width="80px" OnClick="btncancelOrder_Click" />
            </div> 
         </div><!-- End datagrid -->
         
    </div>
    </form>
</body>
</html>

