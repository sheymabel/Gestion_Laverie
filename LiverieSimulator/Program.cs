using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        Uri serverUri = new Uri("ws://localhost:5000/ws"); // Remplacez par l'URL de votre WebSocket

        using (ClientWebSocket socket = new ClientWebSocket())
        {
            await socket.ConnectAsync(serverUri, CancellationToken.None);

            Console.WriteLine("Connected to WebSocket server");

            string message = "Hello from client";
            var buffer = Encoding.UTF8.GetBytes(message);
            await socket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);

            // Recevoir le message
            var receiveBuffer = new byte[1024];
            var result = await socket.ReceiveAsync(new ArraySegment<byte>(receiveBuffer), CancellationToken.None);
            Console.WriteLine("Received from server: " + Encoding.UTF8.GetString(receiveBuffer, 0, result.Count));

            await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
        }
    }
}
