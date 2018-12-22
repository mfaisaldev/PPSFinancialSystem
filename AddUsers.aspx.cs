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

public partial class AddUsers : System.Web.UI.Page
{
    #region Variables
    //private long UserID = 0;
    DataTable dtUserDG = new DataTable();
    #endregion

    # region Event Handler
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.QueryString["UserIDED"] != null && Request.QueryString["UserIDED"].ToString() != "" && !Page.IsPostBack)
        {
            PCSN.InvoiceSystem.BusinessLogicLayer.Users User = new PCSN.InvoiceSystem.BusinessLogicLayer.Users();
            DataTable dtUserDet = new DataTable();
            dtUserDet = User.GetUsersByID(Convert.ToInt32(Request.QueryString["UserIDED"].ToString()));

            if (dtUserDet.Rows.Count > 0)
            {
                txtID.Text = dtUserDet.Rows[0]["ID"].ToString();
                txtUserID.Text = dtUserDet.Rows[0]["UserID"].ToString();
                txtUserName.Text = dtUserDet.Rows[0]["UserName"].ToString();
                txtPassword.Text = dtUserDet.Rows[0]["Password"].ToString();
                txtConfirmPassword.Text = dtUserDet.Rows[0]["Password"].ToString();
                txtCreationDate.Text = dtUserDet.Rows[0]["CreationDate"].ToString();                
                cboUserType.SelectedItem.Text = dtUserDet.Rows[0]["UserType"].ToString();
                txtDescription.Text = dtUserDet.Rows[0]["Description"].ToString();
            }
        }
        if (!Page.IsPostBack)
        {
            PopulateUser();
        }
    }
    private void dgUsers_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        if (e.CommandName == "DeleteUser")
        {
            string argsID = e.CommandArgument.ToString();

            long UserID = Convert.ToInt32(argsID.ToString());

            if (UserID > 0)
            {
                PCSN.InvoiceSystem.BusinessLogicLayer.Users User = new PCSN.InvoiceSystem.BusinessLogicLayer.Users();
                User.DeleteUsers(UserID);
                lblErrorMessage.Text = "User Deleted Successfuly.";
                PopulateUser();
                ClearControls();
            }
        }
    }
    private void dgUsers_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dgUsers.CurrentPageIndex = e.NewPageIndex;
        PopulateUser();
    }

    private void dgUsers_ItemCreated(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        LinkButton link = (LinkButton)e.Item.FindControl("Linkbutton2");
        if (link != null)
            link.Attributes.Add("onClick", "javascript:return confirm('This action will delete the information saved for this User Account.  Are you sure you want to delete this User Account?');");

    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        PopulateUser();
        ClearControls();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ValidateFields())
        {

            if (txtID.Text == "")
            {
                PCSN.InvoiceSystem.BusinessLogicLayer.Users User = new PCSN.InvoiceSystem.BusinessLogicLayer.Users();
                User.InsertUsers(txtUserName.Text.ToString(), txtUserID.Text.ToString(), txtPassword.Text.ToString(), txtCreationDate.Text.ToString(), txtDescription.Text.ToString(), cboUserType.SelectedItem.Text.ToString());
                lblErrorMessage.Text = "User Account Have been saved.";
                PopulateUser();
                ClearControls();

            }
            else
            {
                PCSN.InvoiceSystem.BusinessLogicLayer.Users User = new PCSN.InvoiceSystem.BusinessLogicLayer.Users();
                User.UpdateUsers(Convert.ToInt32(txtID.Text), txtUserName.Text.ToString(), txtUserID.Text.ToString(), txtPassword.Text.ToString(), txtCreationDate.Text.ToString(), txtDescription.Text.ToString(), cboUserType.SelectedItem.Text.ToString());
                lblErrorMessage.Text = "User Account have been updated.";
                PopulateUser();
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

        if (txtUserName.Text.Trim() == "")
        {
            message += "User Name is not specified.<br>";
            error = true;
        }
        if (txtUserID.Text.Trim() == "")
        {
            message += "UserID is not specified.<br>";
            error = true;
        }
        if (txtCreationDate.Text.Trim() == "")
        {
            message += "Password is not specified.<br>";
            error = true;
        }
        else
        {
            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                message += "Password do not match.<br>";
                error = true;
            }
        }       
        if (cboUserType.SelectedItem.Text.Trim() == "Select Type")
        {
            message += "Type is not specified.<br>";
            error = true;
        }

        lblErrorMessage.Text = "";
        if (error)
            lblErrorMessage.Text = message;

        return !error;
    }

    private void PopulateUser()
    {
        PCSN.InvoiceSystem.BusinessLogicLayer.Users User = new PCSN.InvoiceSystem.BusinessLogicLayer.Users();
        dtUserDG = User.GetAllUsers();
        dgUsers.DataSource = dtUserDG;
        dgUsers.DataBind();
    }

    public string EditUser(string UserID)
    {
        return "AddUsers.aspx?UserIDED=" + UserID;
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("default.aspx");
    }
    public string DeleteUser(string UserID)
    {
        return UserID;
    }
    public void ClearControls()
    {
        txtUserID.Text = "";
        txtUserName.Text = "";
        txtConfirmPassword.Text = "";
        txtDescription.Text = "";
        txtCreationDate.Text = "";
        cboUserType.SelectedItem.Text = "Select Type";
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

        this.dgUsers.ItemCreated += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgUsers_ItemCreated);
        this.dgUsers.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgUsers_ItemCommand);
        this.dgUsers.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgUsers_PageIndexChanged);
    }
    #endregion



    
}
