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

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtUserID.Text == "" && txtPassword.Text == "")
        {
            lblErrorMessage.Text = "User ID and Password must be Provided.";
        }
        else
        {
            PCSN.InvoiceSystem.BusinessLogicLayer.Users users = new PCSN.InvoiceSystem.BusinessLogicLayer.Users();
            DataTable dtUsers = new DataTable();
            dtUsers = users.GetUsersByUserIDandPassword(txtUserID.Text.ToString(), txtPassword.Text.ToString());

            if(dtUsers.Rows.Count>0)
            {
                Session["UserID"] = txtUserID.Text.ToString();
                Session["UserType"] = dtUsers.Rows[0]["UserType"].ToString();
                Response.Redirect("default.aspx");
            }
            else
            {
                lblErrorMessage.Text = "Try Again!! User Does Not Exist or Wrong Password.";
            }
        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {

    }
}
