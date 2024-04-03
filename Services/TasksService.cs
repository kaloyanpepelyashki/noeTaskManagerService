using MongoDB.Driver;
using noeTaskManagerService.DAO;
using noeTaskManagerService.Models;
using noeTaskManagerService.Services.Interfaces;

namespace noeTaskManagerService.Services
{
    public class TasksService: ITasksService
    {
        private static TasksService _instance;

        protected MongoDbClient _mongoDbDao;
        protected IMongoDatabase _database;
        protected IMongoCollection<TaskItem> _collection;


        public static TasksService getInstance()
        {
            if( _instance == null )
            {
                _instance = new TasksService();
            }

            return _instance;
        }

        public Task<List<TaskItem>> getAllTasks();

    }
}
