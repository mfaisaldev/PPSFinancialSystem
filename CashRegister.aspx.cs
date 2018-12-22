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

public partial class CashRegister : System.Web.UI.Page
{
    #region Variables
    //private long CashInhandID = 0;
    DataTable dtCashInhandDG = new DataTable();
    #endregion

    # region Event Handler
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["errmsg"] != null && Request.QueryString["errmsg"].ToString() != "")
        {
            lblErrorMessage.Text = Request.QueryString["errmsg"].ToString();
        }

        if (Request.QueryString["CashInhandIDED"] != null && Request.QueryString["CashInhandIDED"].ToString() != "" && Request.QueryString["ChIssueID"] != null && Request.QueryString["ChIssueID"].ToString() != "" && !Page.IsPostBack)
        {
            PCSN.InvoiceSystem.BusinessLogicLayer.CashInHand CashInhand = new PCSN.InvoiceSystem.BusinessLogicLayer.CashInHand();
            DataTable dtCashInhandDet = new DataTable();
            dtCashInhandDet = CashInhand.GetAllCashInHandByChequeIssueID(Convert.ToInt32(Request.QueryString["ChIssueID"].ToString()));
            if (dtCashInhandDet.Rows.Count < 0)
            {
                dtCashInhandDet = CashInhand.GetCashInHandByID(Convert.ToInt32(Request.QueryString["CashInhandIDED"].ToString()));

                if (dtCashInhandDet.Rows.Count > 0)
                {
                    txtCashInHandID.Text = dtCashInhandDet.Rows[0]["ID"].ToString();
                    txtCashIN.Text = dtCashInhandDet.Rows[0]["CashIN"].ToString();
                    txtCashBF.Text = dtCashInhandDet.Rows[0]["CashBF"].ToString();
                    txtDateIN.Text = dtCashInhandDet.Rows[0]["DateIN"].ToString();
                    txtCashAvailable.Text = dtCashInhandDet.Rows[0]["CashAvailable"].ToString();
                    txtDescription.Text = dtCashInhandDet.Rows[0]["Description"].ToString();
                    PopulateChequeIssue();
                    cboChequeIssue.SelectedValue = dtCashInhandDet.Rows[0]["ChequeIssueID"].ToString();

                }
            }
            else
            {
                lblErrorMessage.Text = "This Cheque has already been used for Cash. Please select another Cheque for cash.";
            }

        }
        if (!Page.IsPostBack)
        {
            PopulateCashInhand();
            PopulateChequeIssue();
            ClearControls();
            txtDateIN.Text = DateTime.Now.ToShortDateString();
        }
    }

    private void dgCashInhands_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dgCashInHands.CurrentPageIndex = e.NewPageIndex;
        PopulateCashInhand();
    }

    private void dgCashInhands_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        if (e.CommandName == "DeleteCashInHand")
        {
            string argsID = e.CommandArgument.ToString();

            long CashInhandID = Convert.ToInt32(argsID.ToString());

            if (CashInhandID > 0)
            {
                PCSN.InvoiceSystem.BusinessLogicLayer.CashInHand CashInhand = new PCSN.InvoiceSystem.BusinessLogicLayer.CashInHand();
                CashInhand.DeleteCashInHand(CashInhandID);
                lblErrorMessage.Text = "CashInhand Deleted Successfuly.";
                PopulateCashInhand();
                ClearControls();
            }
        }
    }

    private void dgCashInhands_ItemCreated(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        LinkButton link = (LinkButton)e.Item.FindControl("Linkbutton2");
        if (link != null)
            link.Attributes.Add("onClick", "javascript:return confirm('This action will delete the information saved for this CashInhand.  Are you sure you want to delete this CashInhand?');");

    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        PopulateCashInhand();
        PopulateChequeIssue();        
        ClearControls();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ValidateFields())
        {

            if (txtCashInHandID.Text == "")
            {
                PCSN.InvoiceSystem.BusinessLogicLayer.CashInHand CashInhand = new PCSN.InvoiceSystem.BusinessLogicLayer.CashInHand();

                               
                long CashInHandID = CashInhand.InsertCashInHand(Convert.ToInt32(cboChequeIssue.SelectedValue), Convert.ToInt32(txtCashIN.Text.ToString()), Convert.ToInt32(txtCashBF.Text.ToString()),txtDateIN.Text.ToString(), Convert.ToInt32(txtCashAvailable.Text.ToString()), txtDescription.Text.ToString());
                // Updating Cash in Hand of Today
                long CashStart = 0;
                //long TotalExp = 0;
                int counter = 0;
                DataTable dtCashinHand = new DataTable();
                dtCashinHand = CashInhand.GetAllCashInHandByDateIN(DateTime.Now.ToShortDateString());
                if (dtCashinHand.Rows.Count > 0)
                {
                    for (int i = 0; i < dtCashinHand.Rows.Count; i++)
                    {
                        CashStart = CashStart + Convert.ToInt32(dtCashinHand.Rows[i]["CashIN"].ToString());
                        //TotalExp = TotalExp + Convert.ToInt32(dtCashinHand.Rows[i]["TotalAmount"].ToString());
                        counter++;
                    }

                }
                if (CashStart > 0 && counter >= 1)
                {
                    PCSN.InvoiceSystem.BusinessLogicLayer.DailyExpMaster dtExp = new PCSN.InvoiceSystem.BusinessLogicLayer.DailyExpMaster();
                    DataTable dtDailyExpMas = new DataTable();
                    dtDailyExpMas = dtExp.GetDailyExpMasterByExpDate(DateTime.Now.ToShortDateString());
                    if (dtDailyExpMas.Rows.Count > 0)
                    {
                        long DaExMaID = 0;
                        DaExMaID = Convert.ToInt32(dtDailyExpMas.Rows[0]["ID"].ToString());
                        if (DaExMaID > 0)
                        {
                            dtExp.UpdateDailyExpMasterStartCash(DaExMaID, CashInHandID, CashStart);
                        }
                    }
                }
                //========================================

                lblErrorMessage.Text = "CashInhand Account Have been saved.";
                PopulateCashInhand();
                ClearControls();

            }
            else
            {
                PCSN.InvoiceSystem.BusinessLogicLayer.CashInHand CashInhand = new PCSN.InvoiceSystem.BusinessLogicLayer.CashInHand();
                CashInhand.UpdateCashInHand(Convert.ToInt32(txtCashInHandID.Text), Convert.ToInt32(cboChequeIssue.SelectedValue), Convert.ToInt32(txtCashIN.Text.ToString()), Convert.ToInt32(txtCashBF.Text.ToString()), txtDateIN.Text.ToString(), Convert.ToInt32(txtCashAvailable.Text.ToString()), txtDescription.Text.ToString());
                lblErrorMessage.Text = "CashInhand Account have been updated.";
                PopulateCashInhand();
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

        if (txtCashIN.Text.Trim() == "")
        {
            message += "Cash In is not specified.<br>";
            error = true;
        }


        if (txtCashBF.Text.Trim() == "")
        {
            message += "Cash Out Number is not specified.<br>";
            error = true;
        }
        else
        {
            try
            {
                txtCashBF.Text = Convert.ToInt32(txtCashBF.Text.ToString()).ToString();
            }
            catch
            {
                message += "Cash Out Number must be Numeric.<br>";
                error = true;
                txtCashBF.Text = "0";
            }
        }

        if (txtCashAvailable.Text.Trim() == "")
        {
            message += "Cash Available is not specified.<br>";
            error = true;
        }
        else
        {
            try
            {
                txtCashAvailable.Text = Convert.ToInt32(txtCashAvailable.Text.ToString()).ToString();
            }
            catch
            {
                message += "Cash Available must be Numeric.<br>";
                error = true;
                txtCashAvailable.Text = "0";
            }
        }

        if (txtDateIN.Text.Trim() == "")
        {
            message += "Date is not specified.<br>";
            error = true;
        }
        else
        {
            try
            {
                txtDateIN.Text = Convert.ToDateTime(txtDateIN.Text.ToString()).ToShortDateString();
            }
            catch
            {
                message += "Date must be Numeric.<br>";
                error = true;
                txtDateIN.Text = "0";
            }
        }        
        if (cboChequeIssue.SelectedValue == "0")
        {
            message += "Cheque is not specified.<br>";
            error = true;
        }
        PCSN.InvoiceSystem.BusinessLogicLayer.CashInHand CashInhand = new PCSN.InvoiceSystem.BusinessLogicLayer.CashInHand();
        DataTable dtCashInhandDet = new DataTable();
        dtCashInhandDet = CashInhand.GetAllCashInHandByChequeIssueID(Convert.ToInt32(cboChequeIssue.SelectedValue.ToString()));
        if(dtCashInhandDet.Rows.Count>0)
        {
            message += "This Cheque has already been used for Cash. Please select another Cheque for cash.<br>";
            error = true;
        }

        lblErrorMessage.Text = "";
        if (error)
            lblErrorMessage.Text = message;

        return !error;
    }

    private void PopulateCashInhand()
    {
        PCSN.InvoiceSystem.BusinessLogicLayer.CashInHand CashInhand = new PCSN.InvoiceSystem.BusinessLogicLayer.CashInHand();
        dtCashInhandDG = CashInhand.GetAllCashInHand();
        dgCashInHands.DataSource = dtCashInhandDG;
        dgCashInHands.DataBind();
    }

    private void PopulateChequeIssue()
    {
        PCSN.InvoiceSystem.BusinessLogicLayer.ChequeIssue ChequeIssue = new PCSN.InvoiceSystem.BusinessLogicLayer.ChequeIssue();
        dtCashInhandDG = ChequeIssue.GetChequeIssueForDropDownForCashReg();
        cboChequeIssue.DataSource = dtCashInhandDG;
        cboChequeIssue.DataTextField = "ChequeNumber";
        cboChequeIssue.DataValueField = "ID";

        cboChequeIssue.DataBind();
    }

    public string EditItem(string CashInhandID, string ChequeIssueID)
    {
        return "CashRegister.aspx?CashInhandIDED=" + CashInhandID + "&ChIssueID=" + ChequeIssueID;
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("default.aspx");
    }
    public string DeleteItem(string CashInhandID)
    {
        return CashInhandID;
    }
    public void ClearControls()
    {
        txtCashInHandID.Text = "";
        txtCashIN.Text = "";
        txtCashBF.Text = "";
        txtDateIN.Text = "";
        txtCashAvailable.Text = "";
        txtDescription.Text = "";       
        cboChequeIssue.SelectedValue = "0";
        txtDateIN.Text = DateTime.Now.ToShortDateString();
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
        this.dgCashInHands.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgCashInhands_PageIndexChanged);
        this.dgCashInHands.ItemCreated += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgCashInhands_ItemCreated);
        this.dgCashInHands.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgCashInhands_ItemCommand);

    }
    #endregion


    protected void cboChequeIssue_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cboChequeIssue.SelectedValue != "0")
        {
            PCSN.InvoiceSystem.BusinessLogicLayer.ChequeIssue ChequeIssue = new PCSN.InvoiceSystem.BusinessLogicLayer.ChequeIssue();
            DataTable dtChequeI = new DataTable();
            dtChequeI = ChequeIssue.GetChequeIssueByID(Convert.ToInt32(cboChequeIssue.SelectedValue.ToString()));
            if (dtChequeI.Rows.Count > 0)
            {
                txtCashIN.Text = dtChequeI.Rows[0]["Amount"].ToString();
                PCSN.InvoiceSystem.BusinessLogicLayer.CashInHand CashIn = new PCSN.InvoiceSystem.BusinessLogicLayer.CashInHand();
                DataTable dtCash = new DataTable();
                dtCash = CashIn.GetAllCashInHandByMAXID();
                if (dtCash.Rows.Count > 0)
                {
                    txtCashBF.Text = dtCash.Rows[0]["CashAvailable"].ToString();
                    txtCashAvailable.Text = Convert.ToString(Convert.ToInt32(txtCashIN.Text) + Convert.ToInt32(txtCashBF.Text));
                }
                else
                {
                    txtCashBF.Text = "0";
                    txtCashAvailable.Text = txtCashIN.Text;
                }

            }
            else
            {
                Response.Redirect("ChequeIssue.aspx");
            }
        }
        else
        {
            ClearControls();
        }
    }
}
