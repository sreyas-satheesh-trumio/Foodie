public class OrderCreateDto
{
    public ICollection<OrderItemsCreateDto> OrderItems { get; set; } = [];
}