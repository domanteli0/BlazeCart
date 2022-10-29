using BlazeCart.ViewModels;
using System.Reflection;
using BlazeCart.Data;

namespace BlazeCart.Views;

public partial class ItemCatalogPage : ContentPage
{
	public ItemCatalogPage(ItemsViewModel vm)
	{
		InitializeComponent();

        /*
        //TODO: insert database address
        const String databasePlace = "BlazeCart.Databases.data_base_name.db";

        //TODO Launch when the app firs starts, not when page is created multiple times
        //TODO or reload when there's changes in the database
        //Initialization of itemCatalogDB
        var assembly = IntrospectionExtensions.GetTypeInfo(typeof(App)).Assembly;
        using (Stream stream = assembly.GetManifestResourceStream(databasePlace))
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);

                File.WriteAllBytes(ItemRepository.DbPath, memoryStream.ToArray());
            }
        }*/

        BindingContext = vm;
    }

}
