using BlazeCart.Models;
using BlazeCart.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using BlazeCart.Views;

namespace BlazeCart.ViewModels
{
    [QueryProperty(nameof(TotalPrice), "TotalPrice")]
    public partial class CheapestStorePageViewModel : BaseViewModel
    {

        public ObservableCollection<Item> CartItems { get; set; } = new();
        private ItemService _itemService;
        [ObservableProperty] double totalPrice;
        private static CancellationTokenSource _cancelTokenSource;
        private static bool _isCheckingLocation;

        public CheapestStorePageViewModel(ItemService itemservice) {
            _itemService = itemservice;
            _itemService.CheapestCart += CheapestCartEventHandler;


        }

        private void CheapestCartEventHandler(object sender, CartUsedEventArgs e)
        {
            CartItems.Clear();
            foreach (var item in e.Items)
            {
                CartItems.Add(item);
                totalPrice = (item.Price * item.Quantity) + totalPrice;
            }
            
        }

        [RelayCommand]
        async void BackToCart(object obj)
        {
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        async void GoToMaps(object obj)
        {
            try {
                String coordinates = await GetCachedLocation();
                if (coordinates != "none")
                {
                    await Shell.Current.GoToAsync($"{nameof(GoogleMaps)}?coordinates={coordinates}");
                }
                else {
                    coordinates = await GetCurrentLocation();
                    if (coordinates != "none")
                    {
                        await Shell.Current.GoToAsync($"{nameof(GoogleMaps)}?coordinates={coordinates}");
                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("Klaida!","Nepavyko gauti koordinačių","OK");
                    }
                }
            }
            catch(AggregateException ex)
            {
                foreach (Exception innerException in ex.InnerExceptions)
                {
                    await Shell.Current.DisplayAlert("Nepavyko gauti koordinačių", innerException.Message, "OK");
                }
            }          
        }
  
        private static async Task<string> GetCachedLocation()
        {
            var exceptions = new List<Exception>();
            try
            {
                Location location = await Geolocation.Default.GetLastKnownLocationAsync();
                if (location != null)
                    return $"{location.Latitude}%2C{location.Longitude}";
                else {
                    return "none";
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                exceptions.Add(fnsEx);
            }
            catch (FeatureNotEnabledException fneEx)
            {
                exceptions.Add(fneEx);
            }
            catch (PermissionException pEx)
            {
                exceptions.Add(pEx);
            }
            catch (Exception ex)
            {
                exceptions.Add(ex);
            }
            finally
            {
                if (exceptions.Count > 0)
                {
                    throw new AggregateException("Error in get Catched location", exceptions);
                }
            }
            return "none";
        }

        private static async Task<string> GetCurrentLocation()
        {
            try
            {
                _isCheckingLocation = true;

                GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));

                _cancelTokenSource = new CancellationTokenSource();

                Location location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);

                if (location != null)
                    return $"{location.Latitude}%2C{location.Longitude}";
                else {
                    return "none";
                }
            }
            catch (Exception ex)
            {
                return "none";
            }
            finally
            {
                _isCheckingLocation = false;
            }
        }

        public void CancelRequest()
        {
            if (_isCheckingLocation && _cancelTokenSource != null && _cancelTokenSource.IsCancellationRequested == false)
                _cancelTokenSource.Cancel();
        }


    }
}
