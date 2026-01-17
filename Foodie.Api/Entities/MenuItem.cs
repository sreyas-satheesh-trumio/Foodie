public class MenuItem
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public decimal Price { get; set; }
    public Guid CategoryId { get; set; }
    public Category? Category { get; set; }
    public bool IsAvailable { get; set; } = true;
    public bool IsDeleted { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}