using System.Collections.ObjectModel;
using BlazeCart.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using MonkeyCache.FileStore;
namespace BlazeCart.Services;

public class CategoryService
{
    private static HttpClient _client;
    
    public CategoryService(string baseUrl)
    {
        _client = new HttpClient
        {
            BaseAddress = new Uri(baseUrl),
        };
    }

    public async Task<int> GetCategoriesCount()
    {
        var cats = await GetAsync<ObservableCollection<Category>>($"api/Category", "categoriesAll");
        return cats.Count;
    }
    public async Task<ObservableCollection<Category>> GetCategories(int index, int count)
    {
        var cats = await GetAsync<ObservableCollection<Category>>($"api/Category/{index}/{count}", "categories" + index + count);
        return cats;
    }

    public async Task<ObservableCollection<Item>> GetItemsByCategoryId(Guid id)
    {
        var items = await GetAsync<ObservableCollection<Item>>($"api/Category/{id}/items", "items" + id);
        return items;
    }
    public async Task<ObservableCollection<Item>> GetRangeOfItemsByCategoryId(Guid id, int index, int count)
    {
        var items = await GetAsync<ObservableCollection<Item>>($"api/Category/{id}/{index}/{count}", "items" + id + index + count);
        return items;
    }
    static async Task<T> GetAsync<T>(string url, string key, int days = 3, bool forceRefresh = false)
    {
        var json = string.Empty;

        if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            json = Barrel.Current.Get<string>(key);
        else if (!forceRefresh && !Barrel.Current.IsExpired(key))
            json = Barrel.Current.Get<string>(key);

        try
        {
            if (string.IsNullOrWhiteSpace(json))
            {
                json = await _client.GetStringAsync(url);

                Barrel.Current.Add(key, json, TimeSpan.FromDays(days));
            }
            var jarr = JArray.Parse(json);
            return JsonConvert.DeserializeObject<T>(jarr.ToString());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
