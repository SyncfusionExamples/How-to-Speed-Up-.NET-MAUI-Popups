
namespace PopupPerformanceSample;

public partial class MainPage : ContentPage
{
    private readonly MainViewModel _viewModel;
    private ProductDetailsPopupView? _popupView;

    public MainPage()
    {
        InitializeComponent();

        // Create a single VM instance and set as BindingContext
        _viewModel = new MainViewModel();
        BindingContext = _viewModel;

        // Lazy create once and cache; also reset viewport before each open
        _viewModel.RequestOpenPopup += async (s, product) =>
        {

            ProductPopup.Opened += (_, __) =>
            {
                Dispatcher.Dispatch(() => _popupView.PrepareForOpenSfListView());
            };

            if (_popupView == null)
            {
                _popupView = new ProductDetailsPopupView
                {
                    BindingContext = _viewModel
                };
                ProductPopup.ContentTemplate = new DataTemplate(() => _popupView);
            }


            ProductPopup.Show();
            ProductList.SelectedItem = null;
            await Task.CompletedTask;
        };

        _viewModel.RequestClosePopup += async (s, product) =>
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