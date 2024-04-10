using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using noeTaskManagerService.Services;
using noeTaskManagerService.Services.Interfaces;

namespace noeTaskManagerService.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class GetTask : ControllerBase
    {   
        private readonly ITasksService _tasksService;
        public GetTask()
        {
            _tasksService = TasksService.GetInstance();
        }
    
        [HttpGet("all")]
        public IActionResult All()
        {
            try
            {
                var result = _tasksService.GetAllTasks();
                if(result != null)
                {
                    return Ok(result);

                } else
                {
                    return NotFound("No tasks were found");
                }

            } catch(Exception e)
            {
                throw new Exception($"Error getting all tasks: {e}");
            }
        }
    }
}
