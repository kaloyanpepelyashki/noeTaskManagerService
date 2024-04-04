using noeTaskManagerService.Models;

namespace noeTaskManagerService.Services.Interfaces
{
    public interface ITasksService
    {
        
        List<TaskItem> GetAllTasks();
        Task<bool> InsertTaks(TaskItem task);
        Task<bool> DeleteTaskByKey(string taskKey);
    }
}
