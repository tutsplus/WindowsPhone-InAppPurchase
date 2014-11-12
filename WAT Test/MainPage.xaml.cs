using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WAT_Test.Resources;
using Windows.ApplicationModel.Store;
using System.Collections.ObjectModel;
using GoogleAds;

namespace WAT_Test
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        
        public MainPage()
        {
            InitializeComponent();
            if(AppSettings.DISPLAYADS)
            admob(AdGrid);
        }

        void admob(StackPanel stck)
        {
            AdView bannerAd = new AdView
            {
                Format = AdFormats.Banner,
                AdUnitID = "ca-app-pub-0623406534268990/5403604500"
            };
            AdRequest adRequest = new AdRequest();
            stck.Children.Add(bannerAd);
            bannerAd.LoadAd(adRequest);
            bannerAd.FailedToReceiveAd += bannerAd_FailedToReceiveAd;
            bannerAd.ReceivedAd += bannerAd_ReceivedAd;
        }

        void bannerAd_ReceivedAd(object sender, AdEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Ad received");
        }

        void bannerAd_FailedToReceiveAd(object sender, AdErrorEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Ad failed");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/RemoveAds.xaml",UriKind.Relative));
        }
        
    }
}