using CentralTicket.Contexts.Billing.DTOs.Sale;
using CentralTicket.Contexts.Billing.Entities;
using CentralTicket.Contexts.Billing.Interfaces.IUseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CentralTicket.Contexts.Billing.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly IListSalesUseCase _listSalesUseCase;
        private readonly IGetSaleByIdUseCase _getSaleByIdUseCase;
        private readonly ICreateSaleUseCase _createSaleUseCase;
        private readonly ICancelSaleUseCase _cancelSaleUseCase;

        public SalesController(
            IListSalesUseCase listSalesUseCase,
            IGetSaleByIdUseCase getSaleByIdUseCase,
            ICreateSaleUseCase createSaleUseCase,
            ICancelSaleUseCase cancelSaleUseCase)
        {
            this._listSalesUseCase = listSalesUseCase;
            this._getSaleByIdUseCase = getSaleByIdUseCase;
            this._createSaleUseCase = createSaleUseCase;
            this._cancelSaleUseCase = cancelSaleUseCase;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                List<ReadSaleDTO> list = this._listSalesUseCase.Run();

                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("GetById")]
        public IActionResult GetById([FromQuery] Guid id)
        {
            try
            {
                ReadSaleDTO sale = this._getSaleByIdUseCase.Run(id);

                if (sale == null)
                {
                    return NotFound();
                }

                return Ok(sale);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create([FromBody] CreateSaleDTO sale)
        {
            try
            {
                this._createSaleUseCase.Run(sale);

                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut]
        public IActionResult Cancel([FromQuery] Guid id)
        {
            try
            {
                this._cancelSaleUseCase.Run(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
