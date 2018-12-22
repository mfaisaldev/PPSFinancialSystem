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

public partial class Closing : System.Web.UI.Page
{
    #region Variable Declaration
    private DataTable dtCboFill = new DataTable();
    #endregion
    
    #region Event Handler

    protected void Page_Load(object sender, EventArgs e)
    {
        //lblDateNow.Text = DateTime.Now.ToShortDateString();
        cboByMonth.SelectedValue = DateTime.Now.Month.ToString();
        cboByMonthYear.SelectedValue = DateTime.Now.Year.ToString();
        cboByYear.SelectedValue = DateTime.Now.Year.ToString();
        if (Convert.ToInt16(DateTime.Now.Day) != CalculateDaysofMonth(Convert.ToInt16(DateTime.Now.Month.ToString()), DateTime.IsLeapYear(Convert.ToInt16(DateTime.Now.Year.ToString()))))
        {
            lblErrorMessage.Text = "Closing can not be performed today, it can only be performed at the end of this month. Thanks";
            Response.Redirect("default.aspx?errorMSG=" + lblErrorMessage.Text.ToString());
        }
        
    }

    
    protected void btnClosingMonth_Click(object sender, EventArgs e)
    {
        if (cboIDMonthly.SelectedValue == "0")
        {
            lblErrorMessage.Text = "Please Select an Item to be closed first.";
            return;
        }
        else
        {
            if (cboSelectSysMonthly.SelectedValue == "1") // bank transaction
            {
                if (cboByMonth.SelectedValue == "0" || cboByMonthYear.SelectedValue == "0")
                {
                    lblErrorMessage.Text = "Please select any month and year first.";
                }
                else
                {
                    int CMont = Convert.ToInt16(DateTime.Now.Month.ToString());
                    int CYear = Convert.ToInt16(DateTime.Now.Year.ToString());
                    int CDay = Convert.ToInt16(DateTime.Now.Day.ToString());
                    //if(DateTime.Now.
                    if (Convert.ToInt16(cboByMonth.SelectedValue) != CMont || Convert.ToInt16(cboByMonthYear.SelectedValue.ToString()) != CYear)
                    {
                        lblErrorMessage.Text = "Post months and years can not be closed. You are close Current month and year only.";
                    }
                    else
                    {
                        PCSN.InvoiceSystem.BusinessLogicLayer.BankAccountInfo BBinfo = new PCSN.InvoiceSystem.BusinessLogicLayer.BankAccountInfo();
                        DataTable dtBBinfo = new DataTable();
                        dtBBinfo = BBinfo.GetMonthlyBBalByBMonthandBankID(cboByMonth.SelectedValue, cboByMonthYear.SelectedValue, Convert.ToInt32(cboIDMonthly.SelectedValue.ToString()));
                        if (dtBBinfo.Rows.Count > 0)
                        {
                            lblErrorMessage.Text = "Closing of this bank for this month is already done.";
                        }
                        else
                        {
                            dtBBinfo = BBinfo.GetBankBalancesByBankID(Convert.ToInt32(cboIDMonthly.SelectedValue.ToString()));
                            if (dtBBinfo.Rows.Count > 0)
                            {
                                long AvailableBalance = Convert.ToInt32(dtBBinfo.Rows[0]["AvailableBalance"].ToString());
                                BBinfo.InsertMonthlyBBal(Convert.ToInt32(cboIDMonthly.SelectedValue.ToString()), AvailableBalance, cboByMonth.SelectedValue.ToString(), cboByMonthYear.SelectedValue.ToString());
                                lblErrorMessage.Text = "Closing for this month of this bank account is done now.";
                            }

                        }
                    }
                }
            }
        }

    }
    protected void btnClosingYear_Click(object sender, EventArgs e)
    {
        
    }

    protected void cboSelectSysYearly_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cboSelectSysYearly.SelectedValue == "0")
        {
            lblErrorMessage.Text = "Please Select a System First for Daily report";
            cboIDYearly.Items.Clear();
        }

        if (cboSelectSysYearly.SelectedValue == "1")// Bank Transaction
        {
            PopulateBanks("Yearly");
        }
        else
        {
            cboIDYearly.Items.Clear();
        }
    }
    protected void cboSelectSysMonthly_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cboSelectSysMonthly.SelectedValue == "0")
        {
            lblErrorMessage.Text = "Please Select a System First for Daily report";
            cboIDMonthly.Items.Clear();
        }

        if (cboSelectSysMonthly.SelectedValue == "1")// Bank Transaction
        {
            PopulateBanks("Monthly");
        }
        else
        {
            cboIDMonthly.Items.Clear();
        }

    }

    #endregion
    #region Methods

    private void PopulateBanks(string TypeValue)
    {
        PCSN.InvoiceSystem.BusinessLogicLayer.BankAccountInfo ChequeBook = new PCSN.InvoiceSystem.BusinessLogicLayer.BankAccountInfo();
        dtCboFill = ChequeBook.GetBankAccountInfoForDropDown();        
        if (TypeValue == "Monthly")
        {
            cboIDMonthly.DataSource = dtCboFill;
            cboIDMonthly.DataTextField = "Name";
            cboIDMonthly.DataValueField = "ID";

            cboIDMonthly.DataBind();
        }
        if (TypeValue == "Yearly")
        {
            cboIDYearly.DataSource = dtCboFill;
            cboIDYearly.DataTextField = "Name";
            cboIDYearly.DataValueField = "ID";

            cboIDYearly.DataBind();
        }
    }

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
