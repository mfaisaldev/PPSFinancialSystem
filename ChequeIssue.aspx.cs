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

public partial class ChequeIssue : System.Web.UI.Page
{
    #region Variables
    //private long ChequeIssueID = 0;
    DataTable dtChequeIssueDG = new DataTable();
    #endregion

    # region Event Handler
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.QueryString["ChequeIssueIDED"] != null && Request.QueryString["ChequeIssueIDED"].ToString() != "" && !Page.IsPostBack)
        {
            PCSN.InvoiceSystem.BusinessLogicLayer.ChequeIssue ChequeIssue = new PCSN.InvoiceSystem.BusinessLogicLayer.ChequeIssue();
            DataTable dtChequeIssueDet = new DataTable();
            dtChequeIssueDet = ChequeIssue.GetChequeIssueByID(Convert.ToInt32(Request.QueryString["ChequeIssueIDED"].ToString()));

            if (dtChequeIssueDet.Rows.Count > 0)
            {
                txtChequeIssueID.Text = dtChequeIssueDet.Rows[0]["ID"].ToString();
                txtChequeNumber.Text = dtChequeIssueDet.Rows[0]["ChequeNumber"].ToString();                
                txtIssueDate.Text = dtChequeIssueDet.Rows[0]["IssueDate"].ToString();
                txtAmount.Text = dtChequeIssueDet.Rows[0]["Amount"].ToString();
                txtPrevAmount.Text = dtChequeIssueDet.Rows[0]["Amount"].ToString();
                txtDescription.Text = dtChequeIssueDet.Rows[0]["Description"].ToString();
                
                PopulateChequeIssue();
                cboChequeBook.SelectedValue = dtChequeIssueDet.Rows[0]["ChequeBookID"].ToString();
                PopulateClient();
                cboClient.SelectedValue = dtChequeIssueDet.Rows[0]["ClientID"].ToString();

                PCSN.InvoiceSystem.BusinessLogicLayer.ChequeBooksInfo CHKInfo = new PCSN.InvoiceSystem.BusinessLogicLayer.ChequeBooksInfo();
                DataTable dtCHK = new DataTable();
                dtCHK = CHKInfo.GetChequeBooksInfoByID(Convert.ToInt32(dtChequeIssueDet.Rows[0]["ChequeBookID"].ToString()));
                if (dtCHK.Rows.Count > 0)
                {
                    txtBankID.Text = dtCHK.Rows[0]["BankID"].ToString();
                    txtBBalID.Text = dtCHK.Rows[0]["BBalID"].ToString();
                    txtAvBalance.Text = dtCHK.Rows[0]["AvailableBalance"].ToString();
                    try
                    {
                        txtAvBalance.Text = Convert.ToInt32(txtAvBalance.Text.ToString()).ToString();
                        lblSpErrMsg.Text = "";
                    }
                    catch
                    {
                        txtAvBalance.Text = "0";
                        lblSpErrMsg.Text = "Please add some Capital to your account in order to issue a Cheque.<br />OR Please insert a received cheque in this account. Thanks";
                    }
                }

            }

        }
        if (!Page.IsPostBack)
        {
            PopulateChequeIssue();
            PopulateClient();
            PopulateChequeBooks();
        }
    }
    private void dgChequeIssues_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        if (e.CommandName == "DeleteChequeIssue")
        {
            string argsID = e.CommandArgument.ToString();

            long ChequeIssueID = Convert.ToInt32(argsID.ToString());

            if (ChequeIssueID > 0)
            {
                PCSN.InvoiceSystem.BusinessLogicLayer.ChequeIssue ChequeIssue = new PCSN.InvoiceSystem.BusinessLogicLayer.ChequeIssue();
                dtChequeIssueDG = ChequeIssue.GetChequeIssueByID(ChequeIssueID);
                if (dtChequeIssueDG.Rows.Count > 0)
                {
                    txtAmount.Text = dtChequeIssueDG.Rows[0]["Amount"].ToString();
                    // Add Amount of a cheque into Balance
                    PCSN.InvoiceSystem.BusinessLogicLayer.BankAccountInfo BBal = new PCSN.InvoiceSystem.BusinessLogicLayer.BankAccountInfo();
                    PCSN.InvoiceSystem.BusinessLogicLayer.ChequeBooksInfo CBInfo = new PCSN.InvoiceSystem.BusinessLogicLayer.ChequeBooksInfo();

                    dtChequeIssueDG = CBInfo.GetChequeBooksInfoByID(Convert.ToInt32(dtChequeIssueDG.Rows[0]["ChequeBookID"].ToString()));

                    if (dtChequeIssueDG.Rows.Count > 0)
                    {
                        txtBankID.Text = dtChequeIssueDG.Rows[0]["BankID"].ToString();
                        txtBBalID.Text = dtChequeIssueDG.Rows[0]["BBalID"].ToString();
                        txtAvBalance.Text = dtChequeIssueDG.Rows[0]["AvailableBalance"].ToString();
                        
                        try
                        {
                            txtAvBalance.Text = Convert.ToInt32(txtAvBalance.Text.ToString()).ToString();
                        }
                        catch
                        {
                            txtAvBalance.Text = "0";
                            lblSpErrMsg.Text = "Please add some Capital to your account in order to issue a Cheque.<br />OR Please insert a received cheque in this account. Thanks";
                        }

                    }
                    long availBal = Convert.ToInt32(txtAvBalance.Text) + Convert.ToInt32(txtAmount.Text);
                    BBal.UpdateBankBalances(Convert.ToInt32(txtBBalID.Text), Convert.ToInt32(txtBankID.Text), availBal);
                    // Actuall Deletion of Cheque
                    ChequeIssue.DeleteChequeIssue(ChequeIssueID);
                    //=================================================
                    lblErrorMessage.Text = "Cheque Deleted Successfuly.";
                    PopulateChequeIssue();
                    PopulateClient();
                    PopulateChequeBooks();
                    ClearControls();
                }
            }
        }
    }

    private void dgChequeIssues_ItemCreated(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        LinkButton link = (LinkButton)e.Item.FindControl("Linkbutton2");
        if (link != null)
            link.Attributes.Add("onClick", "javascript:return confirm('This action will delete the information saved for this Issued Cheque.  Are you sure you want to delete this Cheque?');");

    }

    private void dgChequeIssues_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dgChequeIssues.CurrentPageIndex = e.NewPageIndex;
        PopulateChequeBooks();
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        PopulateChequeBooks();
        PopulateChequeBooks();
        PopulateClient();
        ClearControls();
    }
    protected void cboChequeBook_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (cboChequeBook.SelectedValue == "0")
        {
            txtChequeNumber.Text = "Please Generate a Cheque Book First.";
        }
        else
        {
            PCSN.InvoiceSystem.BusinessLogicLayer.ChequeIssue ChequeIssue = new PCSN.InvoiceSystem.BusinessLogicLayer.ChequeIssue();
            PCSN.InvoiceSystem.BusinessLogicLayer.ChequeBooksInfo CBInfo = new PCSN.InvoiceSystem.BusinessLogicLayer.ChequeBooksInfo();
            dtChequeIssueDG = ChequeIssue.GetChequeNumberMAX(Convert.ToInt32(cboChequeBook.SelectedValue.ToString()));
            if (dtChequeIssueDG.Rows.Count > 0)
            {
                if (dtChequeIssueDG.Rows[0]["LatestChequeNumber"].ToString() == "")
                {
                    //PCSN.InvoiceSystem.BusinessLogicLayer.ChequeBooksInfo CHKInfo = new PCSN.InvoiceSystem.BusinessLogicLayer.ChequeBooksInfo();
                    DataTable dtCHK = new DataTable();
                    dtCHK = CBInfo.GetChequeBooksInfoByID(Convert.ToInt32(cboChequeBook.SelectedValue.ToString()));
                    if (dtCHK.Rows.Count > 0)
                    {
                        txtChequeNumber.Text = dtCHK.Rows[0]["FirstChequeNumber"].ToString();
                    }

                }
                else
                {
                    txtChequeNumber.Text = Convert.ToString(Convert.ToInt32(dtChequeIssueDG.Rows[0]["LatestChequeNumber"].ToString()) + 1);
                    DataTable dtCHK = new DataTable();
                    dtCHK = CBInfo.GetChequeBooksInfoByID(Convert.ToInt32(cboChequeBook.SelectedValue.ToString()));
                    if (dtCHK.Rows.Count > 0)
                    {
                        long lastCheNum = Convert.ToInt32(dtCHK.Rows[0]["LastChequeNumber"].ToString());
                        if(lastCheNum == Convert.ToInt32(txtChequeNumber.Text))
                        {
                            lblSpErrMsg.Text = "This is your Last Cheque of this Cheque Book. Please Issue a new Cheque Book later";

                        }
                        else if (lastCheNum < Convert.ToInt32(txtChequeNumber.Text))
                        {
                            lblSpErrMsg.Text = "Cheque book is Empty. Please issue a new Cheque Book for this Account.";
                            txtChequeNumber.Text = "0";
                        }
                        else
                        {
                            lblSpErrMsg.Text = "";
                        }
                    }
                    
                }
            }
            else
            {
                txtChequeNumber.Text = "Please Generate a Cheque Book First.";
            }
            dtChequeIssueDG = CBInfo.GetChequeBooksInfoByID(Convert.ToInt32(cboChequeBook.SelectedValue.ToString()));
            if (dtChequeIssueDG.Rows.Count > 0)
            {
                txtBankID.Text = dtChequeIssueDG.Rows[0]["BankID"].ToString();
                txtBBalID.Text = dtChequeIssueDG.Rows[0]["BBalID"].ToString();
                txtAvBalance.Text = dtChequeIssueDG.Rows[0]["AvailableBalance"].ToString();
                try
                {
                    txtAvBalance.Text = Convert.ToInt32(txtAvBalance.Text.ToString()).ToString();                    
                }
                catch
                {
                    txtAvBalance.Text = "0";
                    lblSpErrMsg.Text = "Please add some Capital to your account in order to issue a Cheque.<br />OR Please insert a received cheque in this account. Thanks";
                }

            }

        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ValidateFields())
        {

            if (txtChequeIssueID.Text == "")
            {
                PCSN.InvoiceSystem.BusinessLogicLayer.ChequeIssue ChequeIssue = new PCSN.InvoiceSystem.BusinessLogicLayer.ChequeIssue();
                ChequeIssue.InsertChequeIssue(Convert.ToInt32(cboChequeBook.SelectedValue), txtChequeNumber.Text.ToString(), txtIssueDate.Text.ToString(), Convert.ToInt32(cboClient.SelectedValue), Convert.ToInt32(txtAmount.Text.ToString()), txtDescription.Text.ToString());
                
                PCSN.InvoiceSystem.BusinessLogicLayer.BankAccountInfo BBal = new PCSN.InvoiceSystem.BusinessLogicLayer.BankAccountInfo();
                
                long availBal = Convert.ToInt32(txtAvBalance.Text) - Convert.ToInt32(txtAmount.Text);                
                BBal.UpdateBankBalances(Convert.ToInt32(txtBBalID.Text), Convert.ToInt32(txtBankID.Text), availBal);
                
                lblErrorMessage.Text = "Issued Cheque Have been saved.";
                PopulateChequeIssue();
                PopulateClient();
                PopulateChequeBooks();
                ClearControls();

            }
            else
            {
                PCSN.InvoiceSystem.BusinessLogicLayer.ChequeIssue ChequeIssue = new PCSN.InvoiceSystem.BusinessLogicLayer.ChequeIssue();
                ChequeIssue.UpdateChequeIssue(Convert.ToInt32(txtChequeIssueID.Text), Convert.ToInt32(cboChequeBook.SelectedValue), txtChequeNumber.Text.ToString(), txtIssueDate.Text.ToString(), Convert.ToInt32(cboClient.SelectedValue), Convert.ToInt32(txtAmount.Text.ToString()), txtDescription.Text.ToString());
                lblErrorMessage.Text = "Issue Cheque have been updated.";

                PCSN.InvoiceSystem.BusinessLogicLayer.BankAccountInfo BBal = new PCSN.InvoiceSystem.BusinessLogicLayer.BankAccountInfo();

                long availBal = Convert.ToInt32(Convert.ToInt32(txtAvBalance.Text) + Convert.ToInt32(txtPrevAmount.Text)) - Convert.ToInt32(txtAmount.Text);
                BBal.UpdateBankBalances(Convert.ToInt32(txtBBalID.Text), Convert.ToInt32(txtBankID.Text), availBal);
                
                PopulateChequeIssue();
                PopulateClient();
                PopulateChequeBooks();
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
                message += "Amount must be Numeric.<br>";
                error = true;
                txtAmount.Text = "0";
            }
        }
        
        if (txtDescription.Text.Trim() == "")
        {
            message += "Description is not specified.<br>";
            error = true;
        }
        if (txtChequeNumber.Text.Trim() == "")
        {
            message += "Cheque Number is not specified.<br>";
            error = true;
        }
        else
        {
            try
            {
                txtChequeNumber.Text = Convert.ToInt32(txtChequeNumber.Text.ToString()).ToString();
                if (txtChequeNumber.Text == "0")
                {
                    message += "Cheque book is Empty. Please issue a new Cheque Book for this Account.";
                    error = true;
                }
                
            }
            catch
            {
                message += "Cheque Number must be Numeric.<br>";
                error = true;
                txtChequeNumber.Text = "0";
            }
        }
        if (txtIssueDate.Text.Trim() == "")
        {
            message += "Branch is not specified.<br>";
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
        if (cboChequeBook.SelectedValue == "0")
        {
            message += "Cheque Book is not specified.<br>";
            error = true;
        }

        if (cboClient.SelectedValue == "0")
        {
            message += "Client is not specified.<br>";
            error = true;
        }
        if (txtAvBalance.Text == "0")
        {
            message += "You can not generate a Cheque as you do not have sufficient funds available in your account.<br>";
            error = true;
        }
        else
        {
            try
            {
                txtAvBalance.Text = Convert.ToInt32(txtAvBalance.Text.ToString()).ToString();
                if (Convert.ToInt32(txtAvBalance.Text.ToString()) < Convert.ToInt32(txtAmount.Text))
                {
                    message += "You can not issue a Cheque having more amount than available amount.<br>";
                    error = true;
                }
                if (txtPrevAmount.Text != "")
                {
                    if (Convert.ToInt32(Convert.ToInt32(txtAvBalance.Text.ToString()) + Convert.ToInt32(txtPrevAmount.Text)) < Convert.ToInt32(txtAmount.Text))
                    {
                        message += "You can not issue a Cheque having more amount than available amount.<br>";
                        error = true;
                    }
                }

            }
            catch
            {
                message += "You can not generate a Cheque as you do not have sufficient funds available in your account.<br>";
                error = true;
            }
        }

        lblErrorMessage.Text = "";
        if (error)
            lblErrorMessage.Text = message;

        return !error;
    }

    private void PopulateChequeIssue()
    {
        PCSN.InvoiceSystem.BusinessLogicLayer.ChequeIssue ChequeIssue = new PCSN.InvoiceSystem.BusinessLogicLayer.ChequeIssue();
        dtChequeIssueDG = ChequeIssue.GetAllChequeIssue();
        dgChequeIssues.DataSource = dtChequeIssueDG;
        dgChequeIssues.DataBind();
    }

    private void PopulateChequeBooks()
    {
        PCSN.InvoiceSystem.BusinessLogicLayer.ChequeBooksInfo ChequeBooks = new PCSN.InvoiceSystem.BusinessLogicLayer.ChequeBooksInfo();
        dtChequeIssueDG = ChequeBooks.GetChequeBooksInfoForDropDown();
        cboChequeBook.DataSource = dtChequeIssueDG;
        cboChequeBook.DataTextField = "Name";
        cboChequeBook.DataValueField = "ID";

        cboChequeBook.DataBind();
    }

    private void PopulateClient()
    {
        PCSN.InvoiceSystem.BusinessLogicLayer.Client client = new PCSN.InvoiceSystem.BusinessLogicLayer.Client();
        dtChequeIssueDG = client.GetClientForDropDown("ForAll");
        cboClient.DataSource = dtChequeIssueDG;
        cboClient.DataTextField = "Name";
        cboClient.DataValueField = "ID";

        cboClient.DataBind();
    }

    

    public string EditChequeIssue(string ChequeIssueID)
    {
        return "ChequeIssue.aspx?ChequeIssueIDED=" + ChequeIssueID;
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("default.aspx");
    }
    public string DeleteChequeIssue(string ChequeIssueID)
    {
        return ChequeIssueID;
    }
    public void ClearControls()
    {
        txtChequeIssueID.Text = "";
        txtChequeNumber.Text = "";
        txtAmount.Text = "";
        txtDescription.Text = "";
        txtIssueDate.Text = "";
        cboChequeBook.SelectedValue = "0";
        cboClient.SelectedValue = "0";
        lblSpErrMsg.Text = "";
        txtAvBalance.Text = "";
        txtBankID.Text = "";
        txtPrevAmount.Text = "";
        txtBBalID.Text = "";

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
        this.dgChequeIssues.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgChequeIssues_PageIndexChanged);
        this.dgChequeIssues.ItemCreated += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgChequeIssues_ItemCreated);
        this.dgChequeIssues.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgChequeIssues_ItemCommand);

    }
    #endregion

    
}
