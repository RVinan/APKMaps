using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using mapsRVinanImp.ViewModels;

namespace mapsRVinanImp
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        MapPageViewModel mapPageViewModel;
        public MainPage()
        {
            InitializeComponent();
            BindingContext = mapPageViewModel = new MapPageViewModel();

        }

        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            var contents = await mapPageViewModel.LoadVehicles();

            if (contents != null)
            {
                foreach (var item in contents)
                {
                    Pin VehiclePins = new Pin()
                    {
                        Label = "Cars",
                        Type = PinType.Place,
                        Icon = (Device.RuntimePlatform == Device.Android) ? BitmapDescriptorFactory.FromBundle("CarPins.png") : BitmapDescriptorFactory.FromView(new Image() { Source = "CarPins.png", WidthRequest = 30, HeightRequest = 30 }),
                        Position = new Position(Convert.ToDouble(item.Latitude), Convert.ToDouble(item.Longitude)),

                    };
                    map.Pins.Add(VehiclePins);
                }
            }

            //This is your location and it should be near to your car location.
            var positions = new Position(28.126825, 82.297106);//Latitude, Longitude
            map.MoveToRegion(MapSpan.FromCenterAndRadius(positions, Distance.FromMeters(100)));

        }
    }
}
