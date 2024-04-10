using noeTaskManagerService.Models;

namespace noeTaskManagerService.Services.Interfaces
{
    public interface ITasksService
    {
        
        List<TaskItem> GetAllTasks();
        Task<bool> InsertTaks(TaskItem task);
        Task<bool> DeleteTaskByKey(string taskKey);
        Task<bool> DeleteTaskBySummary(string title);
        Task<bool> UpdateTaskSummary(string taskToUpdateKey, string newSummary);
        Task<bool> UpdateTaskDescription(string taskToUpdateKey, string newDescription);
        Task<bool> UpdateTaskPriority(string taskToUpdateKey, string newPriority);
        Task<bool> UpdateTaskDueDate(string taskToUpdateKey, string newDueDate);
    }
}
