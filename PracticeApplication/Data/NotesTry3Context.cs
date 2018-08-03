using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PracticeApplication.Models
{
    public class NotesTry3Context : DbContext
    {
        public NotesTry3Context (DbContextOptions<NotesTry3Context> options)
            : base(options)
        {
        }
        
        public DbSet<PracticeApplication.Models.Notes> Notes { get; set; }
        public DbSet<PracticeApplication.Models.CheckList> CheckLists { get; set; }
        public DbSet<PracticeApplication.Models.Label> Labels { get; set; }


    }
}
