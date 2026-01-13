
namespace PopupPerformanceSample.Views;

public partial class MainPage : ContentPage
{
    private ProductDetailsPopupView? _popupView;

    public MainPage()
    {
        InitializeComponent();
        var vm = new MainViewModel();
        BindingContext = vm;

        // Create and cache popup content once; assign to ContentTemplate
        _popupView = new ProductDetailsPopupView();
        _popupView.BindingContext = vm; // bind to same VM
        ProductPopup.ContentTemplate = new DataTemplate(() => _popupView);

        vm.RequestOpenPopup += async (s, product) =>
        {
            // ProductDetailsPopupView listens to VM and updates UI
            ProductPopup.Show();
            ProductList.SelectedItem = null;
            await Task.CompletedTask;
        };

        vm.RequestClosePopup += async (s, product) =>
        {
            ProductPopup.IsOpen = false;
            await DisplayAlert("Cart", $"{product.Name} added to cart!", "OK");
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
