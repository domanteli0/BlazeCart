using BlazeCart.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace BlazeCart.Services;

public class ItemService
{
    ObservableCollection<Item> _itemList = new();
    public ObservableCollection<Item> CartItems { get; set; } = new();

    public event EventHandler<CartUsedEventArgs> CartUsed;

    public event EventHandler<EventArgs> CartTbUpdated;

    public event EventHandler<EventArgs> FavTbUpdated;

    
    private HttpClient _client;
    private string BaseUrl = "https://10.0.2.2:5000";

    public ItemService()
    {
        _client = new HttpClient
        {
            BaseAddress = new Uri(BaseUrl),

        };
    }

    public async Task<ObservableCollection<Item>> Get(int index, int count)
    {
        var json = await _client.GetStringAsync($"api/Item/{index}/{count}");
        var jarr = JArray.Parse(json);
        var items = JsonConvert.DeserializeObject<ObservableCollection<Item>>(jarr.ToString());
        return items;
    }
    public async Task<ObservableCollection<Item>> GetItems(string fileName)
    {

        if (_itemList.Count > 0) {
            return _itemList;
        }

        await using var stream = await FileSystem.OpenAppPackageFileAsync(fileName);
        using StreamReader r = new(stream);
        string json = await r.ReadToEndAsync();
        var jobj = JObject.Parse(json);
        _itemList = JsonConvert.DeserializeObject<ObservableCollection<Item>>(jobj["shopItems"].ToString());
        return _itemList;
    }
  
    public void PutItems(ObservableCollection<Item> items)
    {
        CartItems.Clear();
        foreach (var value in items)
        {
            CartItems.Add(value);
        }
        OnCartChanged(new CartUsedEventArgs(CartItems));

    }
    public void AddToCart(Item item)
    {
        var query = CartItems.Where(x => x.Name == item.Name && x.Store == item.Store);
        var result = query.ToList();
        if (result.Count == 0)
        {
            CartItems.Add(item);
        }
        else
        {
            foreach (var value in CartItems)
            {
                if (result.Contains(value))
                    value.Quantity++;
            }
        }
        OnCartChanged(new CartUsedEventArgs(CartItems));
    }
    public void RemoveFromCart(Item item)
    {
        CartItems.Remove(item);
    }
    public virtual void OnCartChanged(CartUsedEventArgs e)
    {
        if (CartUsed != null) CartUsed(this, e);
    }

    public virtual void OnCartTbUpdated(EventArgs e)
    {
        if (CartTbUpdated != null) CartTbUpdated(this, e);
    }
    public virtual void OnFavTbUpdated(EventArgs e)
    {
        if (FavTbUpdated != null) FavTbUpdated(this, e);
    }
}
