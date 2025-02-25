using CashBalance.Interfaces;
using CashBalance.ViewModel;
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

    /// <summary>
    /// Criar conta Comerciante
    /// The merchant for the cashier will be created.
    /// </summary>
    /// <param name="cashierName"></param>
    /// <returns></returns>
    [HttpPost(Name = "Creater")]
    public async Task<IActionResult> CreaterCashier([FromQuery] string  cashierName)
    {
        var send = await _cashierServices.CreateCashier(cashierName);
        
        return Ok(new CreatedCashManagement(send.Item1.Id, send.Item2, send.Item1.Name) { });
    }

    /// <summary>
    /// Todos os Comerciantes
    /// Get all the merchants from cashier.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAllMerchantCashier()
        => Ok(await _cashierServices.GetAllCashiers());
}