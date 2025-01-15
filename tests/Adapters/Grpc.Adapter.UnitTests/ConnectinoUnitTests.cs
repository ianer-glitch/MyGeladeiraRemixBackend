

using Identity.Domain.Protos;

namespace Grpc.Adapter.UnitTests;

public class ConnectinoUnitTests
{
    [Fact]
    public void GetGrpcClient_WhenClientInstanceIsCorrect_ShouldReturnClient()
    {
        string mockServiceUrl = "http://service-name:5000";
        var adapter = new Connection();
        var client = adapter.GetGrpcClient<GreeterUseCase.GreeterUseCaseClient>(mockServiceUrl);
        
        Assert.NotNull(client);
        Assert.IsType<GreeterUseCase.GreeterUseCaseClient>(client);
    }
    
    private class InvalidMockGrpcClient();
    [Fact]
    public void GetGrpcClient_WhenClientInstanceIsInvalid_ThrowException()
    {
        string mockServiceUrl = "http://service-name:5000";
        var adapter = new Connection();

        var act = () => adapter.GetGrpcClient<InvalidMockGrpcClient>(mockServiceUrl);

        Assert.Throws<MissingMethodException>(act);
    }

   
}