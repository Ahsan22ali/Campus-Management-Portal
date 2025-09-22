using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.LinqData;

namespace WebApplication1.Admin
{
    public partial class ClassFees : System.Web.UI.Page
    {
        private CampusDataDataContext dc;

        protected void Page_Load(object sender, EventArgs e)
        {
            dc = new CampusDataDataContext(
                System.Configuration.ConfigurationManager.ConnectionStrings["CampusDataContext"].ConnectionString);

            if (!IsPostBack)
            {
                GetClass();
                GetFees();
            }
        }

        private void GetClass()
        {
            var classes = dc.Classes
                .Select(c => new { c.ClassId, c.ClassName })
                .ToList();

            ddlClass.DataSource = classes;
            ddlClass.DataTextField = "ClassName";
            ddlClass.DataValueField = "ClassId";
            ddlClass.DataBind();

            ddlClass.Items.Insert(0, new ListItem("Select Class", "0"));
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedClassId = Convert.ToInt32(ddlClass.SelectedValue);

                if (selectedClassId == 0)
                {
                    lblMsg.Text = "Please select a class.";
                    lblMsg.CssClass = "alert alert-danger";
                    return;
                }

                string className = ddlClass.SelectedItem.Text;

                var existingFee = dc.fees.FirstOrDefault(f => f.ClassId == selectedClassId);

                if (existingFee == null)
                {
                    
                    if (!int.TryParse(txtFeeAmounts.Text.Trim(), out int feeAmount))
                    {
                        lblMsg.Text = "Please enter a valid integer fee amount.";
                        lblMsg.CssClass = "alert alert-danger";
                        return;
                    }

                    fee newFee = new fee
                    {
                        ClassId = selectedClassId,
                        FeesAmount = feeAmount  
                    };

                    dc.fees.InsertOnSubmit(newFee);
                    dc.SubmitChanges();

                    lblMsg.Text = "Inserted Successfully!";
                    lblMsg.CssClass = "alert alert-success";

                    ddlClass.SelectedIndex = 0;
                    txtFeeAmounts.Text = string.Empty;
                    GetFees();
                }
                else
                {
                    lblMsg.Text = $"Entered fees already exists for <b>{className}</b>";
                    lblMsg.CssClass = "alert alert-danger";
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Error: " + ex.Message;
                lblMsg.CssClass = "alert alert-danger";
            }
        }

        private void GetFees()
        {
            var feesList = dc.fees
                .Join(dc.Classes,
                      f => f.ClassId,
                      c => c.ClassId,
                      (f, c) => new
                      {
                          f.FeesId,
                          f.ClassId,
                          c.ClassName,
                          FeesAmount = f.FeesAmount
                      })
                .ToList()
                .Select((x, index) => new
                {
                    Sr = index + 1,
                    x.FeesId,
                    x.ClassId,
                    x.ClassName,
                    x.FeesAmount
                }).ToList();

            GridView1.DataSource = feesList;
            GridView1.DataBind();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GetFees();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            GetFees();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int FeesId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);

                var feeToDelete = dc.fees.SingleOrDefault(f => f.FeesId == FeesId);
                if (feeToDelete != null)
                {
                    dc.fees.DeleteOnSubmit(feeToDelete);
                    dc.SubmitChanges();

                    lblMsg.Text = "Fees deleted successfully!";
                    lblMsg.CssClass = "alert alert-success";
                }

                GridView1.EditIndex = -1;
                GetFees();
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Error: " + ex.Message;
                lblMsg.CssClass = "alert alert-danger";
            }
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            GetFees();
        }


        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = GridView1.Rows[e.RowIndex];
                int FeesId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
                string feeAmtStr = (row.FindControl("TextBox1") as TextBox)?.Text.Trim();

                if (int.TryParse(feeAmtStr, out int feeAmt))
                {
                    var feeToUpdate = dc.fees.SingleOrDefault(f => f.FeesId == FeesId);
                    if (feeToUpdate != null)
                    {
                        feeToUpdate.FeesAmount = feeAmt; 
                        dc.SubmitChanges();

                        lblMsg.Text = "Fees updated successfully!";
                        lblMsg.CssClass = "alert alert-success";
                    }
                }
                else
                {
                    lblMsg.Text = "Invalid fee amount. Please enter an integer value.";
                    lblMsg.CssClass = "alert alert-danger";
                }

                GridView1.EditIndex = -1;
                GetFees();
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Error: " + ex.Message;
                lblMsg.CssClass = "alert alert-danger";
            }
        }
    }
}
