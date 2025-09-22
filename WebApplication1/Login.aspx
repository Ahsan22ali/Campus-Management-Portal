<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebApplication1.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login Page</title>
    <style>
        body {
            font-family: Arial;
            background-color: #f0f2f5;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
        }
        .login-container {
            background-color: #fff;
            padding: 30px;
            border-radius: 8px;
            box-shadow: 0 0 15px rgba(0,0,0,0.2);
            width: 350px;
        }
        .login-container h2 {
            text-align: center;
            margin-bottom: 25px;
        }
        .form-group {
            margin-bottom: 15px;
        }
        label {
            display: block;
            margin-bottom: 5px;
        }
        input, select {
            width: 100%;
            padding: 8px;
            box-sizing: border-box;
        }
        .btn {
            width: 100%;
            padding: 10px;
            background-color: #007bff;
            color: white;
            border: none;
            cursor: pointer;
            margin-top: 10px;
        }
        .btn:hover {
            background-color: #0056b3;
        }
        .error-message {
            color: red;
            margin-top: 10px;
            text-align: center;
        }
    </style>

    <script type="text/javascript">
        function toggleInputs() {
            var role = document.getElementById("<%= ddlRole.ClientID %>").value;
            var rollDiv = document.getElementById("rollNoDiv");
            var userDiv = document.getElementById("usernameDiv");

            if (role === "Student") {
                rollDiv.style.display = "block";
                userDiv.style.display = "none";
            } else {
                rollDiv.style.display = "none";
                userDiv.style.display = "block";
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-container">
            <h2>Login</h2>

            <div class="form-group">
                <label for="ddlRole">Select Role</label>
                <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control" AutoPostBack="false" onchange="toggleInputs()">
                    <asp:ListItem Text="Select Role" Value="" />
                    <asp:ListItem Text="Student" Value="Student" />
                    <asp:ListItem Text="Teacher" Value="Teacher" />
                    <asp:ListItem Text="Admin" Value="Admin" />
                </asp:DropDownList>
            </div>

            <div class="form-group" id="rollNoDiv" style="display:none;">
                <label for="txtRollNo">Roll No</label>
                <asp:TextBox ID="txtRollNo" runat="server" CssClass="form-control" />
            </div>

            <div class="form-group" id="usernameDiv" style="display:block;">
                <label for="txtUser">Username</label>
                <asp:TextBox ID="txtUser" runat="server" CssClass="form-control" />
            </div>

            <div class="form-group">
                <label for="txtPassword">Password</label>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" />
            </div>

            <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn" OnClick="btnLogin_Click" />
            <asp:Label ID="lblMessage" runat="server" CssClass="error-message"></asp:Label>
        </div>
    </form>
</body>
</html>
