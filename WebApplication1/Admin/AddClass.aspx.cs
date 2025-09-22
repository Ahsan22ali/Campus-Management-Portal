using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.LinqData;

namespace WebApplication1.Admin
{
    public partial class AddClass : System.Web.UI.Page
    {
        
        private string GetConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["CampusDataContext"].ConnectionString;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetClass();
            }
        }

        private void GetClass()
        {
            using (CampusDataDataContext db = new CampusDataDataContext(GetConnectionString()))
            {
               
                var classList = db.Classes.ToList();

               
                var classesWithSrNo = classList.Select((c, index) => new
                {
                    SrNo = index + 1,
                    c.ClassId,
                    c.ClassName
                }).ToList();

                GridView1.DataSource = classesWithSrNo;
                GridView1.DataBind();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                using (CampusDataDataContext db = new CampusDataDataContext(GetConnectionString()))
                {
                    string className = txtClass.Text.Trim();

                    bool exists = db.Classes.Any(c => c.ClassName == className);

                    if (!exists)
                    {
                        Class newClass = new Class
                        {
                            ClassName = className
                        };

                        db.Classes.InsertOnSubmit(newClass);
                        db.SubmitChanges();

                        lblMsg.Text = "Inserted Successfully!";
                        lblMsg.CssClass = "alert alert-success";
                        txtClass.Text = string.Empty;

                        GetClass();
                    }
                    else
                    {
                        lblMsg.Text = "Entered class already exists";
                        lblMsg.CssClass = "alert alert-danger";
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GetClass();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            GetClass();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            GetClass();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                using (CampusDataDataContext db = new CampusDataDataContext(GetConnectionString()))
                {
                    GridViewRow row = GridView1.Rows[e.RowIndex];
                    int cId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
                    string className = (row.FindControl("txtClassEdit") as TextBox).Text.Trim();

                    var existingClass = db.Classes.FirstOrDefault(c => c.ClassId == cId);
                    if (existingClass != null)
                    {
                        existingClass.ClassName = className;
                        db.SubmitChanges();

                        lblMsg.Text = "Class Updated Successfully!";
                        lblMsg.CssClass = "alert alert-success";
                    }
                    GridView1.EditIndex = -1;
                    GetClass();
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
    }
}
