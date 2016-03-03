using JensenNS.Objects;
using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace LibraryNS.Objects
{
  public class Author : DBHandler
  {
    public static string Table = "authors";
    public static string AuthorColumn = "author_id";
    public static string BookColumn = "book_id";
    public static List<string> Columns = new List<string> { AuthorColumn, BookColumn };

    public static List<SqlParameter> MakeParameters(int authorId, int bookId)
    {
      return new List<SqlParameter> {
        new SqlParameter("@"+Author.AuthorColumn, authorId),
        new SqlParameter("@"+Author.BookColumn, bookId)
      };
    }
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
