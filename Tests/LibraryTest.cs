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
    Book.DeleteAll();
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

      Assert.Equal(newBook.GetTitle(), Book.GetAll()[0].GetTitle());
    }
    [Fact]
    public void BookUpdatesDatabase()
    {
      Book newBook = new Book("The");
      newBook.Save();
      string title = "The Adventures of Huckleberry Finn";
      newBook.SetTitle(title);
      newBook.Save();
      Assert.Equal(title, Book.GetAll()[0].GetTitle());
    }
    [Fact]
    public void BookEqualsBook()
    {
      Book book1 = new Book("the");
      book1.Save();
      Assert.Equal(book1, Book.GetAll()[0]);
    }

    [Fact]
    public void FindBookById()
    {
      Book newBook = new Book("The");
      newBook.Save();

      Assert.Equal(newBook, Book.Find(newBook.GetId()));
    }
    [Fact]
    public void DeleteBookById()
    {
      Book newBook = new Book("The Hitchhiker's Guide to the Galaxy");
      newBook.Save();

      Assert.Equal(1, Book.GetAll().Count);
      newBook.Delete();

      Assert.Equal(0, Book.GetAll().Count);
    }
  }
}
