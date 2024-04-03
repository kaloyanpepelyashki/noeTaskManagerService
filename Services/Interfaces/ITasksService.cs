using noeTaskManagerService.Models;

namespace noeTaskManagerService.Services.Interfaces
{
    public interface ITasksService
    {
        Task<List<TaskItem>> getAllTasks();
        Task<bool> insertTaks(TaskItem task);
    }
}
