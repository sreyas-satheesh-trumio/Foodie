namespace Foodie.Web.Services;

public class AuthService
{
    private string? _currentUser;
    private UserRole _currentRole;

    public event Action? OnAuthStateChanged;

    public bool IsAuthenticated => !string.IsNullOrEmpty(_currentUser);
    public string? CurrentUser => _currentUser;
    public UserRole CurrentRole => _currentRole;

    public void Login(string username, string password)
    {
        if (username == "sreyas@gmail.com" && password == "sreyas@123")
        {
            _currentUser = username;
            _currentRole = UserRole.Customer;
        }
        else if (username == "admin@gmail.com" && password == "admin@123")
        {
            _currentUser = username;
            _currentRole = UserRole.Admin;
        }
        else
        {
            throw new InvalidOperationException("Invalid credentials");
        }

        OnAuthStateChanged?.Invoke();
    }

    public void Logout()
    {
        _currentUser = null;
        _currentRole = UserRole.Customer;
        OnAuthStateChanged?.Invoke();
    }
}

public enum UserRole
{
    Customer,
    Admin
}
