using RequestResponseModel;

namespace IServices
{
    public interface IApisPeruServices:IDisposable //ID para limpiar la memoria
    {
        ApisPeruPersonaResponse PersonaPorDNI(string dni);
        ApisPeruEmpresaResponse EmpresaPorRUC(string ruc);
    }
}