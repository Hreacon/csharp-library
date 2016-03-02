using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;
using JensenNS.Objects;

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

  } // end class
} // end namespace
