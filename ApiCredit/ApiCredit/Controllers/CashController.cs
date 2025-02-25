using ApiCredit.Domain.Interfaces;
using ApiCredit.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ApiCredit.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CashController : ControllerBase
{
    private readonly ICashServices _services;

    public CashController(ICashServices services)
    {
        _services = services;
    }

    [HttpPost]
    public async Task<IActionResult> AddCredit([FromBody] BodyCreditView model)
    {
        await _services.AddCash(model.Amount, model.IdCashed);
        return Ok();
    }
}