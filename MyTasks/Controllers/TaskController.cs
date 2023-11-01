using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyTasks.Data;
using MyTasks.Models;
using MyTasks.ViewModels;
using System.Collections.Generic;

namespace MyTasks.Controllers
{
    [ApiController]
    [Route("api/MyTasks")]
    public class TaskController : ControllerBase
    {
        //injecao de dependencia pra utilizar o DBcontext
        //private readonly AppDbContext _context;

        //public TaskController(AppDbContext context)
        //{
        //    _context = context;
        //}

        [HttpGet]
        [Route("ToDos")]
        public async Task<ActionResult> GetAsync([FromServices] AppDbContext context)
        {
            var todos = await context.Tasks.AsNoTracking().ToListAsync();

            return Ok(todos);
        }

        [HttpGet]
        [Route("ToDos/{id}")]
        public async Task<ActionResult> GetByIdAsync([FromServices] AppDbContext context, [FromRoute] Guid id)
        {
            var todo = await context.Tasks.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (todo == null)
            {
                return NotFound();
            }
            return Ok(todo);
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult> PostAsync([FromServices] AppDbContext context, [FromBody] CreateToDoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var todo = new ToDo
            {
                Title = model.Title,
                Date = DateTime.Now,
                Done = false
            };

            try
            {
                await context.Tasks.AddAsync(todo);
                await context.SaveChangesAsync();
                return Created(uri: $"api/MyTasks/{todo.Id}", todo);
            }
            catch (Exception e)
            {

                return BadRequest();
            }

        }
        [HttpPut]
        [Route("ToDos/{id}")]
        public async Task<ActionResult> PutAsync([FromServices] AppDbContext context, [FromBody] CreateToDoViewModel model, [FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var todo = await context.Tasks.FirstOrDefaultAsync(c => c.Id == id);

            if (todo == null)
            {
                return NotFound();
            }

            try
            {
                todo.Title = model.Title;

                context.Tasks.Update(todo);
                await context.SaveChangesAsync();

                return Ok(todo);
            }
            catch (Exception e)
            {

                return BadRequest();
            }

        }

        [HttpDelete]
        [Route("ToDos/{id}")]
        public async Task<ActionResult> DeleteAsync([FromServices] AppDbContext context, [FromRoute] Guid id)
        {
            var todo = await context.Tasks.FirstOrDefaultAsync(c => c.Id == id);
            try
            {
                context.Tasks.Remove(todo);
                await context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception e)
            {

                return BadRequest();
            }
        }
    }
}
