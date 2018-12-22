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

public partial class SearchDailyExpense : System.Web.UI.Page
{
    #region Variable Declaration
    private DataTable dtDailyExp = new DataTable();
    private int CntExpNumber = 1;
    private bool Checker = true;
    #endregion

    #region Event Handler

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!Page.IsPostBack)
            
    }

    

    private void dgDailyExps_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        if (e.CommandName == "DeleteDailyExp")
        {
            string argsID = e.CommandArgument.ToString();

            long DailyExpID = Convert.ToInt32(argsID.ToString());

            if (DailyExpID > 0)
            {
                PCSN.InvoiceSystem.BusinessLogicLayer.DailyExpMaster DailyExp = new PCSN.InvoiceSystem.BusinessLogicLayer.DailyExpMaster();
                //DailyExp.DeleteDailyExp(DailyExpID);
                lblErrorMessage.Text = "Cheque Deleted Successfuly.";

                
                PopulateDailyExp();
                ClearControls();
            }
        }
    }

    private void dgDailyExps_ItemCreated(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        LinkButton link = (LinkButton)e.Item.FindControl("Linkbutton2");
        if (link != null)
        {
            link.Attributes.Add("onClick", "javascript:return confirm('This action will delete the information saved for this Received Cheque.  Are you sure you want to delete this Cheque?');");
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
                PopulateDailyExpBySearch("ToAndFromDate", FromDate.ToString() + "#" + ToDate.ToString());
            }
            catch
            {
                error += "To and from Dates must be valid. <br />";
            }
        }
        else if (txtByDate.Text != "")
        {
            try
            {
                txtByDate.Text = Convert.ToDateTime(txtByDate.Text.ToString()).ToShortDateString();
                PopulateDailyExpBySearch("ByDate", txtByDate.Text.ToString());
            }
            catch
            {
                error += "Date is not Valid. <br />";
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

                PopulateDailyExpBySearch("ByMonthYear", dtimeSearch.ToString());
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
        txtFromDate.Text = "";
        txtByDate.Text = "";
        txtToDate.Text = "";        
    }
    private void PopulateDailyExpBySearch(string FieldName, string Value)
    {
        PCSN.InvoiceSystem.BusinessLogicLayer.DailyExpMaster DailyExp = new PCSN.InvoiceSystem.BusinessLogicLayer.DailyExpMaster();
        
        if (FieldName == "ByDate")
        {
            dtDailyExp = DailyExp.GetDailyExpMasterByExpDate(Value);
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

            dtDailyExp = DailyExp.GetDailyExpMasterByToandFromDate(toDate.ToString(), fromDate.ToString());
        }
        if (FieldName == "ByMonthYear")
        {
            dtDailyExp = DailyExp.GetDailyExpMasterByMonthYear(Value);
        }
        
        lblCashInHand.Text = "0";
        lblTotalExpense.Text = "0";

        if (dtDailyExp.Rows.Count > 0)
        {
            for (int i = 0; i < dtDailyExp.Rows.Count; i++)
            {
                try
                {
                    if (dtDailyExp.Rows[i]["ID"].ToString() == dtDailyExp.Rows[i+1]["ID"].ToString())
                    {
                        //lblCashInHand.Text = Convert.ToString(Convert.ToInt32(lblCashInHand.Text) + Convert.ToInt32(dtDailyExp.Rows[i]["StartCash"].ToString()));
                        //lblTotalExpense.Text = Convert.ToString(Convert.ToInt32(lblTotalExpense.Text) + Convert.ToInt32(dtDailyExp.Rows[i]["TotalAmount"].ToString()));
                    }
                    else
                    {
                        lblCashInHand.Text = Convert.ToString(Convert.ToInt32(lblCashInHand.Text) + Convert.ToInt32(dtDailyExp.Rows[i]["StartCash"].ToString()));
                        lblTotalExpense.Text = Convert.ToString(Convert.ToInt32(lblTotalExpense.Text) + Convert.ToInt32(dtDailyExp.Rows[i]["TotalAmount"].ToString()));
                    }
                }
                catch
                {
                    lblCashInHand.Text = Convert.ToString(Convert.ToInt32(lblCashInHand.Text) + Convert.ToInt32(dtDailyExp.Rows[i]["StartCash"].ToString()));
                    lblTotalExpense.Text = Convert.ToString(Convert.ToInt32(lblTotalExpense.Text) + Convert.ToInt32(dtDailyExp.Rows[i]["TotalAmount"].ToString()));
                }
            }
        }
        dgDailyExps.DataSource = dtDailyExp;
        dgDailyExps.DataBind();
        CntExpNumber = 1;
    }

    private void PopulateDailyExp()
    {
        PCSN.InvoiceSystem.BusinessLogicLayer.DailyExpMaster DailyExp = new PCSN.InvoiceSystem.BusinessLogicLayer.DailyExpMaster();
        dtDailyExp = DailyExp.GetAllDailyExpMaster();
        dgDailyExps.DataSource = dtDailyExp;
        dgDailyExps.DataBind();
    }
      


    public string EditDailyExp(string DailyExpID, string DailyExpEdID)
    {
        return "DailyExp.aspx?DailyExpMasterID=" + DailyExpID + "&DailyExpDetailID=" + DailyExpEdID;
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("default.aspx");
    }
    public string DeleteDailyExp(string DailyExpID)
    {
        return DailyExpID;
    }
    public string SNum(string Counter)
    {          
        return Convert.ToString(CntExpNumber++);
    }

    public string ShowDailyExp(string DailyExpID)
    {
        return "ViewDailyExpense.aspx?ExpenseMasterID=" + DailyExpID;
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
        //this.dgDailyExps.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgDailyExps_PageIndexChanged);
        this.dgDailyExps.ItemCreated += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgDailyExps_ItemCreated);
        this.dgDailyExps.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgDailyExps_ItemCommand);

    }
    #endregion
}
