using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.LinqData; // Adjust namespace for your LINQ DataContext

namespace WebApplication1.Admin
{
    public partial class StudAttendanceDetails : System.Web.UI.Page
    {
        private CampusDataDataContext dc;

        protected void Page_Load(object sender, EventArgs e)
        {
            dc = new CampusDataDataContext(
                System.Configuration.ConfigurationManager.ConnectionStrings["CampusDataContext"].ConnectionString);

            if (!IsPostBack)
            {
                LoadClass();
            }
        }

        private void LoadClass()
        {
            var classes = dc.Classes
                .Select(c => new { c.ClassId, c.ClassName })
                .ToList();

            ddlClass.DataSource = classes;
            ddlClass.DataTextField = "ClassName";
            ddlClass.DataValueField = "ClassId";
            ddlClass.DataBind();

            ddlClass.Items.Insert(0, new ListItem("Select Class", ""));
        }

        protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            int classId;
            if (int.TryParse(ddlClass.SelectedValue, out classId) && classId > 0)
            {
                var subjects = dc.Subjects
                    .Where(s => s.ClassId == classId)
                    .Select(s => new { s.SubjectId, s.SubjectName })
                    .ToList();

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
            int classId;
            if (int.TryParse(ddlClass.SelectedValue, out classId) && classId > 0)
            {
                var students = dc.Students
                    .Where(s => s.ClassId == classId)
                    .Select(s => new { s.Name, s.RollNO })
                    .ToList();

                GridView1.DataSource = students;
                GridView1.DataBind();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int classId, subjectId;
            if (int.TryParse(ddlClass.SelectedValue, out classId) &&
                int.TryParse(ddlSubject.SelectedValue, out subjectId))
            {
                string date = DateTime.Now.ToString("yyyy-MM-dd");

                foreach (GridViewRow row in GridView1.Rows)
                {
                    string rollNo = (row.Cells[1].Text).Trim(); 
                    RadioButtonList rbl = (RadioButtonList)row.FindControl("rblStatus");
                    bool status = rbl != null && rbl.SelectedValue == "1";

                    StudentAttendence attendance = new StudentAttendence
                    {
                        ClassId = classId,
                        SubjectId = subjectId,
                        RollNo = rollNo,
                        Status = status,
                        Date = DateTime.Parse(date)
                    };

                    dc.StudentAttendences.InsertOnSubmit(attendance);
                }

                dc.SubmitChanges();

                lblMsg.Text = "Attendance marked successfully!";
                lblMsg.CssClass = "text-success";

                GridView1.DataSource = null;
                GridView1.DataBind();
            }
            else
            {
                lblMsg.Text = "Please select class and subject.";
                lblMsg.CssClass = "text-danger";
            }
        }
    }
}
