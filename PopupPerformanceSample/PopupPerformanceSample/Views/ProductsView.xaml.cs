using Microsoft.Maui.Controls;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using PopupPerformanceSample.Models;

namespace PopupPerformanceSample.Views
{
    /// <summary>
    /// A ContentView rendered inside the popup that shows product details,
    /// available variants, and exposes an event to add the product to the cart.
    /// </summary>
    public partial class ProductDetailsPopupView : ContentView
    {
        /// <summary>
        /// Holds the product currently displayed in the popup.
        /// </summary>
        private Product? _currentProduct;

        /// <summary>
        /// Raised when the user taps the Add to Cart button.
        /// </summary>
        public event EventHandler<Product>? AddToCartRequested;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductDetailsPopupView"/> class.
        /// </summary>
        public ProductDetailsPopupView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes the view with the specified product and populates the variants list.
        /// </summary>
        /// <param name="product">The product to display.</param>
        public async Task InitializeAsync(Product product)
        {
            _currentProduct = product;

            // Simulate asynchronous work such as fetching variant data.
            await Task.Delay(200);

            // Bind once per product; XAML binds Name; set initial price here
            BindingContext = product;
            ProductPrice.Text = $"Price: {product.Price:C}";
            VariantsList.SelectedItem = null;

            var baseName = string.IsNullOrWhiteSpace(product.Name) ? "Product" : product.Name;
            var variants = Enumerable.Range(1, 100)
                                     .Select(i => $"{baseName} - Variant {i}")
                                     .ToList();
            VariantsList.ItemsSource = variants;
        }

        /// <summary>
        /// Handles the Add to Cart button click and raises <see cref="AddToCartRequested"/>.
        /// </summary>
        private void OnAddToCartClicked(object? sender, EventArgs e)
        {
            if (_currentProduct != null)
            {
                AddToCartRequested?.Invoke(this, _currentProduct);
            }
        }

        /// <summary>
        /// Updates the displayed price based on the selected product variant.
        /// </summary>
        private void VariantsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_currentProduct == null)
                return;

            string? variant = null;
            if (e.CurrentSelection != null && e.CurrentSelection.Count > 0)
            {
                variant = e.CurrentSelection[0] as string;
            }
            if (string.IsNullOrEmpty(variant))
                return;

            var index = 1;
            if (VariantsList.ItemsSource is IList<string> items)
            {
                var idx = items.IndexOf(variant);
                index = idx >= 0 ? idx + 1 : 1;
            }

            var basePrice = _currentProduct.Price ?? 0m;
            var newPrice = basePrice + (index * 5m);
            ProductPrice.Text = $"Price: {newPrice:C}";
        }
    }

}
