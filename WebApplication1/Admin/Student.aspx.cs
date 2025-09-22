using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.LinqData;
using LinqStudent = WebApplication1.LinqData.Student;

namespace WebApplication1.Admin
{
    public partial class Student : System.Web.UI.Page
    {
        
        CampusDataDataContext db = new CampusDataDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["CampusDataContext"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetClass();
            }
        }

        private void GetClass()
        {
            var classes = db.Classes.ToList();
            ddlClass.DataSource = classes;
            ddlClass.DataTextField = "ClassName";
            ddlClass.DataValueField = "ClassId";
            ddlClass.DataBind();

            var data = (from s in db.Students
                        join c in db.Classes on s.ClassId equals c.ClassId
                        select new
                        {
                            s.StudentId,
                            s.Name,
                            s.DOB,
                            s.Gender,
                            s.Mobile,
                            s.RollNO,   
                            s.Address,
                            ClassName = c.ClassName
                        }).ToList();

            GridView1.DataSource = data;
            GridView1.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedClassId = int.Parse(ddlClass.SelectedValue);

                string lastRollNoStr = db.Students
                    .Where(s => s.ClassId == selectedClassId)
                    .OrderByDescending(s => s.StudentId)
                    .Select(s => s.RollNO)
                    .FirstOrDefault();

                int nextRollNumber = 1;
                if (!string.IsNullOrEmpty(lastRollNoStr) && int.TryParse(lastRollNoStr, out int lastRollNoInt))
                {
                    nextRollNumber = lastRollNoInt + 1;
                }

                // Convert Mobile to long?
                long? mobileNumber = null;
                if (long.TryParse(txtMobile.Text.Trim(), out long parsedMobile))
                {
                    mobileNumber = parsedMobile;
                }
                else
                {
                    lblMsg.Text = "Invalid mobile number format.";
                    return;
                }

                LinqStudent newStudent = new LinqStudent
                {
                    Name = txtName.Text.Trim(),
                    DOB = DateTime.Parse(txtDoB.Text),
                    Gender = ddlGender.SelectedValue,
                    Mobile = mobileNumber,     
                    RollNO = nextRollNumber.ToString(),
                    Address = txtAddress.Text.Trim(),
                    ClassId = selectedClassId
                };

                db.Students.InsertOnSubmit(newStudent);
                db.SubmitChanges();

                lblMsg.Text = "Student added successfully!";
                GetClass();
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Error: " + ex.Message;
            }
        }



        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            GetClass();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            GetClass();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int studentId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
                GridViewRow row = GridView1.Rows[e.RowIndex];

                string name = ((TextBox)row.FindControl("txtName")).Text;
                string mobileStr = ((TextBox)row.FindControl("txtMobile")).Text;

                LinqStudent studentToUpdate = db.Students.FirstOrDefault(s => s.StudentId == studentId);
                if (studentToUpdate != null)
                {
                    studentToUpdate.Name = name;

                    if (long.TryParse(mobileStr.Trim(), out long mobileNum))
                    {
                        studentToUpdate.Mobile = mobileNum;
                    }
                    else
                    {
                        lblMsg.Text = "Invalid mobile number format.";
                        return; 
                    }

                    db.SubmitChanges();
                    lblMsg.Text = "Student updated successfully!";
                }

                GridView1.EditIndex = -1;
                GetClass();
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Error: " + ex.Message;
            }
        }


        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GetClass();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int index = e.Row.RowIndex + 1 + (GridView1.PageIndex * GridView1.PageSize);
                e.Row.Cells[0].Text = index.ToString();
            }
        }
    }
}
