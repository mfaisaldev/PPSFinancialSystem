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

public partial class Reports : System.Web.UI.Page
{
    #region Variable Declaration
    private DataTable dtCboFill = new DataTable();
    #endregion

    #region Events Handler

    protected void Page_Load(object sender, EventArgs e)
    {
        lblDateNow.Text = DateTime.Now.ToShortDateString();
    }
    protected void btnReportMonth_Click(object sender, EventArgs e)
    {
        if (cboSelectSysMonthly.SelectedValue == "0")
        {
            lblErrorMessage.Text = "Please Select a System First for Daily report";
        }
        if (cboSelectSysMonthly.SelectedValue == "1")// Bank Transaction
        {
            if (cboByMonth.SelectedValue != "0")
            {
                string DtOfMonth = "";
                if (cboByMonth.SelectedValue == "1")
                {
                    DtOfMonth = "01/01/2009";
                }
                if (cboByMonth.SelectedValue == "2")
                {
                    DtOfMonth = "02/01/2009";
                }
                if (cboByMonth.SelectedValue == "3")
                {
                    DtOfMonth = "03/01/2009";
                }
                if (cboByMonth.SelectedValue == "4")
                {
                    DtOfMonth = "04/01/2009";
                }
                if (cboByMonth.SelectedValue == "5")
                {
                    DtOfMonth = "05/01/2009";
                }
                if (cboByMonth.SelectedValue == "6")
                {
                    DtOfMonth = "06/01/2009";
                }
                if (cboByMonth.SelectedValue == "7")
                {
                    DtOfMonth = "07/01/2009";
                }
                if (cboByMonth.SelectedValue == "8")
                {
                    DtOfMonth = "08/01/2009";
                }
                if (cboByMonth.SelectedValue == "9")
                {
                    DtOfMonth = "09/01/2009";
                }
                if (cboByMonth.SelectedValue == "10")
                {
                    DtOfMonth = "10/01/2009";
                }
                if (cboByMonth.SelectedValue == "11")
                {
                    DtOfMonth = "11/01/2009";
                }
                if (cboByMonth.SelectedValue == "12")
                {
                    DtOfMonth = "12/01/2009";
                }
                Response.Redirect("ReportBankTrans.aspx?BankID=" + cboIDMonthly.SelectedValue.ToString() + "&RepBy=Monthly&RepValue=" + DtOfMonth.ToString().Trim());
            }
        }
    }
    protected void btnReportYear_Click(object sender, EventArgs e)
    {
        if (cboSelectSysYearly.SelectedValue == "0")
        {
            lblErrorMessage.Text = "Please Select a System First for Daily report";
        }
        if (cboSelectSysYearly.SelectedValue == "1")// Bank Transaction
        {
            if (cboByYear.SelectedValue != "0")
            {
                string DtOfMonth = "";
                if (cboByYear.SelectedValue == "2009")
                {
                    DtOfMonth = "01/01/2009";
                }
                if (cboByYear.SelectedValue == "2010")
                {
                    DtOfMonth = "02/01/2010";
                }
                if (cboByYear.SelectedValue == "2011")
                {
                    DtOfMonth = "03/01/2011";
                }
                if (cboByYear.SelectedValue == "2012")
                {
                    DtOfMonth = "04/01/2012";
                }
                if (cboByYear.SelectedValue == "2013")
                {
                    DtOfMonth = "04/01/2013";
                }
                
                Response.Redirect("ReportBankTrans.aspx?BankID=" + cboIDYearly.SelectedValue.ToString() + "&RepBy=Yearly&RepValue=" + DtOfMonth.ToString().Trim());
            }
        }
    }
    protected void btnReportToday_Click(object sender, EventArgs e)
    {
        if (cboSelectSysDaily.SelectedValue == "0")
        {
            lblErrorMessage.Text = "Please Select a System First for Daily report";
        }
        if (cboSelectSysDaily.SelectedValue == "1")// Bank Transaction
        {
            if (cboIDDate.SelectedValue != "0")
            {
                Response.Redirect("ReportBankTrans.aspx?BankID=" + cboIDDate.SelectedValue.ToString() + "&RepBy=Daily");
            }
        }
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
    protected void cboSelectSysDaily_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(cboSelectSysDaily.SelectedValue == "0")
        {
            lblErrorMessage.Text = "Please Select a System First for Daily report";
            cboIDDate.Items.Clear();
        }

        if (cboSelectSysDaily.SelectedValue == "1")// Bank Transaction
        {
            PopulateBanks("Daily");
        }
        else
        {
            cboIDDate.Items.Clear();
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
        if (TypeValue == "Daily")
        {
            cboIDDate.DataSource = dtCboFill;
            cboIDDate.DataTextField = "Name";
            cboIDDate.DataValueField = "ID";

            cboIDDate.DataBind();
        }
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

    #endregion

}
