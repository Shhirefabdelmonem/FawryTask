namespace FawryTask.Models
{
    public interface ICustomer
    {
        string Name { get; }
        decimal Balance { get; }
        bool HasSufficientBalance(decimal amount);
        void DeductBalance(decimal amount);
    }
}