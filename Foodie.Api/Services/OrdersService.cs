public class OrderService : IOrdersService
{
    public Task<OrderResponseDto?> CreateAsync(OrderCreateDto order)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<OrderResponseDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<OrderResponseDto>> GetAllForSellerAsync()
    {
        throw new NotImplementedException();
    }

    public Task<OrderResponseDto> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<OrderResponseDto?> StatusUpdateAsync(OrderStatusUpdateDto status)
    {
        throw new NotImplementedException();
    }
}