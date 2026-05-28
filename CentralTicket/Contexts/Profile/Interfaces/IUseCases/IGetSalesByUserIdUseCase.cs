using CentralTicket.Contexts.Profile.DTOs.Sale;

namespace CentralTicket.Contexts.Profile.Interfaces.IUseCases
{
    public interface IGetSalesByUserIdUseCase
    {
        public List<ReadSaleDTO> Run(Guid id);
    }
}
