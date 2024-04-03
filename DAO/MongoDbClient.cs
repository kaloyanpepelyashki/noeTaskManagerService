using MongoDB.Driver;

namespace noeTaskManagerService.DAO
{
    public class MongoDbClient
    {
        protected static MongoDbClient _instance;

        protected readonly string _connectionString;
        protected readonly IConfiguration _configuration;

        public IMongoClient client;

        protected MongoDbClient()
        {
            try
            {
                _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").AddEnvironmentVariables().Build();
                _connectionString = _configuration.GetValue<string>("MongoDB:ConnectionString");
                client = new MongoClient(_connectionString);
            } catch(Exception e)
            {
                throw new Exception($"e", e);
            }

        }

        public static MongoDbClient getInstance()
        {   
            if(_instance == null)
            {
                _instance = new MongoDbClient();
            }

            return _instance;
        }

    }
}
