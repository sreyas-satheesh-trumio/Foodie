public class MenuItemService : IMenuItemsService
{
    private readonly AppDbContext _db;
    public MenuItemService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<MenuItemResponseDto?> CreateAsync(MenuItemCreateDto menuItem)
    {
        var newMenuItem = new MenuItem
        {
            Name = menuItem.Name,
            Price = menuItem.Price,
            Category = menuItem.Category,
            IsAvailable = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _db.MenuItems.Add(newMenuItem);
        _db.SaveChanges();

        return new MenuItemResponseDto
        {
            Id = newMenuItem.Id,
            Name = newMenuItem.Name,
            Price = newMenuItem.Price,
            Category = newMenuItem.Category,
            IsAvailable = newMenuItem.IsAvailable,
            CreatedAt = newMenuItem.CreatedAt,
            UpdatedAt = newMenuItem.UpdatedAt
        };
    }

    public async Task<MenuItemResponseDto?> DeleteAsync(Guid id)
    {
        var itemToDelete = await _db.MenuItems.FindAsync(id);
        if (itemToDelete == null || itemToDelete.IsDeleted) return null;

        itemToDelete.IsDeleted = true;
        await _db.SaveChangesAsync();

        return new MenuItemResponseDto
        {
            Id = itemToDelete.Id,
            Name = itemToDelete.Name,
            Price = itemToDelete.Price,
            Category = itemToDelete.Category,
            IsAvailable = itemToDelete.IsAvailable,
            CreatedAt = itemToDelete.CreatedAt,
            UpdatedAt = itemToDelete.UpdatedAt
        };
    }

    public async Task<ICollection<MenuItemResponseDto>> GetAllAsync(MenuItemFilterDto filter)
    {
        Guid userId = AuthService.CustomerId;
        var query = _db.MenuItems.AsQueryable();

        if (!string.IsNullOrEmpty(filter.Category))
        {
            query = query.Where(mi => mi.Category == filter.Category);
        }

        if (!string.IsNullOrEmpty(filter.SearchQuery))
        {
            query = query.Where(mi => mi.Name.Contains(filter.SearchQuery));
        }

        var menuItems = query.Where(mi => !mi.IsDeleted).ToList();

        return menuItems.Select(mi => new MenuItemResponseDto
        {
            Id = mi.Id,
            Name = mi.Name,
            Price = mi.Price,
            Category = mi.Category,
            IsAvailable = mi.IsAvailable,
            CreatedAt = mi.CreatedAt,
            UpdatedAt = mi.UpdatedAt
        }).ToList();
    }

    public async Task<MenuItemResponseDto?> GetAsync(Guid id)
    {
        var menuItem = await _db.MenuItems.FindAsync(id);
        if (menuItem == null || menuItem.IsDeleted) return null;

        return new MenuItemResponseDto
        {
            Id = menuItem.Id,
            Name = menuItem.Name,
            Price = menuItem.Price,
            Category = menuItem.Category,
            IsAvailable = menuItem.IsAvailable,
            CreatedAt = menuItem.CreatedAt,
            UpdatedAt = menuItem.UpdatedAt
        };
    }

    public async Task<MenuItemResponseDto?> UpdateAsync(Guid id, MenuItemUpdateDto menuItem)
    {
        var existingMenuItem = await _db.MenuItems.FindAsync(id);

        if (existingMenuItem == null || existingMenuItem.IsDeleted) return null;

        existingMenuItem.Name = menuItem.Name ?? existingMenuItem.Name;
        existingMenuItem.Price = menuItem.Price ?? existingMenuItem.Price;
        existingMenuItem.Category = menuItem.Category ?? existingMenuItem.Category;
        existingMenuItem.IsAvailable = menuItem.IsAvailable ?? existingMenuItem.IsAvailable;
        existingMenuItem.UpdatedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync();

        return new MenuItemResponseDto
        {
            Id = existingMenuItem.Id,
            Name = existingMenuItem.Name,
            Price = existingMenuItem.Price,
            Category = existingMenuItem.Category,
            IsAvailable = existingMenuItem.IsAvailable,
            CreatedAt = existingMenuItem.CreatedAt,
            UpdatedAt = existingMenuItem.UpdatedAt
        };
    }
}