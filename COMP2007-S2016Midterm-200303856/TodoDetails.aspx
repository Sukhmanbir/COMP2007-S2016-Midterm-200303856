<%--
    
    Author - Sukhmanbir Kaur
    Student # - 200303856
    Filename - TodoDetails.aspx
    Desc - This file lets user add new todo list record to the database
     --%>

<%@ Page Title="Todo Details" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TodoDetails.aspx.cs" Inherits="COMP2007_S2016Midterm_200303856.TodoDetails" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     
    <div class="container">
        <div class="row">
            <div class="col-md-offset-3 col-md-6">
                <h1>Todo Details</h1>

                <div class="form-group">
                    <label class="control-label" for="TodoNameTextBox">Todo Name</label>
                    <asp:TextBox runat="server" CssClass="form-control" ID="TodoNameTextBox" placeholder="Todo Name" required="true"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label class="control-label" for="TodoNotesTextBox">Todo Notes</label>
                    <asp:TextBox runat="server" CssClass="form-control" ID="TodoNotesTextBox" placeholder="Todo Notes" required="true"></asp:TextBox>
                </div>
                
                <div class="form-group checkbox-inline">
                    <asp:Checkbox runat="server" ID="CompletedTextBox" required="false" Visible="true"/>
                    <label class="control-label" for="CompletedTextBox">Completed</label>
                </div>

                <div class="text-right">
                    <asp:Button Text="Cancel" ID="CancelButton" CssClass="btn btn-warning btn-lg" runat="server" 
                        UseSubmitBehavior="false" CausesValidation="false" OnClick="CancelButton_Click" />
                    <asp:Button Text="Save" ID="SaveButton" CssClass="btn btn-primary btn-lg" runat="server" OnClick="SaveButton_Click" />
                </div>
            </div>
        </div>
    </div>

</asp:Content>
