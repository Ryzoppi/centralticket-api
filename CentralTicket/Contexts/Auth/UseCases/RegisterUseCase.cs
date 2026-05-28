using CentralTicket.Contexts.Auth.Entities;
using CentralTicket.Contexts.Auth.Interfaces.IRepositories;
using CentralTicket.Contexts.Auth.Interfaces.IUseCases;
using CentralTicket.Contexts.Auth.Requests;
using Microsoft.AspNetCore.Identity;

namespace CentralTicket.Contexts.Auth.UseCases
{
    public class RegisterUseCase : IRegisterUseCase 
    {

        private readonly IUserRepository _userRepository;

        public RegisterUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User? Run(RegisterRequest request)
        {
            // Se encontrar um usuário com o mesmo email, retornar null ou lançar uma exceção
            var users = _userRepository.GetAll();
            if (users.FirstOrDefault(u => u.Email.Value == request.Email.Value) != null)
            {
                return null;
            }

            var newUser = new User();

            var hashedPassword = new PasswordHasher<User>()
                .HashPassword(newUser, request.Password.Value);

            newUser.Id = Guid.NewGuid();
            newUser.Name = request.Name;
            newUser.Email = request.Email;
            newUser.PasswordHash = hashedPassword;
            newUser.ProfilePictureUrl = "";
            newUser.Sales = new List<Sale>();
            newUser.createdAt = DateOnly.FromDateTime(DateTime.Now);

            _userRepository.Create(newUser);
            return newUser;
        }
    }
}