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


public partial class ReportBankTrans : System.Web.UI.Page
{
    public string GenerateHTMLAccPay = "";
    public string GenerateHTMLAccRec = "";
    public string GenerateHTMLATMTrans = "";
    public string GenerateHTMLCashDep = "";
    DataTable dtBankEdit = new DataTable();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["BankID"] != null && Request.QueryString["BankID"].ToString() != "")
        {
            // This is for Showing Full Invoice
            long BankID = Convert.ToInt32(Request.QueryString["BankID"].ToString());
            //long BankDetailID = 0;
            int CMont = Convert.ToInt16(DateTime.Now.Month.ToString());
            if (CMont.ToString() != "1")
            {
                CMont = CMont - 1;
            }
            int CYear = Convert.ToInt16(DateTime.Now.Year.ToString());
            if (CMont.ToString() == "1")
            {
                CMont = 12;
                CYear = CYear - 1;
            }
            PCSN.InvoiceSystem.BusinessLogicLayer.BankAccountInfo Bank = new PCSN.InvoiceSystem.BusinessLogicLayer.BankAccountInfo();
            lblMainTitle.Text = "Bank Report";
            dtBankEdit = Bank.GetBankAccountInfoByID(BankID);
            if (dtBankEdit.Rows.Count > 0)
            {
                lblDateNow.Text = DateTime.Now.ToShortDateString();
                lblBankName.Text = dtBankEdit.Rows[0]["BankName"].ToString();
                lblAccHolderName.Text = dtBankEdit.Rows[0]["AccountHolderName"].ToString();
                lblAccountNumber.Text = dtBankEdit.Rows[0]["AccountNumber"].ToString();
                lblAvailBalance.Text = dtBankEdit.Rows[0]["AvailableBalance"].ToString();
                BankID = Convert.ToInt32(dtBankEdit.Rows[0]["ID"].ToString());
                
                // Cheque Issue Details
                PCSN.InvoiceSystem.BusinessLogicLayer.ChequeIssue CIssue = new PCSN.InvoiceSystem.BusinessLogicLayer.ChequeIssue();
                DataTable dtAccRecPay = new DataTable();

                string ReportBy = "";
                string RepValue = "";

                if (Request.QueryString["RepBy"] != null && Request.QueryString["RepBy"].ToString() != "")
                {
                    ReportBy = Request.QueryString["RepBy"].ToString();                    
                }
                if (Request.QueryString["RepValue"] != null && Request.QueryString["RepValue"].ToString() != "")
                {
                    RepValue = Request.QueryString["RepValue"].ToString();
                }

                if (ReportBy == "Daily")
                {
                    dtAccRecPay = CIssue.GetChequeIssueByBankIDAndDate(BankID,DateTime.Now.ToShortDateString());
                }
                if (ReportBy == "Monthly")
                {
                    dtAccRecPay = CIssue.GetChequeIssueByBankIDAndMonth(BankID, RepValue);                    
                }
                if (ReportBy == "Yearly")
                {
                    dtAccRecPay = CIssue.GetChequeIssueByBankIDAndYear(BankID, RepValue);
                }

                long TotalTransAmountCI = 0;
                long TotalTransAmountCR = 0;
                long TotalTransATM = 0;
                long TotalCashDep = 0;
                // Cheque Issue Transaction
                if (dtAccRecPay.Rows.Count > 0)
                {                    
                    for (int a = 0; a < dtAccRecPay.Rows.Count; a++)
                    {
                        GenerateHTMLAccPay += "<tr>" + Environment.NewLine;

                        GenerateHTMLAccPay += "<td>" + Environment.NewLine;
                        GenerateHTMLAccPay += dtAccRecPay.Rows[a]["IssueDate"].ToString() + Environment.NewLine;
                        GenerateHTMLAccPay += "</td>" + Environment.NewLine;

                        GenerateHTMLAccPay += "<td>" + Environment.NewLine;
                        GenerateHTMLAccPay += dtAccRecPay.Rows[a]["ChequeNumberWithPreFix"].ToString() + Environment.NewLine;
                        GenerateHTMLAccPay += "</td>" + Environment.NewLine;

                        GenerateHTMLAccPay += "<td>" + Environment.NewLine;
                        GenerateHTMLAccPay += dtAccRecPay.Rows[a]["ClientName"].ToString() + Environment.NewLine;
                        GenerateHTMLAccPay += "</td>" + Environment.NewLine;

                        GenerateHTMLAccPay += "<td align=\"right\">" + Environment.NewLine;
                        GenerateHTMLAccPay += dtAccRecPay.Rows[a]["Amount"].ToString() + Environment.NewLine;
                        GenerateHTMLAccPay += "</td>" + Environment.NewLine;

                        GenerateHTMLAccPay += "</tr>" + Environment.NewLine;
                        TotalTransAmountCI = TotalTransAmountCI + Convert.ToInt32(dtAccRecPay.Rows[a]["Amount"].ToString());
                    }
                    lblTotalAmountCheIssue.Text = TotalTransAmountCI.ToString();
                }
                // Cheque Received Transaction
                TotalTransAmountCR = 0;
                PCSN.InvoiceSystem.BusinessLogicLayer.ChequeReceived Creceived = new PCSN.InvoiceSystem.BusinessLogicLayer.ChequeReceived();

                if (ReportBy == "Daily")
                {                    
                    dtAccRecPay = Creceived.GetChequeReceivedByBankIDandDate(BankID, DateTime.Now.ToShortDateString());
                }
                if (ReportBy == "Monthly")
                {
                    dtAccRecPay = Creceived.GetAllChequeReceivedBankIDandMonth(BankID, RepValue);
                }
                if (ReportBy == "Yearly")
                {
                    dtAccRecPay = Creceived.GetAllChequeReceivedBankIDandYear(BankID, RepValue);
                }

                
                if (dtAccRecPay.Rows.Count > 0)
                {                   
                    for (int a = 0; a < dtAccRecPay.Rows.Count; a++)
                    {
                        GenerateHTMLAccRec += "<tr>" + Environment.NewLine;

                        GenerateHTMLAccRec += "<td>" + Environment.NewLine;
                        GenerateHTMLAccRec += dtAccRecPay.Rows[a]["RecDate"].ToString() + Environment.NewLine;
                        GenerateHTMLAccRec += "</td>" + Environment.NewLine;

                        GenerateHTMLAccRec += "<td>" + Environment.NewLine;
                        GenerateHTMLAccRec += dtAccRecPay.Rows[a]["ChequeNumber"].ToString() + Environment.NewLine;
                        GenerateHTMLAccRec += "</td>" + Environment.NewLine;

                        GenerateHTMLAccRec += "<td>" + Environment.NewLine;
                        GenerateHTMLAccRec += dtAccRecPay.Rows[a]["ClientName"].ToString() + Environment.NewLine;
                        GenerateHTMLAccRec += "</td>" + Environment.NewLine;
                                               
                        GenerateHTMLAccRec += "<td>" + Environment.NewLine;
                        GenerateHTMLAccRec += dtAccRecPay.Rows[a]["AddDate"].ToString() + Environment.NewLine;
                        GenerateHTMLAccRec += "</td>" + Environment.NewLine;

                        GenerateHTMLAccRec += "<td align=\"right\">" + Environment.NewLine;
                        GenerateHTMLAccRec += dtAccRecPay.Rows[a]["Amount"].ToString() + Environment.NewLine;
                        GenerateHTMLAccRec += "</td>" + Environment.NewLine;

                        GenerateHTMLAccRec += "</tr>" + Environment.NewLine;
                        TotalTransAmountCR = TotalTransAmountCR + Convert.ToInt32(dtAccRecPay.Rows[a]["Amount"].ToString());
                    }
                    lblTotalAmountCheRec.Text = TotalTransAmountCR.ToString();
                }
                // ATM Transaction
                PCSN.InvoiceSystem.BusinessLogicLayer.ATMCard ATMTrans = new PCSN.InvoiceSystem.BusinessLogicLayer.ATMCard();
                DataTable dtBBal = new DataTable();
                if (ReportBy == "Daily")
                {                    
                    dtAccRecPay = ATMTrans.GetATMTransByTransDateandBankID(DateTime.Now.ToShortDateString(), BankID);
                    // Balance at start of a Month

                    dtBBal = Bank.GetMonthlyBBalByBMonthandBankID(CMont.ToString(), CYear.ToString(), BankID);
                    if (dtBBal.Rows.Count > 0)
                    {
                        lblBalanceStartOfMonth.Text = dtBBal.Rows[0]["AvailableBalance"].ToString();
                    }
                    //==============================
                }
                if (ReportBy == "Monthly")
                {
                    dtAccRecPay = ATMTrans.GetATMTransByTransMonthandBankID(RepValue, BankID);
                    // Balance at start of a Month

                    dtBBal = Bank.GetMonthlyBBalByBMonthandBankID(CMont.ToString(), CYear.ToString(), BankID);
                    if (dtBBal.Rows.Count > 0)
                    {
                        lblBalanceStartOfMonth.Text = dtBBal.Rows[0]["AvailableBalance"].ToString();
                    }
                    //==============================
                }
                if (ReportBy == "Yearly")
                {
                    dtAccRecPay = ATMTrans.GetATMTransByTransYearandBankID(RepValue, BankID);
                }

                
                if (dtAccRecPay.Rows.Count > 0)
                {                   
                    for (int a = 0; a < dtAccRecPay.Rows.Count; a++)
                    {
                        GenerateHTMLATMTrans += "<tr>" + Environment.NewLine;

                        GenerateHTMLATMTrans += "<td>" + Environment.NewLine;
                        GenerateHTMLATMTrans += dtAccRecPay.Rows[a]["CardNumber"].ToString() + Environment.NewLine;
                        GenerateHTMLATMTrans += "</td>" + Environment.NewLine;

                        GenerateHTMLATMTrans += "<td>" + Environment.NewLine;
                        GenerateHTMLATMTrans += dtAccRecPay.Rows[a]["TransDate"].ToString() + Environment.NewLine;
                        GenerateHTMLATMTrans += "</td>" + Environment.NewLine;

                        GenerateHTMLATMTrans += "<td>" + Environment.NewLine;
                        GenerateHTMLATMTrans += dtAccRecPay.Rows[a]["TransactionID"].ToString() + Environment.NewLine;
                        GenerateHTMLATMTrans += "</td>" + Environment.NewLine;

                        GenerateHTMLATMTrans += "<td align=\"right\">" + Environment.NewLine;
                        GenerateHTMLATMTrans += dtAccRecPay.Rows[a]["Amount"].ToString() + Environment.NewLine;
                        GenerateHTMLATMTrans += "</td>" + Environment.NewLine;
                                                
                        GenerateHTMLATMTrans += "</tr>" + Environment.NewLine;
                        TotalTransATM = TotalTransATM + Convert.ToInt32(dtAccRecPay.Rows[a]["Amount"].ToString());
                    }
                    lblTotalAmountATMTrans.Text = TotalTransATM.ToString();
                }

                // Cash Deposit Transaction
                PCSN.InvoiceSystem.BusinessLogicLayer.CashDeposit CashDep = new PCSN.InvoiceSystem.BusinessLogicLayer.CashDeposit();
                
                if (ReportBy == "Daily")
                {                    
                    dtAccRecPay = CashDep.GetCashDepositByDateandBankID(BankID, DateTime.Now.ToShortDateString());                    
                }
                if (ReportBy == "Monthly")
                {
                    dtAccRecPay = CashDep.GetCashDepositByMonthandBankID(BankID, RepValue);                    
                }
                if (ReportBy == "Yearly")
                {
                    dtAccRecPay = CashDep.GetCashDepositByYearandBankID(BankID, RepValue);
                }

                
                if (dtAccRecPay.Rows.Count > 0)
                {                   
                    for (int a = 0; a < dtAccRecPay.Rows.Count; a++)
                    {
                        GenerateHTMLCashDep += "<tr>" + Environment.NewLine;
                        
                        GenerateHTMLCashDep += "<td>" + Environment.NewLine;
                        GenerateHTMLCashDep += dtAccRecPay.Rows[a]["DepDate"].ToString() + Environment.NewLine;
                        GenerateHTMLCashDep += "</td>" + Environment.NewLine;

                        GenerateHTMLCashDep += "<td>" + Environment.NewLine;
                        GenerateHTMLCashDep += dtAccRecPay.Rows[a]["DepBy"].ToString() + Environment.NewLine;
                        GenerateHTMLCashDep += "</td>" + Environment.NewLine;

                        GenerateHTMLCashDep += "<td align=\"right\">" + Environment.NewLine;
                        GenerateHTMLCashDep += dtAccRecPay.Rows[a]["Amount"].ToString() + Environment.NewLine;
                        GenerateHTMLCashDep += "</td>" + Environment.NewLine;
                                                
                        GenerateHTMLCashDep += "</tr>" + Environment.NewLine;
                        TotalCashDep = TotalCashDep + Convert.ToInt32(dtAccRecPay.Rows[a]["Amount"].ToString());
                    }
                    lblTotalAmountCashDep.Text = TotalCashDep.ToString();
                }
                



            }

        }
    }

}
