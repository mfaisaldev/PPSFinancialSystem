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

public partial class ManageLoan : System.Web.UI.Page
{
    #region Variables
    //private long LoanID = 0;
    DataTable dtLoanDG = new DataTable();
    DataTable dtAccRecPay = new DataTable();
    #endregion

    # region Event Handler
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.QueryString["LoanIDED"] != null && Request.QueryString["LoanIDED"].ToString() != "" && !Page.IsPostBack)
        {
            PCSN.InvoiceSystem.BusinessLogicLayer.Loan Loan = new PCSN.InvoiceSystem.BusinessLogicLayer.Loan();
            DataTable dtLoanDet = new DataTable();
            dtLoanDet = Loan.GetLoanByID(Convert.ToInt32(Request.QueryString["LoanIDED"].ToString()));

            if (dtLoanDet.Rows.Count > 0)
            {
                txtLoanID.Text = dtLoanDet.Rows[0]["ID"].ToString();
                cboLoanAcc.SelectedValue = dtLoanDet.Rows[0]["LoanAccID"].ToString();
                txtLoanDate.Text = dtLoanDet.Rows[0]["LoanDate"].ToString();
                txtDueDate.Text = dtLoanDet.Rows[0]["DueDate"].ToString();
                txtAmount.Text = dtLoanDet.Rows[0]["Amount"].ToString();               
                txtDescription.Text = dtLoanDet.Rows[0]["Description"].ToString();
                if (dtLoanDet.Rows[0]["IsCleared"].ToString() == "True")
                {
                    lblErrorMessage.Text = "This Loan is Cleared.";
                }
                if (txtLoanID.Text != "")
                {
                    DataTable dtAccRecPay = new DataTable();
                    dtAccRecPay = Loan.GetAllAccReceivableByLoanID(Convert.ToInt32(txtLoanID.Text.ToString()));
                    if (dtAccRecPay.Rows.Count > 0)
                    {
                        rdoAcREC.Checked = true;
                    }
                    else
                    {
                        dtAccRecPay = Loan.GetAllAccPayableByLoanID(Convert.ToInt32(txtLoanID.Text.ToString()));
                        if (dtAccRecPay.Rows.Count > 0)
                        {
                            rdoAcPAY.Checked = true;
                        }
                        else
                        {
                            lblErrorMessage.Text = "You have not defined this entry as Account Receivable or Payable yet.";
                        }
                    }
                }
            }

        }
        if (!Page.IsPostBack)
        {
            PopulateLoan();
            PopulateLoanAccounts();
            PopulateAccPay();
            PopulateAccRec();
        }
    }
    private void dgLoans_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        if (e.CommandName == "DeleteLoan")
        {
            string argsID = e.CommandArgument.ToString();

            long LoanID = Convert.ToInt32(argsID.ToString());

            if (LoanID > 0)
            {
                PCSN.InvoiceSystem.BusinessLogicLayer.Loan Loan = new PCSN.InvoiceSystem.BusinessLogicLayer.Loan();
                Loan.DeleteLoan(LoanID);
                lblErrorMessage.Text = "Loan Account Deleted Successfuly.";
                PopulateLoan();
                PopulateLoanAccounts();
                PopulateAccPay();
                PopulateAccRec();
                ClearControls();
            }
        }
    }

    private void dgLoans_ItemCreated(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        LinkButton link = (LinkButton)e.Item.FindControl("Linkbutton2");
        if (link != null)
            link.Attributes.Add("onClick", "javascript:return confirm('This action will delete all the information saved related to this Loan.  Are you sure you want to delete this Loan ?');");

    }
    private void dgLoans_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dgLoans.CurrentPageIndex = e.NewPageIndex;
        PopulateLoan();
    }
    protected void cboLoanSearch_SelectedIndexChanged1(object sender, EventArgs e)
    {
        if (cboLoanSearch.SelectedValue == "Show All")
        {
            PopulateLoan();
        }
        else
        {
            PopulateLoanBySearch("IsCleared", cboLoanSearch.SelectedValue.ToString());
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        PopulateAccPay();
        PopulateAccRec();
        PopulateLoan();
        PopulateLoanAccounts();        
        ClearControls();
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("default.aspx");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ValidateFields())
        {

            if (txtLoanID.Text == "")
            {
                PCSN.InvoiceSystem.BusinessLogicLayer.Loan Loan = new PCSN.InvoiceSystem.BusinessLogicLayer.Loan();
                long LoanID = Loan.InsertLoan(Convert.ToInt32(cboLoanAcc.SelectedValue.ToString()), txtLoanDate.Text.ToString(), Convert.ToInt32(txtAmount.Text.ToString()), txtDueDate.Text.ToString(), txtDescription.Text.ToString());
                if (LoanID > 0)
                {
                    if (rdoAcREC.Checked == true)
                    {
                        Loan.InsertAccReceivable(LoanID, 0, DateTime.Now.ToShortDateString(), "Nill");
                    }
                    if (rdoAcPAY.Checked == true)
                    {
                        Loan.InsertAccPayable(LoanID, 0, DateTime.Now.ToShortDateString(), "Nill");
                    }
                    lblErrorMessage.Text = "Loan has been saved.";
                    PopulateLoan();
                    PopulateLoanAccounts();
                    PopulateAccPay();
                    PopulateAccRec();
                    ClearControls();

                }              

            }
            else
            {
                PCSN.InvoiceSystem.BusinessLogicLayer.Loan Loan = new PCSN.InvoiceSystem.BusinessLogicLayer.Loan();
                Loan.UpdateLoan(Convert.ToInt32(txtLoanID.Text), Convert.ToInt32(cboLoanAcc.SelectedValue.ToString()), txtLoanDate.Text.ToString(), Convert.ToInt32(txtAmount.Text.ToString()), txtDueDate.Text.ToString(), txtDescription.Text.ToString());

                DataTable dtAccRecPay = new DataTable();
                dtAccRecPay = Loan.GetAllAccReceivableByLoanID(Convert.ToInt32(txtLoanID.Text.ToString()));
                if (dtAccRecPay.Rows.Count > 0)
                {
                    
                }
                else
                {
                    dtAccRecPay = Loan.GetAllAccPayableByLoanID(Convert.ToInt32(txtLoanID.Text.ToString()));
                    if (dtAccRecPay.Rows.Count > 0)
                    {
                        
                    }
                    else
                    {
                        if (rdoAcREC.Checked == true)
                        {
                            Loan.InsertAccReceivable(Convert.ToInt32(txtLoanID.Text), 0, DateTime.Now.ToShortDateString(), "Nill");
                        }
                        if (rdoAcPAY.Checked == true)
                        {
                            Loan.InsertAccPayable(Convert.ToInt32(txtLoanID.Text), 0, DateTime.Now.ToShortDateString(), "Nill");
                        }
                        
                    }
                }

                lblErrorMessage.Text = "Loan have been updated.";

                PopulateLoan();
                PopulateLoanAccounts();
                PopulateAccPay();
                PopulateAccRec();
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

        if (txtLoanDate.Text.Trim() == "")
        {
            message += "Loan Date is not specified.<br>";
            error = true;
        }
        else
        {
            try
            {
                txtLoanDate.Text = Convert.ToDateTime(txtLoanDate.Text.ToString()).ToShortDateString();
            }
            catch
            {
                txtLoanDate.Text = "";
                message += "Loan Date is not a Valid Date.<br>";
                error = true;
            }
        }
        if (txtDueDate.Text.Trim() == "")
        {
            message += "Due Date is not specified.<br>";
            error = true;
        }
        else
        {
            try
            {
                txtDueDate.Text = Convert.ToDateTime(txtDueDate.Text.ToString()).ToShortDateString();
            }
            catch
            {
                txtDueDate.Text = "";
                message += "Due Date is not a Valid Date.<br>";
                error = true;
            }
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
                txtAmount.Text = "0";
                message += "Amount must be numeric only.<br>";
                error = true;
            }
        }
        if (txtDescription.Text.Trim() == "")
        {
            message += "Description is not specified.<br>";
            error = true;
        }
        if (rdoAcPAY.Checked == false && rdoAcREC.Checked == false)
        {
            message += "You must mention this Loan as Account Receivable or Payable.";
            error = true;
        }

        lblErrorMessage.Text = "";
        if (error)
            lblErrorMessage.Text = message;

        return !error;
    }

    private void PopulateLoan()
    {
        PCSN.InvoiceSystem.BusinessLogicLayer.Loan Loan = new PCSN.InvoiceSystem.BusinessLogicLayer.Loan();
        dtLoanDG = Loan.GetAllLoan();
        dgLoans.DataSource = dtLoanDG;
        dgLoans.DataBind();
    }

    private void PopulateLoanBySearch(string FieldName, string Value)
    {
        PCSN.InvoiceSystem.BusinessLogicLayer.Loan Loan = new PCSN.InvoiceSystem.BusinessLogicLayer.Loan();
        dtLoanDG = Loan.GetAllLoanByCleared(Convert.ToInt16(Value));
        dgLoans.DataSource = dtLoanDG;
        dgLoans.DataBind();
    }

    private void PopulateAccRec()
    {
        PCSN.InvoiceSystem.BusinessLogicLayer.Loan Loan = new PCSN.InvoiceSystem.BusinessLogicLayer.Loan();
        dtLoanDG = Loan.GetAllAccReceivable();
        dgAccReceivable.DataSource = dtLoanDG;
        dgAccReceivable.DataBind();
    }

    private void PopulateAccPay()
    {
        PCSN.InvoiceSystem.BusinessLogicLayer.Loan Loan = new PCSN.InvoiceSystem.BusinessLogicLayer.Loan();
        dtLoanDG = Loan.GetAllAccPayable();
        dgAccPayable.DataSource = dtLoanDG;
        dgAccPayable.DataBind();
    }


    private void PopulateLoanAccounts()
    {
        PCSN.InvoiceSystem.BusinessLogicLayer.Loan Loan = new PCSN.InvoiceSystem.BusinessLogicLayer.Loan();
        dtLoanDG = Loan.GetLoanAccountForDropDown();
        cboLoanAcc.DataSource = dtLoanDG;
        cboLoanAcc.DataTextField = "Name";
        cboLoanAcc.DataValueField = "ID";
        cboLoanAcc.DataBind();
    }
        
    

    public string TransINOUT(string LoanID)
    {
        PCSN.InvoiceSystem.BusinessLogicLayer.Loan Loan = new PCSN.InvoiceSystem.BusinessLogicLayer.Loan();

        dtAccRecPay = Loan.GetAllAccReceivableByLoanID(Convert.ToInt32(LoanID));
        if (dtAccRecPay.Rows.Count > 0)
        {
            return "ManageAccReceivable.aspx?LoanIDAccRec=" + LoanID;
        }
        else
        {
            dtAccRecPay = Loan.GetAllAccPayableByLoanID(Convert.ToInt32(LoanID));
            if (dtAccRecPay.Rows.Count > 0)
            {
                return "ManageAccPayable.aspx?LoanIDAccPay=" + LoanID;
            }
            else
            {
                return "ManageLoan.aspx?LoanIDED=" + LoanID;
            }
        }
        
    }
    public string EditLoan(string LoanID)
    {
        return "ManageLoan.aspx?LoanIDED=" + LoanID;
    }
    public string ViewLoan(string LoanID)
    {
        return "ViewLoan.aspx?LoanID=" + LoanID;
    }
    public string DeleteLoan(string LoanID)
    {
        return LoanID;
    }

    public string EditAccRec(string LoanID, string AccRecID)
    {
        return "ManageAccReceivable.aspx?LoanIDAccRec=" + LoanID + "&AccRecIDED=" + AccRecID;
    }
    public string DeleteAccRec(string LoanID)
    {
        return LoanID;
    }

    public string EditAccPay(string LoanID, string AccPayID)
    {
        return "ManageAccPayable.aspx?LoanIDAccPay=" + LoanID + "&AccPayIDED=" + AccPayID;
    }
    public string DeleteAccPay(string LoanID)
    {
        return LoanID;
    }

    public void ClearControls()
    {
        txtLoanID.Text = "";
        txtLoanDate.Text = "";
        txtAmount.Text = "";
        txtDueDate.Text = "";
        txtDescription.Text = "";
        rdoAcREC.Checked = true;
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
        this.dgLoans.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgLoans_PageIndexChanged);
        this.dgLoans.ItemCreated += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgLoans_ItemCreated);
        this.dgLoans.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgLoans_ItemCommand);

    }
    #endregion



}
