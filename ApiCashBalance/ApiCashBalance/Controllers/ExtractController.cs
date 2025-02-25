using Microsoft.AspNetCore.Mvc;

namespace ApiCashBalance.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExtractController : ControllerBase
    {
        public ExtractController() { }

        [HttpGet(Name = "Extract")]
        public async Task<IActionResult> GetExtract(Guid IdCashier)
        {
            return Ok();
        }
    }
}
