using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Modules
{
    public class Notes
    {
      
        [Key] [Required]
        public string Title { get; set; }

        public string Message;
        //public List<string> CheckList;
        public bool? Pinned;
    }
}
