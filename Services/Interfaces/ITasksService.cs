using noeTaskManagerService.Models;

namespace noeTaskManagerService.Services.Interfaces
{
    public interface ITasksService
    {
        Task<List<TaskItem>> getAllTasks();
    }
}
