using RequestResponseModel;

namespace IServices
{
	public interface IFilesServices
	{
		//string  SubirArchivo(Stream archivo, string nombre);


		Task<string> SubirArchivo(Stream archivo, string nombre, String carpeta);


	}
}
