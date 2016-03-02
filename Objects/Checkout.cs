using JensenNS.Objects;

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
