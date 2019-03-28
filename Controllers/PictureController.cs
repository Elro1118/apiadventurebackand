using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using apiadventurebackand;
using apiadventurebackand.Modal;

namespace dotnet_sdg_template.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PictureController : ControllerBase
  {
    private readonly DatabaseContext _context;

    public PictureController()
    {
      _context = new DatabaseContext();
    }

    // GET: api/Picture
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Picture>>> GetPictures()
    {
      return await _context.Pictures.ToListAsync();
    }

    // GET: api/Picture/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Picture>> GetPicture(int id)
    {
      var picture = await _context.Pictures.FindAsync(id);

      if (picture == null)
      {
        return NotFound();
      }

      return picture;
    }

    // PUT: api/Picture/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPicture(int id, Picture picture)
    {
      if (id != picture.Id)
      {
        return BadRequest();
      }

      _context.Entry(picture).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!PictureExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return NoContent();
    }

    // POST: api/Picture
    [HttpPost]
    public async Task<ActionResult<Picture>> PostPicture(Picture picture)
    {
      _context.Pictures.Add(picture);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetPicture", new { id = picture.Id }, picture);
    }

    // DELETE: api/Picture/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<Picture>> DeletePicture(int id)
    {
      var picture = await _context.Pictures.FindAsync(id);
      if (picture == null)
      {
        return NotFound();
      }

      _context.Pictures.Remove(picture);
      await _context.SaveChangesAsync();

      return picture;
    }

    private bool PictureExists(int id)
    {
      return _context.Pictures.Any(e => e.Id == id);
    }
  }
}
