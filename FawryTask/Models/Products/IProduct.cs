namespace FawryTask.Models.Products
{
    public interface IProduct
    {
        string Name { get; }
        decimal Price { get; }
        int Quantity { get; set; }
        bool IsAvailable();
    }
}