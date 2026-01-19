using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Foodie.Web.Services;

public class ApiClient
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter() }
    };

    public ApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<MenuItemResponseDto>> GetMenuItemsAsync()
    {
        var response = await _httpClient.GetAsync("/api/items");
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<List<MenuItemResponseDto>>(_jsonOptions) ?? new();
    }

    public async Task<MenuItemResponseDto?> GetMenuItemAsync(Guid id)
    {
        var response = await _httpClient.GetAsync($"/api/items/{id}");
        if (!response.IsSuccessStatusCode) return null;

        return await response.Content.ReadFromJsonAsync<MenuItemResponseDto>(_jsonOptions);
    }

    public async Task<MenuItemResponseDto?> CreateMenuItemAsync(MenuItemCreateDto dto)
    {
        var json = JsonSerializer.Serialize(dto);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("/api/items", content);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<MenuItemResponseDto>(responseContent, _jsonOptions);
    }

    public async Task<MenuItemResponseDto?> UpdateMenuItemAsync(Guid id, MenuItemUpdateDto dto)
    {
        var json = JsonSerializer.Serialize(dto);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PutAsync($"/api/items/{id}", content);
        if (!response.IsSuccessStatusCode) return null;

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<MenuItemResponseDto>(responseContent, _jsonOptions);
    }

    public async Task<bool> DeleteMenuItemAsync(Guid id)
    {
        var response = await _httpClient.DeleteAsync($"/api/items/{id}");
        return response.IsSuccessStatusCode;
    }

    public async Task<OrderResponseDto?> CreateOrderAsync(OrderCreateDto dto)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/orders", dto);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content
            .ReadFromJsonAsync<OrderResponseDto>(_jsonOptions);
    }


    public async Task<List<OrderResponseDto>> GetOrdersAsync()
    {
        var response = await _httpClient.GetAsync("/api/orders");
        if (!response.IsSuccessStatusCode) return new();

        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<OrderResponseDto>>(content, _jsonOptions) ?? new();
    }

    public async Task<List<OrderResponseDto>> GetAllOrdersAsync()
    {
        var response = await _httpClient.GetAsync("/api/orders/seller");

        if (!response.IsSuccessStatusCode) return new();

        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<OrderResponseDto>>(content, _jsonOptions) ?? new();
    }

    public async Task<OrderResponseDto?> GetOrderAsync(Guid id)
    {
        var response = await _httpClient.GetAsync($"/api/orders/{id}");
        if (!response.IsSuccessStatusCode) return null;

        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<OrderResponseDto>(content, _jsonOptions);
    }

    public async Task<OrderResponseDto?> UpdateOrderStatusAsync(Guid orderId, OrderStatus status)
    {
        var dto = new OrderStatusUpdateDto { OrderId = orderId, Status = status };
        var json = JsonSerializer.Serialize(dto);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PutAsync("/api/orders/status", content);
        if (!response.IsSuccessStatusCode) return null;

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<OrderResponseDto>(responseContent, _jsonOptions);
    }
}
