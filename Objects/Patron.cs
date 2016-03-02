using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;
using JensenNS.Objects;
using System.Linq;

namespace LibraryNS.Objects
{
  public class Patron : DBHandler
  {
    private int _id;
    private string _name;
    public static string NameColumn = "name";
    public static string Table = "people";

    public Patron(string name, int id = 0)
    {
      _id = id;
      _name = name;
    }

    public int GetId()
    {
    return _id;
    }

    public void SetId(int newId)
    {
      _id = newId;
    }
    public string GetName() { return _name; }
    public void SetName(string name) { _name = name; }

    public void Save()
    {
      List<string> columns = new List<string>{NameColumn};
      List<SqlParameter> parameters = new List<SqlParameter>{
        new SqlParameter("@"+NameColumn, GetName())
      };
      _id = base.Save(Table, columns, parameters, GetId());
    }
    public void Delete()
    {
      DBHandler.Delete(Table, _id);
    }
    public static List<Patron> GetAll()
    {
      List<Object> fromDB = DBHandler.GetAll(Table, MakeObject);
      return fromDB.Cast<Patron>().ToList();
    }
    public static void DeleteAll()
    {
      DBHandler.DeleteAll(Table);
    }
    public override bool Equals(Object otherPatron)
    {
      if (!(otherPatron is Patron))
      {
        return false;
      }
      else
      {
        Patron newPatron = (Patron) otherPatron;
        bool idEquality = this.GetId() == newPatron.GetId();
        bool nameEquality = this.GetName() == newPatron.GetName();
        return (idEquality && nameEquality);
      }
    }
    public static Object MakeObject(SqlDataReader rdr)
    {
      return new Patron(rdr.GetString(1), rdr.GetInt32(0));
    }
    public static Patron Find(int id)
    {
      SqlParameter parameter = new SqlParameter("@id", id );
      string query = "WHERE id = @id";
      return (Patron) DBHandler.GetObjectFromDB(Table, query, MakeObject, parameter);
    }
  } // end class
} // end namespace
