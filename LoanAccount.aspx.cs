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

public partial class LoanAccount : System.Web.UI.Page
{
    #region Variables
    private long LoanAccountID = 0;
    DataTable dtLoanAccountDG = new DataTable();
    #endregion

    # region Event Handler
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.QueryString["LoanAccountIDED"] != null && Request.QueryString["LoanAccountIDED"].ToString() != "" && !Page.IsPostBack)
        {
            PCSN.InvoiceSystem.BusinessLogicLayer.Loan LoanAccount = new PCSN.InvoiceSystem.BusinessLogicLayer.Loan();
            DataTable dtLoanAccountDet = new DataTable();
            dtLoanAccountDet = LoanAccount.GetLoanAccountByID(Convert.ToInt32(Request.QueryString["LoanAccountIDED"].ToString()));

            if (dtLoanAccountDet.Rows.Count > 0)
            {
                txtLoanAccountID.Text = dtLoanAccountDet.Rows[0]["ID"].ToString();
                txtLoanAccountName.Text = dtLoanAccountDet.Rows[0]["Name"].ToString();
                txtCompanyName.Text = dtLoanAccountDet.Rows[0]["CompanyName"].ToString();
                txtAddress.Text = dtLoanAccountDet.Rows[0]["Address"].ToString();
                txtMobile.Text = dtLoanAccountDet.Rows[0]["Mobile"].ToString();
                txtPhone.Text = dtLoanAccountDet.Rows[0]["Phone"].ToString();
                txtEmail.Text = dtLoanAccountDet.Rows[0]["Email"].ToString();
                txtDescription.Text = dtLoanAccountDet.Rows[0]["Description"].ToString();

            }

        }
        if (!Page.IsPostBack)
        {
            PopulateLoanAccount();
        }
    }
    private void dgLoanAccounts_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        if (e.CommandName == "DeleteLoanAccount")
        {
            string argsID = e.CommandArgument.ToString();

            long LoanAccountID = Convert.ToInt32(argsID.ToString());

            if (LoanAccountID > 0)
            {
                PCSN.InvoiceSystem.BusinessLogicLayer.Loan LoanAccount = new PCSN.InvoiceSystem.BusinessLogicLayer.Loan();
                LoanAccount.DeleteLoanAccount(LoanAccountID);
                lblErrorMessage.Text = "Loan Account Deleted Successfuly.";
                PopulateLoanAccount();
                ClearControls();
            }
        }
    }

    private void dgLoanAccounts_ItemCreated(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        LinkButton link = (LinkButton)e.Item.FindControl("Linkbutton2");
        if (link != null)
            link.Attributes.Add("onClick", "javascript:return confirm('This action will delete all the information saved related to this Loan Account.  Are you sure you want to delete this Loan Account ?');");

    }
    private void dgLoanAccounts_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dgLoanAccounts.CurrentPageIndex = e.NewPageIndex;
        PopulateLoanAccount();
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("default.aspx");
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        ClearControls();
        PopulateLoanAccount();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ValidateFields())
        {

            if (txtLoanAccountID.Text == "")
            {
                PCSN.InvoiceSystem.BusinessLogicLayer.Loan LoanAccount = new PCSN.InvoiceSystem.BusinessLogicLayer.Loan();
                LoanAccount.InsertLoanAccount(txtLoanAccountName.Text.ToString(), txtCompanyName.Text.ToString(), txtAddress.Text.ToString(), txtPhone.Text.ToString(), txtMobile.Text.ToString(), txtEmail.Text.ToString(), txtDescription.Text.ToString());
                lblErrorMessage.Text = "LoanAccount has been saved.";
                PopulateLoanAccount();
                ClearControls();

            }
            else
            {
                PCSN.InvoiceSystem.BusinessLogicLayer.Loan LoanAccount = new PCSN.InvoiceSystem.BusinessLogicLayer.Loan();
                LoanAccount.UpdateLoanAccount(Convert.ToInt32(txtLoanAccountID.Text), txtLoanAccountName.Text.ToString(), txtCompanyName.Text.ToString(), txtAddress.Text.ToString(), txtPhone.Text.ToString(), txtMobile.Text.ToString(), txtEmail.Text.ToString(), txtDescription.Text.ToString());
                lblErrorMessage.Text = "LoanAccount have been updated.";
                PopulateLoanAccount();
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

        if (txtLoanAccountName.Text.Trim() == "")
        {
            message += "LoanAccount Name is not specified.<br>";
            error = true;
        }
        if (txtCompanyName.Text.Trim() == "")
        {
            message += "Company Name is not specified.<br>";
            error = true;
        }
        if (txtAddress.Text.Trim() == "")
        {
            message += "Company Address is not specified.<br>";
            error = true;
        }
        if (txtPhone.Text.Trim() == "")
        {
            message += "Phone is not specified.<br>";
            error = true;
        }
        if (txtEmail.Text.Trim() == "")
        {
            message += "Email is not specified.<br>";
            error = true;
        }
        if (txtDescription.Text.Trim() == "")
        {
            message += "URL is not specified.<br>";
            error = true;
        }

        lblErrorMessage.Text = "";
        if (error)
            lblErrorMessage.Text = message;

        return !error;
    }

    private void PopulateLoanAccount()
    {
        PCSN.InvoiceSystem.BusinessLogicLayer.Loan LoanAccount = new PCSN.InvoiceSystem.BusinessLogicLayer.Loan();
        dtLoanAccountDG = LoanAccount.GetAllLoanAccount();
        dgLoanAccounts.DataSource = dtLoanAccountDG;
        dgLoanAccounts.DataBind();
    }

    public string EditItem(string LoanAccountID)
    {
        return "LoanAccount.aspx?LoanAccountIDED=" + LoanAccountID;
    }
    
    public string DeleteItem(string LoanAccountID)
    {
        return LoanAccountID;
    }
    public void ClearControls()
    {
        txtLoanAccountID.Text = "";
        txtLoanAccountName.Text = "";
        txtCompanyName.Text = "";
        txtAddress.Text = "";
        txtPhone.Text = "";
        txtMobile.Text = "";
        txtEmail.Text = "";
        txtDescription.Text = "";
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
        this.dgLoanAccounts.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgLoanAccounts_PageIndexChanged);
        this.dgLoanAccounts.ItemCreated += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgLoanAccounts_ItemCreated);
        this.dgLoanAccounts.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgLoanAccounts_ItemCommand);

    }
    #endregion



}
