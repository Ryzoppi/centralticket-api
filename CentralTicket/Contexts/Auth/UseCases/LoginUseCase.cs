using CentralTicket.Contexts.Auth.Dtos;
using CentralTicket.Contexts.Auth.Entities;
using CentralTicket.Contexts.Auth.Interfaces.IRepositories;
using CentralTicket.Contexts.Auth.Requests;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;

namespace CentralTicket.Contexts.Auth.UseCases
{
    public class LoginUseCase
    {
        private readonly CreateTokenResponseUseCase _createTokenResponseUseCase;

        private readonly IUserRepository _userRepository;

        public LoginUseCase(CreateTokenResponseUseCase createTokenResponseUseCase, IUserRepository userRepository)
        {
            _createTokenResponseUseCase = createTokenResponseUseCase;
            _userRepository = userRepository;
        }

        public async Task<TokenResponseDto?> Run(LoginRequest request)
        {
            var users = _userRepository.GetAll();
            var user = users.FirstOrDefault(u => u.Email.Value == request.Email.Value);

            if (user == null)
            {
                return null;
            }

            if (new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.Password.Value) == PasswordVerificationResult.Failed)
            {
                return null;
            }

            TokenResponseDto response = await _createTokenResponseUseCase.Run(user);

            return response;
        }

        
    }
}
