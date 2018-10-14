using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
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

        string photoName;
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

            int eventID;//need to assign id
            string eventVenue = EventVenue.Text;
            string eventDescription = OneLineDescription.Text;
            var date = this.EventDate.Date;
            Boolean registration;//need to assign user input
            string eventDetail;//need to assign user input
            Windows.Devices.Geolocation.BasicGeoposition eventlocation;//need to assign user input
           // var anEvent = new Event(eventTitle, eventID, eventDescription,date,registration,eventDetail,eventlocation,photoName);

            //string eventVenue = EventVenue.Text;
            //string eventDescription = OneLineDescription.Text;

           // var anEvent = new Event(eventTitle, 1, eventDescription,);

        }
    }
}
