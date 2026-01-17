public class OrderResponseDto
{
    public Guid Id { get; set; }
    public string CustomerName { get; set; } = default!;
    public decimal TotalPrice { get; set; }
    public ICollection<OrderItemsResponseDto> OrderItems { get; set; } = [];
    public OrderStatus Status { get; set; } = OrderStatus.Placed;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}