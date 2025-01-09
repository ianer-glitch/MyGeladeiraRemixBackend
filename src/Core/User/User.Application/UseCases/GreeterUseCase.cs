using Grpc.Core;
using Identity.Grpc;
using Microsoft.Extensions.Logging;

namespace User.Application.UseCases;

public class GreeterUseCase : Identity.Grpc.GreeterUseCase.GreeterUseCaseBase
{
    private readonly ILogger<GreeterUseCase> _logger;

    public GreeterUseCase(ILogger<GreeterUseCase> logger)
    {
        _logger = logger;
    }

    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        return Task.FromResult(new HelloReply
        {
            Message = "Hello " + request.Name
        });
    }
}