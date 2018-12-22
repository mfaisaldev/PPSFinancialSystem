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


public partial class ViewLoan : System.Web.UI.Page
{
    public string GenerateHTML = "";
    DataTable dtLoanEdit = new DataTable();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["LoanID"] != null && Request.QueryString["LoanID"].ToString() != "")
        {
            // This is for Showing Full Invoice
            long LoanID = Convert.ToInt32(Request.QueryString["LoanID"].ToString());
            //long LoanDetailID = 0;
            PCSN.InvoiceSystem.BusinessLogicLayer.Loan Loan = new PCSN.InvoiceSystem.BusinessLogicLayer.Loan();
            lblMainTitle.Text = "Loan Report";
            dtLoanEdit = Loan.GetLoanByID(LoanID);
            if (dtLoanEdit.Rows.Count > 0)
            {
                lblLoanDate.Text = dtLoanEdit.Rows[0]["LoanDate"].ToString();
                lblLoanID.Text = dtLoanEdit.Rows[0]["ID"].ToString();
                lblDueDate.Text = dtLoanEdit.Rows[0]["DueDate"].ToString();
                if(dtLoanEdit.Rows[0]["IsCleared"].ToString() == "True")
                {
                    lblIsCleared.Text = "Cleared";
                }
                else
                {
                    lblIsCleared.Text = "UnCleared";
                }
                
                lblLoanAccount.Text = dtLoanEdit.Rows[0]["Name"].ToString();
                lblCompanyName.Text = dtLoanEdit.Rows[0]["CompanyName"].ToString();
                lblAddress.Text = dtLoanEdit.Rows[0]["Address"].ToString();
                lblPhone.Text = dtLoanEdit.Rows[0]["Phone"].ToString();
                lblAmountDue.Text = dtLoanEdit.Rows[0]["Amount"].ToString();
                

                //PCSN.InvoiceSystem.BusinessLogicLayer.Loan Loan = new PCSN.InvoiceSystem.BusinessLogicLayer.Loan();
                DataTable dtAccRecPay = new DataTable();
                dtAccRecPay = Loan.GetAllAccReceivableByLoanID(Convert.ToInt32(lblLoanID.Text.ToString()));
                long TotalTransAmount = 0;
                if (dtAccRecPay.Rows.Count > 0)
                {
                    lblAccTitle.Text = "Account Receivable Transactions";
                    for (int a = 0; a < dtAccRecPay.Rows.Count; a++)
                    {

                        GenerateHTML += "<tr>" + Environment.NewLine;

                        GenerateHTML += "<td>" + Environment.NewLine;
                        GenerateHTML += dtAccRecPay.Rows[a]["AccRecDate"].ToString() + Environment.NewLine;
                        GenerateHTML += "</td>" + Environment.NewLine;

                        GenerateHTML += "<td>" + Environment.NewLine;
                        GenerateHTML += dtAccRecPay.Rows[a]["Amount"].ToString() + Environment.NewLine;
                        GenerateHTML += "</td>" + Environment.NewLine;

                        GenerateHTML += "<td align=\"justify\">" + Environment.NewLine;
                        GenerateHTML += dtAccRecPay.Rows[a]["Description"].ToString() + Environment.NewLine;
                        GenerateHTML += "</td>" + Environment.NewLine;

                        GenerateHTML += "</tr>" + Environment.NewLine;
                        TotalTransAmount = TotalTransAmount + Convert.ToInt32(dtAccRecPay.Rows[a]["Amount"].ToString());
                    }
                }
                else
                {
                    dtAccRecPay = Loan.GetAllAccPayableByLoanID(Convert.ToInt32(lblLoanID.Text.ToString()));
                    if (dtAccRecPay.Rows.Count > 0)
                    {
                        lblAccTitle.Text = "Account Payable Transactions";
                        for (int a = 0; a < dtAccRecPay.Rows.Count; a++)
                        {

                            GenerateHTML += "<tr>" + Environment.NewLine;

                            GenerateHTML += "<td>" + Environment.NewLine;
                            GenerateHTML += dtAccRecPay.Rows[a]["AccPayDate"].ToString() + Environment.NewLine;
                            GenerateHTML += "</td>" + Environment.NewLine;

                            GenerateHTML += "<td>" + Environment.NewLine;
                            GenerateHTML += dtAccRecPay.Rows[a]["Amount"].ToString() + Environment.NewLine;
                            GenerateHTML += "</td>" + Environment.NewLine;

                            GenerateHTML += "<td align=\"justify\">" + Environment.NewLine;
                            GenerateHTML += dtAccRecPay.Rows[a]["Description"].ToString() + Environment.NewLine;
                            GenerateHTML += "</td>" + Environment.NewLine;

                            GenerateHTML += "</tr>" + Environment.NewLine;
                            TotalTransAmount = TotalTransAmount + Convert.ToInt32(dtAccRecPay.Rows[a]["Amount"].ToString());

                        }
                    }
                }
                lblTotalAmount.Text = TotalTransAmount.ToString();
    
                
                
            }

        }
    }

}
