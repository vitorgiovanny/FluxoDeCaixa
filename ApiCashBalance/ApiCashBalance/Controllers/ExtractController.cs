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
        [HttpGet("getextract")]
        public async Task<IActionResult> GetExtract([FromQuery] Guid idCash)
        {
            var response = await _services.GetExtract(idCash);
            return Ok(response);
        }

        /// <summary>
        /// Buscar relatorio completo da aplicação
        /// </summary>
        /// <param name="idCash"></param>
        /// <returns></returns>
        [HttpGet("report")]
        public async Task<IActionResult> GetReport([FromQuery] Guid idCash)
        {
            var response = await _services.GetReportPerDay(idCash);
            return Ok(new {Amount = response, Description = "Valor diario"});
        }
    }
}
