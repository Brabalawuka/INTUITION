using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Microsoft.Toolkit.Uwp;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace INTUITION
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class EventDetail : Page
    {
        static StorageFolder folder = Windows.Storage.ApplicationData.Current.LocalFolder;

        public EventDetail()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            int id = (int)e.Parameter;
            Debug.WriteLine(id);

            
            Database db = new Database(folder.Path + "\\sheet.csv");

            string eventTitle = db.getAttributeById(id, "title");
            string eventDescription = db.getAttributeById(id, "description");
            string eventVenue = db.getAttributeById(id, "venue");
            string eventDate = db.getAttributeById(id, "date");
            string eventDuration = db.getAttributeById(id, "time");
            string registration = db.getAttributeById(id, "regrequired");
            Boolean eventRegistration;
            if (registration == "true") eventRegistration = false;
            else eventRegistration = true;

            string eventDetail = db.getAttributeById(id, "detail");
            string eventLongtitude = db.getAttributeById(id, "lon");
            string eventLatitude = db.getAttributeById(id,"lat");
            string eventImageName = db.getAttributeById(id, "image");

            doeverything( eventImageName, eventTitle, eventRegistration, eventDescription, eventDate, eventDuration, eventVenue, eventDetail);

            }

        public void doeverything(string imageloaction, string title, bool IRF, string Osd, string Date, string time, string venue, string detail)
        {


            Eventimage.Source = new BitmapImage(new Uri(folder.Path+"\\"+imageloaction)) ;
            Debug.WriteLine(folder.Path);
            Event.Text = title;
            if (IRF == true)
            {
                registrationimage.Source = new BitmapImage(new Uri(this.BaseUri,"Assets/yes.png"));
                Registerbutton.Visibility = Visibility.Collapsed;
            }
                
            else
            {
                registrationimage.Source = new BitmapImage(new Uri(this.BaseUri,"Assets/no.png"));
                Registerbutton.Visibility = Visibility.Visible;
            }

            OSDblock.Text = Osd;
            Dateblock.Text = "Date: " + Date;
            Timeblock.Text = "Time: " + time;
            Venueblock.Text = "Venue: " + venue;
            detailblock.Text = detail;


        }


        private async void Registerbutton_Click(object sender, RoutedEventArgs e)
        {
            var success = await Windows.System.Launcher.LaunchUriAsync(new Uri("http://medicine.nus.edu.sg/nursing/documents/education/Form-withdrawal-from-univ.pdf"));
        }
    }
}
