using CentralTicket.Contexts.Profile.DTOs.User;
using CentralTicket.Contexts.Profile.Entities;
using CentralTicket.Contexts.Profile.Interfaces.IRepositories;
using CentralTicket.Contexts.Profile.Interfaces.IUseCases;

namespace CentralTicket.Contexts.Profile.UseCases
{
    public class GetUserByIdUseCase : IGetUserByIdUseCase
    {
        private readonly IUserRepository _userRepository;
        public GetUserByIdUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ReadUserDTO Run(Guid id)
        {
            User user = this._userRepository.GetById(id);

            if (user == null) throw new Exception("User nao encontrado");
            
            return new ReadUserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Password = user.Password,
                ProfilePictureUrl = user.ProfilePictureUrl
            };
        }
    }
}
