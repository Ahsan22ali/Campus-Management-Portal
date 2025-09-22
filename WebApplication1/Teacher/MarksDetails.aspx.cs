using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.LinqData; 

namespace WebApplication1.Teacher
{
    public partial class MarksDetails : System.Web.UI.Page
    {
        CampusDataDataContext db = new CampusDataDataContext(
            System.Configuration.ConfigurationManager.ConnectionStrings["CampusDataContext"].ConnectionString
        );

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetClass();
                GetStudents();
                GetMarks();
            }
        }

        private void GetClass()
        {
            var classes = db.Classes.ToList();
            ddlClass.DataSource = classes;
            ddlClass.DataTextField = "ClassName";
            ddlClass.DataValueField = "ClassId";
            ddlClass.DataBind();
            ddlClass.Items.Insert(0, new ListItem("Select Class", ""));
        }

        private void GetSubjects(string classId)
        {
            if (int.TryParse(classId, out int cId))
            {
                var subjects = db.Subjects.Where(s => s.ClassId == cId).ToList();
                ddlSubject.DataSource = subjects;
                ddlSubject.DataTextField = "SubjectName";
                ddlSubject.DataValueField = "SubjectName";  
                ddlSubject.DataBind();
                ddlSubject.Items.Insert(0, new ListItem("Select Subject", ""));
            }
            else
            {
                ddlSubject.Items.Clear();
                ddlSubject.Items.Insert(0, new ListItem("Select Subject", ""));
            }
        }

        private void GetStudents()
        {
            var students = db.Students.Select(s => s.RollNO).Distinct().ToList();
            ddlStudent.DataSource = students;
            ddlStudent.DataBind();
            ddlStudent.Items.Insert(0, new ListItem("Select Student", ""));
        }

        protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetSubjects(ddlClass.SelectedValue);
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string classId = ddlClass.SelectedValue;
            string subjectName = ddlSubject.SelectedValue;  
            string rollNo = ddlStudent.SelectedValue;
            string totalMarksStr = txtTotalMarks.Text;
            string outOfMarksStr = txtOutOfMarks.Text;

            if (string.IsNullOrEmpty(classId) || string.IsNullOrEmpty(subjectName) || string.IsNullOrEmpty(rollNo))
            {
                lblMsg.Text = "Please select class, subject, and student.";
                lblMsg.CssClass = "text-danger";
                return;
            }

            if (!int.TryParse(classId, out int cId) ||
                !int.TryParse(totalMarksStr, out int totalMarks) ||
                !int.TryParse(outOfMarksStr, out int outOfMarks))
            {
                lblMsg.Text = "Invalid numeric input.";
                lblMsg.CssClass = "text-danger";
                return;
            }

            Exam exam = new Exam
            {
                ClassId = cId,
                  RollNo = rollNo,
                TotalMarks = totalMarks,
                OutOfMarks = outOfMarks
            };

            db.Exams.InsertOnSubmit(exam);
            db.SubmitChanges();

            lblMsg.Text = "Marks added successfully!";
            lblMsg.CssClass = "text-success";

           
            txtTotalMarks.Text = "";
            txtOutOfMarks.Text = "";
            ddlClass.SelectedIndex = 0;
            ddlSubject.Items.Clear();
            ddlSubject.Items.Insert(0, new ListItem("Select Subject", ""));
            ddlStudent.SelectedIndex = 0;

            GetMarks();
        }

        private void GetMarks()
        {
            var marks = from e in db.Exams
                        join c in db.Classes on e.ClassId equals c.ClassId
                         join s in db.Subjects on e.ClassId equals s.ClassId into subjGroup
                        from sub in subjGroup.DefaultIfEmpty()
                        select new
                        {
                            c.ClassName,
                            SubjectName = sub != null ? sub.SubjectName : "(No Subject)",  // Show subject name if any for the class
                            e.RollNo,
                            e.TotalMarks,
                            e.OutOfMarks
                        };

            GridView1.DataSource = marks.ToList();
            GridView1.DataBind();
        }
    }
}
