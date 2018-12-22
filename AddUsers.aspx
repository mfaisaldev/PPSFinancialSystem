<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddUsers.aspx.cs" Inherits="AddUsers" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>User Page</title>
    <link href="css\formstyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server"> 
        <asp:ScriptManager runat="server" ID="ScrptManager"></asp:ScriptManager>
    <div id="container">
        <div id="header">
            User Page 
        </div><!-- End Header --> 
        <div id="errormessage">
            <asp:Label ID="lblErrorMessage" runat="server" ></asp:Label>
        </div>
        <div id="form">            
            <asp:TextBox ID="txtID" runat="server" Visible="false"></asp:TextBox>
            
            <table id="tblForm">
                <tr>
                    <td>
                        User Name:
                    </td>
                    <td>
                        <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        User ID:
                    </td>
                    <td>
                        <asp:TextBox ID="txtUserID" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Password:
                    </td>
                    <td>
                        <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Confirm Password:
                    </td>
                    <td>
                        <asp:TextBox ID="txtConfirmPassword" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Date:
                    </td>
                    <td>
                        <asp:TextBox ID="txtCreationDate" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtCreationDate_CalendarExtender" runat="server" 
                            TargetControlID="txtCreationDate">
                        </cc1:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                        Description:
                    </td>
                    <td>
                        <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Columns="50" Rows="5"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        User Type:
                    </td>
                    <td>
                        <asp:DropDownList ID="cboUserType" runat="server">
                        <asp:ListItem Text="Select Type"></asp:ListItem>
                        <asp:ListItem Text="Admin"></asp:ListItem>
                        <asp:ListItem Text="Guest"></asp:ListItem>
                        </asp:DropDownList>
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
            <asp:DataGrid ID="dgUsers" runat="server" AutoGenerateColumns="False" ShowHeader="True" Width="100%" BackColor="Silver" AllowPaging="true" PageSize="15">
                <HeaderStyle Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center" ForeColor="White" VerticalAlign="Middle"></HeaderStyle>
                <Columns>			                                                           										
                			
                    <asp:TemplateColumn HeaderText="User Name">
                        <HeaderStyle Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
			                ForeColor="Black"></HeaderStyle>
				            <ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Left" ForeColor="White"></ItemStyle>
				            <ItemTemplate>							
                                <asp:Panel ID="pnlUserName" runat="server">
					                <asp:Label Runat="server" ID="lblUserName" Text='<%# DataBinder.Eval(Container.DataItem, "UserName") %>'></asp:Label>
                                </asp:Panel>	
                            </ItemTemplate>                                                    
                    </asp:TemplateColumn>											
                    
                    <asp:TemplateColumn HeaderText="User ID">
                        <HeaderStyle Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
			                ForeColor="Black"></HeaderStyle>
				            <ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Left" ForeColor="White"></ItemStyle>
				            <ItemTemplate>							
                                <asp:Panel ID="pnlUserID" runat="server">
					                <asp:Label Runat="server" ID="lblUserID" Text='<%# DataBinder.Eval(Container.DataItem, "UserID") %>'></asp:Label>
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
                    
                    <asp:TemplateColumn HeaderText="User Type">
                        <HeaderStyle Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
			                ForeColor="Black"></HeaderStyle>
				            <ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Left" ForeColor="White"></ItemStyle>
				            <ItemTemplate>							
                                <asp:Panel ID="pnlUserType" runat="server">
					                <asp:Label Runat="server" ID="lblUserType" Text='<%# DataBinder.Eval(Container.DataItem, "UserType") %>'></asp:Label>
                                </asp:Panel>	
                            </ItemTemplate>                                                    
                    </asp:TemplateColumn>                                                             
					
					<asp:TemplateColumn HeaderText="Edit">
						<HeaderStyle Font-Size="X-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
							ForeColor="Black"></HeaderStyle>
						<ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Center" ForeColor="White"></ItemStyle>
						<ItemTemplate>
						<asp:Panel ID="pnlEdit" runat="server">
							<asp:HyperLink id="LinkButton1" runat="server" NavigateUrl='<%# EditUser(DataBinder.Eval(Container.DataItem, "ID").ToString()) %>'>Edit</asp:HyperLink>
						</asp:Panel>
						</ItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="Delete">
						<HeaderStyle Font-Size="X-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
							ForeColor="Black"></HeaderStyle>
						<ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Center" ForeColor="White"></ItemStyle>
						<ItemTemplate>
						<asp:Panel ID="pnlDelete" runat="server">
							<asp:LinkButton id="Linkbutton2" runat="server" Text='Delete' CommandName="DeleteUser" CommandArgument='<%# DeleteUser(DataBinder.Eval(Container.DataItem, "ID").ToString()) %>'>
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



