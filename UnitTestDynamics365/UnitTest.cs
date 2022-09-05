using FakeXrmEasy;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using Xunit;
using Assert = Xunit.Assert;

namespace UnitTestDynamics365;

public class UnitTest : FakeXrmEasyTestsBase
{
    [Fact]
    public async Task MyFirstTest()
    {
        var account1 = new Entity("account") { Id = Guid.NewGuid() };
        var account2 = new Entity("account") { Id = Guid.NewGuid() };
        
        
        var executeMultipleRequest = new ExecuteMultipleRequest
        {
            Settings = new ExecuteMultipleSettings
            {
                ReturnResponses = true
            },
            Requests = new OrganizationRequestCollection
            {
                new CreateRequest
                {
                    Target = account1
                },

                new CreateRequest
                {
                    Target = account2
                }
            }
        };

        var response = await _service.ExecuteAsync(executeMultipleRequest) as ExecuteMultipleResponse;
       
        Assert.False(response.IsFaulted);
        Assert.NotEmpty(response.Responses);
        
        Assert.NotNull(_service.Retrieve("account", account1.Id, new ColumnSet(true)));
        Assert.NotNull(_service.Retrieve("account", account2.Id, new ColumnSet(true)));
    }
}