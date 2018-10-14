using System;
using System.Collections.Generic;
using System.Diagnostics;
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
           

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Windows.Storage.StorageFolder folder = Windows.Storage.ApplicationData.Current.LocalFolder;
            Database db = new Database(folder.Path + "\\sheet.csv");
            // Specify a known location.
            BasicGeoposition cityPosition = new BasicGeoposition() { Latitude = 1.348, Longitude = 103.6827 };
            Geopoint cityCenter = new Geopoint(cityPosition);
            // Set the map location.
            MapControl1.Center = cityCenter;
            //Load all pins
 
                //testing
                int id=1;

                string eventTitle;
                string eventLatitude;
                string eventLongtitude;
                string eventImageName;

                double latitude;
                double longtitude;


            while (db.getAttributeById(id, "title") != null)
            {
                eventTitle = db.getAttributeById(id, "title");
                eventLatitude = db.getAttributeById(id, "lat");
                eventLongtitude = db.getAttributeById(id, "lon");
                eventImageName = db.getAttributeById(id, "image");

                latitude = double.Parse(eventLatitude);
                longtitude = double.Parse(eventLongtitude);

                BasicGeoposition iconPosition = new BasicGeoposition() { Latitude = latitude, Longitude = longtitude };
                AddSpaceNeedleIcon(eventTitle, id, iconPosition);

                id++;
            }
        }

        public void AddSpaceNeedleIcon(string title, int id, BasicGeoposition iconposition)
        {
            var MyLandmarks = new List<MapElement>();

            BasicGeoposition snPosition = iconposition;
            Geopoint snPoint = new Geopoint(snPosition);

            MapIcon spaceNeedleIcon = new MapIcon
            {
                Location = snPoint,
                NormalizedAnchorPoint = new Point(0.5, 1.0),
                Tag = id,
                Title = title,
                

            };

            MapControl1.MapElements.Add(spaceNeedleIcon);

            

        }

        

        private async void MapControl1_MapElementClick(MapControl sender, MapElementClickEventArgs args)
        {
            Windows.Storage.StorageFolder folder = Windows.Storage.ApplicationData.Current.LocalFolder;
            Database db = new Database(folder.Path + "\\sheet.csv");
            MapIcon myClickedIcon = args.MapElements.FirstOrDefault(x => x is MapIcon) as MapIcon;
            EventDialog.Title = myClickedIcon.Title;
            EventDialog.Tag =  myClickedIcon.Tag;
            int eventId = int.Parse(EventDialog.Tag.ToString());


            Tilteblock.Text = db.getAttributeById(eventId, "description"); ////fill in one sentence discription
            Time.Text = "Date:" + db.getAttributeById(eventId,"date");      //////fill in date
            Venue.Text = "Venue" + db.getAttributeById(eventId,"venue");      /////fill in venue

            await EventDialog.ShowAsync();



        }



        private void EventDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            
            Frame.Navigate(typeof(EventDetail), sender.Tag);
        }
    }
}
