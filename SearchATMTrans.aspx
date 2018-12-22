<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SearchATMTrans.aspx.cs" Inherits="SearchATMTrans" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Search Page for ATM Transactions</title>
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
        ATM Transactions Lists
        </div>
        
        <div id="errormessage">
            <asp:Label ID="lblErrorMessage" runat="server" ></asp:Label>
        </div>
        
        <div id="searchbox">
            <table>
                <tr>
                    <td valign="top">
                        ATM Card:<br />
                        Available Balance:
                    </td>
                    <td>
                        <asp:DropDownList ID="cboATMCard" runat="server" OnSelectedIndexChanged="cboATMCard_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList><br />
                        <asp:TextBox ID="txtAvBalance" runat="server" Enabled="false"></asp:TextBox>
                        <asp:TextBox ID="txtBankID" runat="server" Visible="false"></asp:TextBox>
                        <asp:TextBox ID="txtBBalID" runat="server" Visible="false"></asp:TextBox>
                        <asp:TextBox ID="txtAmount" runat="server" Visible="false"></asp:TextBox>                        
                    </td>
                </tr>                             
                <tr>
                    <td>
                        Transaction Date :
                    </td>
                    <td>
                        <asp:TextBox ID="txtDate" runat="server" ></asp:TextBox>
                        <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server" 
                            TargetControlID="txtDate">
                        </cc1:CalendarExtender>
                        (MM/DD/YY)
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnSearch" runat="server" Text="Search"/>                    
                    </td>
                </tr>           
            
            </table>
        </div>
        
        <div id="gridview">            
            <br />
            <br />
            <br />
            
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                DataKeyNames="ID" DataSourceID="SqlDataSource1" AllowPaging="True" 
                AllowSorting="True" OnRowDeleting="GridView1_RowDeleting" 
                onRowDataBound="GridView1_RowDataBound" 
                OnRowCreated="GridView1_OnRowCreated" width="100%" >
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="ID" />                        
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
                    
                    <asp:BoundField DataField="AccountNumber" HeaderText="AccountNumber" 
                        SortExpression="AccountNumber" />
                    <asp:BoundField DataField="BankName" HeaderText="BankName" 
                        SortExpression="BankName" />
                    <asp:BoundField DataField="CardNumber" HeaderText="CardNumber" 
                        SortExpression="CardNumber" />
                    <asp:BoundField DataField="TransactionID" HeaderText="TransactionID" 
                        SortExpression="TransactionID" />
                    <asp:BoundField DataField="Amount" HeaderText="Amount" 
                        SortExpression="Amount" />
                    <asp:BoundField DataField="TransDate" HeaderText="TransDate" ReadOnly="True" 
                        SortExpression="TransDate" />
                    <asp:BoundField DataField="TransDesc" HeaderText="TransDesc" 
                        SortExpression="TransDesc" />
                    <asp:HyperLinkField DataNavigateUrlFields="ID" DataNavigateUrlFormatString="ATMTrans.aspx?ATMTransIDED={0}"
                                NavigateUrl="ATMTrans.aspx" Text="Edit" HeaderText="Edit" ControlStyle-ForeColor="Blue"/>                       
                    <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" ControlStyle-ForeColor="Blue" />
                </Columns>
                <!--#include virtual="GridSetting.html"-->
                
                
            </asp:GridView>
            
             <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="<%$ ConnectionStrings:PPSConnectionString %>" 
                SelectCommand="pcsn_GetATMTransByATMCardID" SelectCommandType="StoredProcedure">
                 <SelectParameters>
                     <asp:ControlParameter ControlID="cboATMCard" Name="ATMCardID" 
                         PropertyName="SelectedValue" Type="Int32" DefaultValue="0" />
                 </SelectParameters>
            </asp:SqlDataSource>
            
             <div id="lowerbuttons">
                <asp:Button ID="btnprintOrder" runat="server" Text="Print" Width="80px" />
                <asp:Button ID="btncancelOrder" runat="server" Text="Cancel" Width="80px" 
                     onclick="btncancelOrder_Click" />
            </div> 
         </div><!-- End datagrid -->
         
    </div>
    </form>
</body>
</html>

