using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Default : System.Web.UI.Page
{
    #region Event Handler
    protected void Page_Load(object sender, EventArgs e)
    {
        /*if (Session["UserID"] == null || Session["UserID"].ToString() == "")
        {
            Response.Redirect("login.aspx");
        }*/

        if (Request.QueryString["errorMSG"] != null && Request.QueryString["errorMSG"].ToString() != "")
        {
            lblErrorMessage.Text = Request.QueryString["errorMSG"].ToString();
        }

        // AUTO Monthly CLOSING OF Bank Accounts        

        if (Convert.ToInt16(DateTime.Now.Day) == CalculateDaysofMonth(Convert.ToInt16(DateTime.Now.Month.ToString()), DateTime.IsLeapYear(Convert.ToInt16(DateTime.Now.Year.ToString()))))
        {

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

            int CDay = Convert.ToInt16(DateTime.Now.Day.ToString());
            PCSN.InvoiceSystem.BusinessLogicLayer.BankAccountInfo BBinfo = new PCSN.InvoiceSystem.BusinessLogicLayer.BankAccountInfo();
            DataTable dtBBinfo = new DataTable();
            DataTable dtMonthBal = new DataTable();
            DataTable dtInsertMonthBal = new DataTable();
            long BankID = 0;
            string BankName = "";
            string AccountNumber = "";
            string Ermsg = "";

            dtBBinfo = BBinfo.GetAllBankAccountInfo();

            for (int i = 0; i < dtBBinfo.Rows.Count; i++)
            {
                BankID = Convert.ToInt32(dtBBinfo.Rows[i]["ID"].ToString());
                BankName = dtBBinfo.Rows[i]["BankName"].ToString();
                AccountNumber = dtBBinfo.Rows[i]["AccountNumber"].ToString();
                dtMonthBal = BBinfo.GetMonthlyBBalByBMonthandBankID(CMont.ToString(), CYear.ToString(), BankID);
                if (dtMonthBal.Rows.Count > 0)
                {

                }
                else
                {
                    dtInsertMonthBal = BBinfo.GetBankBalancesByBankID(BankID);
                    if (dtInsertMonthBal.Rows.Count > 0)
                    {
                        long AvailableBalance = Convert.ToInt32(dtInsertMonthBal.Rows[0]["AvailableBalance"].ToString());
                        BBinfo.InsertMonthlyBBal(BankID, AvailableBalance, CMont.ToString(), CYear.ToString());
                        Ermsg += "Closing for the month " + CMont.ToString() + "/" + CYear.ToString() + " of bank account number " + BankName + "#" + AccountNumber + " is Automaticall done now. <br />";
                        lblErrorMessage.Text = Ermsg;
                    }



                }
            }
        }
        //=========================================================
    }
    #endregion

    #region METHODS
    public int CalculateDaysofMonth(int MonthInWords, bool IsLeapYear)
    {
        if (MonthInWords == 1)
            return 31;
        if (MonthInWords == 2)
        {
            if (IsLeapYear == true)
            {
                return 29;
            }
            else
            {
                return 28;
            }
        }
        if (MonthInWords == 3)
            return 31;
        if (MonthInWords == 4)
            return 30;
        if (MonthInWords == 5)
            return 31;
        if (MonthInWords == 6)
            return 30;
        if (MonthInWords == 7)
            return 31;
        if (MonthInWords == 8)
            return 31;
        if (MonthInWords == 9)
            return 30;
        if (MonthInWords == 10)
            return 31;
        if (MonthInWords == 11)
            return 30;
        if (MonthInWords == 12)
            return 31;
        else
            return 0;
    }
    #endregion
}

