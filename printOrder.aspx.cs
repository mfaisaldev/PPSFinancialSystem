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


public partial class printOrder : System.Web.UI.Page
{
    public string GenerateHTML = "";
    DataTable dtOrderEdit = new DataTable();
    DataTable dtOrderDetailMore = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["OrderMasterID"] != null && Request.QueryString["OrderMasterID"].ToString() != "")
        {
            // This is for Showing Full Invoice
            long OrdermasterID = Convert.ToInt32(Request.QueryString["OrderMasterID"].ToString());
            //long OrderDetailID = 0;
            PCSN.InvoiceSystem.BusinessLogicLayer.Order Order = new PCSN.InvoiceSystem.BusinessLogicLayer.Order();
            if (Request.QueryString["Title"] == null || Request.QueryString["Title"].ToString() == "")
            {
                lblMainTitle.Text = "Order Slip";
            }
            else
            {
                lblMainTitle.Text = Request.QueryString["Title"].ToString();
            }
            lblDetailTitle.Text = "Order Details";
            dtOrderEdit = Order.GetOrderByID(OrdermasterID);
            if (dtOrderEdit.Rows.Count > 0)
            {
                lblOrderDate.Text = Convert.ToDateTime(dtOrderEdit.Rows[0]["OrderDate"].ToString()).ToShortDateString();
                lblOrderNumber.Text = dtOrderEdit.Rows[0]["OrderNumber"].ToString();
                try
                {
                    lblDueDate.Text = Convert.ToDateTime(dtOrderEdit.Rows[0]["DueDate"].ToString()).ToShortDateString();
                }
                catch
                {
                    lblDueDate.Text = "";
                }


                lblClientName.Text = dtOrderEdit.Rows[0]["ClientName"].ToString() + Environment.NewLine;
                lblClientCompanyName.Text = dtOrderEdit.Rows[0]["ClientCompanyName"].ToString() + Environment.NewLine;
                lblClientAddress.Text = dtOrderEdit.Rows[0]["ClientCompanyAddress"].ToString() + Environment.NewLine;

                PCSN.InvoiceSystem.BusinessLogicLayer.OurCompany ourCompany = new PCSN.InvoiceSystem.BusinessLogicLayer.OurCompany();
                DataTable dtOurCompany = new DataTable();
                dtOurCompany = ourCompany.GetOurCompanyByID(Convert.ToInt32("1"));

                if (dtOurCompany.Rows.Count > 0)
                {
                    //txtOurCompanyID.Text = dtOurCompany.Rows[0]["ID"].ToString();
                    lblCompanyName.Text = dtOurCompany.Rows[0]["CompanyName"].ToString() + Environment.NewLine;
                    lblAddress.Text = dtOurCompany.Rows[0]["CompanyAddress"].ToString() + Environment.NewLine;
                }
                else
                {
                    lblCompanyName.Text = "Power Protection System";
                }

                for (int a = 0; a < dtOrderEdit.Rows.Count; a++)
                {

                    GenerateHTML += "<tr>" + Environment.NewLine;

                    GenerateHTML += "<td>" + Environment.NewLine;
                    //GenerateHTML += dtOrderEdit.Rows[a]["Item"].ToString() + Environment.NewLine;
                    GenerateHTML += "</td>" + Environment.NewLine;

                    GenerateHTML += "<td>" + Environment.NewLine;
                    GenerateHTML += dtOrderEdit.Rows[a]["Item"].ToString() + Environment.NewLine;
                    GenerateHTML += "</td>" + Environment.NewLine;



                    GenerateHTML += "<td align=\"justify\">" + Environment.NewLine;
                    GenerateHTML += dtOrderEdit.Rows[a]["Description"].ToString() + Environment.NewLine;
                    GenerateHTML += "</td>" + Environment.NewLine;

                    GenerateHTML += "<td id=\"quantity\">" + Environment.NewLine;
                    GenerateHTML += dtOrderEdit.Rows[a]["Quantity"].ToString() + Environment.NewLine;
                    GenerateHTML += "</td>" + Environment.NewLine;



                    GenerateHTML += "<td id=\"rate\">" + Environment.NewLine;
                    GenerateHTML += dtOrderEdit.Rows[a]["UnitPrice"].ToString() + Environment.NewLine;
                    GenerateHTML += "</td>" + Environment.NewLine;



                    GenerateHTML += "<td id=\"amount\">" + Environment.NewLine;
                    GenerateHTML += dtOrderEdit.Rows[a]["ItemAmount"].ToString() + Environment.NewLine;
                    GenerateHTML += "</td>" + Environment.NewLine;


                    GenerateHTML += "</tr>" + Environment.NewLine;

                }
                lblGrandTotdal.Text = dtOrderEdit.Rows[0]["TotalAmount"].ToString();
            }

        }
    }
    
}
