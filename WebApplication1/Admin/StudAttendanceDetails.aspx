<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMst.Master" AutoEventWireup="true" CodeBehind="StudAttendanceDetails.aspx.cs" Inherits="WebApplication1.Admin.StudAttendanceDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


     <div class="container p-4" style="background-color: #f0f8ff">
        <h3 class="text-center mb-4">Mark Student Attendance</h3>

        <asp:Label ID="lblMsg" runat="server" CssClass="text-success"></asp:Label>

        <div class="row">
            <div class="col-md-6">
                <label>Class:</label>
                <asp:DropDownList ID="ddlClass" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged" CssClass="form-control" />
            </div>
            <div class="col-md-6">
                <label>Subject:</label>
                <asp:DropDownList ID="ddlSubject" runat="server" CssClass="form-control" />
            </div>
        </div>

        <asp:Button ID="btnLoad" runat="server" Text="Load Students" CssClass="btn btn-primary mt-3" OnClick="btnLoad_Click" />

        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered mt-4">
            <Columns>
                <asp:BoundField DataField="RollNo" HeaderText="Roll No" />
                <asp:BoundField DataField="Name" HeaderText="Student Name" />
                <asp:TemplateField HeaderText="Attendance">
                    <ItemTemplate>
                        <asp:RadioButtonList ID="rblStatus" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Present" Value="1" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Absent" Value="0"></asp:ListItem>
                        </asp:RadioButtonList>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <asp:Button ID="btnSubmit" runat="server" Text="Submit Attendance" CssClass="btn btn-success mt-3" OnClick="btnSubmit_Click" />
    </div>



</asp:Content>
