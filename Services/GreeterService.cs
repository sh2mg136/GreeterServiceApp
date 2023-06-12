using Grpc.Core;
using GreeterServiceApp;

namespace GreeterServiceApp.Services;

public class GreeterService : Greeter.GreeterBase
{
    private readonly ILogger<GreeterService> _logger;
    public GreeterService(ILogger<GreeterService> logger)
    {
        _logger = logger;
    }

    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        _logger.LogInformation($"SayHello -> {request.Name}");

        return Task.FromResult(new HelloReply
        {
            Message = "Hello " + request.Name
        });
    }


    /// <summary>
    /// Test gRPC method 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override Task<SquareReply> GetPow2(SquareRequest request, ServerCallContext context)
    {
        double d = 0.0D;

        try
        {
            _logger.LogInformation($"User inputs: {request.Input}");
            d = Convert.ToDouble(request.Input);
            var res = d * d;

            return Task.FromResult(new SquareReply
            {
                Input = request.Input,
                Output = res.ToString(),
                Message = string.Empty
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "");

            return Task.FromResult(new SquareReply
            {
                Input = request.Input,
                Output = "Error!",
                Message = ex.Message
            });
        }
    }
}
