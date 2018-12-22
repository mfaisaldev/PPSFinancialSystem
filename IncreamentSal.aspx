<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IncreamentSal.aspx.cs" Inherits="IncreamentSal" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Increament Salary Page</title>
    <link href="css\formstyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server"> 
        <asp:ScriptManager runat="server" ID="ScrptManager"></asp:ScriptManager>
    <div id="container" align="center">
        <div id="header">
            Increament Salary Page 
        </div><!-- End Header --> 
        <div id="errormessage">
            <asp:Label ID="lblErrorMessage" runat="server" ></asp:Label>
        </div>
        <div id="form" align="left">
            
            
            <asp:TextBox ID="txtSalID" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txtIncSalID" runat="server" Visible="false"></asp:TextBox>
            
            <table id="tblForm">
                <tr>
                    <td>
                        Employee Name:
                    </td>
                    <td>
                        <asp:TextBox ID="txtEmpID" runat="server" Visible="false"></asp:TextBox>
                        <asp:TextBox ID="txtEmployeeName" runat="server" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Increament Date:
                    </td>
                    <td>
                        <asp:TextBox ID="txtIncreamentDate" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtIncreamentDate_CalendarExtender" runat="server" 
                            TargetControlID="txtIncreamentDate">
                        </cc1:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                        Amount to be Increased:
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
                        <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Columns="50" Rows="5"></asp:TextBox>
                    </td>
                </tr>                
                <tr>                    
                    <td colspan="2" align="center">
                        <asp:Button ID="btnClose" runat="server" Text="Close" Width="60px"/>                    
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="60px" OnClick="btnCancel_Click"/>
                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" Width="60px"/>                        
                        <asp:Button ID="btnReset" runat="server" Text="Reset" Width="60px" OnClick="btnReset_Click" />
                    </td>
                    
                </tr>
            </table>
            <br />
            <hr width="100%"/>
            <br />
            <asp:DataGrid ID="dgIncreamentSalarys" runat="server" AutoGenerateColumns="False" ShowHeader="True" Width="100%" BackColor="Silver">
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
                    
                    <asp:TemplateColumn HeaderText="Increament Date">
                        <HeaderStyle Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
			                ForeColor="Black"></HeaderStyle>
				            <ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Left" ForeColor="White"></ItemStyle>
				            <ItemTemplate>							
                                <asp:Panel ID="pnlIncreamentDate" runat="server">
					                <asp:Label Runat="server" ID="lblIncreamentDate" Text='<%# DataBinder.Eval(Container.DataItem, "IncDate") %>'></asp:Label>
                                </asp:Panel>	
                            </ItemTemplate>                                                    
                    </asp:TemplateColumn>  
                    
                    <asp:TemplateColumn HeaderText="Increament Amount">
                        <HeaderStyle Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
			                ForeColor="Black"></HeaderStyle>
				            <ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Left" ForeColor="White"></ItemStyle>
				            <ItemTemplate>							
                                <asp:Panel ID="pnlIncreamentAmount" runat="server">
					                <asp:Label Runat="server" ID="lblIncreamentAmount" Text='<%# DataBinder.Eval(Container.DataItem, "IncAmount") %>'></asp:Label>
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
					
					<asp:TemplateColumn HeaderText="Edit">
						<HeaderStyle Font-Size="X-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
							ForeColor="Black"></HeaderStyle>
						<ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Center" ForeColor="White"></ItemStyle>
						<ItemTemplate>
						<asp:Panel ID="pnlEdit" runat="server">
							<asp:HyperLink id="LinkButton1" runat="server" NavigateUrl='<%# EditIncreamentSalary(DataBinder.Eval(Container.DataItem, "IncSalID").ToString()) %>'>Edit</asp:HyperLink>
						</asp:Panel>
						</ItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="Delete">
						<HeaderStyle Font-Size="X-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
							ForeColor="Black"></HeaderStyle>
						<ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Center" ForeColor="White"></ItemStyle>
						<ItemTemplate>
						<asp:Panel ID="pnlDelete" runat="server">
							<asp:LinkButton id="Linkbutton2" runat="server" Text='Delete' CommandName="DeleteIncreamentSalary" CommandArgument='<%# DeleteIncreamentSalary(DataBinder.Eval(Container.DataItem, "IncSalID").ToString()) %>'>
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