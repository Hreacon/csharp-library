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

    public void Save()
    {
      List<string> columns = new List<string>{TitleColumn, CopiesColumn};
      List<SqlParameter> parameters = new List<SqlParameter>{
        new SqlParameter("@"+CopiesColumn, GetCopies()),
        new SqlParameter("@"+TitleColumn, GetTitle())
      };
      _id = base.Save(Table, columns, parameters, GetId());
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
    public static Object MakeObject(SqlDataReader rdr)
    {
      return new Book(rdr.GetString(1), rdr.GetInt32(2), rdr.GetInt32(0));
    }
  } // end class
} // end namespace
