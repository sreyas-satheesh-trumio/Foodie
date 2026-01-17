public interface IMenuItemsService
{
    public Task<MenuItemResponseDto?> CreateAsync(MenuItemCreateDto menuItem);
    public Task<MenuItemResponseDto?> UpdateAsync(Guid id, MenuItemUpdateDto menuItem);
    public Task<MenuItemResponseDto?> DeleteAsync(Guid id);
    public Task<MenuItemResponseDto?> GetAsync(Guid id);
    public Task<ICollection<MenuItemResponseDto>> GetAllAsync(MenuItemFilterDto filter);
}