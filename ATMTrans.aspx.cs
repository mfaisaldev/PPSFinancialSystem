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

public partial class ATMTrans : System.Web.UI.Page
{
    #region Variables
    //private long ATMTransID = 0;
    DataTable dtATMTransDG = new DataTable();
    #endregion

    # region Event Handler
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.QueryString["ATMTransIDED"] != null && Request.QueryString["ATMTransIDED"].ToString() != "" && !Page.IsPostBack)
        {
            PCSN.InvoiceSystem.BusinessLogicLayer.ATMCard ATMTrans = new PCSN.InvoiceSystem.BusinessLogicLayer.ATMCard();
            DataTable dtATMTransDet = new DataTable();
            dtATMTransDet = ATMTrans.GetATMTransByID(Convert.ToInt32(Request.QueryString["ATMTransIDED"].ToString()));

            if (dtATMTransDet.Rows.Count > 0)
            {
                txtATMTransID.Text = dtATMTransDet.Rows[0]["ID"].ToString();
                txtTransID.Text = dtATMTransDet.Rows[0]["TransactionID"].ToString();
                txtAmount.Text = dtATMTransDet.Rows[0]["Amount"].ToString();
                txtPrevAmount.Text = dtATMTransDet.Rows[0]["Amount"].ToString();
                txtTransDate.Text = dtATMTransDet.Rows[0]["TransDate"].ToString();

                txtDescription.Text = dtATMTransDet.Rows[0]["TransDesc"].ToString();
                PopulateATMCard();
                cboATMCard.SelectedValue = dtATMTransDet.Rows[0]["ATMCardID"].ToString();
                
                //============================================
                // Showing Balance
                
                
                long BankID = Convert.ToInt32(dtATMTransDet.Rows[0]["BankID"].ToString());
                if (BankID > 0)
                {
                    PCSN.InvoiceSystem.BusinessLogicLayer.BankAccountInfo Bank = new PCSN.InvoiceSystem.BusinessLogicLayer.BankAccountInfo();
                    dtATMTransDG = Bank.GetBankBalancesByBankID(BankID);
                    if (dtATMTransDG.Rows.Count > 0)
                    {
                        txtBankID.Text = dtATMTransDG.Rows[0]["BankID"].ToString();
                        txtBBalID.Text = dtATMTransDG.Rows[0]["ID"].ToString();
                        txtAvBalance.Text = dtATMTransDG.Rows[0]["AvailableBalance"].ToString();
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
            PopulateATMTrans();
            PopulateATMCard();
        }
    }
    private void dgATMTranss_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        if (e.CommandName == "DeleteATMTrans")
        {
            string argsID = e.CommandArgument.ToString();

            long ATMTransID = Convert.ToInt32(argsID.ToString());

            if (ATMTransID > 0)
            {
                PCSN.InvoiceSystem.BusinessLogicLayer.ATMCard ATMTrans = new PCSN.InvoiceSystem.BusinessLogicLayer.ATMCard();
                // Add amount of ATM transaction into Bank Balance
                if (ATMTransID > 0)
                {                    
                    dtATMTransDG = ATMTrans.GetATMTransByID(ATMTransID);
                    if (dtATMTransDG.Rows.Count > 0)
                    {
                        txtAmount.Text = dtATMTransDG.Rows[0]["Amount"].ToString();
                        // Add Amount of a cheque into Balance
                        PCSN.InvoiceSystem.BusinessLogicLayer.BankAccountInfo BBal = new PCSN.InvoiceSystem.BusinessLogicLayer.BankAccountInfo();
                        
                        dtATMTransDG = BBal.GetBankBalancesByBankID(Convert.ToInt32(dtATMTransDG.Rows[0]["BankID"].ToString()));

                        if (dtATMTransDG.Rows.Count > 0)
                        {
                            txtBankID.Text = dtATMTransDG.Rows[0]["BankID"].ToString();
                            txtBBalID.Text = dtATMTransDG.Rows[0]["ID"].ToString();
                            txtAvBalance.Text = dtATMTransDG.Rows[0]["AvailableBalance"].ToString();

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
                        long availBal = Convert.ToInt32(txtAvBalance.Text) + Convert.ToInt32(txtAmount.Text);
                        BBal.UpdateBankBalances(Convert.ToInt32(txtBBalID.Text), Convert.ToInt32(txtBankID.Text), availBal);
                        // Actuall Deletion of transaction
                        ATMTrans.DeleteATMTrans(ATMTransID);
                        //=====================================
                        lblErrorMessage.Text = "Transaction Deleted Successfuly.";
                        PopulateATMTrans();
                        PopulateATMCard();
                        ClearControls();
                    }
                }
                //====================================

                
            }
        }
    }

    private void dgATMTranss_ItemCreated(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        LinkButton link = (LinkButton)e.Item.FindControl("Linkbutton2");
        if (link != null)
            link.Attributes.Add("onClick", "javascript:return confirm('This action will delete the information saved for this Issued Cheque.  Are you sure you want to delete this Cheque?');");

    }

    private void dgATMTranss_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dgATMTranss.CurrentPageIndex = e.NewPageIndex;
        PopulateATMTrans();
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        PopulateATMTrans();
        PopulateATMCard();
        ClearControls();
    }
    protected void cboATMCard_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cboATMCard.SelectedValue != "0")
        {
            PCSN.InvoiceSystem.BusinessLogicLayer.ATMCard ATMTrans = new PCSN.InvoiceSystem.BusinessLogicLayer.ATMCard();
            dtATMTransDG = ATMTrans.GetATMCardByID(Convert.ToInt32(cboATMCard.SelectedValue));
            if (dtATMTransDG.Rows.Count > 0)
            {
                long BankID = Convert.ToInt32(dtATMTransDG.Rows[0]["BankAccID"].ToString());
                if (BankID > 0)
                {
                    PCSN.InvoiceSystem.BusinessLogicLayer.BankAccountInfo Bank = new PCSN.InvoiceSystem.BusinessLogicLayer.BankAccountInfo();
                    dtATMTransDG = Bank.GetBankBalancesByBankID(BankID);
                    if (dtATMTransDG.Rows.Count > 0)
                    {
                        txtBankID.Text = dtATMTransDG.Rows[0]["BankID"].ToString();
                        txtBBalID.Text = dtATMTransDG.Rows[0]["ID"].ToString();
                        txtAvBalance.Text = dtATMTransDG.Rows[0]["AvailableBalance"].ToString();
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

            if (txtATMTransID.Text == "")
            {
                PCSN.InvoiceSystem.BusinessLogicLayer.ATMCard ATMTrans = new PCSN.InvoiceSystem.BusinessLogicLayer.ATMCard();
                ATMTrans.InsertATMTrans(Convert.ToInt32(cboATMCard.SelectedValue), txtTransID.Text.ToString(), Convert.ToInt32(txtAmount.Text.ToString()), txtTransDate.Text.ToString(), txtDescription.Text.ToString());

                PCSN.InvoiceSystem.BusinessLogicLayer.BankAccountInfo BBal = new PCSN.InvoiceSystem.BusinessLogicLayer.BankAccountInfo();

                // Decreasing Bank Balance
                long availBal = Convert.ToInt32(txtAvBalance.Text) - Convert.ToInt32(txtAmount.Text);
                BBal.UpdateBankBalances(Convert.ToInt32(txtBBalID.Text), Convert.ToInt32(txtBankID.Text), availBal);
                

                lblErrorMessage.Text = "Card Transaction have been saved.";
                PopulateATMTrans();
                PopulateATMCard();

                ClearControls();

            }
            else
            {
                PCSN.InvoiceSystem.BusinessLogicLayer.ATMCard ATMTrans = new PCSN.InvoiceSystem.BusinessLogicLayer.ATMCard();
                ATMTrans.UpdateATMTrans(Convert.ToInt32(txtATMTransID.Text),Convert.ToInt32(cboATMCard.SelectedValue), txtTransID.Text.ToString(), Convert.ToInt32(txtAmount.Text.ToString()), txtTransDate.Text.ToString(), txtDescription.Text.ToString());
                lblErrorMessage.Text = "Card Transaction have been updated.";

                PCSN.InvoiceSystem.BusinessLogicLayer.BankAccountInfo BBal = new PCSN.InvoiceSystem.BusinessLogicLayer.BankAccountInfo();
                // Adjusting bank Balance
                long availBal = Convert.ToInt32(Convert.ToInt32(txtAvBalance.Text) + Convert.ToInt32(txtPrevAmount.Text)) - Convert.ToInt32(txtAmount.Text);
                BBal.UpdateBankBalances(Convert.ToInt32(txtBBalID.Text), Convert.ToInt32(txtBankID.Text), availBal);
               
                PopulateATMTrans();
                PopulateATMCard();

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

        if (cboATMCard.SelectedValue == "0")
        {
            message += "ATM Card is not specified.<br>";
            error = true;
        }        
        
        if (txtDescription.Text.Trim() == "")
        {
            message += "Description is not specified.<br>";
            error = true;
        }

        if (txtAvBalance.Text == "0")
        {
            message += "You do not have sufficient funds for ATM transactions.<br>";
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
                if (Convert.ToInt32(txtAmount.Text) > Convert.ToInt32(txtAvBalance.Text))
                {
                    message += "Transaction Amount can not be greater than the Amount Available.<br>";
                    error = true;
                }
            }
            catch
            {
                message += "Amount is not a Valid Amount";
                error = true;
            }

        }

        if (txtTransDate.Text.Trim() == "")
        {
            message += "Transaction Date is not specified.<br>";
            error = true;
        }
        else
        {
            try
            {
                txtTransDate.Text = Convert.ToDateTime(txtTransDate.Text.ToString()).ToShortDateString();
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

    private void PopulateATMTrans()
    {
        PCSN.InvoiceSystem.BusinessLogicLayer.ATMCard ATMTrans = new PCSN.InvoiceSystem.BusinessLogicLayer.ATMCard();
        dtATMTransDG = ATMTrans.GetAllATMTrans();
        dgATMTranss.DataSource = dtATMTransDG;
        dgATMTranss.DataBind();
    }


    private void PopulateATMCard()
    {
        PCSN.InvoiceSystem.BusinessLogicLayer.ATMCard ATM = new PCSN.InvoiceSystem.BusinessLogicLayer.ATMCard();
        dtATMTransDG = ATM.GetATMCardForDropDown();
        cboATMCard.DataSource = dtATMTransDG;
        cboATMCard.DataTextField = "Name";
        cboATMCard.DataValueField = "ID";

        cboATMCard.DataBind();
    }



    public string EditATMTrans(string ATMTransID)
    {
        return "ATMTrans.aspx?ATMTransIDED=" + ATMTransID;
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("default.aspx");
    }
    public string DeleteATMTrans(string ATMTransID)
    {
        return ATMTransID;
    }
    public void ClearControls()
    {
        txtATMTransID.Text = "";
        txtTransID.Text = "";
        txtAmount.Text = "";
        txtDescription.Text = "";
        txtTransDate.Text = "";
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
        this.dgATMTranss.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgATMTranss_PageIndexChanged);
        this.dgATMTranss.ItemCreated += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgATMTranss_ItemCreated);
        this.dgATMTranss.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgATMTranss_ItemCommand);

    }
    #endregion


    
}
