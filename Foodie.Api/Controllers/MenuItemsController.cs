using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api")]
public class MenuItemsController : ControllerBase
{
    private readonly IMenuItemsService _menuItemService;

    public MenuItemsController(IMenuItemsService menuItemService) {
        _menuItemService = menuItemService;
    }

    [HttpPost("items")]
    public async Task<ActionResult<MenuItemResponseDto>> MenuItemCreate(MenuItemCreateDto menuItem)
    {
        var res = await _menuItemService.CreateAsync(menuItem);
        if (res == null) return NotFound();

        return Ok(res);
    }

    [HttpPut("items/{id}")]
    public async Task<ActionResult<MenuItemResponseDto>> MenuItemUpdate(Guid id, MenuItemUpdateDto menuItem)
    {
        var res = await _menuItemService.UpdateAsync(id, menuItem);
        if (res == null) return NotFound();

        return Ok(res);
    }

    [HttpDelete("items/{id}")]
    public async Task<ActionResult<MenuItemResponseDto>> MenuItemUpdate(Guid id)
    {
        var res = await _menuItemService.DeleteAsync(id);
        if (res == null) return NotFound();

        return Ok(res);
    }

    [HttpGet("items/{id}")]
    public async Task<ActionResult<MenuItemResponseDto>> MenuItemGet(Guid id)
    {
        var res = await _menuItemService.GetAsync(id);
        if (res == null) return NotFound();

        return Ok(res);
    }

    [HttpGet("items")]
    public async Task<ActionResult<MenuItemResponseDto>> MenuItemGetAll([FromQuery] MenuItemFilterDto filter)
    {
        var res = await _menuItemService.GetAllAsync(filter);
        if (res == null) return NotFound();

        return Ok(res);
    }
}
