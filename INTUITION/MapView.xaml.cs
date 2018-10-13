using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace INTUITION
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MapView : Page
    {
        public MapView()
        {
            this.InitializeComponent();
            
            AddSpaceNeedleIcon("First place");
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Specify a known location.
            BasicGeoposition cityPosition = new BasicGeoposition() { Latitude = 1.348, Longitude = 103.6827 };
            Geopoint cityCenter = new Geopoint(cityPosition);

            // Set the map location.
            MapControl1.Center = cityCenter;
            MapControl1.ZoomLevel = 15.4;
            MapControl1.LandmarksVisible = true;
        }

        public void AddSpaceNeedleIcon(string title )
        {
            var MyLandmarks = new List<MapElement>();

            BasicGeoposition snPosition = new BasicGeoposition { Latitude = 1.348, Longitude = 103.6827 };
            Geopoint snPoint = new Geopoint(snPosition);

            MapIcon spaceNeedleIcon = new MapIcon
            {
                Location = snPoint,
                NormalizedAnchorPoint = new Point(0.5, 1.0),
                
                Title = title,
                CollisionBehaviorDesired = MapElementCollisionBehavior.RemainVisible,

            };

            MapControl1.MapElements.Add(spaceNeedleIcon);

            

        }

        

        private async void MapControl1_MapElementClick(MapControl sender, MapElementClickEventArgs args)
        {
            MapIcon myClickedIcon = args.MapElements.FirstOrDefault(x => x is MapIcon) as MapIcon;
            EventDialog.Title = myClickedIcon.Title;
            Tilteblock.Text = "This is a event for all animal lovers to network!";
            Time.Text = "Time:" + "2018-10-12";
            Venue.Text = "Venue" + "LHS_TR_16";

            await EventDialog.ShowAsync();


        }



        private void EventDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {

        }
    }
}
