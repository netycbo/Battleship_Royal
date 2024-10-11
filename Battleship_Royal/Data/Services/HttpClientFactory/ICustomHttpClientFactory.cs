
namespace Battleship_Royal.Data.Services.HttpClientFactory
{
    public interface ICustomHttpClientFactory
    {
        HttpClient CreateClient(string endpoint);
    }
}