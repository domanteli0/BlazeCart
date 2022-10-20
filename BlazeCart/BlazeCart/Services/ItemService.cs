using BlazeCart.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.ObjectModel;


namespace BlazeCart.Services;

public class ItemService
{
    ObservableCollection<Item> _itemList = new();
    public async Task<ObservableCollection<Item>> GetItems()
    {

        if (_itemList.Count > 0) {
            return _itemList;
        }

        using var stream = await FileSystem.OpenAppPackageFileAsync("shopItems.json");
        using StreamReader r = new(stream);
        string json = r.ReadToEnd();
        var jobj = JObject.Parse(json);
        _itemList = JsonConvert.DeserializeObject<ObservableCollection<Item>>(jobj["shopItems"].ToString());
        return _itemList;
    }
}
