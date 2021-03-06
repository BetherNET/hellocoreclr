using System.Threading.Tasks;
using FluentAssertions;
using HelloCoreClrApp.Data;
using HelloCoreClrApp.WebApi.Actions;
using FakeItEasy;
using Xunit;

namespace HelloCoreClrApp.Test.WebApi.Actions
{
    public class GetTotalNumberOfGreetingsActionTest
    {
        [Fact]
        public async Task ExecuteAsyncTest()
        {
            var dataService = A.Fake<IDataService>();
            A.CallTo(() => dataService.GetNumberOfGreetingsAsync()).Returns(6);
            var sut = new GetTotalNumberOfGreetingsAction(dataService);
            
            var result = await sut.ExecuteAsync();
            
            result.Should().Be(6);
        }
    }
}
