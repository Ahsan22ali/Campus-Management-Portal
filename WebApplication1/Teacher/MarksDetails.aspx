<%@ Page Title="" Language="C#" MasterPageFile="~/Teacher/TeacherMst.Master" AutoEventWireup="true" CodeBehind="MarksDetails.aspx.cs" Inherits="WebApplication1.Teacher.MarksDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <div class="container p-4" style="background-color: #e9faff">
     <h3 class="text-center mb-4">Add Student Marks</h3>

     <asp:Label ID="lblMsg" runat="server" CssClass="text-success"></asp:Label>

     <div class="form-group">
         <label>Class:</label>
         <asp:DropDownList ID="ddlClass" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged" CssClass="form-control"></asp:DropDownList>
     </div>

     <div class="form-group">
         <label>Subject:</label>
         <asp:DropDownList ID="ddlSubject" runat="server" CssClass="form-control"></asp:DropDownList>
     </div>

     <div class="form-group">
         <label>Student (Roll No):</label>
         <asp:DropDownList ID="ddlStudent" runat="server" CssClass="form-control"></asp:DropDownList>
     </div>

     <div class="form-group">
         <label>Total Marks:</label>
         <asp:TextBox ID="txtTotalMarks" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
     </div>

     <div class="form-group">
         <label>Obtained Marks:</label>
         <asp:TextBox ID="txtOutOfMarks" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
     </div>

     <asp:Button ID="btnAdd" runat="server" Text="Add Marks" CssClass="btn btn-primary mt-2" OnClick="btnAdd_Click" />

     <hr />

     <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover mt-4" AutoGenerateColumns="False">
         <Columns>
             <asp:BoundField DataField="ClassName" HeaderText="Class" />
             <asp:BoundField DataField="SubjectName" HeaderText="Subject" />
             <asp:BoundField DataField="RollNo" HeaderText="Roll No" />
             <asp:BoundField DataField="TotalMarks" HeaderText="Total Marks" />
             <asp:BoundField DataField="OutOfMarks" HeaderText="Obtained Marks" />
         </Columns>
     </asp:GridView>
 </div>
</asp:Content>
