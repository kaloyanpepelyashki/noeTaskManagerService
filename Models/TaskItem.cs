namespace noeTaskManagerService.Models
{
    public class TaskItem
    {
        public string taskKey { get; init; }
        public string summary { get; set; }
        public string description { get; set; }
        public string priority { get; set; }
        public DateOnly dueDate { get; set; }

        public TaskItem(string summary, string description, string priority, DateOnly dueDate)
        {
            summary = summary;
            description = description;
            priority = priority;
            dueDate = dueDate;
        }
    }
}
