using Nancy;
using LibraryNS.Objects;
using System.Collections.Generic;
namespace LibraryNS
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        return View["header.cshtml"];
      };
    }
  }
}
