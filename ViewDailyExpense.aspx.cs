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


public partial class ViewDailyExpense : System.Web.UI.Page
{
    public string GenerateHTML = "";
    DataTable dtExpense = new DataTable();    

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["ExpenseMasterID"] != null && Request.QueryString["ExpenseMasterID"].ToString() != "")
        {
            // This is for Showing Full Invoice
            long ExpensemasterID = Convert.ToInt32(Request.QueryString["ExpenseMasterID"].ToString());
            //long ExpenseDetailID = 0;
            PCSN.InvoiceSystem.BusinessLogicLayer.DailyExpMaster Expense = new PCSN.InvoiceSystem.BusinessLogicLayer.DailyExpMaster();
            
            lblMainTitle.Text = "Daily Expense";

            lblDailyExpTitle.Text = "Daily Expense's Detail";

            dtExpense = Expense.GetDailyExpMasterByID(ExpensemasterID);
            if (dtExpense.Rows.Count > 0)
            {
                // Setting up Starting Values
                PCSN.InvoiceSystem.BusinessLogicLayer.CashInHand CashinHand = new PCSN.InvoiceSystem.BusinessLogicLayer.CashInHand();
                DataTable dtCashinHand = new DataTable();
                dtCashinHand = CashinHand.GetCashInHandByID(Convert.ToInt32(dtExpense.Rows[0]["CashInHandID"].ToString()));
                
                if (dtCashinHand.Rows.Count > 0)
                {
                    lblCashWasInHand.Text = dtCashinHand.Rows[0]["CashIN"].ToString();
                    lblRemainingCash.Text = dtCashinHand.Rows[0]["CashAvailable"].ToString();
                }
                lblCashWasInHand.Text = dtExpense.Rows[0]["StartCash"].ToString();
                lblRemainingCash.Text = dtExpense.Rows[0]["EndCash"].ToString();                
                lblGrandTotal.Text = dtExpense.Rows[0]["TotalAmount"].ToString();                
                lblExpDate.Text = dtExpense.Rows[0]["ExpDate"].ToString();
                lblExpOfTheDay.Text = dtExpense.Rows[0]["TotalAmount"].ToString();

                //Setting up Our Company Details
                PCSN.InvoiceSystem.BusinessLogicLayer.OurCompany ourCompany = new PCSN.InvoiceSystem.BusinessLogicLayer.OurCompany();
                DataTable dtOurCompany = new DataTable();
                dtOurCompany = ourCompany.GetOurCompanyByID(Convert.ToInt32("1"));

                if (dtOurCompany.Rows.Count > 0)
                {
                    //txtOurCompanyID.Text = dtOurCompany.Rows[0]["ID"].ToString();
                    lblCompanyName.Text = dtOurCompany.Rows[0]["CompanyName"].ToString() + Environment.NewLine;
                    lblAddress.Text = dtOurCompany.Rows[0]["CompanyAddress"].ToString() + Environment.NewLine;
                }
                else
                {
                    lblCompanyName.Text = "Power Protection System";
                }

                // Setting Up Each Expense
                for (int a = 0; a < dtExpense.Rows.Count; a++)
                {

                    GenerateHTML += "<tr>" + Environment.NewLine;

                    GenerateHTML += "<td>" + Environment.NewLine;
                    GenerateHTML += dtExpense.Rows[a]["ItemDesc"].ToString() + Environment.NewLine;
                    GenerateHTML += "</td>" + Environment.NewLine;

                    GenerateHTML += "<td id=\"quantity\">" + Environment.NewLine;
                    GenerateHTML += dtExpense.Rows[a]["Quantity"].ToString() + Environment.NewLine;
                    GenerateHTML += "</td>" + Environment.NewLine;



                    GenerateHTML += "<td id=\"rate\">" + Environment.NewLine;
                    GenerateHTML += dtExpense.Rows[a]["UnitPrice"].ToString() + Environment.NewLine;
                    GenerateHTML += "</td>" + Environment.NewLine;



                    GenerateHTML += "<td id=\"amount\">" + Environment.NewLine;
                    GenerateHTML += dtExpense.Rows[a]["ItemAmount"].ToString() + Environment.NewLine;
                    GenerateHTML += "</td>" + Environment.NewLine;


                    GenerateHTML += "</tr>" + Environment.NewLine;

                }
                lblGrandTotal.Text = dtExpense.Rows[0]["TotalAmount"].ToString();
            }

        }
    }

}
