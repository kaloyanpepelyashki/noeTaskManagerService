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
            _database = _mongoDbDao.client.GetDatabase("UsersDB");
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
        /* 
        * This is the SignUp method, that takes in email and password parameters to sign user in.
        */
        public async Task<User?> SignIn(string email, string password)
        {
            try
            {   
                var filterBuilder = Builders<User>.Filter;
                var filter = filterBuilder.Eq("email", email);

                //Checks if the email exists in the database
                var result = await _collection.Find(filter).FirstOrDefaultAsync();

                if (result != null)
                {
                    var storedHashedPassword = result.HashedPassword;
                    //Verifies the password matches the stored in the DB password
                    var passwordVerification = PasswordEncryptor.VerifyPassword(password, storedHashedPassword);

                    if (!passwordVerification)
                    {
                        //Throws an exception in case the passwords don't mathc
                        throw new AuthException("Wrong email or password");
                    }

                    var user = new User(result.FirstName, result.LastName, result.Email, result.HashedPassword, result.UserUKey);
                    return user;

                } else
                {
                    throw new AuthException("Wrong email or password");
                }

            } catch(Exception e)
            {
                throw new Exception($"{e}");
            }
        }
        /* 
         * This is the SignUp method, that takes in all parameters needed to create a user in the database.
         */
        public async Task<User?> SignUp(string firstName, string lastName, string email, string hashedPassword)
        {
            try
            {
                var filter = Builders<User>.Filter.Eq("email", email);
                //Checks if the email already exists in the database
                var emailExists = await _collection.Find(filter).FirstOrDefaultAsync();

                if(emailExists != null)
                {
                    throw new AuthException("User with this email already exists");
                }

                var newUser = new User(firstName, lastName, email, hashedPassword);
                await _collection.InsertOneAsync(newUser);

                //Tryes to find the newly created profile of the user in the database
                var userProfile = await FindUser(email);

                if (userProfile == null)
                {
                    //If the newly created profile is not found, the application assummes there was a problem creating it and throws an exception
                    throw new AuthException("Error creating user profile");
                }

                return userProfile;

            } catch(Exception e)
            {
                throw new Exception($"{e}");
            }
        }
    }

    //The auth exception class, which is a custom exception class, for throwing exceptions related to authentication
    public class AuthException : Exception
    {
        public AuthException(string message) : base(message)
        {
        }
    }
}
