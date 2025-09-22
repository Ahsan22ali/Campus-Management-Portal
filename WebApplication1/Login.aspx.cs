using System;
using System.Linq;
using System.Web.UI;
using WebApplication1.LinqData; 
namespace WebApplication1
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ddlRole.Attributes["onchange"] = "toggleInputs()";
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string role = ddlRole.SelectedValue;
            string username = txtUser.Text.Trim();
            string password = txtPassword.Text.Trim();
            string rollNo = txtRollNo.Text.Trim();

            
            if (password != "123")
            {
                lblMessage.Text = "Incorrect password.";
                return;
            }

            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["CampusDataContext"].ToString();
            CampusDataDataContext db = new CampusDataDataContext(connStr);

            if (role == "Admin")
            {
                if (username.Equals("admin", StringComparison.OrdinalIgnoreCase))
                {
                    Session["Username"] = "admin";
                    Response.Redirect("/Admin/AdminHome.aspx");
                }
                else
                {
                    lblMessage.Text = "Invalid admin username.";
                }
            }
            else if (role == "Teacher")
            {
                var teacher = db.Teachers.FirstOrDefault(t => t.Email == username);
                if (teacher != null)
                {
                    Session["Username"] = teacher.Email;
                    Response.Redirect("/Teacher/TeacherHome.aspx");
                }
                else
                {
                    lblMessage.Text = "Invalid teacher email.";
                }
            }
            else if (role == "Student")
            {
                var student = db.Students.FirstOrDefault(s => s.RollNO == rollNo);
                if (student != null)
                {
                    Session["RollNo"] = student.RollNO;
                    Response.Redirect("/Student/StudentHome.aspx");
                }
                else
                {
                    lblMessage.Text = "Invalid student roll number.";
                }
            }
            else
            {
                lblMessage.Text = "Please select a valid role.";
            }
        }
    }
}
