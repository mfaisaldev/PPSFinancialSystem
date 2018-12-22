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


public partial class DailyExp : System.Web.UI.Page
{
    public bool btnAddWasClicked = false;
    public int btnPressedValue = 0;
    private DataTable dtExpenseDG = new DataTable();
    private DataTable dtOurCompany = new DataTable(); 
    private DataTable dtExpenseEdit = new DataTable();
    private DataTable dtExpense = new DataTable();
  

    protected void Page_Load(object sender, EventArgs e)
    {
        //string ModeCheck = "";
        if (Request.QueryString["DailyExpMasterID"] != null && Request.QueryString["DailyExpMasterID"].ToString() != "" && Request.QueryString["DailyExpDetailID"] != null && Request.QueryString["DailyExpDetailID"].ToString() != "" && !Page.IsPostBack)
        {
            
            // This is for Updating and ITEM in Expense
            long ExpenseDetailID = Convert.ToInt32(Request.QueryString["DailyExpDetailID"].ToString());
            PCSN.InvoiceSystem.BusinessLogicLayer.DailyExpMaster Expense = new PCSN.InvoiceSystem.BusinessLogicLayer.DailyExpMaster();
            txtItemDescID.Text = Request.QueryString["DailyExpDetailID"].ToString();
            lblTotalAmount.Text = "0";
            dtExpenseEdit = Expense.GetDailyExpDetailByID(ExpenseDetailID);
            for (int a = 0; a < dtExpenseEdit.Rows.Count; a++)
            {
                if (dtExpenseEdit.Rows[a]["ID"].ToString() == Request.QueryString["DailyExpDetailID"].ToString())
                {                    
                    txtItemDesc.Text = dtExpenseEdit.Rows[a]["ItemDesc"].ToString();
                    txtItemDescID.Text = dtExpenseEdit.Rows[a]["ID"].ToString();
                    txtQuantity.Text = dtExpenseEdit.Rows[a]["Quantity"].ToString();                   
                    txtUnitPrice.Text = dtExpenseEdit.Rows[a]["UnitPrice"].ToString();
                    txtItemAmount.Text = dtExpenseEdit.Rows[a]["ItemAmount"].ToString();
                    txtItemAmountPrevious.Text = dtExpenseEdit.Rows[a]["ItemAmount"].ToString();
                    
                    PopulateExpenses(Convert.ToInt32(Request.QueryString["DailyExpMasterID"].ToString()));                                        
                }

                lblTotalAmount.Text = Convert.ToString(Convert.ToInt32(lblTotalAmount.Text) + Convert.ToInt32(dtExpenseEdit.Rows[a]["ItemAmount"].ToString()));
                lblTotalExpOfToday.Text = lblTotalAmount.Text;

            }
        }

        if (!Page.IsPostBack)
        {
            PCSN.InvoiceSystem.BusinessLogicLayer.DailyExpMaster ExpM = new PCSN.InvoiceSystem.BusinessLogicLayer.DailyExpMaster();
            PCSN.InvoiceSystem.BusinessLogicLayer.Closing closing = new PCSN.InvoiceSystem.BusinessLogicLayer.Closing();
            DataTable dtClosing = new DataTable();
            DataTable dtExpM = new DataTable();
            // It is to refresh the total expense page. 
            if (Request.QueryString["DailyExpMasterID"] != null && Request.QueryString["DailyExpMasterID"].ToString() != "")
            {
                txtDailyExpMasterID.Text = Request.QueryString["DailyExpMasterID"].ToString();
            }
            else
            {
                dtExpM = ExpM.GetDailyExpMasterByExpDate(DateTime.Now.ToShortDateString());
                if (dtExpM.Rows.Count > 0)
                {
                    txtDailyExpMasterID.Text = dtExpM.Rows[0]["ID"].ToString();
                    lblExpenseNumber.Text = txtDailyExpMasterID.Text;
                }
            }
            if (txtDailyExpMasterID.Text == "")
            {
                
                // This will run everytime u access the Expense page, to check if it is a new Day or the closing has been made for this day?.
                
                // Checking the Closing of Today?
                dtClosing = closing.GetAllDailyClosingByDate(DateTime.Now.ToShortDateString());
                if (dtClosing.Rows.Count > 0)
                {
                }
                else
                {
                    // So the Closing has not been performed.. Means NO NEW EXPENSE
                    
                    dtExpM = ExpM.GetDailyExpMasterByExpDate(DateTime.Now.ToShortDateString());
                    if (dtExpM.Rows.Count > 0)
                    {
                        txtDailyExpMasterID.Text = dtExpM.Rows[0]["ID"].ToString();
                        lblExpenseNumber.Text = txtDailyExpMasterID.Text;

                    }                    

                    // Performing Closing ( IT WILL CHECK IF THE CLOSING FOR YESTERDAY IS PERFORMED OR NOT )
                    //=============================
                    long DEM_ID = 0;
                    // First we will get the EXPENSE ID of Yesterday
                    dtExpM = ExpM.GetDailyExpMasterByExpDate(DateTime.Now.AddDays(-1).ToShortDateString());
                    if (dtExpM.Rows.Count > 0)
                    {
                        DEM_ID = Convert.ToInt32(dtExpM.Rows[0]["ID"].ToString());
                    }
                    // Now we are Checking the Closing of Yesterday
                    dtClosing = closing.GetAllDailyClosingByDate(DateTime.Now.AddDays(-1).ToShortDateString());
                    if (dtClosing.Rows.Count > 0 || DEM_ID == 0)
                    {
                        // Closing is already DONE or It was a Holiday or WeekEnd
                    }
                    else
                    {
                        // CLOSING Daily Closing of Yesterday
                        closing.InsertDailyClosing(DateTime.Now.AddDays(-1).ToShortDateString(), DEM_ID);
                    }
                    //========================================
                    
                }
            }
            if (txtDailyExpMasterID.Text == "")
            {
                // This is when a Fresh Expense is About to Generate
                // DAILY CLOSING Is DONE for Previous day
                lblRemainingCash.Text = "0";
                lblStartCash.Text = "0";
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
                lblStartCash.Text = "0";
                PCSN.InvoiceSystem.BusinessLogicLayer.DailyExpMaster Expense = new PCSN.InvoiceSystem.BusinessLogicLayer.DailyExpMaster();
                PCSN.InvoiceSystem.BusinessLogicLayer.CashInHand CashinHand = new PCSN.InvoiceSystem.BusinessLogicLayer.CashInHand();
                DataTable dtCashinHand = new DataTable();
                // Getting Cash in Hand Detail
                dtCashinHand = CashinHand.GetAllCashInHandByMAXID();
                if (dtCashinHand.Rows.Count > 0)
                {
                    txtCashInHandID.Text = dtCashinHand.Rows[0]["ID"].ToString();
                    dtCashinHand = CashinHand.GetAllCashInHandByDateIN(DateTime.Now.ToShortDateString());
                    // Getting the CASH IN HAND details of Today
                    if (dtCashinHand.Rows.Count > 0)
                    {
                        if (dtCashinHand.Rows.Count > 1)
                        {
                            for (int i = 0; i > dtCashinHand.Rows.Count; i++)
                            {
                                txtCashInHandID.Text = dtCashinHand.Rows[i]["ID"].ToString();
                                lblStartCash.Text = Convert.ToString(Convert.ToInt32(lblStartCash.Text) + Convert.ToInt32(dtCashinHand.Rows[i]["CashIN"].ToString()));
                                lblRemainingCash.Text = Convert.ToString(Convert.ToInt32(lblRemainingCash.Text) + Convert.ToInt32(dtCashinHand.Rows[i]["TotalAmount"].ToString()));
                            }

                        }
                        else
                        {
                            txtCashInHandID.Text = dtCashinHand.Rows[0]["ID"].ToString();
                            lblStartCash.Text = dtCashinHand.Rows[0]["CashAvailable"].ToString();
                            lblRemainingCash.Text = dtCashinHand.Rows[0]["CashAvailable"].ToString();
                        }
                        
                    }
                    else
                    {                       

                        dtCashinHand = CashinHand.GetAllCashInHandByMAXID();
                        if (dtCashinHand.Rows.Count > 0)
                        {
                            long CashinhandID = CashinHand.InsertCashInHand(Convert.ToInt32(dtCashinHand.Rows[0]["ChequeIssueID"].ToString()), Convert.ToInt32(dtCashinHand.Rows[0]["CashAvailable"].ToString()), Convert.ToInt32("0"), DateTime.Now.ToShortDateString(), Convert.ToInt32(dtCashinHand.Rows[0]["CashAvailable"].ToString()), "Automatic Entry to Data Base of Starting Cash in Hand for Today");

                            dtCashinHand = CashinHand.GetAllCashInHandByMAXID();
                            if (dtCashinHand.Rows.Count > 0)
                            {
                                txtCashInHandID.Text = dtCashinHand.Rows[0]["ID"].ToString();
                                lblStartCash.Text = dtCashinHand.Rows[0]["CashAvailable"].ToString();
                                lblRemainingCash.Text = dtCashinHand.Rows[0]["CashAvailable"].ToString();
                            }
                        }

                    }                   
                }
                else
                {
                    Response.Redirect("CashRegister.aspx?errmsg=You do not have enough Funds available to use as Cash. Please issue a Cheque for Personal Use and then Use it in order to use Daily Expense System.");
                }

                lblHeaderDate.Text = Convert.ToString(DateTime.Now.ToShortDateString());
                lblTotalAmount.Text = "0";
                lblTotalExpOfToday.Text = lblTotalAmount.Text;
                

                dtExpense = Expense.GetAllDailyExpMasterMAX();
                if (dtExpense.Rows.Count == 0 || dtExpense.Rows[0]["MAXID"].ToString() == "")
                {
                    lblExpenseNumber.Text = "1";
                }
                else
                {
                    lblExpenseNumber.Text = Convert.ToInt32(Convert.ToInt32(dtExpense.Rows[0]["MAXID"].ToString()) + 1).ToString();
                }                
                
            }
            else
            {
                // This is when the Expense ID is getting from somewhere 
                // AND the DAILY CLOSING has not been performed
                // Like from Query Strings

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

                PCSN.InvoiceSystem.BusinessLogicLayer.CashInHand CashinHand = new PCSN.InvoiceSystem.BusinessLogicLayer.CashInHand();
                DataTable dtCashinHand = new DataTable();
                dtCashinHand = CashinHand.GetAllCashInHandByMAXID();
                // Setting up the CASH IN HAND VALUES
                if (dtCashinHand.Rows.Count > 0)
                {
                    txtCashInHandID.Text = dtCashinHand.Rows[0]["ID"].ToString();                             
                    
                }
                else
                {
                    // If Cash in Hand Not Found then you Need to Add some Cash in Hand in order to run Daily Expense
                    Response.Redirect("CashRegister.aspx");
                }

                lblHeaderDate.Text = Convert.ToString(DateTime.Now.ToShortDateString());
                lblTotalAmount.Text = "0";
                PCSN.InvoiceSystem.BusinessLogicLayer.DailyExpMaster Expense = new PCSN.InvoiceSystem.BusinessLogicLayer.DailyExpMaster();

                dtExpense = Expense.GetDailyExpMasterByID(Convert.ToInt32(txtDailyExpMasterID.Text));
                if (dtExpense.Rows.Count>0)
                {
                    lblStartCash.Text = dtExpense.Rows[0]["StartCash"].ToString();
                    lblRemainingCash.Text = dtExpense.Rows[0]["EndCash"].ToString();
                    lblExpenseNumber.Text = dtExpense.Rows[0]["ID"].ToString();
                    lblTotalAmount.Text = dtExpense.Rows[0]["TotalAmount"].ToString();
                    txtCashInHandID.Text = dtExpense.Rows[0]["CashInHandID"].ToString();
                    lblHeaderDate.Text = dtExpense.Rows[0]["ExpDate"].ToString();
                    lblTotalExpOfToday.Text = lblTotalAmount.Text;
                    lblRemainingCash.Text = Convert.ToString(Convert.ToInt32(lblStartCash.Text) - Convert.ToInt32(lblTotalExpOfToday.Text));
                }                
                if (txtDailyExpMasterID.Text != "")
                {
                    PopulateExpenses(Convert.ToInt32(txtDailyExpMasterID.Text));
                }
            }

        }
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        ClearControlDetail();
        //txtDailyExpMasterID.Text = "";
        txtItemDescID.Text = "";
        lblErrorMessage.Text = "";        
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("default.aspx");
    }

    protected void btnSaveExpense_Click(object sender, EventArgs e)
    {
        if (ValidateExpenseSide())
        {            
            PCSN.InvoiceSystem.BusinessLogicLayer.DailyExpMaster DExp = new PCSN.InvoiceSystem.BusinessLogicLayer.DailyExpMaster();
            PCSN.InvoiceSystem.BusinessLogicLayer.CashInHand CashIn = new PCSN.InvoiceSystem.BusinessLogicLayer.CashInHand();
            if (txtDailyExpMasterID.Text == "")
            {
                long newamount = Convert.ToInt32(lblRemainingCash.Text.ToString().Trim())  - Convert.ToInt32(txtItemAmount.Text);
                lblRemainingCash.Text = newamount.ToString();
                long DailyExpMasterID = DExp.InsertDailyExpMaster(Convert.ToInt32(txtCashInHandID.Text), lblHeaderDate.Text, Convert.ToInt32(lblStartCash.Text), Convert.ToInt32(lblRemainingCash.Text), Convert.ToInt32(Convert.ToInt32(lblTotalAmount.Text) + Convert.ToInt32(txtItemAmount.Text)));
                CashIn.UpdateCashInHandCashAvail(Convert.ToInt32(txtCashInHandID.Text), Convert.ToInt32(lblRemainingCash.Text));
                if (DailyExpMasterID > 0)
                {
                    DExp.InsertDailyExpDetail(DailyExpMasterID, txtItemDesc.Text, Convert.ToInt32(txtQuantity.Text), Convert.ToInt32(txtUnitPrice.Text), Convert.ToInt32(txtItemAmount.Text));
                                        
                    lblErrorMessage.Text = "This expense is saved and added to your todays Expense control.";
                    PopulateExpenses(DailyExpMasterID);
                    
                    txtDailyExpMasterID.Text = DailyExpMasterID.ToString();
                    lblExpenseNumber.Text = DailyExpMasterID.ToString();
                    lblTotalExpOfToday.Text = Convert.ToString(Convert.ToInt32(lblTotalAmount.Text) + Convert.ToInt32(txtItemAmount.Text));
                    ClearControlDetail();
                }
            }
            else
            {
                if (txtItemDescID.Text == "")
                {
                    long newamount = Convert.ToInt32(lblRemainingCash.Text.ToString().Trim()) - Convert.ToInt32(txtItemAmount.Text);
                    
                    if (newamount < 0)
                    {
                        lblErrorMessage.Text = "Out of money Error, Please add some cash in Hand first.";

                    }
                    else
                    {
                        lblRemainingCash.Text = newamount.ToString();
                        DExp.InsertDailyExpDetail(Convert.ToInt32(txtDailyExpMasterID.Text), txtItemDesc.Text, Convert.ToInt32(txtQuantity.Text), Convert.ToInt32(txtUnitPrice.Text), Convert.ToInt32(txtItemAmount.Text));
                        //PCSN.InvoiceSystem.BusinessLogicLayer.CashInHand CashIn = new PCSN.InvoiceSystem.BusinessLogicLayer.CashInHand();
                        CashIn.UpdateCashInHandCashAvail(Convert.ToInt32(txtCashInHandID.Text), Convert.ToInt32(lblRemainingCash.Text));

                        DataTable dtUpAmount = new DataTable();
                        dtUpAmount = DExp.GetDailyExpMasterByID(Convert.ToInt32(txtDailyExpMasterID.Text));
                        if (dtUpAmount.Rows.Count > 0)
                        {
                            long startCash = Convert.ToInt32(dtUpAmount.Rows[0]["StartCash"].ToString());
                            long TotalAmount = Convert.ToInt32(dtUpAmount.Rows[0]["TotalAmount"].ToString()) + Convert.ToInt32(txtItemAmount.Text);
                            //lblTotalAmount.Text = TotalAmount.ToString();
                            long endCash = startCash - TotalAmount;
                            DExp.UpdateDailyExpMasterTotalAmount(Convert.ToInt32(txtDailyExpMasterID.Text), endCash, TotalAmount);

                            dtUpAmount = DExp.GetDailyExpMasterByID(Convert.ToInt32(txtDailyExpMasterID.Text));
                            if (dtUpAmount.Rows.Count > 0)
                            {
                                lblTotalAmount.Text = dtUpAmount.Rows[0]["TotalAmount"].ToString();
                            }
                        }

                        lblErrorMessage.Text = "This expense is saved and added to your todays Expense control.";
                        PopulateExpenses(Convert.ToInt32(txtDailyExpMasterID.Text));
                        
                        lblTotalExpOfToday.Text = lblTotalAmount.Text;
                        ClearControlDetail();
                    }
                }
                else
                {
                    long newamount = Convert.ToInt32(Convert.ToInt32(lblRemainingCash.Text.ToString().Trim()) + Convert.ToInt32(txtItemAmountPrevious.Text)) - Convert.ToInt32(txtItemAmount.Text);
                    
                    if (newamount < 0)
                    {
                        lblErrorMessage.Text = "Out of money Error, Please add some cash in Hand first.";

                    }
                    else
                    {
                        lblRemainingCash.Text = newamount.ToString();
                        DExp.UpdateDailyExpDetail(Convert.ToInt32(txtItemDescID.Text), Convert.ToInt32(txtDailyExpMasterID.Text), txtItemDesc.Text, Convert.ToInt32(txtQuantity.Text), Convert.ToInt32(txtUnitPrice.Text), Convert.ToInt32(txtItemAmount.Text));
                        CashIn.UpdateCashInHandCashAvail(Convert.ToInt32(txtCashInHandID.Text), Convert.ToInt32(lblRemainingCash.Text));

                        DataTable dtUpAmount = new DataTable();
                        dtUpAmount = DExp.GetDailyExpMasterByID(Convert.ToInt32(txtDailyExpMasterID.Text));
                        if (dtUpAmount.Rows.Count > 0)
                        {
                            long startCash = Convert.ToInt32(dtUpAmount.Rows[0]["StartCash"].ToString());
                            long TotalAmount = Convert.ToInt32(Convert.ToInt32(dtUpAmount.Rows[0]["TotalAmount"].ToString()) - Convert.ToInt32(txtItemAmountPrevious.Text)) + Convert.ToInt32(txtItemAmount.Text);
                            lblTotalAmount.Text = TotalAmount.ToString();
                            long endCash = startCash - TotalAmount;
                            DExp.UpdateDailyExpMasterTotalAmount(Convert.ToInt32(txtDailyExpMasterID.Text), endCash, TotalAmount);

                            dtUpAmount = DExp.GetDailyExpMasterByID(Convert.ToInt32(txtDailyExpMasterID.Text));
                            if (dtUpAmount.Rows.Count > 0)
                            {
                                lblTotalAmount.Text = dtUpAmount.Rows[0]["TotalAmount"].ToString();
                            }
                        }

                        lblErrorMessage.Text = "This expense is updated to your todays Expense control.";
                        PopulateExpenses(Convert.ToInt32(txtDailyExpMasterID.Text));
                        
                        lblTotalExpOfToday.Text = lblTotalAmount.Text;
                        ClearControlDetail();
                    }
                }

            }
        }
    }

    protected void btnprintExpense_Click(object sender, EventArgs e)
    {
        if (txtDailyExpMasterID.Text != "")
            Response.Redirect("ViewDailyExpense.aspx?ExpenseMasterID=" + txtDailyExpMasterID.Text.ToString());

    }
    protected void btncancelExpense_Click(object sender, EventArgs e)
    {
        Response.Redirect("default.aspx");
    }

    private void dgExpenses_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        if (e.CommandName == "DeleteExpense")
        {
            string argsID = e.CommandArgument.ToString();
            
            long ExpenseDetailID = Convert.ToInt32(argsID);

            if (ExpenseDetailID > 0)
            {
                PCSN.InvoiceSystem.BusinessLogicLayer.DailyExpMaster Expense = new PCSN.InvoiceSystem.BusinessLogicLayer.DailyExpMaster();                
                
                DeletingAnItem(ExpenseDetailID);

                Expense.DeleteDailyExpDetail(ExpenseDetailID);
                lblErrorMessage.Text = "Item Deleted Successfuly.";
                PopulateExpenses(Convert.ToInt32(txtDailyExpMasterID.Text));
            }
        }
    }

    private void dgExpenses_ItemCreated(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        LinkButton link = (LinkButton)e.Item.FindControl("Linkbutton2");
        if (link != null)
            link.Attributes.Add("onClick", "javascript:return confirm('This action will delete thi Item in Expense.  Are you sure you want to delete this Item?');");

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

        this.dgExpenses.ItemCreated += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgExpenses_ItemCreated);
        this.dgExpenses.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgExpenses_ItemCommand);

        //this.Load += new System.EventHandler(this.Page_Load);

    }
    #endregion

    #region Methods

    private bool ValidateExpenseSide()
    {

        bool error = false;
        string message = "";

        if (txtItemDesc.Text.Trim() == "")
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
        
        

        lblErrorMessage.Text = "";
        if (error)
            lblErrorMessage.Text = message;

        return !error;
    }

    
    private void PopulateExpenses(long ExpenseMasterID)
    {
        PCSN.InvoiceSystem.BusinessLogicLayer.DailyExpMaster Expense = new PCSN.InvoiceSystem.BusinessLogicLayer.DailyExpMaster();
        dtExpenseDG = Expense.GetAllDailyExpDetailByDailyExpMasterID(ExpenseMasterID);
        dgExpenses.DataSource = dtExpenseDG;
        dgExpenses.DataBind();
    }

    private void DeletingAnItem(long ExpDetailID)
    {
        PCSN.InvoiceSystem.BusinessLogicLayer.DailyExpMaster DExp = new PCSN.InvoiceSystem.BusinessLogicLayer.DailyExpMaster();
        PCSN.InvoiceSystem.BusinessLogicLayer.CashInHand CashIn = new PCSN.InvoiceSystem.BusinessLogicLayer.CashInHand();
        DataTable dtExpDetail = new DataTable();
        dtExpDetail = DExp.GetDailyExpDetailByID(ExpDetailID);
        if (dtExpDetail.Rows.Count > 0)
        {
            txtItemDesc.Text = dtExpDetail.Rows[0]["ItemDesc"].ToString();
            txtItemDescID.Text = dtExpDetail.Rows[0]["ID"].ToString();
            txtQuantity.Text = dtExpDetail.Rows[0]["Quantity"].ToString();
            txtUnitPrice.Text = dtExpDetail.Rows[0]["UnitPrice"].ToString();
            txtItemAmount.Text = dtExpDetail.Rows[0]["ItemAmount"].ToString();
            txtItemAmountPrevious.Text = dtExpDetail.Rows[0]["ItemAmount"].ToString();
                    
            long newamount = Convert.ToInt32(lblRemainingCash.Text.ToString().Trim()) + Convert.ToInt32(txtItemAmount.Text);

            if (newamount < 0)
            {
                lblErrorMessage.Text = "Out of money Error, Please add some cash in Hand first.";

            }
            else
            {
                lblRemainingCash.Text = newamount.ToString();
                //DExp.InsertDailyExpDetail(Convert.ToInt32(txtDailyExpMasterID.Text), txtItemDesc.Text, Convert.ToInt32(txtQuantity.Text), Convert.ToInt32(txtUnitPrice.Text), Convert.ToInt32(txtItemAmount.Text));
                //PCSN.InvoiceSystem.BusinessLogicLayer.CashInHand CashIn = new PCSN.InvoiceSystem.BusinessLogicLayer.CashInHand();
                CashIn.UpdateCashInHandCashAvail(Convert.ToInt32(txtCashInHandID.Text), Convert.ToInt32(lblRemainingCash.Text));

                DataTable dtUpAmount = new DataTable();
                dtUpAmount = DExp.GetDailyExpMasterByID(Convert.ToInt32(txtDailyExpMasterID.Text));
                if (dtUpAmount.Rows.Count > 0)
                {
                    long startCash = Convert.ToInt32(dtUpAmount.Rows[0]["StartCash"].ToString());
                    long TotalAmount = Convert.ToInt32(dtUpAmount.Rows[0]["TotalAmount"].ToString()) - Convert.ToInt32(txtItemAmount.Text);
                    //lblTotalAmount.Text = TotalAmount.ToString();
                    long endCash = startCash - TotalAmount;
                    DExp.UpdateDailyExpMasterTotalAmount(Convert.ToInt32(txtDailyExpMasterID.Text), endCash, TotalAmount);

                    dtUpAmount = DExp.GetDailyExpMasterByID(Convert.ToInt32(txtDailyExpMasterID.Text));
                    if (dtUpAmount.Rows.Count > 0)
                    {
                        lblTotalAmount.Text = dtUpAmount.Rows[0]["TotalAmount"].ToString();
                    }
                }

                //lblErrorMessage.Text = "This expense is saved and added to your todays Expense control.";                
                lblTotalExpOfToday.Text = lblTotalAmount.Text;
            }
        }
        ClearControlDetail();
    }
    public string EditItem(string DailyExpMasterID, string DailyExpDetailID)
    {
        return "DailyExp.aspx?DailyExpMasterID=" + DailyExpMasterID + "&DailyExpDetailID=" + DailyExpDetailID;
    }

    public string DeleteItem(string DailyExpDetailID)
    {
        return DailyExpDetailID;
    }

    public void ClearControlDetail()
    {
        txtItemDesc.Text = "";
        txtItemDescID.Text = "";        
        txtQuantity.Text = "";
        txtUnitPrice.Text = "";
        txtItemAmount.Text = "";
        txtItemAmountPrevious.Text = "";
    }
    #endregion



    
}

