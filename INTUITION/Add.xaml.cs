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
            //file = await folder.CreateFileAsync(file.Name+ ".jpg");

            string filename = file.Name + ".jpg";

            setPhotoName(filename);

        }


<<<<<<< HEAD

        string photoName;
=======
        string photoName = " ";
>>>>>>> b6e1b1ecade614002499f7e0b92dcb9f9a7daae1
        private void setPhotoName(string photoName)
        {
            this.photoName = photoName;
        }




        private void UploadPhoto_Click(object sender, RoutedEventArgs e)
        {
            SelectPhoto();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string eventTitle = EventName.Text;
<<<<<<< HEAD

            int eventID;//need to assign id
            string eventVenue = EventVenue.Text;
            string eventDescription = OneLineDescription.Text;
            var date = this.EventDate.Date;
            Boolean registration;//need to assign user input
            string eventDetail;//need to assign user input
            Windows.Devices.Geolocation.BasicGeoposition eventlocation;//need to assign user input
            var anEvent = new Event(eventTitle, eventID, eventDescription,date,registration,eventDetail,eventlocation,photoName);


=======
            string myphotoname = photoName;

            int eventID = 0005 ;//need to assign id
            string eventVenue = EventVenue.Text;
            string eventDescription = OneLineDescription.Text;
            var date = this.EventDate.Date;
            Boolean registration = IRR.IsOn;//need to assign user input
            string eventDetail = detail.Text;//need to assign user input
            
            

            
            var anEvent = new Event(eventTitle, eventID, eventDescription, date, registration, eventDetail, defaultposition, myphotoname);
            

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

>>>>>>> b6e1b1ecade614002499f7e0b92dcb9f9a7daae1
        }
    }
}
