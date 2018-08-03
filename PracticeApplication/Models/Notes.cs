using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PracticeApplication.Models
{
    public class Notes
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public List<CheckList> checklist { get; set; }
        public List<Label> label { get; set; }
    }
    public class CheckList
    {   
        [Key]
        public int Id { get; set; }
        public string checklist { get; set; }
        
    }
    public class Label
    {   
        
        [Key]
        public int Id { get; set; }
        public string label { get; set; }
       
    }


}
