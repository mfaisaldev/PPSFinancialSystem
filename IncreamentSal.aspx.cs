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

public partial class IncreamentSal : System.Web.UI.Page
{
    #region Variables
    //private long IncreamentSalaryID = 0;
    DataTable dtIncreamentSalaryDG = new DataTable();
    #endregion

    # region Event Handler
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["SalIncIDED"] != null && Request.QueryString["SalIncIDED"].ToString() != "" && !Page.IsPostBack)
        {
            if (Session["UserType"] != null && Session["UserType"].ToString() == "Admin")
            {
                PCSN.InvoiceSystem.BusinessLogicLayer.IncreamentSalary IncSal = new PCSN.InvoiceSystem.BusinessLogicLayer.IncreamentSalary();
                DataTable dtIncSalDet = new DataTable();
                dtIncSalDet = IncSal.GetIncreamentSalaryByID(Convert.ToInt32(Request.QueryString["SalIncIDED"].ToString()));

                if (dtIncSalDet.Rows.Count > 0)
                {
                    txtIncSalID.Text = dtIncSalDet.Rows[0]["ID"].ToString();
                    txtSalID.Text = dtIncSalDet.Rows[0]["SalaryID"].ToString();
                    txtIncreamentDate.Text = dtIncSalDet.Rows[0]["IncDate"].ToString();
                    txtAmount.Text = dtIncSalDet.Rows[0]["IncAmount"].ToString();
                    txtDescription.Text = dtIncSalDet.Rows[0]["Description"].ToString();

                }
                txtEmpID.Text = Session["EmpID"].ToString();
                txtSalID.Text = Session["SalID"].ToString();
                
                dtIncSalDet = IncSal.GetIncreamentSalaryBySalaryAndEmployeeID(Convert.ToInt32(Session["SalID"].ToString()), Convert.ToInt32(Session["EmpID"].ToString()));
                if (dtIncSalDet.Rows.Count > 0)
                {
                    txtEmployeeName.Text = dtIncSalDet.Rows[0]["Name"].ToString();
                }
                PopulateIncreamentSalary(Convert.ToInt32(Session["SalID"].ToString()), Convert.ToInt32(Session["EmpID"].ToString()));
            }
            else
            {
                lblErrorMessage.Text = "You are not Authorized to perform this operation! Please login as Administrator. Press Button Cancel to Close this Window.";

            }

        }

        if (Request.QueryString["Sal_ID"] != null && Request.QueryString["Emp_ID"] != null && !Page.IsPostBack)
        {
            if (Request.QueryString["Sal_ID"].ToString() != "" && Request.QueryString["Emp_ID"].ToString() != "")
            {
                if (Session["UserType"] != null && Session["UserType"].ToString() == "Admin")
                {
                    Session["SalID"] = Request.QueryString["Sal_ID"].ToString();
                    Session["EmpID"] = Request.QueryString["Emp_ID"].ToString();
                    
                    txtEmpID.Text = Session["EmpID"].ToString();
                    txtSalID.Text = Session["SalID"].ToString();

                    PCSN.InvoiceSystem.BusinessLogicLayer.IncreamentSalary IncSal = new PCSN.InvoiceSystem.BusinessLogicLayer.IncreamentSalary();
                    DataTable dtIncSalDet = new DataTable();
                    dtIncSalDet = IncSal.GetIncreamentSalaryBySalaryAndEmployeeID(Convert.ToInt32(Request.QueryString["Sal_ID"].ToString()),Convert.ToInt32(Request.QueryString["Emp_ID"].ToString()));
                    if(dtIncSalDet.Rows.Count>0)
                    {
                        txtEmployeeName.Text = dtIncSalDet.Rows[0]["Name"].ToString();
                    }
                    PopulateIncreamentSalary(Convert.ToInt32(Request.QueryString["Sal_ID"].ToString()),Convert.ToInt32(Request.QueryString["Emp_ID"].ToString()));

                }
                else
                {
                    lblErrorMessage.Text = "You are not Authorized to perform this operation! Please login as Administrator. Press Button Cancel to Close this Window.";                    
                    btnSave.Visible = false;
                    btnCancel.Visible = false;
                }
            }
        }       
                
            
    }
    private void dgIncreamentSalary_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        if (e.CommandName == "DeleteIncreamentSalary")
        {
            string argsID = e.CommandArgument.ToString();

            long IncreamentSalaryID = Convert.ToInt32(argsID.ToString());

            if (IncreamentSalaryID > 0)
            {
                PCSN.InvoiceSystem.BusinessLogicLayer.IncreamentSalary IncreamentSalary = new PCSN.InvoiceSystem.BusinessLogicLayer.IncreamentSalary();
                IncreamentSalary.DeleteIncreamentSalary(IncreamentSalaryID);
                lblErrorMessage.Text = "IncreamentSalary Deleted Successfuly.";

                PopulateIncreamentSalary(Convert.ToInt32(Session["SalID"].ToString()), Convert.ToInt32(Session["EmpID"].ToString()));
                ClearControls();
            }
        }
    }

    private void dgIncreamentSalary_ItemCreated(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        LinkButton link = (LinkButton)e.Item.FindControl("Linkbutton2");
        if (link != null)
            link.Attributes.Add("onClick", "javascript:return confirm('This action will delete the information saved for this Increament.  Are you sure you want to delete this Increament?');");

    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        ClearControls();        
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("ManageEmployee.aspx");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ValidateFields())
        {

            if (txtIncSalID.Text == "")
            {
                PCSN.InvoiceSystem.BusinessLogicLayer.IncreamentSalary IncreamentSalary = new PCSN.InvoiceSystem.BusinessLogicLayer.IncreamentSalary();
                IncreamentSalary.InsertIncreamentSalary(Convert.ToInt32(txtSalID.Text), txtIncreamentDate.Text.ToString(), Convert.ToInt32(txtAmount.Text.ToString()), txtDescription.Text.ToString());
                lblErrorMessage.Text = "Increament Have been saved.";

                PCSN.InvoiceSystem.BusinessLogicLayer.Salary Sal = new PCSN.InvoiceSystem.BusinessLogicLayer.Salary();
                DataTable dtSalary = new DataTable();
                dtSalary = Sal.GetSalaryByEmployeeID(Convert.ToInt32(Session["EmpID"].ToString()));
                if (dtSalary.Rows.Count > 0)
                {
                    long CurrSal = Convert.ToInt32(dtSalary.Rows[0]["CurrentSalary"].ToString());
                    CurrSal = CurrSal + Convert.ToInt32(txtAmount.Text);
                    Sal.UpdateSalary(Convert.ToInt32(txtSalID.Text), Convert.ToInt32(txtEmpID.Text), CurrSal);
                }

                PopulateIncreamentSalary(Convert.ToInt32(Session["SalID"].ToString()), Convert.ToInt32(Session["EmpID"].ToString()));
                ClearControls();
            }
            else
            {
                PCSN.InvoiceSystem.BusinessLogicLayer.IncreamentSalary IncreamentSalary = new PCSN.InvoiceSystem.BusinessLogicLayer.IncreamentSalary();
                IncreamentSalary.UpdateIncreamentSalary(Convert.ToInt32(txtIncSalID.Text), Convert.ToInt32(txtSalID.Text), txtIncreamentDate.Text.ToString(), Convert.ToInt32(txtAmount.Text.ToString()), txtDescription.Text.ToString());

                PCSN.InvoiceSystem.BusinessLogicLayer.Salary Sal = new PCSN.InvoiceSystem.BusinessLogicLayer.Salary();
                DataTable dtSalary = new DataTable();
                dtSalary = Sal.GetSalaryByEmployeeID(Convert.ToInt32(Session["EmpID"].ToString()));
                if (dtSalary.Rows.Count > 0)
                {                    
                    Sal.UpdateSalary(Convert.ToInt32(txtSalID.Text), Convert.ToInt32(txtEmpID.Text), Convert.ToInt32(txtAmount.Text));
                }

                lblErrorMessage.Text = "Increament have been updated.";
                PopulateIncreamentSalary(Convert.ToInt32(Session["SalID"].ToString()), Convert.ToInt32(Session["EmpID"].ToString()));
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

        if (txtIncreamentDate.Text.Trim() == "")
        {
            message += "Date is not specified.<br>";
            error = true;
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
                txtAmount.Text = Convert.ToInt32(txtAmount.Text).ToString();
            }
            catch
            {
                message += "Amount must be numeric.<br>";
                error = true;
                txtAmount.Text = "0";
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

    private void PopulateIncreamentSalary(long Sal_ID, long Emp_ID)
    {
        PCSN.InvoiceSystem.BusinessLogicLayer.IncreamentSalary IncreamentSalary = new PCSN.InvoiceSystem.BusinessLogicLayer.IncreamentSalary();
        dtIncreamentSalaryDG = IncreamentSalary.GetIncreamentSalaryBySalaryAndEmployeeID(Sal_ID,Emp_ID);
        dgIncreamentSalarys.DataSource = dtIncreamentSalaryDG;
        dgIncreamentSalarys.DataBind();
    }

    public string EditIncreamentSalary(string IncreamentSalaryID)
    {
        return "IncreamentSal.aspx?SalIncIDED=" + IncreamentSalaryID;
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("default.aspx");
    }
    public string DeleteIncreamentSalary(string IncreamentSalaryID)
    {
        return IncreamentSalaryID;
    }
    public void ClearControls()
    {
        txtIncSalID.Text = "";
        txtIncreamentDate.Text = "";
        txtAmount.Text = "";
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
        
        btnClose.Attributes.Add("onclick", "javascript:window.close();");
        
        
    }
    private void InitializeComponent()
    {

        this.dgIncreamentSalarys.ItemCreated += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgIncreamentSalary_ItemCreated);
        this.dgIncreamentSalarys.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgIncreamentSalary_ItemCommand);

    }
    #endregion



    
}
