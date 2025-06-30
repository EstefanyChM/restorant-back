using Firebase.Auth;
using Firebase.Storage;
using IServices;
using RequestResponseModel;

namespace Services
{
    public class FilesServices : IFilesServices
    {
        public async Task<string> SubirArchivo(Stream archivo, string nombre, string carpeta)
        {
            string email = "riccos.pyp@gmail.com";
            string clave = "cuentafirebase";		
            string ruta = "riccos-84c3c.appspot.com";

            string api_key = "AIzaSyA5PFawDPWzoENVQvkI8FsODWpbhebZxxI";
            var auth = new FirebaseAuthProvider(new FirebaseConfig(api_key));
            var a = await auth.SignInWithEmailAndPasswordAsync(email, clave);

            var cancellation = new CancellationTokenSource();
            var task = new FirebaseStorage(
                ruta,
                new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                    ThrowOnCancel = true
                })
                .Child(carpeta)
                .Child(nombre)

                .PutAsync(archivo, cancellation.Token);
            string downloadURL = await task;
            return downloadURL;

        }
    }
}
