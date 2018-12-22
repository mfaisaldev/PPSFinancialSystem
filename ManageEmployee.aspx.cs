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

public partial class ManageEmployee : System.Web.UI.Page
{
    #region Variables
    //private long EmployeeID = 0;
    DataTable dtEmployeeDG = new DataTable();
    #endregion

    # region Event Handler
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.QueryString["EmployeeIDED"] != null && Request.QueryString["EmployeeIDED"].ToString() != "" && !Page.IsPostBack)
        {
            PCSN.InvoiceSystem.BusinessLogicLayer.Employee Employee = new PCSN.InvoiceSystem.BusinessLogicLayer.Employee();
            DataTable dtEmployeeDet = new DataTable();
            dtEmployeeDet = Employee.GetEmployeeByID(Convert.ToInt32(Request.QueryString["EmployeeIDED"].ToString()));

            if (dtEmployeeDet.Rows.Count > 0)
            {
                txtID.Text = dtEmployeeDet.Rows[0]["ID"].ToString();
                txtName.Text = dtEmployeeDet.Rows[0]["Name"].ToString();
                txtFatherName.Text = dtEmployeeDet.Rows[0]["FatherName"].ToString();
                txtPhone.Text = dtEmployeeDet.Rows[0]["Phone"].ToString();
                txtCellPhone.Text = dtEmployeeDet.Rows[0]["Mobile"].ToString();
                txtEmail.Text = dtEmployeeDet.Rows[0]["Email"].ToString();
                txtAddress.Text = dtEmployeeDet.Rows[0]["Address"].ToString();
                txtCity.Text = dtEmployeeDet.Rows[0]["City"].ToString();
                txtCountry.Text = dtEmployeeDet.Rows[0]["Country"].ToString();
                cboDesignation.SelectedValue = dtEmployeeDet.Rows[0]["Designation"].ToString();
                txtSalary.Text = dtEmployeeDet.Rows[0]["CurrentSalary"].ToString();
                txtSalary.Enabled = false;
                txtDateOfJoin.Text = dtEmployeeDet.Rows[0]["DateOfJoin"].ToString();
                txtReference.Text = dtEmployeeDet.Rows[0]["Reference"].ToString();
                txtDescription.Text = dtEmployeeDet.Rows[0]["Description"].ToString();
                txtNIC.Text = dtEmployeeDet.Rows[0]["NIC"].ToString();

            }

        }
        
        if (!Page.IsPostBack)
        {
            PopulateEmployee();
            txtDateOfJoin.Text = DateTime.Now.ToShortDateString();
        }
    }
    private void dgEmployees_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        if (e.CommandName == "DeleteEmployee")
        {
            string argsID = e.CommandArgument.ToString();

            long EmployeeID = Convert.ToInt32(argsID.ToString());

            if (EmployeeID > 0)
            {
                PCSN.InvoiceSystem.BusinessLogicLayer.Employee Employee = new PCSN.InvoiceSystem.BusinessLogicLayer.Employee();
                Employee.DeleteEmployee(EmployeeID);
                lblErrorMessage.Text = "Employee Deleted Successfuly.";
                PopulateEmployee();
                ClearControls();
            }
        }
    }

    private void dgEmployees_ItemCreated(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        LinkButton link = (LinkButton)e.Item.FindControl("Linkbutton2");
        if (link != null)
        {
            link.Attributes.Add("onClick", "javascript:return confirm('This action will delete the information saved for this Employee.  Are you sure you want to delete this Employee?');");
        }

        HyperLink link2 = (HyperLink)e.Item.FindControl("Linkbutton3");
        if (link2 != null)
        {
            if (Session["UserType"] != null && Session["UserType"].ToString().Trim() == "Admin")
            {
                link2.Attributes.Add("onClick", "javascript:return confirm('Are you sure you want to perform Increament for this Employee?');");
            }
            else
            {
                link2.Attributes.Add("onClick", "javascript:return confirm('You are not an Administrator to perform this operation! Please choose cancel and Login as Administrator');");
            }
            
        }
    }
    private void dgEmployees_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dgEmployees.CurrentPageIndex = e.NewPageIndex;
        PopulateEmployee();
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        PopulateEmployee();
        ClearControls();
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("default.aspx");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ValidateFields())
        {

            if (txtID.Text == "")
            {
                
                PCSN.InvoiceSystem.BusinessLogicLayer.Employee Employee = new PCSN.InvoiceSystem.BusinessLogicLayer.Employee();

                long EmpID = Employee.InsertEmployee(txtName.Text.ToString(), txtFatherName.Text.ToString(), txtPhone.Text.ToString(), txtCellPhone.Text.ToString(), txtEmail.Text.ToString(), txtAddress.Text.ToString(), txtCity.Text.ToString(), txtCountry.Text.ToString(), cboDesignation.SelectedItem.Text.ToString(), txtDateOfJoin.Text.ToString(), txtReference.Text.ToString(),txtDescription.Text.ToString(),txtNIC.Text.ToString());

                if (EmpID > 0)
                {
                    PCSN.InvoiceSystem.BusinessLogicLayer.Salary Sal = new PCSN.InvoiceSystem.BusinessLogicLayer.Salary();
                    long SalIncID = Sal.InsertSalary(EmpID,Convert.ToInt32(txtSalary.Text),Convert.ToInt32(txtSalary.Text));
                    if (SalIncID > 0)
                    {
                        PCSN.InvoiceSystem.BusinessLogicLayer.IncreamentSalary IncreamentSalary = new PCSN.InvoiceSystem.BusinessLogicLayer.IncreamentSalary();
                        IncreamentSalary.InsertIncreamentSalary(Convert.ToInt32(SalIncID), DateTime.Now.ToShortDateString(), Convert.ToInt32(txtSalary.Text.ToString()), "Initial Salary at Start of Job. By Modifying this you will just Update the Current Salary, We Recommend you to add new Increament for Increasing the salary of this Employee. Thanks");
                    }
                
                }
                
                lblErrorMessage.Text = "Employee Account Have been saved.";
                PopulateEmployee();
                ClearControls();

            }
            else
            {
                PCSN.InvoiceSystem.BusinessLogicLayer.Employee Employee = new PCSN.InvoiceSystem.BusinessLogicLayer.Employee();
                Employee.UpdateEmployee(Convert.ToInt32(txtID.Text), txtName.Text.ToString(), txtFatherName.Text.ToString(), txtPhone.Text.ToString(), txtCellPhone.Text.ToString(), txtEmail.Text.ToString(), txtAddress.Text.ToString(), txtCity.Text.ToString(), txtCountry.Text.ToString(), cboDesignation.SelectedItem.Text.ToString(), txtDateOfJoin.Text.ToString(), txtReference.Text.ToString(), txtDescription.Text.ToString(), txtNIC.Text.ToString());

                PCSN.InvoiceSystem.BusinessLogicLayer.Salary Sal = new PCSN.InvoiceSystem.BusinessLogicLayer.Salary();
                DataTable dtSalary2 = new DataTable();
                dtSalary2 = Sal.GetSalaryByEmployeeID(Convert.ToInt32(txtID.Text));
                if (dtSalary2.Rows.Count > 0)
                {
                    PCSN.InvoiceSystem.BusinessLogicLayer.IncreamentSalary IncreamentSalary = new PCSN.InvoiceSystem.BusinessLogicLayer.IncreamentSalary();
                    DataTable dtIncSal = new DataTable();
                    dtIncSal = IncreamentSalary.GetIncreamentSalaryBySalaryAndEmployeeID(Convert.ToInt32(dtSalary2.Rows[0]["ID"].ToString()), Convert.ToInt32(txtID.Text));
                    if (dtIncSal.Rows.Count > 0)
                    {

                    }
                    else
                    {
                        IncreamentSalary.InsertIncreamentSalary(Convert.ToInt32(dtSalary2.Rows[0]["ID"].ToString()), DateTime.Now.ToShortDateString(), Convert.ToInt32(txtSalary.Text.ToString()), "Initial Salary at Start of Job. By Modifying this you will just Update the Current Salary, We Recommend you to add new Increament for Increasing the salary of this Employee. Thanks");
                    }
                }
                else
                {
                    long SalIncID = Sal.InsertSalary(Convert.ToInt32(txtID.Text), Convert.ToInt32(txtSalary.Text), Convert.ToInt32(txtSalary.Text));
                    if (SalIncID > 0)
                    {
                        PCSN.InvoiceSystem.BusinessLogicLayer.IncreamentSalary IncreamentSalary = new PCSN.InvoiceSystem.BusinessLogicLayer.IncreamentSalary();
                        IncreamentSalary.InsertIncreamentSalary(Convert.ToInt32(SalIncID), DateTime.Now.ToShortDateString(), Convert.ToInt32(txtSalary.Text.ToString()), "Initial Salary at Start of Job. By Modifying this you will just Update the Current Salary, We Recommend you to add new Increament for Increasing the salary of this Employee. Thanks");
                    }
                }

                lblErrorMessage.Text = "Employee Account have been updated.";
                PopulateEmployee();
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

        if (txtName.Text.Trim() == "")
        {
            message += "Employee Name is not specified.<br>";
            error = true;
        }
        if (txtFatherName.Text.Trim() == "")
        {
            message += "Employee Father Name is not specified.<br>";
            error = true;
        }
        if (txtPhone.Text.Trim() == "")
        {
            message += "Phone is not specified.<br>";
            error = true;
        }
        if (txtCellPhone.Text.Trim() == "")
        {
            message += "Mobile is not specified.<br>";
            error = true;
        }
        if (txtAddress.Text.Trim() == "")
        {
            message += "Address is not specified.<br>";
            error = true;
        }
        if (txtNIC.Text.Trim() == "")
        {
            message += "NIC is not specified.<br>";
            error = true;
        }
        
        if (cboDesignation.SelectedItem.Text.Trim() == "Select Designation")
        {
            message += "Designation is not specified.<br>";
            error = true;
        }
        if (txtSalary.Text.Trim() == "")
        {
            message += "Salary is not specified.<br>";
            error = true;
        }
        else
        {
            try
            {
                txtSalary.Text = Convert.ToInt32(txtSalary.Text).ToString();
            }
            catch
            {
                txtSalary.Text = "0";
                message += "Salary Must be numeric.<br>";
                error = true;
            }
        }

        lblErrorMessage.Text = "";
        if (error)
            lblErrorMessage.Text = message;

        return !error;
    }

    private void PopulateEmployee()
    {
        PCSN.InvoiceSystem.BusinessLogicLayer.Employee Employee = new PCSN.InvoiceSystem.BusinessLogicLayer.Employee();
        dtEmployeeDG = Employee.GetAllEmployee();
        dgEmployees.DataSource = dtEmployeeDG;
        dgEmployees.DataBind();
    }

    public string EditEmployee(string EmployeeID)
    {
        return "ManageEmployee.aspx?EmployeeIDED=" + EmployeeID;
    }
    
    public string DeleteEmployee(string EmployeeID)
    {
        return EmployeeID;
    }
    public string Increament(string SalID, string EmpID)
    {
        return "IncreamentSal.aspx?Sal_ID=" + SalID + "&Emp_ID=" + EmpID;        
    }
    public string View(string EmpID)
    {
        return "ViewEmployee.aspx?EmployeeID=" + EmpID;
    }
    public void ClearControls()
    {
        txtID.Text = "";
        txtName.Text = "";
        txtFatherName.Text = "";
        txtCellPhone.Text = "";
        txtPhone.Text = "";
        txtEmail.Text = "";
        txtAddress.Text = "";
        txtCity.Text = "";
        txtCountry.Text = "";
        txtSalary.Text = "";
        txtSalary.Enabled = true;
        txtDateOfJoin.Text = DateTime.Now.ToShortDateString();
        txtReference.Text = "";
        txtDescription.Text = "";
        txtNIC.Text = "";
        cboDesignation.SelectedValue = "Select Designation";
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
        this.dgEmployees.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgEmployees_PageIndexChanged);
        this.dgEmployees.ItemCreated += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgEmployees_ItemCreated);
        this.dgEmployees.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgEmployees_ItemCommand);

    }
    #endregion



}
