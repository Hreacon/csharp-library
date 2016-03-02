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
       Library.DeleteAll();
     }
  }
}
