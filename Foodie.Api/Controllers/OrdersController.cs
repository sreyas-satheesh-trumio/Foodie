using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api")]
public class OrdersController : ControllerBase
{

    private readonly IOrdersService _ordersService;

    public OrdersController (IOrdersService ordersService) {
        _ordersService = ordersService;
    }

    [HttpPost("orders")]
    public async Task<ActionResult<OrderResponseDto>> OrderCreate([FromBody] OrderCreateDto order)
    {
        var res = await _ordersService.CreateAsync(order);
        if (res == null) return NotFound();

        return Ok(res);
    }  

    [HttpGet("orders/{id}")]
    public async Task<ActionResult<OrderResponseDto>> OrderGet(Guid id)
    {
        var res = await _ordersService.GetAsync(id);
        if (res == null) return NotFound();

        return Ok(res);
    }

    [HttpGet("orders")]
    public async Task<ActionResult<ICollection<OrderResponseDto>>> OrderGetAll()
    {
        var res = await _ordersService.GetAllAsync();
        if (res == null) return NotFound();

        return Ok(res);
    }


    [HttpGet("orders/seller")]
    public async Task<ActionResult<ICollection<OrderResponseDto>>> OrderGetAllForSeller()
    {
        var res = await _ordersService.GetAllForSellerAsync();
        if (res == null) return NotFound();

        return Ok(res);
    }  

    [HttpPut("orders/status")]
    public async Task<ActionResult<OrderResponseDto>> OrderStatusUpdate(OrderStatusUpdateDto status)
    {
        var res = await _ordersService.StatusUpdateAsync(status);
        if (res == null) return NotFound();

        return Ok(res);
    }  
}
