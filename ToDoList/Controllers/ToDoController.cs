using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace ToDoList.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoController : Controller
    {
        ToDoDb db;
        public ToDoController(ToDoDb context)
        {
            db = context;
            if (!db.ToDos.Any())
            {
                db.ToDos.Add(new ToDo { Id = 4, Name = "Tom", Description = "chtoto kupit" });
                db.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDo>>> Get()
        {
            return await db.ToDos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ToDo>> Get(int id) 
        {
            var todo = await db.ToDos.FirstOrDefaultAsync(x => x.Id == id);
            if (todo == null) return NotFound();
            return new ObjectResult(todo);
        }

        [HttpPost]
        public async Task<ActionResult<ToDo>> Post(ToDo todo) {
            if (todo == null) return BadRequest();

            db.ToDos.Add(todo);
            await db.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ToDo>> Put(int id, ToDo updateList)
        {
            var td = await db.ToDos.FindAsync(id);
            if (updateList == null) return BadRequest();
            if (td == null) return NotFound();
            td.Name = updateList.Name;
            td.Description = updateList.Description;
            await db.SaveChangesAsync();
            return Ok(updateList);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ToDo>> Delete(int id)
        {
            var todo = await db.ToDos.FirstOrDefaultAsync(x => x.Id == id);
            if (todo == null) return NotFound();
            db.ToDos.Remove(todo);
            await db.SaveChangesAsync();
            return Ok(todo);
        }
    }
}
