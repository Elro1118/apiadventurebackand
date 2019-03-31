using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apiadventurebackand;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace dotnet_sdg_template.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class SearchController : ControllerBase
  {

    private DatabaseContext db;
    public SearchController()
    {
      this.db = new DatabaseContext();

    }

    [HttpGet("pictures")]
    public ActionResult SearchForPicture([FromQuery] string query)
    {
      query = query.ToLower();
      var results = db.Pictures.Where(w => w.Description.ToLower().Contains(query) || w.Title.ToLower().Contains(query));

      return Ok(new { SearchingFor = query, results = results });
    }

  }
}
