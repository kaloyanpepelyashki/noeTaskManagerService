using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using noeTaskManagerService.Services;
using noeTaskManagerService.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

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

        [HttpPatch("/summary")]
        public async Task<IActionResult> PatchBySummary([FromBody] MutatorRequest mutatorRequest)
        {
            try
            {
                if(String.IsNullOrEmpty(mutatorRequest.targetKey) || String.IsNullOrWhiteSpace(mutatorRequest.mutationValue))
                {
                    return StatusCode(400, "Empty input was provided for one or more parameters");
                }

                var result = await _tasksSevice.UpdateTaskSummary(mutatorRequest.targetKey, mutatorRequest.mutationValue);
                if(result)
                {
                    return Ok("Task updated");
                } else
                {
                    return BadRequest();
                }
            } catch(Exception)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPatch("/description")]
        public async Task<IActionResult> PatchByDescription([FromBody] MutatorRequest mutatorRequest)
        {
            try
            {
                if (String.IsNullOrEmpty(mutatorRequest.targetKey) || String.IsNullOrWhiteSpace(mutatorRequest.mutationValue))
                {
                    return StatusCode(400, "Empty input was provided for one or more parameters");
                }

                var result = await _tasksSevice.UpdateTaskDescription(mutatorRequest.targetKey, mutatorRequest.mutationValue);
                if (result)
                {
                    return Ok("Task updated");
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPatch("/priority")]
        public async Task<IActionResult> PatchByPriority([FromBody] MutatorRequest mutatorRequest)
        {
            try
            {
                if (String.IsNullOrEmpty(mutatorRequest.targetKey) || String.IsNullOrWhiteSpace(mutatorRequest.mutationValue))
                {
                    return StatusCode(400, "Empty input was provided for one or more parameters");
                }

                var result = await _tasksSevice.UpdateTaskPriority(mutatorRequest.targetKey, mutatorRequest.mutationValue);
                if (result)
                {
                    return Ok("Task updated");
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPatch("/dueDate")]
        public async Task<IActionResult> PatchByDueDate([FromBody] MutatorRequest mutatorRequest)
        {
            try
            {
                if (String.IsNullOrEmpty(mutatorRequest.targetKey) || String.IsNullOrWhiteSpace(mutatorRequest.mutationValue))
                {
                    return StatusCode(400, "Empty input was provided for one or more parameters");
                }

                var result = await _tasksSevice.UpdateTaskDueDate(mutatorRequest.targetKey, mutatorRequest.mutationValue);
                if (result)
                {
                    return Ok("Task updated");
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }


        public class MutatorRequest
        {
            [Required]
            public string targetKey { get; set; }
            [Required]
            public string mutationValue { get; set; }

        }

    }
    
}
