using BlazorApp.Model;
using System.Net.Http.Json;

public class ProductApiService
{
    private readonly HttpClient http;

    public ProductApiService(HttpClient http)
    {
        this.http = http;
    }

    public async Task<List<Product>> GetProductsAsync()
    {
        return await http.GetFromJsonAsync<List<Product>>("api/product") ?? new();
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        var response = await http.GetAsync($"api/product/{id}");
        if (!response.IsSuccessStatusCode) return null;
        return await response.Content.ReadFromJsonAsync<Product>();
    }

    public async Task AddProductAsync(Product product)
    {
        var response = await http.PostAsJsonAsync("api/product", product);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteProductAsync(int id)
    {
        var response = await http.DeleteAsync($"api/product/{id}");
        response.EnsureSuccessStatusCode();
    }
}