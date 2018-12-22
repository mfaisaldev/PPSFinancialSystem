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


public partial class ViewEmployee : System.Web.UI.Page
{
    public string GenerateHTML = "";
    DataTable dtEmployeeEdit = new DataTable();
    

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["EmployeeID"] != null && Request.QueryString["EmployeeID"].ToString() != "")
        {
            // This is for Showing Full Invoice
            long EmployeeID = Convert.ToInt32(Request.QueryString["EmployeeID"].ToString());
            //long EmployeeDetailID = 0;
            PCSN.InvoiceSystem.BusinessLogicLayer.Employee Employee = new PCSN.InvoiceSystem.BusinessLogicLayer.Employee();
            
            lblMainTitle.Text = "Employee Details";
            
            dtEmployeeEdit = Employee.GetEmployeeByID(EmployeeID);
            if (dtEmployeeEdit.Rows.Count > 0)
            {
                lblDate.Text = DateTime.Now.ToShortDateString();
                lblEmployeeID.Text = dtEmployeeEdit.Rows[0]["ID"].ToString();
                lblDateOfJoin.Text = dtEmployeeEdit.Rows[0]["DateOfJoin"].ToString();

                lblName.Text = dtEmployeeEdit.Rows[0]["Name"].ToString();
                lblFName.Text = dtEmployeeEdit.Rows[0]["FatherName"].ToString();
                lblDesignation.Text = dtEmployeeEdit.Rows[0]["Designation"].ToString();
                lblNIC.Text = dtEmployeeEdit.Rows[0]["NIC"].ToString();
                lblPhone.Text = dtEmployeeEdit.Rows[0]["Phone"].ToString();
                LblMobile.Text = dtEmployeeEdit.Rows[0]["Mobile"].ToString();
                lblEmail.Text = dtEmployeeEdit.Rows[0]["Email"].ToString();
                lblAddress.Text = dtEmployeeEdit.Rows[0]["Address"].ToString();
                lblCity.Text = dtEmployeeEdit.Rows[0]["City"].ToString();
                lblCountry.Text = dtEmployeeEdit.Rows[0]["Country"].ToString();
                lblReference.Text = dtEmployeeEdit.Rows[0]["Reference"].ToString();
                lblDescription.Text = dtEmployeeEdit.Rows[0]["Description"].ToString();
                lblStartingSalary.Text = dtEmployeeEdit.Rows[0]["StartingSalary"].ToString();

                PCSN.InvoiceSystem.BusinessLogicLayer.OurCompany ourCompany = new PCSN.InvoiceSystem.BusinessLogicLayer.OurCompany();
                DataTable dtOurCompany = new DataTable();
                dtOurCompany = ourCompany.GetOurCompanyByID(Convert.ToInt32("1"));

                if (dtOurCompany.Rows.Count > 0)
                {
                    //txtOurCompanyID.Text = dtOurCompany.Rows[0]["ID"].ToString();
                    lblCompanyName.Text = dtOurCompany.Rows[0]["CompanyName"].ToString() + Environment.NewLine;
                    lblCompanyAddress.Text = dtOurCompany.Rows[0]["CompanyAddress"].ToString() + Environment.NewLine;
                }                    
                else
                {
                    lblCompanyName.Text = "Power Protection System";
                }
                   

                PCSN.InvoiceSystem.BusinessLogicLayer.Salary Sal = new PCSN.InvoiceSystem.BusinessLogicLayer.Salary();
                DataTable dtSalary2 = new DataTable();
                dtSalary2 = Sal.GetSalaryByEmployeeID(EmployeeID);
                if (dtSalary2.Rows.Count > 0)
                {
                    PCSN.InvoiceSystem.BusinessLogicLayer.IncreamentSalary IncreamentSalary = new PCSN.InvoiceSystem.BusinessLogicLayer.IncreamentSalary();
                    DataTable dtIncSal = new DataTable();
                    dtIncSal = IncreamentSalary.GetIncreamentSalaryBySalaryAndEmployeeID(Convert.ToInt32(dtSalary2.Rows[0]["ID"].ToString()), EmployeeID);


                    for (int a = 0; a < dtIncSal.Rows.Count; a++)
                    {

                        GenerateHTML += "<tr>" + Environment.NewLine;

                        GenerateHTML += "<td>" + Environment.NewLine;
                        GenerateHTML += dtIncSal.Rows[a]["IncDate"].ToString() + Environment.NewLine;
                        GenerateHTML += "</td>" + Environment.NewLine;

                        GenerateHTML += "<td>" + Environment.NewLine;
                        GenerateHTML += dtIncSal.Rows[a]["IncAmount"].ToString() + Environment.NewLine;
                        GenerateHTML += "</td>" + Environment.NewLine;



                        GenerateHTML += "<td align=\"justify\">" + Environment.NewLine;
                        GenerateHTML += dtIncSal.Rows[a]["Description"].ToString() + Environment.NewLine;
                        GenerateHTML += "</td>" + Environment.NewLine;

                        GenerateHTML += "</tr>" + Environment.NewLine;

                    }
                }
                lblCurrentSalary.Text = dtEmployeeEdit.Rows[0]["CurrentSalary"].ToString();
            }

        }
    }

}
