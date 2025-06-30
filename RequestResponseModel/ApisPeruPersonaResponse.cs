using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestResponseModel
{
    public class ApisPeruPersonaResponse
    {
        //Al ser strings, necesitan ser inicializados
        public bool Success { get; set; }
        public string dni { get; set; } = "";
        public string Nombres { get; set; } = "";
        public string ApellidoPaterno { get; set; } = "";
        public string ApellidoMaterno { get; set; } = "";
        public string CodVerifica { get; set; } = "";
    }
}
