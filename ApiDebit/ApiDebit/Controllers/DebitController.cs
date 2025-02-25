using ApiDebit.Domain.Interfaces;
using ApiDebit.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ApiDebit.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DebitController : ControllerBase
    {
        private readonly ICashServices _services;

        public DebitController(ICashServices services) 
        {
            _services = services;
        }

        [HttpPost]
        public async Task<IActionResult> AddDebit([FromBody] BodyCreditView model)
        {
            await _services.AddCash(model.Amount, model.IdCashed);
            return Ok();
        }
    }
}
