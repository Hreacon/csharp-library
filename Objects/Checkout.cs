using JensenNS.Objects;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace LibraryNS.Objects
{
  public class Checkout : DBHandler
  {
    public static string Table = "checkouts";
    public static string PatronColumn = "patron_id";
    public static string BookColumn = "book_id";
    public static string DateColumn = "date";
    public static string DueDateColumn = "duedate";
    public static string ReturnedColumn = "returned";
    public static List<string> Columns = new List<string> { PatronColumn, BookColumn, DateColumn, DueDateColumn, ReturnedColumn };

    public static void Delete(int id)
    {
      DBHandler.Delete(Table, id);
    }
    public static void DeleteAll()
    {
      DBHandler.DeleteAll(Table);
    }
    public static Object MakeObject(SqlDataReader rdr)
    {
      return new Object();
    }
  }
}
