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

public partial class ChequeReceived : System.Web.UI.Page
{
    #region Variables
    //private long ChequeReceivedID = 0;
    DataTable dtChequeReceivedDG = new DataTable();
    #endregion

    # region Event Handler
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.QueryString["ChequeReceivedIDED"] != null && Request.QueryString["ChequeReceivedIDED"].ToString() != "" && !Page.IsPostBack)
        {
            if (Request.QueryString["MkCl"] != null && Request.QueryString["MkCl"].ToString() != "")
            {
                PCSN.InvoiceSystem.BusinessLogicLayer.ChequeReceived ChequeReceived = new PCSN.InvoiceSystem.BusinessLogicLayer.ChequeReceived();
                ChequeReceived.ChequeCleared(Convert.ToInt32(Request.QueryString["ChequeReceivedIDED"].ToString()));    
            }
            else
            {                
                PCSN.InvoiceSystem.BusinessLogicLayer.ChequeReceived ChequeReceived = new PCSN.InvoiceSystem.BusinessLogicLayer.ChequeReceived();
                DataTable dtChequeReceivedDet = new DataTable();
                dtChequeReceivedDet = ChequeReceived.GetChequeReceivedByID(Convert.ToInt32(Request.QueryString["ChequeReceivedIDED"].ToString()));

                if (dtChequeReceivedDet.Rows.Count > 0)
                {
                    txtChequeReceivedID.Text = dtChequeReceivedDet.Rows[0]["ID"].ToString();
                    txtChequeNumber.Text = dtChequeReceivedDet.Rows[0]["ChequeNumber"].ToString();
                    txtReceivedDate.Text = dtChequeReceivedDet.Rows[0]["RecDate"].ToString();
                    txtAmount.Text = dtChequeReceivedDet.Rows[0]["Amount"].ToString();
                    txtDescription.Text = dtChequeReceivedDet.Rows[0]["Description"].ToString();
                    txtRecBy.Text = dtChequeReceivedDet.Rows[0]["RecBy"].ToString();
                    PopulateClient();
                    cboClient.SelectedValue = dtChequeReceivedDet.Rows[0]["ClientID"].ToString();
                    //chkIsCleared.Checked = Convert.ToBoolean(dtChequeReceivedDet.Rows[0]["IsCleared"].ToString());

                }
            }

        }
        if (!Page.IsPostBack)
        {            
            PopulateClient();
            PopulateChequeReceived();
        }
    }
    private void dgChequeReceiveds_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        if (e.CommandName == "DeleteChequeReceived")
        {
            string argsID = e.CommandArgument.ToString();

            long ChequeReceivedID = Convert.ToInt32(argsID.ToString());

            if (ChequeReceivedID > 0)
            {
                PCSN.InvoiceSystem.BusinessLogicLayer.ChequeReceived ChequeReceived = new PCSN.InvoiceSystem.BusinessLogicLayer.ChequeReceived();
                ChequeReceived.DeleteChequeReceived(ChequeReceivedID);
                lblErrorMessage.Text = "Cheque Deleted Successfuly.";

                PopulateClient();
                PopulateChequeReceived();               
                ClearControls();
            }
        }
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        PopulateChequeReceived();        
        PopulateClient();
        ClearControls();
    }
    private void dgChequeReceiveds_ItemCreated(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        LinkButton link = (LinkButton)e.Item.FindControl("Linkbutton2");
        if (link != null)
        {
            link.Attributes.Add("onClick", "javascript:return confirm('This action will delete the information saved for this Received Cheque.  Are you sure you want to delete this Cheque?');");
        }

        HyperLink link2 = (HyperLink)e.Item.FindControl("Linkbutton3");
        if (link2 != null)
        {
            link2.Attributes.Add("onClick", "javascript:return confirm('This action will mark this cheque as Cleared.  Are you sure you want to Clear this Cheque?');");
            if (link2.NavigateUrl == "Already Cleared")
            {
                link2.BackColor = System.Drawing.Color.Red;
            }
        }


    }
    private void dgChequeReceiveds_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dgChequeReceiveds.CurrentPageIndex = e.NewPageIndex;
        PopulateChequeReceived();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ValidateFields())
        {

            if (txtChequeReceivedID.Text == "")
            {
                PCSN.InvoiceSystem.BusinessLogicLayer.ChequeReceived ChequeReceived = new PCSN.InvoiceSystem.BusinessLogicLayer.ChequeReceived();
                ChequeReceived.InsertChequeReceived(Convert.ToInt32(cboClient.SelectedValue), txtReceivedDate.Text.ToString(), txtChequeNumber.Text.ToString(), Convert.ToInt32(txtAmount.Text.ToString()), txtRecBy.Text.ToString(), txtDescription.Text.ToString());
                lblErrorMessage.Text = "Received Cheque Have been saved.";

                PopulateClient();
                PopulateChequeReceived();
                ClearControls();

            }
            else
            {
                PCSN.InvoiceSystem.BusinessLogicLayer.ChequeReceived ChequeReceived = new PCSN.InvoiceSystem.BusinessLogicLayer.ChequeReceived();
                ChequeReceived.UpdateChequeReceived(Convert.ToInt32(txtChequeReceivedID.Text), Convert.ToInt32(cboClient.SelectedValue), txtReceivedDate.Text.ToString(), txtChequeNumber.Text.ToString(), Convert.ToInt32(txtAmount.Text.ToString()), txtRecBy.Text.ToString(), txtDescription.Text.ToString());
                lblErrorMessage.Text = "Received Cheque have been updated.";

                PopulateClient();
                PopulateChequeReceived();
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

        if (txtReceivedDate.Text.Trim() == "")
        {
            message += "Received Date is not specified.<br>";
            error = true;
        }
        else
        {
            try
            {
                txtReceivedDate.Text = Convert.ToDateTime(txtReceivedDate.Text.ToString()).ToShortDateString();
            }
            catch
            {
                txtReceivedDate.Text = "";
                message += "Received Date is not a valid date.<br>";
                error = true;
            }
        }
        
        if (cboClient.SelectedValue == "0")
        {
            message += "Client is not specified.<br>";
            error = true;
        }

        lblErrorMessage.Text = "";
        if (error)
            lblErrorMessage.Text = message;

        return !error;
    }

    private void PopulateChequeReceived()
    {
        PCSN.InvoiceSystem.BusinessLogicLayer.ChequeReceived ChequeReceived = new PCSN.InvoiceSystem.BusinessLogicLayer.ChequeReceived();
        dtChequeReceivedDG = ChequeReceived.GetAllChequeReceived();
        dgChequeReceiveds.DataSource = dtChequeReceivedDG;
        dgChequeReceiveds.DataBind();
    }

    private void PopulateClient()
    {
        PCSN.InvoiceSystem.BusinessLogicLayer.Client client = new PCSN.InvoiceSystem.BusinessLogicLayer.Client();
        dtChequeReceivedDG = client.GetClientForDropDown("ForEsp");
        cboClient.DataSource = dtChequeReceivedDG;
        cboClient.DataTextField = "Name";
        cboClient.DataValueField = "ID";

        cboClient.DataBind();
    }



    public string EditChequeReceived(string ChequeReceivedID)
    {
        return "ChequeReceived.aspx?ChequeReceivedIDED=" + ChequeReceivedID;
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("default.aspx");
    }
    public string DeleteChequeReceived(string ChequeReceivedID)
    {
        return ChequeReceivedID;
    }
    public string IsCleared(string IsCleared)
    {
        if (IsCleared == "True")
        {
            return "YES";
        }
        else
        {
            return "NO";
        }
    }
    public string ChequeCleared(string ChequeReceivedID, string IsCleared)
    {
        if (IsCleared == "True")
        {
            return "#";
        }
        else
        {
            return "ChequeReceived.aspx?ChequeReceivedIDED=" + ChequeReceivedID + "&MkCl=True";
        }
    }
    
    public void ClearControls()
    {
        txtChequeReceivedID.Text = "";
        txtChequeNumber.Text = "";
        txtAmount.Text = "";
        txtDescription.Text = "";
        txtReceivedDate.Text = "";
        txtRecBy.Text = "";
        cboClient.SelectedValue = "0";
        //chkIsCleared.Checked = false;

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
        this.dgChequeReceiveds.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgChequeReceiveds_PageIndexChanged);
        this.dgChequeReceiveds.ItemCreated += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgChequeReceiveds_ItemCreated);
        this.dgChequeReceiveds.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgChequeReceiveds_ItemCommand);

    }
    #endregion
    
}
