using Nancy;
using LibraryNS.Objects;
using System.Collections.Generic;
using System;
namespace LibraryNS
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      // Login Route
      Get["/"] = _ => {
        return View["index.cshtml"];
      };

      // Librarian Routes
      Get["/admin"] = _ => {
        return View["admin.cshtml", Patron.GetAll()];
      };
      Get["/admin/books/"] = _ => {
        Dictionary<string, object> model = new Dictionary<string, object>(){};
        model.Add("books",  Book.GetAll());
        model.Add("link", (object)"/admin");
        return View["listBooksAdmin.cshtml", model];
      };
      Get["/admin/patrons/"] = _ => {
        Dictionary<string, object> model = new Dictionary<string, object>(){};
        model.Add("patrons",  Patron.GetAll());
        model.Add("link", (object)"/admin");
        return View["listPatronsAdmin.cshtml", model];
      };
      Get["/admin/authors/"] = _ => {
        return View["listAuthorsAdmin.cshtml", Person.GetAll()];
      };
      Get["/admin/patron/{id}/"] = x => {
        return View["viewPatronAdmin.cshtml", Patron.Find(int.Parse(x.id))];
      };
      Get["/admin/author/{id}/"] = x => {
        return View["viewAuthorAdmin.cshtml", Person.Find(int.Parse(x.id))];
      };
      Get["/admin/book/{id}/"] = x => {
        return View["viewBookAdmin.cshtml", Book.Find(int.Parse(x.id))];
      };
      Get["/admin/book/{id}/edit/"] = x => {
        return View["editBookAdmin.cshtml", Book.Find(int.Parse(x.id))];
      };
      Get["/admin/patron/{id}/edit/"] = x => {
        return View["editPatronAdmin.cshtml", Patron.Find(int.Parse(x.id))];
      };
      Get["/admin/author/{id}/edit/"] = x => {
        return View["editAuthorAdmin.cshtml", Person.Find(int.Parse(x.id))];
      };
      Post["/admin/patron/{id}/edit/save/"] = x => {
        string name = Request.Form["name"];
        Patron edit = Patron.Find(int.Parse(x.id));
        edit.SetName(name);
        edit.Save();
        return View["forward.cshtml", "/admin/patron/"+edit.GetId()];
      };
      Post["/admin/author/{id}/edit/save/"] = x => {
        string name = Request.Form["name"];
        Person edit = Person.Find(int.Parse(x.id));
        edit.SetName(name);
        edit.Save();
        return View["forward.cshtml", "/admin/author/"+edit.GetId()];
      };
      Post["/admin/book/{id}/edit/save/"] = x => {
        string title = Request.Form["title"];
        Book edit = Book.Find(int.Parse(x.id));
        edit.SetTitle(title);
        edit.Save();
        return View["forward.cshtml", "/admin/book/"+edit.GetId()];
      };
      Post["/admin/patron/add"] = x => {
        string name = Request.Form["patronName"];
        new Patron(name).Save();
        return View["forward.cshtml", "/admin/patrons/"];
      };
      Post["/admin/author/add"] = x => {
        string name = Request.Form["name"];
        new Person(name).Save();
        return View["forward.cshtml", "/admin/authors/"];
      };
      Post["/admin/book/add"] = _ => {
        string title = Request.Form["title"];
        int count = int.Parse(Request.Form["count"]);
        new Book(title, count).Save();
        return View["forward.cshtml", "/admin/books/"];
      };
      Post["/admin/books/search/"] = _ => {
        string term = Request.Form["search"];
        Dictionary<string, object> model = new Dictionary<string, object>(){};
        model.Add("books", Book.Search(term));
        model.Add("link", (object)"/admin");
        return View["listBooksAdmin.cshtml", model ];
      };
      Get["/admin/patron/{pid}/return/{bid}"] = x => {
        Patron.Find(int.Parse(x.pid)).ReturnBook(int.Parse(x.bid));
        return View["forward.cshtml", "/admin/patron/"+int.Parse(x.pid)];
      };
      Get["/admin/author/{aid}/addBook"] = x => {
        Dictionary<string, object> model = new Dictionary<string, object>(){};
        model.Add("author", Person.Find(int.Parse(x.aid)));
        model.Add("books", Book.GetAll());
        model.Add("link", (object)"/admin");
        return View["listBooksToAuthorAdmin.cshtml", model];
      };
      Get["/admin/author/{aid}/authorBook/{bid}"] = x => {
        Person.Find(int.Parse(x.aid)).AuthorBook(int.Parse(x.bid));
        return View["forward.cshtml", "/admin/author/"+int.Parse(x.aid)];
      };

      // Patron Routes

      Get["/patron"] = _ => {
        Dictionary<string, object> model = new Dictionary<string, object>(){};
        model.Add("patrons",  Patron.GetAll());
        model.Add("link", "");
        return View["listPatronsPatron.cshtml", model];
      };
      Get["/patron/{pid}"] = x => {
      Dictionary<string, object> model = new Dictionary<string, object>(){};
      model.Add("patron", Patron.Find(int.Parse(x.pid)));
      model.Add("link", "/patron/"+int.Parse(x.pid));
        return View["viewPatronPatron.cshtml", model];
      };
      Get["/patron/{pid}/listbooks"] = x => {
        Dictionary<string, object> model = new Dictionary<string, object>(){};
        model.Add("patron", Patron.Find(int.Parse(x.pid)));
        model.Add("books", Book.GetAll());
        model.Add("link", "/patron/"+int.Parse(x.pid));
        return View["listBooksPatron.cshtml", model];
      };
      Get["/patron/{pid}/search"] = x => {
        string term = Request.Form["search"];
        Dictionary<string, object> model = new Dictionary<string, object>(){};
        model.Add("patron", Patron.Find(int.Parse(x.pid)));
        model.Add("books", Book.Search(term));
        model.Add("link", "/patron/"+int.Parse(x.pid));
        return View["listBooksPatron.cshtml", model];
      };
      Get["/patron/{pid}/book/{bid}"] = x => {
        Dictionary<string, object> model = new Dictionary<string, object>(){};
        model.Add("patron", Patron.Find(int.Parse(x.pid)));
        model.Add("book", Book.Find(int.Parse(x.bid)));
        return View["viewBookPatron.cshtml", model];
      };
      Get["/patron/{pid}/checkout/{bid}"] = x => {
        Patron.Find(int.Parse(x.pid)).CheckoutBook(int.Parse(x.bid));
        return View["forward.cshtml", "/patron/"+int.Parse(x.pid)];
      };
      Get["/patron/{pid}/return/{bid}"] = x => {
        Patron.Find(int.Parse(x.pid)).ReturnBook(int.Parse(x.bid));
        return View["forward.cshtml", "/patron/"+int.Parse(x.pid)];
      };
    }
  }
}
