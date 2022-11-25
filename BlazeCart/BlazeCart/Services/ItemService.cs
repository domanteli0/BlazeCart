using BlazeCart.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
//using Android.Accounts;

namespace BlazeCart.Services;

public class ItemService
{
    ObservableCollection<Item> _itemList = new();
    public ObservableCollection<Item> CartItems { get; set; } = new();

    public event EventHandler<CartUsedEventArgs> CartUsed;

    public event EventHandler<EventArgs> CartTbUpdated;

    public event EventHandler<EventArgs> FavTbUpdated;

    
    private HttpClient _client;
    private string _baseUrl = "https://blazecartapi.azurewebsites.net/";

    public ItemService()
    {
        _client = new HttpClient()
        {
            BaseAddress = new Uri(_baseUrl),

        };
    }

    public async Task<ObservableCollection<Item>> Get(int index, int count)
    {
        // TODO: Use 
        //var json = await _client.GetStringAsync($"api/Item/{index}/{count}");
        //var json = await _client.GetStringAsync("https://blazecartapi.azurewebsites.net/api/category/190a0a4d-6da8-47b9-faa7-08dacbebfe52/items");
        //var response = await _client.SendAsync("https://blazecartapi.azurewebsites.net/api/category/190a0a4d-6da8-47b9-faa7-08dacbebfe52/items");

        var response = await (new HttpClient()).SendAsync(
            new HttpRequestMessage(
                new HttpMethod("GET"),
                "https://blazecartapi.azurewebsites.net/api/category/190a0a4d-6da8-47b9-faa7-08dacbebfe52/items"
            )
        );

        StreamReader reader = new StreamReader(response.Content.ReadAsStream());

        var jarr = JArray.Parse(await reader.ReadToEndAsync());
        var items = JsonConvert.DeserializeObject<ObservableCollection<Item>>(jarr.ToString());
        return items;
    }
    public async Task<ObservableCollection<Item>> GetItems(string fileName)
    {

        if (_itemList.Count > 0) {
            return _itemList;
        }

        //await using var stream = await FileSystem.OpenAppPackageFileAsync(fileName);
        //var stream = await
        //var json = await _client.GetStringAsync($"api/item/{0}/{20}");
        var response = await (new HttpClient()).SendAsync(
            new HttpRequestMessage(
                new HttpMethod("GET"),
                "https://blazecartapi.azurewebsites.net/api/category/190a0a4d-6da8-47b9-faa7-08dacbebfe52/items"
            )
        );

        StreamReader reader = new StreamReader(response.Content.ReadAsStream());
        //Console.WriteLine(json);
        //using StreamReader r = new(stream);
        //string json = await r.ReadToEndAsync();
        var jsonStr = reader.ReadToEnd();

        //throw new Exception(jsonStr);

        int ix = 0;
        foreach (JObject obj in JArray.Parse(jsonStr))
        {
            Console.WriteLine(obj.ToString());

            _itemList.Add(new Item()
            {
                ItemId = (ix += 1),
                Category = "Lol",
                Name = obj["nameLT"]!.ToString(),
                Price = (double) obj["price"]!.ToObject<int>(),
                PackageAmount = (double) obj["ammount"].ToObject<int>(),
                Units = "kg",
                PricePerUnit = 1.05,
                Description = obj["description"].ToString(),
                Origin = "Ekvadoras",
                Components = "Components",
                Image = obj["image"].ToObject<Uri>(),
                Store = obj["store"].ToString(),
                Availability = true,
            });
        }

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
