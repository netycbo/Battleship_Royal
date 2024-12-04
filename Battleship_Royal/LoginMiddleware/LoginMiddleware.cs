using MediatR;
using Serilog;

namespace Battleship_Royal.LoginMiddleware
{
    public class LoginMiddleware<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var requestType = typeof(TRequest).Name;
            var responseType = typeof(TResponse).Name;

            Log.Information("Starting handling request: {RequestType}", requestType);

            var startTime = DateTime.UtcNow;

            try
            {
                var response = await next();

                var elapsedTime = DateTime.UtcNow - startTime;

                Log.Information(
                    "Successfully handled request: {RequestType} with response: {ResponseType} in {ElapsedMilliseconds} ms",
                    requestType, responseType, elapsedTime.TotalMilliseconds);

                return response;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error handling request: {RequestType}", requestType);
                throw;
            }
        }
    }
}
