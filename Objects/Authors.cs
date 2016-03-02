using JensenNS.Objects;

namespace LibraryNS.Objects
{
  public class Author : DBHandler
  {
    public static string Table = "authors";
    public static string AuthorColumn = "author_id";
    public static string BookColumn = "book_id";

    public static void Delete(int id)
    {
      DBHandler.Delete(Table, id);
    }
    public static void DeleteAll()
    {
      DBHandler.DeleteAll(Table);
    }
  }
}
