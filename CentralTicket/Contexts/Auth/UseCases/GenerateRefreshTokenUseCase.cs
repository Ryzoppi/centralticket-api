using CentralTicket.Contexts.Auth.Interfaces.IUseCases;
using System.Security.Cryptography;

namespace CentralTicket.Contexts.Auth.UseCases
{
    public class GenerateRefreshTokenUseCase : IGenerateRefreshTokenUseCase
    {
        public string Run()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);

            return Convert.ToBase64String(randomNumber);
        }
    }
}
