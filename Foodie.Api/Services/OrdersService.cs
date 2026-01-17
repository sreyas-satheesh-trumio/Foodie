using Microsoft.EntityFrameworkCore;

public class OrderService : IOrdersService
{
    private readonly AppDbContext _db;
    public OrderService(AppDbContext db)
    {
        _db = db;
    }
    public async Task<OrderResponseDto?> CreateAsync(OrderCreateDto orderDto)
    {
        Guid userId = AuthService.CustomerId;

        var user = await _db.Users.FindAsync(userId);
        if (user == null)
            return null;

        var menuItemIds = orderDto.OrderItems
            .Select(x => x.MenuItemId)
            .ToList();

        var menuItems = _db.MenuItems
            .Where(x => menuItemIds.Contains(x.Id))
            .ToList();

        if (menuItems.Count != orderDto.OrderItems.Count)
            throw new Exception("One or more menu items not found.");

        var order = new Order
        {
            CustomerName = user.Name,
            UserId = user.Id,
            Status = OrderStatus.Placed,
            CreatedAt = DateTime.UtcNow,
            OrderItems = new List<OrderItem>()
        };

        decimal total = 0;

        foreach (var item in orderDto.OrderItems)
        {
            var menuItem = menuItems
                .First(x => x.Id == item.MenuItemId);

            if (!menuItem.IsAvailable)
                throw new Exception($"{menuItem.Name} is not available.");

            var orderItem = new OrderItem
            {
                MenuItemId = menuItem.Id,
                Quantity = item.Quantity,
                UnitPrice = menuItem.Price
            };

            total += menuItem.Price * item.Quantity;

            order.OrderItems.Add(orderItem);
        }

        order.TotalPrice = total;

        _db.Orders.Add(order);
        await _db.SaveChangesAsync();

        var createdOrder = await _db.Orders
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.MenuItem)
            .FirstAsync(o => o.Id == order.Id);

        return new OrderResponseDto
        {
            Id = createdOrder.Id,
            CustomerName = createdOrder.CustomerName,
            TotalPrice = createdOrder.TotalPrice,
            Status = createdOrder.Status,
            CreatedAt = createdOrder.CreatedAt,

            OrderItems = createdOrder.OrderItems.Select(oi =>
                new OrderItemsResponseDto
                {
                    Id = oi.Id,
                    MenuItem = oi.MenuItem?.Name,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice
                }).ToList()
        };
    }

    public async Task<ICollection<OrderResponseDto>> GetAllAsync()
    {
        Guid userId = AuthService.CustomerId;

        var orders = await _db.Orders
            .AsNoTracking()
            .Where(o => o.UserId == userId)
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.MenuItem)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();

        return orders.Select(MapOrderToResponse).ToList();
    }

    public async Task<ICollection<OrderResponseDto>> GetAllForSellerAsync()
    {
        var orders = await _db.Orders
            .AsNoTracking()
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.MenuItem)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();

        return orders.Select(MapOrderToResponse).ToList();
    }

    public async Task<OrderResponseDto?> GetAsync(Guid id)
    {
        var order = await _db.Orders
            .AsNoTracking()
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.MenuItem)
            .FirstOrDefaultAsync(o => o.Id == id);

        return order == null ? null : MapOrderToResponse(order);
    }

    public async Task<OrderResponseDto?> StatusUpdateAsync(OrderStatusUpdateDto status)
    {
        var order = await _db.Orders
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.MenuItem)
            .FirstOrDefaultAsync(o => o.Id == status.OrderId);

        if (order == null) return null;

        order.Status = status.Status;
        await _db.SaveChangesAsync();

        return MapOrderToResponse(order);
    }

    private static OrderResponseDto MapOrderToResponse(Order order)
    {
        return new OrderResponseDto
        {
            Id = order.Id,
            CustomerName = order.CustomerName,
            TotalPrice = order.TotalPrice,
            Status = order.Status,
            CreatedAt = order.CreatedAt,
            OrderItems = order.OrderItems
                .Select(oi => new OrderItemsResponseDto
                {
                    Id = oi.Id,
                    MenuItem = oi.MenuItem != null ? oi.MenuItem.Name : null,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice
                })
                .ToList()
        };
    }
}