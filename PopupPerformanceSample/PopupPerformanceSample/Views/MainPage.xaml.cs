
namespace PopupPerformanceSample.Views;

public partial class MainPage : ContentPage
{
    private readonly MainViewModel _vm;
    private ProductDetailsPopupView? _popupView;

    public MainPage()
    {
        InitializeComponent();

        // Create a single VM instance and set as BindingContext
        _vm = new MainViewModel();
        BindingContext = _vm;

        // Lazy create once and cache; also reset viewport before each open
        _vm.RequestOpenPopup += async (s, product) =>
        {
            if (_popupView == null)
            {
                _popupView = new ProductDetailsPopupView
                {
                    BindingContext = _vm
                };
                ProductPopup.ContentTemplate = new DataTemplate(() => _popupView);
            }

            ProductPopup.Show();
            ProductList.SelectedItem = null;
            await Task.CompletedTask;
        };

        _vm.RequestClosePopup += async (s, product) =>
        {
            ProductPopup.IsOpen = false;
            await DisplayAlertAsync("Cart", $"{product.Name} added to cart!", "OK");
        };
    }

    private void OnProductSelected(object sender, Syncfusion.Maui.ListView.ItemTappedEventArgs e)
    {
        if (BindingContext is MainViewModel vm && e.DataItem is Models.Product product)
        {
            vm.OnProductTapped(product);
        }
    }
}