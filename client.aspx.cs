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

public partial class client : System.Web.UI.Page
{
    #region Variables
    private long ClientID = 0;
    DataTable dtClientDG = new DataTable();
    #endregion

    # region Event Handler
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (Request.QueryString["ClientIDED"] != null && Request.QueryString["ClientIDED"].ToString() != "" && !Page.IsPostBack)
        {
            PCSN.InvoiceSystem.BusinessLogicLayer.Client Client = new PCSN.InvoiceSystem.BusinessLogicLayer.Client();
            DataTable dtClientDet = new DataTable();
            dtClientDet = Client.GetClientByID(Convert.ToInt32(Request.QueryString["ClientIDED"].ToString()));

            if (dtClientDet.Rows.Count > 0)
            {
                txtClientID.Text = dtClientDet.Rows[0]["ID"].ToString();
                txtClientName.Text = dtClientDet.Rows[0]["ClientName"].ToString();
                txtCompanyName.Text = dtClientDet.Rows[0]["CompanyName"].ToString();
                txtCompanyAddress.Text = dtClientDet.Rows[0]["CompanyAddress"].ToString();
                txtMobile.Text = dtClientDet.Rows[0]["Mobile"].ToString();
                txtPhone.Text = dtClientDet.Rows[0]["Phone"].ToString();
                txtEmail.Text = dtClientDet.Rows[0]["Email"].ToString();
                txtDescription.Text = dtClientDet.Rows[0]["Description"].ToString();

            }
            
        }
        if (!Page.IsPostBack)
        {
            PopulateClient();
        }
    }
    private void dgClients_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        if (e.CommandName == "DeleteClient")
        {
            string argsID = e.CommandArgument.ToString();
            
            long ClientID = Convert.ToInt32(argsID.ToString());
            
            if (ClientID > 0)
            {
                PCSN.InvoiceSystem.BusinessLogicLayer.Client Client = new PCSN.InvoiceSystem.BusinessLogicLayer.Client();
                Client.DeleteClient(ClientID);
                lblErrorMessage.Text = "Client Deleted Successfuly.";
                PopulateClient();
                ClearControls();
            }
        }
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        PopulateClient();        
        ClearControls();
    }
    private void dgClients_ItemCreated(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        LinkButton link = (LinkButton)e.Item.FindControl("Linkbutton2");
        if (link != null)
            link.Attributes.Add("onClick", "javascript:return confirm('This action will delete all the information saved related to this Client.  Are you sure you want to delete this Client ?');");

    }
    private void dgClients_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dgClients.CurrentPageIndex = e.NewPageIndex;
        PopulateClient();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ValidateFields())
        {
            
            if (txtClientID.Text == "")
            {
                PCSN.InvoiceSystem.BusinessLogicLayer.Client Client = new PCSN.InvoiceSystem.BusinessLogicLayer.Client();
                Client.InsertClient(txtClientName.Text.ToString(), txtCompanyName.Text.ToString(), txtCompanyAddress.Text.ToString(), txtPhone.Text.ToString(), txtMobile.Text.ToString(), txtEmail.Text.ToString(), txtDescription.Text.ToString());
                lblErrorMessage.Text = "Client has been saved.";
                PopulateClient();
                ClearControls();

            }
            else
            {
                PCSN.InvoiceSystem.BusinessLogicLayer.Client Client = new PCSN.InvoiceSystem.BusinessLogicLayer.Client();
                Client.UpdateClient(Convert.ToInt32(txtClientID.Text), txtClientName.Text.ToString(), txtCompanyName.Text.ToString(), txtCompanyAddress.Text.ToString(), txtPhone.Text.ToString(), txtMobile.Text.ToString(), txtEmail.Text.ToString(), txtDescription.Text.ToString());
                lblErrorMessage.Text = "Client have been updated.";
                PopulateClient();
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

        if (txtClientName.Text.Trim() == "")
        {
            message += "Client Name is not specified.<br>";
            error = true;
        }
        if (txtCompanyName.Text.Trim() == "")
        {
            message += "Company Name is not specified.<br>";
            error = true;
        }
        if (txtCompanyAddress.Text.Trim() == "")
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

    private void PopulateClient()
    {
        PCSN.InvoiceSystem.BusinessLogicLayer.Client Client = new PCSN.InvoiceSystem.BusinessLogicLayer.Client();
        dtClientDG = Client.GetAllClient();
        dgClients.DataSource = dtClientDG;
        dgClients.DataBind();
    }

    public string EditItem(string ClientID)
    {
        return "client.aspx?ClientIDED=" + ClientID;
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("default.aspx");
    }
    public string DeleteItem(string ClientID)
    {
        return ClientID;
    }
    public void ClearControls()
    {
        txtClientID.Text = "";
        txtClientName.Text = "";
        txtCompanyName.Text = "";
        txtCompanyAddress.Text = "";
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
        this.dgClients.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgClients_PageIndexChanged);
        this.dgClients.ItemCreated += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgClients_ItemCreated);
        this.dgClients.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgClients_ItemCommand);

    }
    #endregion



}
