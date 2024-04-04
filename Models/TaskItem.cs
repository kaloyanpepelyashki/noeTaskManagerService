using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace noeTaskManagerService.Models
{
    public class TaskItem(string summary, string description, string priority, string dueDate)
    {
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        [BsonElement("taskKey")]
        public string TaskKey { get; init; } = Guid.NewGuid().ToString();
        [BsonElement("Summary")]
        public string Summary { get; set; } = summary;
        [BsonElement("Description")]
        public string Description { get; set; } = description;
        [BsonElement("Priority")]
        public string Priority { get; set; } = priority;
        [BsonElement("DueDate")]
        public string DueDate { get; set; } = dueDate;
    }
}
