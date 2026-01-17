public class OrderItemsResponseDto
{
    public Guid Id { get; set; }
    public string? MenuItem { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}