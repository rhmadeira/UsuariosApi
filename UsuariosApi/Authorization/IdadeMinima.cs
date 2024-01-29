using Microsoft.AspNetCore.Authorization;

namespace UsuariosApi.Authorization;

public class IdadeMinima : IAuthorizationRequirement
{
    public int Idade { get; }
    
    public IdadeMinima(int idade)
    {
        Idade = idade;
    }
}
