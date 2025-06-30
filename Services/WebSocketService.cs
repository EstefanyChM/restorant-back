using IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class WebSocketService : IWebSocketService
    {
        private readonly SocketIOClient.SocketIO _socketClient;
        private bool _isConnected = false;

        public WebSocketService()
        {
            _socketClient = new SocketIOClient.SocketIO("https://socket-1-8xy5.onrender.com");
            //_socketClient = new SocketIOClient.SocketIO("http://localhost:5000");

            _socketClient.OnConnected += async (sender, e) =>
            {
                _isConnected = true;
                Console.WriteLine("Conectado al WebSocket");
                /*await EmitMessageAsync("pedido-finalizado-back", "Hola desde Web API .NET 8");*/
            };

            _socketClient.OnDisconnected += (sender, e) =>
            {
                _isConnected = false;
                Console.WriteLine("Desconectado del WebSocket");
            };

            _socketClient.On("message", response =>
            {
                Console.WriteLine($"Mensaje recibido: {response.GetValue<string>()}");
            });

            // Intentar conectar en segundo plano sin bloquear la API
            Task.Run(async () => await TryConnectAsync());
        }

        public async Task TryConnectAsync()
        {
            while (!_isConnected)
            {
                try
                {
                    Console.WriteLine("Intentando conectar al WebSocket...");
                    await _socketClient.ConnectAsync();
                    await Task.Delay(5000); // Esperar 5 segundos antes de reintentar en caso de fallo
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al conectar al WebSocket: {ex.Message}");
                    await Task.Delay(5000); // Esperar antes de volver a intentar
                }
            }
        }

        public async Task EmitMessageAsync(string eventName, object message)
        {
            if (_isConnected)
            {
                await _socketClient.EmitAsync(eventName, message);
                Console.WriteLine($"mando: {eventName}");
            }
            else
            {
                Console.WriteLine("No hay conexión con el WebSocket, no se puede enviar el mensaje.");
            }
        }


    }
}