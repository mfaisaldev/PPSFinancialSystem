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


public partial class ManageStock : System.Web.UI.Page
{
    public bool btnAddWasClicked = false;
    public int btnPressedValue = 0;
    private DataTable dtStockDG = new DataTable();    
    private DataTable dtStockEdit = new DataTable();
    private DataTable dtStock = new DataTable();    

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Request.QueryString["ItemMasterID"] != null && Request.QueryString["ItemMasterID"].ToString() != "" && Request.QueryString["ItemDetailID"] != null && Request.QueryString["ItemDetailID"].ToString() != "" && !Page.IsPostBack)
        if (Request.QueryString["ItemDetailID"] != null && Request.QueryString["ItemDetailID"].ToString() != "" && !Page.IsPostBack)
        {
            // This is for Updating and ITEM in Stock
            long ItemDetailrID = Convert.ToInt32(Request.QueryString["ItemDetailID"].ToString());
            PCSN.InvoiceSystem.BusinessLogicLayer.Stock Stock = new PCSN.InvoiceSystem.BusinessLogicLayer.Stock();
            //txtItemMasterID.Text = Request.QueryString["ItemMasterID"].ToString();
            lblTotalAmount.Text = "0";
            dtStockEdit = Stock.GetStockDetailByID(ItemDetailrID);
            for (int a = 0; a < dtStockEdit.Rows.Count; a++)
            {
                //if (dtStockEdit.Rows[a]["ItemDetailID"].ToString() == Request.QueryString["ItemDetailID"].ToString())
                //{                    
                    txtItemSize.Text = dtStockEdit.Rows[a]["ItemSize"].ToString();
                    txtItemDetailID.Text = dtStockEdit.Rows[a]["ItemDetailID"].ToString();
                    txtColor.Text = dtStockEdit.Rows[a]["Color"].ToString();
                    txtMake.Text = dtStockEdit.Rows[a]["Make"].ToString();
                    txtQuantity.Text = dtStockEdit.Rows[a]["Quantity"].ToString();
                    txtUnitPrice.Text = dtStockEdit.Rows[a]["UnitPrice"].ToString();
                    txtTotalAmount.Text = dtStockEdit.Rows[a]["ItemAmount"].ToString();
                    txtSellingPrice.Text = dtStockEdit.Rows[a]["SellingPrice"].ToString();
                    txtItemMasterID.Text = dtStockEdit.Rows[a]["ItemMasterID"].ToString();
                    //if (dtStockEdit.Rows[a]["ItemDetailID"].ToString() != "")
                    //{
                    //    PopulateStocks(Convert.ToInt32(dtStockEdit.Rows[a]["ItemDetailID"].ToString()));
                    //}
                //}

                //lblTotalAmount.Text = Convert.ToString(Convert.ToInt32(lblTotalAmount.Text) + Convert.ToInt32(dtStockEdit.Rows[a]["Quantity"].ToString()) * Convert.ToInt32(dtStockEdit.Rows[a]["UnitPrice"].ToString()));


            }
        }

        if (!Page.IsPostBack)
        {
            
                if (Request.QueryString["ItemMasterIDEdit"] != null && Request.QueryString["ItemMasterIDEdit"].ToString() != "")
                {
                    txtItemMasterID.Text = Request.QueryString["ItemMasterIDEdit"].ToString();
                }
                //            txtItemMasterID.Text = "16";
                if (txtItemMasterID.Text == "")
                {
                    // This is when a Fresh Stock is About to GeneUnitPrice

                    lblHeaderDate.Text = Convert.ToString(DateTime.Now.ToShortDateString());
                    // Item Code Generator
                    //lblDetailDate.Text = Convert.ToString(DateTime.Now.ToShortDateString());

                    PCSN.InvoiceSystem.BusinessLogicLayer.Stock Stock = new PCSN.InvoiceSystem.BusinessLogicLayer.Stock();

                    dtStock = Stock.GetAllStockMAX();
                    if (dtStock.Rows.Count == 0 || dtStock.Rows[0]["ItemMasterID"].ToString() == "")
                    {
                        lblItemCode.Text = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + "--1";
                        lblTotalAmount.Text = "0";
                    }
                    else
                    {
                        lblItemCode.Text = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + "--" + Convert.ToInt32(Convert.ToInt32(dtStock.Rows[0]["ItemMasterID"].ToString()) + 1).ToString();
                        lblTotalAmount.Text = "0";
                    }
                    if (txtItemMasterID.Text != "")
                    {
                        PopulateStocks(Convert.ToInt32(txtItemMasterID.Text));
                    }
                }
                else
                {
                    // This is when the Stock ID is getting from somewhere
                    // Like from Query Strings
                    long ItemMasterID = Convert.ToInt32(txtItemMasterID.Text.ToString());
                    //lblStockNumber.Text = ItemMasterID.ToString();

                    PopulateStocks(ItemMasterID);
                    PCSN.InvoiceSystem.BusinessLogicLayer.Stock Stock = new PCSN.InvoiceSystem.BusinessLogicLayer.Stock();

                    lblTotalAmount.Text = "0";
                    dtStockEdit = Stock.GetStockDetailByItemID(ItemMasterID);
                    for (int a = 0; a < dtStockEdit.Rows.Count; a++)
                    {
                        if (dtStockEdit.Rows[a]["TotalAmount"].ToString() == null || dtStockEdit.Rows[a]["TotalAmount"].ToString() == "")
                        {
                        }
                        else
                        {
                            lblTotalAmount.Text = Convert.ToString(Convert.ToInt32(lblTotalAmount.Text) + Convert.ToInt32(dtStockEdit.Rows[a]["Quantity"].ToString()) * Convert.ToInt32(dtStockEdit.Rows[a]["UnitPrice"].ToString()));
                        }
                    }
                    if (dtStockEdit.Rows.Count > 0)
                    {
                        //PopulateStocks(Convert.ToInt32(dtStockEdit.Rows[0]["ID"].ToString()));

                        txtItemName.Text = dtStockEdit.Rows[0]["ItemName"].ToString();
                        lblItemCode.Text = dtStockEdit.Rows[0]["ItemCode"].ToString();
                        txtDescription.Text = dtStockEdit.Rows[0]["Description"].ToString();
                        lblHeaderDate.Text = Convert.ToString(Convert.ToDateTime(dtStockEdit.Rows[0]["IDate"].ToString()).ToShortDateString());


                    }

                }
            

        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearControlDetail();
        //txtItemMasterID.Text = "";
        lblErrorMessage.Text = "";
    }

    protected void btnSaveStock_Click(object sender, EventArgs e)
    {
        PCSN.InvoiceSystem.BusinessLogicLayer.Stock Stock = new PCSN.InvoiceSystem.BusinessLogicLayer.Stock();
        PCSN.InvoiceSystem.BusinessLogicLayer.StockEnterExit StockEnterExit = new PCSN.InvoiceSystem.BusinessLogicLayer.StockEnterExit();
        long ItemMasterID = 0;
        long StockDetailID = 0;

        if (txtItemMasterID.Text == "")
        {
            // This runs When a new Stock is Created or New Item Add in a Stock
            if (txtItemMasterID.Text != "")
            {
                ItemMasterID = Convert.ToInt32(txtItemMasterID.Text);
            }
            if (ValidateClientSide())
            {
                if (txtItemMasterID.Text == "")
                {
                    ItemMasterID = Stock.InsertStockMaster(lblItemCode.Text, txtItemName.Text.ToString(), lblHeaderDate.Text.ToString(), txtDescription.Text.ToString(), Convert.ToInt32(lblTotalAmount.Text.ToString()));
                    txtItemMasterID.Text = ItemMasterID.ToString();
                }
                else
                {
                    ItemMasterID = Convert.ToInt32(txtItemMasterID.Text);
                }
                if (ItemMasterID > 0)
                {
                    if (txtSellingPrice.Text == "")
                    {
                        txtSellingPrice.Text = "0";
                    }
                    txtItemMasterID.Text = ItemMasterID.ToString();
                    StockDetailID = Stock.InsertStockDetail(ItemMasterID, txtItemSize.Text.ToString(), txtColor.Text.ToString(), Convert.ToInt32(txtQuantity.Text), txtMake.Text.ToString(),Convert.ToInt32(txtUnitPrice.Text), Convert.ToInt32(txtSellingPrice.Text));
                    // Recording an entry for each item entered in Stock
                    if (StockDetailID > 0)
                    {
                        StockEnterExit.InsertStockEnter(0, StockDetailID, Convert.ToInt32(txtQuantity.Text), DateTime.Now.ToShortDateString(), "Newly Entered Item");
                    }
                    dtStockEdit = Stock.GetStockDetailTotalAmountByItemMasterID(ItemMasterID);

                    lblTotalAmount.Text = "0";
                    if (dtStockEdit.Rows.Count > 0)
                    {
                        lblTotalAmount.Text = dtStockEdit.Rows[0]["TotalAmount"].ToString();

                    }
                    // Update Item Total Amount
                    Stock.UpdateStockMasterTotalAmount(Convert.ToInt32(txtItemMasterID.Text.ToString()), Convert.ToInt32(lblTotalAmount.Text.ToString()));
                }


                if (ItemMasterID > 0)
                {
                    lblErrorMessage.Text = "This Item has been saved.";
                    if (lblTotalAmount.Text == "")
                    {
                        lblTotalAmount.Text = "0";
                    }
                    lblTotalAmount.Text = Convert.ToString(Convert.ToInt32(lblTotalAmount.Text) + Convert.ToInt32(txtQuantity.Text) * Convert.ToInt32(txtUnitPrice.Text));
                    ClearControlDetail();
                }
            }
        }
        else
        {
            // This runs when an Item is being Edited
            if (ValidateClientSide())
            {
                if (txtItemMasterID.Text != "")
                {
                    ItemMasterID = Convert.ToInt32(txtItemMasterID.Text);
                    // Checking if an Item is being altered
                    if (txtItemDetailID.Text != "")
                    {
                        if (txtSellingPrice.Text == "")
                        {
                            txtSellingPrice.Text = "0";
                        }
                        ItemMasterID = Convert.ToInt32(txtItemMasterID.Text);
                        StockDetailID = Convert.ToInt32(txtItemDetailID.Text);
                        
                        dtStockEdit = Stock.GetStockDetailByID(StockDetailID);
                        // Checking if the Quantity is decreased or increased then Entering or Exiting stock item quantity
                        long Quantity = 0;
                        if (dtStockEdit.Rows.Count > 0)
                        {
                            if (Convert.ToInt32(txtQuantity.Text) > Convert.ToInt32(dtStockEdit.Rows[0]["Quantity"].ToString()))
                            {
                                if (StockDetailID > 0)
                                {
                                    // Increased Quantity is Entered in Stock Enter
                                    Quantity = Convert.ToInt32(txtQuantity.Text) - Convert.ToInt32(dtStockEdit.Rows[0]["Quantity"].ToString());
                                    StockEnterExit.InsertStockEnter(0, StockDetailID, Quantity, DateTime.Now.ToShortDateString(), "Item Updated Quantity Increased");
                                }
                            }
                            else
                            {
                                if (StockDetailID > 0)
                                {
                                    // Decreased Quantity is Entered in Stock Exit
                                    Quantity = Convert.ToInt32(dtStockEdit.Rows[0]["Quantity"].ToString()) - Convert.ToInt32(txtQuantity.Text);
                                    StockEnterExit.InsertStockExit(StockDetailID, Quantity, DateTime.Now.ToShortDateString(), "Item Updated Quantity Decreased");
                                }
                            }
                        }
                        // Updating an Existing Item
                        Stock.UpdateStockDetail(Convert.ToInt32(txtItemDetailID.Text), Convert.ToInt32(txtItemMasterID.Text), txtItemSize.Text.ToString(), txtColor.Text.ToString(), Convert.ToInt32(txtQuantity.Text), txtMake.Text.ToString(), Convert.ToInt32(txtUnitPrice.Text), Convert.ToInt32(txtSellingPrice.Text));

                        dtStockEdit = Stock.GetStockDetailTotalAmountByItemMasterID(ItemMasterID);
                        
                        lblTotalAmount.Text = "0";
                        if(dtStockEdit.Rows.Count>0)
                        {
                            lblTotalAmount.Text = dtStockEdit.Rows[0]["TotalAmount"].ToString();
                            
                        }
                        // Updateing Total Amount
                        Stock.UpdateStockMasterTotalAmount(Convert.ToInt32(txtItemMasterID.Text.ToString()), Convert.ToInt32(lblTotalAmount.Text.ToString()));
                    }
                    else
                    {
                        // Entering a New Item Detail
                        if (txtSellingPrice.Text == "")
                        {
                            txtSellingPrice.Text = "0";
                        }
                        StockDetailID = Stock.InsertStockDetail(ItemMasterID, txtItemSize.Text.ToString(), txtColor.Text.ToString(), Convert.ToInt32(txtQuantity.Text), txtMake.Text.ToString(), Convert.ToInt32(txtUnitPrice.Text), Convert.ToInt32(txtSellingPrice.Text));
                        // Recording an entry for each item entered in Stock
                        if (StockDetailID > 0)
                        {
                            StockEnterExit.InsertStockEnter(0, StockDetailID, Convert.ToInt32(txtQuantity.Text), DateTime.Now.ToShortDateString(), "Newly Entered Item");
                        }
                        dtStockEdit = Stock.GetStockDetailTotalAmountByItemMasterID(ItemMasterID);

                        lblTotalAmount.Text = "0";
                        if (dtStockEdit.Rows.Count > 0)
                        {
                            lblTotalAmount.Text = dtStockEdit.Rows[0]["TotalAmount"].ToString();

                        }
                        // Update Item Total Amount
                        Stock.UpdateStockMasterTotalAmount(Convert.ToInt32(txtItemMasterID.Text.ToString()), Convert.ToInt32(lblTotalAmount.Text.ToString()));
                    }                        

                }
                

                lblErrorMessage.Text = "This Item has been Updated.";
                ClearControlDetail();
            }
        }

        if (ItemMasterID != 0)
        {
            PopulateStocks(ItemMasterID);
            lblTotalAmount.Text = "0";
            dtStockEdit = Stock.GetStockDetailTotalAmountByItemMasterID(ItemMasterID);

            lblTotalAmount.Text = "0";
            if (dtStockEdit.Rows.Count > 0)
            {
                lblTotalAmount.Text = dtStockEdit.Rows[0]["TotalAmount"].ToString();

            }
        }
    }

    protected void btnprintStock_Click(object sender, EventArgs e)
    {
        if (txtItemMasterID.Text != "")
            Response.Redirect("printStock.aspx?ItemMasterID=" + txtItemMasterID.Text.ToString());

    }
    protected void btncancelStock_Click(object sender, EventArgs e)
    {
        Response.Redirect("default.aspx");
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {        
        ClearControlDetail();
    }

    protected void GridView1_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            ((CheckBox)e.Row.FindControl("cbSelectAll")).Attributes.Add("onclick", "javascript:SelectAll('" + ((CheckBox)e.Row.FindControl("cbSelectAll")).ClientID + "')");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView rowView = (DataRowView)e.Row.DataItem;

            LinkButton linkButton = (LinkButton)e.Row.Cells[10].Controls[0];
            linkButton.Attributes["onClick"] = "javascript:return confirm ( 'Are you sure you want to Delete this Item?' )";
        }
        RePopulateValues();
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        RememberOldValues();
        GridView1.PageIndex = e.NewPageIndex;
        SqlDataSource1.DataBind();
        GridView1.DataBind();
    }

    protected void GridView1_OnRowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Visible = false;
        }
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        try
        {
            int index = e.RowIndex;

            long ItemDetailID = Convert.ToInt32(GridView1.Rows[index].Cells[0].Text);
            if (ItemDetailID < 0)
            { }
            else
            {
                PCSN.InvoiceSystem.BusinessLogicLayer.Stock stock = new PCSN.InvoiceSystem.BusinessLogicLayer.Stock();
                PCSN.InvoiceSystem.BusinessLogicLayer.StockEnterExit StockEnterExit = new PCSN.InvoiceSystem.BusinessLogicLayer.StockEnterExit();
                if (ItemDetailID > 0)
                {
                    dtStockEdit = stock.GetStockDetailByID(ItemDetailID);
                    // Checking if the Quantity is decreased or increased then Entering or Exiting stock item quantity
                    long Quantity = 0;
                    if (dtStockEdit.Rows.Count > 0)
                    {
                        if (Convert.ToInt32(dtStockEdit.Rows[0]["Quantity"].ToString())>0)
                        {                            
                            // Quantity That is going to be deleted is being recorded in Stock Exit
                            Quantity = Convert.ToInt32(dtStockEdit.Rows[0]["Quantity"].ToString());                            
                            
                        }
                        StockEnterExit.InsertStockExit(ItemDetailID, Quantity, DateTime.Now.ToShortDateString(), "Item Updated Quantity Decreased/Deleted");
                                                
                    }
                    stock.DeleteStockDetail(ItemDetailID);
                    lblTotalAmount.Text = "0";
                }
                
                dtStockEdit = stock.GetStockDetailTotalAmountByItemMasterID(Convert.ToInt32(txtItemMasterID.Text));

                lblTotalAmount.Text = "0";
                if (dtStockEdit.Rows.Count > 0)
                {
                    lblTotalAmount.Text = dtStockEdit.Rows[0]["TotalAmount"].ToString();

                }
                // Update Item Total Amount
                stock.UpdateStockMasterTotalAmount(Convert.ToInt32(txtItemMasterID.Text.ToString()), Convert.ToInt32(lblTotalAmount.Text.ToString()));                
            }
        }
        catch (Exception ex)
        {
            throw ex;
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
        txtQuantity.Attributes.Add("onBlur", "LostFocusQ();");
        InitializeComponent();
        base.OnInit(e);
    }
    private void InitializeComponent()
    {

        
        //this.Load += new System.EventHandler(this.Page_Load);

    }
    #endregion

    #region Methods
    private void RememberOldValues()
    {
        ArrayList categoryIDList = new ArrayList();
        int index = -1;
        foreach (GridViewRow row in GridView1.Rows)
        {
            index = (int)GridView1.DataKeys[row.RowIndex].Value;
            bool result = ((CheckBox)row.FindControl("CheckBox1")).Checked;

            // Check in the Session
            if (Session["CHECKED_ITEMS"] != null)
                categoryIDList = (ArrayList)Session["CHECKED_ITEMS"];
            if (result)
            {
                if (!categoryIDList.Contains(index))
                    categoryIDList.Add(index);
            }
            else
                categoryIDList.Remove(index);
        }
        if (categoryIDList != null && categoryIDList.Count > 0)
            Session["CHECKED_ITEMS"] = categoryIDList;
    }

    private void RePopulateValues()
    {
        ArrayList categoryIDList = (ArrayList)Session["CHECKED_ITEMS"];
        if (categoryIDList != null && categoryIDList.Count > 0)
        {
            foreach (GridViewRow row in GridView1.Rows)
            {
                int index = (int)GridView1.DataKeys[row.RowIndex].Value;
                if (categoryIDList.Contains(index))
                {
                    CheckBox myCheckBox = (CheckBox)row.FindControl("CheckBox1");
                    myCheckBox.Checked = true;
                }
            }
        }
    }
    private bool ValidateClientSide()
    {

        bool error = false;
        string message = "";

        if (txtItemName.Text.Trim() == "")
        {
            message += "Item Name is not specified.<br>";
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
        if (txtTotalAmount.Text.Trim() == "")
        {
            message += "Item Amount is not specified.<br>";
            error = true;
        }
        else
        {
            try
            {
                txtTotalAmount.Text = Convert.ToInt32(txtTotalAmount.Text).ToString();
            }
            catch
            {
                message += "Invalid Total Item Amount specified.<br>";
                error = true;
            }
        }
        if (txtItemSize.Text.Trim() == "")
        {
            message += "Item Size is not specified.<br>";
            error = true;
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
        
        lblErrorMessage.Text = "";
        if (error)
            lblErrorMessage.Text = message;

        return !error;
    }
        
    private void PopulateStocks(long ItemMasterID)
    {
        txtItemMasterID.Text = ItemMasterID.ToString();
        SqlDataSource1.DataBind();
        GridView1.DataBind();
    }

    

    public void ClearControlDetail()
    {
        //txtItemName.Text = "";
        //txtDescription.Text = "";        
        txtQuantity.Text = "";
        txtUnitPrice.Text = "";
        txtSellingPrice.Text = "";
        txtItemSize.Text = "";
        txtColor.Text = "";
        txtMake.Text = "";
        txtTotalAmount.Text = "";
        txtItemDetailID.Text = "";
    }
    #endregion



}

