
using Microsoft.AspNetCore.Mvc;
using SocketIOClient;


namespace ApiWeb.Controllers
{
     [Route("api/[controller]")]
    [ApiController]
    public class SocketController : ControllerBase
    {
        private readonly SocketIOClient.SocketIO _socketClient;

        public SocketController()
        {
            // Conectar al servidor de Socket.IO (cambia la URL por la tuya)
            _socketClient = new SocketIOClient.SocketIO("http://localhost:5000");

            // Manejar eventos de conexión
            _socketClient.OnConnected += async (sender, e) =>
            {
                Console.WriteLine("Conectado al servidor Socket.IO desde .NET");
                await _socketClient.EmitAsync("message", "Hola desde Web API .NET 8");
            };

            // Escuchar evento 'message'
            _socketClient.On("message", response =>
            {
                Console.WriteLine($"Mensaje recibido: {response.GetValue<string>()}");
            });

            // Conectar al servidor
            _socketClient.ConnectAsync().Wait();
        }

        [HttpGet("send")]
        public async Task<IActionResult> SendMessage([FromQuery] string message)
        {
            if (!_socketClient.Connected)
            {
                return BadRequest("No está conectado al servidor Socket.IO");
            }

            await _socketClient.EmitAsync("notificacion", message);
            return Ok($"Mensaje enviado: {message}");
        }

        [HttpGet("close")]
        public async Task<IActionResult> CloseConnection()
        {
            await _socketClient.DisconnectAsync();
            return Ok("Conexión cerrada");
        }
    }
}
