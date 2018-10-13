using System;
using System.Collections.Generic;
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
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace INTUITION
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private async void Dialog(string args)
        {


            ContentDialog dialog = new ContentDialog
            {
                Title = "Errrrr....",
                Content = args,
                CloseButtonText = "Ok"
            };

            ContentDialogResult result = await dialog.ShowAsync();
        }


        public MainPage()
        {
            this.InitializeComponent();
            MainPageFrame.Navigate(typeof(MapView));
        }

        private void Menu_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                MainPageFrame.Navigate(typeof(Setting));
            }
            else
            {
                NavigationViewItem item = args.SelectedItem as NavigationViewItem;
                switch (item.Tag.ToString())
                {
                    case "MapView":
                        MainPageFrame.Navigate(typeof(MapView));
                        break;
                    case "ListView":
                        MainPageFrame.Navigate(typeof(MapView));
                        break;
                    case "AddEvent":
                        MainPageFrame.Navigate(typeof(Add));
                        break;


                }
            }
        }

        private void FeedbackButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Dialog("Surprise Motherf*****!!!!");

        }
    }
}
