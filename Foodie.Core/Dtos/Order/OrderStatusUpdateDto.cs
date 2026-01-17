public class OrderStatusUpdateDto
{
    public Guid OrderId { get; set; }
    public OrderStatus Status { get; set; } = default!;
}