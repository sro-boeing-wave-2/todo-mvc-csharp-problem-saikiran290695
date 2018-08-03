using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Modules;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoogleKeepController : ControllerBase
    {
        private static List<Notes> n = new List<Notes>(new[] {
            new Notes(){ Title="Title-1", Message= "message 1", Pinned = false},
            new Notes(){ Title="Title-2", Message= "message 2", Pinned = false},
            new Notes(){ Title="Title-3", Message= "message 3", Pinned = false},
            new Notes(){ Title="Title-1", Message= "message 1", Pinned = false},
            new Notes(){ Title="Title-2", Message= "message 2", Pinned = false},
            new Notes(){ Title="Title-3", Message= "message 3", Pinned = false},
        });
        // GET: api/GoogleKeep
        [HttpGet]
        public List<Notes> Get(
            [FromQuery(Name = "title")]string title,
            [FromQuery(Name = "message")]string message,
            [FromQuery(Name = "Pinned")]bool Pinned
            )
        {
            List<Notes> sample = n.Where(element => element.Title == ((title is null) ? element.Title : title)
                                                 && element.Message == ((message is null) ? element.Message : message)
                                                 && element.Pinned == ((!Pinned) ? element.Pinned : Pinned)
            ).ToList();

            return sample;
            
                
        }
        
        // post: api/GoogleKeep
        [HttpPost]
        public void Post([FromBody] Notes value)
        {
            n.Add(value);
        }

        // PUT: api/GoogleKeep/5
        [HttpPut("{title}")]
        public void Put(string title, [FromBody] Notes value)

        {
            n.ForEach(element => {
                if (element.Title == title) {
                element.Title = ((value.Title is null) ? element.Title : value.Title);
                element.Message = ((value.Message is null) ? element.Title : value.Message);
                element.Pinned = ((value.Pinned == false) ? element.Pinned : value.Pinned);
                }; 
                });
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete()]
        public void Delete(
            [FromQuery(Name = "title")]string title,
            [FromQuery(Name = "message")]string message,
            [FromQuery(Name = "Pinned")]bool Pinned
            )
        {
            n.RemoveAll(element => element.Title == ((title is null) ? element.Title : title)
                                && element.Message == ((message is null) ? element.Message : message)
                                && element.Pinned == ((!Pinned) ? element.Pinned : Pinned));
        }
    }
}
