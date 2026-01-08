using PopupPerformanceSample.Models;
using PopupPerformanceSample.Views;

namespace PopupPerformanceSample
{
    /// <summary>
    /// The main page that lists products and shows a popup with details when an item is tapped.
    /// </summary>
    public partial class MainPage : ContentPage
    {
        /// <summary>
        /// Cached instance of the product details view to avoid recreating it for every selection.
        /// </summary>
        private ProductDetailsPopupView? _cachedPopupView;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainPage"/> class.
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
            // Create and cache the popup content once; set ContentTemplate once to avoid template churn
            _cachedPopupView = new ProductDetailsPopupView();
            _cachedPopupView.AddToCartRequested += OnAddToCartRequested;
            ProductPopup.ContentTemplate = new DataTemplate(() => _cachedPopupView);

            LoadProducts();
        }

        /// <summary>
        /// Populates the product list with sample data.
        /// </summary>
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

        /// <summary>
        /// Handles the Add to Cart event from the popup view, closes the popup and shows a confirmation.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="product">The product added to the cart.</param>
        private async void OnAddToCartRequested(object? sender, Product product)
        {
            ProductPopup.IsOpen = false;
            await Task.Delay(500);
            await DisplayAlertAsync("Cart", $"{product.Name} added to cart!", "OK");
        }

        /// <summary>
        /// Handles product selection from the list and displays the popup with details.
        /// </summary>
        /// <param name="sender">The list view raising the event.</param>
        /// <param name="e">Tap event data containing the selected item.</param>
        private async void OnProductSelected(object sender, Syncfusion.Maui.ListView.ItemTappedEventArgs e)
        {
            if (e.DataItem is not Product selectedProduct)
            {
                return;
            }

            // Initialize on-demand for the selected product; view is cached and assigned once
            await _cachedPopupView!.InitializeAsync(selectedProduct);

            // Rely on SfPopup's own overlay/animations; avoid page-level fades
            ProductPopup.Show();

            ProductList.SelectedItem = null;
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            if (_cachedPopupView != null)
            {
                _cachedPopupView.AddToCartRequested -= OnAddToCartRequested;
            }
        }
    }
}
