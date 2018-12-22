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

public partial class reportingbySearch : System.Web.UI.Page
{
    #region Event Handler
    DataTable dtOrderDG = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {

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

    

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string error = "";
        lblGranTotal.Text = "0";
        if (txtFromDate.Text != "" && txtToDate.Text != "")
        {
            string FromDate = txtFromDate.Text.ToString();
            string ToDate = txtToDate.Text.ToString();

            try
            {
                FromDate = Convert.ToDateTime(txtFromDate.Text).ToShortDateString();
                ToDate = Convert.ToDateTime(txtToDate.Text).ToShortDateString();
                PopulateOrdersBySearch("ToAndFromDate", FromDate.ToString() + "#" + ToDate.ToString());
            }
            catch
            {
                error += "Dates must be valid. <br />";
            }
        }
        else if (cboByMonth.SelectedValue != "===Select Month===" && cboByYear.SelectedValue != "===Select Year===")
        {
            string SelMonth = "";
            string SelYear = "";
            try
            {
                SelMonth = cboByMonth.SelectedValue.ToString();
                SelYear = cboByYear.SelectedValue.ToString();

                string dtimeSearch = Convert.ToDateTime(SelMonth.Trim() + "/1/" + SelYear.Trim()).ToShortDateString();

                PopulateOrdersBySearch("OrderByMonth", dtimeSearch.ToString());
            }
            catch
            {
                error += "Month and Years are not in Valid Format. <br />";
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

    private void PopulateOrdersBySearch(string FieldName, string Value)
    {        
        PCSN.InvoiceSystem.BusinessLogicLayer.Order Order = new PCSN.InvoiceSystem.BusinessLogicLayer.Order();
        if (FieldName == "ToAndFromDate")
        {
            string[] ToandFrom = Value.Split('#');
            string toDate = "";
            string fromDate = "";
            for (int i = 0; i < ToandFrom.Length; i++)
            {
                toDate = ToandFrom[i].ToString();
                fromDate = ToandFrom[i+1].ToString();
                break;
            }

            dtOrderDG = Order.GetAllOrdersByToAndFromDate(toDate.ToString(),fromDate.ToString());
        }
        if (FieldName == "OrderByMonth")
        {
            dtOrderDG = Order.GetAllOrdersByMonthAndYear(Value.ToString());
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
        return "printOrder.aspx?OrderMasterID=" + OrderID;
    }

    public string DeleteItem(string OrderID)
    {
        return OrderID;
    }

    public string CalculateSUM(string TotalAmount)
    {
        if (lblGranTotal.Text == "")
        {
            lblGranTotal.Text = "0";
        }
        try
        {
            lblGranTotal.Text = Convert.ToString(Convert.ToInt32(lblGranTotal.Text) + Convert.ToInt32(TotalAmount.ToString()));
        }
            catch{}
        return TotalAmount.ToString();
    }

    #endregion
    
    # region Methods

    private void PopulateOrders()
    {
        PCSN.InvoiceSystem.BusinessLogicLayer.Order Order = new PCSN.InvoiceSystem.BusinessLogicLayer.Order();
        dtOrderDG = Order.GetAllOrders();
        dgOrders.DataSource = dtOrderDG;
        dgOrders.DataBind();
    }

    private void FillMonths()
    {
        cboByMonth.Items.Add("===Select Month===");
        cboByMonth.Items.Add("January");
        cboByMonth.Items.Add("Feburary");
        cboByMonth.Items.Add("March");
        cboByMonth.Items.Add("April");
        cboByMonth.Items.Add("May");
        cboByMonth.Items.Add("June");
        cboByMonth.Items.Add("July");
        cboByMonth.Items.Add("August");
        cboByMonth.Items.Add("September");
        cboByMonth.Items.Add("October");
        cboByMonth.Items.Add("November");
        cboByMonth.Items.Add("December");        
    }
    private void FillYears()
    {
        cboByYear.Items.Add("===Select Year===");
        cboByYear.Items.Add("2013");
        cboByYear.Items.Add("2012");
        cboByYear.Items.Add("2011");
        cboByYear.Items.Add("2010");
        cboByYear.Items.Add("2009");
        cboByYear.Items.Add("2008");
        cboByYear.Items.Add("2007");
        cboByYear.Items.Add("2006");
        cboByYear.Items.Add("2005");        
    }

    #endregion

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

        this.dgOrders.ItemCreated += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgOrders_ItemCreated);
        this.dgOrders.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgOrders_ItemCommand);       

    }
    #endregion

    protected void btncancelOrder_Click(object sender, EventArgs e)
    {
        Response.Redirect("default.aspx", false);
    }
}
