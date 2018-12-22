<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManageOrder.aspx.cs" Inherits="ManageOrder" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Order System</title>
    <link href="css\default.css" rel="stylesheet" type="text/css" />
    <link href="css\formstyle.css" rel="stylesheet" type="text/css" />
    <script language="javascript">
        function LostFocus()
        {
            var amt = document.getElementById("txtUnitPrice");                 
            var Quantity = document.getElementById("txtQuantity").value;
            var UnitPrice = document.getElementById("txtUnitPrice").value;     
            document.getElementById("txtItemAmount").value = Quantity * UnitPrice;
            
        }        
    </script>
</head>
<body>
    <form id="form1" runat="server">    
        <asp:ScriptManager ID="scmgr" runat="server"></asp:ScriptManager>
        <div id="container" align="center">
                
        
            <div id="header">
                Order 
            </div><!-- End Header -->                  
            
                <div id="formheader">
                    <div id="dateandOrdernumber">
                        <table>
                            <tr>
                                <td><h5>Date :</h5></td>
                                <td><asp:Label ID="lblHeaderDate" runat="server"></asp:Label></td>
                            </tr> 
                            <tr>
                                <td><h5>Order Number :</h5></td>
                                <td><asp:Label ID="lblOrderNumber" runat="server"></asp:Label><asp:TextBox ID="txtOrderMasterID" runat="server" Visible="false" Width="3px"></asp:TextBox></td>
                            </tr> 
                            <tr>
                                <td><h5>Due Date :</h5></td>
                                <td>
                                    <asp:TextBox ID="txtDueDate" runat="server"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtDueDate_CalendarExtender" runat="server" 
                                        TargetControlID="txtDueDate">
                                    </cc1:CalendarExtender>
                                    (MM/DD/YYYY)                                    
                                </td>
                            </tr>                             
                        </table>
                    </div><!-- End dateandOrdernumber --> 
                        <div id="ourcompany" >
                            From Company:<br />
                            <asp:TextBox ID="txtOurCompanyID" runat="server" Visible="false" Width="3px"></asp:TextBox>
                            <asp:TextBox ID="txtOurCompany" runat="server" TextMode="MultiLine" Rows="5" Enabled="false"></asp:TextBox>
                        </div><!-- End ourcompany -->      
                        <div id="client">
                            To Client:<asp:DropDownList ID="cboClientList" runat="server" OnSelectedIndexChanged="cboClientList_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList><br />
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtClientID" runat="server" Visible="false" Width="3px"></asp:TextBox>                        
                                    <asp:TextBox ID="txtClient" runat="server" TextMode="MultiLine" Rows="5" Columns="25"></asp:TextBox>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="cboClientList" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div><!-- End Client -->
                                         
                </div><!-- End Form Header -->
                
                <div id="errormessage">
                    <asp:Label ID="lblErrorMessage" runat="server" ></asp:Label>
                </div>
                
                <div id="detail">
                    <div id="formentry">    
                        <table id="detailtable">
                            <tr>                                
                                <td>Date</td>
                                <td>Item</td>                                
                                <td>Description</td>                            
                                <td>Quantity</td>
                                <td>UnitPrice</td>
                                <td>Amount</td>
                            </tr>
                            <tr>                            
                                <td><asp:Label ID="lblDetailDate" runat="server"></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtItem" runat="server" MaxLength="255"></asp:TextBox>
                                    <asp:TextBox ID="txtItemID" runat="server" Visible="false" Width="3px"></asp:TextBox>
                                </td>                                
                                <td><asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Rows="4" MaxLength="2000"></asp:TextBox></td>                            
                                <td><asp:TextBox ID="txtQuantity" runat="server" Width="50px" MaxLength="4"></asp:TextBox>                                
                                </td>
                                <td><asp:TextBox ID="txtUnitPrice" runat="server" Width="50px" MaxLength="8" ></asp:TextBox></td>
                                <td><asp:TextBox ID="txtItemAmount" runat="server" Width="50px" MaxLength="10"></asp:TextBox></td>
                            </tr>                                                                    
                            <tr>
                                <td colspan="3">
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                                    <asp:Button ID="btnSaveOrder" runat="server" Text="Save This Item" OnClick="btnSaveOrder_Click" />                                    
                                    <asp:Button ID="btnReset" runat="server" Text="Reset" Width="60px" OnClick="btnReset_Click" />
                                </td>
                                <td align="right" colspan="2">
                                    Order Total:
                                </td>
                                <td align="right">
                                    <asp:Label ID="lblTotalAmount" runat="server" ForeColor="white" Font-Bold="true"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    
                    <p></p>
                    <p></p>
                    
                        <asp:DataGrid ID="dgOrders" runat="server" AutoGenerateColumns="False" ShowHeader="True" Width="100%" BackColor="Silver">
			                <HeaderStyle Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center" ForeColor="White" VerticalAlign="Middle"></HeaderStyle>
                            <Columns>			
                            
                                <asp:TemplateColumn HeaderText="Item">
                                    <HeaderStyle Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
						                ForeColor="Black"></HeaderStyle>
							            <ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Left" ForeColor="White"></ItemStyle>
							            <ItemTemplate>							
                                            <asp:Panel ID="pnlItem" runat="server">
								                <asp:Label Runat="server" ID="lblItem" Text='<%# DataBinder.Eval(Container.DataItem, "Item") %>'></asp:Label>
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

                                <asp:TemplateColumn HeaderText="UnitPrice">
                                    <HeaderStyle Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
						                ForeColor="Black"></HeaderStyle>
							            <ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Left" ForeColor="White"></ItemStyle>
							            <ItemTemplate>							
                                            <asp:Panel ID="pnlUnitPrice" runat="server">
                                                <asp:Label Runat="server" ID="lblUnitPrice" Text='<%# DataBinder.Eval(Container.DataItem, "UnitPrice") %>'></asp:Label>
                                            </asp:Panel>
                                        </ItemTemplate>                                                    
                                </asp:TemplateColumn>	
                                
                                <asp:TemplateColumn HeaderText="Amount">
                                    <HeaderStyle Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
						                ForeColor="Black"></HeaderStyle>
							            <ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Left" ForeColor="White"></ItemStyle>
							            <ItemTemplate>							
                                            <asp:Panel ID="pnlAmount" runat="server">
                                                <asp:Label Runat="server" ID="lblAmount" Text='<%# DataBinder.Eval(Container.DataItem, "ItemAmount") %>'></asp:Label>
                                            </asp:Panel>
                                        </ItemTemplate>                                                    
                                </asp:TemplateColumn>	
                                
                                <asp:TemplateColumn HeaderText="Client Name">
                                    <HeaderStyle Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
						                ForeColor="Black"></HeaderStyle>
							            <ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Left" ForeColor="White"></ItemStyle>
							            <ItemTemplate>							
                                            <asp:Panel ID="pnlClientName" runat="server">
                                                <asp:Label Runat="server" ID="lblClientName" Text='<%# DataBinder.Eval(Container.DataItem, "ClientName") %>'></asp:Label>
                                            </asp:Panel>
                                        </ItemTemplate>                                                    
                                </asp:TemplateColumn>	                           										                    
                                								
								<asp:TemplateColumn HeaderText="Edit">
									<HeaderStyle Font-Size="X-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
										ForeColor="Black"></HeaderStyle>
									<ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Center" ForeColor="White"></ItemStyle>
									<ItemTemplate>
									<asp:Panel ID="pnlEdit" runat="server">
										<asp:HyperLink id="LinkButton1" runat="server" NavigateUrl='<%# EditItem(DataBinder.Eval(Container.DataItem, "ID").ToString(),DataBinder.Eval(Container.DataItem, "OrderDetailID").ToString()) %>'>Edit Item</asp:HyperLink>
									</asp:Panel>
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Delete">
									<HeaderStyle Font-Size="X-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
										ForeColor="Black"></HeaderStyle>
									<ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Center" ForeColor="White"></ItemStyle>
									<ItemTemplate>
									<asp:Panel ID="pnlDelete" runat="server">
										<asp:LinkButton id="Linkbutton2" runat="server" Text='Delete' CommandName="DeleteOrder" CommandArgument='<%# DeleteItem(DataBinder.Eval(Container.DataItem, "ID").ToString(),DataBinder.Eval(Container.DataItem, "OrderDetailID").ToString()) %>'>
										</asp:LinkButton>
									</asp:Panel>
									</ItemTemplate>
								</asp:TemplateColumn>                                                                      
                                
                            </Columns>	
                         </asp:DataGrid>
                        <div id="lowerbuttons">                            
                            <asp:Button ID="btnprintOrder" runat="server" Text="Print to HTML" Width="100px" OnClick="btnprintOrder_Click" />
                            <asp:Button ID="btncancelOrder" runat="server" Text="Cancel" Width="100px" OnClick="btncancelOrder_Click" />
                        </div>       
                     </div><!-- End formentry -->
                     
                     
                </div><!-- End Detail -->
                
                
            
                
        </div><!-- End Container -->
    </form>
</body>
</html>
