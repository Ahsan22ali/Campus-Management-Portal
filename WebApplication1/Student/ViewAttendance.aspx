<%@ Page Title="View Attendance" Language="C#" MasterPageFile="~/Student/StudentMst.Master" AutoEventWireup="true" CodeBehind="ViewAttendance.aspx.cs" Inherits="WebApplication1.Student.ViewAttendance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background-color: #f9f9f9;
        }

        .container {
            max-width: 700px;
            margin: 40px auto;
            background-color: #ffffff;
            padding: 30px;
            box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
            border-radius: 12px;
        }

        h3 {
            text-align: center;
            color: #333;
            margin-bottom: 25px;
        }

        .form-group {
            display: flex;
            gap: 10px;
            justify-content: center;
            margin-bottom: 20px;
        }

        .form-group input[type="text"] {
            width: 60%;
            padding: 10px;
            border: 1px solid #ccc;
            border-radius: 6px;
            font-size: 16px;
        }

        .btn-search {
            padding: 10px 20px;
            background-color: #007bff;
            color: white;
            border: none;
            border-radius: 6px;
            font-size: 16px;
            cursor: pointer;
            transition: background-color 0.3s ease;
        }

        .btn-search:hover {
            background-color: #0056b3;
        }

        .message {
            text-align: center;
            color: red;
            margin-bottom: 10px;
        }

        .table {
            width: 100%;
            border-collapse: collapse;
            margin-top: 20px;
            font-size: 15px;
        }

        .table th, .table td {
            border: 1px solid #e0e0e0;
            padding: 10px;
            text-align: left;
        }

        .table th {
            background-color: #f0f0f0;
            color: #333;
        }

        .table tr:hover {
            background-color: #f9f9f9;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <h3>📅 My Attendance</h3>

        <asp:Label ID="lblMessage" runat="server" CssClass="message"></asp:Label>

        <div class="form-group">
            <asp:TextBox ID="txtRollNo" runat="server" placeholder="Enter Roll Number" CssClass="form-control" />
            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn-search" OnClick="btnSearch_Click" />
        </div>

        <asp:GridView ID="GridViewAttendance" runat="server" CssClass="table" AutoGenerateColumns="False" Visible="false">
            <Columns>
                <asp:BoundField DataField="SubjectName" HeaderText="Subject" />
                <asp:BoundField DataField="Date" HeaderText="Date" DataFormatString="{0:dd-MM-yyyy}" />
                <asp:BoundField DataField="Status" HeaderText="Status" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
