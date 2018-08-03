using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Modules
{
    public class NotesClass
    {
        [Key]       
        public int Id;
        public string Title;
        public string Message;
        public bool IsPin;
    }
}
