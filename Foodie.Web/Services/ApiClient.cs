using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Foodie.Web.Services;

public class ApiClient
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl = "http://localhost:5000/api";
    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter() }
    };

    public ApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<MenuItemResponseDto>> GetMenuItemsAsync(string? search = null, string? category = null)
    {
        var query = new List<string>();
        if (!string.IsNullOrEmpty(search)) query.Add($"searchQuery={Uri.EscapeDataString(search)}");
        if (!string.IsNullOrEmpty(category)) query.Add($"category={Uri.EscapeDataString(category)}");

        var url = $"{_baseUrl}/items" + (query.Count > 0 ? "?" + string.Join("&", query) : "");
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<MenuItemResponseDto>>(content, _jsonOptions) ?? new();
    }

    public async Task<MenuItemResponseDto?> GetMenuItemAsync(Guid id)
    {
        var response = await _httpClient.GetAsync($"{_baseUrl}/items/{id}");
        if (!response.IsSuccessStatusCode) return null;

        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<MenuItemResponseDto>(content, _jsonOptions);
    }

    public async Task<MenuItemResponseDto?> CreateMenuItemAsync(MenuItemCreateDto dto)
    {
        var json = JsonSerializer.Serialize(dto);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync($"{_baseUrl}/items", content);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<MenuItemResponseDto>(responseContent, _jsonOptions);
    }

    public async Task<MenuItemResponseDto?> UpdateMenuItemAsync(Guid id, MenuItemUpdateDto dto)
    {
        var json = JsonSerializer.Serialize(dto);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PutAsync($"{_baseUrl}/items/{id}", content);
        if (!response.IsSuccessStatusCode) return null;

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<MenuItemResponseDto>(responseContent, _jsonOptions);
    }

    public async Task<bool> DeleteMenuItemAsync(Guid id)
    {
        var response = await _httpClient.DeleteAsync($"{_baseUrl}/items/{id}");
        return response.IsSuccessStatusCode;
    }

    // Orders
    public async Task<OrderResponseDto?> CreateOrderAsync(OrderCreateDto dto)
    {
        var json = JsonSerializer.Serialize(dto);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync($"{_baseUrl}/orders", content);
        if (!response.IsSuccessStatusCode) return null;

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<OrderResponseDto>(responseContent, _jsonOptions);
    }

    public async Task<List<OrderResponseDto>> GetOrdersAsync()
    {
        var response = await _httpClient.GetAsync($"{_baseUrl}/orders");
        if (!response.IsSuccessStatusCode) return new();

        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<OrderResponseDto>>(content, _jsonOptions) ?? new();
    }

    public async Task<List<OrderResponseDto>> GetAllOrdersAsync()
    {
        var response = await _httpClient.GetAsync($"{_baseUrl}/orders/seller");
        if (!response.IsSuccessStatusCode) return new();

        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<OrderResponseDto>>(content, _jsonOptions) ?? new();
    }

    public async Task<OrderResponseDto?> GetOrderAsync(Guid id)
    {
        var response = await _httpClient.GetAsync($"{_baseUrl}/orders/{id}");
        if (!response.IsSuccessStatusCode) return null;

        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<OrderResponseDto>(content, _jsonOptions);
    }

    public async Task<OrderResponseDto?> UpdateOrderStatusAsync(Guid orderId, OrderStatus status)
    {
        var dto = new OrderStatusUpdateDto { OrderId = orderId, Status = status };
        var json = JsonSerializer.Serialize(dto);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PutAsync($"{_baseUrl}/orders/status", content);
        if (!response.IsSuccessStatusCode) return null;

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<OrderResponseDto>(responseContent, _jsonOptions);
    }
}

// DTOs
public class MenuItemResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Category { get; set; } = string.Empty;
    public bool IsAvailable { get; set; } = true;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class MenuItemCreateDto
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Category { get; set; } = string.Empty;
}

public class MenuItemUpdateDto
{
    public string? Name { get; set; }
    public decimal? Price { get; set; }
    public string? Category { get; set; }
    public bool? IsAvailable { get; set; }
}

public class MenuItemFilterDto
{
    public string? SearchQuery { get; set; }
    public string? Category { get; set; }
}

public class OrderResponseDto
{
    public Guid Id { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public decimal TotalPrice { get; set; }
    public List<OrderItemsResponseDto> OrderItems { get; set; } = new();
    public OrderStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class OrderItemsResponseDto
{
    public Guid Id { get; set; }
    public string? MenuItem { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}

public class OrderCreateDto
{
    public List<OrderItemsCreateDto> OrderItems { get; set; } = new();
}

public class OrderItemsCreateDto
{
    public Guid MenuItemId { get; set; }
    public int Quantity { get; set; }
}

public class OrderStatusUpdateDto
{
    public Guid OrderId { get; set; }
    public OrderStatus Status { get; set; }
}

public enum OrderStatus
{
    Placed,
    Preparing,
    Delivered
}
