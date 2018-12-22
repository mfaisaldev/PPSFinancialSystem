using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


public partial class Ordersearch : System.Web.UI.Page
{
    private DataTable dtOrderDG = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        PopulateOrders();
    }
    protected void btnprintOrder_Click(object sender, EventArgs e)
    {
        PCSN.InvoiceSystem.BusinessLogicLayer.Order Order = new PCSN.InvoiceSystem.BusinessLogicLayer.Order();
        bool checkBoxChecked = false;
        foreach (DataGridItem dg in dgOrders.Items)
        {
            CheckBox chk = (CheckBox)dg.FindControl("chkSelected");
            if (chk.Checked)
            {
                checkBoxChecked = true;
                break;
            }
        }

        if (!checkBoxChecked)
        {
            lblErrorMessage.Text = "No Order selected.";
            return;
        }

        long OrderID = 0;
        foreach (DataGridItem dg in dgOrders.Items)
        {
            CheckBox chk = (CheckBox)dg.FindControl("chkSelected");
            if (chk.Checked)
            {
                Label lbl = (Label)dg.FindControl("lblHidden");
                OrderID = Convert.ToInt32(lbl.Text);
                Order.UpdateOrderMasterForPending(OrderID);
            }
        }
        lblErrorMessage.Text = "Pending Info saved successfully.";
        PopulateOrders();
    }
    protected void btncancelOrder_Click(object sender, EventArgs e)
    {
        Response.Redirect("default.aspx", false);
    }
    protected void cboOrderSearch_SelectedIndexChanged1(object sender, EventArgs e)
    {
        if (cboOrderSearch.SelectedValue == "Show All")
        {
            PopulateOrders();
        }
        else
        {
            PopulateOrdersBySearch("IsPending",cboOrderSearch.SelectedValue.ToString());
        }
    }
    private void dgOrders_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        if (e.CommandName == "DeleteOrder")
        {
            string argsID = e.CommandArgument.ToString();
            
            long OrderID = Convert.ToInt32(argsID);
            

            if (OrderID > 0)
            {
                PCSN.InvoiceSystem.BusinessLogicLayer.Order Order = new PCSN.InvoiceSystem.BusinessLogicLayer.Order();
                Order.DeleteOrder(OrderID);
                lblErrorMessage.Text = "Order Deleted Successfuly.";
                PopulateOrders();
            }
        }
    }

    private void dgOrders_ItemCreated(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        LinkButton link = (LinkButton)e.Item.FindControl("Linkbutton2");
        if (link != null)
            link.Attributes.Add("onClick", "javascript:return confirm('This action will delete whole Order.  Are you sure you want to delete this Order?');");

    }
    private void dgOrders_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dgOrders.CurrentPageIndex = e.NewPageIndex;
        PopulateOrders();
    }
    private void PopulateOrders()
    {
        PCSN.InvoiceSystem.BusinessLogicLayer.Order Order = new PCSN.InvoiceSystem.BusinessLogicLayer.Order();
        dtOrderDG = Order.GetAllOrders();
        dgOrders.DataSource = dtOrderDG;
        dgOrders.DataBind();
    }

    private void PopulateOrdersBySearch(string FieldName, string Value)
    {
        PCSN.InvoiceSystem.BusinessLogicLayer.Order Order = new PCSN.InvoiceSystem.BusinessLogicLayer.Order();
        if(FieldName == "IsPending")
        {
            dtOrderDG = Order.GetAllOrdersBySearch(Convert.ToInt16(Value));
        }
        if(FieldName == "OrderNumber")
        {
            dtOrderDG = Order.GetAllOrdersByOrderNumber(Convert.ToInt32(Value));
        }
        if(FieldName == "OrderDate")
        {
            dtOrderDG = Order.GetAllOrdersByOrderDate(Value);
        }
        dgOrders.DataSource = dtOrderDG;
        dgOrders.DataBind();
    }

    public string EditItem(string OrderID)
    {
        return "ManageOrder.aspx?OrderMasterIDEdit=" + OrderID;
    }

    public string ShowFullOrder(string OrderID)
    {
        return "printOrder.aspx?OrderMasterID=" + OrderID + "&Title=Order Slip";
    }

    public string GenerateChallan(string OrderID)
    {
        return "printOrder.aspx?OrderMasterID=" + OrderID + "&Title=Delivery Challan";
    }

    public string DeleteItem(string OrderID)
    {
        return OrderID;
    }

    #region Page Initialize
    override protected void OnInit(EventArgs e)
    {
        //
        // CODEGEN: This call is required by the ASP.NET Web Form Designer.
        //                
        
        InitializeComponent();
        base.OnInit(e);
    }
    private void InitializeComponent()
    {
        this.dgOrders.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgOrders_PageIndexChanged);
        this.dgOrders.ItemCreated += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgOrders_ItemCreated);
        this.dgOrders.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgOrders_ItemCommand);
        //this.dgOrders.ItemDataBound += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgOrders_ItemDataBound);
        //this.Load += new System.EventHandler(this.Page_Load);

    }
    #endregion



    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string error = "";
        if (txtOrderNumber.Text != "")
        {
            long OrderNumber = 0;
            try
            {
                OrderNumber = Convert.ToInt32(txtOrderNumber.Text);
                PopulateOrdersBySearch("OrderNumber", OrderNumber.ToString());
            }
            catch
            {
                error += "Order must be numeric. <br />";
            }
        }
        else if (txtDate.Text != "")
        {
            string OrderDate = "";
            try
            {
                OrderDate = Convert.ToDateTime(txtDate.Text).ToShortDateString();
                PopulateOrdersBySearch("OrderDate", OrderDate.ToString());
            }
            catch
            {
                error += "Order date is not a Valid Date. <br />";
            }
        }
        else
        {
            PopulateOrders();
        }

        if (error != "")
            lblErrorMessage.Text = error.ToString();
        else
            lblErrorMessage.Text = "";
        
    }
}

