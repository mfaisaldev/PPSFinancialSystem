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

public partial class SearchChequeIssue : System.Web.UI.Page
{
    #region Variables
    //private long ChequeIssueID = 0;
    DataTable dtChequeIssueDG = new DataTable();
    #endregion


    #region Event Handler

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            PopulateBanks();
            PopulateClient();
            ClearControls();
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
                PopulateChequeIssueBySearch("ToAndFromDate", FromDate.ToString() + "#" + ToDate.ToString());
            }
            catch
            {
                error += "Dates must be valid. <br />";
            }
        }
        else if (txtDate.Text != "")
        {
            try
            {
                txtDate.Text = Convert.ToDateTime(txtDate.Text.ToString()).ToShortDateString();
                PopulateChequeIssueBySearch("ChequeIssueByIssueDate", txtDate.Text.ToString());
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

                PopulateChequeIssueBySearch("ChequeIssueByMonth", dtimeSearch.ToString());
            }
            catch
            {
                error += "Month and Years are not in Valid Format. <br />";
            }
        }
        else if (cboBank.SelectedValue != "0" && cboChequeBook.SelectedValue != "0" && cboChequeNumber.SelectedValue != "0")
        {
            try
            {
                PopulateChequeIssueBySearch("ChequeIssueByChequeNumber", cboChequeNumber.SelectedValue.ToString());
            }
            catch
            {
                error += "You Must Select Bank, Account Number, Cheque Number to search a Specific Cheque. <br />";
            }
        }
        else if (cboBank.SelectedValue != "0" && cboChequeBook.SelectedValue != "0" && cboChequeNumber.SelectedValue == "0")
        {
            try
            {
                PopulateChequeIssueBySearch("ChequeIssueByChequeBookNumber", cboChequeBook.SelectedValue.ToString());
            }
            catch
            {
                error += "You Must Select Bank, Account Number, Cheque Book Number to search a Specific Cheque Book in an Account. <br />";
            }
        }
        else if (cboBank.SelectedValue != "0" && cboChequeBook.SelectedValue == "0" && cboChequeNumber.SelectedValue == "0" || cboChequeNumber.SelectedValue == "")
        {
            try
            {
                PopulateChequeIssueBySearch("ChequeIssueByBankID", cboBank.SelectedValue.ToString());
            }
            catch
            {
                error += "You Must Select Bank and Account Number to search a Specific Cheque books in an Account. <br />";
            }
        }
        else
        {
            //PopulateChequeIssue();
        }

        if (error != "")
            lblErrorMessage.Text = error.ToString();
        else
            lblErrorMessage.Text = "";
        

    }

    protected void cboBank_SelectedIndexChanged(object sender, EventArgs e)
    {
        string BankID = cboBank.SelectedItem.Text.ToString();
        string[] idSpliter = BankID.Split('#');
        string BankName = "";
        string AccNumber = "";
        if (cboBank.SelectedValue != "0")
        {
            for (int i = 0; i < idSpliter.Length; i++)
            {
                BankName = idSpliter[i].ToString();
                AccNumber = idSpliter[i + 1].ToString();
                break;
            }
            if (BankName == "" || AccNumber == "")
            {
                
            }
            else
            {                
                PopulateChequeBooks(Convert.ToInt32(cboBank.SelectedValue.ToString()), AccNumber.ToString());
                lblAccount.Text = "Please Select Cheque Book Now.";
            }
        }
    }

    protected void cboChequeBook_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (cboChequeBook.SelectedValue == "0")
        {
            lblErrorMessage.Text = "Please Generate a Cheque Book First.";
        }
        else
        {
            PopulateChequeNumbers(Convert.ToInt32(cboChequeBook.SelectedValue.ToString()));
            lblChequeNumber.Text = "Please Select Cheque Number here now!";
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
                ChequeIssue.DeleteChequeIssue(ChequeIssueID);
                lblErrorMessage.Text = "Cheque Deleted Successfuly.";
                PopulateBanks();
                PopulateClient();
                ClearControls();
                
            }
        }
    }

    private void dgChequeIssues_ItemCreated(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        LinkButton link = (LinkButton)e.Item.FindControl("Linkbutton2");
        if (link != null)
            link.Attributes.Add("onClick", "javascript:return confirm('This action will delete the information saved for this Issued Cheque.  Are you sure you want to delete this Cheque?');");

    }
    #endregion
    #region Methods

    private void PopulateChequeIssueBySearch(string FieldName, string Value)
    {
        PCSN.InvoiceSystem.BusinessLogicLayer.ChequeIssue ChequeIssue = new PCSN.InvoiceSystem.BusinessLogicLayer.ChequeIssue();
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

            dtChequeIssueDG = ChequeIssue.GetChequeIssueByToandFromDate(toDate.ToString(), fromDate.ToString());
        }
        if (FieldName == "ChequeIssueByMonth")
        {
            dtChequeIssueDG = ChequeIssue.GetChequeIssueByMonthYear(Value.ToString());
        }
        if (FieldName == "ChequeIssueByChequeNumber")
        {
            dtChequeIssueDG = ChequeIssue.GetChequeIssueByChequeNumber(Convert.ToInt32(Value.ToString()));
        }
        if (FieldName == "ChequeIssueByChequeBookNumber")
        {
            dtChequeIssueDG = ChequeIssue.GetChequeIssueByChequeBookIDForSearch(Convert.ToInt32(Value.ToString()));
        }
        if (FieldName == "ChequeIssueByBankID")
        {
            dtChequeIssueDG = ChequeIssue.GetChequeIssueByBankID(Convert.ToInt32(Value.ToString()));
        }
        if (FieldName == "ChequeIssueByIssueDate")
        {
            dtChequeIssueDG = ChequeIssue.GetChequeIssueByIssueDate(Value.ToString());
        }
        
        dgChequeIssues.DataSource = dtChequeIssueDG;
        dgChequeIssues.DataBind();

        ClearControls();

    }

    private void PopulateChequeIssue()
    {
        PCSN.InvoiceSystem.BusinessLogicLayer.ChequeIssue ChequeIssue = new PCSN.InvoiceSystem.BusinessLogicLayer.ChequeIssue();
        dtChequeIssueDG = ChequeIssue.GetAllChequeIssue();
        dgChequeIssues.DataSource = dtChequeIssueDG;
        dgChequeIssues.DataBind();
    }

    private void PopulateChequeBooks(long BankID, string AccountNumber)
    {
        PCSN.InvoiceSystem.BusinessLogicLayer.ChequeBooksInfo ChequeBooks = new PCSN.InvoiceSystem.BusinessLogicLayer.ChequeBooksInfo();
        dtChequeIssueDG = ChequeBooks.GetChequeBooksInfoForDropDownByBankIDandAccNum(BankID, AccountNumber);
        cboChequeBook.DataSource = dtChequeIssueDG;
        cboChequeBook.DataTextField = "Name";
        cboChequeBook.DataValueField = "ID";

        cboChequeBook.DataBind();
    }

    private void PopulateChequeNumbers(long ChequeBookID)
    {
        PCSN.InvoiceSystem.BusinessLogicLayer.ChequeIssue ChequeIssue = new PCSN.InvoiceSystem.BusinessLogicLayer.ChequeIssue();
        dtChequeIssueDG = ChequeIssue.GetChequeIssueByChequeBookID(ChequeBookID);
        cboChequeNumber.DataSource = dtChequeIssueDG;
        cboChequeNumber.DataTextField = "Name";
        cboChequeNumber.DataValueField = "ID";

        cboChequeNumber.DataBind();
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

    private void PopulateBanks()
    {
        PCSN.InvoiceSystem.BusinessLogicLayer.BankAccountInfo ChequeBook = new PCSN.InvoiceSystem.BusinessLogicLayer.BankAccountInfo();
        dtChequeIssueDG = ChequeBook.GetBankAccountInfoForDropDown();
        cboBank.DataSource = dtChequeIssueDG;
        cboBank.DataTextField = "Name";
        cboBank.DataValueField = "ID";

        cboBank.DataBind();
    }

    private void ClearControls()
    {
        lblBank.Text = "Please Select Bank First.";
        lblChequeNumber.Text = "";
        lblAccount.Text="";
        txtDate.Text = "";
        txtFromDate.Text = "";
        txtToDate.Text = "";
        cboBank.SelectedValue = "0";
        cboChequeBook.Items.Clear();
        cboChequeNumber.Items.Clear();
        cboByMonth.SelectedValue = "0";
        cboByYear.SelectedValue = "0";
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

        this.dgChequeIssues.ItemCreated += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgChequeIssues_ItemCreated);
        this.dgChequeIssues.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgChequeIssues_ItemCommand);

    }
    #endregion
    
}
