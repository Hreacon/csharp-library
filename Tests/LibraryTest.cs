using Xunit;
using LibraryNS.Objects;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace LibraryNS
{
  public class BookTest : IDisposable
  {
     public BookTest()
     {
       DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=library_test;Integrated Security=SSPI;";
     }
     public void Dispose()
     {
      // Library.DeleteAll();
     }

     [Fact]
     public void BookHoldsTitle()
     {
       Book newBook = new Book("The Adventures of Huckleberry Finn");
       string output = newBook.GetTitle();
       Assert.Equal(output, "The Adventures of Huckleberry Finn");
     }

     [Fact]
     public void BookTableStartsEmpty()
     {
       Assert.Equal(0, Book.GetAll().Count);
     }

     [Fact]
     public void SaveBook()
     {
       Book newBook = new Book("The Adventures of Huckleberry Finn");
       newBook.Save();

       List<Book> testList = new List<Book>{newBook};

       Assert.Equal(testList, Book.GetAll());
     }
  }
}
