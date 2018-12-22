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

public partial class SearchLoan : System.Web.UI.Page
{
    #region Variable Declaration
    DataTable dtLoanDG = new DataTable();
    #endregion

    #region Event Handler
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //PopulateLoan();
            PopulateLoanAccounts();            
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("default.aspx");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string error = "";
        //lblGranTotal.Text = "0";
        if (txtFromDate.Text != "" && txtToDate.Text != "")
        {
            string FromDate = txtFromDate.Text.ToString();
            string ToDate = txtToDate.Text.ToString();

            try
            {
                FromDate = Convert.ToDateTime(txtFromDate.Text).ToShortDateString();
                ToDate = Convert.ToDateTime(txtToDate.Text).ToShortDateString();
                PopulateLoanBySearch("ToAndFromDate", FromDate.ToString() + "#" + ToDate.ToString());
            }
            catch
            {
                error += "Dates must be valid. <br />";
            }
        }
        else if (txtIssueDate.Text != "")
        {
            try
            {
                txtIssueDate.Text = Convert.ToDateTime(txtIssueDate.Text.ToString()).ToShortDateString();
                PopulateLoanBySearch("LoanByIssueDate", txtIssueDate.Text.ToString());
            }
            catch
            {
                error += "Date is not Valid. <br />";
            }
        }
        else if (txtDueDate.Text != "")
        {
            try
            {
                txtDueDate.Text = Convert.ToDateTime(txtDueDate.Text.ToString()).ToShortDateString();
                PopulateLoanBySearch("LoanByDueDate", txtDueDate.Text.ToString());
            }
            catch
            {
                error += "Date is not Valid. <br />";
            }
        }
        else if (cboByMonth.SelectedValue != "0" && cboByYear.SelectedValue != "0")
        {
            string SelMonth = "";
            string SelYear = "";
            try
            {
                SelMonth = cboByMonth.SelectedValue.ToString();
                SelYear = cboByYear.SelectedValue.ToString();

                string dtimeSearch = Convert.ToDateTime(SelMonth.Trim() + "/1/" + SelYear.Trim()).ToShortDateString();

                PopulateLoanBySearch("LoanByMonth", dtimeSearch.ToString());
            }
            catch
            {
                error += "Month and Years are not in Valid Format. <br />";
            }
        }
        else if (cboLoanAcc.SelectedValue != "0")
        {
            PopulateLoanBySearch("LoanByAccount", cboLoanAcc.SelectedValue.ToString());
        }
        else
        {
            //PopulateLoan();
        }

        if (error != "")
            lblErrorMessage.Text = error.ToString();
        else
            lblErrorMessage.Text = "";


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
    #endregion

    #region Methods

    private void PopulateLoanBySearch(string FieldName, string Value)
    {
        PCSN.InvoiceSystem.BusinessLogicLayer.Loan Loan = new PCSN.InvoiceSystem.BusinessLogicLayer.Loan();
        if (FieldName == "ToAndFromDate")
        {
            string[] ToandFrom = Value.Split('#');
            string toDate = "";
            string fromDate = "";
            for (int i = 0; i < ToandFrom.Length; i++)
            {
                toDate = ToandFrom[i].ToString();
                fromDate = ToandFrom[i + 1].ToString();
                break;
            }

            dtLoanDG = Loan.GetAllLoanByToAndFromDate(toDate.ToString(), fromDate.ToString());
        }
        if (FieldName == "LoanByMonth")
        {
            dtLoanDG = Loan.GetAllLoanByMonthYear(Value.ToString());
        }
        if (FieldName == "LoanByAccount")
        {
            dtLoanDG = Loan.GetAllLoanByAccountID(Convert.ToInt32(Value.ToString()));
        }        
        if (FieldName == "LoanByDueDate")
        {
            dtLoanDG = Loan.GetLoanByDueDate(Value.ToString());
        }
        if (FieldName == "LoanByIssueDate")
        {
            dtLoanDG = Loan.GetAllLoanByLoanDate(Value.ToString());
        }

        dgLoans.DataSource = dtLoanDG;
        dgLoans.DataBind();

        ClearControls();

    }

    private void PopulateLoan()
    {
        PCSN.InvoiceSystem.BusinessLogicLayer.Loan Loan = new PCSN.InvoiceSystem.BusinessLogicLayer.Loan();
        dtLoanDG = Loan.GetAllLoan();
        dgLoans.DataSource = dtLoanDG;
        dgLoans.DataBind();
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

    private void ClearControls()
    {        
        txtDueDate.Text = "";
        txtIssueDate.Text = "";
        txtFromDate.Text = "";
        txtToDate.Text = "";
        cboLoanAcc.SelectedValue = "0";       
        cboByMonth.SelectedValue = "0";
        cboByYear.SelectedValue = "0";
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
