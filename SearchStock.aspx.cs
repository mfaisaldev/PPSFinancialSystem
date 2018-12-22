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
using System.Data.SqlClient;
using System.IO;

public partial class SearchStock : System.Web.UI.Page
{
    #region Variable Declaration
    public bool btnAddWasClicked = false;
    public int btnPressedValue = 0;
    private DataTable dtStockDG = new DataTable();
    private DataTable dtStockEdit = new DataTable();
    private DataTable dtStock = new DataTable();   
    #endregion

    #region Events Handler
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void GridView1_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            ((CheckBox)e.Row.FindControl("cbSelectAll")).Attributes.Add("onclick", "javascript:SelectAll('" + ((CheckBox)e.Row.FindControl("cbSelectAll")).ClientID + "')");
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
    #endregion
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (txtItemCode.Text != "")
        {
            if (chkSize.Checked != true)
            {
                txtSearch.Text = "[Stockmaster].[ItemCode]='" + txtItemCode.Text.ToString() + "'";
            }
            else
            {
                txtSearch.Text = "[Stockmaster].[ItemCode]='" + txtItemCode.Text.ToString() + "' AND [StockDetail].[ItemSize] = '" + txtItemSize.Text + "'";
            }
            SqlDataSource1.DataBind();
            GridView1.DataBind();
            //txtSearch.Text = "";
        }
        if (txtItemName.Text != "")
        {
            if (chkSize.Checked != true)
            {
                txtSearch.Text = "[Stockmaster].[ItemName]='" + txtItemName.Text.ToString() + "'";
            }
            else
            {
                txtSearch.Text = "[Stockmaster].[ItemName]='" + txtItemName.Text.ToString() + "' AND [StockDetail].[ItemSize] = '" + txtItemSize.Text + "'";
            }
            SqlDataSource1.DataBind();
            GridView1.DataBind();
            //txtSearch.Text = "";
        }
        if (txtMadeBy.Text != "")
        {
            txtSearch.Text = "[Stockdetail].[Make]='" + txtMadeBy.Text.ToString() + "'";
            SqlDataSource1.DataBind();
            GridView1.DataBind();
        }
        if (txtDate.Text != "")
        {
            txtSearch.Text = "[Stockmaster].[IDate]='" + txtDate.Text.ToString() + "'";
            SqlDataSource1.DataBind();
            GridView1.DataBind();
        }
        if (ChkQtyLessTen.Checked == true)
        {
            txtSearch.Text = "[Stockdetail].[Quantity]< 10";
            SqlDataSource1.DataBind();
            GridView1.DataBind();
        }
        if (ChkQtyLessFive.Checked == true)
        {
            txtSearch.Text = "[Stockdetail].[Quantity]< 5";
            SqlDataSource1.DataBind();
            GridView1.DataBind();
        }
        
    }
    
}
