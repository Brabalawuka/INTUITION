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

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace INTUITION
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class EventDetail : Page
    {
        public EventDetail()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            int id = (int)e.Parameter;
            Debug.WriteLine(id);
            doeverything("aaa", "GREATEST EVENT EVER", false, "This is the event once in a life time, never miss it !!!!!!", "14 Oct 2018", "7pm-9pm", "LHR_TR_16", "Lorem ipsum dolor sit amet, his in enim postulant theophrastus. Wisi virtute percipit ea mel, nam ut iuvaret debitis. Blandit percipit pri ea, an sed nihil reprehendunt, in mei facilisis definitiones. No vim alia utroque definiebas, no tota exerci duo, nobis laoreet ei eum.In sit brute justo instructior, vel eu laudem laboramus.Et sit sumo facer, ius no affert accusam conclusionemque.Diam blandit an has.Ius te nihil repudiare.Est solum putant inimicus ut, doctus blandit verterem eos at.Alienum delicata mel ex, ne omnes affert pri, an vel everti graecis disputationi.Melius maluisset tincidunt te vis, fabulas apeirian definiebas ius te.In est affert ornatus, eum essent dissentiunt te.At evertitur constituam sed, et primis qualisque ius, magna detraxit assueverit his ex.In verear eripuit consequat quo, has ne diceret bonorum.Ad case dicit affert mei.At vim tation deserunt.Ridens doming duo eu, eu augue detraxit mei.Novum iriure discere te mel, id vim invenire molestiae disputando.Ad usu tractatos maiestatis vituperata, mei at laboramus persecuti, cum sumo primis officiis no.");
        }

        public void doeverything(string imageloaction, string title, bool IRF, string Osd, string Date, string time, string venue, string detail)
        {
            Eventimage.Source = new BitmapImage(new Uri(this.BaseUri,"Assets/first.jpg")) ;
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
