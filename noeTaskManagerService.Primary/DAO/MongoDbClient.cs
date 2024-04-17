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

                _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).AddEnvironmentVariables().Build();
                _connectionString = _configuration["MongoDB:DBConnectionString"];

                

                client = new MongoClient(_connectionString);

                Console.WriteLine($"testConnectionString: {_connectionString}");

            } catch(Exception e)
            {
                throw new Exception($"{e}");
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
