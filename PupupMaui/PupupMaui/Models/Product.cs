namespace PupupMaui.Models;

public class Product
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal? Price { get; set; }
    public string? ImageUrl { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }

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
