using PopupPerformanceSample.Models;
using PopupPerformanceSample.Views;

namespace PopupPerformanceSample
{
    public partial class MainPage : ContentPage
    {
        private ProductDetailsPopupView? _cachedPopupView;

        public MainPage()
        {
            InitializeComponent();
            LoadProducts();
        }

        private void LoadProducts()
        {
            var products = new List<Product>
           {
              new Product { Name="Laptop", Description="High performance laptop", Price=1200, MinPrice=999, MaxPrice=1899 },
              new Product { Name="Smartphone", Description="Latest model smartphone", Price=800, MinPrice=599, MaxPrice=1299 },
              new Product { Name="Headphones", Description="Noise cancelling headphones", Price=200, MinPrice=149, MaxPrice=349 },
              new Product { Name="Smartwatch", Description="Feature-rich smartwatch with health tracking", Price=250, MinPrice=199, MaxPrice=499 },
              new Product { Name="Tablet", Description="Lightweight tablet for work and entertainment", Price=450, MinPrice=299, MaxPrice=899 },
              new Product { Name="Gaming Console", Description="Next-gen gaming console with 4K support", Price=500, MinPrice=399, MaxPrice=699 },
              new Product { Name="Bluetooth Speaker", Description="Portable speaker with deep bass", Price=150, MinPrice=79, MaxPrice=299 }, new Product { Name="Wireless Earbuds", Description="Compact earbuds with superior sound quality", Price=180, MinPrice=129, MaxPrice=299 },
              new Product { Name="4K Monitor", Description="Ultra HD monitor for gaming and productivity", Price=350, MinPrice=249, MaxPrice=599 },
              new Product { Name="External SSD", Description="High-speed portable SSD for data storage", Price=220, MinPrice=149, MaxPrice=399 },
              new Product { Name="Home Theater System", Description="Immersive surround sound system for home entertainment", Price=800, MinPrice=599, MaxPrice=1299 }

           };

            ProductList.ItemsSource = products;
        }

        private async void OnAddToCartRequested(object? sender, Product product)
        {
            ProductPopup.IsOpen = false;
            await Task.Delay(500);
            await DisplayAlertAsync("Cart", $"{product.Name} added to cart!", "OK");
        }

        private async void OnProductSelected(object sender, Syncfusion.Maui.ListView.ItemTappedEventArgs e)
        {
            if (e.DataItem == null) return;

            var selectedProduct = e.DataItem as Product;

            if (_cachedPopupView == null)
            {
                _cachedPopupView = new ProductDetailsPopupView();
                _cachedPopupView.AddToCartRequested += OnAddToCartRequested;
            }

            if (selectedProduct != null)
            {
                await _cachedPopupView.InitializeAsync(selectedProduct);
            }

            ProductPopup.ContentTemplate = new DataTemplate(() => _cachedPopupView);

            await this.FadeToAsync(0.8, 150);
            ProductPopup.Show();
            await this.FadeToAsync(1, 150);

            ProductList.SelectedItem = null;
        }
    }
}
