<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManageEmployee.aspx.cs" Inherits="ManageEmployee" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Employee Page</title>
    <link href="css\formstyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server"> 
        
    <div id="container" align="center">
        <div id="header">
            Employee Page 
        </div><!-- End Header --> 
        <div id="errormessage">
            <asp:Label ID="lblErrorMessage" runat="server" ></asp:Label>
        </div>
        <div id="form" align="left">
            
            
            <asp:TextBox ID="txtID" runat="server" Visible="false"></asp:TextBox>
            
            <table id="tblForm">
                <tr>
                    <td>
                        Name:
                    </td>
                    <td>
                        <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Father Name:
                    </td>
                    <td>
                        <asp:TextBox ID="txtFatherName" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Phone:
                    </td>
                    <td>
                        <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Cell Phone:
                    </td>
                    <td>
                        <asp:TextBox ID="txtCellPhone" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Email:
                    </td>
                    <td>
                        <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Address:
                    </td>
                    <td>
                        <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" Rows="5" Columns="25"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        City:
                    </td>
                    <td>
                        <asp:TextBox ID="txtCity" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Country:
                    </td>
                    <td>
                        <asp:TextBox ID="txtCountry" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Designation:
                    </td>
                    <td>
                        
                        <asp:DropDownList ID="cboDesignation" runat="server" Width="155px">
                        <asp:ListItem Text="Select Designation"></asp:ListItem>
                        <asp:ListItem Text="Owners"></asp:ListItem>
                        <asp:ListItem Text="Managers Staff"></asp:ListItem>
                        <asp:ListItem Text="Operational Staff"></asp:ListItem>
                        <asp:ListItem Text="Peons"></asp:ListItem>
                        <asp:ListItem Text="Security Staff"></asp:ListItem>
                        </asp:DropDownList>
                    
                    </td>
                </tr>
                <tr>
                    <td>
                        Salary:
                    </td>
                    <td>
                        <asp:TextBox ID="txtSalary" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Date Of Join:
                    </td>
                    <td>
                        <asp:TextBox ID="txtDateOfJoin" runat="server" Enabled="false"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtDateOfJoin_CalendarExtender" runat="server" 
                            TargetControlID="txtDateOfJoin">
                        </cc1:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                        Reference:
                    </td>
                    <td>
                        <asp:TextBox ID="txtReference" runat="server" TextMode="MultiLine" Rows="5" Columns="25"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        NIC:
                    </td>
                    <td>
                        <asp:TextBox ID="txtNIC" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Any Description:
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
            <asp:DataGrid ID="dgEmployees" runat="server" AutoGenerateColumns="False" ShowHeader="True" Width="100%" BackColor="Silver" AllowPaging="true" PageSize="10">
                <HeaderStyle Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center" ForeColor="White" VerticalAlign="Middle"></HeaderStyle>
                <Columns>			                                                           										                	      								
                                                            
                    <asp:TemplateColumn HeaderText="Employee Name">
                        <HeaderStyle Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
			                ForeColor="Black"></HeaderStyle>
				            <ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Left" ForeColor="White"></ItemStyle>
				            <ItemTemplate>							
                                <asp:Panel ID="pnlEmployeeName" runat="server">
					                <asp:Label Runat="server" ID="lblEmployeeName" Text='<%# DataBinder.Eval(Container.DataItem, "Name") %>'></asp:Label>
                                </asp:Panel>	
                            </ItemTemplate>                                                    
                    </asp:TemplateColumn>		
                    
                    <asp:TemplateColumn HeaderText="Phone">
                        <HeaderStyle Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
			                ForeColor="Black"></HeaderStyle>
				            <ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Left" ForeColor="White"></ItemStyle>
				            <ItemTemplate>							
                                <asp:Panel ID="pnlPhone" runat="server">
					                <asp:Label Runat="server" ID="lblPhone" Text='<%# DataBinder.Eval(Container.DataItem, "Phone") %>'></asp:Label>
                                </asp:Panel>	
                            </ItemTemplate>                                                    
                    </asp:TemplateColumn>  
                    
                    <asp:TemplateColumn HeaderText="Cell Phone">
                        <HeaderStyle Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
			                ForeColor="Black"></HeaderStyle>
				            <ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Left" ForeColor="White"></ItemStyle>
				            <ItemTemplate>							
                                <asp:Panel ID="pnlMobilePhone" runat="server">
					                <asp:Label Runat="server" ID="lblMobilePhone" Text='<%# DataBinder.Eval(Container.DataItem, "Mobile") %>'></asp:Label>
                                </asp:Panel>	
                            </ItemTemplate>                                                    
                    </asp:TemplateColumn> 
                    
                    <asp:TemplateColumn HeaderText="Address">
                        <HeaderStyle Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
			                ForeColor="Black"></HeaderStyle>
				            <ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Left" ForeColor="White"></ItemStyle>
				            <ItemTemplate>							
                                <asp:Panel ID="pnlAddress" runat="server">
					                <asp:Label Runat="server" ID="lblAddress" Text='<%# DataBinder.Eval(Container.DataItem, "Address") %>'></asp:Label>
                                </asp:Panel>	
                            </ItemTemplate>                                                    
                    </asp:TemplateColumn>         
                    
                    <asp:TemplateColumn HeaderText="Designation">
                        <HeaderStyle Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
			                ForeColor="Black"></HeaderStyle>
				            <ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Left" ForeColor="White"></ItemStyle>
				            <ItemTemplate>							
                                <asp:Panel ID="pnlDesignation" runat="server">
					                <asp:Label Runat="server" ID="lblDesignation" Text='<%# DataBinder.Eval(Container.DataItem, "Designation") %>'></asp:Label>
                                </asp:Panel>	
                            </ItemTemplate>                                                    
                    </asp:TemplateColumn>                                                    
					
					<asp:TemplateColumn HeaderText="NIC">
                        <HeaderStyle Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
			                ForeColor="Black"></HeaderStyle>
				            <ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Left" ForeColor="White"></ItemStyle>
				            <ItemTemplate>							
                                <asp:Panel ID="pnlNIC" runat="server">
					                <asp:Label Runat="server" ID="lblNIC" Text='<%# DataBinder.Eval(Container.DataItem, "NIC") %>'></asp:Label>
                                </asp:Panel>	
                            </ItemTemplate>                                                    
                    </asp:TemplateColumn>
                    
                    <asp:TemplateColumn HeaderText="Increament">
                        <HeaderStyle Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
			                ForeColor="Black"></HeaderStyle>
				            <ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Center" ForeColor="White"></ItemStyle>
				            <ItemTemplate>							
                                <asp:Panel ID="pnlIncreament" runat="server">
					                <asp:HyperLink id="LinkButton3" runat="server" ForeColor="red" NavigateUrl='<%# Increament(DataBinder.Eval(Container.DataItem, "SalaryID").ToString(),DataBinder.Eval(Container.DataItem, "ID").ToString()) %>'  Target="_blank">Increament</asp:HyperLink>
                                </asp:Panel>	
                            </ItemTemplate>                                                    
                    </asp:TemplateColumn> 
                    
                    <asp:TemplateColumn HeaderText="Detail View">
                        <HeaderStyle Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
			                ForeColor="Black"></HeaderStyle>
				            <ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Center" ForeColor="White"></ItemStyle>
				            <ItemTemplate>							
                                <asp:Panel ID="pnlView" runat="server">
					                <asp:HyperLink id="LinkButton4" runat="server" NavigateUrl='<%# View(DataBinder.Eval(Container.DataItem, "ID").ToString()) %>'  Target="_blank">View</asp:HyperLink>
                                </asp:Panel>	
                            </ItemTemplate>                                                    
                    </asp:TemplateColumn> 
                    
					<asp:TemplateColumn HeaderText="Edit">
						<HeaderStyle Font-Size="X-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
							ForeColor="Black"></HeaderStyle>
						<ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Center" ForeColor="White"></ItemStyle>
						<ItemTemplate>
						<asp:Panel ID="pnlEdit" runat="server">
							<asp:HyperLink id="LinkButton1" runat="server" NavigateUrl='<%# EditEmployee(DataBinder.Eval(Container.DataItem, "ID").ToString()) %>'>Edit</asp:HyperLink>
						</asp:Panel>
						</ItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="Delete">
						<HeaderStyle Font-Size="X-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
							ForeColor="Black"></HeaderStyle>
						<ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Center" ForeColor="White"></ItemStyle>
						<ItemTemplate>
						<asp:Panel ID="pnlDelete" runat="server">
							<asp:LinkButton id="Linkbutton2" runat="server" Text='Delete' CommandName="DeleteEmployee" CommandArgument='<%# DeleteEmployee(DataBinder.Eval(Container.DataItem, "ID").ToString()) %>'>
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




