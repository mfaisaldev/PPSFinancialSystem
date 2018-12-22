<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SearchAdvStock.aspx.cs" Inherits="SearchAdvStock" %>


<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Searching Stock Page</title>
    <link href="css\default.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript">

        function SelectAll(id) {
            var frm = document.forms[0];

            for (i = 0; i < frm.elements.length; i++) {
                if (frm.elements[i].type == "checkbox") {
                    frm.elements[i].checked = document.getElementById(id).checked;
                }
            }
        }  


    </script>
</head>

<body>
    <form id="form1" runat="server">
    <asp:ScriptManager runat="server" ID="ScrptManager"></asp:ScriptManager>
    <div>
        <div id="header">
        Searching Stocks
        </div>
        
        <div id="errormessage">
            <asp:Label ID="lblErrorMessage" runat="server" ></asp:Label>
        </div>
        
        <div id="searchbox">
            <table>
                <tr>
                    <td>
                        Item Code :
                    </td>
                    <td>
                        <asp:TextBox ID="txtItemCode" runat="server" ></asp:TextBox>
                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" MinimumPrefixLength="1" ServiceMethod="GetItemCodes" ServicePath="WebService.asmx" TargetControlID="txtItemCode"> </cc1:AutoCompleteExtender>                                                
                        <asp:TextBox ID="txtSearch" runat="server" Visible="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Item Name :
                    </td>
                    <td>
                        <asp:TextBox ID="txtItemName" runat="server" ></asp:TextBox>
                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" MinimumPrefixLength="1" ServiceMethod="GetItemNames" ServicePath="WebService.asmx" TargetControlID="txtItemName"> </cc1:AutoCompleteExtender>                                                
                    </td>
                </tr>
                <tr>
                    <td></td><td>                        
                        <asp:CheckBox ID="chkSize" runat="server" Text="Check to add Size"/>                        
                        <asp:TextBox ID="txtItemSize" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Item Made By :
                    </td>
                    <td>
                        <asp:TextBox ID="txtMadeBy" runat="server" ></asp:TextBox>
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
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnSearch" runat="server" Width="100px" Text="Search" 
                            onclick="btnSearch_Click"/>                    
                    </td>
                </tr>                       
            </table>
            <br />
            <hr />
            <br />            
            <table>
                <tr>
                    <td colspan="2" align="center" style="font-size:medium;">This search box gives u extra power! for example to check the usage of Stock or Entry in Stock during any dates</td>
                </tr>
                </table>
                <table>            
                <tr>
                    <td>
                        From Date :
                    </td>
                    <td>
                        <asp:TextBox ID="txtFromDate" runat="server" ></asp:TextBox>                        
                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" 
                            TargetControlID="txtFromDate">
                        </cc1:CalendarExtender>
                    </td>
                </tr>                
                <tr>
                    <td>
                        To Date :
                    </td>
                    <td>
                        <asp:TextBox ID="txtToDate" runat="server" ></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" 
                            TargetControlID="txtToDate">
                        </cc1:CalendarExtender>                        
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnSearchAdv" runat="server" Width="100px" Text="Search" 
                            onclick="btnSearchAdv_Click" />                    
                    </td>
                </tr>                       
            </table>
        </div>                
        <div id="gridview" style="height:auto;">
        <br />
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                DataSourceID="SqlDataSource1" AllowPaging="True" 
                AllowSorting="True" 
                onRowDataBound="GridView1_RowDataBound" 
                OnRowCreated="GridView1_OnRowCreated" width="100%" 
                DataKeyNames="ItemDetailID" >
                <Columns>                                     
                    <asp:BoundField DataField="ItemDetailID" HeaderText="ItemDetailID" 
                        InsertVisible="False" ReadOnly="True" SortExpression="ItemDetailID" />
                    <asp:TemplateField>

                    <ItemTemplate>

                        <asp:CheckBox ID="CheckBox1" runat="server" />

                    </ItemTemplate>

                    <HeaderTemplate>

                        <asp:CheckBox ID="cbSelectAll" runat="server" Text="" OnClick="selectAll(this)" />

                    </HeaderTemplate>

                    <HeaderStyle HorizontalAlign="Left" />

                    <ItemStyle HorizontalAlign="Left" />

                    </asp:TemplateField>
                    <asp:BoundField DataField="ItemCode" HeaderText="ItemCode" 
                        SortExpression="ItemCode" />
                    <asp:BoundField DataField="ItemName" HeaderText="ItemName" 
                        SortExpression="ItemName" />
                    <asp:BoundField DataField="IDate" HeaderText="Creation Date" 
                        SortExpression="IDate" ReadOnly="True" />                    
                    <asp:BoundField DataField="ItemSize" HeaderText="Size" 
                        SortExpression="ItemSize" />
                    <asp:BoundField DataField="Color" HeaderText="Color" 
                        SortExpression="Color" />
                    <asp:BoundField DataField="Make" HeaderText="Make" 
                        SortExpression="Make" />
                    <asp:BoundField DataField="Quantity" HeaderText="CurrentQuantity" 
                        SortExpression="Quantity" />                    
                    <asp:BoundField DataField="EnterQuantity" HeaderText="Entered Quantity" 
                        SortExpression="EnterQuantity" />
                    <asp:BoundField DataField="EnterDate" HeaderText="EnterDate" ReadOnly="True" 
                        SortExpression="EnterDate" />
                    <asp:BoundField DataField="EnterDesc" HeaderText="EnterDesc" 
                        SortExpression="EnterDesc" />
                    <asp:HyperLinkField DataNavigateUrlFields="ItemDetailID" DataNavigateUrlFormatString="ManageStock.aspx?ItemDetailID={0}"
                            NavigateUrl="ManageStock.aspx" Text="Edit" HeaderText="Edit" ControlStyle-ForeColor="White" ControlStyle-Font-Bold="true"/>                       
                    
                </Columns>
                <!--#include virtual="GridSetting.html"-->
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="<%$ ConnectionStrings:PPSConnectionString %>" 
                SelectCommand="pcsn_GetStockDetailByAdvSearch" 
                SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:ControlParameter ControlID="txtSearch" 
                        DefaultValue="[StockMaster].[ID] &gt; 0" Name="Search" 
                        PropertyName="Text" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
            <br /><hr /><br />
            
            <div id="lowerbuttons">
                <asp:Button ID="btnprintOrder" runat="server" Text="Mark as Pending" Width="150px"/>
                <asp:Button ID="btncancelOrder" runat="server" Text="Cancel" Width="80px"/>
                 
            </div> 
         </div><!-- End datagrid -->
         
    </div>
    </form>
</body>
</html>

