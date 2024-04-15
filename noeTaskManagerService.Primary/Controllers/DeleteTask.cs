using Microsoft.AspNetCore.Mvc;
using noeTaskManagerService.Services;
using noeTaskManagerService.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace noeTaskManagerService.Controllers
{
    public class DeleteTask : Controller
    {
        private readonly ITasksService _tasksService;

        public DeleteTask()
        {
            _tasksService = TasksService.GetInstance();
        }

        [HttpDelete("deleteByKey")]
        public async Task<IActionResult> DeleteATask([FromBody] DeleteRequest deleteRequest)
        {
            try
            {
                var response = await _tasksService.DeleteTaskByKey(deleteRequest.TaskKey);

                if(response)
                {
                    return Ok("Task was deleted");

                } else
                {
                    return BadRequest("Failed to delete task");
                }
            } catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }


        public class DeleteRequest
        {
            [Required]
            public string TaskKey { get; set; }
        }
    }
}
