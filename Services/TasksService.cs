using MongoDB.Bson;
using MongoDB.Driver;
using noeTaskManagerService.DAO;
using noeTaskManagerService.Models;
using noeTaskManagerService.Services.Interfaces;

namespace noeTaskManagerService.Services
{
    //Handles all interactions with the Tasks collection in the MongoDb
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
        public static TasksService GetInstance()
        {
            if( _instance == null )
            {
                _instance = new TasksService();
            }

            return _instance;
        }

        //Gets all tasks from the database
        public List<TaskItem>? GetAllTasks()
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

        public async Task<bool> InsertTaks(TaskItem task)
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

        public async Task<bool> DeleteTaskByKey(string taskKey)
        {
            try
            {
                var filter = Builders<TaskItem>.Filter.Eq("taskKey", taskKey);
                var checkValidity = _collection.Find(filter).First();

                if(checkValidity != null)
                {
                    await _collection.DeleteOneAsync(filter);
                    return true;

                } else
                {
                    throw new Exception($"No item with key {taskKey} in database");
                }

            } catch(Exception e)
            {
                throw new Exception($"{e}");
            }
        }

        public async Task<bool> DeleteTaskBySummary(string taskSummary)
        {
            try
            {
                var filter = Builders<TaskItem>.Filter.Eq("taskSummary", taskSummary);
                var checkValidity = _collection.Find(filter).First();

                if( checkValidity != null)
                {
                    await _collection.DeleteOneAsync(filter);
                    return true;
                } else
                {
                    throw new Exception($"No item with summary {taskSummary} in database");
                }
            }
            catch (Exception e)
            {
                throw new Exception($"{e}");
            }
        }

        public async Task<bool>? UpdateTaskSummary(string taskToUpdateKey, string newSummary)
        {
            try
            {
                var filter = Builders<TaskItem>.Filter.Eq("taskKey", taskToUpdateKey);
                var checkValidity = _collection.Find(filter).First();

                if (checkValidity != null)
                {
                    var patch = Builders<TaskItem>.Update.Set("summary", newSummary);
                    var updateResult = await _collection.UpdateOneAsync(filter, patch);

                    return updateResult.IsAcknowledged;
                }
                else
                {
                    throw new Exception("No item with key {taskToUpdateKey} in database");
                }
            }
            catch (Exception e)
            {
                throw new Exception($"{e}");
            }
        }

        public async Task<bool>? UpdateTaskDescription(string taskToUpdateKey, string newDescription)
        {
            try
            {
                var filter = Builders<TaskItem>.Filter.Eq("taskKey", taskToUpdateKey);
                var checkValidity = _collection.Find(filter).First();

                if (checkValidity != null)
                {
                    var patch = Builders<TaskItem>.Update.Set("description", newDescription);
                    var updateResult = await _collection.UpdateOneAsync(filter, patch);

                    return updateResult.IsAcknowledged;
                }
                else
                {
                    throw new Exception("No item with key {taskToUpdateKey} in database");
                }

            }
            catch (Exception e)
            {
                throw new Exception($"{e}");
            }
        }

        public async Task<bool>? UpdateTaskPriority(string taskToUpdateKey, string newPriority)
        {
            try
            {
                var filter = Builders<TaskItem>.Filter.Eq("taskKey", taskToUpdateKey);
                var checkValidity = _collection.Find(filter).First();

                if (checkValidity != null)
                {
                    var patch = Builders<TaskItem>.Update.Set("priority", newPriority);
                    var updateResult = await _collection.UpdateOneAsync(filter, patch);

                    return updateResult.IsAcknowledged;
                }
                else
                {
                    throw new Exception("No item with key {taskToUpdateKey} in database");
                }
            }
            catch (Exception e)
            {
                throw new Exception($"{e}");
            }
        }
        public async Task<bool>? UpdateTaskDueDate(string taskToUpdateKey, string newDueDate)
        {
            try
            {
                var filter = Builders<TaskItem>.Filter.Eq("taskKey", taskToUpdateKey);
                var checkValidity = _collection.Find(filter).First();

                if (checkValidity != null)
                {
                    var patch = Builders<TaskItem>.Update.Set("dueDate", newDueDate);
                    var updateResult = await _collection.UpdateOneAsync(filter, patch);

                    return updateResult.IsAcknowledged;
                }
                else
                {
                    throw new Exception("No item with key {taskToUpdateKey} in database");
                }
            }
            catch (Exception e)
            {
                throw new Exception($"{e}");
            }
        }

    }
}
