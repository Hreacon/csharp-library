using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;
using JensenNS.Objects;
using System.Linq;

namespace LibraryNS.Objects
{
  public class Patron : DBHandler
  {
    private int _id;
    private string _name;
    public static string NameColumn = "name";
    public static string Table = "people";

    public Patron(string name, int id = 0)
    {
      _id = id;
      _name = name;
    }

    public int GetId()
    {
    return _id;
    }

    public void SetId(int newId)
    {
      _id = newId;
    }
    public string GetName() { return _name; }
    public void SetName(string name) { _name = name; }

    public int CountBooksCheckedOut()
    {
      string query = "WHERE patron_id = @id and returned = 0";
      List<SqlParameter> parameters = new List<SqlParameter> {
        new SqlParameter("@id", GetId())

      };
       List<Object> CheckedOutList = GetList(Checkout.Table, query, Checkout.MakeObject, parameters);
      return CheckedOutList.Count;
    }
    public List<Book> GetCheckedOutBooks() { return GetBooks(0); }
    public List<Book> GetPreviouslyCheckedOutBooks() { return GetBooks(1); }
    private List<Book> GetBooks(int returned)
    {
      string table = Book.Table + ", " + Checkout.Table;
      string query = "WHERE " + Checkout.Table + "." + Checkout.BookColumn + " = " + Book.Table + ".id AND "+ Checkout.Table + "." + Checkout.PatronColumn + " = @patronid AND " + Checkout.Table + "." + Checkout.ReturnedColumn + " = " + returned;
      List<Object> books = base.GetList(table, query, Book.MakeObject, new SqlParameter("@patronid", GetId()));
      return books.Cast<Book>().ToList();
    }
    public void CheckoutBook(int bookId)
    {
      DateTime today = DateTime.Today;
      DateTime dueDate = DateTime.Now.AddDays(21);
      int returned = 0;
      List<SqlParameter> parameters = new List<SqlParameter> {
        new SqlParameter("@"+Checkout.DateColumn, today),
        new SqlParameter("@"+Checkout.DueDateColumn, dueDate),
        new SqlParameter("@"+Checkout.ReturnedColumn, returned),
        new SqlParameter("@"+Checkout.PatronColumn, GetId()),
        new SqlParameter("@"+Checkout.BookColumn, bookId)
      };
      base.Save(Checkout.Table, Checkout.Columns, parameters);
    }
    public void ReturnBook(int bookId)
    {
      DateTime today = DateTime.Today;
      string query = "UPDATE " +Checkout.Table+ " SET " +Checkout.ReturnedColumn+ " = 1  WHERE " +Checkout.ReturnedColumn+ " = 0 AND " +Checkout.PatronColumn+ " = @GetId AND " +Checkout.BookColumn+ " = @bookId";

      List<SqlParameter> parameters = new List<SqlParameter> {
        new SqlParameter("@GetId", GetId()),
        new SqlParameter("@bookId", bookId)
      };
      DatabaseOperation(query, parameters);
    }
    public void Save()
    {
      List<string> columns = new List<string>{NameColumn};
      List<SqlParameter> parameters = new List<SqlParameter>{
        new SqlParameter("@"+NameColumn, GetName())
      };
      _id = base.Save(Table, columns, parameters, GetId());
    }
    public void Delete()
    {
      DBHandler.Delete(Table, _id);
    }
    public static List<Patron> GetAll()
    {
      List<Object> fromDB = DBHandler.GetAll(Table, MakeObject);
      return fromDB.Cast<Patron>().ToList();
    }
    public static void DeleteAll()
    {
      DBHandler.DeleteAll(Table);
    }
    public override bool Equals(Object otherPatron)
    {
      if (!(otherPatron is Patron))
      {
        return false;
      }
      else
      {
        Patron newPatron = (Patron) otherPatron;
        bool idEquality = this.GetId() == newPatron.GetId();
        bool nameEquality = this.GetName() == newPatron.GetName();
        return (idEquality && nameEquality);
      }
    }
    public static Object MakeObject(SqlDataReader rdr)
    {
      return new Patron(rdr.GetString(1), rdr.GetInt32(0));
    }
    public static Patron Find(int id)
    {
      SqlParameter parameter = new SqlParameter("@id", id );
      string query = "WHERE id = @id";
      return (Patron) DBHandler.GetObjectFromDB(Table, query, MakeObject, parameter);
    }
  } // end class
} // end namespace
