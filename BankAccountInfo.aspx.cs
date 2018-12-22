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

public partial class BankAccountInfo : System.Web.UI.Page
{
    #region Variables
    //private long BankID = 0;
    DataTable dtBankDG = new DataTable();
    #endregion

    # region Event Handler
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.QueryString["BankIDED"] != null && Request.QueryString["BankIDED"].ToString() != "" && !Page.IsPostBack)
        {
            PCSN.InvoiceSystem.BusinessLogicLayer.BankAccountInfo Bank = new PCSN.InvoiceSystem.BusinessLogicLayer.BankAccountInfo();
            DataTable dtBankDet = new DataTable();
            dtBankDet = Bank.GetBankAccountInfoByID(Convert.ToInt32(Request.QueryString["BankIDED"].ToString()));

            if (dtBankDet.Rows.Count > 0)
            {
                txtBankID.Text = dtBankDet.Rows[0]["ID"].ToString();
                cboBank.SelectedValue = dtBankDet.Rows[0]["BankID"].ToString();
                txtAccountHolderName.Text = dtBankDet.Rows[0]["AccountHolderName"].ToString();
                txtAccountNumber.Text = dtBankDet.Rows[0]["AccountNumber"].ToString();
                txtBranch.Text = dtBankDet.Rows[0]["Branch"].ToString();
                txtPhone.Text = dtBankDet.Rows[0]["Phone"].ToString();
                txtAddress.Text = dtBankDet.Rows[0]["Address"].ToString();
                txtDescription.Text = dtBankDet.Rows[0]["Description"].ToString();
                txtBankBalID.Text = dtBankDet.Rows[0]["BBalID"].ToString();
                txtPrevAvBal.Text = dtBankDet.Rows[0]["AvailableBalance"].ToString();
                txtAvBalance.Text = dtBankDet.Rows[0]["AvailableBalance"].ToString();
                if (txtAvBalance.Text == "")
                {
                    lblErrorMessage.Text = "In this Account, you have not provided any fund available <br />Because of this reason you will not be able to do following things.<br />1. You certainly can not issue a Cheque from this Account.<br />2. You can not draw Cash and can not use Daily Expense System. <br />Please add some amount in this account or insert any cheque in this account. Thanks";
                }

            }            
        }
        if (!Page.IsPostBack)
        {
            PopulateBank();
            PopulateBanksDD();
        }
    }
    private void dgBanks_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        if (e.CommandName == "DeleteBank")
        {
            string argsID = e.CommandArgument.ToString();

            long BankID = Convert.ToInt32(argsID.ToString());

            if (BankID > 0)
            {
                PCSN.InvoiceSystem.BusinessLogicLayer.BankAccountInfo Bank = new PCSN.InvoiceSystem.BusinessLogicLayer.BankAccountInfo();
                Bank.DeleteBankAccountInfo(BankID);                
                lblErrorMessage.Text = "Bank Deleted Successfuly.";
                PopulateBank();
                ClearControls();
            }
        }
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        PopulateBank();
        PopulateBanksDD();
        ClearControls();
    }
    private void dgBanks_ItemCreated(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        LinkButton link = (LinkButton)e.Item.FindControl("Linkbutton2");
        if (link != null)
            link.Attributes.Add("onClick", "javascript:return confirm('This action will delete the information saved for this Bank Account.  Are you sure you want to delete this Bank Account?');");

    }
    private void dgBanks_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dgBanks.CurrentPageIndex = e.NewPageIndex;
        PopulateBank();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ValidateFields())
        {

            if (txtBankID.Text == "")
            {
                PCSN.InvoiceSystem.BusinessLogicLayer.BankAccountInfo Bank = new PCSN.InvoiceSystem.BusinessLogicLayer.BankAccountInfo();
                long BankAccID = Bank.InsertBankAccountInfo(cboBank.SelectedValue.ToString(), txtAccountHolderName.Text.ToString(), txtAccountNumber.Text.ToString(), txtBranch.Text.ToString(), Convert.ToInt32(txtPhone.Text.ToString()), txtAddress.Text.ToString(), txtDescription.Text.ToString());
                
                if (BankAccID > 0)
                {
                    try
                    {
                        txtAvBalance.Text = Convert.ToInt32(txtAvBalance.Text).ToString();
                    }
                    catch
                    {
                        txtAvBalance.Text = "0";
                    }

                    Bank.InsertBankBalances(BankAccID,Convert.ToInt32(txtAvBalance.Text));
                }
                if (txtAvBalance.Text.ToString() == "0")
                {
                    lblErrorMessage.Text = "Account have been created, But you have not provided any fund available in this Account <br />Because of this reason you will not be able to do following things.<br />1. You certainly can not issue a Cheque from this Account.<br />2. You can not draw Cash and can not use Daily Expense System. <br />Please add some amount in this account or insert any cheque in this account. Thanks";
                }
                else
                {
                    lblErrorMessage.Text = "Bank Account Have been saved.";
                }
                

                PopulateBank();
                ClearControls();                

            }
            else
            {
                PCSN.InvoiceSystem.BusinessLogicLayer.BankAccountInfo Bank = new PCSN.InvoiceSystem.BusinessLogicLayer.BankAccountInfo();
                Bank.UpdateBankAccountInfo(Convert.ToInt32(txtBankID.Text), cboBank.SelectedValue.ToString(), txtAccountHolderName.Text.ToString(), txtAccountNumber.Text.ToString(), txtBranch.Text.ToString(), Convert.ToInt32(txtPhone.Text.ToString()), txtAddress.Text.ToString(), txtDescription.Text.ToString());
                if (txtAvBalance.Text.ToString().Trim() == txtPrevAvBal.Text.ToString().Trim())
                {
                }
                else
                {
                    try
                    {
                        txtAvBalance.Text = Convert.ToInt32(txtAvBalance.Text).ToString();
                    }
                    catch
                    {
                        txtAvBalance.Text = "0";
                    }
                    DataTable dtBBal = new DataTable();
                    dtBBal = Bank.GetBankBalancesByBankID(Convert.ToInt32(txtBankID.Text));
                    if (dtBBal.Rows.Count > 0)
                    {
                        Bank.UpdateBankBalances(Convert.ToInt32(txtBankBalID.Text), Convert.ToInt32(txtBankID.Text), Convert.ToInt32(txtAvBalance.Text));
                    }
                    else
                    {
                        Bank.InsertBankBalances(Convert.ToInt32(txtBankID.Text), Convert.ToInt32(txtAvBalance.Text));
                    }
                }
                if (txtAvBalance.Text.ToString() == "0")
                {
                    lblErrorMessage.Text = "Account have been Updated, But you have not provided any fund available in this Account <br />Because of this reason you will not be able to do following things.<br />1. You certainly can not issue a Cheque from this Account.<br />2. You can not draw Cash and can not use Daily Expense System. <br />Please add some amount in this account or insert any cheque in this account. Thanks";
                }
                else
                {
                    lblErrorMessage.Text = "Bank Account have been updated.";
                }
                PopulateBank();
                ClearControls();
            }

        }
    }


    #endregion

    #region Methods

    private bool ValidateFields()
    {

        bool error = false;
        string message = "";

        if (cboBank.SelectedValue == "0")
        {
            message += "Bank Name is not specified.<br>";
            error = true;
        }
        if (txtAccountHolderName.Text.Trim() == "")
        {
            message += "Account Holder Name is not specified.<br>";
            error = true;
        }
        if (txtAccountNumber.Text.Trim() == "")
        {
            message += "Account Number is not specified.<br>";
            error = true;
        }
        if (txtPhone.Text.Trim() == "")
        {
            message += "Phone is not specified.<br>";
            error = true;
        }
        else
        {
            try
            {
                txtPhone.Text = Convert.ToInt32(txtPhone.Text.ToString()).ToString();
            }
            catch
            {
                message += "Phone Number must be Numeric.<br>";
                error = true;
                txtPhone.Text = "0";
            }
        }
        if (txtBranch.Text.Trim() == "")
        {
            message += "Branch is not specified.<br>";
            error = true;
        }
        if (txtAddress.Text.Trim() == "")
        {
            message += "Address is not specified.<br>";
            error = true;
        }
        if (txtDescription.Text.Trim() == "")
        {
            message += "Description is not specified.<br>";
            error = true;
        }

        lblErrorMessage.Text = "";
        if (error)
            lblErrorMessage.Text = message;

        return !error;
    }

    private void PopulateBank()
    {
        PCSN.InvoiceSystem.BusinessLogicLayer.BankAccountInfo Bank = new PCSN.InvoiceSystem.BusinessLogicLayer.BankAccountInfo();
        dtBankDG = Bank.GetAllBankAccountInfo();
        dgBanks.DataSource = dtBankDG;
        dgBanks.DataBind();
    }

    private void PopulateBanksDD()
    {
        PCSN.InvoiceSystem.BusinessLogicLayer.BankAccountInfo ChequeBook = new PCSN.InvoiceSystem.BusinessLogicLayer.BankAccountInfo();
        dtBankDG = ChequeBook.GetAllBankListForDropDown();
        cboBank.DataSource = dtBankDG;
        cboBank.DataTextField = "Name";
        cboBank.DataValueField = "ID";

        cboBank.DataBind();
    }

    public string EditItem(string BankID)
    {
        return "BankAccountInfo.aspx?BankIDED=" + BankID;
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("default.aspx");
    }
    public string DeleteItem(string BankID)
    {
        return BankID;
    }
    public void ClearControls()
    {
        txtBankID.Text = "";
        //txtBankName.Text = "";
        txtAccountHolderName.Text = "";
        txtAccountNumber.Text = "";
        txtPhone.Text = "";
        txtBranch.Text = "";
        txtAddress.Text = "";
        txtDescription.Text = "";
        txtBankBalID.Text = "";
        txtAvBalance.Text = "0";
        txtPrevAvBal.Text = "0";
    }
    #endregion

    # region Page Init
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
        this.dgBanks.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgBanks_PageIndexChanged);
        this.dgBanks.ItemCreated += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgBanks_ItemCreated);
        this.dgBanks.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgBanks_ItemCommand);

    }
    #endregion



}
