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
using System.Data.SqlClient;
using System.IO;


public partial class ManageOrder : System.Web.UI.Page
{
    public bool btnAddWasClicked = false;
    public int btnPressedValue = 0;
    private DataTable dtOrderDG = new DataTable();
    private DataTable dtClientCbo = new DataTable();
    private DataTable dtOrderEdit = new DataTable();
    private DataTable dtOrder = new DataTable();
    private DataTable dtClient = new DataTable();
    private DataTable dtOurCompany = new DataTable();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["OrderMasterID"] != null && Request.QueryString["OrderMasterID"].ToString() != "" && Request.QueryString["OrderDetailID"] != null && Request.QueryString["OrderDetailID"].ToString() != "" && !Page.IsPostBack)
        {
            // This is for Updating and ITEM in Order
            long OrdermasterID = Convert.ToInt32(Request.QueryString["OrderMasterID"].ToString());
            PCSN.InvoiceSystem.BusinessLogicLayer.Order Order = new PCSN.InvoiceSystem.BusinessLogicLayer.Order();
            txtOrderMasterID.Text = Request.QueryString["OrderMasterID"].ToString();
            lblTotalAmount.Text = "0";
            dtOrderEdit = Order.GetOrderByID(OrdermasterID);
            for (int a = 0; a < dtOrderEdit.Rows.Count; a++)
            {
                if (dtOrderEdit.Rows[a]["OrderDetailID"].ToString() == Request.QueryString["OrderDetailID"].ToString())
                {
                    lblDetailDate.Text = Convert.ToDateTime(dtOrderEdit.Rows[a]["OrderDate"].ToString()).ToShortDateString();
                    txtItem.Text = dtOrderEdit.Rows[a]["Item"].ToString();
                    txtItemID.Text = dtOrderEdit.Rows[a]["OrderDetailID"].ToString();                    
                    txtDescription.Text = dtOrderEdit.Rows[a]["Description"].ToString();
                    txtQuantity.Text = dtOrderEdit.Rows[a]["Quantity"].ToString();
                    txtUnitPrice.Text = dtOrderEdit.Rows[a]["UnitPrice"].ToString();
                    txtItemAmount.Text = dtOrderEdit.Rows[a]["ItemAmount"].ToString();
                    cboClientList.SelectedValue = dtOrderEdit.Rows[a]["ClientID"].ToString();
                    txtDueDate.Text = Convert.ToDateTime(dtOrderEdit.Rows[a]["DueDate"].ToString()).ToShortDateString();
                    //lblTotalAmount.Text = dtOrderEdit.Rows[a]["TotalAmount"].ToString();

                    if (dtOrderEdit.Rows[a]["ID"].ToString() != "")
                    {
                        PopulateOrders(Convert.ToInt32(dtOrderEdit.Rows[a]["ID"].ToString()));
                    }
                }

                lblTotalAmount.Text = Convert.ToString(Convert.ToInt32(lblTotalAmount.Text) + Convert.ToInt32(dtOrderEdit.Rows[a]["ItemAmount"].ToString()));
                
                
            }
        }

        if (!Page.IsPostBack)
        {
            if (Request.QueryString["OrderMasterIDEdit"] != null && Request.QueryString["OrderMasterIDEdit"].ToString() != "")
            {
                txtOrderMasterID.Text = Request.QueryString["OrderMasterIDEdit"].ToString();
            }
//            txtOrderMasterID.Text = "16";
            if (txtOrderMasterID.Text == "")
            {               
                // This is when a Fresh Order is About to GeneUnitPrice
                txtClient.Text = "Please Select a Client from Above List";
                
                PCSN.InvoiceSystem.BusinessLogicLayer.OurCompany ourCompany = new PCSN.InvoiceSystem.BusinessLogicLayer.OurCompany();
                dtOurCompany = ourCompany.GetOurCompanyByID(Convert.ToInt32("1"));

                if (dtOurCompany.Rows.Count <= 0)
                {
                    txtOurCompany.Text = "PC.Solutions.NET";
                }
                else
                {
                    txtOurCompanyID.Text = dtOurCompany.Rows[0]["ID"].ToString();
                    txtOurCompany.Text = dtOurCompany.Rows[0]["CompanyName"].ToString() + Environment.NewLine;
                    txtOurCompany.Text = txtOurCompany.Text + dtOurCompany.Rows[0]["CompanyAddress"].ToString() + Environment.NewLine;
                }


                lblHeaderDate.Text = Convert.ToString(DateTime.Now.ToShortDateString());
                lblDetailDate.Text = Convert.ToString(DateTime.Now.ToShortDateString());

                PCSN.InvoiceSystem.BusinessLogicLayer.Order Order = new PCSN.InvoiceSystem.BusinessLogicLayer.Order();

                dtOrder = Order.GetAllOrderMAX();
                if (dtOrder.Rows.Count == 0 || dtOrder.Rows[0]["ID"].ToString() == "")
                {
                    lblOrderNumber.Text = "1";
                }
                else
                {
                    lblOrderNumber.Text = Convert.ToInt32(Convert.ToInt32(dtOrder.Rows[0]["ID"].ToString()) + 1).ToString();                    
                }
                if (txtOrderMasterID.Text != "")
                {
                    PopulateOrders(Convert.ToInt32(txtOrderMasterID.Text));
                }
                PopulateClients();
            }
            else
            {
                // This is when the Order ID is getting from somewhere
                // Like from Query Strings
                long OrdermasterID = Convert.ToInt32(txtOrderMasterID.Text.ToString());
                lblOrderNumber.Text = OrdermasterID.ToString();
                PopulateClients();
                PopulateOrders(OrdermasterID);
                PCSN.InvoiceSystem.BusinessLogicLayer.Order Order = new PCSN.InvoiceSystem.BusinessLogicLayer.Order();

                lblTotalAmount.Text = "0";
                dtOrderEdit = Order.GetOrderByID(OrdermasterID);
                for (int a = 0; a < dtOrderEdit.Rows.Count; a++)
                {
                    if (dtOrderEdit.Rows[a]["ItemAmount"].ToString() == null || dtOrderEdit.Rows[a]["ItemAmount"].ToString() == "")
                    {
                    }
                    else
                    {
                        lblTotalAmount.Text = Convert.ToString(Convert.ToInt32(lblTotalAmount.Text) + Convert.ToInt32(dtOrderEdit.Rows[a]["ItemAmount"].ToString()));
                    }
                }
                if (dtOrderEdit.Rows.Count > 0)
                {
                    //PopulateOrders(Convert.ToInt32(dtOrderEdit.Rows[0]["ID"].ToString()));

                    PCSN.InvoiceSystem.BusinessLogicLayer.Client client = new PCSN.InvoiceSystem.BusinessLogicLayer.Client();
                    dtClient = client.GetClientByID(Convert.ToInt32(dtOrderEdit.Rows[0]["ClientID"].ToString()));

                    if (dtClient.Rows.Count <= 0)
                    {
                        txtClient.Text = "Our Client";
                    }
                    else
                    {
                        cboClientList.SelectedValue = dtClient.Rows[0]["ID"].ToString();
                        txtClientID.Text = dtClient.Rows[0]["ID"].ToString();
                        txtClient.Text = dtClient.Rows[0]["ClientName"].ToString() + Environment.NewLine;
                        txtClient.Text = txtClient.Text + dtClient.Rows[0]["CompanyName"].ToString() + Environment.NewLine;
                        txtClient.Text = txtClient.Text + dtClient.Rows[0]["Address"].ToString() + Environment.NewLine;
                    }

                    txtOurCompanyID.Text = "1";                    
                    txtOurCompany.Text = "Power Protection Services" + Environment.NewLine;
                    txtOurCompany.Text = txtOurCompany.Text + "XYZ Address" + Environment.NewLine;
                


                    lblHeaderDate.Text = Convert.ToString(Convert.ToDateTime(dtOrderEdit.Rows[0]["OrderDate"].ToString()).ToShortDateString());
                    txtDueDate.Text = Convert.ToString(Convert.ToDateTime(dtOrderEdit.Rows[0]["DueDate"].ToString()).ToShortDateString());
                    lblDetailDate.Text = Convert.ToString(Convert.ToDateTime(dtOrderEdit.Rows[0]["OrderDate"].ToString()).ToShortDateString());
                }
                
            }

        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearControlDetail();
        txtItemID.Text = "";        
        lblErrorMessage.Text = "";
    }

    protected void btnSaveOrder_Click(object sender, EventArgs e)
    {
        PCSN.InvoiceSystem.BusinessLogicLayer.Order Order = new PCSN.InvoiceSystem.BusinessLogicLayer.Order();
        long OrderMasterID = 0;
        long OrderDetailID = 0;

        if (txtItemID.Text == "")
        {
            // This runs When a new Order is Created or New Item Add in a Order
            if (txtOrderMasterID.Text != "")
            {
                OrderMasterID = Convert.ToInt32(txtOrderMasterID.Text);
            }
            if (ValidateClientSide())
            {
                if (txtOrderMasterID.Text == "")
                {
                    OrderMasterID = Order.InsertOrderMaster(Convert.ToInt32(lblOrderNumber.Text), lblHeaderDate.Text.ToString(), txtDueDate.Text.ToString(), Convert.ToInt32(txtClientID.Text), lblTotalAmount.Text.ToString());
                    txtOrderMasterID.Text = OrderMasterID.ToString();
                }
                else
                {
                    OrderMasterID = Convert.ToInt32(txtOrderMasterID.Text);
                }
                if (OrderMasterID > 0)
                {
                    txtOrderMasterID.Text = OrderMasterID.ToString();
                    OrderDetailID = Order.InsertOrderDetail(OrderMasterID, txtItem.Text.ToString(), txtDescription.Text.ToString(), Convert.ToInt32(txtQuantity.Text), Convert.ToInt32(txtUnitPrice.Text), Convert.ToInt32(txtItemAmount.Text));
                                        
                    dtOrderEdit = Order.GetOrderByID(OrderMasterID);
                    lblTotalAmount.Text = "0";
                    for (int a = 0; a < dtOrderEdit.Rows.Count; a++)
                    {
                        if (dtOrderEdit.Rows[a]["ItemAmount"].ToString() == null || dtOrderEdit.Rows[a]["ItemAmount"].ToString() == "")
                        {
                        }
                        else
                        {
                            lblTotalAmount.Text = Convert.ToString(Convert.ToInt32(lblTotalAmount.Text) + Convert.ToInt32(dtOrderEdit.Rows[a]["ItemAmount"].ToString()));
                        }
                    }
                    Order.UpdateOrderMaster(Convert.ToInt32(txtOrderMasterID.Text.ToString()), Convert.ToInt32(lblOrderNumber.Text.ToString()), lblHeaderDate.Text.ToString(), txtDueDate.Text.ToString(), Convert.ToInt32(txtClientID.Text.ToString()), lblTotalAmount.Text.ToString());
                }


                if (OrderMasterID > 0)
                {
                    lblErrorMessage.Text = "This Item has been saved.";
                    if (lblTotalAmount.Text == "")
                    {
                        lblTotalAmount.Text = "0";
                    }
                    lblTotalAmount.Text = Convert.ToString(Convert.ToInt32(lblTotalAmount.Text) + Convert.ToInt32(txtItemAmount.Text));
                    ClearControlDetail();
                }
            }            
        }
        else
        {
            // This runs when an Item is being Edited
            if (ValidateClientSide())
            {
                if (txtOrderMasterID.Text != "")
                {
                    OrderMasterID = Convert.ToInt32(txtOrderMasterID.Text);
                    Order.UpdateOrderDetail(Convert.ToInt32(txtItemID.Text), Convert.ToInt32(txtOrderMasterID.Text), txtItem.Text.ToString(), txtDescription.Text.ToString(), Convert.ToInt32(txtQuantity.Text), Convert.ToInt32(txtUnitPrice.Text), Convert.ToInt32(txtItemAmount.Text));
                    
                    dtOrderEdit = Order.GetOrderByID(OrderMasterID);
                    lblTotalAmount.Text = "0";
                    for (int a = 0; a < dtOrderEdit.Rows.Count; a++)
                    {
                        if (dtOrderEdit.Rows[a]["ItemAmount"].ToString() == null || dtOrderEdit.Rows[a]["ItemAmount"].ToString() == "")
                        {
                        }
                        else
                        {
                            lblTotalAmount.Text = Convert.ToString(Convert.ToInt32(lblTotalAmount.Text) + Convert.ToInt32(dtOrderEdit.Rows[a]["ItemAmount"].ToString()));
                        }
                    }
                    Order.UpdateOrderMaster(Convert.ToInt32(txtOrderMasterID.Text.ToString()), Convert.ToInt32(lblOrderNumber.Text.ToString()), lblHeaderDate.Text.ToString(), txtDueDate.Text.ToString(), Convert.ToInt32(txtClientID.Text.ToString()), lblTotalAmount.Text.ToString());
                }
                lblErrorMessage.Text = "This Item has been Updated.";
                ClearControlDetail();
            }
        }

        if (OrderMasterID != 0)
        {
            PopulateOrders(OrderMasterID);
            lblTotalAmount.Text = "0";
            dtOrderEdit = Order.GetOrderByID(OrderMasterID);
            for (int a = 0; a < dtOrderEdit.Rows.Count; a++)
            {
                if (dtOrderEdit.Rows[a]["ItemAmount"].ToString() == null || dtOrderEdit.Rows[a]["ItemAmount"].ToString() == "")
                {
                }
                else
                {
                    lblTotalAmount.Text = Convert.ToString(Convert.ToInt32(lblTotalAmount.Text) + Convert.ToInt32(dtOrderEdit.Rows[a]["ItemAmount"].ToString()));
                }
            }
        }
    }

    protected void btnprintOrder_Click(object sender, EventArgs e)
    {
        if (txtOrderMasterID.Text != "")
            Response.Redirect("printOrder.aspx?OrderMasterID=" + txtOrderMasterID.Text.ToString());

    }
    protected void btncancelOrder_Click(object sender, EventArgs e)
    {
        Response.Redirect("default.aspx");
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        PopulateClients();
        ClearControlDetail();
    }

    private void dgOrders_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {        
        if (e.CommandName == "DeleteOrder")
        {
            string argsID = e.CommandArgument.ToString();
            string[] idSpliter = argsID.Split('/');
            long OrderMasterID = 0;
            long OrderDetailID = 0;
            for (int i = 0; i < idSpliter.Length; i++)
            {
                OrderMasterID = Convert.ToInt32(idSpliter[i].ToString());
                OrderDetailID = Convert.ToInt32(idSpliter[i+1].ToString());
                break;
            }

            if (OrderMasterID > 0 && OrderDetailID>0)
            {
                PCSN.InvoiceSystem.BusinessLogicLayer.Order Order = new PCSN.InvoiceSystem.BusinessLogicLayer.Order();
                Order.DeleteOrderItem(OrderMasterID, OrderDetailID);
                lblErrorMessage.Text = "Item Deleted Successfuly.";
                PopulateOrders(OrderMasterID);
            }
        }        
    }

    private void dgOrders_ItemCreated(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {        
        LinkButton link = (LinkButton)e.Item.FindControl("Linkbutton2");
        if (link != null)
            link.Attributes.Add("onClick", "javascript:return confirm('This action will delete thi Item in Order.  Are you sure you want to delete this Item?');");
        
    }

    protected void cboClientList_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtClient.Text = "";
        long ClientID = Convert.ToInt32(cboClientList.SelectedValue.ToString());
        PCSN.InvoiceSystem.BusinessLogicLayer.Client client = new PCSN.InvoiceSystem.BusinessLogicLayer.Client();
        dtClient = client.GetClientByID(ClientID);

        if (dtClient.Rows.Count <= 0)
        {
            txtClient.Text = "Our Client";
        }
        else
        {
            txtClientID.Text = dtClient.Rows[0]["ID"].ToString();
            txtClient.Text = dtClient.Rows[0]["ClientName"].ToString() + Environment.NewLine;
            txtClient.Text = txtClient.Text + dtClient.Rows[0]["CompanyName"].ToString() + Environment.NewLine;
            txtClient.Text = txtClient.Text + dtClient.Rows[0]["Address"].ToString() + Environment.NewLine;
        }
    }

    #region Page Initialize
    override protected void OnInit(EventArgs e)
    {
        //
        // CODEGEN: This call is required by the ASP.NET Web Form Designer.
        //        
        //GeneUnitPriceControls();        
        txtUnitPrice.Attributes.Add("onBlur", "LostFocus();");        
        InitializeComponent();
        base.OnInit(e);
    }
    private void InitializeComponent()
    {

        this.dgOrders.ItemCreated += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgOrders_ItemCreated);
        this.dgOrders.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgOrders_ItemCommand);       
       
        //this.Load += new System.EventHandler(this.Page_Load);

    }
    #endregion

    #region Methods

    private bool ValidateClientSide()
    {

        bool error = false;
        string message = "";

        if (txtItem.Text.Trim() == "")
        {
            message += "Item is not specified.<br>";
            error = true;
        }                
        if (txtUnitPrice.Text.Trim() == "")
        {
            message += "UnitPrice is not specified.<br>";
            error = true;
        }
        else
        {
            try
            {
                txtUnitPrice.Text = Convert.ToInt32(txtUnitPrice.Text).ToString();
            }
            catch
            {
                message += "Invalid UnitPrice specified.<br>";
                error = true;
            }
        }
        if (txtQuantity.Text.Trim() == "")
        {
            message += "Quantity is not specified.<br>";
            error = true;
        }
        else
        {
            try
            {
                txtQuantity.Text = Convert.ToInt32(txtQuantity.Text).ToString();
            }
            catch
            {
                message += "Invalid Quantity specified.<br>";
                error = true;
            }
        }
        if (txtItemAmount.Text.Trim() == "")
        {
            message += "Amount is not specified.<br>";
            error = true;
        }
        else
        {
            try
            {
                txtItemAmount.Text = Convert.ToInt32(txtItemAmount.Text).ToString();
            }
            catch
            {
                message += "Invalid Amount specified.<br>";
                error = true;
            }
        }
        if(cboClientList.SelectedValue=="0")
        {
            message += "Please Select a Client.<br>";
            error = true;
        }
        try
        {
            txtDueDate.Text = Convert.ToDateTime(txtDueDate.Text).ToShortDateString();
        }
        catch
        {
            message += "Invalid format of Date. It should be MM/DD/YYYY.<br>";
            error = true;
        }
                       
        lblErrorMessage.Text = "";
        if (error)
            lblErrorMessage.Text = message;

        return !error;
    }
        
    private void PopulateClients()
    {
        PCSN.InvoiceSystem.BusinessLogicLayer.Client client = new PCSN.InvoiceSystem.BusinessLogicLayer.Client();
        dtClientCbo = client.GetClientForDropDown("ForEsp");
        cboClientList.DataSource = dtClientCbo;
        cboClientList.DataTextField = "Name";
        cboClientList.DataValueField = "ID";

        cboClientList.DataBind();
    }
    private void PopulateOrders(long OrderMasterID)
    {
        PCSN.InvoiceSystem.BusinessLogicLayer.Order Order = new PCSN.InvoiceSystem.BusinessLogicLayer.Order();
        dtOrderDG = Order.GetOrderByID(OrderMasterID);
        dgOrders.DataSource = dtOrderDG;
        dgOrders.DataBind();
    }

    public string EditItem(string OrderMasterID, string OrderDetailID)
    {        
        return "ManageOrder.aspx?OrderMasterID=" + OrderMasterID + "&OrderDetailID=" + OrderDetailID;        
    }        

    public string DeleteItem(string OrderMasterID,string OrderDetailID)
    {
        return OrderMasterID + "/" +OrderDetailID;
    }

    public void ClearControlDetail()
    {
        txtItem.Text = "";
        txtItemID.Text = "";
        txtDescription.Text = "";
        txtQuantity.Text = "";
        txtUnitPrice.Text="";
        txtItemAmount.Text="";        
    }
    #endregion



}

