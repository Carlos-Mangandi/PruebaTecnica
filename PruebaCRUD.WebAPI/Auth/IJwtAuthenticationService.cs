using PruebaCRUD.EN;

namespace PruebaCRUD.WebAPI.Auth
{
    public interface IJwtAuthenticationService
    {
        string Authenticate(Usuario pUsuario);
    }
}
