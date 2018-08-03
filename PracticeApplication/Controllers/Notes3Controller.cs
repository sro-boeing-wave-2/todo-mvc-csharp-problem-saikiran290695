using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticeApplication.Models;
using PracticeApplication.Classes;

namespace PracticeApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Notes3Controller : ControllerBase
    {
        private readonly NotesTry3Context _context;

        public Notes3Controller(NotesTry3Context context)
        {
            _context = context;
        }

        // GET: api/Notes3
        [HttpGet("Notes")]
        public IEnumerable<Notes> GetNotes()
        {
            return _context.Notes;
        }
        [HttpGet("Checklist")]
        public IEnumerable<CheckList> GetChecklist()
        {
            return _context.CheckLists;
        }
        [HttpGet("Label")]
        public IEnumerable<Label> GetLabel()
        {
            return _context.Labels;
        }
        // GET: api/Notes3/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNotes([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var notes = await _context.Notes.FindAsync(id);

            if (notes == null)
            {
                return NotFound();
            }

            return Ok(notes);
        }

        // PUT: api/Notes3/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNotes([FromRoute] int id, [FromBody] Notes notes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != notes.Id)
            {
                return BadRequest();
            }

            _context.Entry(notes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotesExists(id))
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

        // POST: api/Notes3
        [HttpPost]
        public async Task<IActionResult> PostNotes([FromBody] Notes Note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Notes.Add(Note);
         //   Note.checklist.ForEach(e => _context.CheckLists.Add(e));
           // commondata.checklist.ForEach(e => _context.CheckLists.Add(e));
            // _context.Labels.Add(commondata.label);
            // _context.CheckLists.Add(commondata.checklist);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNotes", new { id = Note.Id }, Note);
        }

        // DELETE: api/Notes3/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotes([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var notes = await _context.Notes.FindAsync(id);
            if (notes == null)
            {
                return NotFound();
            }

            _context.Notes.Remove(notes);
            await _context.SaveChangesAsync();

            return Ok(notes);
        }

        private bool NotesExists(int id)
        {
            return _context.Notes.Any(e => e.Id == id);
        }
    }
}