using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectX.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ProjectX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly NoteContext _context;

        public NotesController(NoteContext context)
        {
            _context = context;
        }

        // GET: api/Notes
        [HttpGet]
        public IEnumerable<Note> GetNote()
        {
            return _context.Note.Include(x=>x.CheckList).Include(x=>x.Label);
        }

        // GET: api/Notes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNote([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            List<Note> temp = new List<Note>(); 
            //var note = from Lists in _context.Note.Include(x => x.CheckList).Include(x => x.Label)
            //            where Lists.Title == id 
            //            select Lists;
            await _context.Note.Include(x => x.CheckList).Include(x => x.Label).ForEachAsync(Lists =>
            {
                Lists.CheckList.ForEach(element => {
                    if (element.Equals(id))
                     temp.Add(Lists);
            });
            });
            var note = temp;

            //var note =  _context.Note.Include(x => x.CheckList).Include(x => x.Label);

            Console.WriteLine(note);
                //Note.Include(x => x.CheckList).Include(x => x.Label);

            if (note == null)
            {
                return NotFound();
            }

            return Ok(note);
        }

        // PUT: api/Notes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNote([FromRoute] int id, [FromBody] Note note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != note.Id)
            {
                return BadRequest();
            }
            
            //Console.WriteLine(note.CheckList.ToString() + note.Label.ToString);
           await _context.Note.Include(x => x.CheckList).Include(x => x.Label).ForEachAsync(element => {
               //  element.Id = note.Id;

               if (element.Id == note.Id)
               {
                   element.Message = note.Message;
                   element.Pinned = note.Pinned;
                   element.Title = note.Title;
                   List<Label> temp = note.Label;
                   List<Label> actual = element.Label;


                   temp.ForEach(label =>
                   {
                       actual.ForEach(actualLabel =>
                       {
                           if (label.Id == actualLabel.Id)
                           {
                               actualLabel.label = label.label;
                               actualLabel.
                           }
                       });
                   });
                   element.Label.AddRange(temp);
               }
           });
            //_context.Entry(note).State = EntityState.Modified;
            //_context.Entry(note.CheckList).State = EntityState.Modified;
            //_context.Entry(note.Label).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetNote", new { id = note.Id }, note);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            
        }

        // POST: api/Notes
        [HttpPost]
        public async Task<IActionResult> PostNote([FromBody] Note note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Note.Add(note);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNote", new { id = note.Id }, note);
        }

        // DELETE: api/Notes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var note = await _context.Note.Include(x => x.CheckList).Include(x => x.Label).SingleOrDefaultAsync(x => x.Id == id);
            if (note == null)
            {
                return NotFound();
            }

            _context.Note.Remove(note);
            await _context.SaveChangesAsync();

            return Ok(note);
        }

        private bool NoteExists(int id)
        {
            return _context.Note.Any(e => e.Id == id);
        }
    }
}