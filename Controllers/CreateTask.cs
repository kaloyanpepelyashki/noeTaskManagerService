using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using noeTaskManagerService.Services;
using noeTaskManagerService.Services.Interfaces;
using noeTaskManagerService.Models;

namespace noeTaskManagerService.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class CreateTask : Controller
    {
        private readonly ITasksService _tasksService;

        public CreateTask()
        {
            _tasksService = TasksService.GetInstance();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] NewTask newTask)
        {
            try
            {
                var taskToCreate = new TaskItem(newTask.summary, newTask.description, newTask.priority, newTask.dueDate);
                var result = await _tasksService.InsertTaks(taskToCreate);

                if(result)
                {
                    return Ok("Task created");
                } else
                {
                    return BadRequest("Error creating task");
                }

            } catch(Exception e)
            {
                return BadRequest($"Error creating task: {e}");
            }
        }

        public class NewTask
        {
            public string summary { get; set; }
            public string description { get; set; }
            public string priority { get; set; }
            public string dueDate { get; set; }

        }
    }

   
}
