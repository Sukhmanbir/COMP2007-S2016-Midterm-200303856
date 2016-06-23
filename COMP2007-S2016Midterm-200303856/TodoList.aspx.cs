using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

// using statements that are required to connect to EF DB
using COMP2007_S2016Midterm_200303856.Models;
using System.Web.ModelBinding;
using System.Linq.Dynamic;

namespace COMP2007_S2016Midterm_200303856
{
    public partial class TodoList : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            // if loading the page for the first time, populate the todo grid
            if (!IsPostBack)
            {
                Session["SortColumn"] = "TodoID"; // default sort column
                Session["SortDirection"] = "ASC";
                // Get the todo data
                this.GetTodos();
            }
        }

        //fetches the todo list records from todos table
        protected void GetTodos()
        {
            // connect to EF
            using (TodoConnection db = new TodoConnection())
            {
                string SortString = Session["SortColumn"].ToString() + " " + Session["SortDirection"].ToString();

                // query the Todo Table using EF and LINQ
                var Todos = (from allTodos in db.Todos
                                select allTodos);

                // bind the result to the GridView
                TodoGridView.DataSource = Todos.AsQueryable().OrderBy(SortString).ToList();
                TodoGridView.DataBind();
            }
        }

        protected void TodoGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // store which row was clicked
            int selectedRow = e.RowIndex;

            // get the selected TodoID using the Grid's DataKey collection
            int TodoID = Convert.ToInt32(TodoGridView.DataKeys[selectedRow].Values["TodoID"]);

            // use EF to find the selected todo in the DB and remove it
            using (TodoConnection db = new TodoConnection())
            {
                // create object of the Todo class and store the query string inside of it
                Todo deletedTodo = (from todoRecords in db.Todos
                                          where todoRecords.TodoID == TodoID
                                          select todoRecords).FirstOrDefault();

                // remove the selected todo from the db
                db.Todos.Remove(deletedTodo);

                // save my changes back to the database
                db.SaveChanges();

                // refresh the grid
                this.GetTodos();
            }
        }

        protected void PageSizeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Set the new Page size
            TodoGridView.PageSize = Convert.ToInt32(PageSizeDropDownList.SelectedValue);

            // refresh the grid
            this.GetTodos();
        }

        protected void TodoGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            // get the column to sorty by
            Session["SortColumn"] = e.SortExpression;

            // Refresh the Grid
            this.GetTodos();

            // toggle the direction
            Session["SortDirection"] = Session["SortDirection"].ToString() == "ASC" ? "DESC" : "ASC";
        }

        protected void TodoGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (IsPostBack)
            {
                // if header row has been clicked
                if (e.Row.RowType == DataControlRowType.Header) 
                {
                    LinkButton linkbutton = new LinkButton();

                    for (int index = 0; index < TodoGridView.Columns.Count - 1; index++)
                    {
                        if (TodoGridView.Columns[index].SortExpression == Session["SortColumn"].ToString())
                        {
                            if (Session["SortDirection"].ToString() == "ASC")
                            {
                                linkbutton.Text = " <i class='fa fa-caret-up fa-lg'></i>";
                            }
                            else
                            {
                                linkbutton.Text = " <i class='fa fa-caret-down fa-lg'></i>";
                            }

                            e.Row.Cells[index].Controls.Add(linkbutton);
                        }
                    }
                }
            }
        }

        protected void TodoGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // Set the new page number
            TodoGridView.PageIndex = e.NewPageIndex;

            // refresh the grid
            this.GetTodos();
        }

        protected void CompletedTask_CheckedChanged(object sender, EventArgs e)
        {
            GridViewRow gridRow = (sender as CheckBox).NamingContainer as GridViewRow;

            //TodoDetails.GetTodo();
        }
    }
}