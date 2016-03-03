using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;
using JensenNS.Objects;
using System.Linq;

namespace LibraryNS.Objects
{
  public class Book : DBHandler
  {
    private int _id;
    private string _title;
    private int _copies;
    public static string TitleColumn = "title";
    public static string CopiesColumn = "copies";
    public static string Table = "books";

    public Book(string title, int copies = 1, int id = 0)
    {
      _id = id;
      _title = title;
      _copies = copies;
    }

    public int GetId()
    {
      return _id;
    }

    public void SetId(int newId)
    {
      _id = newId;
    }

    public string GetTitle()
    {
      return _title;
    }

    public void SetTitle(string newTitle)
    {
      _title = newTitle;
    }

    public int GetCopies()
    {
      return _copies;
    }

    public void SetCopies(int newCopies)
    {
      _copies = newCopies;
    }

    public void AddCopy() { _copies++; }
    public void RemoveCopy() { _copies--; }

    public void Save()
    {
      List<string> columns = new List<string>{TitleColumn, CopiesColumn};
      List<SqlParameter> parameters = new List<SqlParameter>{
        new SqlParameter("@"+CopiesColumn, GetCopies()),
        new SqlParameter("@"+TitleColumn, GetTitle())
      };
      _id = base.Save(Table, columns, parameters, GetId());
      // Console.WriteLine("Book ID: " + _id);
    }
    public void Delete()
    {
      DBHandler.Delete(Table, _id);
    }
    public int GetRemainingCopies()
    {
      string query = "select COUNT(*) from checkouts where book_id = @BookId and returned = 0";
      SqlDataReader rdr = DatabaseOperation(query, new SqlParameter("@BookId", GetId()));
      rdr.Read();
      int output = GetCopies() - rdr.GetInt32(0);
      DatabaseCleanup(rdr);
      return output;
    }
    public List<string> GetCurrentCheckout()
    {
      string query = "SELECT patrons.name, checkouts.* FROM checkouts, patrons WHERE book_id = @BookId AND returned = 0";
      SqlDataReader rdr = DatabaseOperation(query, new SqlParameter("@BookId", GetId()));
      List<string> output = new List<string>(){};
      while(rdr.Read())
      {
        output.Add(rdr.GetString(0) + " Checked out on " + rdr.GetDateTime(4).ToString("MM dd, yyyy") + " Due By " + rdr.GetDateTime(5).ToString("MM dd, yyyy"));
      }
      DatabaseCleanup(rdr);
      return output;
    }
    public List<Patron> GetPatrons() { return GetPatronsByReturned(0); }
    public List<Patron> GetPreviousPatrons() { return GetPatronsByReturned(1); }
    private List<Patron> GetPatronsByReturned(int returned)
    {
      List<Patron> patrons = new List<Patron>{};
      string query = "SELECT " +Patron.Table+ ".* FROM " +Patron.Table+ " JOIN " +Checkout.Table+ " ON ("+Checkout.Table+"."+Checkout.PatronColumn+"="+Patron.Table+".id) WHERE "  +Checkout.Table + "." +Checkout.BookColumn+"= @GetId AND "+ Checkout.Table+"."+Checkout.ReturnedColumn+" = " + returned;
      SqlParameter parameter = new SqlParameter("@GetId", GetId());
      SqlDataReader rdr = DatabaseOperation(query, parameter);
      while (rdr.Read())
      {
        patrons.Add((Patron)Patron.MakeObject(rdr));
      }
      return patrons;
    }
    public static List<Book> GetAll()
    {
      List<Object> fromDB = DBHandler.GetAll(Table, MakeObject);
      return fromDB.Cast<Book>().ToList();
    }
    public static void DeleteAll()
    {
      DBHandler.DeleteAll(Table);
    }
    public override bool Equals(Object otherBook)
    {
      if (!(otherBook is Book))
      {
        return false;
      }
      else
      {
        Book newBook = (Book) otherBook;
        bool idEquality = this.GetId() == newBook.GetId();
        bool titleEquality = this.GetTitle() == newBook.GetTitle();
        return (idEquality && titleEquality);
      }
    }
    public static Object MakeObject(SqlDataReader rdr)
    {
      return new Book(rdr.GetString(1), rdr.GetInt32(2), rdr.GetInt32(0));
    }
    public static Book Find(int id)
    {
      SqlParameter parameter = new SqlParameter("@id", id );
      string query = "WHERE id = @id";
      return (Book) DBHandler.GetObjectFromDB(Table, query, MakeObject, parameter);
    }
  } // end class
} // end namespace
