namespace PopupPerformanceSample.Views;

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

    private void OnAddToCartClicked(object sender, EventArgs e)
    {
        if (BindingContext is MainViewModel vm)
            vm.OnAddToCart();
    }
}