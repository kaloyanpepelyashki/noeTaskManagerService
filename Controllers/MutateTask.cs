using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using noeTaskManagerService.Services;
using noeTaskManagerService.Services.Interfaces;

namespace noeTaskManagerService.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class MutateTask : Controller
    {
        private readonly ITasksService _tasksSevice;

        public MutateTask()
        {
            _tasksSevice = TasksService.GetInstance();
        }

        [HttpPatch("/bySummary")]
        public async Task<IActionResult> PatchBySummary([FromBody] string taskKey, string newSummary)
        {
            try
            {
                var result = await _tasksSevice.UpdateTaskSummary(taskKey, newSummary);
                if(result)
                {
                    return Ok("Task updated");
                } else
                {
                    return BadRequest();
                }
            } catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
