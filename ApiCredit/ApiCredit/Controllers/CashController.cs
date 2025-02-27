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

    /// <summary>
    /// Adicionar dinheiro na conta
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("AddCredit")]
    public async Task<IActionResult> AddCredit([FromBody] BodyCreditView model)
    {
        await _services.AddCash(model.Amount, model.IdCashed, model.IdCash);
        return Ok(new {Amount = model.Amount, description = $"adicionado o montante de {model.Amount}"});
    }
}