public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public UserRole Role { get; set; } = UserRole.Customer;
    public string Username { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
    public bool IsDeleted { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
