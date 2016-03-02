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
      Person.DeleteAll();
      Checkout.DeleteAll();
      Author.DeleteAll();
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

    [Fact]
    public void PersonHoldsName()
    {
      Person newPerson = new Person("The Adventures of Huckleberry Finn");
      string output = newPerson.GetName();
      Assert.Equal(output, "The Adventures of Huckleberry Finn");
    }

    [Fact]
    public void PersonTableStartsEmpty()
    {
      Assert.Equal(0, Person.GetAll().Count);
    }

    [Fact]
    public void SavePerson()
    {
      Person newPerson = new Person("The Adventures of Huckleberry Finn");
      newPerson.Save();

      Assert.Equal(newPerson.GetName(), Person.GetAll()[0].GetName());
    }
    [Fact]
    public void PersonUpdatesDatabase()
    {
      Person newPerson = new Person("The");
      newPerson.Save();
      string title = "The Adventures of Huckleberry Finn";
      newPerson.SetName(title);
      newPerson.Save();
      Assert.Equal(title, Person.GetAll()[0].GetName());
    }
    [Fact]
    public void PersonEqualsPerson()
    {
      Person book1 = new Person("the");
      book1.Save();
      Assert.Equal(book1, Person.GetAll()[0]);
    }

    [Fact]
    public void FindPersonById()
    {
      Person newPerson = new Person("The");
      newPerson.Save();

      Assert.Equal(newPerson, Person.Find(newPerson.GetId()));
    }
    [Fact]
    public void DeletePersonById()
    {
      Person newPerson = new Person("The Hitchhiker's Guide to the Galaxy");
      newPerson.Save();

      Assert.Equal(1, Person.GetAll().Count);
      newPerson.Delete();

      Assert.Equal(0, Person.GetAll().Count);
    }
    [Fact]
    public void PatronHoldsName()
    {
      Patron newPatron = new Patron("The Adventures of Huckleberry Finn");
      string output = newPatron.GetName();
      Assert.Equal(output, "The Adventures of Huckleberry Finn");
    }

    [Fact]
    public void PatronTableStartsEmpty()
    {
      Assert.Equal(0, Patron.GetAll().Count);
    }

    [Fact]
    public void SavePatron()
    {
      Patron newPatron = new Patron("The Adventures of Huckleberry Finn");
      newPatron.Save();

      Assert.Equal(newPatron.GetName(), Patron.GetAll()[0].GetName());
    }
    [Fact]
    public void PatronUpdatesDatabase()
    {
      Patron newPatron = new Patron("The");
      newPatron.Save();
      string title = "The Adventures of Huckleberry Finn";
      newPatron.SetName(title);
      newPatron.Save();
      Assert.Equal(title, Patron.GetAll()[0].GetName());
    }
    [Fact]
    public void PatronEqualsPatron()
    {
      Patron book1 = new Patron("the");
      book1.Save();
      Assert.Equal(book1, Patron.GetAll()[0]);
    }

    [Fact]
    public void FindPatronById()
    {
      Patron newPatron = new Patron("The");
      newPatron.Save();

      Assert.Equal(newPatron, Patron.Find(newPatron.GetId()));
    }
    [Fact]
    public void DeletePatronById()
    {
      Patron newPatron = new Patron("The Hitchhiker's Guide to the Galaxy");
      newPatron.Save();

      Assert.Equal(1, Patron.GetAll().Count);
      newPatron.Delete();

      Assert.Equal(0, Patron.GetAll().Count);
    }

    [Fact]
    public void PatronChecksBookOut()
    {
      Patron paul = new Patron("Paul");
      paul.Save();
      Book guide = new Book("The Hitchhiker's Guide to the Galaxy");
      guide.Save();
      paul.CheckoutBook(guide.GetId());
      Assert.Equal(1, paul.CountBooksCheckedOut());
    }
    [Fact]
    public void PatronChecksBookOutandReturns()
    {
      Patron paul = new Patron("Paul");
      paul.Save();
      Book guide = new Book("The Hitchhiker's Guide to the Galaxy");
      guide.Save();
      paul.CheckoutBook(guide.GetId());
      Assert.Equal(1, paul.CountBooksCheckedOut());
      paul.ReturnBook(guide.GetId());
      Assert.Equal(0, paul.CountBooksCheckedOut());
    }
    [Fact]
    public void PatronKnowsWhichBooksItHas()
    {
      Patron paul = new Patron("Paul");
      paul.Save();
      Book guide = new Book("The Hitchhiker's Guide to the Galaxy");
      guide.Save();
      paul.CheckoutBook(guide.GetId());

      Assert.Equal(guide, paul.GetCheckedOutBooks()[0]);
    }
    [Fact]
    public void PatronKnowsWhichBooksCheckedOutInPast()
    {
      Patron paul = new Patron("Paul");
      paul.Save();
      Book guide = new Book("The Hitchhiker's Guide to the Galaxy");
      guide.Save();
      paul.CheckoutBook(guide.GetId());
      paul.ReturnBook(guide.GetId());
      Assert.Equal(guide, paul.GetPreviouslyCheckedOutBooks()[0]);
    }
    [Fact]
    public void LibrarianAddsCopiesOfBook()
    {
      Book guide = new Book("The Hitchhiker's Guide to the Galaxy");
      guide.Save();

      guide.SetCopies(5);
      // save to Database
      guide.Save();

      Assert.Equal(5, Book.Find(guide.GetId()).GetCopies());
    }
    public void LibrarianDeletesCheckout()
    {
      Patron paul = new Patron("Paul");
      paul.Save();
      Book guide = new Book("The Hitchhiker's Guide to the Galaxy");
      guide.Save();
      paul.CheckoutBook(guide.GetId());
      Assert.Equal(1, paul.CountBooksCheckedOut());

      paul.DeleteCheckout(guide.GetId());
      Assert.Equal(0, paul.CountBooksCheckedOut());
    }

    [Fact]
    public void BookKnowsWhoHasCopies()
    {
      Patron paul = new Patron("Paul");
      paul.Save();
      Book guide = new Book("The Hitchhiker's Guide to the Galaxy", 2);
      guide.Save();
      paul.CheckoutBook(guide.GetId());
      Assert.Equal(paul, guide.GetPatrons()[0]);
    }
    /*
    [Fact]
    public void BookKnowsWhoPreviouslyCheckedItOut()
    {

    }
    /*
    book knows how many copies are left
    book knows how many copies are total
    when patron checks book out it makes sure there is a copy left
    authors know what books they've written
    books know who its authors are
    people become authors
    book can search for books by title or author name

    /**/
  }
}
