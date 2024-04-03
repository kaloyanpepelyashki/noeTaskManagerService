using MongoDB.Bson;
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

        private TasksService()
        {
            _mongoDbDao = MongoDbClient.getInstance();
            _database = _mongoDbDao.client.GetDatabase("TaskManager");
            _collection = _database.GetCollection<TaskItem>("Tasks");

        }
        public static TasksService getInstance()
        {
            if( _instance == null )
            {
                _instance = new TasksService();
            }

            return _instance;
        }

        //Gets all tasks from the database
        public List<TaskItem> getAllTasks()
        { 
            try
            {
                var result = _collection.Find(new BsonDocument()).ToList();

                if(result == null)
                {
                    return null;
                } else
                {
                    return result;
                }
            } catch(Exception e)
            {
                throw new Exception($"{e}");
            }
        }

        public async Task<bool> insertTaks(TaskItem task)
        {
            try
            {
                await _collection.InsertOneAsync(task);
                return true;

            } catch(Exception e)
            {
                throw new Exception($"{e}");
            }
        }

    }
}
