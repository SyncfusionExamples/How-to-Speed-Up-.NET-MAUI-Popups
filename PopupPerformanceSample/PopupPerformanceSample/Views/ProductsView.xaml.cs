using Microsoft.Maui.Controls;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using PopupPerformanceSample.Models;

namespace PopupPerformanceSample.Views
{
    public partial class ProductDetailsPopupView : ContentView
    {
        private Product? _currentProduct;
        public event EventHandler<Product>? AddToCartRequested;

        public ProductDetailsPopupView()
        {
            InitializeComponent();
        }

        public async Task InitializeAsync(Product product)
        {
            _currentProduct = product;

            await Task.Delay(200);

            ProductName.Text = product.Name;
            ProductPrice.Text = $"Price: {product.Price:C}";

            var baseName = string.IsNullOrWhiteSpace(product.Name) ? "Product" : product.Name;
            var variants = Enumerable.Range(1, 100)
                                     .Select(i => $"{baseName} - Variant {i}")
                                     .ToList();
            VariantsList.ItemsSource = variants;
        }

        private void OnAddToCartClicked(object? sender, EventArgs e)
        {
            if (_currentProduct != null)
            {
                AddToCartRequested?.Invoke(this, _currentProduct);
            }
        }

        private void VariantsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_currentProduct == null || e.CurrentSelection.FirstOrDefault() is not string variant)
                return;

            var index = 1;
            if (VariantsList.ItemsSource is IEnumerable<string> items)
            {
                var list = items.ToList();
                index = list.IndexOf(variant) + 1;
                if (index <= 0) index = 1;
            }

            var basePrice = _currentProduct.Price ?? 0m;
            var newPrice = basePrice + (index * 5m);
            ProductPrice.Text = $"Price: {newPrice:C}";
        }
    }

}
