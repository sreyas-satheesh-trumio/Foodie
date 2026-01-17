public interface IOrdersService
{
    public Task<OrderResponseDto?> CreateAsync(OrderCreateDto order);
    public Task<ICollection<OrderResponseDto>> GetAllAsync();
    public Task<ICollection<OrderResponseDto>> GetAllForSellerAsync();
    public Task<OrderResponseDto?> GetAsync(Guid id);
    public Task<OrderResponseDto?> StatusUpdateAsync(OrderStatusUpdateDto status);
}