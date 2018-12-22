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

public partial class ChequeBooksInfo : System.Web.UI.Page
{
    #region Variables
    //private long ChequeBookID = 0;
    DataTable dtChequeBookDG = new DataTable();
    #endregion

    # region Event Handler
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.QueryString["ChequeBookIDED"] != null && Request.QueryString["ChequeBookIDED"].ToString() != "" && !Page.IsPostBack)
        {
            PCSN.InvoiceSystem.BusinessLogicLayer.ChequeBooksInfo ChequeBook = new PCSN.InvoiceSystem.BusinessLogicLayer.ChequeBooksInfo();
            DataTable dtChequeBookDet = new DataTable();
            dtChequeBookDet = ChequeBook.GetChequeBooksInfoByID(Convert.ToInt32(Request.QueryString["ChequeBookIDED"].ToString()));

            if (dtChequeBookDet.Rows.Count > 0)
            {
                txtChequeBookID.Text = dtChequeBookDet.Rows[0]["ID"].ToString();
                txtChequeBookNumber.Text = dtChequeBookDet.Rows[0]["ChequeBookNumber"].ToString();
                txtChequeNumberPrefix.Text = dtChequeBookDet.Rows[0]["ChequeNumberPreFix"].ToString();
                txtFirstChequeNumber.Text = dtChequeBookDet.Rows[0]["FirstChequeNumber"].ToString();
                txtLastChequeNumber.Text = dtChequeBookDet.Rows[0]["LastChequeNumber"].ToString();
                txtIssueDate.Text = dtChequeBookDet.Rows[0]["IssueDate"].ToString();
                txtEndDate.Text = dtChequeBookDet.Rows[0]["EndDate"].ToString();
                chkIsFinished.Checked = Convert.ToBoolean(dtChequeBookDet.Rows[0]["IsFinished"].ToString());
                PopulateChequeBooks();
                cboBank.SelectedValue = dtChequeBookDet.Rows[0]["BankID"].ToString();

            }

        }
        if (!Page.IsPostBack)
        {
            PopulateChequeBook();
            PopulateChequeBooks();
        }
    }
    private void dgChequeBooks_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        if (e.CommandName == "DeleteChequeBook")
        {
            string argsID = e.CommandArgument.ToString();

            long ChequeBookID = Convert.ToInt32(argsID.ToString());

            if (ChequeBookID > 0)
            {
                PCSN.InvoiceSystem.BusinessLogicLayer.ChequeBooksInfo ChequeBook = new PCSN.InvoiceSystem.BusinessLogicLayer.ChequeBooksInfo();
                ChequeBook.DeleteChequeBooksInfo(ChequeBookID);
                lblErrorMessage.Text = "ChequeBook Deleted Successfuly.";
                PopulateChequeBook();
                ClearControls();
            }
        }
    }

    private void dgChequeBooks_ItemCreated(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        LinkButton link = (LinkButton)e.Item.FindControl("Linkbutton2");
        if (link != null)
            link.Attributes.Add("onClick", "javascript:return confirm('This action will delete the information saved for this ChequeBook.  Are you sure you want to delete this ChequeBook?');");

    }

    private void dgChequeBooks_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dgChequeBooks.CurrentPageIndex = e.NewPageIndex;
        PopulateChequeBook();
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        PopulateChequeBook();
        PopulateChequeBooks();
        ClearControls();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ValidateFields())
        {

            if (txtChequeBookID.Text == "")
            {
                PCSN.InvoiceSystem.BusinessLogicLayer.ChequeBooksInfo ChequeBook = new PCSN.InvoiceSystem.BusinessLogicLayer.ChequeBooksInfo();
                ChequeBook.InsertChequeBooksInfo(Convert.ToInt32(cboBank.SelectedValue), Convert.ToInt32(txtChequeBookNumber.Text.ToString()), txtChequeNumberPrefix.Text.ToString(), txtFirstChequeNumber.Text.ToString(), txtLastChequeNumber.Text.ToString(), txtIssueDate.Text.ToString(), txtEndDate.Text.ToString());
                lblErrorMessage.Text = "ChequeBook Account Have been saved.";
                PopulateChequeBook();
                ClearControls();

            }
            else
            {
                PCSN.InvoiceSystem.BusinessLogicLayer.ChequeBooksInfo ChequeBook = new PCSN.InvoiceSystem.BusinessLogicLayer.ChequeBooksInfo();
                ChequeBook.UpdateChequeBooksInfo(Convert.ToInt32(txtChequeBookID.Text), Convert.ToInt32(cboBank.SelectedValue), Convert.ToInt32(txtChequeBookNumber.Text.ToString()), txtChequeNumberPrefix.Text.ToString(), txtFirstChequeNumber.Text.ToString(), txtLastChequeNumber.Text.ToString(), txtIssueDate.Text.ToString(), txtEndDate.Text.ToString());
                lblErrorMessage.Text = "ChequeBook Account have been updated.";
                PopulateChequeBook();
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

        if (txtEndDate.Text.Trim() == "")
        {
            message += "End Dater is not specified.<br>";
            error = true;
        }

        
        if (txtFirstChequeNumber.Text.Trim() == "")
        {
            message += "First Cheque Number is not specified.<br>";
            error = true;
        }
        else
        {
            try
            {
                txtFirstChequeNumber.Text = Convert.ToInt32(txtFirstChequeNumber.Text.ToString()).ToString();
            }
            catch
            {
                message += "First Cheque Number must be Numeric.<br>";
                error = true;
                txtFirstChequeNumber.Text = "0";
            }
        }

        if (txtLastChequeNumber.Text.Trim() == "")
        {
            message += "Last Cheque Number is not specified.<br>";
            error = true;
        }
        else
        {
            try
            {
                txtLastChequeNumber.Text = Convert.ToInt32(txtLastChequeNumber.Text.ToString()).ToString();
            }
            catch
            {
                message += "Last Cheque Number must be Numeric.<br>";
                error = true;
                txtLastChequeNumber.Text = "0";
            }
        }

        if (txtChequeBookNumber.Text.Trim() == "")
        {
            message += "Cheque Book Number is not specified.<br>";
            error = true;
        }
        else
        {
            try
            {
                txtChequeBookNumber.Text = Convert.ToInt32(txtChequeBookNumber.Text.ToString()).ToString();
            }
            catch
            {
                message += "Cheque Book Number must be Numeric.<br>";
                error = true;
                txtChequeBookNumber.Text = "0";
            }
        }
        if (txtIssueDate.Text.Trim() == "")
        {
            message += "Branch is not specified.<br>";
            error = true;
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

    private void PopulateChequeBook()
    {
        PCSN.InvoiceSystem.BusinessLogicLayer.ChequeBooksInfo ChequeBook = new PCSN.InvoiceSystem.BusinessLogicLayer.ChequeBooksInfo();
        dtChequeBookDG = ChequeBook.GetAllChequeBooksInfo();
        dgChequeBooks.DataSource = dtChequeBookDG;
        dgChequeBooks.DataBind();
    }

    private void PopulateChequeBooks()
    {
        PCSN.InvoiceSystem.BusinessLogicLayer.BankAccountInfo ChequeBook = new PCSN.InvoiceSystem.BusinessLogicLayer.BankAccountInfo();
        dtChequeBookDG = ChequeBook.GetBankAccountInfoForDropDown();
        cboBank.DataSource = dtChequeBookDG;
        cboBank.DataTextField = "Name";
        cboBank.DataValueField = "ID";

        cboBank.DataBind();
    }

    public string EditItem(string ChequeBookID)
    {
        return "ChequeBooksInfo.aspx?ChequeBookIDED=" + ChequeBookID;
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("default.aspx");
    }
    public string DeleteItem(string ChequeBookID)
    {
        return ChequeBookID;
    }
    public void ClearControls()
    {
        txtChequeBookID.Text = "";
        txtChequeBookNumber.Text = "";
        txtChequeNumberPrefix.Text = "";
        txtEndDate.Text = "";
        txtFirstChequeNumber.Text = "";
        txtIssueDate.Text = "";
        txtLastChequeNumber.Text = "";
        cboBank.SelectedValue = "0";
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
        this.dgChequeBooks.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgChequeBooks_PageIndexChanged);
        this.dgChequeBooks.ItemCreated += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgChequeBooks_ItemCreated);
        this.dgChequeBooks.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgChequeBooks_ItemCommand);

    }
    #endregion

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
                txtChequeBookNumber.Text = "1";
            }
            else
            {
                PCSN.InvoiceSystem.BusinessLogicLayer.ChequeBooksInfo ChequeBook = new PCSN.InvoiceSystem.BusinessLogicLayer.ChequeBooksInfo();
                dtChequeBookDG = ChequeBook.GetAllChequeBooksInfoMAX(Convert.ToInt32(cboBank.SelectedValue.ToString()), AccNumber.ToString());
                if (dtChequeBookDG.Rows.Count > 0)
                {
                    txtChequeBookNumber.Text = Convert.ToString(Convert.ToInt32(dtChequeBookDG.Rows[0]["MAXNUMBER"].ToString()) + 1);
                }
                else
                {
                    txtChequeBookNumber.Text = "1";
                }

            }
        }
    }
}
