# How-to-do-performance-tuning-in-SfPopup
This demo shows how to do performance tuning in .NET MAUI Popup.

Introduction
Syncfusion .NET MAUI Popup  is a powerful UI element; however, popups can introduce performance challenges when dealing with heavy content or complex layouts. In this blog, we’ll explore how to optimize popup performance and deliver smooth, responsive user experiences using the following practical techniques:
•	On-demand content (lazy initialization)
•	Content caching (reuse across opens)
•	Virtualization for large lists
•	Fluid, Jank‑free animations
•	Content template strategy (proper lifecycle)

.NET MAUI Popup performance patterns
We’ve seen the key techniques to tune popup performance. Let’s integrate .NET MAUI POPUP into a .NET MAUI app and apply these patterns.
Wire up lazy initialization and Caching
Defer building popup content until it is needed to reduce initial load time and memory usage. Create the popup content once and reuse cached popup instances across multiple opens to eliminate redundant initialization. By reusing the same popup instance, you eliminate redundant object creation, reduce garbage collection pressure, and deliver a faster, more consistent user experience. 
Jank free animations in .NET MAUI Popup
Apply lightweight transitions and keep heavy work outside the animation window.
                                      
<syncfusion:SfPopup x:Name="ProductPopup"
                                              IsOpen="False"
                                             WidthRequest="350"
                                             HeightRequest="350"
                                             ShowCloseButton="True"  
                                             HorizontalOptions="Center"
                                             VerticalOptions="Center" />   
                                              

private async void OnProductSelected(object sender, Syncfusion.Maui.ListView.ItemTappedEventArgs e){
    if(e.DataItem==null) return;
    var selectedProduct=e.DataItem as Product;
    if(_cachedPopupView==null){
        _cachedPopupView=new ProductDetailsPopupView();
        _cachedPopupView.AddToCartRequested+=OnAddToCartRequested;
    }
    if(selectedProduct!=null){
        await _cachedPopupView.InitializeAsync(selectedProduct);
    }
    ProductPopup.ContentTemplate=new DataTemplate(()=>_cachedPopupView);
    await this.FadeToAsync(0.8,150);
    ProductPopup.Show();
    await this.FadeToAsync(1,150);
    ProductList.SelectedItem=null;
}

Efficient Rendering of Large Lists in Popup Content Template
Using ContentTemplate instead of direct Content assignment ensures MAUI handles view creation and display more efficiently, giving you cleaner lifecycle management, reduced memory leaks, and a more maintainable architecture. Add a CollectionView or SfListView inside the popup’s ContentTemplate to efficiently render large datasets. By enabling virtualization, only visible items are loaded, ensuring smooth scrolling and minimal memory usage even when handling thousands of records.

// PopupContentview.xaml.cs
public async Task InitializeAsync()
{
    await Task.Delay(500); // Simulate API call
    var items = Enumerable.Range(1, 1000).Select(i => $"Item {i}").ToList();
    VirtualizedList.ItemsSource = items;
}

