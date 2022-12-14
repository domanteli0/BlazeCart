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

    public event EventHandler<CartUsedEventArgs> CheapestCart;


    private HttpClient _client;

    public ItemService(string baseUrl)
    {
        _client = new HttpClient
        {
            BaseAddress = new Uri(baseUrl),

        };
    }

    public async Task<ObservableCollection<Item>> Get(int index, int count)
    {
        var json = await _client.GetStringAsync($"api/Item/{index}/{count}");
        var jarr = JArray.Parse(json);
        var items = JsonConvert.DeserializeObject<ObservableCollection<Item>>(jarr.ToString());

        var cats = await _client.GetStringAsync($"api/Item/{index}/{count}/cats");
        var jarCats = JArray.Parse(cats);
        var categories = JsonConvert.DeserializeObject<List<String>>(jarCats.ToString());
        foreach (var item in items)
        {
            for(int i = 0; i < categories.Count(); i++)
            {
                item.Category = categories[i];
            }
        }
        
        return items;
    }
    public async Task<ObservableCollection<Item>> GetCheapestItems(ObservableCollection<Item> items)
    {
        ObservableCollection<Item> cheapestItems = new ObservableCollection<Item>();
        foreach(var item in items)
        {
            string name = item.NameLT;
            string? category = item.Category;
            double price = item.Price;
            double? amount = item.Ammount;
            
            var json = await _client.GetStringAsync($"api/Item/{name}/{category}/{price}/{amount}");
            
            cheapestItems.Add((JsonConvert.DeserializeObject<Item>(json.ToString())));
        }
        return cheapestItems;
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
        var query = CartItems.Where(x => x.NameLT == item.NameLT && x.Store == item.Store);
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
    public virtual void OnCheapestCart(CartUsedEventArgs e)
    {
        if (CheapestCart != null) CheapestCart(this, e);
    }
}
