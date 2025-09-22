<%@ Page Title="Student Home" Language="C#" MasterPageFile="~/Student/StudentMst.Master" AutoEventWireup="true" CodeBehind="StudentHome.aspx.cs" Inherits="WebApplication1.Student.StudentHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        body {
    font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
    background: #f4f7fc;
    color: #333;
    margin: 0;
    padding: 0;
}
.section {
    max-width: 400px;
    margin: 40px auto;
    padding: 30px 25px;
    border-radius: 12px;
    background-color: #ffffff;
    box-shadow: 0 4px 15px rgba(0,0,0,0.1);
    transition: transform 0.3s ease, box-shadow 0.3s ease;
}
.section:hover {
    transform: translateY(-5px);
    box-shadow: 0 10px 30px rgba(0,0,0,0.15);
}
.section h2 {
    margin-top: 0;
    font-size: 1.9rem;
    color: #004085;
    font-weight: 700;
    margin-bottom: 15px;
    text-align: center;
}
.section p {
    font-size: 1.1rem;
    color: #6c757d;
    text-align: center;
    line-height: 1.5;
}
.btn-link {
    display: block;
    width: 100%;
    text-align: center;
    padding: 12px 0;
    background-color: #007bff;
    color: white !important;
    text-decoration: none;
    font-weight: 600;
    font-size: 1.1rem;
    border-radius: 8px;
    margin-top: 25px;
    box-shadow: 0 4px 8px rgba(0,123,255,0.3);
    transition: background-color 0.3s ease, box-shadow 0.3s ease;
    user-select: none;
}
.btn-link:hover, .btn-link:focus {
    background-color: #0056b3;
    box-shadow: 0 6px 14px rgba(0,86,179,0.6);
    text-decoration: none;
    outline: none;
    cursor: pointer;
}
/* Responsive */
@media (max-width: 500px) {
    .section {
        margin: 20px 15px;
        padding: 25px 20px;
    }
    .btn-link {
        font-size: 1rem;
    }
}

    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="section" id="marksSection">
        <h2>View Marks</h2>
        <p>Click the button below to view your exam marks.</p>
        <asp:HyperLink ID="lnkViewMarks" runat="server" CssClass="btn-link" NavigateUrl="ViewMarks.aspx">View Marks</asp:HyperLink>
    </div>

    <div class="section" id="attendanceSection">
        <h2>View Attendance</h2>
        <p>Click the button below to view your attendance records.</p>
        <asp:HyperLink ID="lnkViewAttendance" runat="server" CssClass="btn-link" NavigateUrl="ViewAttendance.aspx">View Attendance</asp:HyperLink>
    </div>

</asp:Content>
