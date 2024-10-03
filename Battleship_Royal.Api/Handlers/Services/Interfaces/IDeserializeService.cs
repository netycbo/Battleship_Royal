namespace Battleship_Royal.Api.Handlers.Services.Interfaces
{
    public interface IDeserializeService
    {
        T Deserialize<T>(string json);
    }
}