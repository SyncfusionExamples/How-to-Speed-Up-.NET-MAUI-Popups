# How-to-do-performance-tuning-in-SfPopup
This demo shows how to do performance tuning in .NET MAUI Popup.

## Introduction
Imagine this: your .NET MAUI app looks and feels polished until a popup with heavy content opens and the interface stutters. Animations hitch, scrolling slows down, and the snappy experience you designed suddenly feels fragile. 
 Syncfusion® .NET MAUI Popup is a powerful UI element; however, popups can introduce performance challenges when dealing with heavy content or complex layouts. 

In this blog, we will show you how to supercharge your app performance when used Syncfusion® .NET MAUI Popup using proven techniques below.
•	On-demand content (lazy initialization)
•	Content caching (reuse across opens)
•	Virtualization for large lists
•	Fluid, Jank‑free animations
•	Content template strategy (proper lifecycle)

By applying these strategies, you will achieve faster load times, reduced memory usage, and seamless UI experience across Android, iOS, Windows, and macOS.

## .NET MAUI Popup performance patterns
We have seen the key techniques to tune performance when ued popups. Let us integrate Syncfusion® .NET MAUI Popup into a .NET MAUI app and apply these patterns.
### Wire up lazy initialization and Caching
Defer building popup content until it is needed to reduce initial load time and memory usage. Create the popup content once and reuse cached popup instances across multiple opens to eliminate redundant initialization. By reusing the same popup instance, you eliminate redundant object creation, reduce garbage collection pressure, and deliver a faster, more consistent user experience. 
#### Jank free animations in .NET MAUI Popup
Apply lightweight transitions and keep heavy work outside the animation window.

 ```                                     
<syncfusion:SfPopup x:Name="ProductPopup"
                    IsOpen="False"
                    WidthRequest="350"
                    HeightRequest="350"
                    ShowCloseButton="True"  
                    HorizontalOptions="Center"
                    VerticalOptions="Center" />   
                                              

private async void OnProductSelected(object sender, Syncfusion.Maui.ListView.ItemTappedEventArgs e)
{
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

```

### Efficient Rendering of Large Lists in Popup Content Template
Using ContentTemplate instead of direct Content assignment ensures MAUI handles view creation and display more efficiently, giving you cleaner lifecycle management, reduced memory leaks, and a more maintainable architecture. Add a CollectionView or SfListView inside the popup’s ContentTemplate to efficiently render large datasets. By enabling virtualization, only visible items are loaded, ensuring smooth scrolling and minimal memory usage even when handling thousands of records.

```
// PopupContentview.xaml.cs
public async Task InitializeAsync()
{
    await Task.Delay(500); // Simulate API call
    var items = Enumerable.Range(1, 1000).Select(i => $"Item {i}").ToList();
    VirtualizedList.ItemsSource = items;
}

```

## Requirements to run the demo
To run the demo, refer to [System Requirements for .NET MAUI](https://help.syncfusion.com/maui/system-requirements)

## Troubleshooting:
### Path too long exception
If you are facing path too long exception when building this example project, close Visual Studio and rename the repository to short and build the project.

## License
Syncfusion® has no liability for any damage or consequence that may arise from using or viewing the samples. The samples are for demonstrative purposes. If you choose to use or access the samples, you agree to not hold Syncfusion® liable, in any form, for any damage related to use, for accessing, or viewing the samples. By accessing, viewing, or seeing the samples, you acknowledge and agree Syncfusion®'s samples will not allow you seek injunctive relief in any form for any claim related to the sample. If you do not agree to this, do not view, access, utilize, or otherwise do anything with Syncfusion®'s samples.
