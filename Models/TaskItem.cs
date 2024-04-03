using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace noeTaskManagerService.Models
{
    public class TaskItem
    {
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public Object ObjectId { get; set; }
        [BsonElement("taskKey")]
        public string taskKey { get; init; }
        [BsonElement("Summary")]
        public string summary { get; set; }
        [BsonElement("Description")]
        public string description { get; set; }
        [BsonElement("Priority")]
        public string priority { get; set; }
        [BsonElement("DueDate")]
        public DateOnly dueDate { get; set; }

        public TaskItem(string summary, string description, string priority, DateOnly dueDate)
        {   

            this.summary = summary;
            this.description = description;
            this.priority = priority;
            this.dueDate = dueDate;
        }
    }
}
