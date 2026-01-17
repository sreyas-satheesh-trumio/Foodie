using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api")]
public class OrdersController : ControllerBase
{

    private readonly IOrdersService _ordersService;

    public OrdersController (IOrdersService ordersService) {
        _ordersService = ordersService;
    }

    [HttpPost("/orders")]
    public async Task<ActionResult<OrderResponseDto>> OrderCreate(OrderCreateDto order)
    {
        var res = _ordersService.CreateAsync(order);
        if (res == null) return NotFound();

        return Ok(res);
    }  

    [HttpGet("/orders")]
    public async Task<ActionResult<ICollection<OrderResponseDto>>> OrderGetAll()
    {
        var res = _ordersService.GetAllAsync();
        if (res == null) return NotFound();

        return Ok(res);
    }  

    [HttpGet("/orders/seller")]
    public async Task<ActionResult<ICollection<OrderResponseDto>>> OrderGetAllForSeller()
    {
        var res = _ordersService.GetAllForSellerAsync();
        if (res == null) return NotFound();

        return Ok(res);
    }  

    [HttpPut("/orders/status")]
    public async Task<ActionResult<OrderResponseDto>> OrderStatusUpdate(OrderStatusUpdateDto status)
    {
        var res = _ordersService.StatusUpdateAsync(status);
        if (res == null) return NotFound();

        return Ok(res);
    }  
}
