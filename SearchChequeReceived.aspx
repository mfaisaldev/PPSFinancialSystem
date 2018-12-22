<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SearchChequeReceived.aspx.cs" Inherits="SearchChequeReceived" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Bank Page</title>
    <link href="css\formstyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server"> 
    <asp:ScriptManager id="ScrManager" runat="server"></asp:ScriptManager>    
    <div id="container">
        <div id="header">
            Cheque Received Page 
        </div><!-- End Header --> 
        <div id="errormessage">
            <asp:Label ID="lblErrorMessage" runat="server" ></asp:Label>
        </div>
        <div id="form">
            
            <table id="tblForm">                
                <tr>
                    <td>
                        Client:
                    </td>
                    <td>
                        <asp:DropDownList ID="cboClient" runat="server" Width="156px">             
                        </asp:DropDownList>
                    </td>
                </tr>                
                <tr>
                    <td>
                        Received Date:
                    </td>
                    <td>
                        <asp:TextBox ID="txtReceivedDate" runat="server" Width="150px"></asp:TextBox>
                        
                        <cc1:CalendarExtender ID="txtReceivedDate_CalendarExtender" runat="server" 
                            TargetControlID="txtReceivedDate">
                        </cc1:CalendarExtender>
                        
                    </td>
                </tr>
                <tr>
                    <td>
                        Cheque Number:
                    </td>
                    <td>
                        <asp:TextBox ID="txtChequeNumber" runat="server" Width="150px"></asp:TextBox>
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
            Search By:<asp:DropDownList ID="cboChkRecSearch" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cboChkRecSearch_SelectedIndexChanged1">
                <asp:ListItem>Show All</asp:ListItem>
                <asp:ListItem Value="1">Cleared</asp:ListItem>
                <asp:ListItem Value="0">UnCleared</asp:ListItem>
            </asp:DropDownList>
            <br />
            <br />
            <asp:DataGrid ID="dgChequeReceiveds" runat="server" AutoGenerateColumns="False" ShowHeader="True" Width="100%" BackColor="Silver" AllowPaging="true" PageSize="15">
                <HeaderStyle Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center" ForeColor="White" VerticalAlign="Middle"></HeaderStyle>
                <Columns>			                                                           										
                	
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
                    
                    <asp:TemplateColumn HeaderText="Received Date">
                        <HeaderStyle Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
			                ForeColor="Black"></HeaderStyle>
				            <ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Left" ForeColor="White"></ItemStyle>
				            <ItemTemplate>							
                                <asp:Panel ID="pnlReceivedDate" runat="server">
					                <asp:Label Runat="server" ID="lblReceivedDate" Text='<%# DataBinder.Eval(Container.DataItem, "RecDate") %>'></asp:Label>
                                </asp:Panel>	
                            </ItemTemplate>                                                    
                    </asp:TemplateColumn>  
                    
                    <asp:TemplateColumn HeaderText="Amount">
                        <HeaderStyle Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
			                ForeColor="Black"></HeaderStyle>
				            <ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Right" ForeColor="White"></ItemStyle>
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
					
					<asp:TemplateColumn HeaderText="IsCleared">
                        <HeaderStyle Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
			                ForeColor="Black"></HeaderStyle>
				            <ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Center" ForeColor="White"></ItemStyle>
				            <ItemTemplate>							
                                <asp:Panel ID="pnlIsCleared" runat="server">
					                <asp:Label Runat="server" ID="lblIsCleared" Text='<%# IsCleared(DataBinder.Eval(Container.DataItem, "IsCleared").ToString()) %>'></asp:Label>
                                </asp:Panel>	
                            </ItemTemplate>                                                    
                    </asp:TemplateColumn> 
                                       
                    <asp:TemplateColumn HeaderText="Cleared">
						<HeaderStyle Font-Size="X-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
							ForeColor="Black"></HeaderStyle>
						<ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Center" ForeColor="White"></ItemStyle>
						<ItemTemplate>
						<asp:Panel ID="pnlCleared" runat="server">
							<asp:HyperLink id="LinkButton3" runat="server" ForeColor="red" NavigateUrl='<%# ChequeCleared(DataBinder.Eval(Container.DataItem, "ID").ToString(),DataBinder.Eval(Container.DataItem, "IsCleared").ToString()) %>'>Make Cleared</asp:HyperLink>
						</asp:Panel>
						</ItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="Edit">
						<HeaderStyle Font-Size="X-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
							ForeColor="Black"></HeaderStyle>
						<ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Center" ForeColor="White"></ItemStyle>
						<ItemTemplate>
						<asp:Panel ID="pnlEdit" runat="server">
							<asp:HyperLink id="LinkButton1" runat="server" NavigateUrl='<%# EditChequeReceived(DataBinder.Eval(Container.DataItem, "ID").ToString()) %>'>Edit</asp:HyperLink>
						</asp:Panel>
						</ItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="Delete">
						<HeaderStyle Font-Size="X-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
							ForeColor="Black"></HeaderStyle>
						<ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Center" ForeColor="White"></ItemStyle>
						<ItemTemplate>
						<asp:Panel ID="pnlDelete" runat="server">
							<asp:LinkButton id="Linkbutton2" runat="server" Text='Delete' CommandName="DeleteChequeReceived" CommandArgument='<%# DeleteChequeReceived(DataBinder.Eval(Container.DataItem, "ID").ToString()) %>'>
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



