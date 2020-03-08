using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using AndersonBookShop.Models;

namespace AndersonBookShop.Controllers
{
    public class BookController : Controller
    {
        string connectionString = @"Data Source = LAPTOP-3KFTRPBK; Initial Catalog = AndersonBookShopDb; Integrated Security = True";
        // GET: Book
        [HttpGet]
        public ActionResult Index()
        {
            DataTable dataTableBook = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("Select * from Books", connection);
                dataAdapter.Fill(dataTableBook);
            }
            return View(dataTableBook);
        }

        
        // GET: Book/Create
        public ActionResult Create()
        {
            return View(new Books());
        }

        // POST: Book/Create
        [HttpPost]
        public ActionResult Create(Books book)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "Insert into Books Values(@BookName, @AuthorName, @Published, @Available)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@BookName", book.BookName);
                command.Parameters.AddWithValue("@AuthorName", book.Author);
                command.Parameters.AddWithValue("@Published", book.Published);
                command.Parameters.AddWithValue("@Available", book.Available);
                command.ExecuteNonQuery();
             }

            return RedirectToAction("Index");
            
        }

        // GET: Book/Edit/5
        public ActionResult Edit(int id)
        {
            Books book = new Books();
            DataTable dataTableBook = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "Select * from Books where BookId = @BookId";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.SelectCommand.Parameters.AddWithValue("@BookId", id);
                adapter.Fill(dataTableBook);
            }
            if(dataTableBook.Rows.Count == 1)
            {
                book.BookID = Convert.ToInt32(dataTableBook.Rows[0][0].ToString());
                book.BookName = dataTableBook.Rows[0][1].ToString();
                book.Author = dataTableBook.Rows[0][2].ToString();
                book.Published = Convert.ToInt32(dataTableBook.Rows[0][3].ToString());
                book.Available = dataTableBook.Rows[0][4].ToString();
                return View(book);
            }
            
            return RedirectToAction("Index");
        }

        // POST: Book/Edit/5
        [HttpPost]
        public ActionResult Edit(Books book)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "Update Books Set BookName = @BookName, AuthorName = @AuthorName, Published = @Published, Available = @Available";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@BookName", book.BookName);
                command.Parameters.AddWithValue("@AuthorName", book.Author);
                command.Parameters.AddWithValue("@Published", book.Published);
                command.Parameters.AddWithValue("@Available", book.Available);
                command.ExecuteNonQuery();
            }

            return RedirectToAction("Index");

        }

        // GET: Book/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "Delete from  Books where BookId = @BookId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@BookId", id);
                command.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }
       
    }
}
