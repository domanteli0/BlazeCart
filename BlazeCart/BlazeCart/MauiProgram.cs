using BlazeCart.Views;
using BlazeCart.Services;
using BlazeCart.ViewModels;

namespace BlazeCart;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
                fonts.AddFont("Poppins-Bold.ttf", "Poppins-Bold");
                fonts.AddFont("Poppins-Light.ttf", "Poppins-Light");
                fonts.AddFont("Poppins-Medium.ttf", "Poppins-Medium");
                fonts.AddFont("Poppins-Regular.ttf", "Poppins-Regular");
                fonts.AddFont("Poppins-SemiBold.ttf", "Poppins-SemiBold");
                fonts.AddFont("Roboto-Black.ttf", "Roboto-Black");
                fonts.AddFont("Roboto-BlackItalic.ttf", "Roboto-BlackItalic");
                fonts.AddFont("Roboto-Bold.ttf", "Roboto-Bold");
                fonts.AddFont("Roboto-BoldItalic.ttf", "Roboto-BoldItalic");
                fonts.AddFont("Roboto-Italic.ttf", "Roboto-Italic");
                fonts.AddFont("Roboto-Light.ttf", "Roboto-Light");
                fonts.AddFont("Roboto-LightItalic.ttf", "Roboto-LightItalic");
                fonts.AddFont("Roboto-Medium.ttf", "Roboto-Medium");
                fonts.AddFont("Roboto-MediumItalic.ttf", "Roboto-MediumItalic");
                fonts.AddFont("Roboto-Regular.ttf", "Roboto-Regular");
                fonts.AddFont("Roboto-Thin.ttf", "Roboto-Thin");
                fonts.AddFont("Roboto-ThinItalic.ttf", "Roboto-ThinItalic");

            });

        builder.Services.AddTransient<ItemCatalogPage>();
        builder.Services.AddSingleton<ItemService>();
        builder.Services.AddTransient<ItemsViewModel>();
        return builder.Build();
	}
}
