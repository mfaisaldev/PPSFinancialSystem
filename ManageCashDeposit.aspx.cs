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

public partial class ManageCashDeposit : System.Web.UI.Page
{
    #region Variables
    //private long CashDepositID = 0;
    DataTable dtCashDepositDG = new DataTable();
    #endregion

    # region Event Handler
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.QueryString["CashDepositIDED"] != null && Request.QueryString["CashDepositIDED"].ToString() != "" && !Page.IsPostBack)
        {
            PCSN.InvoiceSystem.BusinessLogicLayer.CashDeposit CashDeposit = new PCSN.InvoiceSystem.BusinessLogicLayer.CashDeposit();
            DataTable dtCashDepositDet = new DataTable();
            dtCashDepositDet = CashDeposit.GetCashDepositByID(Convert.ToInt32(Request.QueryString["CashDepositIDED"].ToString()));

            if (dtCashDepositDet.Rows.Count > 0)
            {
                txtCashDepositID.Text = dtCashDepositDet.Rows[0]["ID"].ToString();
                txtDepDate.Text = dtCashDepositDet.Rows[0]["DepDate"].ToString();
                txtAmount.Text = dtCashDepositDet.Rows[0]["Amount"].ToString();
                txtPrevAmount.Text = dtCashDepositDet.Rows[0]["Amount"].ToString();
                txtDepBy.Text = dtCashDepositDet.Rows[0]["DepBy"].ToString();

                txtDescription.Text = dtCashDepositDet.Rows[0]["Description"].ToString();
                PopulateBank();
                cboBank.SelectedValue = dtCashDepositDet.Rows[0]["BankAccID"].ToString();

                //============================================
                // Showing Balance


                long BankID = Convert.ToInt32(dtCashDepositDet.Rows[0]["BankAccID"].ToString());
                if (BankID > 0)
                {
                    PCSN.InvoiceSystem.BusinessLogicLayer.BankAccountInfo Bank = new PCSN.InvoiceSystem.BusinessLogicLayer.BankAccountInfo();
                    dtCashDepositDG = Bank.GetBankBalancesByBankID(BankID);
                    if (dtCashDepositDG.Rows.Count > 0)
                    {
                        txtBankID.Text = dtCashDepositDG.Rows[0]["BankID"].ToString();
                        txtBBalID.Text = dtCashDepositDG.Rows[0]["ID"].ToString();
                        txtAvBalance.Text = dtCashDepositDG.Rows[0]["AvailableBalance"].ToString();
                    }
                    else
                    {
                        txtAvBalance.Text = "0";
                    }
                }

                //===================================

            }

        }
        if (!Page.IsPostBack)
        {
            PopulateBank();
            PopulateCashDeposit();
        }
    }
    private void dgCashDeposits_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        if (e.CommandName == "DeleteCashDeposit")
        {
            string argsID = e.CommandArgument.ToString();

            long CashDepositID = Convert.ToInt32(argsID.ToString());

            if (CashDepositID > 0)
            {
                PCSN.InvoiceSystem.BusinessLogicLayer.CashDeposit CashDeposit = new PCSN.InvoiceSystem.BusinessLogicLayer.CashDeposit();
                // Add amount of ATM transaction into Bank Balance
                if (CashDepositID > 0)
                {
                    dtCashDepositDG = CashDeposit.GetCashDepositByID(CashDepositID);
                    if (dtCashDepositDG.Rows.Count > 0)
                    {
                        txtAmount.Text = dtCashDepositDG.Rows[0]["Amount"].ToString();
                        // Add Amount of a cheque into Balance
                        PCSN.InvoiceSystem.BusinessLogicLayer.BankAccountInfo BBal = new PCSN.InvoiceSystem.BusinessLogicLayer.BankAccountInfo();

                        dtCashDepositDG = BBal.GetBankBalancesByBankID(Convert.ToInt32(dtCashDepositDG.Rows[0]["BankAccID"].ToString()));

                        if (dtCashDepositDG.Rows.Count > 0)
                        {
                            txtBankID.Text = dtCashDepositDG.Rows[0]["BankID"].ToString();
                            txtBBalID.Text = dtCashDepositDG.Rows[0]["ID"].ToString();
                            txtAvBalance.Text = dtCashDepositDG.Rows[0]["AvailableBalance"].ToString();

                            try
                            {
                                txtAvBalance.Text = Convert.ToInt32(txtAvBalance.Text.ToString()).ToString();
                            }
                            catch
                            {
                                txtAvBalance.Text = "0";
                                //lblSpErrMsg.Text = "Please add some Capital to your account in order to issue a Cheque.<br />OR Please insert a received cheque in this account. Thanks";
                            }

                        }
                        // Increasing Bank Balance
                        long availBal = Convert.ToInt32(Convert.ToInt32(txtAvBalance.Text) - Convert.ToInt32(txtAmount.Text));
                        BBal.UpdateBankBalances(Convert.ToInt32(txtBBalID.Text), Convert.ToInt32(txtBankID.Text), availBal);
                        // Actuall Deletion of transaction
                        CashDeposit.DeleteCashDeposit(CashDepositID);
                        //=====================================
                        lblErrorMessage.Text = "Transaction Deleted Successfuly.";
                        PopulateBank();
                        PopulateCashDeposit();
                        ClearControls();
                    }
                }
                //====================================


            }
        }
    }

    private void dgCashDeposits_ItemCreated(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        LinkButton link = (LinkButton)e.Item.FindControl("Linkbutton2");
        if (link != null)
            link.Attributes.Add("onClick", "javascript:return confirm('This action will delete the information saved for this Deposit Transaction.  Are you sure you want to delete this Transaction?');");

    }

    private void dgCashDeposits_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dgCashDeposits.CurrentPageIndex = e.NewPageIndex;
        PopulateCashDeposit();
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        PopulateBank();
        PopulateCashDeposit();
        ClearControls();
    }
    protected void cboBank_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cboBank.SelectedValue != "0")
        {
            PCSN.InvoiceSystem.BusinessLogicLayer.BankAccountInfo Bank = new PCSN.InvoiceSystem.BusinessLogicLayer.BankAccountInfo();
            dtCashDepositDG = Bank.GetBankBalancesByBankID(Convert.ToInt32(cboBank.SelectedValue));
            if (dtCashDepositDG.Rows.Count > 0)
            {
                long BankID = Convert.ToInt32(dtCashDepositDG.Rows[0]["BankID"].ToString());
                if (BankID > 0)
                {
                    
                    dtCashDepositDG = Bank.GetBankBalancesByBankID(BankID);
                    if (dtCashDepositDG.Rows.Count > 0)
                    {
                        txtBankID.Text = dtCashDepositDG.Rows[0]["BankID"].ToString();
                        txtBBalID.Text = dtCashDepositDG.Rows[0]["ID"].ToString();
                        txtAvBalance.Text = dtCashDepositDG.Rows[0]["AvailableBalance"].ToString();
                    }
                    else
                    {
                        txtAvBalance.Text = "0";
                    }
                }
            }
        }
        else
        {
            txtAvBalance.Text = "0";
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ValidateFields())
        {

            if (txtCashDepositID.Text == "")
            {
                PCSN.InvoiceSystem.BusinessLogicLayer.CashDeposit CashDeposit = new PCSN.InvoiceSystem.BusinessLogicLayer.CashDeposit();
                CashDeposit.InsertCashDeposit(Convert.ToInt32(cboBank.SelectedValue),txtDepDate.Text.ToString(), Convert.ToInt32(txtAmount.Text.ToString()), txtDepBy.Text.ToString(), txtDescription.Text.ToString());

                PCSN.InvoiceSystem.BusinessLogicLayer.BankAccountInfo BBal = new PCSN.InvoiceSystem.BusinessLogicLayer.BankAccountInfo();

                // Decreasing Bank Balance
                long availBal = Convert.ToInt32(txtAvBalance.Text) + Convert.ToInt32(txtAmount.Text);
                BBal.UpdateBankBalances(Convert.ToInt32(txtBBalID.Text), Convert.ToInt32(txtBankID.Text), availBal);


                lblErrorMessage.Text = "Deposit Transaction have been saved.";
                PopulateBank();
                PopulateCashDeposit();

                ClearControls();

            }
            else
            {
                PCSN.InvoiceSystem.BusinessLogicLayer.CashDeposit CashDeposit = new PCSN.InvoiceSystem.BusinessLogicLayer.CashDeposit();
                CashDeposit.UpdateCashDeposit(Convert.ToInt32(txtCashDepositID.Text), Convert.ToInt32(cboBank.SelectedValue), txtDepDate.Text.ToString(), Convert.ToInt32(txtAmount.Text.ToString()), txtDepBy.Text.ToString(), txtDescription.Text.ToString());
                lblErrorMessage.Text = "Deposit Transaction have been updated.";

                PCSN.InvoiceSystem.BusinessLogicLayer.BankAccountInfo BBal = new PCSN.InvoiceSystem.BusinessLogicLayer.BankAccountInfo();
                // Adjusting bank Balance
                long availBal = Convert.ToInt32(Convert.ToInt32(txtAvBalance.Text) - Convert.ToInt32(txtPrevAmount.Text)) + Convert.ToInt32(txtAmount.Text);
                BBal.UpdateBankBalances(Convert.ToInt32(txtBBalID.Text), Convert.ToInt32(txtBankID.Text), availBal);

                PopulateBank();
                PopulateCashDeposit();

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
            message += "Bank is not specified.<br>";
            error = true;
        }
        
        if (txtAmount.Text.Trim() == "")
        {
            message += "Amount is not specified.<br>";
            error = true;
        }
        else
        {
            try
            {
                txtAmount.Text = Convert.ToInt32(txtAmount.Text.ToString()).ToString();                
            }
            catch
            {
                message += "Amount is not a Valid Amount";
                error = true;
            }

        }

        if (txtDepDate.Text.Trim() == "")
        {
            message += "Transaction Date is not specified.<br>";
            error = true;
        }
        else
        {
            try
            {
                txtDepDate.Text = Convert.ToDateTime(txtDepDate.Text.ToString()).ToShortDateString();
            }
            catch
            {
                message += "Transaction Date is not a Valid Date";
                error = true;
            }

        }



        lblErrorMessage.Text = "";
        if (error)
            lblErrorMessage.Text = message;

        return !error;
    }

    private void PopulateCashDeposit()
    {
        PCSN.InvoiceSystem.BusinessLogicLayer.CashDeposit CashDeposit = new PCSN.InvoiceSystem.BusinessLogicLayer.CashDeposit();
        dtCashDepositDG = CashDeposit.GetAllCashDeposit();
        dgCashDeposits.DataSource = dtCashDepositDG;
        dgCashDeposits.DataBind();
    }


    private void PopulateBank()
    {
        PCSN.InvoiceSystem.BusinessLogicLayer.BankAccountInfo Bank = new PCSN.InvoiceSystem.BusinessLogicLayer.BankAccountInfo();
        dtCashDepositDG = Bank.GetBankAccountInfoForDropDown();
        cboBank.DataSource = dtCashDepositDG;
        cboBank.DataTextField = "Name";
        cboBank.DataValueField = "ID";

        cboBank.DataBind();
    }



    public string EditCashDeposit(string CashDepositID)
    {
        return "ManageCashDeposit.aspx?CashDepositIDED=" + CashDepositID;
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("default.aspx");
    }
    public string DeleteCashDeposit(string CashDepositID)
    {
        return CashDepositID;
    }
    public void ClearControls()
    {
        txtCashDepositID.Text = "";
        txtAmount.Text = "";        
        txtDescription.Text = "";
        txtDepDate.Text = "";
        txtDepBy.Text = "";
        txtBankID.Text = "";
        txtBBalID.Text = "";
        txtAvBalance.Text = "0";
        txtPrevAmount.Text = "";
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
        this.dgCashDeposits.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgCashDeposits_PageIndexChanged);
        this.dgCashDeposits.ItemCreated += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgCashDeposits_ItemCreated);
        this.dgCashDeposits.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgCashDeposits_ItemCommand);

    }
    #endregion



}
