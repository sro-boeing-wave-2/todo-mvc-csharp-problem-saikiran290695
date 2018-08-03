using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

using ProjectX.Models;

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
        [HttpGet]
        public IActionResult GetNoteByPrimitive(
            [FromQuery(Name = "Id")] int Id,
            [FromQuery(Name = "Title")] string Title,
            [FromQuery(Name = "Message")] string Message,
            [FromQuery(Name = "Pinned")] string Pinned,
            [FromQuery(Name = "Label")] string Label) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            List<Note> temp = new List<Note>();
            temp = _context.Note.Include(x => x.CheckList).Include(x => x.Label)
                .Where(element => element.Title == ((Title == null) ? element.Title : Title)
                      && element.Message == ((Message == null) ? element.Message : Message)
                      && element.Pinned == ((Pinned == null) ? element.Pinned : Convert.ToBoolean(Pinned))
                      && element.Id == ((Id == 0) ? element.Id : Id)
                      && element.Label.Any(x => (Label != null) ? x.label == Label : true )).ToList();
            if (temp == null)
            {
                return NotFound();
            }

            return Ok(temp);
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
            await _context.Note.Include(x => x.CheckList).Include(x => x.Label).ForEachAsync(element =>
            {
                if (element.Id == note.Id)
                {
                    element.Message = note.Message;
                    element.Pinned = note.Pinned;
                    element.Title = note.Title;
                    _context.Label.RemoveRange(element.Label);
                    element.Label.AddRange(note.Label);
                    _context.CheckList.RemoveRange(element.CheckList);
                    element.CheckList.AddRange(note.CheckList);
                }
            });
            
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
            return CreatedAtAction(nameof(GetNoteByPrimitive), new { id = note.Id }, note);
        }
        // DELETE: api/Notes/5
        [HttpDelete]
        public async Task<IActionResult> DeleteNote(
            [FromQuery(Name = "Id")] int Id,
            [FromQuery(Name = "Title")] string Title,
            [FromQuery(Name = "Message")] string Message,
            [FromQuery(Name = "Pinned")] string Pinned,
            [FromQuery(Name = "Label")] string Label)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            List<Note> note = _context.Note.Include(x => x.CheckList).Include(x => x.Label)
                .Where(element =>
                element.Id == ((Id == 0) ? element.Id : Id)
                && element.Title == ((Title == null) ? element.Title : Title)
                && element.Message == ((Message == null) ? element.Message : Message)
                && element.Pinned == ((Pinned == null) ? element.Pinned : Convert.ToBoolean(Pinned))
                && element.Label.Any(label => label.label == ((Label == null) ? label.label: Label))             
                ).ToList();
            if (note == null)
            {
                return NotFound();
            }
            note.ForEach(NoteDel => _context.CheckList.RemoveRange(NoteDel.CheckList));
            note.ForEach(NoteDel => _context.Label.RemoveRange(NoteDel.Label));
            _context.Note.RemoveRange(note);    
            
            await _context.SaveChangesAsync();
            return Ok(note);
        }
        private bool NoteExists(int id)
        {
            return _context.Note.Any(e => e.Id == id);
        }
    }
}