namespace FawryTask.Models.Products
{
    public interface IPerishable
    {
        DateTime ExpirationDate { get; }
        bool IsExpired();
    }
}