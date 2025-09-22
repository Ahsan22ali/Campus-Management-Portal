using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.LinqData;  // Your LINQ to SQL DataContext namespace

namespace WebApplication1.Admin
{
    public partial class Marks : System.Web.UI.Page
    {
        private CampusDataDataContext dc;

        protected void Page_Load(object sender, EventArgs e)
        {
            dc = new CampusDataDataContext(
                System.Configuration.ConfigurationManager.ConnectionStrings["CampusDataContext"].ConnectionString);

            if (!IsPostBack)
            {
                LoadClasses();
                LoadStudents();
                LoadMarks();
            }
        }

        private void LoadClasses()
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

        private void LoadStudents()
        {
            var students = dc.Students
                .Select(s => s.RollNO)
                .Distinct()
                .ToList();

            ddlStudent.DataSource = students;
            ddlStudent.DataBind();

            ddlStudent.Items.Insert(0, new ListItem("Select Student", ""));
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            int classId;
            string rollNo = ddlStudent.SelectedValue;

            if (!int.TryParse(ddlClass.SelectedValue, out classId) || classId == 0 ||
                string.IsNullOrEmpty(rollNo))
            {
                lblMsg.Text = "Please select class and student.";
                lblMsg.CssClass = "text-danger";
                return;
            }

            int totalMarks, outOfMarks;
            if (!int.TryParse(txtTotalMarks.Text.Trim(), out totalMarks) ||
                !int.TryParse(txtOutOfMarks.Text.Trim(), out outOfMarks))
            {
                lblMsg.Text = "Please enter valid numeric values for marks.";
                lblMsg.CssClass = "text-danger";
                return;
            }

            Exam newExam = new Exam
            {
                ClassId = classId,
                RollNo = rollNo,
                TotalMarks = totalMarks,
                OutOfMarks = outOfMarks
            };

            dc.Exams.InsertOnSubmit(newExam);
            dc.SubmitChanges();

            lblMsg.Text = "Marks added successfully!";
            lblMsg.CssClass = "text-success";

            // Clear inputs and reload
            txtTotalMarks.Text = "";
            txtOutOfMarks.Text = "";
            ddlClass.SelectedIndex = 0;
            ddlStudent.SelectedIndex = 0;

            LoadMarks();
        }

        private void LoadMarks()
        {
            var marks = (from e in dc.Exams
                         join c in dc.Classes on e.ClassId equals c.ClassId
                         select new
                         {
                             c.ClassName,
                             e.RollNo,
                             e.TotalMarks,
                             e.OutOfMarks
                         }).ToList();

            GridView1.DataSource = marks;
            GridView1.DataBind();
        }
    }
}
