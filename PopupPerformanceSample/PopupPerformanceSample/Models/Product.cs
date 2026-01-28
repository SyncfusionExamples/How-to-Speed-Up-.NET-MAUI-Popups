namespace PopupPerformanceSample.Models;

/// <summary>
/// Represents a product, including descriptive details and pricing information.
/// </summary>
public class Product
{
    /// <summary>
    /// Gets or sets the product name.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the product description.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Image of the product.
    /// </summary>
    public string? Image { get; set; }

    /// <summary>
    /// Gets or sets the base price of the product.
    /// </summary>
    public decimal? Price { get; set; }

    /// <summary>
    /// Gets or sets the URL of the product image.
    /// </summary>
    public string? ImageUrl { get; set; }

    /// <summary>
    /// Gets or sets the minimum price used for displaying the price range.
    /// </summary>
    public decimal? MinPrice { get; set; }

    /// <summary>
    /// Gets or sets the maximum price used for displaying the price range.
    /// </summary>
    public decimal? MaxPrice { get; set; }

    /// <summary>
    /// Gets the formatted price range for the product.
    /// </summary>
    /// <remarks>
    /// If <see cref="MinPrice"/> or <see cref="MaxPrice"/> is not set, the value falls back to <see cref="Price"/>.
    /// Ensures the maximum is not less than the minimum.
    /// </remarks>
    public string PriceRange
    {
        get
        {
            var min = MinPrice ?? Price ?? 0m;
            var max = MaxPrice ?? MinPrice ?? Price ?? 0m;
            if (max < min) max = min;
            return $"Price: {min:C} - {max:C}";
        }
    }
}
