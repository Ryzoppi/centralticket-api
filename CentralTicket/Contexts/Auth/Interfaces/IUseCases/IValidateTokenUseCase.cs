using System.Security.Claims;

namespace CentralTicket.Contexts.Auth.Interfaces.IUseCases
{
    public interface IValidateTokenUseCase
    {
        public ClaimsPrincipal? Run(string token);
    }
}
