<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMst.Master" AutoEventWireup="true" CodeBehind="Student.aspx.cs" Inherits="WebApplication1.Admin.Student" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



    
    <div style="background-color:aqua">
    <div class="container p-md-4 p-sm-4">
        <div>
            <asp:Label ID="lblMsg" runat="server" Text="Label"></asp:Label>
        </div>
        <h3 class="text-center"> Add Student</h3>

        <div class="row mb-3 mr-lg-5 ml-lg-5 mt-md-5">

            <div class="col-md-6">
           <label for="txtName">Name</label>
                <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="enter Name"  required ></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Name Should be characters " ForeColor="Red" ValidationExpression="^[A-Za-z]*$" Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtName"></asp:RegularExpressionValidator>
                
              </div>

            
            <div class="col-md-6">
                <label for="txtDoB">Date of Birth</label>
                <asp:TextBox ID="txtDoB" runat="server" CssClass="form-control" Textmode="Date"  required ></asp:TextBox>
          
                </div>
        </div>



         <div class="row mb-3 mr-lg-5 ml-lg-5 mt-md-5">

     <div class="col-md-6">
    <label for="ddlGender">Gender</label>

         <asp:DropDownList ID="ddlGender" runat="server">
             <asp:ListItem Value="0">Select Gender</asp:ListItem>
             <asp:ListItem>Male</asp:ListItem>
             <asp:ListItem>Female</asp:ListItem>
             <asp:ListItem>other</asp:ListItem>
         </asp:DropDownList>
         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Gender is required" ForeColor="Red" ControlToValidate="ddlGender" Display="Dynamic" SetFocusOnError="true" InitialValue="Select Gender"></asp:RequiredFieldValidator>

       </div>

     
     <div class="col-md-6">
         <label for="txtMobile">Contact Number</label>
         <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" Textmode="Number" placeholder="11 digits mobile number" required >
         </asp:TextBox>
         <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="invalid mobile number " ForeColor="Red" ValidationExpression="^[0-9]{11}" Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtMobile"></asp:RegularExpressionValidator>
                
         </div>
 </div>

         <div class="row mb-3 mr-lg-5 ml-lg-5 mt-md-5">

     <div class="col-md-6">
    <label for="txtRoll">Roll Number </label>
         <asp:TextBox ID="txtRoll" runat="server" CssClass="form-control" placeholder="enter Roll No"   required ></asp:TextBox>
        
       </div>

     
     <div class="col-md-6">
         <label for="ddlClass">Class</label>
         <asp:DropDownList ID="ddlClass" runat="server"></asp:DropDownList>
         </div>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"  ControlToValidate="ddlClass" 
                  Display="Dynamic" ForeColor="Red" InitialValue="Select Class"  SetFocusOnError="true" ErrorMessage ="">

             </asp:RequiredFieldValidator>
 </div>


       <div class="row mb-3 mr-lg-5 ml-lg-5 mt-md-5">

    <div class="col-md-12">
   <label for="txtAddress">Address</label>
        <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" placeholder="enter Address" TextMode="MultiLine"  required ></asp:TextBox>
       
      </div>
</div>




         <div class="row mb-3 mr-lg-5 ml-lg-5"> 
             <div class="col-md-3 col-md-offset-2 mb-3">
                 <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary btn-block" BackColor="#5558C" Text="Add Student" OnClick="btnAdd_Click"   />

             </div>

         </div>
        <div class="row mb-3 mr-lg-5 ml-lg-5 ">
    <div class="col-md-12">
        <asp:GridView  ID="GridView1" runat="server" CssClass="table table-hover table-bordered"  AllowPaging="True" PageSize="4" EmptyDataText="No record to display" AutoGenerateColumns="False" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCancelingEdit="GridView1_RowCancelingEdit"  OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating"
            DataKeyNames="StudentId"  OnRowDataBound="GridView1_RowDataBound">
            <Columns>
                <asp:BoundField DataField="" HeaderText="Sr.No" ReadOnly="True">
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Name">
                    <EditItemTemplate>
                         <asp:TextBox ID="txtName" runat="server" Text='<%# Eval("Name") %>' CssClass="form-control" Width="100px"></asp:TextBox>
                 
                          </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Mobile">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtMobile" runat="server" Text='<%# Eval("Mobile") %>' CssClass="form-control" width="100%"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblMobile" runat="server" Text='<%# Eval("Mobile") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>


                  <asp:TemplateField HeaderText="Roll Number">
      
                       <EditItemTemplate>
     <asp:TextBox ID="txtRollNo" runat="server" Text='<%# Eval("RollNo") %>' CssClass="form-control" width="100%"></asp:TextBox>
 </EditItemTemplate>


      <ItemTemplate>
          <asp:Label ID="lblRollNo" runat="server" Text='<%# Eval("RollNo") %>'></asp:Label>
      </ItemTemplate>
      <ItemStyle HorizontalAlign="Center" />
  </asp:TemplateField>


                  <asp:TemplateField HeaderText="Class">
      <EditItemTemplate>
          <asp:DropDownList ID="ddlClass" runat="server"></asp:DropDownList>
          </EditItemTemplate>
      <ItemTemplate>
          <asp:Label ID="lblClass" CssClass="form-control" runat="server" Width="120px" Text='<%# Eval("ClassName") %>'></asp:Label>
      </ItemTemplate>
      <ItemStyle HorizontalAlign="Center" />
  </asp:TemplateField>

       <asp:TemplateField HeaderText="Address">
    <EditItemTemplate>
        <asp:TextBox ID="txtAddress" runat="server" Text='<%# Eval("Address") %>' CssClass="form-control" width="100%"></asp:TextBox>
    </EditItemTemplate>
    <ItemTemplate>
        <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("Address") %>'></asp:Label>
    </ItemTemplate>
    <ItemStyle HorizontalAlign="Center" />
</asp:TemplateField>


                <asp:CommandField HeaderText="Operation" CausesValidation="false"   ShowEditButton="True" ShowDeleteButton="true">
                <ItemStyle HorizontalAlign="Center" />
                </asp:CommandField>
            </Columns>
        </asp:GridView>
    </div>
     </div>
    </div>
</div>



</asp:Content>
