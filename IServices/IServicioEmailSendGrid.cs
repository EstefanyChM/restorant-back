using BDRiccosModel;
using RequestResponseModel;
namespace IServices
{
	public interface IServicioEmailSendGrid
    {
		Task EnviarMasivos(Promocion promocion, List<string> destinatarios);
	}
}
