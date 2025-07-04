namespace FawryTask.Models
{
    public interface IPerishable
    {
        DateTime ExpirationDate { get; }
        bool IsExpired();
    }
} 