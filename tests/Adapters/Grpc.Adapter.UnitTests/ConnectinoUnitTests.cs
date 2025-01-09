using Identity.Grpc;

namespace Grpc.Adapter.UnitTests;

public class ConnectinoUnitTests
{
    [Fact]
    public void GetGrpcClient_WhenClientIstanceIsCorrect_ShouldReturnClient()
    {
        var adapter = new Connection();
        string mockServiceUrl = "http://service-name:5000";
        var client = adapter.GetGrpcClient<GreeterUseCase.GreeterUseCaseClient>(mockServiceUrl);
        
        Assert.NotNull(client);
        Assert.IsType<GreeterUseCase.GreeterUseCaseClient>(client);
    }
    
    private class InvalidMockGrpcClient();
    [Fact]
    public void GetGrpcClient_WhenClientIstanceIsInvalid_ThrowException()
    {
        var adapter = new Connection();
        string mockServiceUrl = "http://service-name:5000";

        var act = () => adapter.GetGrpcClient<InvalidMockGrpcClient>(mockServiceUrl);

        Assert.Throws<MissingMethodException>(act);
    }

   
}