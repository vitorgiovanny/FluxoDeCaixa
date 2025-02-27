using CashBalance.Domain;
using CashBalance.Domain.Entities;
using CashBalance.Interfaces;
using CashBalance.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ApiCashBalance.Controllers;

[ApiController]
[Route("cashbalance/[controller]")]
public class CashierController : ControllerBase
{
    private readonly ICashierServices _cashierServices;
    public CashierController(ICashierServices services) 
    { 
        _cashierServices = services;
    }

    /// <summary>
    /// Criar o comerciante responsavel pelo fluxo de caixa
    /// </summary>
    /// <param name="cashierName"></param>
    /// <returns></returns>
    /// <response code="200">Comerciante criado com sucesso</response>
    /// <response code="400">Erro ao criar o comerciante</response>
    [HttpPost("creater")]
    [ProducesResponseType(typeof(CreatedCashManagement), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> CreaterCashier([FromQuery] string  cashierName)
    {
        var send = await _cashierServices.CreateCashier(cashierName);
        
        return Ok(new CreatedCashManagement(send.Item1.Id, send.Item2, send.Item1.Name) { });
    }

    /// <summary>
    /// Retornar com todos os Comerciantes
    /// Get all the merchants from cashier.
    /// </summary>
    /// <returns></returns>
    /// <response code="200">Efetuado o retorno de todos os comerciantes com sucesso</response>
    /// <response code="400">Erro ao retornar todos os comerciantes</response>
    [HttpGet("getall")]
    [ProducesResponseType(typeof(IEnumerable<Cashier>),200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetAllMerchantCashier()
        => Ok(await _cashierServices.GetAllCashiers());
}