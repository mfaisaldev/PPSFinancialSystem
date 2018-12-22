<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManageStock.aspx.cs" Inherits="ManageStock" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Stock System</title>
    <link href="css\default.css" rel="stylesheet" type="text/css" />
    <link href="css\formstyle.css" rel="stylesheet" type="text/css" />
    <script language="javascript">
        function LostFocus()
        {
            //var amt = document.getElementById("txtUnitPrice");                 
            var Quantity = document.getElementById("txtQuantity").value;
            var UnitPrice = document.getElementById("txtUnitPrice").value;     
            document.getElementById("txtTotalAmount").value = Quantity * UnitPrice;

        }
        function LostFocusQ() {
            //var amt = document.getElementById("txtUnitPrice");
            var Quantity = document.getElementById("txtQuantity").value;
            var UnitPrice = document.getElementById("txtUnitPrice").value;
            document.getElementById("txtTotalAmount").value = Quantity * UnitPrice;

        }         
    </script>
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
        <asp:ScriptManager ID="scmgr" runat="server"></asp:ScriptManager>
        <div id="container" align="center">
                
        
            <div id="header">
                Manage Stock 
            </div><!-- End Header -->                  
            
                <div id="formheader">
                    <div id="dateandStocknumber">
                        <table>
                            <tr>
                                <td><h5>Date :</h5></td>
                                <td><asp:Label ID="lblHeaderDate" runat="server"></asp:Label></td>
                            </tr> 
                            <tr>
                                <td><h5>Item Code :</h5></td>
                                <td valign="middle" style="height:20px;"><asp:Label ID="lblItemCode" runat="server"></asp:Label><asp:TextBox ID="txtItemMasterID" runat="server" Visible="false" Width="3px"></asp:TextBox></td>                            </tr> 
                            <tr>
                                <td><h5>Item Name :</h5></td>
                                <td><asp:TextBox ID="txtItemName" MaxLength="255" runat="server" Width="200px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td><h5>Description :</h5></td>
                                <td>
                                    <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Rows="10" 
                                        Columns="25" MaxLength="500" Height="100px"></asp:TextBox>                                                                        
                                </td>
                            </tr>                             
                        </table>
                    </div><!-- End dateandStocknumber --> 
                    <div id="ourcompany" >
                            
                        </div><!-- End ourcompany -->      
                        <div id="client">
                            
                        </div><!-- End Client -->                     
                </div><!-- End Form Header -->
                
                <div id="errormessage">
                    <asp:Label ID="lblErrorMessage" runat="server" ></asp:Label>
                </div>
                
                <div id="detail">
                    <div id="formentry">    
                        <table id="detailtable">
                            <tr>                                
                                <td>Item Size</td>
                                <td>Color</td>                                
                                <td>Make</td>                            
                                <td>Quantity</td>
                                <td>Unit Price</td>
                                <td>Total Amount</td>
                                <td>Selling Price</td>
                            </tr>
                            <tr>                            
                                <td><asp:TextBox ID="txtItemSize" runat="server" MaxLength="20"></asp:TextBox>
                                    <asp:TextBox ID="txtItemDetailID" runat="server" Visible="false" Width="3px"></asp:TextBox></td>
                                <td>
                                    <asp:TextBox ID="txtColor" runat="server" MaxLength="20"></asp:TextBox>
                                </td>                                
                                <td><asp:TextBox ID="txtMake" runat="server" MaxLength="255"></asp:TextBox></td>
                                                            
                                <td><asp:TextBox ID="txtQuantity" runat="server" Width="50px" MaxLength="4"></asp:TextBox>                                
                                </td>
                                
                                <td><asp:TextBox ID="txtUnitPrice" runat="server" Width="50px" MaxLength="8" ></asp:TextBox></td>
                                <td align="right"><asp:TextBox ID="txtTotalAmount" runat="server" Width="70px" MaxLength="10"></asp:TextBox></td>
                                <td><asp:TextBox ID="txtSellingPrice" runat="server" Width="50px" MaxLength="10"></asp:TextBox></td>
                            </tr>                                                                    
                            <tr>
                                <td colspan="3">
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                                    <asp:Button ID="btnSaveStock" runat="server" Text="Save This Item" OnClick="btnSaveStock_Click" />                                    
                                    <asp:Button ID="btnReset" runat="server" Text="Reset" Width="60px" OnClick="btnReset_Click" />
                                </td>
                                <td align="right" colspan="2">
                                    Item Total Amount:
                                </td>
                                <td align="right">
                                    <asp:Label ID="lblTotalAmount" runat="server" ForeColor="white" Font-Bold="true"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    
                    <p></p>
                    <p></p>
                    
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                            DataSourceID="SqlDataSource1" AllowPaging="True" 
                            AllowSorting="True" OnRowDeleting="GridView1_RowDeleting" 
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
                                
                                <asp:BoundField DataField="ItemSize" HeaderText="ItemSize" 
                                    SortExpression="ItemSize" />
                                <asp:BoundField DataField="Color" HeaderText="Color" SortExpression="Color" />
                                <asp:BoundField DataField="Make" HeaderText="Make" SortExpression="Make" />
                                <asp:BoundField DataField="Quantity" HeaderText="Quantity" 
                                    SortExpression="Quantity" />                                
                                <asp:BoundField DataField="UnitPrice" HeaderText="UnitPrice" 
                                    SortExpression="UnitPrice" />
                                <asp:BoundField DataField="ItemAmount" HeaderText="ItemAmount" 
                                    SortExpression="ItemAmount" />
                                <asp:BoundField DataField="SellingPrice" HeaderText="SellingPrice" 
                                    SortExpression="SellingPrice" />
                                <asp:HyperLinkField DataNavigateUrlFields="ItemDetailID" DataNavigateUrlFormatString="ManageStock.aspx?ItemDetailID={0}"
                                    NavigateUrl="ManageStock.aspx" Text="Edit" HeaderText="Edit" ControlStyle-ForeColor="White" ControlStyle-Font-Bold="true"/>                       
                                <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" ControlStyle-ForeColor="White" ControlStyle-Font-Bold="true"/>
                            </Columns>
                                                                                    
                            <!--#include virtual="GridSetting.html"-->
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" DeleteCommand="Select * from StockMaster"
                            ConnectionString="<%$ ConnectionStrings:PPSConnectionString %>" 
                            SelectCommand="pcsn_GetStockDetailByItemID" SelectCommandType="StoredProcedure">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="txtItemMasterID" DefaultValue="0" 
                                    Name="ItemMasterID" PropertyName="Text" Type="String" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                        <div id="lowerbuttons">                            
                            <asp:Button ID="btnprintStock" runat="server" Text="Print to HTML" Width="100px" OnClick="btnprintStock_Click" />
                            <asp:Button ID="btncancelStock" runat="server" Text="Cancel" Width="100px" OnClick="btncancelStock_Click" />
                        </div>       
                     </div><!-- End formentry -->
                     
                     
                </div><!-- End Detail -->
                
                
            
                
        </div><!-- End Container -->
    </form>
</body>
</html>

