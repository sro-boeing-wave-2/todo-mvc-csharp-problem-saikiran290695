using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApi.Modules;

namespace WebApi.Models
{
    public class NotesContext : DbContext
    {
        public NotesContext (DbContextOptions<NotesContext> options)
            : base(options)
        {
        }

        public DbSet<WebApi.Modules.Notes> Notes { get; set; }
    }
}
