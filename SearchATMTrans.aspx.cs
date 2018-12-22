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

public partial class SearchATMTrans : System.Web.UI.Page
{
    #region Variables
    //private long ATMTransID = 0;
    DataTable dtATMTransDG = new DataTable();
    #endregion

    # region Event Handler
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.QueryString["ATMTransIDED"] != null && Request.QueryString["ATMTransIDED"].ToString() != "" && !Page.IsPostBack)
        {
            PCSN.InvoiceSystem.BusinessLogicLayer.ATMCard ATMTrans = new PCSN.InvoiceSystem.BusinessLogicLayer.ATMCard();
            DataTable dtATMTransDet = new DataTable();
            dtATMTransDet = ATMTrans.GetATMTransByID(Convert.ToInt32(Request.QueryString["ATMTransIDED"].ToString()));

            

        }
        if (!Page.IsPostBack)
        {
            PopulateATMCard();
            PopulateATMTrans();            
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        PopulateATMCard();
        PopulateATMTrans();        
        ClearControls();
    }
    protected void cboATMCard_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cboATMCard.SelectedValue != "0")
        {
            PCSN.InvoiceSystem.BusinessLogicLayer.ATMCard ATMTrans = new PCSN.InvoiceSystem.BusinessLogicLayer.ATMCard();
            dtATMTransDG = ATMTrans.GetATMCardByID(Convert.ToInt32(cboATMCard.SelectedValue));
            if (dtATMTransDG.Rows.Count > 0)
            {
                long BankID = Convert.ToInt32(dtATMTransDG.Rows[0]["BankAccID"].ToString());
                if (BankID > 0)
                {
                    PCSN.InvoiceSystem.BusinessLogicLayer.BankAccountInfo Bank = new PCSN.InvoiceSystem.BusinessLogicLayer.BankAccountInfo();
                    dtATMTransDG = Bank.GetBankBalancesByBankID(BankID);
                    if (dtATMTransDG.Rows.Count > 0)
                    {
                        txtBankID.Text = dtATMTransDG.Rows[0]["BankID"].ToString();
                        txtBBalID.Text = dtATMTransDG.Rows[0]["ID"].ToString();
                        txtAvBalance.Text = dtATMTransDG.Rows[0]["AvailableBalance"].ToString();
                    }
                    else
                    {
                        txtAvBalance.Text = "0";
                    }
                }
            }
            SqlDataSource1.DataBind();
            GridView1.DataBind();
        }
        else
        {
            txtAvBalance.Text = "0";
        }
    }

    protected void GridView1_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            ((CheckBox)e.Row.FindControl("cbSelectAll")).Attributes.Add("onclick", "javascript:SelectAll('" + ((CheckBox)e.Row.FindControl("cbSelectAll")).ClientID + "')");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView rowView = (DataRowView)e.Row.DataItem;

            LinkButton linkButton = (LinkButton)e.Row.Cells[10].Controls[0];
            linkButton.Attributes["onClick"] = "javascript:return confirm ( 'Are you sure you want to Delete this Transaction?' )";
        }
        RePopulateValues();
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        RememberOldValues();
        GridView1.PageIndex = e.NewPageIndex;
        SqlDataSource1.DataBind();
        GridView1.DataBind();
    }

    protected void GridView1_OnRowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Visible = false;
        }
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        try
        {
            int index = e.RowIndex;

            long ATMTransID = Convert.ToInt32(GridView1.Rows[index].Cells[0].Text);
            if (ATMTransID>0)
            { }
            else
            {
                if (ATMTransID > 0)
                {
                    PCSN.InvoiceSystem.BusinessLogicLayer.ATMCard ATMTrans = new PCSN.InvoiceSystem.BusinessLogicLayer.ATMCard();
                    // Add amount of ATM transaction into Bank Balance
                    if (ATMTransID > 0)
                    {
                        dtATMTransDG = ATMTrans.GetATMTransByID(ATMTransID);
                        if (dtATMTransDG.Rows.Count > 0)
                        {
                            txtAmount.Text = dtATMTransDG.Rows[0]["Amount"].ToString();
                            // Add Amount of a cheque into Balance
                            PCSN.InvoiceSystem.BusinessLogicLayer.BankAccountInfo BBal = new PCSN.InvoiceSystem.BusinessLogicLayer.BankAccountInfo();

                            dtATMTransDG = BBal.GetBankBalancesByBankID(Convert.ToInt32(dtATMTransDG.Rows[0]["BankID"].ToString()));

                            if (dtATMTransDG.Rows.Count > 0)
                            {
                                txtBankID.Text = dtATMTransDG.Rows[0]["BankID"].ToString();
                                txtBBalID.Text = dtATMTransDG.Rows[0]["ID"].ToString();
                                txtAvBalance.Text = dtATMTransDG.Rows[0]["AvailableBalance"].ToString();

                                try
                                {
                                    txtAvBalance.Text = Convert.ToInt32(txtAvBalance.Text.ToString()).ToString();
                                }
                                catch
                                {
                                    txtAvBalance.Text = "0";
                                    //lblSpErrMsg.Text = "Please add some Capital to your account in order to issue a Cheque.<br />OR Please insert a received cheque in this account. Thanks";
                                }

                            }
                            // Increasing Bank Balance
                            long availBal = Convert.ToInt32(txtAvBalance.Text) + Convert.ToInt32(txtAmount.Text);
                            BBal.UpdateBankBalances(Convert.ToInt32(txtBBalID.Text), Convert.ToInt32(txtBankID.Text), availBal);
                            // Actuall Deletion of transaction
                            ATMTrans.DeleteATMTrans(ATMTransID);
                            //=====================================
                            lblErrorMessage.Text = "Transaction Deleted Successfuly.";                            
                            PopulateATMCard();
                            ClearControls();
                        }
                    }
                    //====================================


                }
                SqlDataSource1.DataBind();
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    #endregion

    #region Methods

    private void RememberOldValues()
    {
        ArrayList categoryIDList = new ArrayList();
        int index = -1;
        foreach (GridViewRow row in GridView1.Rows)
        {
            index = (int)GridView1.DataKeys[row.RowIndex].Value;
            bool result = ((CheckBox)row.FindControl("CheckBox1")).Checked;

            // Check in the Session
            if (Session["CHECKED_ITEMS"] != null)
                categoryIDList = (ArrayList)Session["CHECKED_ITEMS"];
            if (result)
            {
                if (!categoryIDList.Contains(index))
                    categoryIDList.Add(index);
            }
            else
                categoryIDList.Remove(index);
        }
        if (categoryIDList != null && categoryIDList.Count > 0)
            Session["CHECKED_ITEMS"] = categoryIDList;
    }

    private void RePopulateValues()
    {
        ArrayList categoryIDList = (ArrayList)Session["CHECKED_ITEMS"];
        if (categoryIDList != null && categoryIDList.Count > 0)
        {
            foreach (GridViewRow row in GridView1.Rows)
            {
                int index = (int)GridView1.DataKeys[row.RowIndex].Value;
                if (categoryIDList.Contains(index))
                {
                    CheckBox myCheckBox = (CheckBox)row.FindControl("CheckBox1");
                    myCheckBox.Checked = true;
                }
            }
        }
    }

    private bool ValidateFields()
    {

        bool error = false;
        string message = "";

        if (cboATMCard.SelectedValue == "0")
        {
            message += "ATM Card is not specified.<br>";
            error = true;
        }

        



        lblErrorMessage.Text = "";
        if (error)
            lblErrorMessage.Text = message;

        return !error;
    }

    private void PopulateATMTrans()
    {
        PCSN.InvoiceSystem.BusinessLogicLayer.ATMCard ATMTrans = new PCSN.InvoiceSystem.BusinessLogicLayer.ATMCard();
        dtATMTransDG = ATMTrans.GetAllATMTrans();        
    }


    private void PopulateATMCard()
    {
        PCSN.InvoiceSystem.BusinessLogicLayer.ATMCard ATM = new PCSN.InvoiceSystem.BusinessLogicLayer.ATMCard();
        dtATMTransDG = ATM.GetATMCardForDropDown();
        cboATMCard.DataSource = dtATMTransDG;
        cboATMCard.DataTextField = "Name";
        cboATMCard.DataValueField = "ID";

        cboATMCard.DataBind();
    }

    public string EditATMTrans(string ATMTransID)
    {
        return "ATMTrans.aspx?ATMTransIDED=" + ATMTransID;
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("default.aspx");
    }
    public string DeleteATMTrans(string ATMTransID)
    {
        return ATMTransID;
    }
    public void ClearControls()
    {
       
        txtBankID.Text = "";
        txtBBalID.Text = "";
        txtAvBalance.Text = "0";
        
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
        
    }
    #endregion



    protected void btncancelOrder_Click(object sender, EventArgs e)
    {
        Response.Redirect("default.aspx");
    }
}
