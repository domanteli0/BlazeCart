using BlazeCart.Views;
using BlazeCart.Services;
using BlazeCart.ViewModels;
using Syncfusion.Maui.Core.Hosting;

namespace BlazeCart;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .ConfigureSyncfusionCore()
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

        builder.Services.AddSingleton<ItemCatalogPage>();
        builder.Services.AddSingleton<ItemService>();
        builder.Services.AddSingleton<ItemsViewModel>();

        builder.Services.AddSingleton<RegisterPage>();
        builder.Services.AddSingleton<RegisterPageViewModel>();

        builder.Services.AddSingleton<LoginPage>();
        builder.Services.AddSingleton<LoginPageViewModel>();

        builder.Services.AddSingleton<CartPage>();
        builder.Services.AddSingleton<CartPageViewModel>();
        builder.Services.AddSingleton<CartService>();

        builder.Services.AddSingleton<HomePage>();
        builder.Services.AddSingleton<HomePageViewModel>();

        builder.Services.AddSingleton<WelcomePage1>();
        builder.Services.AddSingleton<WelcomePage1ViewModel>();

        builder.Services.AddSingleton<WelcomePage2>();
        builder.Services.AddSingleton<WelcomePage2ViewModel>();

        builder.Services.AddSingleton<ErrorPage>();
        builder.Services.AddSingleton<ErrorPageViewModel>();

        builder.Services.AddSingleton<CategoryPage>();

        builder.Services.AddSingleton<CheapestStorePage>();
        builder.Services.AddSingleton<CheapestStorePageViewModel>();

        builder.Services.AddTransient<ItemPage>();
        builder.Services.AddTransient<ItemPageViewModel>();
        return builder.Build();
	}
}
