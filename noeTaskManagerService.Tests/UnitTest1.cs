using FluentAssertions;
using noeTaskManagerService.DAO;

namespace noeTaskManagerService.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void MongoDbClient_GetInstance_ReturnMongoDbClientInstance()
        {
           var result = MongoDbClient.getInstance();


            result.Should().BeOfType<MongoDbClient>();

        }

 
    }
}