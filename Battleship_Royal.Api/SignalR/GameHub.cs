using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace Battleship_Royal.Api.SignalR
{
    public class GameHub : Hub
    {
        // Dictionary to store rooms and their participants
        private static ConcurrentDictionary<string, List<string>> gameRooms = new ConcurrentDictionary<string, List<string>>();

        // Method to create a game room
        public async Task CreateRoom(string roomName, string nickname)
        {
            if (!gameRooms.ContainsKey(roomName))
            {
                gameRooms.TryAdd(roomName, new List<string> { nickname });
                await Clients.Caller.SendAsync("RoomCreated", roomName);
            }
            else
            {
                await Clients.Caller.SendAsync("Error", "Room already exists.");
            }
        }

        // Method to join a game room
        public async Task JoinRoom(string roomName, string nickname)
        {
            if (gameRooms.TryGetValue(roomName, out var players))
            {
                if (players.Count < 2)
                {
                    players.Add(nickname);
                    await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
                    await Clients.Group(roomName).SendAsync("PlayerJoined", nickname);

                    if (players.Count == 2)
                    {
                        await Clients.Group(roomName).SendAsync("StartGame");
                    }
                }
                else
                {
                    await Clients.Caller.SendAsync("Error", "Room is full.");
                }
            }
            else
            {
                await Clients.Caller.SendAsync("Error", "Room does not exist.");
            }
        }

        // Method to leave a game room
        public async Task LeaveRoom(string roomName, string nickname)
        {
            if (gameRooms.TryGetValue(roomName, out var players))
            {
                players.Remove(nickname);
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
                await Clients.Group(roomName).SendAsync("PlayerLeft", nickname);

                if (players.Count == 0)
                {
                    gameRooms.TryRemove(roomName, out _);
                    await Clients.Group(roomName).SendAsync("RoomClosed");
                }
            }
        }
        public async Task SendMove(string roomName, int row, int col)
        {
            // Broadcast the move to the other player
            await Clients.OthersInGroup(roomName).SendAsync("ReceiveMove", row, col);
        }

        public async Task PlaceShip(string roomName, string shipType)
        {
            // Notify the other player about ship placement
            await Clients.OthersInGroup(roomName).SendAsync("ShipPlaced", shipType);
        }
    }
}
