<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ATMTrans.aspx.cs" Inherits="ATMTrans" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>ATM Page</title>
    <link href="css\formstyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server"> 
        <asp:ScriptManager ID="scmgr" runat="server"></asp:ScriptManager>
    <div id="container" align="center">
        <div id="header">
            ATM Transaction Page 
        </div><!-- End Header --> 
        <div id="errormessage">
            <asp:Label ID="lblErrorMessage" runat="server" ></asp:Label>            
        </div>
        <div id="form" align="left">
            
            <asp:TextBox ID="txtATMTransID" runat="server" Visible="false"></asp:TextBox>
            
            <table id="tblForm">
                <tr>
                    <td valign="top">
                        ATM Card:<br />
                        Available Balance:
                    </td>
                    <td>
                        <asp:DropDownList ID="cboATMCard" runat="server" OnSelectedIndexChanged="cboATMCard_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList><br />
                         <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txtAvBalance" runat="server" Enabled="false"></asp:TextBox>
                                <asp:TextBox ID="txtBankID" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtBBalID" runat="server" Visible="false"></asp:TextBox>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="cboATMCard" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                        
                    </td>
                </tr>                                
                <tr>
                    <td>
                        Transaction ID (If Any):
                    </td>
                    <td>
                        <asp:TextBox ID="txtTransID" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Amount:
                    </td>
                    <td>
                        <asp:TextBox ID="txtAmount" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtPrevAmount" runat="server" Visible="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Trans Date:
                    </td>
                    <td>
                        <asp:TextBox ID="txtTransDate" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtTransDate_CalendarExtender" runat="server" 
                            TargetControlID="txtTransDate" >
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
                        <asp:Button ID="btnReset" runat="server" Text="Reset" Width="60px" OnClick="btnReset_Click" />
                    </td>
                    
                </tr>
            </table>
            <br />
            <hr width="100%"/>
            <br />
            <asp:DataGrid ID="dgATMTranss" runat="server" AutoGenerateColumns="False" ShowHeader="True" Width="100%" BackColor="Silver" AllowPaging="true" PageSize="15">
                <HeaderStyle Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center" ForeColor="White" VerticalAlign="Middle"></HeaderStyle>
                <Columns>			                                                           										
                			
                    <asp:TemplateColumn HeaderText="Bank Name">
                        <HeaderStyle Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
			                ForeColor="Black"></HeaderStyle>
				            <ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Left" ForeColor="White"></ItemStyle>
				            <ItemTemplate>							
                                <asp:Panel ID="pnlBankName" runat="server">
					                <asp:Label Runat="server" ID="lblBankName" Text='<%# DataBinder.Eval(Container.DataItem, "BankName") %>'></asp:Label>
                                </asp:Panel>	
                            </ItemTemplate>                                                    
                    </asp:TemplateColumn>											
                                        
                    <asp:TemplateColumn HeaderText="Account Number">
                        <HeaderStyle Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
			                ForeColor="Black"></HeaderStyle>
				            <ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Left" ForeColor="White"></ItemStyle>
				            <ItemTemplate>							
                                <asp:Panel ID="pnlAccountNumber" runat="server">
					                <asp:Label Runat="server" ID="lblAccountNumber" Text='<%# DataBinder.Eval(Container.DataItem, "AccountNumber") %>'></asp:Label>
                                </asp:Panel>	
                            </ItemTemplate>                                                    
                    </asp:TemplateColumn>                                     
                    
                    <asp:TemplateColumn HeaderText="CardNumber">
                        <HeaderStyle Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
			                ForeColor="Black"></HeaderStyle>
				            <ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Left" ForeColor="White"></ItemStyle>
				            <ItemTemplate>							
                                <asp:Panel ID="pnlCardNumber" runat="server">
					                <asp:Label Runat="server" ID="lblCardNumber" Text='<%# DataBinder.Eval(Container.DataItem, "CardNumber") %>'></asp:Label>
                                </asp:Panel>	
                            </ItemTemplate>                                                    
                    </asp:TemplateColumn>    
                    
                    <asp:TemplateColumn HeaderText="Amount">
                        <HeaderStyle Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
			                ForeColor="Black"></HeaderStyle>
				            <ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Left" ForeColor="White"></ItemStyle>
				            <ItemTemplate>							
                                <asp:Panel ID="pnlAmount" runat="server">
					                <asp:Label Runat="server" ID="lblAmount" Text='<%# DataBinder.Eval(Container.DataItem, "Amount") %>'></asp:Label>
                                </asp:Panel>	
                            </ItemTemplate>                                                    
                    </asp:TemplateColumn>  
                    
                    <asp:TemplateColumn HeaderText="Date">
                        <HeaderStyle Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
			                ForeColor="Black"></HeaderStyle>
				            <ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Left" ForeColor="White"></ItemStyle>
				            <ItemTemplate>							
                                <asp:Panel ID="pnlTransDate" runat="server">
					                <asp:Label Runat="server" ID="lblTransDate" Text='<%# DataBinder.Eval(Container.DataItem, "TransDate") %>'></asp:Label>
                                </asp:Panel>	
                            </ItemTemplate>                                                    
                    </asp:TemplateColumn>                                                            
					
					<asp:TemplateColumn HeaderText="Edit">
						<HeaderStyle Font-Size="X-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
							ForeColor="Black"></HeaderStyle>
						<ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Center" ForeColor="White"></ItemStyle>
						<ItemTemplate>
						<asp:Panel ID="pnlEdit" runat="server">
							<asp:HyperLink id="LinkButton1" runat="server" NavigateUrl='<%# EditATMTrans(DataBinder.Eval(Container.DataItem, "ID").ToString()) %>'>Edit</asp:HyperLink>
						</asp:Panel>
						</ItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="Delete">
						<HeaderStyle Font-Size="X-Small" Font-Names="Verdana" Font-Bold="True" HorizontalAlign="Center"
							ForeColor="Black"></HeaderStyle>
						<ItemStyle Font-Size="Small" Font-Names="Verdana" HorizontalAlign="Center" ForeColor="White"></ItemStyle>
						<ItemTemplate>
						<asp:Panel ID="pnlDelete" runat="server">
							<asp:LinkButton id="Linkbutton2" runat="server" Text='Delete' CommandName="DeleteATMTrans" CommandArgument='<%# DeleteATMTrans(DataBinder.Eval(Container.DataItem, "ID").ToString()) %>'>
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




