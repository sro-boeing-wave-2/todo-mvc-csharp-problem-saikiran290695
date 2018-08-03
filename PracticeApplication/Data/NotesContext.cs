using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PracticeApplication.Models
{
    public class NotesContext : DbContext
    {
        public NotesContext (DbContextOptions<NotesContext> options)
            : base(options)
        {
        }

        public DbSet<PracticeApplication.Models.Notes> Notes { get; set; }
    }
}
