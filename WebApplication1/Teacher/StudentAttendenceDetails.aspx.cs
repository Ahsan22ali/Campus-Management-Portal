using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.LinqData;  // Your LINQ to SQL namespace

namespace WebApplication1.Teacher
{
    public partial class StudentAttendenceDetails : System.Web.UI.Page
    {
        CampusDataDataContext db = new CampusDataDataContext(
            System.Configuration.ConfigurationManager.ConnectionStrings["CampusDataContext"].ConnectionString
        );

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadClass();
            }
        }

        private void LoadClass()
        {
            var classes = db.Classes.ToList();
            ddlClass.DataSource = classes;
            ddlClass.DataTextField = "ClassName";
            ddlClass.DataValueField = "ClassId";
            ddlClass.DataBind();
            ddlClass.Items.Insert(0, new ListItem("Select Class", ""));
        }

        protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.TryParse(ddlClass.SelectedValue, out int classId))
            {
                var subjects = db.Subjects.Where(s => s.ClassId == classId).ToList();
                ddlSubject.DataSource = subjects;
                ddlSubject.DataTextField = "SubjectName";
                ddlSubject.DataValueField = "SubjectId";
                ddlSubject.DataBind();
                ddlSubject.Items.Insert(0, new ListItem("Select Subject", ""));
            }
            else
            {
                ddlSubject.Items.Clear();
                ddlSubject.Items.Insert(0, new ListItem("Select Subject", ""));
            }
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            if (int.TryParse(ddlClass.SelectedValue, out int classId))
            {
                var students = db.Students
                                 .Where(st => st.ClassId == classId)
                                 .Select(st => new { st.Name, st.RollNO })
                                 .ToList();

                GridView1.DataSource = students;
                GridView1.DataBind();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(ddlClass.SelectedValue, out int classId) ||
                !int.TryParse(ddlSubject.SelectedValue, out int subjectId))
            {
                lblMsg.Text = "Please select valid class and subject.";
                lblMsg.CssClass = "text-danger";
                return;
            }

            DateTime date = DateTime.Now.Date;

            foreach (GridViewRow row in GridView1.Rows)
            {
                // Assuming first cell contains RollNo
                string rollNo = row.Cells[1].Text.Trim(); // Usually first cell (0) might be serial or something else, adjust if needed
                RadioButtonList rbl = (RadioButtonList)row.FindControl("rblStatus");

                if (rbl != null)
                {
                    bool status = rbl.SelectedValue == "1";

                    // Check if attendance already exists for this student on this date, for idempotency (optional)
                    var existing = db.StudentAttendences
                                     .FirstOrDefault(a => a.ClassId == classId
                                                       && a.SubjectId == subjectId
                                                       && a.RollNo == rollNo
                                                       && a.Date == date);

                    if (existing == null)
                    {
                        StudentAttendence attendance = new StudentAttendence
                        {
                            ClassId = classId,
                            SubjectId = subjectId,
                            RollNo = rollNo,
                            Status = status,
                            Date = date
                        };

                        db.StudentAttendences.InsertOnSubmit(attendance);
                    }
                    else
                    {
                        // Optionally update existing record
                        existing.Status = status;
                    }
                }
            }

            db.SubmitChanges();

            lblMsg.Text = "Attendance marked successfully!";
            lblMsg.CssClass = "text-success";

            GridView1.DataSource = null;
            GridView1.DataBind();
        }
    }
}
