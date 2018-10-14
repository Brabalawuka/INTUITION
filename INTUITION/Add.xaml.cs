using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class Add : Page
    {
        BasicGeoposition defaultposition = new BasicGeoposition() { Latitude = 1.348, Longitude = 103.6827 };

        public Add()
        {
            this.InitializeComponent();

        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await Task.Delay(1);
            base.OnNavigatedTo(e);
            if (App.getLogInStatus() == false)
            {
                var result = await LoginDialog.ShowAsync();
            }

            BasicGeoposition cityPosition = new BasicGeoposition() { Latitude = 1.348, Longitude = 103.6827 };
            Geopoint cityCenter = new Geopoint(cityPosition);
            // Set the map location.
            mymap.Center = cityCenter;
            mymap.ZoomLevel = 15;





        }
        private void SignInButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            App.setLogInStatus();
            LoginDialog.Hide();
        }


        private async void SelectPhoto() {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");

            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                // Application now has read/write access to the picked file
                UploadPhoto.Content = "Picked photo: " + file.Name;
            }
            else
            {
                UploadPhoto.Content = "Operation cancelled.";
            }
            Windows.Storage.StorageFolder folder = Windows.Storage.ApplicationData.Current.LocalFolder;
            await file.CopyAsync(folder,file.Name);

            string filename = file.Name;

            setPhotoName(filename);

        }




        string photoName;
        private void setPhotoName(string photoName)
        {
            this.photoName = photoName;
        }

        string longitude;
        string latitude;
        private void setLocation(string latitude, string longtitude) {
            this.longitude = longtitude;
            this.latitude = latitude;

        }


        private void UploadPhoto_Click(object sender, RoutedEventArgs e)
        {
            SelectPhoto();
        }

    

        private void mymap_MapDoubleTapped(MapControl sender, MapInputEventArgs args)
        {
            mymap.MapElements.Clear();
            defaultposition = args.Location.Position;
            Geopoint snPoint = new Geopoint(defaultposition);

            MapIcon spaceNeedleIcon = new MapIcon
            {
                Location = snPoint,
                NormalizedAnchorPoint = new Point(0.5, 0.5),
                Title = "Event Venue"

            };

            mymap.MapElements.Add(spaceNeedleIcon);

            setLocation(defaultposition.Latitude.ToString(), defaultposition.Longitude.ToString());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string eventTitle = EventName.Text;

            string myphotoname = photoName;

            int eventID = 0005;
            string eventVenue = EventVenue.Text;
            string eventDescription = OneLineDescription.Text;
            var date = this.EventDate.Date;
            string eventDate = date.ToString();
            Boolean registration = IRR.IsOn;
            string NeedForRegistration = registration.ToString();
            string eventDetail = detail.Text;
            string eventDuration = Timeandduration.Text;


            Windows.Storage.StorageFolder folder = Windows.Storage.ApplicationData.Current.LocalFolder;
            
            Database test = new Database(folder.Path+"\\sheet.csv");
            int createdEventId = test.createEvent(eventTitle,  eventDescription,eventVenue, eventDate, eventDuration, NeedForRegistration, eventDetail, this.longitude,this.latitude, this.photoName);

            var anEvent = new Event(eventTitle, createdEventId, eventDescription, date, registration, eventDetail, defaultposition, myphotoname);

            Frame.Navigate(typeof(MapView));



        }



    }
}
