using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServices
{
    public interface IWebSocketService
    {
        /// <summary>
        /// Intenta conectar al WebSocket de manera asincrónica.
        /// </summary>
        Task TryConnectAsync();

        /// <summary>
        /// Envía un mensaje a un evento específico en el servidor WebSocket.
        /// </summary>
        /// <param name="eventName">Nombre del evento en el servidor WebSocket.</param>
        /// <param name="message">Mensaje a enviar.</param>
        Task EmitMessageAsync(string eventName, object message);
    }


}
