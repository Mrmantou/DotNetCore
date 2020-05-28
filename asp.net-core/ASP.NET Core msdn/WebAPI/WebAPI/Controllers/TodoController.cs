using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-2.1#create
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : Controller
    {
        private readonly TodoContext context;
        public TodoController(TodoContext context)
        {
            this.context = context;

            if (context.TodoItems.Count() == 0)
            {
                context.TodoItems.Add(new TodoItem { Name = "Item1" });
                context.TodoItems.Add(new TodoItem { Name = "Item2" });
                context.SaveChanges();
            }
        }

        [HttpGet]
        public ActionResult<List<TodoItem>> GetAll()
        {
            return context.TodoItems.ToList();
        }

        [HttpGet("{id}", Name = "GetTodo")]
        public ActionResult<TodoItem> GetById(long id)
        {
            var item = context.TodoItems.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        [HttpPost]
        public IActionResult Create(TodoItem item)
        {
            context.TodoItems.Add(item);
            context.SaveChanges();

            return CreatedAtRoute("GetTodo", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, TodoItem item)
        {
            var todo = context.TodoItems.Find(id);
            if(todo==null)
            {
                return NotFound();
            }

            todo.Name = item.Name;
            todo.IsComplete = item.IsComplete;

            context.TodoItems.Update(todo);

            context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = context.TodoItems.Find(id);

            if(todo==null)
            {
                return NotFound();
            }

            context.TodoItems.Remove(todo);
            context.SaveChanges();

            return NoContent();
        }
    }
}
