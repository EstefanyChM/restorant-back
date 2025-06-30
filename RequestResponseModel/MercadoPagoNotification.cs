using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestResponseModel
{
    public class MercadoPagoNotification
    {
        public string Id { get; set; }
        public string LiveMode { get; set; }
        public string Type { get; set; }
        public string DateCreated { get; set; }
        public string Resource { get; set; }
        public string UserId { get; set; }
    }

}
