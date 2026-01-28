namespace PopupPerformanceSample;

public partial class ProductDetailsPopupView : ContentView
{
    public ProductDetailsPopupView()
    {
        InitializeComponent();
        BindingContextChanged += (_, __) =>
        {
            if (BindingContext is MainViewModel vm)
            {
                ProductPrice.Text = vm.PriceText;
                vm.PropertyChanged += (s, e) =>
                {
                    if (e.PropertyName == nameof(MainViewModel.PriceText))
                        ProductPrice.Text = vm.PriceText;
                };
            }
        };
    }

    public void PrepareForOpenSfListView()
    {
        if (BindingContext is not MainViewModel vm || VariantsList == null)
            return;

        VariantsList.SelectedItem = null;

        var source = vm.Variants;
        VariantsList.ItemsSource = null;
        VariantsList.ItemsSource = source;
    }

    private void OnAddToCartClicked(object sender, EventArgs e)
    {
        if (BindingContext is MainViewModel vm)
            vm.OnAddToCart();
    }
}