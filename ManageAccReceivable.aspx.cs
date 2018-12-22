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

public partial class ManageAccReceivable : System.Web.UI.Page
{
    #region Variables
    //private long LoanID = 0;
    DataTable dtLoanDG = new DataTable();
    #endregion

    # region Event Handler
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.QueryString["LoanIDAccRec"] != null && Request.QueryString["LoanIDAccRec"].ToString() != "" && !Page.IsPostBack)
        {
            PCSN.InvoiceSystem.BusinessLogicLayer.Loan Loan = new PCSN.InvoiceSystem.BusinessLogicLayer.Loan();
            DataTable dtLoanDet = new DataTable();
            dtLoanDet = Loan.GetAllAccReceivableByLoanID(Convert.ToInt32(Request.QueryString["LoanIDAccRec"].ToString()));

            if (dtLoanDet.Rows.Count > 0)
            {
                //txtAccRecID.Text = dtLoanDet.Rows[0]["ID"].ToString();
                txtLoanID.Text = dtLoanDet.Rows[0]["LoanID"].ToString();
                txtTotalRec.Text = dtLoanDet.Rows[0]["AmountRec"].ToString();
                txtAmountCol.Text = "0";
                for(int i=0 ; i<dtLoanDet.Rows.Count ; i++)
                {
                    txtAmountCol.Text = Convert.ToString(Convert.ToInt32(txtAmountCol.Text.ToString()) + Convert.ToInt32(dtLoanDet.Rows[i]["Amount"].ToString()));
                }
                //txtAccRecDate.Text = dtLoanDet.Rows[0]["AccRecDate"].ToString();                
                //txtAmount.Text = dtLoanDet.Rows[0]["Amount"].ToString();
                //txtDescription.Text = dtLoanDet.Rows[0]["Description"].ToString();                
            }
            PopulateAccRec(Convert.ToInt32(txtLoanID.Text));
        }
        if (Request.QueryString["AccRecIDED"] != null && Request.QueryString["AccRecIDED"].ToString() != "" && !Page.IsPostBack)
        {
            PCSN.InvoiceSystem.BusinessLogicLayer.Loan Loan = new PCSN.InvoiceSystem.BusinessLogicLayer.Loan();
            DataTable dtLoanDet = new DataTable();
            dtLoanDet = Loan.GetAccReceivableByID(Convert.ToInt32(Request.QueryString["AccRecIDED"].ToString()));

            if (dtLoanDet.Rows.Count > 0)
            {
                txtAccRecID.Text = dtLoanDet.Rows[0]["ID"].ToString();
                txtLoanID.Text = dtLoanDet.Rows[0]["LoanID"].ToString();
                txtAccRecDate.Text = dtLoanDet.Rows[0]["AccRecDate"].ToString();
                txtAmount.Text = dtLoanDet.Rows[0]["Amount"].ToString();
                txtPrvAmount.Text = dtLoanDet.Rows[0]["Amount"].ToString();
                txtDescription.Text = dtLoanDet.Rows[0]["Description"].ToString();
            }
            
            dtLoanDet = Loan.GetAllAccReceivableByLoanID(Convert.ToInt32(txtLoanID.Text.ToString()));

            if (dtLoanDet.Rows.Count > 0)
            {
                //txtLoanID.Text = dtLoanDet.Rows[0]["LoanID"].ToString();
                txtTotalRec.Text = dtLoanDet.Rows[0]["AmountRec"].ToString();
                txtAmountCol.Text = "0";
                for (int i = 0; i < dtLoanDet.Rows.Count; i++)
                {
                    txtAmountCol.Text = Convert.ToString(Convert.ToInt32(txtAmountCol.Text.ToString()) + Convert.ToInt32(dtLoanDet.Rows[i]["Amount"].ToString()));
                }
                //txtAccRecDate.Text = dtLoanDet.Rows[0]["AccRecDate"].ToString();                
                //txtAmount.Text = dtLoanDet.Rows[0]["Amount"].ToString();
                //txtDescription.Text = dtLoanDet.Rows[0]["Description"].ToString();                
            }
            PopulateAccRec(Convert.ToInt32(txtLoanID.Text));
        }
        if (!Page.IsPostBack)
        {
            
        }
    }
    private void dgAccReceivable_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        if (e.CommandName == "DeleteLoan")
        {
            string argsID = e.CommandArgument.ToString();

            long AccRecID = Convert.ToInt32(argsID.ToString());

            if (AccRecID > 0)
            {
                PCSN.InvoiceSystem.BusinessLogicLayer.Loan Loan = new PCSN.InvoiceSystem.BusinessLogicLayer.Loan();
                Loan.DeleteAccReceivable(AccRecID);
                lblErrorMessage.Text = "Receivable Account Deleted Successfuly.";
                //==========================================
                // Checking if the Loan is Receivable or Not?
                
                DataTable dtLoanDet = new DataTable();
                dtLoanDet = Loan.GetAllAccReceivableByLoanID(Convert.ToInt32(txtLoanID.Text));
                long TotalAmount = 0;
                long PrePlusCurrAmount = 0 ;
                long TotRec = 0 ;
                if (dtLoanDet.Rows.Count > 0)
                {
                    //txtAccRecID.Text = dtLoanDet.Rows[0]["ID"].ToString();
                    //txtLoanID.Text = dtLoanDet.Rows[0]["LoanID"].ToString();
                    TotRec = Convert.ToInt32(dtLoanDet.Rows[0]["AmountRec"].ToString());
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
                PopulateAccRec(Convert.ToInt32(txtLoanID.Text.ToString()));
                ClearControls();
            }
        }
    }

    private void dgAccReceivable_ItemCreated(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        LinkButton link = (LinkButton)e.Item.FindControl("PLinkbutton2");
        if (link != null)
            link.Attributes.Add("onClick", "javascript:return confirm('This action will delete information saved related to this Account Receivable entry.  Are you sure you want to delete this entry ?');");

    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        PopulateAccRec(Convert.ToInt32(txtLoanID.Text));
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

            if (txtAccRecID.Text == "")
            {
                long PrePlusCurrAmount = Convert.ToInt32(txtAmount.Text.ToString()) + Convert.ToInt32(txtAmountCol.Text.ToString());
                long TotRec = Convert.ToInt32(txtTotalRec.Text.ToString());
                if (TotRec < PrePlusCurrAmount)
                {
                    lblErrorMessage.Text = "Collecting amount must be equal or less to the Receivable Amount.<br>";
                    return;
                }

                PCSN.InvoiceSystem.BusinessLogicLayer.Loan Loan = new PCSN.InvoiceSystem.BusinessLogicLayer.Loan();
                Loan.InsertAccReceivable(Convert.ToInt32(txtLoanID.Text.ToString()), Convert.ToInt32(txtAmount.Text.ToString()), txtAccRecDate.Text.ToString(), txtDescription.Text.ToString());
                if (TotRec == PrePlusCurrAmount)
                {
                    Loan.UpdateLoanIsCleared(Convert.ToInt32(txtLoanID.Text), Convert.ToInt16("1"));
                }
                lblErrorMessage.Text = "Accounts Receivable have been saved.";
                PopulateAccRec(Convert.ToInt32(txtLoanID.Text.ToString()));
                ClearControls();
                txtAmountCol.Text = PrePlusCurrAmount.ToString();

            }
            else
            {
                long PrePlusCurrAmount = Convert.ToInt32(txtAmount.Text.ToString()) + Convert.ToInt32(txtAmountCol.Text.ToString()) - Convert.ToInt32(txtPrvAmount.Text.ToString());
                long TotRec = Convert.ToInt32(txtTotalRec.Text.ToString());
                if (TotRec < PrePlusCurrAmount)
                {
                    lblErrorMessage.Text = "Collecting amount must be equal or less to the Receivable Amount.<br>";
                    return;
                }
                PCSN.InvoiceSystem.BusinessLogicLayer.Loan Loan = new PCSN.InvoiceSystem.BusinessLogicLayer.Loan();
                Loan.UpdateAccReceivable(Convert.ToInt32(txtAccRecID.Text), Convert.ToInt32(txtLoanID.Text.ToString()), Convert.ToInt32(txtAmount.Text.ToString()), txtAccRecDate.Text.ToString(), txtDescription.Text.ToString());
                lblErrorMessage.Text = "Accounts Receivable have been updated.";
                if (TotRec == PrePlusCurrAmount)
                {
                    Loan.UpdateLoanIsCleared(Convert.ToInt32(txtLoanID.Text), Convert.ToInt16("1"));
                }
                PopulateAccRec(Convert.ToInt32(txtLoanID.Text.ToString()));
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
        
        if (txtAccRecDate.Text.Trim() == "")
        {
            message += "Accounts Receivable Date is not specified.<br>";
            error = true;
        }
        else
        {
            try
            {
                txtAccRecDate.Text = Convert.ToDateTime(txtAccRecDate.Text.ToString()).ToShortDateString();
            }
            catch
            {
                txtAccRecDate.Text = "";
                message += "Accounts Receivable is not a Valid Date.<br>";
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

    private void PopulateAccRec(long LoanID)
    {
        PCSN.InvoiceSystem.BusinessLogicLayer.Loan Loan = new PCSN.InvoiceSystem.BusinessLogicLayer.Loan();
        dtLoanDG = Loan.GetAllAccReceivableByLoanID(LoanID);
        dgAccReceivable.DataSource = dtLoanDG;
        dgAccReceivable.DataBind();
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("ManageLoan.aspx");
    }

    public string EditAccRec(string AccRecIDED)
    {
        return "ManageAccReceivable.aspx?AccRecIDED=" + AccRecIDED;
    }
    public string DeleteAccRec(string AccRecIDED)
    {
        return AccRecIDED;
    }

    public void ClearControls()
    {
        //txtLoanID.Text = "";
        txtAccRecDate.Text = "";
        txtAmount.Text = "";
        txtDescription.Text = "";
        txtAccRecID.Text = "";
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
        //this.dgAccReceivable.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgAccReceivable_PageIndexChanged);
        this.dgAccReceivable.ItemCreated += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgAccReceivable_ItemCreated);
        this.dgAccReceivable.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgAccReceivable_ItemCommand);

    }
    #endregion



}
