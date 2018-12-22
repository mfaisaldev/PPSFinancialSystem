<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManageAccReceivable.aspx.cs" Inherits="ManageAccReceivable" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Loan Page</title>
    <link href="css\formstyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server"> 
        <asp:ScriptManager runat="server" ID="ScrptManager"></asp:ScriptManager>
    <div id="container" align="center">
        <div id="header">
            Accounts Receivable Page 
        </div><!-- End Header --> 
        <div id="errormessage">
            <asp:Label ID="lblErrorMessage" runat="server" ></asp:Label>
        </div>
        <div id="form" align="left">
            
            <asp:TextBox ID="txtAccRecID" runat="server" Visible="false"></asp:TextBox>
            
            <table id="tblForm">
                <tr>
                    <td>
                        LoanID:
                    </td>
                    <td>
                        <asp:TextBox ID="txtLoanID" runat="server" Enabled="false"></asp:TextBox>
                    </td>
                </tr>  
                <tr>
                    <td>
                        Total Receivable:
                    </td>
                    <td>
                        <asp:TextBox ID="txtTotalRec" runat="server" Enabled="false"></asp:TextBox>
                    </td>
                </tr>                        
                <tr>
                    <td>
                        Total Amount Collected Yet:
                    </td>
                    <td>
                        <asp:TextBox ID="txtAmountCol" runat="server" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Amount:
                    </td>
                    <td>
                        <asp:TextBox ID="txtAmount" runat="server"></asp:TextBox> Please input the amount you got.
                        <asp:TextBox ID="txtPrvAmount" runat="server" Visible="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Account Receivable Date:
                    </td>
                    <td>
                        <asp:TextBox ID="txtAccRecDate" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtAccRecDate_CalendarExtender" runat="server" 
                            TargetControlID="txtAccRecDate">
                        </cc1:CalendarExtender>
                    </td>
                </tr>                      
                <tr>
                    <td>
                        Description:
                    </td>
                    <td>
                        <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Rows="5" Columns="30"></asp:TextBox>                        
                    </td>
                </tr>                
                <tr>                    
                    <td colspan="2" align="center">
                        <asp:Button ID="btnClose" runat="server" Text="Cancel" OnClick="btnClose_Click" Width="60px"/>                    
                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" Width="60px"/>
                        <asp:Button ID="btnViewReport" runat="server" Text="View Report" OnClick="btnViewReport_Click" Width="80px"/>
                        <asp:Button ID="btnReset" runat="server" Text="Reset" Width="60px" OnClick="btnReset_Click" />
                    </td>
                    
                </tr>
            </table>
            <br />
            <hr width="100%"/>
            <table width="100%">
                <tr>
                    <td align="center">
                        Grid For Account Receivable
                    </td>
                </tr>
            </table>
            <br />
            <asp:DataGrid ID="dgAccReceivable" runat="server" AutoGenerateColumns="False" ShowHeader="True" Width="100%" BackColor="Silver" AllowPaging="true" PageSize="15">
                <HeaderStyle Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center" ForeColor="White" VerticalAlign="Middle"></HeaderStyle>
                <Columns>			                                                           										
                			
                    <asp:TemplateColumn HeaderText="Account Name">
                        <HeaderStyle Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
			                ForeColor="Black"></HeaderStyle>
				            <ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Left" ForeColor="White"></ItemStyle>
				            <ItemTemplate>							
                                <asp:Panel ID="pnlPLoanName" runat="server">
					                <asp:Label Runat="server" ID="lblPLoanName" Text='<%# DataBinder.Eval(Container.DataItem, "Name") %>'></asp:Label>
                                </asp:Panel>	
                            </ItemTemplate>                                                    
                    </asp:TemplateColumn>											
                    
                    <asp:TemplateColumn HeaderText="Receivable Date">
                        <HeaderStyle Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
			                ForeColor="Black"></HeaderStyle>
				            <ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Left" ForeColor="White"></ItemStyle>
				            <ItemTemplate>							
                                <asp:Panel ID="pnlPPayDate" runat="server">
					                <asp:Label Runat="server" ID="lblPPayDate" Text='<%# DataBinder.Eval(Container.DataItem, "AccRecDate") %>'></asp:Label>
                                </asp:Panel>	
                            </ItemTemplate>                                                    
                    </asp:TemplateColumn>   
                    
                    <asp:TemplateColumn HeaderText="Due Date">
                        <HeaderStyle Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
			                ForeColor="Black"></HeaderStyle>
				            <ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Left" ForeColor="White"></ItemStyle>
				            <ItemTemplate>							
                                <asp:Panel ID="pnlPDueDate" runat="server">
					                <asp:Label Runat="server" ID="lblPDueDate" Text='<%# DataBinder.Eval(Container.DataItem, "DueDate") %>'></asp:Label>
                                </asp:Panel>	
                            </ItemTemplate>                                                    
                    </asp:TemplateColumn>   
                    
                    <asp:TemplateColumn HeaderText="Amount Receivable">
                        <HeaderStyle Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
			                ForeColor="Black"></HeaderStyle>
				            <ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Left" ForeColor="White"></ItemStyle>
				            <ItemTemplate>							
                                <asp:Panel ID="pnlPAmountP" runat="server">
					                <asp:Label Runat="server" ID="lblPAmountP" Text='<%# DataBinder.Eval(Container.DataItem, "AmountRec") %>'></asp:Label>
                                </asp:Panel>	
                            </ItemTemplate>                                                    
                    </asp:TemplateColumn>
                    
                    <asp:TemplateColumn HeaderText="Amount Collected">
                        <HeaderStyle Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
			                ForeColor="Black"></HeaderStyle>
				            <ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Left" ForeColor="White"></ItemStyle>
				            <ItemTemplate>							
                                <asp:Panel ID="pnlPAmountPd" runat="server">
					                <asp:Label Runat="server" ID="lblPAmountPd" Text='<%# DataBinder.Eval(Container.DataItem, "Amount") %>'></asp:Label>
                                </asp:Panel>	
                            </ItemTemplate>                                                    
                    </asp:TemplateColumn>    
                    
                    <asp:TemplateColumn HeaderText="Description">
                        <HeaderStyle Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
			                ForeColor="Black"></HeaderStyle>
				            <ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Left" ForeColor="White"></ItemStyle>
				            <ItemTemplate>							
                                <asp:Panel ID="pnlPDescription" runat="server">
					                <asp:Label Runat="server" ID="lblPDescription" Text='<%# DataBinder.Eval(Container.DataItem, "Description") %>'></asp:Label>
                                </asp:Panel>	
                            </ItemTemplate>                                                    
                    </asp:TemplateColumn>                                                               
					
					<asp:TemplateColumn HeaderText="Edit">
						<HeaderStyle Font-Size="X-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
							ForeColor="Black"></HeaderStyle>
						<ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Center" ForeColor="White"></ItemStyle>
						<ItemTemplate>
						<asp:Panel ID="pnlPEdit" runat="server">
							<asp:HyperLink id="PLinkButton1" runat="server" NavigateUrl='<%# EditAccRec(DataBinder.Eval(Container.DataItem, "ID").ToString()) %>'>Edit Item</asp:HyperLink>
						</asp:Panel>
						</ItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="Delete">
						<HeaderStyle Font-Size="X-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
							ForeColor="Black"></HeaderStyle>
						<ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Center" ForeColor="White"></ItemStyle>
						<ItemTemplate>
						<asp:Panel ID="pnlPDelete" runat="server">
							<asp:LinkButton id="PLinkbutton2" runat="server" Text='Delete' CommandName="DeleteLoan" CommandArgument='<%# DeleteAccRec(DataBinder.Eval(Container.DataItem, "ID").ToString()) %>'>
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



