
using System.Collections.ObjectModel;
using BlazeCart.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BlazeCart.Services;

public class CategoryService
{
    ObservableCollection<Category> _categoryList = new();
    public async Task<ObservableCollection<Category>> GetCategories()
    {

        if (_categoryList.Count > 0)
        {
            return _categoryList;
        }

        using var stream = await FileSystem.OpenAppPackageFileAsync("category.json");
        using StreamReader r = new(stream);
        string json = r.ReadToEnd();
        var jobj = JObject.Parse(json);
        _categoryList = JsonConvert.DeserializeObject<ObservableCollection<Category>>(jobj["category"].ToString());
        return _categoryList;
    }
}

