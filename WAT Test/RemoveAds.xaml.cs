using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Windows.ApplicationModel.Store;
using System.Collections.ObjectModel;

namespace WAT_Test
{
    public partial class RemoveAds : PhoneApplicationPage
    {
        public class ProductItem
        {
            public string imgLink { get; set; }
            public string Status { get; set; }
            public string Name { get; set; }
            public string key { get; set; }
            public System.Windows.Visibility BuyNowButtonVisible { get; set; }
        }
        public RemoveAds()
        {
            InitializeComponent();
        }
        private async void ButtonBuyNow_Clicked(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;

            string key = btn.Tag.ToString();

            if (!Windows.ApplicationModel.Store.CurrentApp.LicenseInformation.ProductLicenses[key].IsActive)
            {
                ListingInformation li = await Windows.ApplicationModel.Store.CurrentApp.LoadListingInformationAsync();
                string pID = li.ProductListings[key].ProductId;

                string receipt = await Windows.ApplicationModel.Store.CurrentApp.RequestProductPurchaseAsync(pID, false);

                // RenderStoreItems();
            }
        }
        public ObservableCollection<ProductItem> picItems = new ObservableCollection<ProductItem>();

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            RenderStoreItems();
            base.OnNavigatedTo(e);
        }

        private async void RenderStoreItems()
        {
            picItems.Clear();

            try
            {
                //StoreManager mySM = new StoreManager();
                ListingInformation li = await Windows.ApplicationModel.Store.CurrentApp.LoadListingInformationAsync();

                foreach (string key in li.ProductListings.Keys)
                {
                    ProductListing pListing = li.ProductListings[key];
                    System.Diagnostics.Debug.WriteLine(key);

                    string status = Windows.ApplicationModel.Store.CurrentApp.LicenseInformation.ProductLicenses[key].IsActive ? "Purchased" : pListing.FormattedPrice;

                    string imageLink = string.Empty;

                    picItems.Add(
                        new ProductItem
                        {
                            imgLink = key.Equals("AdBlocker") ? "block-ads.png" : "block-ads.png",
                            Name = pListing.Name,
                            Status = status,
                            key = key,
                            BuyNowButtonVisible = Windows.ApplicationModel.Store.CurrentApp.LicenseInformation.ProductLicenses[key].IsActive ? System.Windows.Visibility.Collapsed : System.Windows.Visibility.Visible
                        }
                    );
                }

                pics.ItemsSource = picItems;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
        }
    }
}