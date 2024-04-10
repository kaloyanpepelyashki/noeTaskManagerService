using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace noeTaskManagerService.Models
{
    public class User(string firstName, string lastName, string email, string? hashedPassword, string? uuid = null)
    {   

        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; init; }
        [BsonElement("UUID")]
        public string UserUKey { get; set; } = uuid ?? Guid.NewGuid().ToString();
        [BsonElement("firstName")]
        public string FirstName { get; set; } = firstName;
        [BsonElement("lastName")]
        public string LastName { get; set; } = lastName;
        [BsonElement("email")]
        public string Email { get; set; } = email;
        [BsonElement("password")]
        public string HashedPassword = hashedPassword ?? " ";
    }
}
