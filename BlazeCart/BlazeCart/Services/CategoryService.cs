using System.Collections.ObjectModel;
using BlazeCart.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BlazeCart.Services;

public class CategoryService
{
    private HttpClient _client;
    private string BaseUrl = "https://blazecartapi.azurewebsites.net/";
    
    public CategoryService()
    {
        _client = new HttpClient
        {
            BaseAddress = new Uri(BaseUrl),
        };
    }
    public async Task<ObservableCollection<Category>> GetCategories(int index, int count)
    {
        var json = await _client.GetStringAsync($"api/Category/{index}/{count}");
        var jarr = JArray.Parse(json);
        var cats = JsonConvert.DeserializeObject<ObservableCollection<Category>>(jarr.ToString());

        return cats;
    }

    public async Task<ObservableCollection<Item>> GetItemsByCategoryId(Guid id)
    {
        var json = await _client.GetStringAsync($"api/Category/{id}/items");
        var jarr = JArray.Parse(json);
        var items = JsonConvert.DeserializeObject<ObservableCollection<Item>>(jarr.ToString());

        return items;
    }
    public async Task<ObservableCollection<Item>> GetRangeOfItemsByCategoryId(Guid id, int index, int count)
    {
        var json = await _client.GetStringAsync($"api/Category/{id}/{index}/{count}");
        var jarr = JArray.Parse(json);
        var items = JsonConvert.DeserializeObject<ObservableCollection<Item>>(jarr.ToString());

        return items;
    }
}
