using CashBalance.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiCashBalance.Controllers;

[ApiController]
[Route("[controller]")]
public class CashierController : ControllerBase
{
    private readonly ICashierServices _cashierServices;
    public CashierController(ICashierServices services) 
    { 
        _cashierServices = services;
    }

    [HttpPost(Name = "Creater")]
    public async Task<IActionResult> CreaterCashier([FromBody] string  cashierName)
    {
        var send = await _cashierServices.CreateCashier(cashierName);
        
        return Ok(send);
    }
}