using Microsoft.AspNetCore.Authorization;

namespace UsuariosApi.Authorization
{
    public class IdadeAuthorization : AuthorizationHandler<IdadeMinima>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IdadeMinima requirement)
        {
            var dataNacimentoClain = context.User.FindFirst(c => c.Type == "DataNascimento");
            
            if (dataNacimentoClain == null) return Task.CompletedTask;
          
            var dataNascimento = Convert.ToDateTime(dataNacimentoClain.Value);

            var idadeUser = DateTime.Now.Year - dataNascimento.Year;

            if (dataNascimento > DateTime.Now.AddYears(-idadeUser)) idadeUser--;

            if (idadeUser >= requirement.Idade) context.Succeed(requirement);

            return Task.CompletedTask;

        }
    }
}
