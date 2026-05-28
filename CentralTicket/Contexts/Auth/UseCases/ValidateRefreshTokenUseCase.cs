using CentralTicket.Contexts.Auth.Entities;
using CentralTicket.Contexts.Auth.Interfaces.IRepositories;
using CentralTicket.Contexts.Auth.Interfaces.IUseCases;

namespace CentralTicket.Contexts.Auth.UseCases
{
    public class ValidateRefreshTokenUseCase : IValidateRefreshTokenUseCase
    {
        private readonly IUserRepository _userRepository;

        public ValidateRefreshTokenUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User?> Run(Guid userId, string refreshToken)
        {
            
            var users = _userRepository.GetAll();
            var user = users.Find(x => x.Id == userId);

            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return null;
            }
            return user;
        }
    }
}
