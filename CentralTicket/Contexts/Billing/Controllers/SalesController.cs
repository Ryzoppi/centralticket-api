using CentralTicket.Contexts.Billing.DTOs.Sale;
using CentralTicket.Contexts.Billing.Interfaces.IUseCases;
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
        private readonly IConfirmSaleUseCase _confirmSaleUseCase;

        public SalesController(
            IListSalesUseCase listSalesUseCase,
            IGetSaleByIdUseCase getSaleByIdUseCase,
            ICreateSaleUseCase createSaleUseCase,
            ICancelSaleUseCase cancelSaleUseCase,
            IConfirmSaleUseCase confirmSaleUseCase)
        {
            this._listSalesUseCase = listSalesUseCase;
            this._getSaleByIdUseCase = getSaleByIdUseCase;
            this._createSaleUseCase = createSaleUseCase;
            this._cancelSaleUseCase = cancelSaleUseCase;
            this._confirmSaleUseCase = confirmSaleUseCase;
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

        [HttpPut]
        public IActionResult Cofirm([FromQuery] Guid id)
        {
            try
            {
                this._confirmSaleUseCase.Run(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
