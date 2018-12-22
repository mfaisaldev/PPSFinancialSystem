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

public partial class ManageAccPayable : System.Web.UI.Page
{
    #region Variables
    //private long LoanID = 0;
    DataTable dtLoanDG = new DataTable();
    #endregion

    # region Event Handler
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.QueryString["LoanIDAccPay"] != null && Request.QueryString["LoanIDAccPay"].ToString() != "" && !Page.IsPostBack)
        {
            PCSN.InvoiceSystem.BusinessLogicLayer.Loan Loan = new PCSN.InvoiceSystem.BusinessLogicLayer.Loan();
            DataTable dtLoanDet = new DataTable();
            dtLoanDet = Loan.GetAllAccPayableByLoanID(Convert.ToInt32(Request.QueryString["LoanIDAccPay"].ToString()));

            if (dtLoanDet.Rows.Count > 0)
            {
                //txtAccPayID.Text = dtLoanDet.Rows[0]["ID"].ToString();
                txtLoanID.Text = dtLoanDet.Rows[0]["LoanID"].ToString();
                txtTotalRec.Text = dtLoanDet.Rows[0]["AmountPay"].ToString();
                txtAmountCol.Text = "0";
                for (int i = 0; i < dtLoanDet.Rows.Count; i++)
                {
                    txtAmountCol.Text = Convert.ToString(Convert.ToInt32(txtAmountCol.Text.ToString()) + Convert.ToInt32(dtLoanDet.Rows[i]["Amount"].ToString()));
                }
                //txtAccPayDate.Text = dtLoanDet.Rows[0]["AccPayDate"].ToString();                
                //txtAmount.Text = dtLoanDet.Rows[0]["Amount"].ToString();
                //txtDescription.Text = dtLoanDet.Rows[0]["Description"].ToString();                
            }
            if (Convert.ToInt32(txtAmountCol.Text) == Convert.ToInt32(txtTotalRec.Text))
            {
                lblErrorMessage.Text = "This Loan is Cleared.";
            }
            PopulateAccPay(Convert.ToInt32(txtLoanID.Text));
        }
        if (Request.QueryString["AccPayIDED"] != null && Request.QueryString["AccPayIDED"].ToString() != "" && !Page.IsPostBack)
        {
            PCSN.InvoiceSystem.BusinessLogicLayer.Loan Loan = new PCSN.InvoiceSystem.BusinessLogicLayer.Loan();
            DataTable dtLoanDet = new DataTable();
            dtLoanDet = Loan.GetAccPayableByID(Convert.ToInt32(Request.QueryString["AccPayIDED"].ToString()));

            if (dtLoanDet.Rows.Count > 0)
            {
                txtAccPayID.Text = dtLoanDet.Rows[0]["ID"].ToString();
                txtLoanID.Text = dtLoanDet.Rows[0]["LoanID"].ToString();
                txtAccPayDate.Text = dtLoanDet.Rows[0]["AccPayDate"].ToString();
                txtAmount.Text = dtLoanDet.Rows[0]["Amount"].ToString();
                txtPrvAmount.Text = dtLoanDet.Rows[0]["Amount"].ToString();
                txtDescription.Text = dtLoanDet.Rows[0]["Description"].ToString();
            }

            dtLoanDet = Loan.GetAllAccPayableByLoanID(Convert.ToInt32(txtLoanID.Text.ToString()));

            if (dtLoanDet.Rows.Count > 0)
            {
                //txtLoanID.Text = dtLoanDet.Rows[0]["LoanID"].ToString();
                txtTotalRec.Text = dtLoanDet.Rows[0]["AmountPay"].ToString();
                txtAmountCol.Text = "0";
                for (int i = 0; i < dtLoanDet.Rows.Count; i++)
                {
                    txtAmountCol.Text = Convert.ToString(Convert.ToInt32(txtAmountCol.Text.ToString()) + Convert.ToInt32(dtLoanDet.Rows[i]["Amount"].ToString()));
                }
                //txtAccPayDate.Text = dtLoanDet.Rows[0]["AccPayDate"].ToString();                
                //txtAmount.Text = dtLoanDet.Rows[0]["Amount"].ToString();
                //txtDescription.Text = dtLoanDet.Rows[0]["Description"].ToString();                
            }
            PopulateAccPay(Convert.ToInt32(txtLoanID.Text));
        }
        if (!Page.IsPostBack)
        {

        }
    }
    private void dgAccPayable_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        if (e.CommandName == "DeleteLoan")
        {
            string argsID = e.CommandArgument.ToString();

            long AccPayID = Convert.ToInt32(argsID.ToString());

            if (AccPayID > 0)
            {
                PCSN.InvoiceSystem.BusinessLogicLayer.Loan Loan = new PCSN.InvoiceSystem.BusinessLogicLayer.Loan();
                Loan.DeleteAccPayable(AccPayID);
                lblErrorMessage.Text = "Payable Account Deleted Successfuly.";

                //==========================================
                // Checking if the Loan is Payable or Not?
                DataTable dtLoanDet = new DataTable();
                dtLoanDet = Loan.GetAllAccPayableByLoanID(Convert.ToInt32(txtLoanID.Text));
                long TotalAmount = 0;
                long PrePlusCurrAmount = 0;
                long TotRec = 0;
                if (dtLoanDet.Rows.Count > 0)
                {
                    //txtAccRecID.Text = dtLoanDet.Rows[0]["ID"].ToString();
                    //txtLoanID.Text = dtLoanDet.Rows[0]["LoanID"].ToString();
                    TotRec = Convert.ToInt32(dtLoanDet.Rows[0]["AmountPay"].ToString());
                    //txtAmountCol.Text = "0";
                    for (int i = 0; i < dtLoanDet.Rows.Count; i++)
                    {
                        TotalAmount = TotalAmount + Convert.ToInt32(dtLoanDet.Rows[i]["Amount"].ToString());
                    }
                    //txtAccRecDate.Text = dtLoanDet.Rows[0]["AccRecDate"].ToString();                
                    //txtAmount.Text = dtLoanDet.Rows[0]["Amount"].ToString();
                    //txtDescription.Text = dtLoanDet.Rows[0]["Description"].ToString();                
                }
                PrePlusCurrAmount = TotalAmount;
                //TotRec = Convert.ToInt32(txtTotalRec.Text.ToString());
                if (TotRec != PrePlusCurrAmount)
                {
                    Loan.UpdateLoanIsCleared(Convert.ToInt32(txtLoanID.Text), Convert.ToInt16("0"));
                }
                //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-


                PopulateAccPay(Convert.ToInt32(txtLoanID.Text.ToString()));
                
                ClearControls();
            }
        }
    }

    private void dgAccPayable_ItemCreated(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        LinkButton link = (LinkButton)e.Item.FindControl("PLinkbutton2");
        if (link != null)
            link.Attributes.Add("onClick", "javascript:return confirm('This action will delete information saved related to this Account Payable entry.  Are you sure you want to delete this entry ?');");

    }
    private void dgAccPayable_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dgAccPayable.CurrentPageIndex = e.NewPageIndex;
        PopulateAccPay(Convert.ToInt32(txtLoanID.Text.ToString()));
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {        
        PopulateAccPay(Convert.ToInt32(txtLoanID.Text));
        ClearControls();
    }
    protected void btnViewReport_Click(object sender, EventArgs e)
    {
        Response.Redirect("ViewLoan.aspx?LoanID=" + txtLoanID.Text.ToString());
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ValidateFields())
        {

            if (txtAccPayID.Text == "")
            {
                long PrePlusCurrAmount = Convert.ToInt32(txtAmount.Text.ToString()) + Convert.ToInt32(txtAmountCol.Text.ToString());
                long TotPay = Convert.ToInt32(txtTotalRec.Text.ToString());
                if (TotPay < PrePlusCurrAmount)
                {
                    lblErrorMessage.Text = "Paid amount must be equal or less to the Payable Amount.<br>";
                    return;
                }

                PCSN.InvoiceSystem.BusinessLogicLayer.Loan Loan = new PCSN.InvoiceSystem.BusinessLogicLayer.Loan();
                Loan.InsertAccPayable(Convert.ToInt32(txtLoanID.Text.ToString()), Convert.ToInt32(txtAmount.Text.ToString()), txtAccPayDate.Text.ToString(), txtDescription.Text.ToString());
                if (TotPay == PrePlusCurrAmount)
                {
                    Loan.UpdateLoanIsCleared(Convert.ToInt32(txtLoanID.Text),Convert.ToInt16("1"));
                }
                lblErrorMessage.Text = "Accounts Payable have been saved.";
                PopulateAccPay(Convert.ToInt32(txtLoanID.Text.ToString()));
                ClearControls();
                txtAmountCol.Text = PrePlusCurrAmount.ToString();

            }
            else
            {
                long PrePlusCurrAmount = Convert.ToInt32(txtAmount.Text.ToString()) + Convert.ToInt32(txtAmountCol.Text.ToString()) - Convert.ToInt32(txtPrvAmount.Text.ToString());
                long TotPay = Convert.ToInt32(txtTotalRec.Text.ToString());
                if (TotPay < PrePlusCurrAmount)
                {
                    lblErrorMessage.Text = "Paid amount must be equal or less to the Payable Amount.<br>";
                    return;
                }

                PCSN.InvoiceSystem.BusinessLogicLayer.Loan Loan = new PCSN.InvoiceSystem.BusinessLogicLayer.Loan();
                Loan.UpdateAccPayable(Convert.ToInt32(txtAccPayID.Text), Convert.ToInt32(txtLoanID.Text.ToString()), Convert.ToInt32(txtAmount.Text.ToString()), txtAccPayDate.Text.ToString(), txtDescription.Text.ToString());
                if (TotPay == PrePlusCurrAmount)
                {
                    Loan.UpdateLoanIsCleared(Convert.ToInt32(txtLoanID.Text), Convert.ToInt16("1"));
                }
                lblErrorMessage.Text = "Accounts Payable have been updated.";
                PopulateAccPay(Convert.ToInt32(txtLoanID.Text.ToString()));
                ClearControls();
                txtAmountCol.Text = PrePlusCurrAmount.ToString();
            }

        }
    }


    #endregion

    #region Methods

    private bool ValidateFields()
    {

        bool error = false;
        string message = "";

        if (txtAccPayDate.Text.Trim() == "")
        {
            message += "Accounts Payable Date is not specified.<br>";
            error = true;
        }
        else
        {
            try
            {
                txtAccPayDate.Text = Convert.ToDateTime(txtAccPayDate.Text.ToString()).ToShortDateString();
            }
            catch
            {
                txtAccPayDate.Text = "";
                message += "Accounts Payable is not a Valid Date.<br>";
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
        

        lblErrorMessage.Text = "";
        if (error)
            lblErrorMessage.Text = message;

        return !error;
    }
        
    private void PopulateAccPay(long LoanID)
    {
        PCSN.InvoiceSystem.BusinessLogicLayer.Loan Loan = new PCSN.InvoiceSystem.BusinessLogicLayer.Loan();
        dtLoanDG = Loan.GetAllAccPayableByLoanID(LoanID);
        dgAccPayable.DataSource = dtLoanDG;
        dgAccPayable.DataBind();
    }            

    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("ManageLoan.aspx");
    }

    public string EditAccPay(string AccPayIDED)
    {
        return "ManageAccPayable.aspx?AccPayIDED=" + AccPayIDED;
    }
    public string DeleteAccPay(string AccPayIDED)
    {
        return AccPayIDED;
    }

    public void ClearControls()
    {
        //txtLoanID.Text = "";
        txtAccPayDate.Text = "";
        txtAmount.Text = "";
        txtDescription.Text = "";
        txtAccPayID.Text = "";
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
        this.dgAccPayable.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgAccPayable_PageIndexChanged);
        this.dgAccPayable.ItemCreated += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgAccPayable_ItemCreated);
        this.dgAccPayable.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgAccPayable_ItemCommand);

    }
    #endregion



}
