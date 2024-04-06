using MongoDB.Driver;
using noeTaskManagerService.DAO;
using noeTaskManagerService.Services.Interfaces;
using noeTaskManagerService.Models;

namespace noeTaskManagerService.Services
{
    public class AuthService: IAuthService 
    {
        private static AuthService _instance;
        protected MongoDbClient _mongoDbDao;
        protected IMongoDatabase _database;
        protected IMongoCollection<User> _collection;

        protected AuthService()
        {
            _mongoDbDao = MongoDbClient.getInstance();
            _database = _mongoDbDao.client.GetDatabase("Users");
            _collection = _database.GetCollection<User>("UsersAccounts");
        }

        public static AuthService GetInstance()
        {
            if(_instance == null)
            {
                _instance = new AuthService();
            }

            return _instance;
        }

        protected async Task<User?> FindUser(string email)
        {
            try
            {
                var filterBuilder = Builders<User>.Filter;
                var filter = filterBuilder.Eq("email", email);

                var result = await _collection.Find(filter).FirstOrDefaultAsync();

                return result;

            } catch(Exception e) 
            {
                throw new Exception($"{e}");
            }
        }

        public async Task<User?> SignIn(string email, string hashedPassword)
        {
            try
            {
                var filterBuilder = Builders<User>.Filter;
                var filter = filterBuilder.Eq("email", email) & filterBuilder.Eq("password", hashedPassword);

                var result = await _collection.Find(filter).FirstOrDefaultAsync();

                if (result != null)
                {
                    var user = new User(result.FirstName, result.LastName, result.Email, result.HashedPassword, result.UserUKey);
                    return user;

                } else
                {
                    return null;
                }

            } catch(Exception e)
            {
                throw new Exception($"{e}");
            }
        }

        public async Task<User?> SignUp(string firstName, string lastName, string email, string hashedPassword)
        {
            try
            {
                var filter = Builders<User>.Filter.Eq("email", email);
                var emailExists = await _collection.Find(filter).FirstOrDefaultAsync();

                if(emailExists != null)
                {
                    throw new AuthException("User with this email already exists");
                }

                var newUser = new User(firstName, lastName, email, hashedPassword);
                await _collection.InsertOneAsync(newUser);

                var userProfile = await FindUser(email);

                if (userProfile != null)
                {
                    throw new AuthException("Error creating user profile");
                }

                return userProfile;

            } catch(Exception e)
            {
                throw new Exception($"{e}");
            }
        }
    }

    public class AuthException : Exception
    {
        public AuthException(string message) : base(message)
        {
        }
    }
}
