using Microsoft.AspNetCore.Mvc;
using TodosWebApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodosWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        // GET: api/<TodoController>
        [HttpGet]
        public List<Todo> Get()
        {
            var db = new Dbconnection();
            var todos = db.GetTodos();
            return todos;   
        }

        // GET api/<TodoController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
           var db = new Dbconnection();
           var todo = db.GetTodo(id);  
            if (todo == null)
            {
                return NotFound();
            }
            return Ok(todo);
        }

        // POST api/<TodoController>
        [HttpPost]
        public void Post([FromBody] Todo todo)
        {
            var db = new Dbconnection();
            db.SaveTodo(todo);  
        }

        // PUT api/<TodoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Todo todo)
        {
            var db = new Dbconnection();    
           db.UpdateTodo(id, todo);
        }

        // DELETE api/<TodoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var db = new Dbconnection();    
            db.DeleteTodo(id);  
        }
    }
}
