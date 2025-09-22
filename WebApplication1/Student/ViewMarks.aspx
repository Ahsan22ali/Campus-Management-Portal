<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewMarks.aspx.cs" Inherits="WebApplication1.Student.ViewMarks" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View Marks</title>
    <style>
        body {
    font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
    background: #f4f7f9;
    color: #333;
    margin: 0;
    padding: 0;
}

div {
    max-width: 600px;
    margin: 40px auto;
    background: white;
    padding: 30px 40px;
    box-shadow: 0 8px 16px rgba(0,0,0,0.1);
    border-radius: 8px;
}

h2 {
    color: #007bff;
    font-weight: 700;
    margin-bottom: 20px;
    font-size: 28px;
}

asp\:TextBox, input[type="text"], input[type="email"], input[type="number"], input[type="password"] {
    font-size: 16px;
    padding: 10px 12px;
    width: 100%;
    border: 1.5px solid #ced4da;
    border-radius: 6px;
    box-sizing: border-box;
    margin-bottom: 15px;
    transition: border-color 0.3s ease;
}

asp\:TextBox:focus, input[type="text"]:focus, input[type="email"]:focus, input[type="number"]:focus, input[type="password"]:focus {
    border-color: #007bff;
    outline: none;
}

asp\:Button, button {
    background-color: #007bff;
    border: none;
    color: white;
    padding: 12px 25px;
    font-size: 16px;
    border-radius: 6px;
    cursor: pointer;
    transition: background-color 0.3s ease;
    display: inline-block;
    margin-top: 5px;
}

asp\:Button:hover, button:hover {
    background-color: #0056b3;
}

.error {
    color: #dc3545;
    font-weight: 600;
    margin-top: 10px;
    display: block;
}

table {
    width: 100%;
    border-collapse: collapse;
    margin-top: 30px;
    font-size: 16px;
}

th, td {
    padding: 12px 15px;
    border: 1px solid #dee2e6;
    text-align: left;
}

th {
    background-color: #007bff;
    color: white;
    font-weight: 600;
}

tr:nth-child(even) {
    background-color: #f8f9fa;
}

tr:hover {
    background-color: #e9ecef;
    transition: background-color 0.3s ease;
}

    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <p class="welcome-msg">
                Welcome! Please enter your roll number to view your exam marks.</p>
  
            <h2>Enter Your Roll Number</h2>
            <asp:TextBox ID="txtRollNo" runat="server" Width="200px"></asp:TextBox>
            <asp:Button ID="btnGetMarks" runat="server" Text="Get Marks" OnClick="btnGetMarks_Click" />
            <br /><br />
            <asp:Label ID="lblMessage" runat="server" CssClass="error"></asp:Label>

            <asp:GridView ID="gvMarks" runat="server" AutoGenerateColumns="false" Visible="false">
                <Columns>
                    <asp:BoundField DataField="ClassName" HeaderText="Class" />
                    <asp:BoundField DataField="SubjectName" HeaderText="Subject" />
                    <asp:BoundField DataField="TotalMarks" HeaderText="Marks Obtained" />
                    <asp:BoundField DataField="OutOfMarks" HeaderText="Total Marks" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
