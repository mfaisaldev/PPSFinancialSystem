<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SearchDailyExpense.aspx.cs" Inherits="SearchDailyExpense" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Daily Expense Search Page</title>
    <link href="css\formstyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server"> 
    <asp:ScriptManager id="ScrManager" runat="server"></asp:ScriptManager>    
    <div id="container" align="center">
        <div id="header">
            Daily Expense Search Page 
        </div><!-- End Header --> 
        <div id="errormessage">
            <asp:Label ID="lblErrorMessage" runat="server" ></asp:Label>
        </div>
        <div id="form" align="left">
            
            <table id="tblForm">              
                          
                <tr>
                    <td>
                        By Date:
                    </td>
                    <td>
                        <asp:TextBox ID="txtByDate" runat="server" Width="150px"></asp:TextBox>
                        
                        <cc1:CalendarExtender ID="txtByDate_CalendarExtender" runat="server" 
                            TargetControlID="txtByDate">
                        </cc1:CalendarExtender>
                        
                    </td>
                </tr>                       
                <tr>
                    <td>
                        By Month:
                    </td>
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
                    </td>
                </tr>
                <tr>
                    <td>
                        By Year:
                    </td>
                    <td>
                        <asp:DropDownList ID="cboByYear" runat="server" Width="156px">
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
                        <asp:TextBox ID="txtFromDate" runat="server" Width="150px"></asp:TextBox>
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
                        <asp:TextBox ID="txtToDate" runat="server" Width="150px"></asp:TextBox>
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
            <hr width="100%"/>           
            <br />
            <asp:DataGrid ID="dgDailyExps" runat="server" AutoGenerateColumns="False" ShowHeader="True" Width="100%" BackColor="Silver" AllowPaging="true" PageSize="15">
                <HeaderStyle Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center" ForeColor="White" VerticalAlign="Middle"></HeaderStyle>
                <Columns>			                                                           										
                	
                    <asp:TemplateColumn HeaderText="S. No.">
                        <HeaderStyle Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
			                ForeColor="Black"></HeaderStyle>
				            <ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Left" ForeColor="White"></ItemStyle>
				            <ItemTemplate>							
                                <asp:Panel ID="pnlExpNum" runat="server">
					                <asp:Label Runat="server" ID="lblExpNum" Text='<%# SNum(DataBinder.Eval(Container.DataItem, "ID").ToString()) %>'></asp:Label>
                                </asp:Panel>	
                            </ItemTemplate>                                                    
                    </asp:TemplateColumn>    
                    
                    <asp:TemplateColumn HeaderText="Date">
                        <HeaderStyle Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
			                ForeColor="Black"></HeaderStyle>
				            <ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Left" ForeColor="White"></ItemStyle>
				            <ItemTemplate>							
                                <asp:Panel ID="pnlExpDate" runat="server">
					                <asp:Label Runat="server" ID="lblExpDate" Text='<%# DataBinder.Eval(Container.DataItem, "ExpDate") %>'></asp:Label>
                                </asp:Panel>	
                            </ItemTemplate>                                                    
                    </asp:TemplateColumn>  
                    
                    <asp:TemplateColumn HeaderText="Item Description">
                        <HeaderStyle Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
			                ForeColor="Black"></HeaderStyle>
				            <ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Left" ForeColor="White"></ItemStyle>
				            <ItemTemplate>							
                                <asp:Panel ID="pnlItemDesc" runat="server">
					                <asp:Label Runat="server" ID="lblItemDesc" Text='<%# DataBinder.Eval(Container.DataItem, "ItemDesc") %>'></asp:Label>
                                </asp:Panel>	
                            </ItemTemplate>                                                    
                    </asp:TemplateColumn>
                    
                    <asp:TemplateColumn HeaderText="Quantity">
                        <HeaderStyle Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
			                ForeColor="Black"></HeaderStyle>
				            <ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Left" ForeColor="White"></ItemStyle>
				            <ItemTemplate>							
                                <asp:Panel ID="pnlQuantity" runat="server">
					                <asp:Label Runat="server" ID="lblQuantity" Text='<%# DataBinder.Eval(Container.DataItem, "Quantity") %>'></asp:Label>
                                </asp:Panel>	
                            </ItemTemplate>                                                    
                    </asp:TemplateColumn>  
                    
                    <asp:TemplateColumn HeaderText="Unit Price">
                        <HeaderStyle Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
			                ForeColor="Black"></HeaderStyle>
				            <ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Right" ForeColor="White"></ItemStyle>
				            <ItemTemplate>							
                                <asp:Panel ID="pnlUnitPrice" runat="server">
					                <asp:Label Runat="server" ID="lblUnitPrice" Text='<%# DataBinder.Eval(Container.DataItem, "UnitPrice") %>'></asp:Label>
                                </asp:Panel>	
                            </ItemTemplate>                                                    
                    </asp:TemplateColumn>   
                    
                    <asp:TemplateColumn HeaderText="Item Amount">
                        <HeaderStyle Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
			                ForeColor="Black"></HeaderStyle>
				            <ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Right" ForeColor="White"></ItemStyle>
				            <ItemTemplate>							
                                <asp:Panel ID="pnlItemAmount" runat="server">
					                <asp:Label Runat="server" ID="lblItemAmount" Text='<%# DataBinder.Eval(Container.DataItem, "ItemAmount") %>'></asp:Label>
                                </asp:Panel>	
                            </ItemTemplate>                                                    
                    </asp:TemplateColumn>    
                                      					
					<asp:TemplateColumn HeaderText="Detail View">
						<HeaderStyle Font-Size="X-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
							ForeColor="Black"></HeaderStyle>
						<ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Center" ForeColor="White"></ItemStyle>
						<ItemTemplate>
						<asp:Panel ID="pnlShowDetail" runat="server">
							<asp:HyperLink id="Hyperlink1" runat="server" Target=_blank NavigateUrl='<%# ShowDailyExp(DataBinder.Eval(Container.DataItem, "ID").ToString()) %>'>View</asp:HyperLink>
						</asp:Panel>
						</ItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="Edit">
						<HeaderStyle Font-Size="X-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
							ForeColor="Black"></HeaderStyle>
						<ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Center" ForeColor="White"></ItemStyle>
						<ItemTemplate>
						<asp:Panel ID="pnlEdit" runat="server">
							<asp:HyperLink id="LinkButton1" runat="server" NavigateUrl='<%# EditDailyExp(DataBinder.Eval(Container.DataItem, "ID").ToString(),DataBinder.Eval(Container.DataItem, "DailyExpDetailID").ToString()) %>'>Edit</asp:HyperLink>
						</asp:Panel>
						</ItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="Delete">
						<HeaderStyle Font-Size="X-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
							ForeColor="Black"></HeaderStyle>
						<ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Center" ForeColor="White"></ItemStyle>
						<ItemTemplate>
						<asp:Panel ID="pnlDelete" runat="server">
							<asp:LinkButton id="Linkbutton2" runat="server" Text='Delete' CommandName="DeleteDailyExp" CommandArgument='<%# DeleteDailyExp(DataBinder.Eval(Container.DataItem, "ID").ToString()) %>'>
							</asp:LinkButton>
						</asp:Panel>
						</ItemTemplate>
					</asp:TemplateColumn>                                                                      
                    
                </Columns>	
             </asp:DataGrid>
             <br />
             <br />
             <div id="grandtotal" align="center">
                <table id="Table1">                        
                    <tr>
                        <td>
                             Total Cash In Hand:                        
                        </td>
                        <td>
                             <asp:Label ID="lblCashInHand" runat="server"></asp:Label>                        
                        </td>
                        <td>
                             Total Expense:                        
                        </td>
                        <td>
                             <asp:Label ID="lblTotalExpense" runat="server"></asp:Label>                        
                        </td>
                    </tr>  
                </table>
             </div>
         </div><!-- End datagrid -->
            
        </div>
    
    </form>
</body>
</html>



