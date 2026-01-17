public class MenuItemService : IMenuItemsService
{
    public Task<MenuItemResponseDto?> CreateAsync(MenuItemCreateDto menuItem)
    {
        throw new NotImplementedException();
    }

    public Task<MenuItemResponseDto?> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<MenuItemResponseDto>> GetAllAsync(MenuItemFilterDto filter)
    {
        throw new NotImplementedException();
    }

    public Task<MenuItemResponseDto?> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<MenuItemResponseDto?> UpdateAsync(Guid id, MenuItemUpdateDto menuItem)
    {
        throw new NotImplementedException();
    }
}