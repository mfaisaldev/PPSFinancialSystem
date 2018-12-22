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

public partial class ManageATMCard : System.Web.UI.Page
{
    #region Variables
    //private long ManageATMCardID = 0;
    DataTable dtATMCardDG = new DataTable();
    #endregion

    # region Event Handler
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.QueryString["ATMCardIDED"] != null && Request.QueryString["ATMCardIDED"].ToString() != "" && !Page.IsPostBack)
        {
            PCSN.InvoiceSystem.BusinessLogicLayer.ATMCard ManageATMCard = new PCSN.InvoiceSystem.BusinessLogicLayer.ATMCard();
            DataTable dtManageATMCardDet = new DataTable();
            dtManageATMCardDet = ManageATMCard.GetATMCardByID(Convert.ToInt32(Request.QueryString["ATMCardIDED"].ToString()));

            if (dtManageATMCardDet.Rows.Count > 0)
            {
                txtATMCardID.Text = dtManageATMCardDet.Rows[0]["ID"].ToString();
                txtCardNumber.Text = dtManageATMCardDet.Rows[0]["CardNumber"].ToString();
                txtIssueDate.Text = dtManageATMCardDet.Rows[0]["IssueDate"].ToString();
                txtExpiryDate.Text = dtManageATMCardDet.Rows[0]["ExpiryDate"].ToString();

                txtDescription.Text = dtManageATMCardDet.Rows[0]["Description"].ToString();
                PopulateBank();
                cboBank.SelectedValue = dtManageATMCardDet.Rows[0]["BankAccID"].ToString();                

            }

        }
        if (!Page.IsPostBack)
        {
            PopulateATMCard();
            PopulateBank();
        }
    }
    private void dgATMCards_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        if (e.CommandName == "DeleteATMCard")
        {
            string argsID = e.CommandArgument.ToString();

            long ATMCardID = Convert.ToInt32(argsID.ToString());

            if (ATMCardID > 0)
            {
                PCSN.InvoiceSystem.BusinessLogicLayer.ATMCard ManageATMCard = new PCSN.InvoiceSystem.BusinessLogicLayer.ATMCard();
                ManageATMCard.DeleteATMCard(ATMCardID);
                lblErrorMessage.Text = "Cheque Deleted Successfuly.";
                PopulateATMCard();
                PopulateBank();                
                ClearControls();
            }
        }
    }

    private void dgATMCards_ItemCreated(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        LinkButton link = (LinkButton)e.Item.FindControl("Linkbutton2");
        if (link != null)
            link.Attributes.Add("onClick", "javascript:return confirm('This action will delete the information saved for this Issued Cheque.  Are you sure you want to delete this Cheque?');");

    }

    private void dgATMCards_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dgATMCards.CurrentPageIndex = e.NewPageIndex;
        PopulateATMCard();
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        PopulateATMCard();        
        PopulateBank();
        ClearControls();
    }
    
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ValidateFields())
        {

            if (txtATMCardID.Text == "")
            {
                PCSN.InvoiceSystem.BusinessLogicLayer.ATMCard ManageATMCard = new PCSN.InvoiceSystem.BusinessLogicLayer.ATMCard();
                ManageATMCard.InsertATMCard(Convert.ToInt32(cboBank.SelectedValue), txtCardNumber.Text.ToString(), txtIssueDate.Text.ToString(), txtExpiryDate.Text.ToString(), txtDescription.Text.ToString());
                                
                lblErrorMessage.Text = "ATM Card have been saved.";
                PopulateATMCard();
                PopulateBank();
                
                ClearControls();

            }
            else
            {
                PCSN.InvoiceSystem.BusinessLogicLayer.ATMCard ManageATMCard = new PCSN.InvoiceSystem.BusinessLogicLayer.ATMCard();
                ManageATMCard.UpdateATMCard(Convert.ToInt32(txtATMCardID.Text), Convert.ToInt32(cboBank.SelectedValue), txtCardNumber.Text.ToString(), txtIssueDate.Text.ToString(), txtExpiryDate.Text.ToString(), txtDescription.Text.ToString());
                lblErrorMessage.Text = "ATM Card have been updated.";
                                
                PopulateATMCard();
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

        if (txtCardNumber.Text.Trim() == "")
        {
            message += "Card Number is not specified.<br>";
            error = true;
        }
        else
        {
            try
            {
                txtCardNumber.Text = Convert.ToInt64(txtCardNumber.Text.ToString()).ToString();
            }
            catch
            {
                message += "Card Number must be Numeric.<br>";
                error = true;
                txtCardNumber.Text = "0";
            }
        }

        if (txtDescription.Text.Trim() == "")
        {
            message += "Description is not specified.<br>";
            error = true;
        }
        
        if (txtIssueDate.Text.Trim() == "")
        {
            message += "Issue Date is not specified.<br>";
            error = true;
        }
        else
        {
            try
            {
                txtIssueDate.Text = Convert.ToDateTime(txtIssueDate.Text.ToString()).ToShortDateString();
            }
            catch
            {
                message += "Issue Date is not a Valid Date";
                error = true;
            }

        }

        if (txtExpiryDate.Text.Trim() == "")
        {
            message += "Expiry Date is not specified.<br>";
            error = true;
        }
        else
        {
            try
            {
                txtExpiryDate.Text = Convert.ToDateTime(txtExpiryDate.Text.ToString()).ToShortDateString();
            }
            catch
            {
                message += "Expiry Date is not a Valid Date";
                error = true;
            }

        }
        if (cboBank.SelectedValue == "0")
        {
            message += "Bank is not specified.<br>";
            error = true;
        }

        

        lblErrorMessage.Text = "";
        if (error)
            lblErrorMessage.Text = message;

        return !error;
    }

    private void PopulateATMCard()
    {
        PCSN.InvoiceSystem.BusinessLogicLayer.ATMCard ManageATMCard = new PCSN.InvoiceSystem.BusinessLogicLayer.ATMCard();
        dtATMCardDG = ManageATMCard.GetAllATMCard();
        dgATMCards.DataSource = dtATMCardDG;
        dgATMCards.DataBind();
    }
        

    private void PopulateBank()
    {
        PCSN.InvoiceSystem.BusinessLogicLayer.BankAccountInfo Banks = new PCSN.InvoiceSystem.BusinessLogicLayer.BankAccountInfo();
        dtATMCardDG = Banks.GetBankAccountInfoForDropDown();
        cboBank.DataSource = dtATMCardDG;
        cboBank.DataTextField = "Name";
        cboBank.DataValueField = "ID";

        cboBank.DataBind();
    }



    public string EditATMCard(string ATMCardID)
    {
        return "ManageATMCard.aspx?ATMCardIDED=" + ATMCardID;
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("default.aspx");
    }
    public string DeleteATMCard(string ATMCardID)
    {
        return ATMCardID;
    }
    public void ClearControls()
    {
        txtATMCardID.Text = "";
        txtCardNumber.Text = "";
        txtExpiryDate.Text = "";
        txtDescription.Text = "";
        txtIssueDate.Text = "";
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
        this.dgATMCards.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgATMCards_PageIndexChanged);
        this.dgATMCards.ItemCreated += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgATMCards_ItemCreated);
        this.dgATMCards.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgATMCards_ItemCommand);

    }
    #endregion


}
