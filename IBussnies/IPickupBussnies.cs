using BDRiccosModel;
using Microsoft.AspNetCore.Mvc;

namespace IBussnies
{
    public interface IPickupBussnies : ICRUDBussnies<Pickup, Pickup>
    {
        
    }
}
