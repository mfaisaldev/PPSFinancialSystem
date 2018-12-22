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

public partial class ChkRecAddToBalance : System.Web.UI.Page
{
    #region Variable Declaration

    private DataTable dtChequeRecDG = new DataTable();
    private DataTable dtChkRecSrch = new DataTable();
    
    #endregion

    #region Event Handler
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            PopulateChequeReceived();
            PopulateClient();
            PopulateBanks();
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        PopulateChequeReceived();
        PopulateClient();
        PopulateBanks();        
        ClearControls();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string error = "";
        //lblGranTotal.Text = "0";
        if (txtFromDate.Text != "" && txtToDate.Text != "")
        {
            string FromDate = txtFromDate.Text.ToString();
            string ToDate = txtToDate.Text.ToString();

            try
            {
                FromDate = Convert.ToDateTime(txtFromDate.Text).ToShortDateString();
                ToDate = Convert.ToDateTime(txtToDate.Text).ToShortDateString();
                PopulateChequeReceivedBySearch("ToAndFromDate", FromDate.ToString() + "#" + ToDate.ToString());
            }
            catch
            {
                error += "Dates must be valid. <br />";
            }
        }
        else if (txtReceivedDate.Text != "")
        {
            try
            {
                txtReceivedDate.Text = Convert.ToDateTime(txtReceivedDate.Text.ToString()).ToShortDateString();
                PopulateChequeReceivedBySearch("ByRecDate", txtReceivedDate.Text.ToString());
            }
            catch
            {
                error += "Received Date is not Valid. <br />";
            }
        }
        else if (txtChequeNumber.Text != "")
        {
            try
            {
                //txtReceivedDate.Text = Convert.ToDateTime(txtReceivedDate.Text.ToString()).ToShortDateString();
                PopulateChequeReceivedBySearch("ByChequeNumber", txtChequeNumber.Text.ToString().Trim());
            }
            catch
            {
                error += "Cheque Number is not Valid. <br />";
            }
        }
        else if (cboClient.SelectedValue != "0")
        {
            try
            {
                //txtReceivedDate.Text = Convert.ToDateTime(txtReceivedDate.Text.ToString()).ToShortDateString();
                PopulateChequeReceivedBySearch("ByClient", cboClient.SelectedValue.ToString());
            }
            catch
            {
                error += "Please Select a Client. <br />";
            }
        }
        else if (cboByMonth.SelectedValue != "0" && cboByYear.SelectedValue != "0")
        {
            string SelMonth = "";
            string SelYear = "";
            try
            {
                SelMonth = cboByMonth.SelectedValue.ToString();
                SelYear = cboByYear.SelectedValue.ToString();

                string dtimeSearch = Convert.ToDateTime(SelMonth.Trim() + "/1/" + SelYear.Trim()).ToShortDateString();

                PopulateChequeReceivedBySearch("ByMonthYear", dtimeSearch.ToString());
            }
            catch
            {
                error += "Month and Years are not in Valid Format. <br />";
            }
        }
        else
        {
            //PopulateChequeIssue();
        }

        if (error != "")
            lblErrorMessage.Text = error.ToString();
        else
            lblErrorMessage.Text = "";

        ClearControls();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }
    protected void btncancelOrder_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }
    protected void btnprintOrder_Click(object sender, EventArgs e)
    {
        lblErrorMessage.Text = "";
        if (cboBank.SelectedValue != "0")
        {
            long bankID = Convert.ToInt32(cboBank.SelectedValue.ToString());
            DataTable dtChkRec = new DataTable();
            DataTable dtBBal = new DataTable();
            PCSN.InvoiceSystem.BusinessLogicLayer.ChequeReceived CheRec = new PCSN.InvoiceSystem.BusinessLogicLayer.ChequeReceived();
            PCSN.InvoiceSystem.BusinessLogicLayer.BankAccountInfo BAccInfo = new PCSN.InvoiceSystem.BusinessLogicLayer.BankAccountInfo();
            bool checkBoxChecked = false;
            foreach (DataGridItem dg in dgChequeReceiveds.Items)
            {
                CheckBox chk = (CheckBox)dg.FindControl("chkSelected");
                if (chk.Checked)
                {
                    checkBoxChecked = true;
                    break;
                }
            }

            if (!checkBoxChecked)
            {
                lblErrorMessage.Text = "No Cheque selected.";
                return;
            }
            bool CheckingBit = true;
            long CRID = 0;
            long RecAmount = 0;
            dtChkRec = BAccInfo.GetBankAccountInfoByID(bankID);
            if (dtChkRec.Rows.Count > 0)
            {
                if (dtChkRec.Rows[0]["AvailableBalance"] != null && dtChkRec.Rows[0]["AvailableBalance"].ToString() != "")
                {
                    RecAmount = Convert.ToInt32(dtChkRec.Rows[0]["AvailableBalance"].ToString());
                }
                foreach (DataGridItem dg in dgChequeReceiveds.Items)
                {
                    CheckBox chk = (CheckBox)dg.FindControl("chkSelected");
                    if (chk.Checked)
                    {
                        Label lbl = (Label)dg.FindControl("lblChkRecID");
                        CRID = Convert.ToInt32(lbl.Text);
                        dtChkRec = CheRec.GetChequeReceivedByID(CRID);
                        if (dtChkRec.Rows.Count > 0)
                        {
                            if (dtChkRec.Rows[0]["IsCleared"].ToString() == "True")
                            {                                
                                dtBBal = BAccInfo.GetAddBalanceByChequeRecID(CRID);
                                if (dtBBal.Rows.Count > 0)
                                {
                                    lblErrorMessage.Text += "You have already used Cheque Number : " + dtBBal.Rows[0]["ChequeNumber"].ToString() + " For the Account Number : " + dtBBal.Rows[0]["AccountNumber"].ToString() + " of Bank : " + dtBBal.Rows[0]["BankName"].ToString() + ".<br />";
                                }
                                else
                                {
                                    CheckingBit = false;
                                    RecAmount = RecAmount + Convert.ToInt32(dtChkRec.Rows[0]["Amount"].ToString());
                                    BAccInfo.InsertAddBalance(CRID, bankID);
                                }
                            }
                            else
                            {
                                lblErrorMessage.Text += "Cheque Number : " + dtChkRec.Rows[0]["ChequeNumber"].ToString() + " is not Cleared yet.<br />";
                            }  
                        }
                                                                      
                    }
                }
                if (CheckingBit == false)
                {
                    dtBBal = BAccInfo.GetBankBalancesByBankID(bankID);
                    if (dtBBal.Rows.Count > 0)
                    {
                        BAccInfo.UpdateBankBalances(Convert.ToInt32(dtBBal.Rows[0]["ID"].ToString()), bankID, RecAmount);
                        lblErrorMessage.Text += "Cheque(s) amount balance added to Bank Account successfully.";
                        PopulateChequeReceived();
                    }
                    else
                    {
                        BAccInfo.InsertBankBalances(bankID, RecAmount);
                        lblErrorMessage.Text += "Cheque(s) amount balance added to Bank Account successfully.";
                        PopulateChequeReceived();
                    }
                }
                
            }
            else
            {
                lblErrorMessage.Text += "Bank not Found. Please add a bank first";
            }
        }
        else
        {
            lblErrorMessage.Text += "Please select the Bank First.";
        }
    }
    #endregion

    #region Methods

    private void PopulateChequeReceivedBySearch(string FieldName, string Value)
    {
        PCSN.InvoiceSystem.BusinessLogicLayer.ChequeReceived ChequeReceived = new PCSN.InvoiceSystem.BusinessLogicLayer.ChequeReceived();
        if (FieldName == "IsCleared")
        {
            dtChkRecSrch = ChequeReceived.GetAllChequeReceivedByCleared(Convert.ToInt16(Value));
        }
        if (FieldName == "ByClient")
        {
            dtChkRecSrch = ChequeReceived.GetAllChequeReceivedByClient(Convert.ToInt16(Value));
        }
        if (FieldName == "ByChequeNumber")
        {
            dtChkRecSrch = ChequeReceived.GetAllChequeReceivedByChequeNumber(Value.Trim());
        }
        if (FieldName == "ByRecDate")
        {
            dtChkRecSrch = ChequeReceived.GetAllChequeReceivedByRecDate(Value);
        }
        if (FieldName == "ToAndFromDate")
        {
            string[] ToandFrom = Value.Split('#');
            string toDate = "";
            string fromDate = "";
            for (int i = 0; i < ToandFrom.Length; i++)
            {
                toDate = ToandFrom[i].ToString();
                fromDate = ToandFrom[i + 1].ToString();
                break;
            }

            dtChkRecSrch = ChequeReceived.GetAllChequeReceivedByToAndFromDate(toDate.ToString(), fromDate.ToString());
        }
        if (FieldName == "ByMonthYear")
        {
            dtChkRecSrch = ChequeReceived.GetAllChequeReceivedByMonthYear(Value);
        }

        dgChequeReceiveds.DataSource = dtChkRecSrch;
        dgChequeReceiveds.DataBind();
    }


    private void ClearControls()
    {        
        txtFromDate.Text = "";
        txtToDate.Text = "";
        txtReceivedDate.Text = "";
        txtChequeNumber.Text = "";
        //cboChkRecSearch.SelectedValue = "Show All";
        cboClient.SelectedValue = "0";
        cboByYear.SelectedValue = "0";
        cboByMonth.SelectedValue = "0";
        cboBank.SelectedValue = "0";
    }
    
    private void PopulateClient()
    {
        PCSN.InvoiceSystem.BusinessLogicLayer.Client client = new PCSN.InvoiceSystem.BusinessLogicLayer.Client();
        dtChequeRecDG = client.GetClientForDropDown("ForEsp");
        cboClient.DataSource = dtChequeRecDG;
        cboClient.DataTextField = "Name";
        cboClient.DataValueField = "ID";

        cboClient.DataBind();
    }
    private void PopulateChequeReceived()
    {
        PCSN.InvoiceSystem.BusinessLogicLayer.ChequeReceived ChequeReceived = new PCSN.InvoiceSystem.BusinessLogicLayer.ChequeReceived();
        dtChequeRecDG = ChequeReceived.GetAllChequeReceived();
        dgChequeReceiveds.DataSource = dtChequeRecDG;
        dgChequeReceiveds.DataBind();
    }

    private void PopulateBanks()
    {
        PCSN.InvoiceSystem.BusinessLogicLayer.BankAccountInfo ChequeBook = new PCSN.InvoiceSystem.BusinessLogicLayer.BankAccountInfo();
        dtChequeRecDG = ChequeBook.GetBankAccountInfoForDropDown();
        cboBank.DataSource = dtChequeRecDG;
        cboBank.DataTextField = "Name";
        cboBank.DataValueField = "ID";

        cboBank.DataBind();
    }
        

    public string EditChequeReceived(string ChequeReceivedID)
    {
        return "ChequeReceived.aspx?ChequeReceivedIDED=" + ChequeReceivedID;
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("default.aspx");
    }    
    public string IsCleared(string IsCleared)
    {
        if (IsCleared == "True")
        {
            return "YES";
        }
        else
        {
            return "NO";
        }
    }
    #endregion
    
}
