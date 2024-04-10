using FluentAssertions;
using noeTaskManagerService.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace noeTaskManagerService.Tests
{
    public class MongoDbClientTest
    {
        [Fact]
        public void MongoDbClient_GetInstance_ReturnMongoDbClientInstance()
        {
            //Act
            var result = MongoDbClient.getInstance();

            //Assert
            result.Should().BeOfType<MongoDbClient>();

        }
    }
}
