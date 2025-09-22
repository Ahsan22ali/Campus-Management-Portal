using System;
using System.Linq;
using System.Web.UI;
using WebApplication1.LinqData;

namespace WebApplication1.Student
{
    public partial class ViewAttendance : Page
    {
      
        CampusDataDataContext db = new CampusDataDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["CampusDataContext"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GridViewAttendance.Visible = false;
                lblMessage.Text = "";
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string rollNo = txtRollNo.Text.Trim();

            if (string.IsNullOrEmpty(rollNo))
            {
                lblMessage.Text = "Please enter a Roll Number.";
                GridViewAttendance.Visible = false;
                return;
            }

            BindAttendance(rollNo);
        }

        private void BindAttendance(string rollNo)
        {
           
            var attendanceData = (from a in db.StudentAttendences
                                  join s in db.Subjects on a.SubjectId equals s.SubjectId
                                  where a.RollNo == rollNo
                                  orderby a.Date descending
                                  select new
                                  {
                                      s.SubjectName,
                                      a.Date,
                                      Status = (a.Status == true) ? "Present" : "Absent"
                                  }).ToList();

            if (attendanceData.Any())
            {
                GridViewAttendance.DataSource = attendanceData;
                GridViewAttendance.DataBind();
                GridViewAttendance.Visible = true;
                lblMessage.Text = "";
            }
            else
            {
                lblMessage.Text = "No attendance records found for this Roll Number.";
                GridViewAttendance.Visible = false;
            }
        }
    }
}
