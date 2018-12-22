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

public partial class SearchChequeReceived : System.Web.UI.Page
{
    #region Variable Declaration
    private DataTable dtChkRecSrch = new DataTable();
    #endregion

    #region Event Handler

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        PopulateClient();
    }

    protected void cboChkRecSearch_SelectedIndexChanged1(object sender, EventArgs e)
    {
        if (cboChkRecSearch.SelectedValue == "Show All")
        {
            PopulateChequeReceived();
        }
        else
        {
            PopulateChequeReceivedBySearch("IsCleared", cboChkRecSearch.SelectedValue.ToString());
        }
    }

    private void dgChequeReceiveds_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        if (e.CommandName == "DeleteChequeReceived")
        {
            string argsID = e.CommandArgument.ToString();

            long ChequeReceivedID = Convert.ToInt32(argsID.ToString());

            if (ChequeReceivedID > 0)
            {
                PCSN.InvoiceSystem.BusinessLogicLayer.ChequeReceived ChequeReceived = new PCSN.InvoiceSystem.BusinessLogicLayer.ChequeReceived();
                ChequeReceived.DeleteChequeReceived(ChequeReceivedID);
                lblErrorMessage.Text = "Cheque Deleted Successfuly.";

                PopulateClient();
                PopulateChequeReceived();
                ClearControls();
            }
        }
    }

    private void dgChequeReceiveds_ItemCreated(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        LinkButton link = (LinkButton)e.Item.FindControl("Linkbutton2");
        if (link != null)
        {
            link.Attributes.Add("onClick", "javascript:return confirm('This action will delete the information saved for this Received Cheque.  Are you sure you want to delete this Cheque?');");
        }

        HyperLink link2 = (HyperLink)e.Item.FindControl("Linkbutton3");
        if (link2 != null)
        {
            link2.Attributes.Add("onClick", "javascript:return confirm('This action will mark this cheque as Cleared.  Are you sure you want to Clear this Cheque?');");
            if (link2.NavigateUrl == "Already Cleared")
            {
                link2.BackColor = System.Drawing.Color.Red;
            }
        }


    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
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


    #endregion

    #region Methods

    private void ClearControls()
    {
        txtChequeNumber.Text = "";
        txtFromDate.Text = "";
        txtReceivedDate.Text = "";
        txtToDate.Text = "";
        cboChkRecSearch.SelectedValue="Show All";
        cboClient.SelectedValue = "0";
    }
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

    private void PopulateChequeReceived()
    {
        PCSN.InvoiceSystem.BusinessLogicLayer.ChequeReceived ChequeReceived = new PCSN.InvoiceSystem.BusinessLogicLayer.ChequeReceived();
        dtChkRecSrch = ChequeReceived.GetAllChequeReceived();
        dgChequeReceiveds.DataSource = dtChkRecSrch;
        dgChequeReceiveds.DataBind();
    }

    private void PopulateClient()
    {
        PCSN.InvoiceSystem.BusinessLogicLayer.Client client = new PCSN.InvoiceSystem.BusinessLogicLayer.Client();
        dtChkRecSrch = client.GetClientForDropDown("ForEsp");
        cboClient.DataSource = dtChkRecSrch;
        cboClient.DataTextField = "Name";
        cboClient.DataValueField = "ID";

        cboClient.DataBind();
    }



    public string EditChequeReceived(string ChequeReceivedID)
    {
        return "ChequeReceived.aspx?ChequeReceivedIDED=" + ChequeReceivedID;
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("default.aspx");
    }
    public string DeleteChequeReceived(string ChequeReceivedID)
    {
        return ChequeReceivedID;
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
    public string ChequeCleared(string ChequeReceivedID, string IsCleared)
    {
        if (IsCleared == "True")
        {
            return "#";
        }
        else
        {
            return "ChequeReceived.aspx?ChequeReceivedIDED=" + ChequeReceivedID + "&MkCl=True";
        }
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
        //this.dgChequeReceiveds.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgChequeReceiveds_PageIndexChanged);
        this.dgChequeReceiveds.ItemCreated += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgChequeReceiveds_ItemCreated);
        this.dgChequeReceiveds.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgChequeReceiveds_ItemCommand);

    }
    #endregion
}
