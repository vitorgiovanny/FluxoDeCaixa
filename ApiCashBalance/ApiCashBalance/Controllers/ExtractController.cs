using CashBalance.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiCashBalance.Controllers
{
    [ApiController]
    [Route("cashbalance/[controller]")]
    public class ExtractController : ControllerBase
    {
        private readonly IExtractServices _services;

        public ExtractController(IExtractServices services) 
        { 
            _services = services;
        }

        /// <summary>
        /// Buscar o Extrato completo da aplicação
        /// </summary>
        /// <param name="idCash"></param>
        /// <returns></returns>
        [HttpGet("Extract")]
        public async Task<IActionResult> GetExtract([FromQuery] Guid idCash)
        {
            return Ok(_services.GetExtract(idCash));
        }

        /// <summary>
        /// Buscar relatorio completo da aplicação
        /// </summary>
        /// <param name="idCash"></param>
        /// <returns></returns>
        [HttpGet("Report")]
        public async Task<IActionResult> GetReport([FromQuery] Guid idCash)
        {
            return Ok(_services.GetReportPerDay(idCash));
        }
    }
}
