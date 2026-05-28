using CentralTicket.Contexts.Profile.DTOs.User;
using CentralTicket.Contexts.Profile.Interfaces.IUseCases;
using Microsoft.AspNetCore.Mvc;

namespace CentralTicket.Contexts.Profile.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly IGetUserByIdUseCase _getUser;
        private readonly IGetSalesByUserIdUseCase _getSales;
        private readonly IGetTicketsByUserIdUseCase _getTickets;

        public ProfileController(
            IGetUserByIdUseCase getUser,
            IGetSalesByUserIdUseCase getSales,
            IGetTicketsByUserIdUseCase getTickets)
        {
            _getUser = getUser;
            _getSales = getSales;
            _getTickets = getTickets;
        }

        [HttpGet("GetById")]
        public IActionResult GetById([FromQuery] Guid id)
        {
            try
            {
                ReadUserDTO user = this._getUser.Run(id);

                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}/sales")]
        public async Task<IActionResult> GetSales(Guid id)
        {
            var sales = _getSales.Run(id);
            return Ok(sales);
        }

        [HttpGet("{id}/tickets")]
        public async Task<IActionResult> GetTickets(Guid id)
        {
            var tickets = _getTickets.Run(id);
            return Ok(tickets);
        }

       
    }
}
