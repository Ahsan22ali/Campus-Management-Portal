using System;
using System.Linq;
using System.Web.UI;
using System.Configuration;  
using WebApplication1.LinqData;

namespace WebApplication1.Student
{
    public partial class ViewMarks : Page
    {
        private CampusDataDataContext db = new CampusDataDataContext(ConfigurationManager.ConnectionStrings["CampusDataContext"].ConnectionString);

        protected void btnGetMarks_Click(object sender, EventArgs e)
        {
            string rollNo = txtRollNo.Text.Trim();

            if (string.IsNullOrEmpty(rollNo))
            {
                lblMessage.Text = "Please enter a Roll Number.";
                gvMarks.Visible = false;
                return;
            }

            try
            {
                var marksData = (from exam in db.Exams
                                 join s in db.Subjects on exam.SubjectID equals s.SubjectId
                                 join c in db.Classes on exam.ClassId equals c.ClassId
                                 where exam.RollNo == rollNo
                                 select new
                                 {
                                     ClassName = c.ClassName,
                                     SubjectName = s.SubjectName,
                                     TotalMarks = exam.TotalMarks,
                                     OutOfMarks = exam.OutOfMarks
                                 }).ToList();

                if (marksData.Count > 0)
                {
                    gvMarks.DataSource = marksData;
                    gvMarks.DataBind();
                    gvMarks.Visible = true;
                    lblMessage.Text = "";
                }
                else
                {
                    gvMarks.Visible = false;
                    lblMessage.Text = "No marks found for this Roll Number.";
                }
            }
            catch (Exception ex)
            {
                gvMarks.Visible = false;
                lblMessage.Text = "Error: " + ex.Message;
            }
        }
    }
}
